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
using System.Drawing;
using System.Drawing.Imaging;



public partial class SISTEMA_Intranet : System.Web.UI.Page
{
    string FolderIntranet = ConfigurationManager.AppSettings["FolderIntranet"];
    int TAMANIO_IMG;
    int TIPO;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(Button2);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ParametrosTipo();
            Personal();
            PROMOCION_PERSONAL();
            //Banner();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
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
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
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
    protected void ParametrosTipo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlTipo.DataSource = obj.ListarParametros("INTRANET", "IMG");
        ddlTipo.DataTextField = "DES_ASUNTO";
        ddlTipo.DataValueField = "ID_PARAMETRO";
        ddlTipo.DataBind();
        Banner();


    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Banner();
    }
    

    protected void EliminarFile(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderIntranet);
        //int IDE_CARTA = Convert.ToInt32(Request.QueryString["IDE_CARTA"].ToString());

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_BANNER = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_BANNER"].ToString();
        string IMG_URL = GridView1.DataKeys[grdrow.RowIndex].Values["IMG_URL"].ToString();
        string IMG_ZOOM = GridView1.DataKeys[grdrow.RowIndex].Values["IMG_ZOOM"].ToString();

        BL_INTRANET obj = new BL_INTRANET();
        DataTable dtResultado = new DataTable();
        try
        {
            if (File.Exists(path + IMG_URL))
            {
                File.Delete(path + IMG_URL);

            }

            if (File.Exists(path + IMG_ZOOM))
            {
                File.Delete(path + IMG_ZOOM);

            }
        }
        catch (Exception ex)
        {

        }

        dtResultado = obj.SP_ELIMINAR_LISTAR_BANNER(Convert.ToInt32(IDE_BANNER));
        Banner();
    }
    protected void Banner()
    {

     
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_PARAMETRO_LISTAR_IDE(Convert.ToInt32(ddlTipo.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            lblImg.Text = dtResultado.Rows[0]["DES_CAMPO4"].ToString();
            string IN_ORDEN = dtResultado.Rows[0]["IN_ORDEN"].ToString();
            TIPO = Convert.ToInt32(dtResultado.Rows[0]["IN_ORDEN"].ToString());

            BL_INTRANET Xobj = new BL_INTRANET();
            DataTable dt = new DataTable();
            dt = Xobj.SP_LISTAR_BANNER(Convert.ToInt32(IN_ORDEN),"");
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        if (ddlTipo.SelectedIndex==0)
        {
            lblUrl.Text = "Clasificación";
        }
        else
        {
            lblUrl.Text = "Url";
        }
       
    }

    protected void VerDatos(object sender, EventArgs e)
    {
        
        string cleanMessage = string.Empty;
       

        ImageButton btnVer = ((ImageButton)sender);
        GridViewRow row = btnVer.NamingContainer as GridViewRow;

        int IDE_BANNER = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0].ToString());

        BL_INTRANET obj = new BL_INTRANET();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.SP_LISTAR_BANNER_IDE(IDE_BANNER);
        if (dtResultado.Rows.Count > 0)
        {
            lblCodigo.Text = dtResultado.Rows[0]["IDE_BANNER"].ToString();
            ddlEstados.SelectedValue = dtResultado.Rows[0]["ESTADO"].ToString();
            ddlOrden.SelectedValue = dtResultado.Rows[0]["ORDEN"].ToString();

            txtdescripcion.Text  = dtResultado.Rows[0]["DESCRIPCION"].ToString();
            txtUrl.Text = dtResultado.Rows[0]["URL"].ToString();
          
        }
        //////////////////////////////////////
    }
    private static System.Drawing.Image RedimensionarImagen(Stream stream, int tamanio)
    {
        // Se crea un objeto Image, que contiene las propiedades de la imagen
        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

        // Tamaño máximo de la imagen (altura o anchura)
         int max = tamanio;

        int h = img.Height;
        int w = img.Width;
        int newH, newW;

        if (h > w && h > max)
        {
            // Si la imagen es vertical y la altura es mayor que max,
            // se redefinen las dimensiones.
            newH = max;
            newW = (w * max) / h;
        }
        else if (w > h && w > max)
        {
            // Si la imagen es horizontal y la anchura es mayor que max,
            // se redefinen las dimensiones.
            newW = max;
            newH = (h * max) / w;
        }
        else
        {
            newH = h;
            newW = w;
        }
        if (h != newH && w != newW)
        {
            // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
              System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
        }
        else
            return img;
    }

    protected void cargar()
    {
        string cleanMessage = string.Empty;
        String datos = string.Empty;
        string ArchivoIMG = string.Empty;
        string ArchivoZOOM = string.Empty;
        String fileExtension = string.Empty;
        String fileExtensionZOOM = string.Empty;
        ImageFormat format;
        ImageFormat formatFoto;

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_PARAMETRO_LISTAR_IDE(Convert.ToInt32(ddlTipo.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            TIPO = Convert.ToInt32(dtResultado.Rows[0]["IN_ORDEN"].ToString());
            TAMANIO_IMG = Convert.ToInt32(dtResultado.Rows[0]["DES_CAMPO2"].ToString());

        }

        //IMG IMAGEN
        Boolean fileOKIMG = false;
        String pathLogo = Server.MapPath(FolderIntranet);

        if (FileUpload1.HasFile)
        {
            fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOKIMG = true;
                }
            }
        }

        if (fileOKIMG)
        {
            try
            {
                //if (File.Exists(pathLogo + ArchivoIMG))
                //{
                //    File.Delete(pathLogo + ArchivoIMG);
                //}
                ArchivoIMG = "IMG_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                System.Drawing.Image img = RedimensionarImagen(FileUpload1.PostedFile.InputStream, TAMANIO_IMG);
                switch (fileExtension)
                {
                    case "gif":
                        format = ImageFormat.Gif;
                        break;
                    case "bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case "png":
                        format = ImageFormat.Png;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }
                img.Save(pathLogo + ArchivoIMG, format);


            }
            catch (Exception ex)
            {
                //cleanMessage = "Archivo logo, no puedo subir";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        //FIN IMG

        //portada imagen
        Boolean fileOKZOOM = false;
        String path = Server.MapPath(FolderIntranet);

        if (FileUpload2.HasFile)
        {
            fileExtensionZOOM = Path.GetExtension(FileUpload2.PostedFile.FileName);

            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtensionZOOM == allowedExtensions[i])
                {
                    fileOKZOOM = true;
                }
            }
        }

        if (fileOKZOOM)
        {
            try
            {


          
                //if (File.Exists(path + ArchivoZOOM))
                //{
                //    File.Delete(path + ArchivoZOOM);
                //}
                ArchivoZOOM = "ZOOM_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload2.PostedFile.FileName);
                //System.Drawing.Image imgFoto = RedimensionarImagen(FileUpload1.PostedFile.InputStream, 540);
                switch (fileExtensionZOOM)
                {
                    case "gif":
                        formatFoto = ImageFormat.Gif;
                        break;
                    case "bmp":
                        formatFoto = ImageFormat.Bmp;
                        break;
                    case "png":
                        formatFoto = ImageFormat.Png;
                        break;
                    default:
                        formatFoto = ImageFormat.Jpeg;
                        break;
                }
                FileUpload2.SaveAs(path + ArchivoZOOM);


            }
            catch (Exception ex)
            {
                //cleanMessage = "File could not be uploaded.";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        //FIN PORTADA
       

        BE_INTRANET_BANER oBESol = new BE_INTRANET_BANER();
        oBESol.IDE_BANNER = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        oBESol.DESCRIPCION = txtdescripcion.Text.Trim () ;
        oBESol.IMG_URL = ArchivoIMG;
        oBESol.IMG_ZOOM = ArchivoZOOM;
        oBESol.RUTA = FolderIntranet;
        oBESol.URL = txtUrl.Text.Trim();
        oBESol.FLG_ESTADO = Convert.ToInt32(ddlEstados.SelectedValue);
        oBESol.ORDEN = Convert.ToInt32(ddlOrden.SelectedValue); 
        oBESol.TIPO = TIPO.ToString();
        oBESol.TIPO_DESCRIPCION = ddlTipo.SelectedItem.ToString();
        oBESol.DESCRIPCION_ADICIONAL = txtUrl.Text.Trim();
        int rpta;
        rpta = new BL_INTRANET_BANER().uspINS_INTRANET_BANER(oBESol);
        if (rpta > 0)
        {
            Banner();

            cleanMessage = "Registro exitoso.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            lblCodigo.Text = string.Empty;
            txtUrl.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
           
            //CheckSI.Checked = false;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ddlTipo.SelectedIndex == 0)
        {
            cargarArchivos();
        }
        else
        {
            cargar();
        }
       
    }
    protected void cargarArchivos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_PARAMETRO_LISTAR_IDE(Convert.ToInt32(ddlTipo.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            TIPO = Convert.ToInt32(dtResultado.Rows[0]["IN_ORDEN"].ToString());


        }

        string cleanMessage = string.Empty;
        // Si el directorio no existe, crearlo
        if (!Directory.Exists(Server.MapPath(FolderIntranet)))
            Directory.CreateDirectory(FolderIntranet);

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
                string ruta = Server.MapPath(FolderIntranet);

                // Si el directorio no existe, crearlo
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.FileName);

                // Verificar que el archivo no exista
                if (File.Exists(archivo))
                {
                    fileArchivo = "FILE_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(ruta + fileArchivo);
                }

                else
                {
                    fileArchivo = FileUpload1.PostedFile.FileName;
                    FileUpload1.SaveAs(archivo);
                }

                

                BE_INTRANET_BANER oBESol = new BE_INTRANET_BANER();
                oBESol.IDE_BANNER = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
                oBESol.DESCRIPCION = txtdescripcion.Text.Trim();
                oBESol.IMG_URL = fileArchivo;
                oBESol.IMG_ZOOM = string.Empty ;
                oBESol.RUTA = FolderIntranet;
                oBESol.URL = txtUrl.Text.Trim();
                oBESol.FLG_ESTADO = Convert.ToInt32(ddlEstados.SelectedValue);
                oBESol.ORDEN = Convert.ToInt32(ddlOrden.SelectedValue);
                oBESol.TIPO = TIPO.ToString();
                oBESol.TIPO_DESCRIPCION = ddlTipo.SelectedItem.ToString();
                oBESol.DESCRIPCION_ADICIONAL = txtUrl.Text.Trim();
                int rpta;
                rpta = new BL_INTRANET_BANER().uspINS_INTRANET_BANER(oBESol);
                if (rpta > 0)
                {
                    Banner();

                    cleanMessage = "Registro exitoso.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    lblCodigo.Text = string.Empty;
                    txtUrl.Text = string.Empty;
                    txtdescripcion.Text = string.Empty;

                    //CheckSI.Checked = false;
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
            BE_INTRANET_BANER oBESol = new BE_INTRANET_BANER();
            oBESol.IDE_BANNER = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.DESCRIPCION = txtdescripcion.Text.Trim();
            oBESol.IMG_URL = string.Empty;
            oBESol.IMG_ZOOM = string.Empty;
            oBESol.RUTA = FolderIntranet;
            oBESol.URL = txtUrl.Text.Trim();
            oBESol.FLG_ESTADO = Convert.ToInt32(ddlEstados.SelectedValue);
            oBESol.ORDEN = Convert.ToInt32(ddlOrden.SelectedValue);
            oBESol.TIPO = TIPO.ToString();
            oBESol.TIPO_DESCRIPCION = ddlTipo.SelectedItem.ToString();
            oBESol.DESCRIPCION_ADICIONAL = txtUrl.Text.Trim();
            int rpta;
            rpta = new BL_INTRANET_BANER().uspINS_INTRANET_BANER(oBESol);
            if (rpta > 0)
            {
                Banner();

                cleanMessage = "Registro exitoso.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                lblCodigo.Text = string.Empty;
                txtUrl.Text = string.Empty;
                txtdescripcion.Text = string.Empty;

                //CheckSI.Checked = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void ddlNuevo_SelectedIndexChanged(object sender, EventArgs e)
    {
        PROMOCION_PERSONAL();
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    { 
        BL_INTRANET obj = new BL_INTRANET();
        DataTable dtResultado = new DataTable();

        string cleanMessage;
        if (ddlPersonal.SelectedValue == string.Empty )
        {
            cleanMessage = "Falta seleccionar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            cleanMessage = "Registro exitoso.";
            dtResultado = obj.SP_UPDATE_ANUNCIOS_PERSONAL(Convert.ToInt32(ddlNuevo.SelectedValue), ddlPersonal.SelectedValue, txtnuevo.Text.Trim(), ddlProceso.SelectedValue);
            PROMOCION_PERSONAL();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        txtnuevo.Text = string.Empty;
        ddlPersonal.Text = string.Empty;
    }
    protected void datosPersonal(object sender, EventArgs e)
    {

        string cleanMessage = string.Empty;


        ImageButton btnDatos = ((ImageButton)sender);
        GridViewRow row = btnDatos.NamingContainer as GridViewRow;

        string ID_DNI = GridView2.DataKeys[row.RowIndex].Values[0].ToString();

        BL_RRHH_PERSONAL_EMPRESA obj = new BL_RRHH_PERSONAL_EMPRESA();
        DataTable dtResultado = new DataTable();
        string dni = BL_Session.Usuario;
        dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID(ID_DNI);
        
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.SelectedValue = dtResultado.Rows[0]["ID_DNI"].ToString();
            Boolean FLG_INGRESO_NUEVO =Convert.ToBoolean(dtResultado.Rows[0]["FLG_INGRESO_NUEVO"].ToString());
            Boolean FLG_ASCENSO_NUEVO = Convert.ToBoolean(dtResultado.Rows[0]["FLG_ASCENSO_NUEVO"].ToString());
          
            txtnuevo.Text = dtResultado.Rows[0]["DESCRIPCION_NUEVO"].ToString();

            int PROMO = 1;
            if (FLG_INGRESO_NUEVO == true )
            {
                PROMO = 1;
            }
            else if (FLG_ASCENSO_NUEVO == true)
            {
                PROMO = 2;
            }
            ddlNuevo.SelectedValue = PROMO.ToString();
        }
        //////////////////////////////////////
    }
    protected void PROMOCION_PERSONAL()
    {


        
            BL_INTRANET Xobj = new BL_INTRANET();
            DataTable dt = new DataTable();
            dt = Xobj.SP_LISTAR_ANUNCIOS_PERSONAL(Convert.ToInt32(ddlNuevo.SelectedValue));
            this.GridView2.DataSource = dt;
            this.GridView2.DataBind();
        
    }
}