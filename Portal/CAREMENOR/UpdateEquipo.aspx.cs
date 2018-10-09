using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
public partial class CAREMENOR_UpdateEquipo : System.Web.UI.Page
{
    string Reqs_ItemSecuencia;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Reqs_ItemSecuencia = Request.QueryString["Reqs_ItemSecuencia"].ToString();

            Listar();
        }
    }
    protected void Listar()
    {
        Reqs_ItemSecuencia = Request.QueryString["Reqs_ItemSecuencia"].ToString();

        BL_TBL_RequerimientoSubDetalle Xobj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dt = new DataTable();
        dt = Xobj.LISTAR_DATOS_EQUIPO(Reqs_ItemSecuencia);
        dt.Rows.Count.ToString();

        if (dt.Rows.Count > 0)
        {

            GridReq.DataSource = dt;
            GridReq.DataBind();


        }
        else
        {
            string cleanMessage = "Falta adjuntar documentación de legajos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

       
    }



    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        Reqs_ItemSecuencia = Request.QueryString["Reqs_ItemSecuencia"].ToString();
        foreach (GridViewRow row in GridReq.Rows)
        {


            DropDownList ddlSubFamilia = ((DropDownList)row.FindControl("ddlSubFamilia"));
            DropDownList ddlMarca = ((DropDownList)row.FindControl("ddlMarca"));
            DropDownList ddlModelo = ((DropDownList)row.FindControl("ddlModelo"));
            TextBox txtCapacidad = ((TextBox)row.FindControl("txtCapacidad"));


            obj.UPD_SEL_TBL_REQUERIMIENTO_EQUIPO_OBRA(
                    Reqs_ItemSecuencia,
                    ddlSubFamilia.SelectedValue.ToString(),
                    ddlMarca.SelectedValue.ToString(),
                    ddlModelo.SelectedValue.ToString(),
                    txtCapacidad.Text
                );


        }
         cleanMessage = "Actualización satisfactoria";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
    }
    protected void GridReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string lblFamilia = (e.Row.FindControl("lblFamilia") as Label).Text;
            DropDownList ddlSubFamilia = (e.Row.FindControl("ddlSubFamilia") as DropDownList);
            ddlSubFamilia.DataSource = GetDataSubFamilia(lblFamilia);
            ddlSubFamilia.DataTextField = "Subf_Descripcion";
            ddlSubFamilia.DataValueField = "Subf_Codigo";
            ddlSubFamilia.DataBind();

            //Add Default Item in the DropDownList
            ddlSubFamilia.Items.Insert(0, new ListItem("---"));

            //Select the Country of Customer in DropDownList
            string SubFamilia = (e.Row.FindControl("lblSubFamilia") as Label).Text;

            try
            {
                if (SubFamilia != string.Empty)
                {
                    ddlSubFamilia.Items.FindByValue(SubFamilia).Selected = true;

                }
            }
            catch (Exception ex)
            {

            }





            DropDownList ddlMarca = (e.Row.FindControl("ddlMarca") as DropDownList);
            ddlMarca.DataSource = GetDataMarca("");
            ddlMarca.DataTextField = "Marc_Descripcion";
            ddlMarca.DataValueField = "Marc_Codigo";
            ddlMarca.DataBind();

            //Add Default Item in the DropDownList
            ddlMarca.Items.Insert(0, new ListItem("---"));

            //Select the Country of Customer in DropDownList
            string Marca = (e.Row.FindControl("lblMarca") as Label).Text;

            try
            {
                if (Marca != string.Empty)
                {
                    ddlMarca.Items.FindByValue(Marca).Selected = true;

                }
            }
            catch (Exception ex)
            {

            }






            DropDownList ddlModelo = (e.Row.FindControl("ddlModelo") as DropDownList);
            ddlModelo.DataSource = GetDataModeo(ddlMarca.SelectedValue, "");
            ddlModelo.DataTextField = "Mode_Descripcion";
            ddlModelo.DataValueField = "Mode_Codigo";
            ddlModelo.DataBind();

            //Add Default Item in the DropDownList
            ddlModelo.Items.Insert(0, new ListItem("---"));

            //Select the Country of Customer in DropDownList
            string Modelo = (e.Row.FindControl("lblModelo") as Label).Text;

            try
            {
                if (Modelo != string.Empty)
                {
                    ddlModelo.Items.FindByValue(Modelo).Selected = true;

                }
            }
            catch (Exception ex)
            {

            }

        }
    }
    private DataTable GetDataSubFamilia(string Fami_Codigo)
    {
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dt = new DataTable();
        dt = obj.SP_BUSCAR_TBL_SubFamilia(Fami_Codigo);
        return dt;
    }
    private DataTable GetDataMarca(string marca)
    {
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dt = new DataTable();
        dt = obj.SP_BUSCAR_TBL_MARCA(marca);
        int x = dt.Rows.Count;
        return dt;
    }

    private DataTable GetDataModeo(string Marc_Codigo, string Mode_Codigo)
    {
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dt = new DataTable();
        dt = obj.SP_BUSCAR_TBL_Modelo(Marc_Codigo, Mode_Codigo);
        return dt;
    }

    protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ImageButton btnSelect = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((DropDownList)sender).NamingContainer;
        DropDownList ddlMarca = (DropDownList)grdrow.FindControl("ddlMarca");

        DropDownList ddlModelo = (DropDownList)grdrow.FindControl("ddlModelo");

        ddlModelo.DataSource = GetDataModeo(ddlMarca.SelectedValue, "");
        ddlModelo.DataTextField = "Mode_Descripcion";
        ddlModelo.DataValueField = "Mode_Codigo";
        ddlModelo.DataBind();


    }
    protected void ddlSubFamilia_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ImageButton btnSelect = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((DropDownList)sender).NamingContainer;
        Label lblFamilia = (Label)grdrow.FindControl("lblFamilia");
        DropDownList ddlSubFamilia = (DropDownList)grdrow.FindControl("ddlSubFamilia");
        DropDownList ddlMarca = (DropDownList)grdrow.FindControl("ddlMarca");
        //ddlMarca.DataSource = GetDataMarca(ddlSubFamilia.SelectedValue);
        //ddlMarca.DataTextField = "Marc_Descripcion";
        //ddlMarca.DataValueField = "Marc_Codigo";
        //ddlMarca.DataBind();

        //Add Default Item in the DropDownList
        //ddlMarca.Items.Insert(0, new ListItem("---"));

        //DropDownList ddlModelo = (DropDownList)grdrow.FindControl("ddlModelo");
        //ddlModelo.DataSource = GetDataModeo(ddlMarca.SelectedValue, "");
        //ddlModelo.DataTextField = "Mode_Descripcion";
        //ddlModelo.DataValueField = "Mode_Codigo";
        //ddlModelo.DataBind();

    }
    protected void GridReq_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Solped";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Datos de atención";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridReq.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}