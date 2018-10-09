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

public partial class OPERACIONES_BuenasIdeasBandeja : System.Web.UI.Page
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
   

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
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
        BL_BUENAS_IDEAS obj = new BL_BUENAS_IDEAS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_BUENAS_IDEAS_TODOS(estado);
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false ;
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

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ver_Sustento(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        Session["IDE_IDEAS"] = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        Response.Redirect("~/operaciones/BuenasideasDetalle.aspx");
    }
    protected void ProcesarReconocimiento(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");
        TextBox txtPunto = (TextBox)row.FindControl("txtPunto");


        //string z= pk + " - " + rb.SelectedValue;
        BL_BUENAS_IDEAS obj = new BL_BUENAS_IDEAS();
        DataTable dt = new DataTable();

        if (rb.SelectedValue == "R")
        {
            ModalRegistro.Show();
            lblCodigo.Text = pk.ToString();
        }
        else if (rb.SelectedValue == "A")
        {
            if (txtPunto.Text == string.Empty)
            {
                string cleanMessage = "Ingresar puntaje de reconocimiento";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                dt = obj.uspSEL_BUENA_IDEA_PROCESAR(Convert.ToInt32(pk), rb.SelectedValue, Convert.ToInt32(txtPunto.Text), "", Session["IDE_USUARIO"].ToString ());

                string cleanMessage = "Registro procesado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                Listar();
            }
        }

    }

    

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        BL_BUENAS_IDEAS obj = new BL_BUENAS_IDEAS();
        DataTable dt = new DataTable();
        if (txtSustento.Text != string.Empty)
        {
            dt = obj.uspSEL_BUENA_IDEA_PROCESAR(Convert.ToInt32(lblCodigo.Text), "R", 0, txtSustento.Text.Trim(), Session["IDE_USUARIO"].ToString ());

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
        Response.Redirect("~/Operaciones/ReporteBuenasIdeas.aspx");
    }
}