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

public partial class RRHH_FormativoRancking : System.Web.UI.Page
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

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Personal()
    {
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_FORMATIVO_FICHA("");
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
    protected void btnmenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/Formativomenu.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void View_MitadDesempenio(object sender, EventArgs e)
    {

        LinkButton LinkButton1 = ((LinkButton)sender);
     
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_FASE = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FASE"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(1, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_MitadSeguimiento(object sender, EventArgs e)
    {

        LinkButton LinkButton2 = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_FASE = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FASE"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(2, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_FinalDesempenio(object sender, EventArgs e)
    {

        LinkButton LinkButton3 = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_FASE = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FASE"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(3, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_FinalSeguimiento(object sender, EventArgs e)
    {

        LinkButton LinkButton4 = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_FASE = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FASE"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(4, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_FIcha(object sender, EventArgs e)
    {

        LinkButton LinkButton5 = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_FASE = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FASE"];

        Session["FICHA"] = IDE_FICHA.ToString();
        Response.Redirect("~/RRHH/FormativoFicha.aspx");
    }


    protected void btnReporte_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/FormativoRpt.aspx");
    }
}