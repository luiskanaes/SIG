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


using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class CAREMENOR_RegistroSolped : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        if (!Page.IsPostBack)
        {
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            Listar();
        }
    }
    protected void Listar()
    {
        lblRequ_Numero.Text = Request.QueryString["Requ_Numero"].ToString();
        lblReqd_CodLinea.Text = Request.QueryString["Reqd_CodLinea"].ToString();
        lblReqs_Correlativo.Text = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALQUILER SOLPED", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {

            BL_TBL_RequerimientoSubDetalle Xobj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dt = new DataTable();
            dt = Xobj.LISTAR_GRUPO_LEGAJOFILE(lblRequ_Numero.Text, lblReqd_CodLinea.Text, lblReqs_Correlativo.Text, 1);
            dt.Rows.Count.ToString();

            if (dt.Rows.Count > 0)
            {

                GridReq.DataSource = dt;
                GridReq.DataBind();


            }
            else
            {
                GridReq.DataSource = dt;
                GridReq.DataBind();
                string cleanMessage = "Falta adjuntar documentación de legajos";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

        }
        else
        {
            string cleanMessage = "No tiene permisos para realizar esta operación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblRequ_Numero.Text = string.Empty;
        lblReqd_CodLinea.Text = string.Empty;
        lblReqs_Correlativo.Text = string.Empty;
        txtSolped.Text = string.Empty;
        lblMsg.Text = string.Empty;
        //Listar("", "", "", "", "");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (txtSolped.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar N° SOLPED";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtSolped.Text.Trim().Length < 10)
        {
            cleanMessage = "Falta completar los 10 digitos requeridos para el N° SOLPED";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();


            string Requ_Numero = string.Empty;
            string Reqd_CodLinea = string.Empty;
            string Reqs_Correlativo = string.Empty;

            int CantidadPos = 0;
            int registros = 0;
            foreach (GridViewRow row in GridReq.Rows)
            {
                Requ_Numero = GridReq.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                Reqd_CodLinea = GridReq.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                Reqs_Correlativo = GridReq.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key

                TextBox txtPosAlquiler = ((TextBox)row.FindControl("txtPosAlquiler"));
                TextBox txtPosMov = ((TextBox)row.FindControl("txtPosMov"));

                DropDownList ddlSubFamilia = ((DropDownList)row.FindControl("ddlSubFamilia"));
                DropDownList ddlMarca = ((DropDownList)row.FindControl("ddlMarca"));
                DropDownList ddlModelo = ((DropDownList)row.FindControl("ddlModelo"));
                TextBox txtCapacidad = ((TextBox)row.FindControl("txtCapacidad"));

                if (txtPosAlquiler.Text.Trim() == string.Empty)
                {
                    CantidadPos++;
                }


                else
                {
                    obj.UPD_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_SOLPED(
                            Requ_Numero, Reqd_CodLinea,
                            Reqs_Correlativo, txtSolped.Text.Trim(),
                            txtPosAlquiler.Text,
                            txtPosMov.Text,
                            ddlSubFamilia.SelectedValue.ToString(),
                            ddlMarca.SelectedValue.ToString(),
                            ddlModelo.SelectedValue.ToString(),
                            txtCapacidad.Text
                        );

                    if (Requ_Numero != string.Empty)
                    {
                        registros++;
                    }
                }
            }


            if (registros > 0)
            {
                string url = URLSSK;
                string mensaje = "El Sistema de Equipos Menores SSK informa, se asignó el código Solped " + txtSolped.Text + " a los siguientes requerimientos del proyecto ";
                obj.USP_SEL_TBL_REQUERIMIENTO_CORREO_SOLPED_VARIOS(txtSolped.Text, mensaje, "ALQUILER CARE", url);

                txtSolped.Text = string.Empty;

                cleanMessage = "Registro satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                BL_TBL_RequerimientoSubDetalle Xobj = new BL_TBL_RequerimientoSubDetalle();
                DataTable dt = new DataTable();
                dt = Xobj.LISTAR_GRUPO_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
                dt.Rows.Count.ToString();

                if (dt.Rows.Count > 0)
                {

                    GridReq.DataSource = dt;
                    GridReq.DataBind();


                }
            }
            if (CantidadPos > 0)
            {

                cleanMessage = "Completar todas las posiciones de alquiler";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
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
            catch(Exception ex)
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