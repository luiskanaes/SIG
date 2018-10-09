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
using Excel = Microsoft.Office.Interop.Excel;
using Excel_Worksheet = Microsoft.Office.Interop.Excel.Worksheet;

public partial class Logistica_ReporteGlobal_Carga : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnProcesar);
        if (!Page.IsPostBack)
        {

        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Logistica/ReporteGlobal_Consulta.aspx");
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        //LOGISTICA_ME5A
        try
        {
            if (FileUpload1.FileBytes.Length <= 0 && FileUpload2.FileBytes.Length <= 0 && FileUpload3.FileBytes.Length <= 0 && FileUpload4.FileBytes.Length <= 0
                && FileUpload5.FileBytes.Length <= 0 && FileUpload6.FileBytes.Length <= 0)
            {


            }
            else
            {
                BL_LOGISTICA obj = new BL_LOGISTICA();
                DataTable dtResultado = new DataTable();
                dtResultado = obj.Eliminar_Tabla_ReporteGeneral();

                string path = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(path).ToLower();
                if (ValidaExtension(fileExtension))
                {

                    Bulk_Insert(FileUpload1, "dbo.LOGISTICA_ME5A", img1);
                }
                else
                {
                    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para ME5A");
                    return;
                }

                ////////////////    ME2N       ///////////////////
                string path2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string fileExtension2 = Path.GetExtension(path2).ToLower();
                if (ValidaExtension(fileExtension2))
                {

                    Bulk_Insert(FileUpload2, "dbo.LOGISTICA_ME2N", img2);
                }
                else
                {
                    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para ME2N");
                    return;

                }

                ////////////////    ZMM033       ///////////////////
                string path3 = Path.GetFileName(FileUpload3.PostedFile.FileName);
                string fileExtension3 = Path.GetExtension(path3).ToLower();
                if (ValidaExtension(fileExtension3))
                {

                    Bulk_Insert(FileUpload3, "dbo.LOGISTICA_ZMM033", img3);
                }
                else
                {
                    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para ZMM033");
                    return;

                }

                ////////////////    ZMM033C       ///////////////////
                string path4 = Path.GetFileName(FileUpload4.PostedFile.FileName);
                string fileExtension4 = Path.GetExtension(path4).ToLower();
                if (ValidaExtension(fileExtension4))
                {

                    Bulk_Insert_Vacios(FileUpload4, "dbo.LOGISTICA_ZMM033", img4);
                    Bulk_Insert_Vacios(FileUpload4, "dbo.LOGISTICA_ZMM033C", img4);
                }
                else
                {
                    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para ZMM033 Concluidas");
                    return;

                }

                ////////////////    ME80FN       ///////////////////
                string path5 = Path.GetFileName(FileUpload5.PostedFile.FileName);
                string fileExtension5 = Path.GetExtension(path5).ToLower();
                if (ValidaExtension(fileExtension5))
                {

                    Bulk_Insert(FileUpload5, "dbo.LOGISTICA_ME80FN_REP", img5);
                }
                else
                {
                    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para ME80FN Repartos");
                    return;

                }


                ////////////////    ME80FN_HIS       ///////////////////
                string path6 = Path.GetFileName(FileUpload6.PostedFile.FileName);
                string fileExtension6 = Path.GetExtension(path6).ToLower();
                if (ValidaExtension(fileExtension6))
                {

                    Bulk_Insert(FileUpload6, "dbo.LOGISTICA_ME80FN_HIS", img6);
                }
                else
                {
                    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para ME80FN Historicos");
                    return;

                }

                ////////////////    SSK       ///////////////////
                //string path7 = Path.GetFileName(FileUpload7.PostedFile.FileName);
                //string fileExtension7 = Path.GetExtension(path7).ToLower();
                //if (ValidaExtension(fileExtension7))
                //{
                //    //Bulk_Insert
                //    Bulk_Insert(FileUpload7, "dbo.LOGISTICA_REG_ALMACEN", img7);
                //}
                //else
                //{
                //    UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido para Almacen SSK");
                //    return;

                //}

                //exportarXLS_Plantilla();
                exportarXLS();
            }

        }

        catch (Exception ex)
        {
            // hay borrar los datos
            BL_LOGISTICA obj = new BL_LOGISTICA();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.Eliminar_Tabla_ReporteGeneral();

            string cleanMessage = ex.Message + " Intente cambiar la extension de los Archivo Almacen a .XLS(libro 97-2003)"; ;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }


    }
    protected void Bulk_Insert_Almacen(FileUpload FileUpload1, string tabla, Image img)
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


            dtExcelData.Columns.AddRange(new DataColumn[19] 
            {   new DataColumn("N° Entrega", typeof(string)),
                new DataColumn("Clase", typeof(string)),
                new DataColumn("N° Traslado",typeof(string)) ,
                new DataColumn("Codigo",typeof(string)) ,
                new DataColumn("Descripcion",typeof(string)) ,
                new DataColumn("Destinatario",typeof(string)) ,
                new DataColumn("P/Compra",typeof(string)) ,
                new DataColumn("Centro",typeof(string)) ,
                new DataColumn("Almacén",typeof(string)) ,
                new DataColumn("Elemento PEP",typeof(string)) ,
                new DataColumn("Cantidad",typeof(float )) ,
                new DataColumn("U/M",typeof(string )) ,
                new DataColumn("Fecha salida",typeof(DateTime)) ,
                new DataColumn("G/Remision",typeof(string)) ,
                new DataColumn("Transporte",typeof(string)) ,
                new DataColumn("Placa",typeof(string)) ,
                new DataColumn("Chofer",typeof(string)) ,
                new DataColumn("Fecha Llegada",typeof(DateTime)) ,
                new DataColumn("Observaciones",typeof(string))
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
                        sqlBulkCopy.ColumnMappings.Add("N° Entrega", "num_entrega");
                        sqlBulkCopy.ColumnMappings.Add("Clase", "cod_clase");
                        sqlBulkCopy.ColumnMappings.Add("N° Traslado", "num_traslado");
                        sqlBulkCopy.ColumnMappings.Add("Codigo", "cod_material");
                        sqlBulkCopy.ColumnMappings.Add("Descripcion", "gls_material");
                        sqlBulkCopy.ColumnMappings.Add("Destinatario", "cod_destino");
                        sqlBulkCopy.ColumnMappings.Add("P/Compra", "num_doc_compra");
                        sqlBulkCopy.ColumnMappings.Add("Centro", "cod_centro");
                        sqlBulkCopy.ColumnMappings.Add("Almacén", "cod_almacen");
                        sqlBulkCopy.ColumnMappings.Add("Elemento PEP", "cod_pep");
                        sqlBulkCopy.ColumnMappings.Add("Cantidad ", "num_cantidad");
                        sqlBulkCopy.ColumnMappings.Add("U/M", "cod_unidadUM");
                        sqlBulkCopy.ColumnMappings.Add("Fecha salida", "Fecha_salida");
                        sqlBulkCopy.ColumnMappings.Add("G/Remision", "G_Remision");
                        sqlBulkCopy.ColumnMappings.Add("Transporte", "gls_transporte");
                        sqlBulkCopy.ColumnMappings.Add("Placa", "gls_placa");
                        sqlBulkCopy.ColumnMappings.Add("Chofer", "gls_chofer");
                        sqlBulkCopy.ColumnMappings.Add("Fecha Llegada", "Fecha_Llegada");
                        sqlBulkCopy.ColumnMappings.Add("Observaciones", "gls_observacion");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        gvwAsignacion.DataSource = dtExcelData;
                        gvwAsignacion.DataBind();
                        con.Close();
                    
                        img.Visible = true;
                        img.ImageUrl = "~/imagenes/check.png";
                    }
                    catch (Exception ex)
                    {
                        img.Visible = true;
                        img.ImageUrl = "~/imagenes/Error.png";
                        UC_MessageBox.Show(Page, Page.GetType(), "Error en archivo : " + ex.Message);
                        return;
                    }
                }
            }
        }
    }
    protected void Bulk_Insert(FileUpload FileUpload1, string tabla, Image img)
    {
        //string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        string cleanMessage;
        string fileName;
        try
        {
        fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
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
            DataTable dtResultado = new DataTable();
            dtResultado = obj.Eliminar_Tabla_ReporteGeneral();
        }
        }
        catch (Exception ex)
        {
            
            img.Visible = true;
            img.ImageUrl = "~/imagenes/Error.png";
            // hay borrar los datos
            BL_LOGISTICA obj = new BL_LOGISTICA();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.Eliminar_Tabla_ReporteGeneral();

            cleanMessage = ex.Message + " Intente cambiar la extension de los Archivos a .XLS(libro 97-2003) " ;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

           
        }


    }

    protected void Bulk_Insert_Vacios(FileUpload FileUpload1, string tabla, Image img)
    {
        string cleanMessage;
        try
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
        catch (System.IO.IOException e)
        {

            img.Visible = true;
            img.ImageUrl = "~/imagenes/Error.png";
            // hay borrar los datos
            BL_LOGISTICA obj = new BL_LOGISTICA();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.Eliminar_Tabla_ReporteGeneral();

            cleanMessage = e.Message + " Intente cambiar la extension de los Archivos a .XLS(libro 97-2003)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


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
    protected void exportarXLS()
    {

        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtResultadoeExcel = new DataTable();


        string empresa = RdoEmpresa.SelectedValue;
        dtResultadoeExcel = obj.ListarProcesao_ReporteGlobal(empresa);
        //gvwAsignacion.Visible = false;
        //gvwAsignacion.DataSource = dtResultadoeExcel;
        //gvwAsignacion.DataBind();
        string cleanMessage = "";
        if (dtResultadoeExcel.Rows.Count > 0)
        {
            // cleanMessage = "Proceso Satisfactorio";
            //GridViewExportUtil.Export("REPORTE_GLOBAL_" + empresa + ".xls", gvwAsignacion);
            //return;

        }
        else
        {
            cleanMessage = "Se encontraron errores al exportar la carga de Reporte";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }


    }

    protected void exportarXLS_Plantilla()
    {


        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtbReporte = new DataTable();
        string empresa = RdoEmpresa.SelectedValue;
        dtbReporte = obj.ListarProcesao_ReporteGlobal(empresa);

        int Col; int Fil;

        try
        {
            Excel.Application excelApp = new Excel.Application();



            string workbookPath = Server.MapPath("~/Plantilla/ReporteGlobal.xltx");

            Excel.Workbook excelWorkbook = null;

            try
            {
                excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
                    false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true,
                    false, 0, true, false, false);
            }
            catch
            {

                excelWorkbook = excelApp.Workbooks.Add();
            }


            Excel.Sheets excelSheets = excelWorkbook.Worksheets;

            string currentSheet = "Reporte General";
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);

            // The following gets cell A1 for editing
            //Excel.Range excelCell = (Excel.Range)excelWorksheet;

            if (dtbReporte.Rows.Count > 0)
            {

                Col = 1;
                Fil = 2;

                foreach (DataRow objRow in dtbReporte.Rows)
                {


                    ((Excel.Range)excelWorksheet.Cells[Fil, Col]).Value = objRow[0];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 1]).Value = objRow[1];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 2]).Value = objRow[2];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 3]).Value = objRow[3];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 4]).Value = objRow[4];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 5]).Value = objRow[5];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 6]).Value = objRow[6];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 7]).Value = objRow[7];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 8]).Value = objRow[8];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 9]).Value = objRow[9];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 10]).Value = objRow[10];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 11]).Value = objRow[11];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 12]).Value = objRow[12];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 13]).Value = objRow[13];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 14]).Value = objRow[14];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 15]).Value = objRow[15];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 16]).Value = objRow[16];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 17]).Value = objRow[17];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 18]).Value = objRow[18];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 19]).Value = objRow[19];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 20]).Value = objRow[20];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 21]).Value = objRow[21];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 22]).Value = objRow[22];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 23]).Value = objRow[23];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 24]).Value = objRow[24];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 25]).Value = objRow[25];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 26]).Value = objRow[26];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 27]).Value = objRow[27];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 28]).Value = objRow[28];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 29]).Value = objRow[29];

                    //.Cells[Fil, Col + 30] = objRow.Item[30)
                    //.Cells[Fil, Col + 31] = objRow.Item[31)



                    Fil = Fil + 1;

                }

                excelApp.Visible = true;
                return;
            }
            else
            {

                UC_MessageBox.Show(Page, this.GetType(), "Error al realizar calculo en Reporte Global.");
                return;

            }
            //exportarXLS(dtbReporte);
        }
        catch (Exception ex)
        {

            UC_MessageBox.Show(Page, this.GetType(), ex.Message);
            return;

        }
        finally
        {


        }

    }

    protected void gvwAsignacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int cant = gvwAsignacion.Columns.Count;
        for (int x = 0; x < cant; x++)
        {
            if (x < 11)
            {
                gvwAsignacion.HeaderRow.Cells[x].BackColor = System.Drawing.Color.FromName("#FA0707");
            }
            else
            {
                gvwAsignacion.HeaderRow.Cells[x].BackColor = System.Drawing.Color.FromName("#FA0707");
            }
        }
    }

}