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

public partial class Logistica_AlmacenImportar : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnConsultar);
        if (!Page.IsPostBack)
        {

            string query = "SELECT CONVERT(CHAR(10),MIN([Fecha_salida]),103) AS FECHA FROM LOGISTICA_REG_ALMACEN WHERE YEAR(Fecha_salida) = YEAR(GETDATE())";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            con.Close();

            string fecha = t1.Rows[0]["FECHA"].ToString();
            txtInicio.Text = fecha;
            txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void rpt_Cuadro()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Logistica/Reportes/Rpt_Almacen.rdlc");

        DataTable dsMOI = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsMOI);

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
   

    }
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_LISTAR_REGISTROS_ALMACEN", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FECHA_INI", SqlDbType.VarChar, 10).Value = txtInicio.Text;
        cmd.Parameters.Add("@FECHA_FIN", SqlDbType.VarChar, 10).Value = txtFin.Text;
        cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 10).Value = RdoEmpresa.SelectedValue ;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void Bulk_Insert(FileUpload FileUpload1, string tabla, Image img)
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        string connectionString = string.Empty;

        string fullPath = Path.Combine(Server.MapPath("~/File/"), fileName);

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
            int cantidad = Convert.ToInt32(dtExcelRecords.Columns.Count );

            if (cantidad == 13)
            {
                Bulk_Insert_AlmacenSAP(FileUpload1, "dbo.TMP_LOGISTICA_REG_ALMACEN", img1);
            }
            else
            {

                string consString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection conx = new SqlConnection(consString))
                {
                    conx.Open();
                    // Get a reference to a single row in the table. 
                    DataRow[] rowArray = dtExcelRecords.Select();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conx))
                    {
                        bulkCopy.DestinationTableName = tabla;

                        try
                        {
                            // Write the array of rows to the destination.
                            bulkCopy.BulkCopyTimeout = 5000;
                            bulkCopy.BatchSize = 50000;
                            bulkCopy.WriteToServer(rowArray);
                            img.Visible = true;
                            img.ImageUrl = "~/imagenes/check.png";
                            BL_LOGISTICA obj = new BL_LOGISTICA();
                            DataTable dtb = new DataTable();
                            dtb = obj.ProcesarRegistros_Almacen("");
                        }
                        catch (Exception ex)
                        {
                            img.Visible = true;
                            img.ImageUrl = "~/imagenes/Error.png";
                            EliminarTMP();
                            UC_MessageBox.Show(Page, Page.GetType(), "Error en archivo : " + ex.Message);
                            return;
                        }
                    }
                }
            }
            rpt_Cuadro();
        }
        else
        {
            img.Visible = true;
            img.ImageUrl = "~/imagenes/Error.png";
            EliminarTMP();
            //// hay borrar los datos
            //BL_LOGISTICA obj = new BL_LOGISTICA();
            //obj.Eliminar_CargaIndicadoresRendimiento();
        }
    }
    protected void EliminarTMP()
    {
        int result = 0;
        string sql = "DELETE FROM TMP_LOGISTICA_REG_ALMACEN";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
        {
            try
            {
                conn.Open();
                SqlCommand cmdx = new SqlCommand(sql, conn);
                result = Convert.ToInt32(cmdx.ExecuteScalar());
                cmdx.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
    }
    protected void Bulk_Insert_AlmacenSAP(FileUpload FileUpload1, string tabla, Image img)
    {
        string excelPath = Server.MapPath("~/File/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(excelPath);

        string conString = string.Empty;
        string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        switch (extension)
        {
            case ".xls": //Excel 97-03
                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 or higher
                conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                break;

        }
        conString = string.Format(conString, excelPath);
        using (OleDbConnection excel_con = new OleDbConnection(conString))
        {
            excel_con.Open();
            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            DataTable dtExcelData = new DataTable();

            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.


            dtExcelData.Columns.AddRange(new DataColumn[13] 
            {   new DataColumn("Entrega", typeof(string)),
                new DataColumn("Clase de entrega", typeof(string)),
                new DataColumn("Documento modelo",typeof(string)) ,
                new DataColumn("Material",typeof(string)) ,
                new DataColumn("Denominación",typeof(string)) ,
                new DataColumn("Destinatario mcía.",typeof(string)) ,
                new DataColumn("Nombre destinatario de mercancías",typeof(string)) ,
                new DataColumn("Centro",typeof(string)) ,
                new DataColumn("Almacén",typeof(string)) ,
                new DataColumn("Nº stock especial",typeof(string)) ,
                new DataColumn("Cantidad entrega",typeof(float )) ,
                new DataColumn("Un.medida venta",typeof(string )) ,
                new DataColumn("Fecha salida mcías.",typeof(DateTime))
            });

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
            {
                oda.Fill(dtExcelData);
                int x = Convert.ToInt32(dtExcelData.Rows.Count);
            }
            excel_con.Close();


            string consString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = tabla;
                    try
                    {
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Entrega", "num_entrega");
                        sqlBulkCopy.ColumnMappings.Add("Clase de entrega", "cod_clase");
                        sqlBulkCopy.ColumnMappings.Add("Documento modelo", "num_traslado");
                        sqlBulkCopy.ColumnMappings.Add("Material", "cod_material");
                        sqlBulkCopy.ColumnMappings.Add("Denominación", "gls_material");
                        sqlBulkCopy.ColumnMappings.Add("Destinatario mcía.", "cod_destino");
                        sqlBulkCopy.ColumnMappings.Add("Nombre destinatario de mercancías", "num_doc_compra");
                        sqlBulkCopy.ColumnMappings.Add("Centro", "cod_centro");
                        sqlBulkCopy.ColumnMappings.Add("Almacén", "cod_almacen");
                        sqlBulkCopy.ColumnMappings.Add("Nº stock especial", "cod_pep");
                        sqlBulkCopy.ColumnMappings.Add("Cantidad entrega ", "num_cantidad");
                        sqlBulkCopy.ColumnMappings.Add("Un.medida venta", "cod_unidadUM");
                        sqlBulkCopy.ColumnMappings.Add("Fecha salida mcías.", "Fecha_salida");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        //gvwAsignacion.DataSource = dtExcelData;
                        //gvwAsignacion.DataBind();
                        con.Close();

                        img.Visible = true;
                        img.ImageUrl = "~/imagenes/check.png";
                        BL_LOGISTICA obj = new BL_LOGISTICA();
                        DataTable dtb = new DataTable();
                        dtb = obj.ProcesarRegistros_Almacen(RdoEmpresa.SelectedValue );
                    }
                    catch (Exception ex)
                    {
                        img.Visible = true;
                        img.ImageUrl = "~/imagenes/Error.png";
                        EliminarTMP();
                        UC_MessageBox.Show(Page, Page.GetType(), "Error en archivo : " + ex.Message);
                        return;
                    }
                }
            }
        }
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        if (FileUpload1.FileBytes.Length <= 0)
        {


        }
        else
        {
            string path = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(path).ToLower();
            if (ValidaExtension(fileExtension))
            {

                Bulk_Insert(FileUpload1, "dbo.TMP_LOGISTICA_REG_ALMACEN", img1);
            }
            else
            {
                UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido");
                return;
            }

        }
    }
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension)
        {
            case ".xls":
            case ".Xls":
            case ".Xlsx":
            case ".XLSX":
            case ".xlsx":
                return true;
            default:
                return false;
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        rpt_Cuadro();
    }
}