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
public partial class RRHH_frmReporteMOI : System.Web.UI.Page
{

    public string ControlUsuario;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            ControlBotones();
            //rpt_Cuadro();
            Anio();
        }
    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {

        }
        else
        {

        }
    }

    protected void btnPersonal_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmPersonal.aspx");
    }
    protected void btnControl_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmMOI.aspx");
    }
    protected void btnReportes_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmReporteMOI.aspx");
    }
    protected void rpt_Cuadro()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RRHH/Reporte/Rpt_MOI.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);
      

        ReportViewer1.LocalReport.DataSources.Clear();
        if (dsCustomers.Rows.Count > 0)
        {

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        else
        {

            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    protected void rpt_Barra()
    {
        ReportViewer2.ProcessingMode = ProcessingMode.Local;
        ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/RRHH/Reporte/Rpt_MOI_BARRAS.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);


        ReportViewer2.LocalReport.DataSources.Clear();
        if (dsCustomers.Rows.Count > 0)
        {

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datasource);

        }
        else
        {

            ReportViewer2.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_CONTROL_MOI_REPORTE", con);
        cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.Add("@FECHA_INICIO", SqlDbType.Int).Value = Convert.ToInt32 (ddlInicio.SelectedValue );
        //cmd.Parameters.Add("@FECHA_FIN", SqlDbType.Int).Value = Convert.ToInt32(ddlFin.SelectedValue); 

        cmd.Parameters.Add("@FECHA_INI", SqlDbType.VarChar ,10).Value = txtInicio.Text;
        cmd.Parameters.Add("@FECHA_TER", SqlDbType.VarChar ,10).Value = txtFin.Text; 

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void Anio()
    {

        con.Open();
        string query = "SELECT DISTINCT YEAR([FEC_FECHA_APROBACION]) as IN_ANIO FROM [RRHH_MOI] WHERE [FEC_FECHA_APROBACION] IS NOT NULL";

        SqlCommand cmd = new SqlCommand(query, con);

        DataTable t1 = new DataTable();
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(t1);
        }

        con.Close();

        //ddlInicio.DataSource = t1;
        //ddlInicio.DataTextField = "IN_ANIO";
        //ddlInicio.DataValueField = "IN_ANIO";
        //ddlInicio.DataBind();

        //ddlFin.DataSource = t1;
        //ddlFin.DataTextField = "IN_ANIO";
        //ddlFin.DataValueField = "IN_ANIO";
        //ddlFin.DataBind();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    { 
      DateTime inicio =  Convert.ToDateTime( txtInicio.Text);
      DateTime fin =  Convert.ToDateTime(txtFin.Text ) ;
      if (inicio > fin)
      {
          string cleanMessage = "El Periodo Fin no puede ser menor al Periodo Inicio";
          ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
      }
      else
      {
          rpt_Cuadro();
          rpt_Barra();
      }
    }
    protected void btnSeguimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/SeguimientoMOI.aspx");
    }
}