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

public partial class OPERACIONES_Solped : System.Web.UI.Page
{
    string FolderSOLPED = ConfigurationManager.AppSettings["FolderSOLPED"];
    string FolderSOLPEDBackups = ConfigurationManager.AppSettings["FolderSOLPEDBackups"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnviar);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //ControlCecos();
            ControlProcesoEmpresa();
            //botones();
            Anios();
            Personal();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();
            Listar();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCentro.Text = BL_Session.CENTRO_COSTO.ToString();
            TipoSolicitud();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
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
    protected void TipoSolicitud()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlTipo.DataSource = obj.ListarParametros("TIPO_SOLICITUD", "SOLPED");
        ddlTipo.DataTextField = "DES_ASUNTO";
        ddlTipo.DataValueField = "ID_PARAMETRO";
        ddlTipo.DataBind();
        ddlTipo.Items.Insert(0, new ListItem("--- SELECCIONAR ---", ""));

    }
    protected void ControlProcesoEmpresa()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_EMPRESA(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN");
        if (dtResultado.Rows.Count > 0)
        {

            ddlEmpresa.DataSource = dtResultado;
            ddlEmpresa.DataTextField = "DES_ABREV";
            ddlEmpresa.DataValueField = "ID_EMPRESA";
            ddlEmpresa.DataBind();
            //ddlEmpresa.Items.Insert(0, new ListItem("--- TODOS ---", ""));
            botones();
            ControlCecos();
           
        }

    }
    protected void ControlCecos()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_CENTRO(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN");
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_SOLPED(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN", ddlEmpresa.SelectedValue.ToString());
        
        if (dtResultado.Rows.Count > 0)
        {
            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "CENTRO";
            ddlcentro.DataValueField = "CENTRO";
            ddlcentro.DataBind();
            //ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }


    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        //dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_CC(ddlcentro.SelectedValue.ToString());
        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
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
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN", ddlEmpresa.SelectedValue.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            btnBandeja.Visible = true;
            btnSolicitar.Visible = true;
        }
        else
        {
            dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN (SERVICIOS Y TRANSPORTE)", ddlEmpresa.SelectedValue.ToString());
            if (dtResultado.Rows.Count > 0)
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
        else if (txtCodigo.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar codigo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlTipo.SelectedIndex == 0)
        {
            cleanMessage = "Falta indicar tipo de solicitud";
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
            if (!Directory.Exists(Server.MapPath(FolderSOLPED)))
                Directory.CreateDirectory(FolderSOLPED);

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
                    if (fileExtension.ToUpper() == allowedExtensions[i].ToUpper())
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
                    string ruta = Server.MapPath(FolderSOLPED);
                    string rutaBackups = FolderSOLPEDBackups;
                    // Si el directorio no existe, crearlo
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.PostedFile.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = ddlcentro.SelectedValue.ToString() + "_" + txtCodigo.Text.Trim() + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                        FileUpload1.SaveAs(rutaBackups + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = ddlcentro.SelectedValue.ToString() + "_" + txtCodigo.Text.Trim() + "_"  + Path.GetFileName(FileUpload1.FileName);
                        //FileUpload1.SaveAs(archivo);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                        FileUpload1.SaveAs(rutaBackups + fileArchivo);
                    }

                    BE_SOLPED oBESol = new BE_SOLPED();
                    oBESol.IDE_SOLPED = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
                    oBESol.FECHA = txtFecha.Text.Trim();
                    oBESol.IDE_USUARIO = Session["IDE_USUARIO"].ToString();
                    oBESol.FILE_SOLPED = fileArchivo;
                    oBESol.FILE_RUTA = FolderSOLPED;
                    oBESol.COMENTARIOS = txtcomentarios.Text.ToString();
                    oBESol.IPCENTRO = ddlcentro.SelectedValue.ToString(); //BL_Session.IP_CENTRO.ToString();
                    oBESol.CODIGO = txtCodigo.Text.Trim();
                    oBESol.CODIGO_SI = txtSI.Text.Trim();
                    oBESol.SOLICITANTE = ddlPersonal.SelectedValue.ToString();
                    oBESol.TIPO_SOLICITUD = ddlTipo.SelectedValue.ToString();
                    oBESol.IDE_EMPRESA = ddlEmpresa.SelectedValue.ToString();
                    int dtrpta = 0;
                    dtrpta = new BL_SOLPED().uspINS_SOLPED(oBESol);
                    if (dtrpta > 0)
                    {
                        Listar();

                        BL_SOLPED objCorreo = new BL_SOLPED();
                        objCorreo.SP_ENVIARCORREO_SOLPED(dtrpta, Session["IDE_USUARIO"].ToString());
                        cleanMessage = "Registro exitoso.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        lblCodigo.Text = string.Empty;
                        txtCodigo.Text = string.Empty;
                        txtcomentarios.Text = string.Empty;
                        txtSI.Text = string.Empty;
                        ddlPersonal.Text = string.Empty;
                        //CheckSI.Checked = false;
                    }
                    else
                    {
                        cleanMessage = "Advertencia: Codigo ingresado ya se encuentra registrado.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }

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

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();

        string anio = string.Empty;
        if (ddlanio.SelectedIndex == 0)
        {
            anio = string.Empty;
        }
        else
        {
            anio = ddlanio.SelectedValue.ToString();
        }

        dtResultado = obj.uspSEL_SOLPED_USUARIO(Session["IDE_USUARIO"].ToString(), estado, anio);
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
            ddlanio.Items.Insert(0, new ListItem("--- TODOS ---", ""));

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
        Response.Redirect("~/OPERACIONES/Solped.aspx");
    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/SolpedBandeja.aspx");
    }

    //protected void CheckSI_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckSI.Checked)
    //    {
    //        txtSI.ReadOnly = false ;
    //        txtSI.Focus();
    //    }
    //    else
    //    {
    //        txtSI.ReadOnly = true ;
    //    }

    //}



    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Solicitud";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Atención";
            HeaderCell.ColumnSpan = 6;
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

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlCecos();
    }
}