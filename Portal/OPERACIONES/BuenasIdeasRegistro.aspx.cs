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

public partial class OPERACIONES_BuenasIdeasRegistro : System.Web.UI.Page
{
    string FolderBuenasIdeas = ConfigurationManager.AppSettings["FolderBuenasIdeas"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnenviar);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
           
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    protected void btnenviar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txttitulo.Text.Trim()== string.Empty  )
        {
            cleanMessage = "Ingresar título de la propuesta de mejora";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if(txtdescripcion.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar objetivo de la meta o propósito a alcanzar";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtsolucion.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar descripción de la solución a plantear";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtventajas.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar descripción de la solución a plantear";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtareas.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar áreas involucradas";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            

            // Si el directorio no existe, crearlo
            if (!Directory.Exists(Server.MapPath(FolderBuenasIdeas)))
                Directory.CreateDirectory(FolderBuenasIdeas);

            String fileExtension = string.Empty;
            Boolean fileOK = false;
            string fileArchivo = string.Empty;
            if (FileUpload1.HasFile)
            {

                string fileName = FileUpload1.FileName;
                int length = FileUpload1.PostedFile.ContentLength;

                fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
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
                    string ruta = Server.MapPath(FolderBuenasIdeas);

                    // Si el directorio no existe, crearlo
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                       fileArchivo =  DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(ruta+fileArchivo);
                    }
                     
                    else
                    {
                        fileArchivo = FileUpload1.PostedFile.FileName;
                        FileUpload1.SaveAs(archivo);
                    }
                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }

            BE_BUENAS_IDEAS oBESol = new BE_BUENAS_IDEAS();
            oBESol.IDE_IDEAS = 0;
            oBESol.DESCRIPCION_PROPUESTA = txtdescripcion.Text.Trim(); 
            oBESol.SOLUCION  = txtsolucion.Text.Trim(); 
            oBESol.VENTAJAS = txtventajas.Text.Trim();
            oBESol.AREAS = txtareas.Text.Trim();
            oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
            oBESol.TITULO = txttitulo.Text.Trim();
            oBESol.FILE = fileArchivo;
            oBESol.URL = Server.MapPath(FolderBuenasIdeas);
            int dtrpta = 0;
            dtrpta = new BL_BUENAS_IDEAS().uspINS_BUENAS_IDEAS(oBESol);
            if (dtrpta > 0)
            {
                BL_BUENAS_IDEAS oB = new BL_BUENAS_IDEAS();
                oB.SP_EnviarCorreo_BuenIdea(dtrpta.ToString ());
                cleanMessage = "Registro exitoso. Tu propuesta está siendo revisada, en caso de ser aprobada, te informaremos sobre tu reconocimiento, gracias";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                txtdescripcion.Text = string.Empty;
                txtsolucion.Text = string.Empty;
                txtventajas.Text = string.Empty;
                txtareas.Text = string.Empty;
                txttitulo.Text = string.Empty;
            }
        }
    }
    private void GuardarArchivo(HttpPostedFile file)
    {
        //string FolderBuenasIdeas = ConfigurationManager.AppSettings["FolderBuenasIdeas"];
        // Se carga la ruta física de la carpeta temp del sitio
        string ruta = Server.MapPath(FolderBuenasIdeas);
        string MensajeError;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);

        string archivo = String.Format("{0}\\{1}", ruta, file.FileName);

        // Verificar que el archivo no exista
        if (File.Exists(archivo))


            MensajeError = "Ya existe una imagen con nombre\"{0}\"." + file.FileName;
        else
        {
            file.SaveAs(archivo);
        }
    }


   
}