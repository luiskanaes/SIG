using BusinessEntity;
using BusinessLogic;
using UserCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;

public partial class OPERACIONES_ReporteHistoricoTareo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TareoSSK"].ToString());
    string INICIO, CENTRO_COSTO, FIN;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            INICIO = Request.QueryString["INICIO"];
            FIN = Request.QueryString["FIN"];
            CENTRO_COSTO = Request.QueryString["CENTRO_COSTO"];

          
            rpt_cuadro();



        }
    }
    protected void rpt_cuadro()
    {
        INICIO = Request.QueryString["INICIO"];
        FIN = Request.QueryString["FIN"];
        CENTRO_COSTO = Request.QueryString["CENTRO_COSTO"];

      
        
        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.Clear();

       
            FIN = FIN.Replace("/", @"_");
            INICIO = INICIO.Replace("/", @"_");

            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.Reset();


            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = ReportViewer1.LocalReport;
            rep.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/Rp_TareoHistorico.rdlc");
            //rep.SetParameters(param);

            //this.ReportViewer1.LocalReport.RefreshReport();


            //ReportViewer1.LocalReport.EnableExternalImages = true;
            //string imagePath = new Uri(Server.MapPath(FolderFirmas + "44085236.jpg")).AbsoluteUri;
            //ReportParameter parameter = new ReportParameter("Path", imagePath);
            //ReportViewer1.LocalReport.SetParameters(parameter);




            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            DataSet dsGrpSum, dsActPlan, dsProfitDetails,
                dsProfitSum, dsSumHeader, dsDetailsHeader, dsBudCom = null;

            byte[] bytes = ReportViewer1.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + CENTRO_COSTO + "_" + INICIO + "_" + FIN + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();


        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetData()
    {
        INICIO = Request.QueryString["INICIO"];
        FIN = Request.QueryString["FIN"];
        CENTRO_COSTO = Request.QueryString["CENTRO_COSTO"];

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspRPT_TAREO_SEMANA_DE_TRABAJO_WEB", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@INICIO", SqlDbType.VarChar, 20).Value = INICIO;
        cmd.Parameters.Add("@FIN", SqlDbType.VarChar, 20).Value = FIN;
        cmd.Parameters.Add("@CENTRO_COSTO", SqlDbType.VarChar, 20).Value = CENTRO_COSTO;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}