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

public partial class OPERACIONES_PERMISOS_SOLICITUD : System.Web.UI.Page
{
    string FolderMDP = ConfigurationManager.AppSettings["FolderMDP"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnviar);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ParametrosMotivo();
            Anios();
            
            ddlanio.SelectedValue = DateTime.Today.Year.ToString ();
            ListarPermisos();
            VerificarResposanble();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void VerificarResposanble()
    {
        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_EQUIPO_TRABAJO_DNI(Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            Boolean FLG_ESTADO = Convert.ToBoolean(dtResultado.Rows[0]["FLG_ESTADO"].ToString());
            if (FLG_ESTADO == true)
            {
                btnviar.Visible = true;
            }
            else
            {
                btnviar.Visible = false;
                string cleanMessage = "Momentaneamente no puede generar solicitudes";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else
        {
            btnviar.Visible = false;
            string cleanMessage = "Momentaneamente no puede generar solicitudes";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    
   
    protected void ParametrosMotivo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlmotivo.DataSource = obj.USP_PARAMETRO_LISTAR_MDP("IDE_MOTIVO", "TBSOLICITUD_PERMISOS");
        ddlmotivo.DataTextField = "DES_ASUNTO";
        ddlmotivo.DataValueField = "ID_PARAMETRO";
        ddlmotivo.DataBind();


    }
    protected void Anios()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableAnio();
        if (dtResultado.Rows.Count > 0)
        {
            ddlanio.DataSource = dtResultado;
            ddlanio.DataTextField = "ANIO1";
            ddlanio.DataValueField = "ANIO";
            ddlanio.DataBind();

        }
    }
    static DataTable GetTableAnio()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ANIO", typeof(int));
        table.Columns.Add("ANIO1", typeof(string));

        int anio = 0;
        int anioActual = 0;
        anio = DateTime.Today.Year + 1;
        anioActual = DateTime.Today.Year + 1;
        for (int i = 0; i < 5; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
    }

    protected void btnviar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txtInicio.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de inicio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtfin.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de termino";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {
            if (Convert.ToDateTime(txtInicio.Text) > Convert.ToDateTime(txtfin.Text))
            {
                cleanMessage = "La fecha de inicio no puede ser mayor a la fecha de termino";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

            else
            {
                string Motivo = ddlmotivo.SelectedItem.ToString();

                // Si el directorio no existe, crearlo
                if (!Directory.Exists(Server.MapPath(FolderMDP)))
                    Directory.CreateDirectory(FolderMDP);

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
                        string ruta = Server.MapPath(FolderMDP);

                        // Si el directorio no existe, crearlo
                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);

                        string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.FileName);

                        // Verificar que el archivo no exista
                        if (File.Exists(archivo))
                        {
                            fileArchivo = DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                            FileUpload1.SaveAs(ruta + fileArchivo);
                        }

                        else
                        {
                            fileArchivo = FileUpload1.PostedFile.FileName;
                            FileUpload1.SaveAs(archivo);

                        }
                        Guardar(fileArchivo);
                    }
                    catch (Exception ex)
                    {
                        cleanMessage = "Archivo no puedo ser cargado";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }

                else
                {
                    if (Motivo == "DESCANSO MEDICO")
                    {
                        if (fileOK==false )
                        {
                            cleanMessage = "Adjuntar documento de sustento";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        }
                        else
                        {
                            Guardar(fileArchivo);
                        }
                    }
                    else
                    {
                        Guardar("");
                    }
                    
                   
                }
            }
        }
    }
    public String dayOfWeek(DateTime? date)
    {
        return date.Value.ToString("dddd");

    }
    protected void Guardar(string file)
    {
        string cleanMessage = string.Empty;
        BE_TBSOLICITUD_PERMISOS oBESol = new BE_TBSOLICITUD_PERMISOS();
        oBESol.Ide_permiso = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        oBESol.Ide_usuario = Session["IDE_USUARIO"].ToString();
        oBESol.Ide_motivo = Convert.ToInt32(ddlmotivo.SelectedValue);
        oBESol.Inicio = txtInicio.Text.Trim();
        oBESol.Fin = txtfin.Text.Trim();
        oBESol.Comentario = txtcomentarios.Text.Trim();
        oBESol.FILE = file;
        oBESol.URL = FolderMDP;
        oBESol.NOMBRE_DIA = dayOfWeek(Convert.ToDateTime(txtInicio.Text.Trim()));
        int dtrpta = 0;
        dtrpta = new BL_TBSOLICITUD_PERMISOS().MANT_TBSOLICITUD_PERMISOS_INSERT_DATOS(oBESol);
        if (dtrpta > 0)
        {
            BL_TBSOLICITUD_PERMISOS oB = new BL_TBSOLICITUD_PERMISOS();
            oB.correo_solicitud(dtrpta, Session["IDE_USUARIO"].ToString());
            cleanMessage = "Registro exitoso. Tu solicitud está siendo revisada, te informaremos sobre su situación lo más antes posible, gracias";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            lblCodigo.Text = string.Empty;
            txtcomentarios.Text = string.Empty;
            txtfin.Text = string.Empty;
            txtInicio.Text = string.Empty;
            ListarPermisos();

        }
    }
    protected void ver_editar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SP_TBSOLICITUD_PERMISOS_SELECT_DATOS_ID(Convert.ToInt32 (pk));
        if (dtResultado.Rows.Count > 0)
        {
            lblCodigo.Text = dtResultado.Rows[0]["IDE_PERMISO"].ToString();
            ddlmotivo.SelectedValue = dtResultado.Rows[0]["IDE_MOTIVO"].ToString();
            txtcomentarios .Text = dtResultado.Rows[0]["COMENTARIOS"].ToString();
            txtfin.Text = dtResultado.Rows[0]["FIN"].ToString();
            txtInicio.Text = dtResultado.Rows[0]["INICIO"].ToString();
        }
    }
    protected void eliminarpermiso(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.MANT_TBSOLICITUD_PERMISOS_DELETE_DATOS(Convert.ToInt32(pk));
        ListarPermisos();
    }
    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarPermisos();
    }
    protected void ListarPermisos()
    {
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.MANT_TBSOLICITUD_PERMISOS_SELECT_DATOS(Session["IDE_USUARIO"].ToString(),ddlanio.SelectedValue);
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
}