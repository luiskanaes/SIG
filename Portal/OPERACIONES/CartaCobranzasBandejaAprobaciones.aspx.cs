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
using System.Drawing;

public partial class OPERACIONES_CartaCobranzasBandejaAprobaciones : System.Web.UI.Page
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
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();
            Estado();
            ddlEstado.SelectedIndex = 2;
            Listar();
        


        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
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
    protected void Listar()
    {

        string Estado = string.Empty;
        if (ddlEstado.SelectedIndex == 0)
        {
            Estado = string.Empty;
         }
        else
        {
            Estado = ddlEstado.SelectedValue.ToString();
        }



        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_APROBACIONES(Convert.ToInt32(ddlanio.SelectedValue), Session["IDE_USUARIO"].ToString(), Estado);
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
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Response.Redirect("~/OPERACIONES/CartaCobranzas.aspx");
    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Response.Redirect("~/OPERACIONES/CartaCobranzasBandeja.aspx");
    }

    protected void btnValidar_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    protected void seleccionar(object sender, EventArgs e)
    {

        ImageButton btnSelect = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        Session["IDE_CARTA_APROBAR"] = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();
        Session["IDE_APROBACION"] = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_APROBACION"].ToString();
        
        Response.Redirect("~/OPERACIONES/CartaCobranzasAprobaciones.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string ESTADO_ING_OPE = DataBinder.Eval(e.Row.DataItem, "ESTADO_ING_OPE").ToString();
            string ESTADO_GERENTE_OPE = DataBinder.Eval(e.Row.DataItem, "ESTADO_GERENTE_OPE").ToString();
            string ESTADO_ING_DEST = DataBinder.Eval(e.Row.DataItem, "ESTADO_ING_DEST").ToString();
            string ESTADO_GERENTE_DEST = DataBinder.Eval(e.Row.DataItem, "ESTADO_GERENTE_DEST").ToString();


            e.Row.Cells[5].BackColor = Color.FromName(ESTADO_ING_OPE);
            e.Row.Cells[6].BackColor = Color.FromName(ESTADO_GERENTE_OPE);
            e.Row.Cells[7].BackColor = Color.FromName(ESTADO_ING_DEST);
            e.Row.Cells[8].BackColor = Color.FromName(ESTADO_GERENTE_DEST);

            //if (  e.Row.Cells[10].Text == "PENDIENTE")
            //{
            //    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
            //    e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
            //}
            
            //else
            //{
            //    e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
            //    e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
        
            //}


        }
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    protected void Estado()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableEstado();
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstado.DataSource = dtResultado;
            ddlEstado.DataTextField = "DESCRIPCION";
            ddlEstado.DataValueField = "ID";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("--- Todos ---", ""));
        }
    }
    static DataTable GetTableEstado()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("DESCRIPCION", typeof(string));


        table.Rows.Add("1", "Revisado");
        table.Rows.Add("0", "Pendiente");




        return table;
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Carta cobranzas";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "De:";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BackColor = System.Drawing.Color.Gray;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Para:";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Estado";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.BackColor = System.Drawing.Color.Purple;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}