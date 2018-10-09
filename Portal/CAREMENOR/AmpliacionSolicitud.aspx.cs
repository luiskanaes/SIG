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

public partial class CAREMENOR_AmpliacionSolicitud : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string FolderAlquilerBackups = ConfigurationManager.AppSettings["FolderAlquilerBackups"];
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnBuscar);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardar);
        if (!Page.IsPostBack)
        {
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardar);

            Proyectos();
         
        }
    }
    protected void Proyectos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_ARRIENDO_CC("RESPONSABLE ALQUILER", Session["IDE_USUARIO"].ToString(), BL_Session.CENTRO_COSTO);
        if (dtResultado.Rows.Count > 0)
        {


            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "Proy_Nombre";
            ddlcentro.DataValueField = "Proy_Codigo";
            ddlcentro.DataBind();

            if (dtResultado.Rows.Count > 1)
            {
                ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
            }

            //Proveedor();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {


            string cleanMessage = "No cuenta con permisos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        if (txtPdc.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar PDC";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            consultarFile_v2();
            Buscarrequerimientos();
        }
    }
    protected void Buscarrequerimientos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        string Centro = string.Empty;
        int contarCC = Convert.ToInt32(ddlcentro.Items.Count.ToString());
        if (contarCC <= 1)
        {
            Centro = ddlcentro.SelectedValue.ToString();
        }
        else
        {
            if (ddlcentro.SelectedIndex == 0)
            {
                Centro = string.Empty;
            }
            else
            {
                Centro = ddlcentro.SelectedValue.ToString();
            }
        }

        /**
        * @FLG_BORRADOR_AMPLIACION: para listar todos aquellos que han sido indicados como ampliacion (aprobados y rechazados)
        * @FLG_AMPLIACION: solo los requermientos aprobados como ampliacion
        * 
        * **/
        dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_CONSULTAR_AMPLIAR(Centro, txtPdc.Text.Trim());
        if (dtResultado.Rows.Count > 0)
        {
            FileUpload1.Visible = true;
            btnGuardar.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();

        }
        else
        {
            FileUpload1.Visible = false;
            btnGuardar.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
   
  
     int cargar(FileUpload FileUpload1, string TipoArchivo, string g, string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
    {

        FileUpload1 = (FileUpload)Session["FileUpload1"];

        string cleanMessage = string.Empty;

        int registros = 0;


        // Si el directorio no existe, crearlo
        //if (!Directory.Exists(Server.MapPath(FolderAlquiler)))
        //    Directory.CreateDirectory(FolderAlquiler);

        String fileExtension = string.Empty;
        Boolean fileOK = false;
        string fileArchivo = string.Empty;
        string Name = string.Empty;
        if (FileUpload1.HasFile)
        {

            string fileName = FileUpload1.FileName.ToString();
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
                Name = EliminarCaracteres.ReemplazarCaracteresEspeciales(FileUpload1.PostedFile.FileName);
                // Verificar que el archivo no exista
                if (File.Exists(archivo))
                {
                    fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales(BL_Session.CENTRO_COSTO + "_" + TipoArchivo + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName));
                    FileUpload1.SaveAs(ruta + fileArchivo);
                    FileUpload1.SaveAs(rutaBackups + fileArchivo);
                }

                else
                {
                    fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales(BL_Session.CENTRO_COSTO + "_" + TipoArchivo + "_" + Path.GetFileName(FileUpload1.FileName));
                    //FileUpload1.SaveAs(archivo);
                    FileUpload1.SaveAs(ruta + fileArchivo);
                    FileUpload1.SaveAs(rutaBackups + fileArchivo);
                }

            }
            catch (Exception ex)
            {
                cleanMessage = "Archivo no puedo ser cargado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        ////////////////////////////////



        BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
        oBESol.Requ_Numero = Requ_Numero;
        oBESol.Reqd_CodLinea = Reqd_CodLinea;
        oBESol.Reqs_Correlativo = Reqs_Correlativo;
        oBESol.D_DOCUMENTO_TIPO = 0;
        oBESol.D_DOCUMENTO_RUTA = FolderAlquiler;
        oBESol.D_DOCUMENTO_FILE = fileArchivo;
        oBESol.D_DOC_MOVILIZACION = 0;
        oBESol.D_DOCUMENTO_FECHA = DateTime.Today.ToString("dd/MM/yyyy");
        oBESol.D_Prov_RUC = "";
        oBESol.D_ATENCION_TIPO = 0;
        oBESol.TIPO_OPERACION = 1;
        oBESol.D_DOCUMENTO_FILENAME = Name;
        oBESol.TIPO_FILE = TipoArchivo;
        oBESol.D_FLG_OPERARIO = "";
        oBESol.COD_GUID = g.ToString();
        oBESol.D_FECHA_SALE_OBRA = "";
        DataTable dtrpta = new DataTable();
        dtrpta = new BL_TBL_RequerimientoSubDetalle().uspUPD_TBL_RequerimientoSubDetalle_Alquiler(oBESol);

        if (dtrpta.Rows.Count > 0)
        {

            registros++;

        }
        return registros;
    }

    public bool IsDate(object inValue)
    {
        bool bValid;
        try
        {
            DateTime myDT = DateTime.Parse(inValue.ToString());
            bValid = true;
        }
        catch (Exception e)
        {
            bValid = false;
        }

        return bValid;
    }

    protected void consultarFile()
    {
        if (Session["FileUpload1"] == null && FileUpload1.HasFile)
        {
            Session["FileUpload1"] = FileUpload1;
            Label1.Text = FileUpload1.FileName;
            FileUpload1 = (FileUpload)Session["FileUpload1"];
        }
        // Next time submit and Session has values but FileUpload is Blank
        // Return the values from session to FileUpload
        else if (Session["FileUpload1"] != null && (!FileUpload1.HasFile))
        {
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Label1.Text = FileUpload1.FileName;
        }
        else if (Session["FileUpload1"] != null)
        {
            //Session["FileUpload1"] = FileUpload1;
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Label1.Text = FileUpload1.FileName;
        }
     
        else if (FileUpload1.HasFile)
        {
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Session["FileUpload1"] = FileUpload1;
            Label1.Text = FileUpload1.FileName;
        }
        else if (FileUpload1.FileBytes.Length > 0)
        {
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Session["FileUpload1"] = FileUpload1;
            Label1.Text = FileUpload1.FileName;
        }
        else
        {
            string cleanMessage = "Falta adjuntar documentos de sustento de ampliacion";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        //if (Session["FileUpload1"]==null)
        //{
        //   string cleanMessage = "Falta adjuntar documentos de sustento de ampliacion";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, cleanMessage, Reqs_ItemSecuencia;


        int intContador = 0;
        int intregistros = 0;
        if (GridView1.Rows.Count == 0)
        {

            cleanMessage = "No existe registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        foreach (GridViewRow Fila in GridView1.Rows)
        {
            CheckBox ChkBoxCell = ((CheckBox)Fila.FindControl("chkSelect"));
            if (ChkBoxCell.Checked == true)
            {
                intContador += 1;
            }
        }

        if (intContador == 0)
        {
            cleanMessage = "Debe seleccionar al menos un registro.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }

        Guid g;
        g = Guid.NewGuid();
        string codigos = string.Empty;
       
        if (FileUpload1.HasFile)
        {
            consultarFile();


            foreach (GridViewRow row in GridView1.Rows)
            {

                CheckBox ChkBoxCell = ((CheckBox)row.FindControl("chkSelect"));
                if (ChkBoxCell.Checked)
                {
                    intregistros++;
                    Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                    Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                    Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key

                    //Reqs_ItemSecuencia = GridView1.DataKeys[row.RowIndex].Values[4].ToString(); // extrae key

                    TextBox txtMonto = ((TextBox)row.FindControl("txtMonto"));
                    TextBox txtInicio = ((TextBox)row.FindControl("txtInicio"));
                    TextBox txtFin = ((TextBox)row.FindControl("txtFin"));
                    if (txtMonto.Text.Trim() == string.Empty)
                    {
                        cleanMessage = "Falta ingresar monto del req.: " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }

                    else if (txtInicio.Text.Trim() == string.Empty)
                    {
                        cleanMessage = "Falta ingresar fecha de inicio del req: " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if (txtFin.Text.Trim() == string.Empty)
                    {
                        cleanMessage = "Falta ingresar fecha de termino del req: " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if (IsDate(txtInicio.Text.Trim()) == false)
                    {
                        cleanMessage = "Error en la fecha de inicio del req : " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if (IsDate(txtFin.Text.Trim()) == false)
                    {
                        cleanMessage = "Error en la fecha de termino del req : " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if (Convert.ToDateTime(txtInicio.Text) > Convert.ToDateTime(txtFin.Text))
                    {
                        cleanMessage = "Error en la fecha inicio no puede ser mayor a la fecha de termino del req : " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }

                    else
                    {


                        BL_TBL_RequerimientoSubDetalle xobj = new BL_TBL_RequerimientoSubDetalle();
                        DataTable xdtResultado = new DataTable();
                        xdtResultado = xobj.uspUPD_MONTO_AMPLIACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, txtMonto.Text.Trim(), Convert.ToDateTime(txtInicio.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtFin.Text).ToString("dd/MM/yyyy"), Session["IDE_USUARIO"].ToString());

                        codigos += Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim() + ",";

                        if ((Session["FileUpload1"] != null) || (FileUpload1.HasFile))
                        {
                            intContador = cargar(FileUpload1, "AMPLIACION", g.ToString(), Requ_Numero.Trim(), Reqd_CodLinea.Trim(), Reqs_Correlativo.Trim());
                            if (intContador > 0)
                            {
                                intContador++;
                            }
                        }


                    }
                }
                ChkBoxCell = null;
            }
        }
        else
        {
            cleanMessage = "Falta adjuntar documentos de sustento de ampliacion";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }


        if (intregistros > 0)
        {
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();

            obj.USP_SEL_TBL_REQUERIMIENTO_CORREO_AMPLIACION(codigos, "ALQUILER CARE", 1);
            cleanMessage = "Envio satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Session.Remove("FileUpload1");
        }
    }

    protected void btncrear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/AmpliacionBandeja");

    }

    protected void consultarFile_v2()
    {
        if (Session["FileUpload1"] == null && FileUpload1.HasFile)
        {
            Session["FileUpload1"] = FileUpload1;
            Label1.Text = FileUpload1.FileName;
            FileUpload1 = (FileUpload)Session["FileUpload1"];
        }
        // Next time submit and Session has values but FileUpload is Blank
        // Return the values from session to FileUpload
        else if (Session["FileUpload1"] != null && (!FileUpload1.HasFile))
        {
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Label1.Text = FileUpload1.FileName;
        }
        else if (Session["FileUpload1"] != null)
        {
            //Session["FileUpload1"] = FileUpload1;
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Label1.Text = FileUpload1.FileName;
        }

        else if (FileUpload1.HasFile)
        {
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Session["FileUpload1"] = FileUpload1;
            Label1.Text = FileUpload1.FileName;
        }
        else if (FileUpload1.FileBytes.Length > 0)
        {
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            Session["FileUpload1"] = FileUpload1;
            Label1.Text = FileUpload1.FileName;
        }
        
        
    }
}