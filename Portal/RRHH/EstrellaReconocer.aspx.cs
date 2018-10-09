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
using UserCode;

public partial class RRHH_EstrellaReconocer : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnSustentar);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            CentroCostos();
            ParametrosCompetencia();
            Personal();

         
        }
    }
    protected void CentroCostos()
    {
       
            BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
            DataTable dtResultado = new DataTable();

            dtResultado = obj.USP_ESTRELLA_CC_EVALUADOR(Session["IDE_USUARIO"].ToString(), BL_Session.CENTRO_COSTO);
            if (dtResultado.Rows.Count > 0)
            {

                ddlCentro.DataSource = dtResultado;
                ddlCentro.DataTextField = "CC";
                ddlCentro.DataValueField = "CC_EVALUAR";
                ddlCentro.DataBind();
                ddlCentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));

            }
            else
            {

                ddlCentro.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

            }
        
    }
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersonal"))
            {
                string strSelectedValue = ddlPersonal.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Personal()
    {
        BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultado = new DataTable();

     
          dtResultado = obj.USP_ESTRELLA_CC_PERSONAL(Session["IDE_USUARIO"].ToString(), ddlCentro.SelectedValue.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            //btnIngresar.Visible = true;

        }
        else
        {
            ddlPersonal.Items.Clear();
               ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            //btnIngresar.Visible = false;

            string cleanMessage = "No existe personal a reconocer";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Personal();
    }
    protected void ParametrosCompetencia()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        CheckCompetencia.DataSource = obj.ListarParametros("COMPETENCIAS", "RRHH_COMPETENCIAS_EVAL");
        CheckCompetencia.DataTextField = "DES_ASUNTO";
        CheckCompetencia.DataValueField = "ID_PARAMETRO";
        CheckCompetencia.DataBind();

      
    }

    protected void btnSustentar_Click(object sender, EventArgs e)
    {
        if (ddlPersonal.SelectedValue == string.Empty  )
        {
            string cleanMessage = "Falta seleccionar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.uspSEL_TIPO_EVALUACION_ESTRELLA(Session["IDE_USUARIO"].ToString());
            if (dtResultado.Rows[0]["EVALUACION"].ToString()=="0")
            {
                Reconocimiento();
            }
            else
            {
                Reconocimiento_Obra();
            }
            
        }
    }
    protected void Reconocimiento_Obra()
    {
        String datos = string.Empty;
        lblCodigos.Text = string.Empty;
        //if (CheckCompetencia.SelectedIndex != -1)
            if (CheckCompetencia.SelectedIndex > -1)
            {

            foreach (ListItem li in CheckCompetencia.Items)
            {
                if (li.Selected)
                {
                    datos += li.Value + ",";
                   
                }
            }
            lblCodigos.Text = datos;

            BE_RRHH_ESTRELLA_NOMINACION_OBRA oBESol = new BE_RRHH_ESTRELLA_NOMINACION_OBRA();
            oBESol.IDE_NOMINACION = 0;
            oBESol.DNI_EVALUADO = ddlPersonal.SelectedValue.ToString();
            oBESol.DNI_SUPERVISOR = Session["IDE_USUARIO"].ToString();
            oBESol.FACTORES = datos;

            int dtrpta = 0;
            dtrpta = new BL_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS_OBRA(oBESol);
            if (dtrpta > 0)
            {
                ListarCompetenciaAsignadas_obra();
            }
        }
        else
        {
            string cleanMessage = "Debe seleccionar alguna competencia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void Reconocimiento()
    {
        String datosItem = string.Empty;
        lblCodigos.Text = string.Empty;
        if (CheckCompetencia.SelectedIndex > -1)
        {
            foreach (ListItem li in CheckCompetencia.Items)
            {
                if (li.Selected)
                {
                    datosItem += li.Value + ",";
                   
                }
            }
            lblCodigos.Text = datosItem;
            BE_RRHH_ESTRELLA_NOMINACION oBESol = new BE_RRHH_ESTRELLA_NOMINACION();
            oBESol.IDE_NOMINACION = 0;
            oBESol.DNI_EVALUADO = ddlPersonal.SelectedValue.ToString();
            oBESol.DNI_SUPERVISOR = Session["IDE_USUARIO"].ToString();
            oBESol.FACTORES  = datosItem;
            
            int dtrpta = 0;
            dtrpta = new BL_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS(oBESol);
            if (dtrpta > 0)
            {
                ListarCompetenciaAsignadas();
            }
        }
        else
        {
            string cleanMessage = "Debe seleccionar alguna competencia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void ListarCompetenciaAsignadas_obra()
    {
        BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO_OBRA(ddlPersonal.SelectedValue.ToString(), Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
            btnEnviar.Visible = true;
        }
        else
        {
            btnEnviar.Visible = false;
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
        }
    }
    protected void ListarCompetenciaAsignadas()
    {
        BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO(ddlPersonal.SelectedValue.ToString(),  Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0 )
        {
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
            btnEnviar.Visible = true;
        }
        else
        {
            btnEnviar.Visible = false ;
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
        }
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_TIPO_EVALUACION_ESTRELLA( Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows[0]["EVALUACION"].ToString() == "0")
        {
            Registro();
        }
        else
        {
            Registro_obra();
        }
    }
    protected void Registro_obra()
    {
        string cleanMessage = string.Empty;


        foreach (ListViewDataItem FilaFactor in ListView1.Items)
        {
            TextBox txtsustento = ((TextBox)FilaFactor.FindControl("txtSustento"));
            Label lblIDE_NOMINACION = ((Label)FilaFactor.FindControl("lblIDE_NOMINACION"));
            Label lblDNI_EVALUADO = ((Label)FilaFactor.FindControl("lblDNI_EVALUADO"));
            Label lblIDE_FACTOR = ((Label)FilaFactor.FindControl("lblIDE_FACTOR"));


            if (txtsustento.Text != string.Empty)
            {
                BE_RRHH_ESTRELLA_NOMINACION_OBRA oBESol = new BE_RRHH_ESTRELLA_NOMINACION_OBRA();
                oBESol.IDE_NOMINACION = Convert.ToInt32(lblIDE_NOMINACION.Text);
                oBESol.DNI_EVALUADO = lblDNI_EVALUADO.Text;
                oBESol.DNI_SUPERVISOR = Session["IDE_USUARIO"].ToString();
                oBESol.IDE_FACTOR = Convert.ToInt32(lblIDE_FACTOR.Text);
                oBESol.SUSTENTO = txtsustento.Text.Trim();

                int dtrpta = 0;
                dtrpta = new BL_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION_OBRA(oBESol);
                if (dtrpta > 0)
                {

                    BL_RRHH_ESTRELLA_NOMINACION ob = new BL_RRHH_ESTRELLA_NOMINACION();
                    ob.SP_CORREO_NUEVA_NOMINACION_OBRA(dtrpta);
                    ob.SP_CORREO_NOMINACION_IDE_OBRA(dtrpta);
                    ModalRegistro.Show();

                }
            }
            else
            {
                cleanMessage = "Ingresar sustento de nominación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    protected void Registro()
    {
        string cleanMessage = string.Empty;


        foreach (ListViewDataItem FilaFactor in ListView1.Items)
        {
            TextBox txtsustento = ((TextBox)FilaFactor.FindControl("txtSustento"));
            Label lblIDE_NOMINACION = ((Label)FilaFactor.FindControl("lblIDE_NOMINACION"));
            Label lblDNI_EVALUADO = ((Label)FilaFactor.FindControl("lblDNI_EVALUADO"));
            Label lblIDE_FACTOR = ((Label)FilaFactor.FindControl("lblIDE_FACTOR"));


            if (txtsustento.Text != string.Empty)
            {
                BE_RRHH_ESTRELLA_NOMINACION oBESol = new BE_RRHH_ESTRELLA_NOMINACION();
                oBESol.IDE_NOMINACION = Convert.ToInt32(lblIDE_NOMINACION.Text);
                oBESol.DNI_EVALUADO = lblDNI_EVALUADO.Text;
                oBESol.DNI_SUPERVISOR = Session["IDE_USUARIO"].ToString();
                oBESol.IDE_FACTOR = Convert.ToInt32(lblIDE_FACTOR.Text);
                oBESol.SUSTENTO = txtsustento.Text.Trim();

                int dtrpta = 0;
                dtrpta = new BL_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION(oBESol);
                if (dtrpta > 0)
                {

                    BL_RRHH_ESTRELLA_NOMINACION ob = new BL_RRHH_ESTRELLA_NOMINACION();
                    ob.CORREO_NOMINACION(dtrpta);
                    ob.SP_CORREO_NOMINACION_IDE(dtrpta);
                    //cleanMessage = "Registro exitoso, en poco tiempo estaremos informando sobre la situación de esta nominación, gracias";
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    ModalRegistro.Show();

                }
            }
            else
            {
                cleanMessage = "Ingresar sustento de nominación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    protected void EliminarNominacion(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnEliminarFase = ((ImageButton)sender);


        BL_RRHH_ESTRELLA_NOMINACION objX = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultadoX = new DataTable();
        dtResultadoX = objX.uspSEL_TIPO_EVALUACION_ESTRELLA(Session["IDE_USUARIO"].ToString());
        if (dtResultadoX.Rows[0]["EVALUACION"].ToString() == "0")
        {
            BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID(Convert.ToInt32(btnEliminarFase.CommandArgument));
            if (Convert.ToInt32(dtResultado.Rows[0]["ID"].ToString()) > 0)
            {
                try
                {


                    ListarCompetenciaAsignadas();

                }
                catch (Exception ex)
                {
                    cleanMessage = "Existen problemas con la eliminación de nominaciones";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "No se puede eliminar nominación, en proceso de atención";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else
        {
            BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID_OBRA(Convert.ToInt32(btnEliminarFase.CommandArgument));
            if (Convert.ToInt32(dtResultado.Rows[0]["ID"].ToString()) > 0)
            {
                try
                {


                    ListarCompetenciaAsignadas_obra();

                }
                catch (Exception ex)
                {
                    cleanMessage = "Existen problemas con la eliminación de nominaciones";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "No se puede eliminar nominación, en proceso de atención";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }

        

    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void btnNominaciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/MisNominaciones.aspx");
    }
}