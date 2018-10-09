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

public partial class Reporte_MOD_PEP : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    SqlConnection con_prod = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionProd"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnListar);
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnResumen);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ListarAnio();
            Meses();
            Estados();
            Empresas();
            CentroCostos();
            btnConsultar.Visible = false;
            //btnResumen.Visible = false;
            //txtCustomer.Visible = false;
            //CheckProyectos.Visible = false;
            //PnlCust.Visible = false;

        }
    }
     protected void CentroCostos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_centro_Costos(null);
        if (dtResultado.Rows.Count > 0)
        {
            CheckProyectos.DataSource = dtResultado;
            CheckProyectos.DataTextField = "DES_CCOSTO";
            CheckProyectos.DataValueField = "ID_CENTROCOSTO";
            CheckProyectos.DataBind(); 
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

         }

         //DataTable dtEmpresa = new DataTable();

         ////Add Columns to Table
         //dtEmpresa.Columns.Add(new DataColumn("DisplayMember"));
         //dtEmpresa.Columns.Add(new DataColumn("ValueMember"));

         ////Now Add Values

         //dtEmpresa.Rows.Add("01", "SSK");
         //dtEmpresa.Rows.Add("12", "SKEX");

         //ddlEmpresas.DataSource = dtEmpresa;

         //ddlEmpresas.DataTextField = "ValueMember";
         //ddlEmpresas.DataValueField = "DisplayMember";
         //ddlEmpresas.DataBind(); 

     }


    protected void ListarAnio()
    {
        ddlAnio.DataSource = GetAnios();
        ddlAnio.DataTextField = "ValueMember";
        ddlAnio.DataValueField = "DisplayMember";
        ddlAnio.DataBind();
    }
    protected void ListarProyecto()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarProyecto_CJI3(Convert.ToInt32(ddlAnio.SelectedValue), 0);

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
            //btnResumen.Visible = true;


        }
        else
        {
            CheckProyectos.DataSource = null;
            CheckProyectos.DataBind();
            btnConsultar.Visible = false;
            //btnResumen.Visible = false;
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
        //ddlEstado.DataSource = GetEstados();
        //ddlEstado.DataTextField = "ValueMember";
        //ddlEstado.DataValueField = "DisplayMember";
        //ddlEstado.DataBind();
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

    private DataTable GetAnios()
    {
        DataTable dtAnio = new DataTable();

        //Add Columns to Table
        dtAnio.Columns.Add(new DataColumn("DisplayMember"));
        dtAnio.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dtAnio.Rows.Add(2010, "2010");
        dtAnio.Rows.Add(2011, "2011");
        dtAnio.Rows.Add(2012, "2012");
        dtAnio.Rows.Add(2013, "2013");
        dtAnio.Rows.Add(2014, "2014");
        dtAnio.Rows.Add(2015, "2015"); 
        dtAnio.Rows.Add(2016, "2016"); 
        return dtAnio;

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
    protected void btnListar_Click(object sender, EventArgs e)
    {
        DescargarExcel();
        //ListarProyecto();

    }
    protected void rpt_Cuadro()
    {
        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/Rpt_CJI3.rdlc");

        //DataTable dsCustomers = GetData();
        //ReportDataSource datasource = new ReportDataSource("DsCJI3", dsCustomers);

        //if (dsCustomers.Rows.Count > 0)
        //{
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(datasource);

        //}
        //else
        //{
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //}
    }
    protected void rpt_CostoVentas()
    {
        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/reportes/Rpt_CostoVentas_CJI3.rdlc");

        //DataTable dsCustomers = GetData_CostoVentas();
        //ReportDataSource datasource = new ReportDataSource("Ds_ProyectoCJI3", dsCustomers);

        //if (dsCustomers.Rows.Count > 0)
        //{
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(datasource);

        //}
        //else
        //{
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //}
    }
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_LISTAR_CJI3", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        //cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        //cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = ddlEstado.SelectedValue;
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
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        //cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
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


    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        //String datos = string.Empty;
        //lblSplit.Text = string.Empty;
        //if (CheckProyectos.SelectedIndex != -1)
        //{


        //    foreach (ListItem li in CheckProyectos.Items)
        //    {
        //        if (li.Selected)
        //        {

        //            datos += li.Value + ",";
        //        }
        //        else
        //        {

        //        }

        //    }
        //}
        //else
        //{
        //    UC_MessageBox.Show(Page, this.GetType(), "Debe Seleccionar algun Proyecto");
        //    return;
        //}
        //lblSplit.Text = datos;
        //CheckProyectos = null;
        //rpt_CostoVentas();
        //DatosProyectos();
        DescargarExcel();


        // nuevo query
    }

    protected void DescargarExcel()
    {
        int numSelected = 0;
        String datos = string.Empty;
        if (CheckProyectos.SelectedIndex != -1)
        {
            var ds = new DataSet();
            var dt = new DataTable();

            foreach (ListItem li in CheckProyectos.Items)
            {
                if (li.Selected)
                {
                    numSelected = numSelected + 1;



                    ds.Tables.Add(GetData_HH_MOD(li.Value));
                    //ExcelHelper.ToExcel(ds, "test1.xls", Page.Response);

                    int dsi = 0;

                    dsi = ds.Tables.Count;
                    /*-------------------------------------------------------------------------*/
                    for (int i = 0; i < dsi; i++)
                    {
                        DataTable dt1 = ds.Tables[i];


              

                        foreach (DataRow row in dt1.Rows)
                        { 
                            decimal rowSum = 0;
                            int columna = 9;
                            foreach (DataColumn col in dt1.Columns)
                            {
                                if (!row.IsNull(col))
                                {
                                    string stringValue = row[columna].ToString();
                                    decimal d;
                                    if (decimal.TryParse(stringValue, out d))
                                        rowSum += d;
                                    columna++;
                                }
                                
                                if (columna == 12)
                                {
                                    break;
                                }
                             }
                            row.SetField("TOTAL_COSTO_PEN", rowSum);                            
                        }

                        foreach (DataRow row in dt1.Rows)
                        {
                            decimal rowSum = 0;
                            int columna = 13    ;
                            foreach (DataColumn col in dt1.Columns)
                            {
                                if (!row.IsNull(col))
                                {
                                    string stringValue = row[columna].ToString();
                                    decimal d;
                                    if (decimal.TryParse(stringValue, out d))
                                        rowSum += d;
                                    columna++;
                                }

                                if (columna == 16)
                                {
                                    break;
                                }
                            }
                            row.SetField("TOTAL_HH", rowSum);
                        }


                        /*-------------------------------------------------------------------------*/
                         
                        decimal sum_TOTAL_COSTO_PEN = 0;
                        int num_filas = 2;
                        foreach (DataRow dr in dt1.Rows)
                        {
                            string stringValue = dr["TOTAL_COSTO_PEN"].ToString();
                            decimal d;
                            if (decimal.TryParse(stringValue, out d))
                                sum_TOTAL_COSTO_PEN += d;
                            num_filas++;
                        }

                        // dt1.Columns.Add("Total", typeof(decimal));
 
                        decimal sum_TOTAL_COSTO_HH = 0;
                        int num_filasHH = 2;
                        foreach (DataRow dr in dt1.Rows)
                        {
                            string stringValue = dr["TOTAL_HH"].ToString();
                            decimal d;
                            if (decimal.TryParse(stringValue, out d))
                                sum_TOTAL_COSTO_HH += d;
                            num_filasHH++;
                        }

                        // dt1.Columns.Add("Total", typeof(decimal));
                      
                                               
                    }
                }
            }
            ExcelHelper.ToExcel(ds, "REPORTE_MOD_X_ELEMENTO_PEP" + ddlAnio.SelectedValue + "_"+ddlMes.SelectedItem + ".xls", Page.Response);


        }
        else
        {

            string cleanMessage = "Debe Seleccionar algun Proyecto";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }


    }


    private DataTable GetData_HH_MOD(string proyecto)
    {
        string nombre = proyecto.Replace("/", @"_"); 
        DataTable dt = new DataTable(nombre);
        DataTable dtResultado = new DataTable(nombre);
        SqlCommand cmd = new SqlCommand("PA_CGO_PROCESA_COSTO_MO_MENSUAL", con_prod);
        cmd.CommandTimeout = 99999;
        cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.Add("@P_ORIGEN", SqlDbType.VarChar).Value = "0" + ddlEmpresas.SelectedValue;
        cmd.Parameters.Add("@P_ORIGEN", SqlDbType.VarChar).Value = "12"; //skex
        cmd.Parameters.Add("@P_TIPO_TRABAJADOR", SqlDbType.VarChar).Value = "02";
        cmd.Parameters.Add("@P_CENTRO_COSTO", SqlDbType.VarChar).Value = proyecto;
        cmd.Parameters.Add("@P_ANIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        cmd.Parameters.Add("@P_MES", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        cmd.Parameters.Add("@P_CUENTA_COSTO", SqlDbType.VarChar).Value ="%";
        cmd.Parameters.Add("@P_CATEGORIA", SqlDbType.VarChar).Value = "%";
        cmd.Parameters.Add("@P_CODIGO_TRABAJADOR", SqlDbType.VarChar).Value = "%";
        cmd.Parameters.Add("@P_USUARIO", SqlDbType.VarChar).Value = "VVARGAS";
        cmd.Parameters.Add("@P_MAQUINA", SqlDbType.VarChar).Value = "SSK-ICA";
 
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);
       

        SqlCommand cmd2 = new SqlCommand("SP_CGO_COSTO_MANO_OBRA_V2", con_prod);
        cmd2.CommandTimeout = 99999;
        cmd2.CommandType = CommandType.StoredProcedure; 
        cmd2.Parameters.Add("@P_USUARIO", SqlDbType.VarChar).Value = "VVARGAS";
        cmd2.Parameters.Add("@P_MAQUINA", SqlDbType.VarChar).Value = "SSK-ICA";
        cmd2.Parameters.Add("@P_VER_HOMOLOGADO", SqlDbType.VarChar).Value = "1";
         
        SqlDataAdapter da2 = new SqlDataAdapter();
        da2.SelectCommand = cmd2;
         
        da2.Fill(dtResultado);
        return dtResultado;
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
            ExcelHelper.ToExcel(ds, "CJI3_" + "AÑO" + ".xls", Page.Response);


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
        //cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 1).Value = 1;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 100).Value = proyecto;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void btnResumen_Click(object sender, EventArgs e)
    {
        rpt_Cuadro();
    }

}