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

public partial class OPERACIONES_RO_PEP : System.Web.UI.Page
{

    public string ControlUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            ControlBotones();
            Previstos();
            //IndicadoresPEP();
            //Listar_IndicadoresPEP();
        }

    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = true;
            btnProyecto.Visible = true;
            btnReporte.Visible = true;
        }
        else
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = false;
            btnProyecto.Visible = false;
            btnReporte.Visible = true;
        }
    }
    protected void btnPEP_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PEP.aspx");
    }
    protected void btnProyecto_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PROYECTOS.aspx");
    }
    protected void btnMantenimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_COSTOS_VENTAS.aspx");
    }
    protected void btnReporte_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_REPORTE.aspx");
    }

    protected void Previstos()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.previsto_Tipo();

        if (dtResultado.Rows.Count > 0)
        {
            ddlPrevisto.DataSource = dtResultado;
            ddlPrevisto.DataTextField = dtResultado.Columns["TIPO_PREVISTO"].ToString();
            ddlPrevisto.DataValueField = dtResultado.Columns["DES_TIPO_PREVISTO"].ToString();
            ddlPrevisto.DataBind();
            IndicadoresPEP();
        }
    }
    protected void IndicadoresPEP()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.previsto_ListaR(ddlPrevisto.SelectedValue);

        if (dtResultado.Rows.Count > 0)
        {
            ddlIndicador.DataSource = dtResultado;
            ddlIndicador.DataTextField = dtResultado.Columns["DES_PREVISTO"].ToString();
            ddlIndicador.DataValueField = dtResultado.Columns["IDE_PREVISTO"].ToString();
            ddlIndicador.DataBind();
            Listar_IndicadoresPEP();

        }
    }
    protected void Listar_IndicadoresPEP()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_PEP(Convert.ToInt32(ddlIndicador.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            GridPEP.DataSource = dtResultado;
            GridPEP.DataBind();
        }
        else
        {
            GridPEP.DataSource = null;
            GridPEP.DataBind();
        }
    }
    protected void ddlPrevisto_SelectedIndexChanged(object sender, EventArgs e)
    {
        IndicadoresPEP();
        //Listar_IndicadoresPEP();
    }
    protected void ddlIndicador_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar_IndicadoresPEP();
    }

    protected void GridPEP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlGridIndicador = (e.Row.FindControl("ddlGridIndicador") as DropDownList);

            BL_RO obj = new BL_RO();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.previsto_ListaR(ddlPrevisto.SelectedValue);

            if (dtResultado.Rows.Count > 0)
            {

                ddlGridIndicador.DataSource = dtResultado;
                ddlGridIndicador.DataTextField = dtResultado.Columns["DES_PREVISTO"].ToString();
                ddlGridIndicador.DataValueField = dtResultado.Columns["IDE_PREVISTO"].ToString();
                ddlGridIndicador.DataBind();
            }



            //Select the Country of Customer in DropDownList
            string country = (e.Row.FindControl("lblIndicador") as Label).Text;
            ddlGridIndicador.Items.FindByValue(country).Selected = true;
        }
    }
    protected void Actualizar_Indicador(object sender, ImageClickEventArgs e)
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();

        ImageButton btnSave = ((ImageButton)sender);
        GridViewRow row = btnSave.NamingContainer as GridViewRow;
        string pk = GridPEP.DataKeys[row.RowIndex].Values[0].ToString();
        DropDownList ddlGridIndicador = (DropDownList)row.FindControl("ddlGridIndicador");
        TextBox txtDescripcionPep = (TextBox)row.FindControl("txtDescripcionPep");


        obj.actualizar_Indicador_PEP(Convert.ToInt32(pk), Convert.ToInt32(ddlGridIndicador.SelectedValue), txtDescripcionPep.Text);
        Listar_IndicadoresPEP();
        UC_MessageBox.Show(Page, Page.GetType(), txtDescripcionPep.Text + " : Se actualizo al indicador " + ddlGridIndicador.SelectedItem);
        return;
    }
}