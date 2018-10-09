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
public partial class CAREMENOR_PDC_Adjunto : System.Web.UI.Page
{
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string FolderAlquilerBackups = ConfigurationManager.AppSettings["FolderAlquilerBackups"];
    string FolderFTP = ConfigurationManager.AppSettings["FolderFTP"];


    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardar);
        if (!Page.IsPostBack)
        {
            Moneda();
            Estados();
            txtFechaPDC.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            datos();
            file();
            GrupoFile();
        }
    }
    protected void GrupoFile()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.LISTAR_GRUPO_SOLPED(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
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
    protected void Moneda()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableMoneda();
        if (dtResultado.Rows.Count > 0)
        {
            ddlMoneda.DataSource = dtResultado;
            ddlMoneda.DataTextField = "descripcion";
            ddlMoneda.DataValueField = "codigo";
            ddlMoneda.DataBind();

        }
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


    }
    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "EN PROCESO");
        table.Rows.Add(2, "ATENDIDO");
        table.Rows.Add(3, "ANULADO");
        table.Rows.Add(4, "STOCK SSK");

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




    public  int registroPDC(string _Requ_Numero, string _Reqd_CodLinea, string _Reqs_Correlativo, decimal valor, decimal total, int contador, int registros, decimal valorMov, string G)
    {
        string ruta = Server.MapPath(FolderAlquiler);
        string rutaBackups = FolderAlquilerBackups;
        int dtrpta = 0;
        string cleanMessage = string.Empty;

        if (txtFechaPDC.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de PDC";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtPdc.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar PDC";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtPdc.Text.Length < 10)
        {
            cleanMessage = "Favor de ingresar los 10 digtos de la PDC";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        //else if (txtmonto.Text.Trim() == string.Empty)
        //{
        //    cleanMessage = "Ingresar monto PDC";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
        else
        {

            // Si el directorio no existe, crearlo
            //if (!Directory.Exists(Server.MapPath(FolderAlquiler)))
            //    Directory.CreateDirectory(FolderAlquiler);

            String fileExtension = string.Empty;
            Boolean fileOK = false;
            string fileArchivo = string.Empty;
            if (FileUploadGuia.HasFile)
            {

                string fileName = FileUploadGuia.FileName.ToString();
                int length = FileUploadGuia.PostedFile.ContentLength;

                fileExtension = Path.GetExtension(FileUploadGuia.FileName).ToUpper();

                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i].ToUpper())
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                try
                {
                   
                    // Si el directorio no existe, crearlo
                    //if (!Directory.Exists(ruta))
                    //    Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUploadGuia.PostedFile.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales("PDC_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUploadGuia.PostedFile.FileName));
                        fileArchivo = fileArchivo.Replace("&", "y");
                        FileUploadGuia.SaveAs(ruta + fileArchivo);
                        FileUploadGuia.SaveAs(rutaBackups + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales("PDC_" + Path.GetFileName(FileUploadGuia.FileName));
                        fileArchivo = fileArchivo.Replace("&", "y");
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


            BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
            oBESol.Requ_Numero = _Requ_Numero;
            oBESol.Reqd_CodLinea = _Reqd_CodLinea;
            oBESol.Reqs_Correlativo = _Reqs_Correlativo;
            oBESol.D_PDC = txtPdc.Text.Trim();
            oBESol.D_OBSERVACION_RUTA = FolderAlquiler;
            oBESol.D_GUIA_RUTA = FolderAlquiler;
            oBESol.D_GUIA_FILE = fileArchivo;
            oBESol.D_PDC_FECHA = txtFechaPDC.Text;
            oBESol.D_PDC_MONEDA = Convert.ToInt32(ddlMoneda.SelectedValue);
            oBESol.D_PDC_MONTO = Convert.ToDecimal(string.IsNullOrEmpty(valor.ToString()) ? "0" : valor.ToString());

            int ampliacion = 0;
            if (CheckAmpliacion.Checked)
            {
                ampliacion = 1;
            }
            else
            {
                ampliacion = 0;
            }

            oBESol.D_AMPLIACION = ampliacion;
            oBESol.D_PDC_MONTO_TOTAL = Convert.ToDecimal(string.IsNullOrEmpty(total.ToString()) ? "0" : total.ToString());
            oBESol.D_PDC_MONTO_MOVIL = Convert.ToDecimal(string.IsNullOrEmpty(valorMov.ToString()) ? "0" : valorMov.ToString());
            oBESol.GUID = G.ToString();
            dtrpta = new BL_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_PDC(oBESol);
           

            if (contador == registros)
            {


                ////**********************************************************
                //******** CREAR DIRECTORIO PROYECTO ******************************
                string Proyecto = Request.QueryString["Requ_Numero"].ToString();

                string rutaOBRA = FolderFTP + Proyecto.Substring(0, 5);

                // Si el directorio no existe, crearlo
                if (!Directory.Exists(rutaOBRA))//directorio OBRA
                    Directory.CreateDirectory(rutaOBRA);

                //DIRECTORIO  PDC
                string rutaSAT = Path.Combine(rutaOBRA, "SAT");
                if (!Directory.Exists(rutaSAT))//directorio final
                    Directory.CreateDirectory(rutaSAT);

                string DIRECTORIO_PDC = string.Empty;
                BL_TBL_RequerimientoSubDetalle objx = new BL_TBL_RequerimientoSubDetalle();
                DataTable dt = new DataTable();
                dt = objx.SP_LISTAR_ARCHIVOS_PDC_TODOS(txtPdc.Text.Trim());
                if (dt.Rows.Count> 0)
                {
                    DIRECTORIO_PDC = dt.Rows[0]["DIRECTORIO"].ToString();

                    //DIRECTORIO  CODIGO DE PDC
                    string rutaPDC_CODIGO = Path.Combine(rutaSAT, DIRECTORIO_PDC);
                    if (!Directory.Exists(rutaPDC_CODIGO))//directorio final
                        Directory.CreateDirectory(rutaPDC_CODIGO);

                    BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                    DataTable dtResultado = new DataTable();
                    dtResultado = obj.SP_LISTAR_ARCHIVOS_PDC(txtPdc.Text.Trim());
                    for (int i = 0; i < dtResultado.Rows.Count; i++)
                    {


                        string adjunto = dtResultado.Rows[i]["ARCHIVO"].ToString();
                        if (File.Exists(Path.Combine(ruta, adjunto)))
                        {
                            File.Copy(Path.Combine(ruta, adjunto), Path.Combine(rutaPDC_CODIGO, adjunto), true);
                        }

                    }

                }


                


                cleanMessage = "Registro exitoso.";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                datos();
                file();
                GrupoFile();
            }

           
        }
        return dtrpta;
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

                txtPdc.Text = dtResultado.Rows[0]["D_PDC"].ToString();
                txtFechaPDC.Text = dtResultado.Rows[0]["D_PDC_FECHA"].ToString();
                txtmonto.Text = dtResultado.Rows[0]["D_PDC_MONTO_TOTAL"].ToString();

                ddlMoneda.SelectedValue = dtResultado.Rows[0]["D_PDC_MONEDA"].ToString();
            }
            catch (Exception ex)
            {

            }

        }
    }
    protected void file()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.uspSEL_TBL_REQUERIMIENTOSUBDETALLE_PDC(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
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

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;


        string _Requ_Numero = string.Empty;
        string _Reqd_CodLinea = string.Empty;
        string _Reqs_Correlativo = string.Empty;
        decimal total = 0;
        string valor ;
        string valorMov;
        int registros = 0;
        int contador = 0;
        int insert = 0;
        registros = GridView2.Rows.Count;

        foreach (GridViewRow Fila in GridView2.Rows)
        {
            TextBox txtValor = ((TextBox)Fila.FindControl("txtValor"));
            TextBox txtValorMov = ((TextBox)Fila.FindControl("txtValorMov"));

            valorMov = string.IsNullOrEmpty(txtValorMov.Text) ? "0" : txtValorMov.Text;
            valor = string.IsNullOrEmpty(txtValor.Text) ? "0" : txtValor.Text;

            
            if (isInt32(valorMov) == true)
            {
                if (isInt32(valorMov) == true)
                {
                    total = total + Convert.ToDecimal(string.IsNullOrEmpty(txtValor.Text) ? "0" : txtValor.Text) + Convert.ToDecimal(string.IsNullOrEmpty(txtValorMov.Text) ? "0" : txtValorMov.Text);

                }
                else
                {
                    cleanMessage = "Error de digitación";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "Error de digitación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }

        Guid g = Guid.NewGuid();
        foreach (GridViewRow row in GridView2.Rows)
        {
            _Requ_Numero = GridView2.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
            _Reqd_CodLinea = GridView2.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
            _Reqs_Correlativo = GridView2.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key

            TextBox txtValor = ((TextBox)row.FindControl("txtValor"));
            TextBox txtValorMov = ((TextBox)row.FindControl("txtValorMov"));

           
            valorMov = string.IsNullOrEmpty(txtValorMov.Text) ? "0" : txtValorMov.Text;
            valor = string.IsNullOrEmpty(txtValor.Text) ? "0" : txtValor.Text;

            contador++;
            
            if(isInt32(valorMov)==true)
            {
                if (isInt32(valorMov) == true)
                {
                    int result = registroPDC(_Requ_Numero, _Reqd_CodLinea, _Reqs_Correlativo,Convert.ToDecimal( valor), total, contador, registros, Convert.ToDecimal(valorMov), g.ToString());

                    if (result == 1)
                    {
                        insert++;
                    }
                }
                else
                {
                    cleanMessage = "Error de digitación";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "Error de digitación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

           
        }

        if (insert > 0)
        {
            enviarCorreo();
        }


    }
    public bool isInt32(String num)
    {
        try
        {
            decimal.Parse(num);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void enviarCorreo()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();

        string url = URLSSK;
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        string mensaje = "El Sistema de Equipos Menores SSK informa, que se registró los datos de la PDC " + txtPdc.Text + ", de lista de equipos solicitados por el  proyecto ";


        obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO_PDC(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER CARE SOLPED", url);

        //string cleanMessage = "Envio satisfactorio";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
    }

    protected void VerdetallePrecio(object sender, EventArgs e)

    {
        ImageButton btnSelect = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;


        string _Requ_Numero = GridView2.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string _Reqd_CodLinea = GridView2.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string _Reqs_Correlativo = GridView2.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();


        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.uspSEL_TBL_REQUERIMIENTOSUBDETALLE_PDC_HISTORIAL(_Requ_Numero, _Reqd_CodLinea, _Reqs_Correlativo);
        if (dtResultado.Rows.Count > 0)
        {
            GridView3.DataSource = dtResultado;
            GridView3.DataBind();
        }
        else
        {
            GridView3.DataSource = dtResultado;
            GridView3.DataBind();
        }
        ModalRegistro.Show();
    }

}