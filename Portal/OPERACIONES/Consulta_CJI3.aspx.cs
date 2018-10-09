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

public partial class OPERACIONES_Consulta_CJI3 : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnConsultar);
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnResumen);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ListarAnio();
            Meses();
            Estados();
            btnConsultar.Visible = false;
            btnResumen.Visible = false;
            txtCustomer.Visible = false;
            CheckProyectos.Visible = false;
            PnlCust.Visible = false;

        }
    }
    protected void ListarAnio()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_anio();

        if (dtResultado.Rows.Count > 0)
        {
            ddlAnio.DataSource = dtResultado;
            ddlAnio.DataTextField = dtResultado.Columns["ANIO"].ToString();
            ddlAnio.DataValueField = dtResultado.Columns["INT_EJERCICIO"].ToString();
            ddlAnio.DataBind();
        }
    }
    protected void ListarProyecto()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarProyecto_CJI3(Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            txtCustomer.Visible = true;
            PnlCust.Visible = true;
            CheckProyectos.Visible = true;
            CheckProyectos.DataSource = dtResultado;
            CheckProyectos.DataTextField = dtResultado.Columns["DES_PROYECTO"].ToString();
            CheckProyectos.DataValueField = dtResultado.Columns["IDPROYECTO"].ToString();
            CheckProyectos.DataBind();

            btnConsultar.Visible = true;
            btnResumen.Visible = true;


        }
        else
        {
            CheckProyectos.DataSource = null;
            CheckProyectos.DataBind();
            btnConsultar.Visible = false;
            btnResumen.Visible = false;
            txtCustomer.Visible = false;
            CheckProyectos.Visible = false;
            PnlCust.Visible = false;

            string cleanMessage = "No existen registro, verificar filtros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();
    }
    protected void Estados()
    {
        ddlEstado.DataSource = GetEstados();
        ddlEstado.DataTextField = "ValueMember";
        ddlEstado.DataValueField = "DisplayMember";
        ddlEstado.DataBind();
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
    private DataTable GetEstados()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add(1, "TODOS");
        dt.Rows.Add(2, "ACTIVOS");
        return dt;

    }
    private DataTable GetCostosVentas()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add(1, "COSTOS");
        dt.Rows.Add(2, "VENTAS");
        return dt;

    }

    protected void rpt_Cuadro()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/Rpt_CJI3.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DsCJI3", dsCustomers);

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
    protected void rpt_CostoVentas()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/Rpt_CostoVentas_CJI3.rdlc");

        DataTable dsCustomers = GetData_CostoVentas();
        ReportDataSource datasource = new ReportDataSource("Ds_ProyectoCJI3", dsCustomers);

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
        SqlCommand cmd = new SqlCommand("USP_LISTAR_CJI3", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = ddlEstado.SelectedValue;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 30).Value = 1;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetData_CostoVentas()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_LISTAR_CJI3_PROYECTOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 1).Value = 1;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 10000).Value = lblSplit.Text;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Carga_CJI3.aspx");
    }


    protected void DatosProyectos()
    {
        int numSelected = 0;
        String datos = string.Empty;
        if (CheckProyectos.SelectedIndex != -1)
        {
            var ds = new DataSet();

            foreach (ListItem li in CheckProyectos.Items)
            {
                if (li.Selected)
                {
                    numSelected = numSelected + 1;

                    ds.Tables.Add(GetData_CostoVentas_Individual(li.Value));
                    //ExcelHelper.ToExcel(ds, "test1.xls", Page.Response);
                }
            }
            ExcelHelper.ToExcel(ds, "CJI3_" + ddlMes.SelectedItem + ".xls", Page.Response);


        }
        else
        {

            string cleanMessage = "Debe Seleccionar algun Proyecto";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }


    }


    private DataTable GetData_CostoVentas_Individual(string proyecto)
    {
        string nombre = proyecto.Replace("/", @"_"); ;
        DataTable dt = new DataTable(nombre);
        SqlCommand cmd = new SqlCommand("USP_LISTAR_CJI3_PROYECTOS_INDIVIDUAL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 1).Value = 1;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 100).Value = proyecto;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }



    protected void btnListar_Click(object sender, ImageClickEventArgs e)
    {
        ListarProyecto();
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        DatosProyectos();
    }
    protected void btnResumen_Click(object sender, ImageClickEventArgs e)
    {

        rpt_Cuadro();
    }
}