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

public partial class RRHH_ReconocerPremios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            bravo();
            ValorProducto();
        }
    }
    protected void bravo()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SEL_RRHH_PERSONAL_EMPRESA_POR_ID(Session["IDE_USUARIO"].ToString ());
        if (dtResultado.Rows.Count > 0)
        {
            txtPunto.Text = dtResultado.Rows[0]["PUNTO_BRAVO"].ToString();
            ListView1.Enabled = true ;
        }
        else
        {
            ListView1.Enabled = false;
        }

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarProductos();
    }
    protected void ValorProducto()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        ddlProductos.DataSource = obj.SEL_RRHH_PRODUCTO_TIPOS();
        ddlProductos.DataTextField = "Puntos";
        ddlProductos.DataValueField = "Puntos";
        ddlProductos.DataBind();

        ddlProductos.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void ListarProductos()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dt = new DataTable();
        if (ddlProductos.SelectedIndex > 0)
        {
            dt = obj.SEL_RRHH_PRODUCTO_POR_PUNTOS(Convert.ToDecimal (ddlProductos.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                ListView1.DataSource = dt;
                ListView1.DataBind();

            }
            else
            {
                ListView1.DataSource = dt;
                ListView1.DataBind();
            }
        }
        else
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }

    }
    protected void SeleccionarPremio(object sender, EventArgs e)
    {

        Button btnPremio = ((Button)sender);
        int item = Convert.ToInt32(btnPremio.CommandArgument);
        ListViewItem CommentItem = btnPremio.NamingContainer as ListViewItem;
        decimal  pto = (Decimal)ListView1.DataKeys[CommentItem.DisplayIndex].Values["Puntos"];



        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SEL_RRHH_PERSONAL_EMPRESA_POR_ID(Session["IDE_USUARIO"].ToString());
        decimal puntos =Convert.ToDecimal ( dtResultado.Rows[0]["PUNTO_BRAVO"].ToString());

        if (puntos < pto)
        {
            string cleanMessage = "Puntos insuficiente";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            string cleanMessage = "Pronto te avisaremos cuándo puedes recoger tu premio, gracias";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            obj.KARDEX_PRODUCTO_SOLICITA(Session["IDE_USUARIO"].ToString(), item, "SALIDA", 0, Decimal.ToInt32(pto));
            bravo();
        }
    }
}