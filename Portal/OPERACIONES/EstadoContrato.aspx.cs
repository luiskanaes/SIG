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
public partial class OPERACIONES_EstadoContrato : System.Web.UI.Page
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

            int anio  = DateTime.Today.Year ;
            ddlAnio3.SelectedValue = anio.ToString();
            Anio();
            //mes = DateTime.Today.Month - 1;
            //ddlMes.SelectedValue = mes.ToString();
            Meses();

            int mes = DateTime.Today.Month;
            ddlMes.SelectedValue = mes.ToString();
            ddlMes2.SelectedValue = mes.ToString();
            ddlMes3.SelectedValue = mes.ToString();
            ListarTC();

            Estados();

            btnConsultar.Visible = false;
            btnResumen.Visible = false;
            PnlCust.Visible = false;
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    protected void Estados()
    {
        ddlEstado.DataSource = GetEstados();
        ddlEstado.DataTextField = "ValueMember";
        ddlEstado.DataValueField = "DisplayMember";
        ddlEstado.DataBind();
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
    protected void ListarAnio()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_anio();

        if (dtResultado.Rows.Count > 0)
        {
            ddlAnio3.DataSource = dtResultado;
            ddlAnio3.DataTextField = dtResultado.Columns["ANIO"].ToString();
            ddlAnio3.DataValueField = dtResultado.Columns["INT_EJERCICIO"].ToString();
            ddlAnio3.DataBind();
        }
    }
    protected void Anio()
    {
        ddlAnio.DataSource = GetAnio();
        ddlAnio.DataTextField = "ValueMember";
        ddlAnio.DataValueField = "DisplayMember";
        ddlAnio.DataBind();

        ddlAnio2.DataSource = GetAnio();
        ddlAnio2.DataTextField = "ValueMember";
        ddlAnio2.DataValueField = "DisplayMember";
        ddlAnio2.DataBind();


    
    }
    private DataTable GetAnio()
    {
        DataTable dt = new DataTable();
        int anio;
        anio = DateTime.Today.Year;

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dt.Rows.Add(anio, anio);
        dt.Rows.Add(anio - 1, anio - 1);

        return dt;

    }
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();

        ddlMes2.DataSource = GetMeses();
        ddlMes2.DataTextField = "ValueMember";
        ddlMes2.DataValueField = "DisplayMember";
        ddlMes2.DataBind();

        ddlMes3.DataSource = GetMeses();
        ddlMes3.DataTextField = "ValueMember";
        ddlMes3.DataValueField = "DisplayMember";
        ddlMes3.DataBind();
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

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        if (FileUpload1.FileBytes.Length <= 0)
        {


            string cleanMessage = "Adjuntar archivo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            string path = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(path).ToLower();
            if (ValidaExtension(fileExtension))
            {

                Bulk_Insert();
            }
            else
            {

                string cleanMessage = "Formato no permitido";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    protected void Bulk_Insert()
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName).ToUpper(); 
        string connectionString = string.Empty;


        fileName = fileName.Replace(".XLSX", ".XLS");

        string fullPath = Path.Combine(Server.MapPath("~/File/TEST/"), fileName);

        if (System.IO.File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }


        FileUpload1.SaveAs(fullPath);

        if (fileExtension.ToUpper() == ".XLS")
        {
            connectionString = (Convert.ToString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") + fullPath) + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
        }
        else if (fileExtension.ToUpper() == ".XLSX")
        {
            connectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") + fullPath) + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        }

        OleDbConnection con = new OleDbConnection(connectionString);
        OleDbCommand cmd = new OleDbCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = con;
        OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
        DataTable dtExcelRecords = new DataTable();
        con.Open();
        DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
        cmd.CommandText = (Convert.ToString("SELECT * FROM [") + getExcelSheetName) + "]";
        dAdapter.SelectCommand = cmd;
        dAdapter.Fill(dtExcelRecords);
        con.Close();


        if (dtExcelRecords.Rows.Count > 0)
        {
            string consString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection conx = new SqlConnection(consString))
            {
                conx.Open();
                // Get a reference to a single row in the table. 
                DataRow[] rowArray = dtExcelRecords.Select();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conx))
                {
                    bulkCopy.DestinationTableName = "dbo.TMP_CJI3";

                    try
                    {
                        // Write the array of rows to the destination.
                        //bulkCopy.NotifyAfter = 15000;
                        bulkCopy.BulkCopyTimeout = 5000;
                        bulkCopy.BatchSize = 50000;
                        bulkCopy.WriteToServer(rowArray);

                        BL_CJI3 obj = new BL_CJI3();
                        DataTable dtResultado = new DataTable();
                        dtResultado = obj.USP_REGISTRAR_ESTADO_CONTRATO(Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue));
                        if (dtResultado.Rows.Count > 0)
                        {
                            string estado = dtResultado.Rows[0]["PROCESADO"].ToString();
                            if (estado == "1")
                            {


                                string cleanMessage = "Proceso Satisfactorio : " + Convert.ToString(dtExcelRecords.Rows.Count).ToString() + " registros procesados";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                            }
                            else
                            {


                                string cleanMessage = "El archivo ya se encuentra procesado";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        string cleanMessage = "Error en archivo : " + ex.Message;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }

            }//using

        }
    }
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension)
        {
            case ".xls":
            case ".Xls":
                //case ".Xlsx":
                //case ".XLSX":
                //case ".xlsx":
                return true;
            default:
                return false;
        }
    }
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtTc.Text == string.Empty )
        {
            string cleanMessage = "No se permiten datos vacios";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            GrabarTC();
        }
    }
    protected void GrabarTC()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        int id = Convert.ToInt32(string.IsNullOrEmpty(hdIdTc.Value) ? "0" : hdIdTc.Value);
        dtResultado = obj.Registrar_CJI3_TC(id, Convert.ToDecimal(txtTc.Text), Convert.ToInt32(ddlAnio2.SelectedValue), Convert.ToInt32(ddlMes2.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            ListarTC();

          
            txtTc.Text = string.Empty;
            hdIdTc.Value = string.Empty;

            string cleanMessage = "Registro Satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

    }

    protected void Seleccionar(object sender, ImageClickEventArgs e)
    {
        BL_CJI3 obj = new BL_CJI3();
        ImageButton btnEditar = ((ImageButton)sender);
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_CJI3_TC(Convert.ToInt32(btnEditar.CommandArgument));
        if (dtResultado.Rows.Count > 0)
        {

            hdIdTc.Value = dtResultado.Rows[0]["ID_TC"].ToString();
            txtTc.Text = dtResultado.Rows[0]["DEC_TC"].ToString();
            ddlAnio2.SelectedValue = dtResultado.Rows[0]["INT_ANIO"].ToString();
            ddlMes2.SelectedValue = dtResultado.Rows[0]["INT_MES"].ToString();
            //Meses();
        }

    }
    protected void ListarTC()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarTipodeCambio();
        if (dtResultado.Rows.Count > 0)
        {
            GridTc.DataSource = dtResultado;
            GridTc.DataBind();
        }

    }

    protected void ddlAnio3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarTC();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        txtTc.Text = string.Empty;
        hdIdTc.Value = string.Empty;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        BL_CJI3 obj = new BL_CJI3();
        BL_Seguridad objSeg = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.eliminar_periodo(Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue));
        string estado = dtResultado.Rows[0]["ESTADO"].ToString();
        if (estado == "1")
        {
            objSeg.auditoria_procesos("CJI3", Session["IDE_USUARIO"].ToString(), "");
           
            UC_MessageBox.Show(Page, this.GetType(), "Periodo eliminado satisfactoriamente");
        }
        else
        {
            
            UC_MessageBox.Show(Page, this.GetType(), "No existen registros ha eliminar");
            return;
        }
    }

    protected void btnListar_Click(object sender, ImageClickEventArgs e)
    {
        ListarProyecto();
    }
    protected void ListarProyecto()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarProyecto_CJI3(Convert.ToInt32(ddlAnio3.SelectedValue), Convert.ToInt32(ddlMes3.SelectedValue));

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

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        DatosProyectos();
    }

    protected void btnResumen_Click(object sender, ImageClickEventArgs e)
    {
        rpt_Cuadro();
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
            ExcelHelper.ToExcel(ds, "CJI3_" + ddlMes3.SelectedItem + ".xls", Page.Response);


        }
        else
        {

            string cleanMessage = "Debe seleccionar algún proyecto";
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
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio3.SelectedValue);
        cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes3.SelectedValue);
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
        cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio3.SelectedValue);
        cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes3.SelectedValue);
        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 1).Value = 1;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 10000).Value = lblSplit.Text;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}