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
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

public partial class CAREMENOR_RegularizacionEquipo : System.Web.UI.Page
{
    public string Requ_Numero;
    public string Reqd_CodLinea;
    public string Reqs_Correlativo;
    public string Reqs_Regularizar;
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string FolderAlquilerBackups = ConfigurationManager.AppSettings["FolderAlquilerBackups"];
    protected void Page_Load(object sender, EventArgs e)
    {
      

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnRespnder);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(GridView1);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnFile);
        if (!Page.IsPostBack)
        {
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
            //    FileUpload1.Attributes["onchange"] = "UploadFile(this)";

            //}


            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            Reqs_Regularizar = Request.QueryString["Reqs_Regularizar"];

            
            Listar();
            file();
        }
    }
    protected void Listar()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        Reqs_Regularizar = Request.QueryString["Reqs_Regularizar"];
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        int PENDIENTES = 0;
        dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_REGULARIZACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        if (dtResultado.Rows.Count >0 )
        {

            PENDIENTES = Convert.ToInt32(dtResultado.Rows[0]["PENDIENTES"].ToString());
        }
      

        if(PENDIENTES == 0)
        {

            btn.Visible = false;

        }
        else
        {
            //btn.Visible = true;


            if (Reqs_Regularizar == "1")// respuestas
            {
                btn.Visible = false;
                btnRespnder.Visible = true;
            }
            else
            {
                btn.Visible = true;
                btnRespnder.Visible = false;
            }

        }


        if (dtResultado.Rows.Count > 0)
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
    protected void file()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.uspSEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
        if (dtResultado.Rows.Count > 0)
        {
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        string _Requ_Numero = string.Empty;
        string _Reqd_CodLinea = string.Empty;
        string _Reqs_Correlativo = string.Empty;

        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();

        try
        {
            int intContador = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {

                RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");
                TextBox txtObservaciones = (TextBox)row.FindControl("txtObservaciones");
                _Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                _Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                _Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key

                BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                DataTable dtResultado = new DataTable();

                if(rb.SelectedValue=="I+")
                {
                    intContador++;
                }
                dtResultado = obj.USP_SEL_PROCESAR_REGULARIZACION(_Requ_Numero, _Reqd_CodLinea, _Reqs_Correlativo, rb.SelectedValue, Session["IDE_USUARIO"].ToString(), txtObservaciones.Text.Trim(),1);
               


            }

            // correo para los encargos del sig
            BL_TBL_RequerimientoSubDetalle xobj = new BL_TBL_RequerimientoSubDetalle();
            string url = URLSSK;
            string mensaje = "El Sistema de Equipos Menores SSK informa que se realizó la revisión del proceso de REGULARIZACIÓN de la lista de equipos para el proyecto ";
            xobj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER CARE SOLPED", url);


            cleanMessage = "Se actualizo el estado de los equipos correctamente.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            // correo solicitando mas informacion 
            if (intContador > 0)
            {

                cleanMessage = "Se ha notificado a los responsables de OT sobre el envio de más información";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                mensaje = "El Sistema de Equipos Menores SSK informa que se realizó la revisión del proceso de REGULARIZACIÓN, la cual se solicita el envio de más información de la lista de equipos para el proyecto ";
                xobj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_REG_INFORMACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER CARE (OT)", url);

            }


            Listar();

        }
        catch (Exception ex)
        {

        }
        
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Atención";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Datos del requerimiento";
            HeaderCell.ColumnSpan = 10;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        DataTable dtResultado = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            dtResultado = GetTableEstados();
            if (dtResultado.Rows.Count > 0)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
            }


        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string RESUMEN_ESTADO_CGO = GridView1.DataKeys[e.Row.RowIndex].Values[3].ToString();

            if (RESUMEN_ESTADO_CGO == "I+")
            {
                e.Row.BackColor = Color.FromName("#ffeb9c");
            }
        }
    }

    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "P");
        table.Rows.Add(2, "A");
        table.Rows.Add(3, "R");
        table.Rows.Add(4, "I+");

        return table;
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool  FLG_EXTRA = Convert.ToBoolean( GridView2.DataKeys[e.Row.RowIndex].Values[3].ToString());

            if (FLG_EXTRA == true   )
            {
                e.Row.BackColor = Color.FromName("#ffeb9c");
            }
        }
    }

    protected void btnRespnder_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        string _Requ_Numero = string.Empty;
        string _Reqd_CodLinea = string.Empty;
        string _Reqs_Correlativo = string.Empty;

        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();


        try
        {
             foreach (GridViewRow row in GridView1.Rows)
            {
                RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

                FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
                TextBox txtRespuesta = (TextBox)row.FindControl("txtRespuesta");

                _Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                _Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                _Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key

                if (rb.SelectedValue == "I+")
                {
                    BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                    DataTable dtResultado = new DataTable();
                    dtResultado = obj.USP_SEL_PROCESAR_REGULARIZACION(_Requ_Numero, _Reqd_CodLinea, _Reqs_Correlativo, rb.SelectedValue, Session["IDE_USUARIO"].ToString(), txtRespuesta.Text.Trim(), 2);

                }
                    
            }
            CargarFile();

            BL_TBL_RequerimientoSubDetalle xobj = new BL_TBL_RequerimientoSubDetalle();
            string url = URLSSK;
            string mensaje = "El Sistema de Equipos Menores SSK informa sobre las respuestas solicitas del invio de información adicional del proyecto ";
            xobj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_REG_INFORMACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER REGULARIZACION", url);


            cleanMessage = "Se ha enviado su respuesta correctamente";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnFile_Click(object sender, EventArgs e)
    {
        
    }
    protected void CargarFile()
    {
        string cleanMessage = string.Empty;
        string _Requ_Numero = string.Empty;
        string _Reqd_CodLinea = string.Empty;
        string _Reqs_Correlativo = string.Empty;

        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();

        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {

                FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
                //FileUpload1.Attributes["onchange"] = "UploadFile(this)";
                TextBox txtObservaciones = (TextBox)row.FindControl("txtObservaciones");

                _Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                _Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                _Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key
                string fileName = string.Empty;
                String fileExtension = string.Empty;
                Boolean fileOK = false;
                string fileArchivo = string.Empty;
                string Name = string.Empty;
                if (FileUpload1.HasFile)
                {

                    fileName = EliminarCaracteres.ReemplazarCaracteresEspeciales(FileUpload1.FileName.ToString());
                    int length = FileUpload1.PostedFile.ContentLength;

                    fileExtension = Path.GetExtension(FileUpload1.FileName);

                    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }
                }

                if (fileOK)
                {
                    try
                    {

                        // Se carga la ruta física de la carpeta temp del sitio
                        string ruta = Server.MapPath(FolderAlquiler);
                        string rutaBackups = FolderAlquilerBackups;
                        // Si el directorio no existe, crearlo
                        //if (!Directory.Exists(ruta))
                        //    Directory.CreateDirectory(ruta);

                        string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.PostedFile.FileName);
                        Name = FileUpload1.PostedFile.FileName;
                        // Verificar que el archivo no exista
                        if (File.Exists(archivo))
                        {
                            fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales( _Requ_Numero + "." + _Reqd_CodLinea + "-" + _Reqs_Correlativo + "_Sustentos_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName));
                            FileUpload1.SaveAs(ruta + fileArchivo);
                            FileUpload1.SaveAs(rutaBackups + fileArchivo);
                        }

                        else
                        {
                            fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales(_Requ_Numero + "." + _Reqd_CodLinea + "-" + _Reqs_Correlativo + "_Sustentos_" + Path.GetFileName(FileUpload1.FileName));
                            //FileUpload1.SaveAs(archivo);
                            FileUpload1.SaveAs(ruta + fileArchivo);
                            FileUpload1.SaveAs(rutaBackups + fileArchivo);
                        }


                        DataTable dtrpta = new DataTable();
                        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();

                        obj.uspInsertarArchivosAdicionales_Alquiler(_Requ_Numero, _Reqd_CodLinea, _Reqs_Correlativo, FolderAlquiler, fileArchivo,fileName);
                        file();

                    }
                    catch (Exception ex)
                    {
                        cleanMessage = "Archivo no puedo ser cargado";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }

            }



        }
        catch (Exception ex)
        {

        }
    }
 
}