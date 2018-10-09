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
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;
public partial class RRHH_FormativoSolicitud : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["FORMATIVO_PROYECTO"] != null)
        {
            string cleanMessage = "¡Gracias! Pronto te confirmaremos si tu solicitud es aprobada";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        if (!Page.IsPostBack)
        {
            ParametrosCategoria();

        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ParametrosCategoria()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEspecialidad.DataSource = obj.ListarParametros("CATEGORIA", "RRHH_SOLICITUD_TALENTO");
        ddlEspecialidad.DataTextField = "DES_ASUNTO";
        ddlEspecialidad.DataValueField = "ID_PARAMETRO";
        ddlEspecialidad.DataBind();

        ddlEspecialidad.Items.Insert(0, new ListItem("--- Seleccionar cargo---", ""));
    }
    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlEspecialidad.SelectedIndex < 1)
        {

            cleanMessage = "Seleccionar cargo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }
        else
        {
            Session["IDE_CATEGORIA"] = ddlEspecialidad.SelectedValue.ToString();
            Session["NOMBRE_CATEGORIA"] = ddlEspecialidad.SelectedItem.ToString();
            Response.Redirect("~/RRHH/FormativoProyecto.aspx");
        }
    }

    protected void btnIniciar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/formativoBandejaExamen.aspx");
    }
}