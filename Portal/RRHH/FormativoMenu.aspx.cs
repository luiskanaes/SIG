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

public partial class RRHH_FormativoMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {
            Personal();
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
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_FORMATIVO_FICHA_TODOS(string.Empty  );
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRES_COMPLETO";
            ddlPersonal.DataValueField = "IDE_FICHA";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
           

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- No existen fichas registradas ---", ""));

        }
    }
    protected void btnFicha_Click(object sender, EventArgs e)
    {
        Session["FICHA"] = null;
        Response.Redirect("~/RRHH/FormativoFicha.aspx");

    }

    protected void btnRanking_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/FormativoRancking.aspx");
    }

    //protected void btnTrazabilidad_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("");
    //}

    protected void btnRequerimientos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/FormativoFichaBandeja.aspx");
    }

    protected void btnVer_Click(object sender, EventArgs e)
    {
        if (ddlPersonal.SelectedValue != string.Empty )
        {
            Session["FICHA"] = ddlPersonal.SelectedValue.ToString();
            Response.Redirect("~/RRHH/FormativoFicha.aspx");
        }
        else
        {
            string  cleanMessage = "Seleccionar nombre";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
}