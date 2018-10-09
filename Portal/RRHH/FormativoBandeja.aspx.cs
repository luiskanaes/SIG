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
using System.Diagnostics;
using System.Data.SqlClient;

public partial class RRHH_FormativoBandeja : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {
            ParametrosEstados();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ParametrosEstados()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEstados.DataSource = obj.ListarParametros("ESTADO", "RRHH_COMPETENCIAS_EVAL");
        ddlEstados.DataTextField = "DES_ASUNTO";
        ddlEstados.DataValueField = "ID_PARAMETRO";
        ddlEstados.DataBind();

        ddlEstados.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        //Listar();
    }
    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnMenu_Click(object sender, EventArgs e)
    {

    }
}