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

public partial class OPERACIONES_RptEstadoContrato : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
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
            proyectos();
        }
    }

    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        proyectos();
    }
    protected void proyectos()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_CJI3_CENTROS_X_EMPRESA(ddlEmpresas.SelectedValue.ToString());

        if (dtResultado.Rows.Count > 0)
        {
            ddlProyectos.DataSource = dtResultado;
            ddlProyectos.DataTextField = dtResultado.Columns["DES_PROYECTO"].ToString();
            ddlProyectos.DataValueField = dtResultado.Columns["COD_CENTRO"].ToString();
            ddlProyectos.DataBind();
        }
        else
        {
            ddlProyectos.Items.Clear();
        }
        
    }
    protected void rpt_Presupuesto()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/RptCJI3_Contrato.rdlc");

        DataTable dsVentas = GetDataPresupuesto(ddlProyectos.SelectedValue, string.Empty);
        ReportDataSource datasourceVentas = new ReportDataSource("DsPresupuesto_V", dsVentas);

        if (dsVentas.Rows.Count > 0)
        {
            //ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasourceVentas);
          

        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    //protected void rpt_Contrato()
    //{
    //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
    //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/Rpt_CostoVentas_CJI3.rdlc");

    //    DataTable dsCustomers = GetData_Contrato();
    //    ReportDataSource datasource = new ReportDataSource("Ds_ProyectoCJI3", dsCustomers);

    //    if (dsCustomers.Rows.Count > 0)
    //    {
    //        ReportViewer1.LocalReport.DataSources.Clear();
    //        ReportViewer1.LocalReport.DataSources.Add(datasource);

    //    }
    //    else
    //    {
    //        ReportViewer1.LocalReport.DataSources.Clear();
    //    }
    //}
    private DataTable GetDataPresupuesto(string COD_CENTRO, string GRUPO)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_RPT_PROYECTO_PRESUPUESTO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@COD_CENTRO", SqlDbType.VarChar, 20).Value = COD_CENTRO;
        cmd.Parameters.Add("@GRUPO", SqlDbType.VarChar, 20).Value = GRUPO;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetData_Contrato(string COD_CENTRO, string GRUPO)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_RPT_CJI3_CONTRATO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@COD_CENTRO", SqlDbType.VarChar,20).Value = COD_CENTRO;
        cmd.Parameters.Add("@GRUPO", SqlDbType.VarChar,20).Value = GRUPO;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        rpt_Presupuesto();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        rpt_detalle();
    }

    protected void rpt_detalle()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/RptCJI3_ContratoDetalle.rdlc");

        //DataTable dsVentas = GetData_Contrato(ddlProyectos.SelectedValue, "VENTA");
        //DataTable dsCostos = GetData_Contrato(ddlProyectos.SelectedValue, "COSTO");


        DataTable dsTodos = GetData_Contrato(ddlProyectos.SelectedValue, string.Empty);

        ReportDataSource datasourceTodos = new ReportDataSource("DsEstadoContrato_T", dsTodos);
        //ReportDataSource datasourceVentas = new ReportDataSource("DsEstadoContrato_V", dsVentas);
        //ReportDataSource datasourceCostos = new ReportDataSource("DsEstadoContrato_C", dsCostos);


        if (dsTodos.Rows.Count > 0)
        {

            ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(datasourceVentas);
            //ReportViewer1.LocalReport.DataSources.Add(datasourceCostos);
            ReportViewer1.LocalReport.DataSources.Add(datasourceTodos);
        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
}