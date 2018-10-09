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
using System.Data.SqlClient;


public partial class OPERACIONES_MOD_BANDEJA : System.Web.UI.Page
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
            ControlCecos();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();
            
            Listar("", "");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }

   
    protected void Listar(string campo, string direccion)
    {
        

        string centro = string.Empty;
        if (ddlcentro.SelectedIndex == 0)
        {
            centro = string.Empty;
        }
        else
        {
            centro = ddlcentro.SelectedValue.ToString();
        }

        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_MOD_REQUERIMIENTO_BANDEJA(txtTicket.Text, ddlcentro.SelectedValue.ToString() , Convert.ToInt32( ddlanio.SelectedValue.ToString()));
        if (dtResultado.Rows.Count > 0)
        {
           


            dlCustomers.DataSource = dtResultado;
            dlCustomers.DataBind();
            
        }
        else
        {
            dlCustomers.DataSource = dtResultado;
            dlCustomers.DataBind();

       
        }
    }
    protected void ControlCecos()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE SELECCION MOD", BL_Session.ID_EMPRESA.ToString());

        if (dtResultado.Rows.Count > 0)
        {
            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "CENTRO";
            ddlcentro.DataValueField = "CENTRO";
            ddlcentro.DataBind();
            //ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
        else
        {

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
        anio = DateTime.Today.Year + 1;
        anioActual = DateTime.Today.Year + 1;
        for (int i = 0; i < 5; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["IDE_MOD"] = null;
        Session["IDE_MOD"] = string.Empty;
        Session.Remove("IDE_MOD");

        Session["IDE_REQUERIMIENTO"] = null;
        Session["IDE_REQUERIMIENTO"] = string.Empty;
        Session.Remove("IDE_REQUERIMIENTO");
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["IDE_MOD"] = null;
        Session["IDE_MOD"] = string.Empty;
        Session.Remove("IDE_MOD");

        Session["IDE_REQUERIMIENTO"] = null;
        Session["IDE_REQUERIMIENTO"] = string.Empty;
        Session.Remove("IDE_REQUERIMIENTO");
        Response.Redirect("~/OPERACIONES/MOD.aspx");

    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "");
    }

   

    protected void ddlcentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "");
    }
  
    protected void dlCustomers_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();

        DataRowView drv = e.Item.DataItem as DataRowView;
        GridView innerDataList = e.Item.FindControl("GridView2") as GridView;
        Label lblIDE_MOD = (Label)e.Item.FindControl("lblIDE_MOD");

        DataTable _DataTable2 = new DataTable();
        _DataTable2 = obj.uspSEL_MOD_REQUERIMIENTO_DETALLE_IDE_MOD(Convert.ToInt32(lblIDE_MOD.Text));
        foreach (DataRow rw in _DataTable2.Rows)
        {
            innerDataList.DataSource = _DataTable2;
            innerDataList.DataBind();
        }
    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEliminar.CommandArgument);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;

        string IDE_REQUERIMIENTO = btnEliminar.CommandArgument.ToString();

        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspDEL_MOD_REQUERIMIENTO_IDE(Convert.ToInt32(IDE_REQUERIMIENTO), Convert.ToInt32(0));
        Listar("", "");
    }
    protected void Ver(object sender, ImageClickEventArgs e)
    {

        ImageButton btnVer = ((ImageButton)sender);
        int item = Convert.ToInt32(btnVer.CommandArgument);
        GridViewRow row = btnVer.NamingContainer as GridViewRow;
        string IDE_REQUERIMIENTO = btnVer.CommandArgument.ToString();

   

        Session["IDE_REQUERIMIENTO"] = IDE_REQUERIMIENTO;
        //Session["IDE_MOD"] = IDE_MOD;
        Response.Redirect("~/OPERACIONES/MOD_PERSONAL.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Listar("", "");
    }
    protected void Datos_Req(object sender, ImageClickEventArgs e)
    {

        ImageButton btn1 = ((ImageButton)sender);
        int item = Convert.ToInt32(btn1.CommandArgument);
        GridViewRow row = btn1.NamingContainer as GridViewRow;
       
        Session["IDE_MOD"] = btn1.CommandArgument.ToString();
        Response.Redirect("~/OPERACIONES/MOD.aspx");
    }
    protected void Eliminar_Req(object sender, ImageClickEventArgs e)
    {

        ImageButton btn2 = ((ImageButton)sender);
        int item = Convert.ToInt32(btn2.CommandArgument);
        GridViewRow row = btn2.NamingContainer as GridViewRow;

      
        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspDEL_MOD_REQUERIMIENTO(0, Convert.ToInt32(btn2.CommandArgument.ToString()));
        Listar("", "");
    }
}