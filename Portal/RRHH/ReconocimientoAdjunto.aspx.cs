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
using System.Web.Services;
using System.Web.Script.Services;
using System.Text.RegularExpressions;

public partial class RRHH_ReconocimientoAdjunto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnCarga);
        if (!Page.IsPostBack)
        {

        }
    }
    //Response.Redirect("~/RRHH/ReconocerBandeja.aspx");
    
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
    protected void btnCarga_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            if (ValidaExtension(Extension))
            {

                string FilePath = Server.MapPath(FolderPath + FileName);
                try
                {
                    if (System.IO.File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                    FileUpload1.SaveAs(FilePath);

                    Import_To_Grid(FilePath, Extension, "Yes");
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                UC_MessageBox.Show(Page, Page.GetType(), "Formato no permitido");
                return;

            }
            
        }
    }
    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string cleanMessage;
        try
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        catch (System.IO.IOException e)
        {



            cleanMessage = e.Message + " Intente cambiar la extension de los Archivos a .XLS(libro 97-2003)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/ReconocerBandeja.aspx");
    }

    protected void btnRegistro_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        int dtrpta = 0;
        int cantidad = 0;
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {

                string evaluado;
                string evaluador;
                string competencia;
                int  CODIGO;
                string sustento;
                evaluador  = Server.HtmlDecode(((string)(row.Cells[2].Text.Replace(" ", String.Empty))).Trim());
                evaluado = Server.HtmlDecode(((string)(row.Cells[4].Text.Replace(" ", String.Empty))).Trim());
                competencia = Server.HtmlDecode(((string)(row.Cells[6].Text)).Trim());
                sustento = Server.HtmlDecode(((string)(row.Cells[7].Text)).Trim());

                


                
                if (evaluado ==string.Empty )
                {
                    cleanMessage = "Ingresar número de DNI del Reconocedor";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else if (evaluado == string.Empty)
                {
                    cleanMessage = "Ingresar número de DNI Reconocido";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else if (competencia == string.Empty)
                {
                    cleanMessage = "Ingresar competencia";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else if (sustento == string.Empty)
                {
                    cleanMessage = "Ingresar sustento";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else
                {
                    //Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
                    //competencia = replace_a_Accents.Replace(competencia, "a");

                    //Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
                    //competencia = replace_a_Accents.Replace(competencia, "e");

                    //Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
                    //competencia = replace_a_Accents.Replace(competencia, "i");

                    //Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
                    //competencia = replace_a_Accents.Replace(competencia, "o");

                    //Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
                    //competencia = replace_a_Accents.Replace(competencia, "u");

                    CODIGO = ConsultaCompetencia(competencia);
                    BE_RRHH_COMPETENCIAS_EVAL oBESol = new BE_RRHH_COMPETENCIAS_EVAL();
                    oBESol.IDE_COMPETENCIA = 0;
                    oBESol.DNI_EVALUADO = evaluado ;
                    oBESol.DNI_SUPERVISOR = evaluador ;
                    oBESol.IDE_FACTOR = CODIGO;
                    oBESol.SUSTENTO = sustento;

                   
                    dtrpta = new BL_RRHH_COMPETENCIAS_EVAL().Mant_Insert_Reconocimiento(oBESol);
                    if (dtrpta > 0)
                    {
                        cantidad++;
                    }

                  
                }
            }
            if (dtrpta > 0)
            {
                BL_RRHH_COMPETENCIAS_EVAL ob = new BL_RRHH_COMPETENCIAS_EVAL();

                cleanMessage = "Registro exitoso, total (" + cantidad.ToString() + ")";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


            }
        }
    }
    public int ConsultaCompetencia(string CAMPO)
    {

        int result = 0;
        string sql = "SELECT TOP 1 ID_PARAMETRO FROM [PARAMETROS] WHERE DES_DESCRIPCION ='COMPETENCIAS'  AND  [DES_TABLA] ='RRHH_COMPETENCIAS_EVAL' AND [DES_ASUNTO] LIKE " + "'%" + CAMPO.Substring(0, 7) + "%'";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return result;
    }
    public static string RemoveAccentsWithRegEx(string inputString)
    {
        Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
        Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
        Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
        Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
        Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
        inputString = replace_a_Accents.Replace(inputString, "a");
        inputString = replace_e_Accents.Replace(inputString, "e");
        inputString = replace_i_Accents.Replace(inputString, "i");
        inputString = replace_o_Accents.Replace(inputString, "o");
        inputString = replace_u_Accents.Replace(inputString, "u");
        return inputString;
    }
}