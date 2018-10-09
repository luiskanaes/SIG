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


public partial class OPERACIONES_RO_REPORTE : System.Web.UI.Page
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
            Empresas();
            ddlMes.SelectedValue = DateTime.Now.Month.ToString();
            Meses();
            Anio();

        }

    }

    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();

    }
    protected void Anio()
    {

        con.Open();
        string query = "SELECT DISTINCT IN_ANIO FROM [RO_PEP_VALORES]";

        SqlCommand cmd = new SqlCommand(query, con);

        DataTable t1 = new DataTable();
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(t1);
        }

        con.Close();

        ddlAnio.DataSource = t1;
        ddlAnio.DataTextField = "IN_ANIO";
        ddlAnio.DataValueField = "IN_ANIO";
        ddlAnio.DataBind();
    }
    private DataTable GetMeses()
    {
        DataTable dtMes = new DataTable();

        //Add Columns to Table
        dtMes.Columns.Add(new DataColumn("DisplayMember"));
        dtMes.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dtMes.Rows.Add(1, "ENERO");
        dtMes.Rows.Add(2, "FEBRERO");
        dtMes.Rows.Add(3, "MARZO");
        dtMes.Rows.Add(4, "ABRIL");
        dtMes.Rows.Add(5, "MAYO");
        dtMes.Rows.Add(6, "JUNIO");
        dtMes.Rows.Add(7, "JULIO");
        dtMes.Rows.Add(8, "AGOSTO");
        dtMes.Rows.Add(9, "SETIEMBRE");
        dtMes.Rows.Add(10, "OCTUBRE");
        dtMes.Rows.Add(11, "NOVIEMBRE");
        dtMes.Rows.Add(12, "DICIEMBRE");

        return dtMes;

    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = true;
            btnProyecto.Visible = true;
            btnReporte.Visible = true;
        }
        else
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = false;
            btnProyecto.Visible = false;
            btnReporte.Visible = true;
        }
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
            ListarProyectos();
        }
    }
    protected void ListarProyectos()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        // lista solo los proyecto en cierre y ejecucion
        dtResultado = obj.ListarProyectos_Estados_RO(Convert.ToInt32(ddlEmpresas.SelectedValue), "1,2,");
        if (dtResultado.Rows.Count > 0)
        {
            ddlProyecto.Visible = true;
            ddlProyecto.DataSource = dtResultado;
            ddlProyecto.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlProyecto.DataValueField = dtResultado.Columns["IDE_PROYECTO"].ToString();
            ddlProyecto.DataBind();
            //ReportViewer1.Visible = true;
        }
        else
        {
            ddlProyecto.Visible = false;

            //ReportViewer1.Visible = false;
            string cleanMessage = "No se registra informacion";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }


    }
    protected void btnPEP_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PEP.aspx");
    }
    protected void btnProyecto_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PROYECTOS.aspx");
    }
    protected void btnMantenimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_COSTOS_VENTAS.aspx");
    }
    protected void btnReporte_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_REPORTE.aspx");
    }
    protected void rpt_Cuadro()
    {
        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/Rp_RO_Tipo.rdlc");

        //DataTable dsCustomers = GetDataVentas();
        //ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);
        ////Ds_AcumuladoPresente
        //DataTable dsCostos = GetDataCostos();
        //ReportDataSource datasourceCostos = new ReportDataSource("DataSet2", dsCostos);

        //DataTable dsAcumualdos = GetDataAcumulados();
        //ReportDataSource datasourceAcumulado = new ReportDataSource("DataSet3", dsAcumualdos);

        //ReportViewer1.LocalReport.DataSources.Clear();
        //if (dsCustomers.Rows.Count > 0)
        //{

        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(datasource);
        //    ReportViewer1.LocalReport.DataSources.Add(datasourceCostos);
        //    ReportViewer1.LocalReport.DataSources.Add(datasourceAcumulado);
        //}
        //else
        //{

        //    ReportViewer1.LocalReport.DataSources.Clear();
        //}
    }
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_REPORTE_RESULTADO_RO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 20).Value = ddlProyecto.SelectedValue;
        cmd.Parameters.Add("@FECHA_CONSULTA", SqlDbType.VarChar, 10).Value = "01/0" + ddlMes.SelectedValue + "/" + ddlAnio.SelectedValue;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetDataVentas()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_REPORTE_RESULTADO_RO_TIPO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 20).Value = ddlProyecto.SelectedValue;
        cmd.Parameters.Add("@FECHA_CONSULTA", SqlDbType.VarChar, 10).Value = "01/0" + ddlMes.SelectedValue + "/" + ddlAnio.SelectedValue;
        cmd.Parameters.Add("@DES_TIPO_PREVISTO ", SqlDbType.VarChar, 20).Value = "1";
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetDataCostos()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_REPORTE_RESULTADO_RO_TIPO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 20).Value = ddlProyecto.SelectedValue;
        cmd.Parameters.Add("@FECHA_CONSULTA", SqlDbType.VarChar, 10).Value = "01/0" + ddlMes.SelectedValue + "/" + ddlAnio.SelectedValue;
        cmd.Parameters.Add("@DES_TIPO_PREVISTO ", SqlDbType.VarChar, 20).Value = "2";
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetDataAcumulados()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_REPORTE_RESULTADO_RO_ACUMULADOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 20).Value = ddlProyecto.SelectedValue;
        cmd.Parameters.Add("@FECHA_CONSULTA", SqlDbType.VarChar, 10).Value = "01/0" + ddlMes.SelectedValue + "/" + ddlAnio.SelectedValue;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarProyectos();

    }

    protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
    {
        rpt_Cuadro();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        rpt_Cuadro();
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        rpt_Cuadro();
    }
}