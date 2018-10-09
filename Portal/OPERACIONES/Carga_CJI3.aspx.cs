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

public partial class OPERACIONES_Carga_CJI3 : System.Web.UI.Page
{


    public string ControlUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();

        //this.Master.RegisterTrigger(btnProcesar);
        if (!Page.IsPostBack)
        {
            ControlBotones();
            Anio();
            //mes = DateTime.Today.Month - 1;
            //ddlMes.SelectedValue = mes.ToString();
            Meses();
        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Consulta_CJI3.aspx");
    }

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(50000);
        if (FileUpload1.FileBytes.Length <= 0)
        {
           

            string cleanMessage = "No ha adjuntado ningun archivo";
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
        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
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
                        dtResultado = obj.Registrar_CJI3(Convert.ToInt32 (ddlAnio.SelectedValue),Convert.ToInt32(ddlMes.SelectedValue ));
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
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {
            btnAdmin.Visible = true;
            btnTC.Visible = true;
        }
        else
        {
            btnAdmin.Visible = false;
            btnTC.Visible = false;
        }
    }
    protected void btnAdmin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Reportes/Control.aspx");
    }
    protected void btnTC_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/TipoCambio.aspx");
    }
    protected void Anio()
    {
        ddlAnio.DataSource = GetAnio();
        ddlAnio.DataTextField = "ValueMember";
        ddlAnio.DataValueField = "DisplayMember";
        ddlAnio.DataBind();
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
        dt.Rows.Add(anio - 1, anio -1);

        return dt;

    }
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();
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
}