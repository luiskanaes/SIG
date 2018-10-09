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

public partial class OPERACIONES_Andamios : System.Web.UI.Page
{
    string FolderANDAMIOS = ConfigurationManager.AppSettings["FolderANDAMIOS"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnviar);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ControlCecos();
            botones();
            Anios();
            Personal();
            Especialidad();
            Solicitud();
            Tipo();
            area();


            ddlanio.SelectedValue = DateTime.Today.Year.ToString();
            Listar();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCentro.Text = BL_Session.CENTRO_COSTO.ToString();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
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
    protected void ControlCecos()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_CENTRO(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN");

        if (dtResultado.Rows.Count > 0)
        {
            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "CENTRO";
            ddlcentro.DataValueField = "CENTRO";
            ddlcentro.DataBind();
        }


    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_CC(ddlcentro.SelectedValue.ToString());
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

    protected void botones()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ANDAMIOS", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            btnBandeja.Visible = true;
            btnSolicitar.Visible = true;
        }
        else
        {

            DataTable dt = new DataTable();
            dt = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ANDAMIOS SUPERVISOR", BL_Session.ID_EMPRESA.ToString());

            if (dt.Rows.Count > 0)
            {
                btnBandeja.Visible = true;
                btnSolicitar.Visible = true;
            }
            else
            {
                btnBandeja.Visible = false;
                btnSolicitar.Visible = false;
            }
        }

    }

    protected void btnviar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txtFecha.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de solicitud";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtrequerida.Text.Trim() == string.Empty)
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
                        fileArchivo = "AND_"  + ddlcentro.SelectedValue.ToString() + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = "AND_" + ddlcentro.SelectedValue.ToString() + "_" + Path.GetFileName(FileUpload1.FileName);
                        //FileUpload1.SaveAs(archivo);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                    }

                    BE_SOL_ANDAMIOS oBESol = new BE_SOL_ANDAMIOS();
                    oBESol.IDE_ANDAMIOS = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
                    oBESol.ANDAMIOS = txtCodAndamio.Text.Trim();
                    oBESol.FECHA = txtFecha.Text.Trim();
                    oBESol.IDE_USUARIO = Session["IDE_USUARIO"].ToString();
                    oBESol.SOLICITANTE = ddlPersonal.SelectedValue.ToString();
                    oBESol.FILE_ANDAMIOS = fileArchivo;
                    oBESol.FILE_RUTA = FolderANDAMIOS;
                    oBESol.COMENTARIOS = txtcomentarios.Text.ToString();
                    oBESol.ESPECIALIDAD = ddlespecialidad.SelectedValue;
                    oBESol.AREA = ddlarea.SelectedValue;
                    oBESol.SOLICITUD = ddlSolicitud.SelectedValue;
                    oBESol.TIPO = ddlTipo .SelectedValue;
                    oBESol.FECHA_REQUERIDA = txtrequerida.Text;

                    oBESol.IPCENTRO = ddlcentro.SelectedValue.ToString(); //BL_Session.IP_CENTRO.ToString();
  
                   
                    int dtrpta = 0;
                    dtrpta = new BL_SOL_ANDAMIOS().uspINS_SOL_ANDAMIOS(oBESol);
                    if (dtrpta > 0)
                    {
                        Listar();

                        BL_SOL_ANDAMIOS objCorreo = new BL_SOL_ANDAMIOS();
                        objCorreo.SP_ENVIARCORREO_SOL_ANDAMIOS(dtrpta, Session["IDE_USUARIO"].ToString());
                        cleanMessage = "Registro exitoso.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        lblCodigo.Text = string.Empty;
                        txtcomentarios.Text = string.Empty;
                        ddlPersonal.Text = string.Empty;
                        txtCodAndamio.Text = string.Empty;
                    }
                    //else
                    //{
                    //    cleanMessage = "Advertencia: Codigo ingresado ya se encuentra registrado.";
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    //}

                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "Falta cargar archivo de solicitud";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }


        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }
    protected void Listar()
    {
        string estado = string.Empty;
        if (ddlEstados.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString();
        }

        BL_SOL_ANDAMIOS obj = new BL_SOL_ANDAMIOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_SOL_ANDAMIOS_USUARIO(Session["IDE_USUARIO"].ToString(), estado, ddlanio.SelectedValue.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            //GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            //GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
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
    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Andamios.aspx");
    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/AndamiosBandeja.aspx");
    }


    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Solicitud";
            HeaderCell.ColumnSpan = 12;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Prioridad";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.BackColor = System.Drawing.Color.Orange;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Atención";
            HeaderCell.ColumnSpan = 10;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }


    protected void ddlcentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Personal();
    }
}