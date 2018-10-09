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

public partial class CAREMENOR_EquipoMayoresAtencion2 : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string FolderAlquilerBackups = ConfigurationManager.AppSettings["FolderAlquilerBackups"];
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";


        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnCargar);
        if (!Page.IsPostBack)
        {
            Moneda();
            Estados();

            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            datos();
        }
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
    protected void Moneda()
    {

        
    }
    static DataTable GetTableMoneda()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "SOLES");
        table.Rows.Add(2, "DOLARES");


        return table;
    }
    protected void Estados()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableEstados();
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstado.DataSource = dtResultado;
            ddlEstado.DataTextField = "descripcion";
            ddlEstado.DataValueField = "codigo";
            ddlEstado.DataBind();

        }
    }
    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "EN PROCESO");
        table.Rows.Add(2, "ATENDIDO TERCEROS");
        table.Rows.Add(3, "ANULADO");
        table.Rows.Add(4, "ATENDIDO SSK");

        return table;
    }
  
    static DataTable GetTableDocumento()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "LEGAJO");
        table.Rows.Add(2, "ADJUDICACIÓN");


        return table;
    }

    static DataTable GetTableMovilizacion()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "SI");
        table.Rows.Add(0, "NO");


        return table;
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        string estado = ddlEstado.SelectedItem.ToString();
        

        if (txtPDC.Text.Trim() != string.Empty && estado == "ANULADO")
        {
            cleanMessage = "No se puede realizar esta operación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtPDC.Text.Trim() != string.Empty && estado == "ATENDIDO SSK")
        {
            cleanMessage = "No se puede realizar esta operación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtPDC.Text.Trim() == string.Empty && estado == "ATENDIDO TERCEROS")
        {
            cleanMessage = "Falta ingresar PDC";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            registroPDC();
            //UpdateCerrar();
        }
        
        

    }
    protected void UpdateCerrar()
    {
        if (ddlEstado.SelectedValue.ToString() == "2")
        {
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();
            string mensaje = "El Sistema de Equipos Menores SSK informa, la atención del requerimiento " + Requ_Numero + ", del proyecto ";
            string url = URLSSK;
            obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER GERENTE", url);


        }


        if (ddlEstado.SelectedValue.ToString() == "3")
        {
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();
            string url = URLSSK;
            string mensaje = "El Sistema de Equipos Menores SSK informa, la anulación del requerimiento " + Requ_Numero + ", del proyecto ";
            obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER GERENTE", url);
        }
    }

    protected void registroPDC()
    {
        string estado = ddlEstado.SelectedItem.ToString();
        string cleanMessage = string.Empty;

        if (txtFechaProyecto.Text.Trim() == string.Empty && estado == "ATENDIDO TERCEROS" )
        {
            cleanMessage = "Ingresar fecha de envio proyecto";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtFechaProyecto.Text.Trim() != string.Empty && estado == "ATENDIDO SSK")
        {
            cleanMessage = "Quitar fecha de envio proyecto (" + estado + ")";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            // Si el directorio no existe, crearlo
            if (!Directory.Exists(Server.MapPath(FolderAlquiler)))
                Directory.CreateDirectory(FolderAlquiler);

            String fileExtension = string.Empty;
            Boolean fileOK = false;
            string fileArchivo = string.Empty;
            if (FileUploadGuia.HasFile)
            {

                string fileName = FileUploadGuia.FileName.ToString();
                int length = FileUploadGuia.PostedFile.ContentLength;

                fileExtension = Path.GetExtension(FileUploadGuia.FileName);

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
                    string ruta = Server.MapPath(FolderAlquiler);
                    string rutaBackups = FolderAlquilerBackups;
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUploadGuia.PostedFile.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = BL_Session.CENTRO_COSTO + "_GUIA_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUploadGuia.PostedFile.FileName);
                        FileUploadGuia.SaveAs(ruta + fileArchivo);
                        FileUploadGuia.SaveAs(rutaBackups + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = BL_Session.CENTRO_COSTO + "_GUIA_" + Path.GetFileName(FileUploadGuia.FileName);
                        //FileUpload1.SaveAs(archivo);
                        FileUploadGuia.SaveAs(ruta + fileArchivo);
                        FileUploadGuia.SaveAs(rutaBackups + fileArchivo);
                    }

                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }

            String fileExtensionObserva = string.Empty;
            Boolean fileOKObserva = false;
            string fileArchivoObserva = string.Empty;


            if (FileUploadObserva.HasFile)
            {

                string fileName = FileUploadObserva.FileName.ToString();
                int length = FileUploadObserva.PostedFile.ContentLength;

                fileExtensionObserva = Path.GetExtension(FileUploadObserva.FileName);

                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtensionObserva == allowedExtensions[i])
                    {
                        fileOKObserva = true;
                    }
                }
            }
            if (fileOKObserva)
            {
                try
                {
                    string ruta = Server.MapPath(FolderAlquiler);
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUploadObserva.PostedFile.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivoObserva = BL_Session.CENTRO_COSTO + "_OBSERVA_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUploadObserva.PostedFile.FileName);
                        FileUploadObserva.SaveAs(ruta + fileArchivoObserva);
                    }

                    else
                    {
                        fileArchivoObserva = BL_Session.CENTRO_COSTO + "_OBSERVA_" + Path.GetFileName(FileUploadObserva.FileName);
                        //FileUpload1.SaveAs(archivo);
                        FileUploadObserva.SaveAs(ruta + fileArchivoObserva);
                    }

                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }

            ////////////////////////////////
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();


            BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
            oBESol.Requ_Numero = Requ_Numero;
            oBESol.Reqd_CodLinea = Reqd_CodLinea;
            oBESol.Reqs_Correlativo = Reqs_Correlativo;
            oBESol.D_FECHA_ENVIO_OBRA = txtFechaProyecto.Text;
            oBESol.D_CODIGO_CARE = txtCare.Text.Trim();
            oBESol.D_COMENTARIOS = txtComentarios.Text.Trim();
            oBESol.D_OBSERVACION_RUTA = FolderAlquiler;
            oBESol.D_GUIA_RUTA = FolderAlquiler;
            oBESol.D_ESTADO_PROCESO = Convert.ToInt32(ddlEstado.SelectedValue.ToString());
            oBESol.D_FECHA_SALE_OBRA = txtSalida.Text;
            oBESol.D_OBSERVACION_FILE = fileArchivoObserva;
            oBESol.D_GUIA_FILE = fileArchivo;
            oBESol.D_HRAS_MIN = Convert.ToDecimal(string.IsNullOrEmpty(txtHminimas.Text.Trim()) ? "0" : txtHminimas.Text.Trim());
            oBESol.D_COSTO_HORA = Convert.ToDecimal(string.IsNullOrEmpty(txtCostoxHora.Text.Trim()) ? "0" : txtCostoxHora.Text.Trim());
            oBESol.A_GUIA_INGRESO = txtGuia_i.Text.Trim();
            oBESol.A_SERIE = txtserie.Text.Trim();
            oBESol.A_PLACA = txtPlaca.Text.Trim();
            oBESol.A_GUIA_SALIDA= txtGuia_S.Text.Trim();
            oBESol.USUARIO_ATIENDE = Session["IDE_USUARIO"].ToString();
            oBESol.FECHA_DESPACHO = txtFechaDespacho.Text.Trim();

            int dtrpta = 0;
            dtrpta = new BL_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_PDC_OBSERVA(oBESol);
            if (dtrpta > 0)
            {
                cleanMessage = "Registro exitoso.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                UpdateCerrar();
                datos();
            }
        }
    }
    public void CleanControl(ControlCollection controles)
    {

        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Text = string.Empty;
            //else if (control is DropDownList)
            //    ((DropDownList)control).ClearSelection();
            else if (control is RadioButtonList)
                ((RadioButtonList)control).ClearSelection();
            else if (control is CheckBoxList)
                ((CheckBoxList)control).ClearSelection();
            else if (control is RadioButton)
                ((RadioButton)control).Checked = false;
            else if (control is CheckBox)
                ((CheckBox)control).Checked = false;
            else if (control.HasControls())
                //Esta linea detécta un Control que contenga otros Controles
                //Así ningún control se quedará sin ser limpiado.
                CleanControl(control.Controls);
        }
    }
    protected void datos()
    {
        CleanControl(this.Controls);
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle Obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = Obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_ID(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        if (dtResultado.Rows.Count > 0)
        {
            try
            {


               
               
                txtFechaProyecto.Text = dtResultado.Rows[0]["D_FECHA_ENVIO_OBRA"].ToString();
                txtCare.Text = dtResultado.Rows[0]["D_CODIGO_CARE"].ToString();
                txtComentarios.Text = dtResultado.Rows[0]["D_COMENTARIOS"].ToString();
                ddlEstado.SelectedValue = dtResultado.Rows[0]["D_ESTADO_PROCESO"].ToString();
              
                txtSalida.Text = dtResultado.Rows[0]["D_FECHA_SALE_OBRA"].ToString();

                txtHminimas.Text = dtResultado.Rows[0]["D_HRAS_MIN"].ToString();

                txtCostoxHora.Text = dtResultado.Rows[0]["D_COSTO_HORA"].ToString();

                txtGuia_i.Text = dtResultado.Rows[0]["A_GUIA_INGRESO"].ToString();
                txtserie.Text = dtResultado.Rows[0]["A_SERIE"].ToString();
                txtPlaca.Text = dtResultado.Rows[0]["A_PLACA"].ToString();
                txtGuia_S.Text = dtResultado.Rows[0]["A_GUIA_SALIDA"].ToString();

                txtPDC.Text = dtResultado.Rows[0]["D_PDC"].ToString();
                txtFechaDespacho.Text = dtResultado.Rows[0]["FECHA_DESPACHO"].ToString();
            }
            catch (Exception ex)
            {

            }

        }
    }
}