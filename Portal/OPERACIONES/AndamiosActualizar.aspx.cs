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
public partial class OPERACIONES_AndamiosActualizar : System.Web.UI.Page
{
    String GERENCIA, CECOS_GESTOR, IDE_ANDAMIOS;
    string FolderANDAMIOS = ConfigurationManager.AppSettings["FolderANDAMIOS"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Personal();
            Especialidad();
            Solicitud();
            Tipo();
            area();

            lblCodigo.Text = Request.QueryString["IDE_ANDAMIOS"].ToString();
            CECOS_GESTOR = Request.QueryString["CECOS_GESTOR"].ToString();
            IDE_ANDAMIOS = Request.QueryString["IDE_ANDAMIOS"].ToString();
            CartaDatos(lblCodigo.Text);

            //txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //lblCentro.Text = BL_Session.CENTRO_COSTO.ToString();
        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        CECOS_GESTOR = Request.QueryString["CECOS_GESTOR"].ToString();
        dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_CC(CECOS_GESTOR);
        //dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.Items.Clear();
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));



        }
    }
    protected void Especialidad()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlespecialidad.DataSource = obj.ListarParametros("ESPECIALIDAD", "SOL_ANDAMIOS");
        ddlespecialidad.DataTextField = "DES_ASUNTO";
        ddlespecialidad.DataValueField = "ID_PARAMETRO";
        ddlespecialidad.DataBind();


    }
    protected void area()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlarea.DataSource = obj.ListarParametros("AREA", "SOL_ANDAMIOS");
        ddlarea.DataTextField = "DES_ASUNTO";
        ddlarea.DataValueField = "ID_PARAMETRO";
        ddlarea.DataBind();
    }
    protected void Solicitud()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlSolicitud.DataSource = obj.ListarParametros("SOLICITUD", "SOL_ANDAMIOS");
        ddlSolicitud.DataTextField = "DES_ASUNTO";
        ddlSolicitud.DataValueField = "ID_PARAMETRO";
        ddlSolicitud.DataBind();
    }
    protected void Tipo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlTipo.DataSource = obj.ListarParametros("TIPO", "SOL_ANDAMIOS");
        ddlTipo.DataTextField = "DES_ASUNTO";
        ddlTipo.DataValueField = "ID_PARAMETRO";
        ddlTipo.DataBind();
    }
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersonal"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void btnviar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        CECOS_GESTOR = Request.QueryString["CECOS_GESTOR"].ToString();
        
        if (txtrequerida.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de requerimiento";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlPersonal.SelectedValue == string.Empty)
        {
            cleanMessage = "Falta indicar personal solicitante";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {


            // Si el directorio no existe, crearlo
            if (!Directory.Exists(Server.MapPath(FolderANDAMIOS)))
                Directory.CreateDirectory(FolderANDAMIOS);

            String fileExtension = string.Empty;
            Boolean fileOK = false;
            string fileArchivo = string.Empty;
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
                    string ruta = Server.MapPath(FolderANDAMIOS);

                    // Si el directorio no existe, crearlo
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.PostedFile.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = "AND_" + CECOS_GESTOR + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = "AND_" + CECOS_GESTOR + "_" + Path.GetFileName(FileUpload1.FileName);
                        //FileUpload1.SaveAs(archivo);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                    }

                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }

            BE_SOL_ANDAMIOS oBESol = new BE_SOL_ANDAMIOS();
            oBESol.IDE_ANDAMIOS = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.ANDAMIOS = txtCodAndamio.Text.Trim();
            oBESol.FECHA = string.Empty;
            oBESol.IDE_USUARIO = Session["IDE_USUARIO"].ToString();
            oBESol.SOLICITANTE = ddlPersonal.SelectedValue.ToString();
            oBESol.FILE_ANDAMIOS = fileArchivo;
            oBESol.FILE_RUTA = FolderANDAMIOS;
            oBESol.COMENTARIOS = txtcomentarios.Text.ToString();
            oBESol.ESPECIALIDAD = ddlespecialidad.SelectedValue;
            oBESol.AREA = ddlarea.SelectedValue;
            oBESol.SOLICITUD = ddlSolicitud.SelectedValue;
            oBESol.TIPO = ddlTipo.SelectedValue;
            oBESol.FECHA_REQUERIDA = txtrequerida.Text;

            oBESol.IPCENTRO = CECOS_GESTOR; //BL_Session.IP_CENTRO.ToString();


            int dtrpta = 0;
            dtrpta = new BL_SOL_ANDAMIOS().uspINS_SOL_ANDAMIOS(oBESol);
            if (dtrpta > 0)
            {
                cleanMessage = "Registro exitoso.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }


        }
    }
    protected void CartaDatos(string IDE_ANDAMIOS)
    {
        DataTable dtResultado = new DataTable();
        BL_SOL_ANDAMIOS obj = new BL_SOL_ANDAMIOS();
        dtResultado = obj.uspSEL_SOL_ANDAMIOS_IDE(Convert.ToInt32(IDE_ANDAMIOS));
        if (dtResultado.Rows.Count > 0)
        {
            lblTicket.Text = dtResultado.Rows[0]["TICKET"].ToString();
            lblCodigo.Text = dtResultado.Rows[0]["IDE_ANDAMIOS"].ToString();
            txtrequerida.Text = dtResultado.Rows[0]["FECHA_REQUERIDA"].ToString();

            //txtrequerida.Text = dtResultado.Rows[0]["FECHA_ATENCION"].ToString();
            ddlPersonal.SelectedValue = dtResultado.Rows[0]["SOLICITANTE"].ToString();
            ddlarea.SelectedValue = dtResultado.Rows[0]["IDE_AREA"].ToString();
            ddlSolicitud.SelectedValue = dtResultado.Rows[0]["IDE_SOLICITUD"].ToString();
            ddlTipo.SelectedValue = dtResultado.Rows[0]["IDE_TIPO"].ToString();
            ddlespecialidad.SelectedValue = dtResultado.Rows[0]["IDE_ESPECIALIDAD"].ToString();
            txtcomentarios.Text = dtResultado.Rows[0]["COMENTARIOS"].ToString();
            txtCodAndamio.Text = dtResultado.Rows[0]["ANDAMIOS"].ToString();
            //txtFechaDestino.Text = dtResultado.Rows[0]["D_FECHA"].ToString();

        }
    }
}