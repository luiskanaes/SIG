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

public partial class OPERACIONES_Equipolibre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            
            Listar();
      
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    
    
    protected void Listar()
    {
        string NOMBRE = string.Empty;
        if (txtPersonal.Text.Trim()== "")
        {
            NOMBRE = string.Empty;
        }
        else
        {
            NOMBRE = txtPersonal.Text.Trim();
        }

        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_EQUIPO_TRABAJO_LIBRE(NOMBRE);
        lblTotal.Text = " - " + dtResultado.Rows.Count.ToString();
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }

    protected void txtPersonal_TextChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Equipos.aspx");
    }
}