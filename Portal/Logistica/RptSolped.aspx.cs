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

public partial class Logistica_RptSolped : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    string codigo;
    string Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        codigo = Session["CODIGO"].ToString();
        Id = Session["ID"].ToString();
     
        if (!Page.IsPostBack)
        {
            rpt_Cuadro();
        }
    }
    protected void rpt_Cuadro()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Logistica/reportes/RptSolped.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DsSolped", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspINS_SOLPED_LISTA_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@COD_PEDIDO", SqlDbType.VarChar,20).Value = codigo;
        cmd.Parameters.Add("@IDE_SOLICITUD", SqlDbType.Int ).Value = Convert.ToInt32(Id);

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}