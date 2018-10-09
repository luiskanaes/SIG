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
using Microsoft.Reporting.WebForms;
public partial class OPERACIONES_Presupuesto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnConsultar);
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnResumen);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Empresas();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresas.DataSource = dtResultado;
            ddlEmpresas.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresas.DataBind();

            gerencias();



        }
    }
    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA(Convert.ToInt32(ddlEmpresas.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerencia.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerencia.DataBind();
            centros();

        }
        else
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataBind();

            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(2, ddlGerencia.SelectedValue.ToString(), 1);
        dtResultado = obj.uspLISTAR_GERENCIA_X_CENTROS(ddlGerencia.SelectedValue.ToString(), Convert.ToInt32(ddlEmpresas.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["COD_CENTRO"].ToString();
            ddlCentro.DataBind();

        }
        else
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }

    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
    }

    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        BE_CECOS oBESol = new BE_CECOS();
        oBESol.COD_CENTRO = ddlCentro.SelectedValue.ToString();
        oBESol.V_PO_FACTURADO = Convert.ToDecimal(string.IsNullOrEmpty(txtV_PO_FACTURADO.Text) ? "0" : txtV_PO_FACTURADO.Text);
        oBESol.V_PO_PROVISION = Convert.ToDecimal(string.IsNullOrEmpty(txtV_PO_PROVISION.Text) ? "0" : txtV_PO_PROVISION.Text);
        oBESol.V_PO_DESAJUSTE = Convert.ToDecimal(string.IsNullOrEmpty(txtV_PO_DESAJUSTE.Text) ? "0" : txtV_PO_DESAJUSTE.Text);
        oBESol.V_POM_FACTURADO = Convert.ToDecimal(string.IsNullOrEmpty(txtV_POM_FACTURADO.Text) ? "0" : txtV_POM_FACTURADO.Text);
        oBESol.V_POM_PROVISION = Convert.ToDecimal(string.IsNullOrEmpty(txtV_POM_PROVISION.Text) ? "0" : txtV_POM_PROVISION.Text);
        oBESol.V_POM_DESAJUSTE = Convert.ToDecimal(string.IsNullOrEmpty(txtV_POM_DESAJUSTE.Text) ? "0" : txtV_POM_DESAJUSTE.Text);
        oBESol.C_PO_PROVISION = Convert.ToDecimal(string.IsNullOrEmpty(txtC_PO_PROVISION.Text) ? "0" : txtC_PO_PROVISION.Text);
        oBESol.C_PO_DIRECTO = Convert.ToDecimal(string.IsNullOrEmpty(txtC_PO_DIRECTO.Text) ? "0" : txtC_PO_DIRECTO.Text);
        oBESol.C_PO_INDIRECTO = Convert.ToDecimal(string.IsNullOrEmpty(txtC_PO_INDIRECTO.Text) ? "0" : txtC_PO_INDIRECTO.Text);
        oBESol.C_PO_MATERIAL = Convert.ToDecimal(string.IsNullOrEmpty(txtC_PO_MATERIAL.Text) ? "0" : txtC_PO_MATERIAL.Text);
        oBESol.C_PO_SUBCONTRATO = Convert.ToDecimal(string.IsNullOrEmpty(txtC_PO_SUBCONTRATO.Text) ? "0" : txtC_PO_SUBCONTRATO.Text);
        oBESol.C_POM_PROVISION = Convert.ToDecimal(string.IsNullOrEmpty(txtC_POM_PROVISION.Text) ? "0" : txtC_POM_PROVISION.Text);
        oBESol.C_POM_DIRECTO = Convert.ToDecimal(string.IsNullOrEmpty(txtC_POM_DIRECTO.Text) ? "0" : txtC_POM_DIRECTO.Text);
        oBESol.C_POM_INDIRECTO = Convert.ToDecimal(string.IsNullOrEmpty(txtC_POM_INDIRECTO.Text) ? "0" : txtC_POM_INDIRECTO.Text);
        oBESol.C_POM_MATERIAL = Convert.ToDecimal(string.IsNullOrEmpty(txtC_POM_MATERIAL.Text) ? "0" : txtC_POM_MATERIAL.Text);
        oBESol.C_POM_SUBCONTRATO = Convert.ToDecimal(string.IsNullOrEmpty(txtC_POM_SUBCONTRATO.Text) ? "0" : txtC_POM_SUBCONTRATO.Text);

        int dtrpta = 0;
        dtrpta = new BL_CECOS().uspUPD_CECOS_PRESUPUESTO(oBESol);
        if (dtrpta > 0)
        {
           

            cleanMessage = "Registro exitoso.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            
        }
    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Datos();
    }
    protected void Datos()
    {
        DataTable dtResultado = new DataTable();
        BL_CECOS obj = new BL_CECOS();
        dtResultado = obj.uspSEL_CECOS_POR_ID(ddlCentro.SelectedValue);
        if (dtResultado.Rows.Count > 0)
        {

            txtV_PO_FACTURADO.Text = dtResultado.Rows[0]["V_PO_FACTURADO"].ToString();
            txtV_PO_PROVISION.Text = dtResultado.Rows[0]["V_PO_PROVISION"].ToString();
            txtV_PO_DESAJUSTE.Text = dtResultado.Rows[0]["V_PO_DESAJUSTE"].ToString();
            txtV_POM_FACTURADO.Text = dtResultado.Rows[0]["V_POM_FACTURADO"].ToString();
            txtV_POM_PROVISION.Text = dtResultado.Rows[0]["V_POM_PROVISION"].ToString();

            txtV_POM_DESAJUSTE.Text = dtResultado.Rows[0]["V_POM_DESAJUSTE"].ToString();
            txtC_PO_PROVISION.Text = dtResultado.Rows[0]["C_PO_PROVISION"].ToString();
            txtC_PO_DIRECTO.Text = dtResultado.Rows[0]["C_PO_DIRECTO"].ToString();
            txtC_PO_INDIRECTO.Text = dtResultado.Rows[0]["C_PO_INDIRECTO"].ToString();
            txtC_PO_MATERIAL.Text = dtResultado.Rows[0]["C_PO_MATERIAL"].ToString();
            txtC_PO_SUBCONTRATO.Text = dtResultado.Rows[0]["C_PO_SUBCONTRATO"].ToString();
            txtC_POM_PROVISION.Text = dtResultado.Rows[0]["C_POM_PROVISION"].ToString();
            txtC_POM_DIRECTO.Text = dtResultado.Rows[0]["C_POM_DIRECTO"].ToString();
            txtC_POM_INDIRECTO.Text = dtResultado.Rows[0]["C_POM_INDIRECTO"].ToString();
            txtC_POM_MATERIAL.Text = dtResultado.Rows[0]["C_POM_MATERIAL"].ToString();
            txtC_POM_SUBCONTRATO.Text = dtResultado.Rows[0]["C_POM_SUBCONTRATO"].ToString();
            
        }
    }
}