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

public partial class OPERACIONES_ReporteBuenasIdeas : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ParametrosEstados();
        
        }
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
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        rpt_cuadro();
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Operaciones/BuenasIdeasBandeja.aspx");
    }
    protected void rpt_cuadro()
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

        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/RptBuenasIdeas.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);

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
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_BUENAS_IDEAS_TODOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@FLG_ETAPAS", SqlDbType.VarChar ,20).Value = ddlEstados.SelectedValue.ToString();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}