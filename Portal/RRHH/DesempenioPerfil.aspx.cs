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
public partial class RRHH_DesempenioPerfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Anios();
            Empresas();
            int anio = DateTime.Today.Year;
            ddlanio.SelectedValue = anio.ToString();
            Perfiles();
            Personal();
            Familia();
            Listar();
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
                string strSelectedValue = ddlPersonal.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarPersonal_mando(Session["IDE_USUARIO"].ToString ());
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


            string cleanMessage = "No existe personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresa.DataSource = dtResultado;
            ddlEmpresa.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresa.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresa.DataBind();
            gerencias();
        }
        
    }
    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(1,"", Convert.ToInt32(ddlEmpresa.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlGerencia.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerencia.DataBind();
            centros();
        }
        else
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataBind();

            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }
        
    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(2, ddlGerencia.SelectedValue.ToString(), Convert.ToInt32(ddlEmpresa.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlCentro.DataBind();

        }
        else
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void Anios()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableAnio();
        if (dtResultado.Rows.Count > 0)
        {
            ddlanio.DataSource = dtResultado;
            ddlanio.DataTextField = "ANIO1";
            ddlanio.DataValueField = "ANIO";
            ddlanio.DataBind();

        }
    }
    static DataTable GetTableAnio()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ANIO", typeof(int));
        table.Columns.Add("ANIO1", typeof(string));

        int anio = 0;
        int anioActual = 0;
        anio = DateTime.Today.Year;
        anioActual = DateTime.Today.Year;
        for (int i = 0; i < 6; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
    }
    protected void Perfiles()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlPerfil.DataSource = obj.ListarParametros("IDE_PERFIL", "RRHH_DESEMPENIO_FICHA");
        ddlPerfil.DataTextField = "DES_ASUNTO";
        ddlPerfil.DataValueField = "ID_PARAMETRO";
        ddlPerfil.DataBind();

        ddlPerfil.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void Familia()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlFamilia.DataSource = obj.ListarParametros("IDE_FAMILIA", "RRHH_DESEMPENIO_FICHA");
        ddlFamilia.DataTextField = "RESUMEN1";
        ddlFamilia.DataValueField = "ID_PARAMETRO";
        ddlFamilia.DataBind();

        ddlFamilia.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
    }

    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlPersonal.SelectedIndex < 1)
        {

            cleanMessage = "Seleccionar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }
        else if (ddlPerfil.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar perfil";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlFamilia.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar familia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlGerencia.SelectedItem.ToString() == string.Empty)
        {
            cleanMessage = "Selecciona gerencia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlCentro.SelectedItem.ToString() == string.Empty)
        {
            cleanMessage = "Selecciona centro de costo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {
            BE_RRHH_DESEMPENIO_FICHA oBESol = new BE_RRHH_DESEMPENIO_FICHA();
            oBESol.IDE_DESEMPENIO = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.DNI = ddlPersonal.SelectedValue.ToString();
            oBESol.ANIO = Convert.ToInt32(ddlanio.SelectedValue);
            oBESol.IDE_PERFIL = Convert.ToInt32(ddlPerfil.SelectedValue);
            oBESol.CODIGO_GERENCIA = ddlGerencia.SelectedValue.ToString ();

            oBESol.CCENTRO = ddlCentro.SelectedValue.ToString();
            oBESol.IDE_FAMILIA = Convert.ToInt32(ddlFamilia.SelectedValue);
            oBESol.CARGO = BL_Session.NombreCargo;
            oBESol.USER_REGISTRA = Session["IDE_USUARIO"].ToString();

            int dtrpta = 0;
            dtrpta = new BL_RRHH_DESEMPENIO_FICHA().uspINS_RRHH_DESEMPENIO_PERFIL(oBESol);
            if (dtrpta > 0)
            {
                lblCodigo.Text = string.Empty;
                cleanMessage = "Registro exitoso";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                ddlPersonal.Text = ""; 
                Listar();
            }
        }
    }
    protected void Actualizar(object sender, ImageClickEventArgs e)
    {
        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        //TextBox txtSustento = (TextBox)row.FindControl("txtSustento");

        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_FICHA_ID(Convert.ToInt32(pk));

        if (dtResultado.Rows.Count > 0)
        {
            lblCodigo.Text = dtResultado.Rows[0]["IDE_DESEMPENIO"].ToString();
            ddlanio.SelectedValue = dtResultado.Rows[0]["ANIO"].ToString();
            ddlPersonal.SelectedValue = dtResultado.Rows[0]["DNI"].ToString();
            ddlEmpresa.SelectedValue = dtResultado.Rows[0]["EMPRESA"].ToString();
            gerencias();
            ddlGerencia.SelectedValue = dtResultado.Rows[0]["CODIGO_GERENCIA"].ToString();
            centros();
            ddlCentro.SelectedValue = dtResultado.Rows[0]["CCENTRO"].ToString();

            ddlFamilia.SelectedValue = dtResultado.Rows[0]["IDE_FAMILIA"].ToString();

        }
    }
    protected void Listar()
    {
        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();
     

        if (Convert.ToInt32(ddlPerfil.SelectedIndex) > 0)
        {
            dtResultado = obj.uspSEL_RRHH_DESEMPENIO_FICHA_PERFIL(Convert.ToInt32(ddlanio.SelectedValue), Convert.ToInt32(ddlPerfil.SelectedValue));
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
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Anios();
        //Empresas();
        //int anio = DateTime.Today.Year;
        //ddlanio.SelectedValue = anio.ToString();
        //Perfiles();
        Personal();
        //Familia();
        //Listar();
        lblCodigo.Text = string.Empty;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }
}