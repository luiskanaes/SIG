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
public partial class RRHH_ReconocerRegalos : System.Web.UI.Page
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

        ddlEstados.DataSource = GetEstados();
        ddlEstados.DataTextField = "ValueMember";
        ddlEstados.DataValueField =  "DisplayMember";
        ddlEstados.DataBind();


        Listar();
    }
    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void btnReconocimiento_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/ReconocerBandeja.aspx");
    }
    protected void Listar()
    {
        
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SEL_RRHH_KARDEX_PRODUCTOS(ddlEstados.SelectedValue );
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        
    }
    private DataTable GetEstados()
    {
        DataTable dtName = new DataTable();

        //Add Columns to Table
        dtName.Columns.Add(new DataColumn("DisplayMember"));
        dtName.Columns.Add(new DataColumn("ValueMember"));

       
        dtName.Rows.Add("0", "PENDIENTE");
        dtName.Rows.Add("1", "ENTREGADO");

        return dtName;

    }
    protected void ProcesarReconocimiento(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

     

        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dt = new DataTable();
        dt = obj.ProcesarEntregaPremio(Convert.ToInt32(pk));

        string cleanMessage = "Registro Procesado";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        Listar();

    }

    protected void btnProductos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/ReconocerRegalos.aspx");
    }
}