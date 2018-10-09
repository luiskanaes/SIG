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

public partial class OPERACIONES_RptCartaCobranzas : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    string IDE_CARTA, TICKET;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["IDE_USUARIO"] == null)
        //{
        //    Response.Redirect("~/default.aspx");
        //}
        if (!Page.IsPostBack)
        {
            Session["TICKET"] = Request.QueryString["TICKET"].ToString();
            Session["IDE_CARTA"]=  Request.QueryString["IDE_CARTA"].ToString();
            TICKET = Session["TICKET"].ToString();
            IDE_CARTA = Session["IDE_CARTA"].ToString();
            rpt_cuadro();



        }
    }
    protected void rpt_cuadro()
    {
        Session["TICKET"] = Request.QueryString["TICKET"].ToString();
        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/RptMDP.rdlc");
        string NOMBRE_CORTO = string.Empty;
        string MOTIVO = string.Empty;
        string INICIO = string.Empty;
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/RptCartaCobranza.rdlc");


        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);

        DataTable dsCustomers2 = GetDataDetalle();
        ReportDataSource datasource2 = new ReportDataSource("DataSet2", dsCustomers2);

        if (dsCustomers.Rows.Count > 0)
        {

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(datasource2);

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            DataSet dsGrpSum, dsActPlan, dsProfitDetails,
                dsProfitSum, dsSumHeader, dsDetailsHeader, dsBudCom = null;

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=CARTA_" + Session["TICKET"].ToString ()  + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();


        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetDataDetalle()
    {
        Session["IDE_CARTA"] = Request.QueryString["IDE_CARTA"].ToString();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_CARTA_COBRAZAS_DETALLE_RPT", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@IDE_CARTA", SqlDbType.Int).Value = Convert.ToInt32(Session["IDE_CARTA"].ToString());
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetData()
    {
        Session["IDE_CARTA"] = Request.QueryString["IDE_CARTA"].ToString();

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_CARTA_COBRAZAS_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@IDE_CARTA", SqlDbType.Int).Value = Convert.ToInt32(Session["IDE_CARTA"].ToString());
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}