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
using System.Text.RegularExpressions;
public partial class RRHH_FormativoFichaBandeja : System.Web.UI.Page
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
    protected void ParametrosEstados()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEstados.DataSource = obj.ListarParametros_orden("ESTADO", "RRHH_SOL_FORMATIVO");
        ddlEstados.DataTextField = "ASUNTO_RESUMEN";
        ddlEstados.DataValueField = "ID_PARAMETRO";
        ddlEstados.DataBind();

        ddlEstados.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        //Listar();
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Listar()
    {
        string estado = string.Empty;
        if (ddlEstados.SelectedIndex == 0)
        {
            estado = "";
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString();
        }
        BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_SOL_FORMATIVO_POR_ESTADO(estado);
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

        BL_PERSONAL ObjEstado = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            dtResultado = ObjEstado.ListarParametros("ESTADO", "RRHH_SOL_FORMATIVO");
            if (dtResultado.Rows.Count > 0)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
            }


        }
    }
    protected void ProcesarReconocimiento(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

        lblCodigo.Text = string.Empty;
        lblvalor.Text = string.Empty;
        txtComentarios.Text = string.Empty;
        txtCorreo.Text = string.Empty;

        if (rb.SelectedValue == "A3")
        {
            
            BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
            DataTable dt = new DataTable();
            dt = obj.uspSEL_RRHH_SOL_FORMATIVO_PROCESAR(Convert.ToInt32(pk), rb.SelectedValue);
            string cleanMessage = "Solicitud procesada";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Listar();
        }
        else if (rb.SelectedValue == "A1")
        {
            lblvalor.Text = rb.SelectedValue.ToString();
            lblCodigo.Text = pk;
            ModalRegistro.Show();
        }
        else if (rb.SelectedValue == "A2")
        {
            BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
            DataTable dt = new DataTable();
            DataTable dtCorreo = new DataTable();
            dt = obj.uspSEL_RRHH_SOL_FORMATIVO_PROCESAR(Convert.ToInt32(pk), rb.SelectedValue);
            dtCorreo = obj.SP_CORREO_FORMATIVO_APROBACIONES(Convert.ToInt32(pk), "A3", "");
            string cleanMessage = "Solicitud procesada";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Listar();
        }
        else if (rb.SelectedValue == "R")
        {
            BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
            DataTable dt = new DataTable();
            DataTable dtCorreo = new DataTable();
            dt = obj.uspSEL_RRHH_SOL_FORMATIVO_PROCESAR(Convert.ToInt32(pk), rb.SelectedValue);
            dtCorreo = obj.SP_CORREO_FORMATIVO_APROBACIONES(Convert.ToInt32(pk), rb.SelectedValue, "");
          
            string cleanMessage = "Solicitud procesada";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Listar();
        }


    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        

        if (txtCorreo.Text != string.Empty )
        {
            Boolean correo = email_bien_escrito(txtCorreo.Text.Trim ());
           
            if (correo == true)
            {
                BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
                DataTable dt = new DataTable();
                DataTable dtCorreo = new DataTable();
                dt = obj.uspSEL_RRHH_FORMATIVO_PROCESAR_AREAS(Convert.ToInt32(lblCodigo.Text), txtComentarios.Text  , lblvalor.Text, Session["IDE_USUARIO"].ToString ());
                dtCorreo = obj.SP_CORREO_FORMATIVO_APROBACIONES(Convert.ToInt32(lblCodigo.Text), "A2", txtCorreo.Text );
                if (dtCorreo.Rows.Count > 0)
                {
                    string cleanMessage = "Solicitud enviada";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                string cleanMessage = "Error! verificar correo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else
        {
            string cleanMessage = "Falta ingresar correo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    private Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }
    protected void VerFicha(object sender, ImageClickEventArgs e)
    {

        ImageButton btnVer = ((ImageButton)sender);
        GridViewRow row = btnVer.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        Session["IDE_FORMATIVO"]= pk;
        Response.Redirect("~/RRHH/FormativoProyectoView.aspx");


    }


    protected void btnmenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/Formativomenu.aspx");

    }

    
   
}