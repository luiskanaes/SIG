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

public partial class OPERACIONES_BuenasideasDetalle : System.Web.UI.Page
{
    int IDE_IDEAS;
    protected void Page_Load(object sender, EventArgs e)
    {
        IDE_IDEAS = Convert.ToInt32(Session["IDE_IDEAS"].ToString());

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Nominaciones();
        }

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    
    protected void Nominaciones()
    {
        BL_BUENAS_IDEAS obj = new BL_BUENAS_IDEAS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_BUENAS_IDEAS_ID(Convert.ToInt32 ( Session["IDE_IDEAS"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {

            ListView1.DataSource = dtResultado;
            ListView1.DataBind();

        }
        else
        {

            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Operaciones/BuenasIdeasBandeja.aspx");
    }
}