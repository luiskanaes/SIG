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
//using Excel = Microsoft.Office.Interop.Excel;
//using Excel_Worksheet = Microsoft.Office.Interop.Excel.Worksheet;


public partial class Logistica_Indicadores_Carga : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);

        if (FileUpload1.FileBytes.Length <= 0)
        {


        }
        else
        {
            string path = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(path).ToLower();
            if (ValidaExtension(fileExtension))
            {

                Bulk_Insert(FileUpload1, "dbo.TMP_LOG_INDICADORES_ASIGNACION", img1);
            }
            else
            {
                UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido");
                return;
            }

        }
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
                        dtb = obj.indicadores_registro(Session["IDE_USUARIO"].ToString());
                    }
                    catch (Exception ex)
                    {
                        img.Visible = true;
                        img.ImageUrl = "~/imagenes/Error.png";
                        UC_MessageBox.Show(Page, Page.GetType(), "Error en archivo : " + ex.Message);
                        return;
                    }
                }
            }//using
        }
        else
        {
            img.Visible = true;
            img.ImageUrl = "~/imagenes/Error.png";
            // hay borrar los datos
            BL_LOGISTICA obj = new BL_LOGISTICA();
            obj.Eliminar_CargaIndicadoresRendimiento();
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
    protected void btnConsultas_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Logistica/Indicadores_Consulta.aspx");
    }
}