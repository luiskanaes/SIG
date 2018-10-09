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

public partial class OPERACIONES_EquipoTrabajo : System.Web.UI.Page
{   string IDE_RESPONSABLE = string.Empty;
    public string NuevoPersonal = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            IDE_RESPONSABLE = Session["IDE_RESPONSABLE"].ToString();
            Personal();
            datosResponsable();
            
            //CargarTrabajadores();
            equipo();

        }
    }
    //protected void CargarTrabajadores()
    //{
    //    BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
    //    DataTable dtResultado = new DataTable();
    //    dtResultado = obj.uspINS_EQUIPO_TRABAJO_VARIOS(Convert.ToInt32( Session["IDE_RESPONSABLE"].ToString()), txtCentro.Text, Session["IDE_USUARIO"].ToString());
    //}
    protected void datosResponsable()
    {
        BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_EQUIPO_RESPONSABLE_POR_ID(Convert.ToInt32(Session["IDE_RESPONSABLE"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {

            txtResponsable.Text = dtResultado.Rows[0]["NOMBRE_COMPLETO"].ToString();
            txtCentro.Text = dtResultado.Rows[0]["CENTRO"].ToString();
            txtGerencia.Text = dtResultado.Rows[0]["GERENCIA"].ToString();

        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
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
                NuevoPersonal = ddlPersonal.SelectedValue;
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));



        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        if (ddlPersonal.SelectedValue == string.Empty)
        {
            string cleanMessage = "Falta indicar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
            DataTable dtResultado = new DataTable();

            dtResultado = obj.uspSEL_EQUIPO_TRABAJO_ACTIVO(ddlPersonal.SelectedValue.ToString(),0);
            if (dtResultado.Rows.Count > 0)
            {

                string jefe = dtResultado.Rows[0]["NOMBRE_COMPLETO"].ToString();
                string centro = dtResultado.Rows[0]["CC_SUPERVISOR"].ToString();
                //string gerencia = dtResultado.Rows[0]["GERENCIA"].ToString();

                string cleanMessage = "Personal ya se encuentra asignado en el CC. " + centro + ", responsable " + jefe;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
            else
            {
                Registrar();
            }
           
        }
    }
    protected void Registrar()
    {
        string cleanMessage = string.Empty;
        BE_EQUIPO_TRABAJO oBESol = new BE_EQUIPO_TRABAJO();
        oBESol.IDE_EQUIPO = 0;
        oBESol.FLG_ESTADO = 1; 
        oBESol.USER_REGISTRA = Session["IDE_USUARIO"].ToString();
        oBESol.IDE_RESPONSABLE = Convert.ToInt32( Session["IDE_RESPONSABLE"].ToString());
        oBESol.FLG_ESTADO = 1;
        oBESol.DNI_TRABAJADOR = ddlPersonal.SelectedValue.ToString();

        int dtrpta = 0;
        dtrpta = new BL_EQUIPO_TRABAJO().uspINS_EQUIPO_TRABAJO(oBESol);
        if (dtrpta > 0)
        {

            ddlPersonal.Text = string.Empty;
            cleanMessage = "Registro exitoso";
            equipo();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

    }
    protected void equipo()
    {
        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_EQUIPO_TRABAJO_SUPERVISOR(Convert.ToInt32(Session["IDE_RESPONSABLE"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();


        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspUPD_EQUIPO_TRABAJADOR(Convert.ToInt32(btnEliminar.CommandArgument), 0);
        equipo();

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Equipos.aspx");
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
     

        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();
        

        string cleanMessage = string.Empty;
     
        

        int intContador = 0;

        if (GridView1.Rows.Count == 0)
        {
            cleanMessage= "No existe Registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        foreach (GridViewRow Fila in GridView1.Rows)
        {
            CheckBox ChkBoxCell = ((CheckBox)Fila.FindControl("chkSelect"));
            if (ChkBoxCell.Checked == true)
            {
                intContador += 1;
            }
        }

        if (intContador == 0)
        {
            cleanMessage = "Debe seleccionar al menos un registro.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        int Cod;


        foreach (GridViewRow row in GridView1.Rows)
        {

            CheckBox ChkBoxCell = ((CheckBox)row.FindControl("chkSelect"));
            if (ChkBoxCell.Checked)
            {
                Cod = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0].ToString()); // extrae key
                dtResultado = obj.uspUPD_EQUIPO_TRABAJADOR(Cod, 0);
            }
            ChkBoxCell = null;
        }

        equipo();
    }

    protected void btnLiberar_Click(object sender, EventArgs e)
    {
        if (ddlPersonal.SelectedValue == string.Empty)
        {
            string cleanMessage = "Falta indicar personal responsable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
            DataTable dtResultado = new DataTable();
           
            dtResultado = obj.uspUPD_LIBERAR_EQUIPO_RESPONSABLE(Convert.ToInt32(Session["IDE_RESPONSABLE"].ToString()), ddlPersonal.SelectedValue.ToString());
            datosResponsable();

           string cleanMessage = "Asignación de equipo satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
}