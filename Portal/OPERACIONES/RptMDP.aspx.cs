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

public partial class OPERACIONES_RptMDP : System.Web.UI.Page
{
    string IDE_PERMISO, USUARIO, USUARIO_ATIENDE;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    string FolderFirmas = ConfigurationManager.AppSettings["FolderFirmas"];
    string UrlFirmas = ConfigurationManager.AppSettings["UrlFirmas"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["IDE_USUARIO"] == null)
        //{
        //    Response.Redirect("~/default.aspx");
        //}
        if (!Page.IsPostBack)
        {
            IDE_PERMISO = Request.QueryString["IDE_PERMISO"];
            USUARIO = Request.QueryString["USUARIO"];
            USUARIO_ATIENDE = Request.QueryString["USUARIO_ATIENDE"];

            Session["IDE_PERMISO"] = Request.QueryString["IDE_PERMISO"];
            Session["USUARIO"] = Request.QueryString["USUARIO"];
            Session["USUARIO_ATIENDE"] = Request.QueryString["USUARIO_ATIENDE"];
            rpt_cuadro();

        
           
        }
    }
    protected void rpt_cuadro()
    {
        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/RptMDP.rdlc");
        string NOMBRE_CORTO = string.Empty;
        string MOTIVO = string.Empty;
        string INICIO = string.Empty;
        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.Clear();



            //ReportParameter param = new ReportParameter("Path", Server.MapPath(@FolderFirmas + "44085236.jpg"), true);
            ////this.ReportViewer1.DataBind(); // Added
            //this.ReportViewer1.LocalReport.SetParameters(param);

            NOMBRE_CORTO =  dsCustomers.Rows[0]["NOMBRE_CORTO"].ToString();
            MOTIVO = dsCustomers.Rows[0]["MOTIVO"].ToString();
            INICIO = dsCustomers.Rows[0]["INICIO"].ToString();
            MOTIVO = MOTIVO.Replace(" ", @"_");
            INICIO = INICIO.Replace("/", @"_"); 

            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.Reset();

            //string FilePath = @"file:\\" + Server.MapPath(FolderFirmas + "44085236.jpg");
            //string FilePath = @"file:\\" + "D:\\CGO\\SIG\\SolSystem\\File\\Firmas\\44085236.jpg";


            //string FilePath1 = "leonssk.png";
            //string FilePath2 = "leonssk.png";


            string FilePath1 = Session["USUARIO"].ToString() + ".jpg";
            string FilePath2 = Session["USUARIO_ATIENDE"].ToString() + ".jpg";

            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("Path", UrlFirmas + FilePath1, true );
            param[1] = new ReportParameter("Path2", UrlFirmas + FilePath2, true);

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = ReportViewer1.LocalReport;
            rep.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/RptMDP.rdlc");
            rep.SetParameters(param);

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

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=MDP_" + NOMBRE_CORTO + "_" + MOTIVO + "_"+ INICIO + "." + extension);
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

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("SP_TBSOLICITUD_PERMISOS_SELECT_DATOS_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@IDE_PERMISO", SqlDbType.VarChar, 20).Value =Convert.ToInt32 ( Session["IDE_PERMISO"].ToString ());
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}