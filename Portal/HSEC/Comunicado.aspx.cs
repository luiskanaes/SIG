using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
public partial class HSEC_Comunicado : System.Web.UI.Page
{
    string FolderHSEC = ConfigurationManager.AppSettings["FolderHSEC"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnCargar);
        if (!Page.IsPostBack)
        {
            categorias();
            estados();
            listar();
        }
    }

    protected void categorias()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros_orden("ANUNCIOS", "HSEC_ANUNCIOS");

        if (dtResultado.Rows.Count > 0)
        {
            ddlTipo.DataSource = dtResultado;
            ddlTipo.DataTextField = "DES_ASUNTO";
            ddlTipo.DataValueField = "ID_PARAMETRO";
            ddlTipo.DataBind();

            ddlTipo2.DataSource = dtResultado;
            ddlTipo2.DataTextField = "DES_ASUNTO";
            ddlTipo2.DataValueField = "ID_PARAMETRO";
            ddlTipo2.DataBind();
            //ddlMoneda.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    protected void estados()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableEstados();
        if (dtResultado.Rows.Count > 0)
        {
            ddlVisible.DataSource = dtResultado;
            ddlVisible.DataTextField = "DESCRIPCION";
            ddlVisible.DataValueField = "IDE";
            ddlVisible.DataBind();
            //ddlVisible.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
    }
    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("IDE", typeof(string));
        table.Columns.Add("DESCRIPCION", typeof(string));


        table.Rows.Add("1", "SI");
        table.Rows.Add("0", "NO");


        return table;
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (txtinicio.Text == string.Empty)
        {
            cleanMessage = "Ingresar fecha de inicio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txttitulo.Text == string.Empty)
        {
            cleanMessage = "Ingresar titulo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            Guardar();
        }
    }
    protected void Guardar()
    {
        string cleanMessage = string.Empty;
        string RUTA = Server.MapPath(FolderHSEC + ddlTipo.SelectedItem.ToString());
        string ARCHIVO = string.Empty;
        string ARCHIVO_RUTA = string.Empty;
        string ARCHIVO_EXTESION = string.Empty;
        



        Boolean fileOK = false;



        if (FileUpload1.HasFile)
        {

           
            int length = FileUpload1.PostedFile.ContentLength;

            ARCHIVO_EXTESION = Path.GetExtension(FileUpload1.FileName).ToUpper();

            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (ARCHIVO_EXTESION.ToUpper() == allowedExtensions[i].ToUpper())
                {
                    fileOK = true;
                }
            }

            if (fileOK)
            {
                try
                {
                    // Se carga la ruta física de la carpeta temp del sitio
                  
                    //string rutaBackups = FolderAlquilerBackups;
                    // Si el directorio no existe, crearlo

                    if (!Directory.Exists(RUTA))
                        Directory.CreateDirectory(RUTA);

                    ARCHIVO = EliminarCaracteres.ReemplazarCaracteresEspeciales(FileUpload1.PostedFile.FileName);
                    ARCHIVO_RUTA = String.Format("{0}\\{1}", RUTA, ARCHIVO);

                    ImageFormat formatFoto;

                    if (ValidaExtension(ARCHIVO_EXTESION.ToUpper()))
                    {

                        System.Drawing.Image imgFoto = RedimensionarImagen(FileUpload1.PostedFile.InputStream, "700");
                        switch (ARCHIVO_EXTESION.ToUpper())
                        {
                            case "GF":
                                formatFoto = ImageFormat.Gif;
                                break;
                            case "BMP":
                                formatFoto = ImageFormat.Bmp;
                                break;
                            case "PNG":
                                formatFoto = ImageFormat.Png;
                                break;
                            default:
                                formatFoto = ImageFormat.Jpeg;
                                break;
                        }
                    }

                    


                    // Verificar que el archivo no exista
                    if (File.Exists(ARCHIVO_RUTA))
                    {

                        //File.Delete(ARCHIVO_RUTA);
                        
                        ARCHIVO = EliminarCaracteres.ReemplazarCaracteresEspeciales(Path.GetFileName(FileUpload1.FileName));
                        ARCHIVO_RUTA = RUTA +"\\" + ARCHIVO;


                        FileUpload1.SaveAs(ARCHIVO_RUTA);
                        //FileUpload1.SaveAs(rutaBackups + fileArchivo);
                    }

                    else
                    {
                        ARCHIVO = EliminarCaracteres.ReemplazarCaracteresEspeciales(DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName));

                        //FileUpload1.SaveAs(archivo);
                        ARCHIVO_RUTA = RUTA + "\\" + ARCHIVO;
                        FileUpload1.SaveAs(ARCHIVO_RUTA);
                        //FileUpload1.SaveAs(rutaBackups + fileArchivo);
                    }

                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }

            }
        }

                   


        BE_HSEC_ANUNCIOS oBESol = new BE_HSEC_ANUNCIOS();

        oBESol.IDE_ANUNCIO = Convert.ToInt32(string.IsNullOrEmpty(hdcodigo.Value) ? "0" : hdcodigo.Value);
        oBESol.ARCHIVO = ARCHIVO;
        oBESol.ARCHIVO_NOMBRE = txttitulo.Text.Trim();
        oBESol.ARCHIVO_URL = FolderHSEC + ddlTipo.SelectedItem.ToString();
        oBESol.ARCHIVO_EXTESION = ARCHIVO_EXTESION;
        oBESol.TIPO_ANUNCIO = Convert.ToInt32(ddlTipo.SelectedValue);
        oBESol.COMENTARIOS = txtcomentarios.Text.Trim();
        oBESol.URL = txturl.Text.Trim();
        oBESol.ORDEN = 1;
        oBESol.FECHA_INICIO = txtinicio.Text;
        oBESol.FECHA_FIN = txtfin.Text;
        oBESol.FLG_VISIBLE = Convert.ToInt32(ddlVisible.SelectedValue);
        oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
        int dtrpta = 0;
        dtrpta = new BL_HSEC_ANUNCIOS().uspINS_HSEC_ANUNCIOS(oBESol);
        if (dtrpta > 0)
        {
            hdcodigo.Value = string.Empty;
            txtinicio.Text = string.Empty;
            txttitulo.Text = string.Empty;
            txturl.Text = string.Empty;
            txtcomentarios.Text = string.Empty;
            txtfin.Text = string.Empty;
            listar();
            cleanMessage = "Registro exitoso.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }

    }
    protected void ver_datos(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        DataTable dtResultado = new DataTable();
        BL_HSEC_ANUNCIOS obj = new BL_HSEC_ANUNCIOS();
        dtResultado = obj.uspSEL_HSEC_ANUNCIOS_POR_ID(pk);
        if (dtResultado.Rows.Count > 0)
        {
          
            hdcodigo.Value  = dtResultado.Rows[0]["IDE_ANUNCIO"].ToString();
            txtinicio.Text = dtResultado.Rows[0]["FECHA_INICIO"].ToString();
            txtfin.Text = dtResultado.Rows[0]["FECHA_FIN"].ToString();
            txttitulo.Text = dtResultado.Rows[0]["ARCHIVO_NOMBRE"].ToString();
            ddlTipo.SelectedValue = dtResultado.Rows[0]["TIPO_ANUNCIO"].ToString();
            txturl.Text = dtResultado.Rows[0]["URL"].ToString();
            ddlVisible.SelectedValue = dtResultado.Rows[0]["VISIBLE"].ToString();
        }
    }
    protected void listar()
    {

        DataTable dtResultado = new DataTable();
        BL_HSEC_ANUNCIOS obj = new BL_HSEC_ANUNCIOS();
        dtResultado = obj.uspSEL_HSEC_ANUNCIOS_POR_TIPO(ddlTipo2.SelectedValue,"");
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

    protected void ddlTipo2_SelectedIndexChanged(object sender, EventArgs e)
    {
        listar();
    }
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension.ToUpper())
        {
            case ".PNG":
            case ".JPG":
            case ".JPEG":
            case ".GIF":
            case ".GIFS":
                //case ".Xlsx":
                //case ".XLSX":
                //case ".xlsx":
                return true;
            default:
                return false;
        }
    }
    private static System.Drawing.Image RedimensionarImagen(Stream stream, string tamanio)
    {
        // Se crea un objeto Image, que contiene las propiedades de la imagen
        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

        // Tamaño máximo de la imagen (altura o anchura)
        int max = Convert.ToInt32(tamanio);

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
}