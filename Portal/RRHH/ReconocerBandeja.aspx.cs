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

public partial class RRHH_ReconocerBandeja : System.Web.UI.Page
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
            Listar();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ParametrosEstados()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEstados.DataSource = obj.ListarParametros("ESTADO", "RRHH_COMPETENCIAS_EVAL");
        ddlEstados.DataTextField = "DES_ASUNTO";
        ddlEstados.DataValueField = "ID_PARAMETRO";
        ddlEstados.DataBind();

        ddlEstados.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        //Listar();
    }
    protected void Listar()
    {
        string estado = string.Empty;
        if (ddlEstados.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString(); 
        }
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarRequerimiento(estado);
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

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
   
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        BL_PERSONAL ObjEstado = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            dtResultado = ObjEstado.ListarParametros("ESTADO", "RRHH_COMPETENCIAS_EVAL");
            if (dtResultado.Rows.Count > 0)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
            }


        }
        
    }
    private DataTable GetEstados()
    {
        DataTable dtName = new DataTable();

        //Add Columns to Table
        dtName.Columns.Add(new DataColumn("DisplayMember"));
        dtName.Columns.Add(new DataColumn("ValueMember"));

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.ListarParametros("ESTADO", "RRHH_COMPETENCIAS_EVAL");

        //Now Add Values

        for (int i = 0; i < dtResultado.Rows.Count; i++)
        {
            dtName.Rows.Add(dtResultado.Rows[0]["DES_CAMPO1"].ToString(), dtResultado.Rows[0]["ID_PARAMETRO"].ToString()); 
        }

        return dtName;

    }
    protected void ProcesarReconocimiento(object sender, ImageClickEventArgs e)
    {
 
        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");
        TextBox  txtPunto = (TextBox)row.FindControl("txtPunto");


        //string z= pk + " - " + rb.SelectedValue;
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dt = new DataTable();

        if (rb.SelectedValue == "R")
        {
            ModalRegistro.Show();
            lblCodigo.Text = pk.ToString();
        }
        else if ( rb.SelectedValue == "A")
        {
            if (txtPunto.Text == string.Empty)
            {
                string cleanMessage = "Ingresar puntaje de reconocimiento";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                dt = obj.ProcesarKardex_Competencia(Convert.ToInt32(pk), rb.SelectedValue, Convert.ToInt32(txtPunto.Text), "");

                string cleanMessage = "Registro procesado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                Listar();
            }
        }
        
    }




    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }

    protected void btnProductos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/ReconocerRegalos.aspx");
    }

    protected void btnReconocimiento_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/ReconocerBandeja.aspx");
    }
    protected void Actualizar_Sustento(object sender, ImageClickEventArgs e)
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        TextBox txtSustento = (TextBox)row.FindControl("txtSustento");


        obj.uspUPD_RRHH_COMPETENCIAS_EVAL_SUSTENTO(Convert.ToInt32(pk), txtSustento.Text);
        string cleanMessage = "Sustento actualizado";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        Listar();
    }

    protected void btnCarga_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/ReconocimientoAdjunto.aspx");
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dt = new DataTable();
        if (txtSustento.Text != string.Empty )
        {
            dt = obj.ProcesarKardex_Competencia(Convert.ToInt32(lblCodigo.Text), "R", 100, txtSustento.Text.Trim());

            string cleanMessage = "Registro procesado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            Listar();
        }
        else
        {
            string cleanMessage = "ingresar sustento de rechazo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/BravoReporte.aspx");
    }
}