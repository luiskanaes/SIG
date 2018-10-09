using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;
using System.Data.Odbc;
using BusinessEntity;
using BusinessLogic;


public partial class RRHH_SolicitudReclutamientoAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Listar("","","");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Listar(string nombre,string COD_CENTRO,string TICKET)
    {
        string estado = string.Empty;
        
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_SOLICITUD_ASIGNACION(estado, COD_CENTRO, TICKET);
        if (dtResultado.Rows.Count > 0)
        {
        
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }

    protected void btnImagen_Click(object sender, ImageClickEventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            TextBox txtTICKET_F = (TextBox)GridView1.HeaderRow.FindControl("txtTICKET_F");
            TextBox txtNOMBRE_F = (TextBox)GridView1.HeaderRow.FindControl("txtNOMBRE_F");
            TextBox txtCOD_CENTRO_F = (TextBox)GridView1.HeaderRow.FindControl("txtCOD_CENTRO_F");

            Listar(txtNOMBRE_F.Text.Trim(), txtCOD_CENTRO_F.Text.Trim(), txtTICKET_F.Text.Trim());
        }
        else
        {
           
            Listar("", "", "");
            
        }

      
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("IDE_ASIGNACION");
        Response.Redirect("~/RRHH/SolicitudReclutamiento");
    }
    protected void ver_ficha(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        Session["IDE_ASIGNACION"] = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        Response.Redirect("~/RRHH/SolicitudReclutamiento");
    }
    protected void reenviar_aprobacion(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEmail = ((ImageButton)sender);
        GridViewRow row = btnEmail.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        //RECURSOS MOVIL ENVIAMOS NOTIFICACION AL APROBADOR
        BL_RRHH_SOLICITUD_ASIGNACION _obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable _dtResultado = new DataTable();

        _dtResultado = _obj.usp_correo_notificar_apobrador_asignacion(pk, "RECURSOS MOVIL", 1);

        string cleanMessage = "Se envio notificación de aprobación";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
    }

    protected void Anular_requerimiento(object sender, ImageClickEventArgs e)
    {

        ImageButton btnAnular = ((ImageButton)sender);
        GridViewRow row = btnAnular.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        Session["IDE_ASIGNACION"] = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        string CODIGO_CARE_PADRE = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        string cleanMessage;
        int Contador = 0;

        //REVISAMOS SI EXISTE ATENCION EL MOBILE
        BL_MOBILE objMB = new BL_MOBILE();
        DataTable dtMB = new DataTable();
        dtMB = objMB.usp_RequerimientoListado_codigoCare(CODIGO_CARE_PADRE);
        if(dtMB.Rows.Count>0)
        {
            //    IdEstadoRequerimiento 
            //1   Pendiente
            //2   Aprobado
            string IdEstadoRequerimiento = dtMB.Rows[0]["IdEstadoRequerimiento"].ToString(); 
            if (IdEstadoRequerimiento.Trim() != "1")
            {
                Contador++;
            }
           
        }
        

        BL_TBL_RequerimientoSubDetalle objcare = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtcare = new DataTable();
        dtcare = objcare.uspTBL_RequerimientoDetalle_EstadoAtencion(CODIGO_CARE_PADRE);
        if (dtcare.Rows.Count > 0)
        {   //Reqd_flagTemporal 0 es pendiente
            string Reqd_flagTemporal = dtcare.Rows[0]["Reqd_flagTemporal"].ToString();  
            if (Reqd_flagTemporal == "1")
            {
                Contador++;
            }
        }


        if(Contador> 0)
        {
            cleanMessage = "No se puede realizar esta operación, requerimiento atendido";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            Anular(pk);
        }
       
    }
    protected void Ver_recursos(object sender, ImageClickEventArgs e)
    {
        ModalRegistro.Show();
        ImageButton btnRecursos = ((ImageButton)sender);
        GridViewRow row = btnRecursos.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.usp_RRHH_SOLICITUD_RECURSOS(pk);
        if (dtResultado.Rows.Count > 0)
        {

            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
        else
        {

            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
    }
    

    protected void Anular(string pk)
    {
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspANULAR_RRHH_SOLICITUD_ASIGNACION(pk, Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            string CODIGO_CARE_PADRE = dtResultado.Rows[0]["CODIGO_CARE_PADRE"].ToString();

            //MOBILE
            BL_MOBILE objMB = new BL_MOBILE();
            DataTable dtMB = new DataTable();
            dtMB = objMB.uspANULAR_RRHH_SOLICITUD_ASIGNACION(CODIGO_CARE_PADRE);
            if (dtMB.Rows.Count > 0)
            {
                //recorremos los equipos solicitados
                for (int i = 0; i < dtMB.Rows.Count; i++)
                {
                    string IdRequerimiento = dtMB.Rows[i]["IdRequerimiento"].ToString();

                    BL_RRHH_SOLICITUD_ASIGNACION objCare = new BL_RRHH_SOLICITUD_ASIGNACION();
                    DataTable dtCare = new DataTable();
                    dtCare = objCare.uspANULAR_RRHH_SOLICITUD_ASIGNACION_C(CODIGO_CARE_PADRE, IdRequerimiento);
                }
            }
            else
            {
                //anulamos requerimiento del care
                BL_RRHH_SOLICITUD_ASIGNACION objCare = new BL_RRHH_SOLICITUD_ASIGNACION();
                DataTable dtCare = new DataTable();
                dtCare = objCare.uspANULAR_RRHH_SOLICITUD_ASIGNACION_C(CODIGO_CARE_PADRE, "0");
            }

            //NOTIFICAR
            obj.usp_correo_responsable_recursos(pk, "3");
            Listar("", "", "");

            string cleanMessage = "Actualización correcta";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

        }
    }
}