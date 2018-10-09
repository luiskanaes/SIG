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

public partial class OPERACIONES_FIRMA : System.Web.UI.Page
{
    string FolderFirmas = ConfigurationManager.AppSettings["FolderFirmas"];
    string FolderFotos = ConfigurationManager.AppSettings["FolderFotos"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnGuardar);
        if (!Page.IsPostBack)
        {
            SelecionarPerfil();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    public void SelecionarPerfil()
    {
        BL_RRHH_PERSONAL_EMPRESA obj = new BL_RRHH_PERSONAL_EMPRESA();
        DataTable dt = new DataTable();
        string dni = BL_Session.Usuario;
        dt = obj.uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID(dni);
        if (dt.Rows.Count > 0)
        {
            lblNombre.Text = dt.Rows[0]["NOMBRE_COMPLETO"].ToString();
            lblCodigo.Text = dt.Rows[0]["ID_DNI"].ToString();
            lblCcentro.Text = dt.Rows[0]["CENTRO_COSTO"].ToString();
            string url = Server.MapPath(FolderFirmas + dt.Rows[0]["FIRMA"].ToString());
            lblfirma.Text = dt.Rows[0]["FIRMA"].ToString();
           

            string firma = dt.Rows[0]["FIRMA"].ToString();
            if (firma == string.Empty)
            {
                ImgFirma.Visible = false;
            }
            else
            {
                ImgFirma.Visible = true ;
                ImgFirma.ImageUrl = FolderFirmas + dt.Rows[0]["FIRMA"].ToString();
            }

            string foto = dt.Rows[0]["FOTO"].ToString();
            if (foto == string.Empty )
            {
                imgFoto.ImageUrl = "~/imagenes/Foto_Fondo.png";
            }
            else
            {
                imgFoto.ImageUrl = FolderFotos + dt.Rows[0]["FOTO"].ToString();
            }

            //if (dt.Rows[0]["FIRMA_BINARY"] != DBNull.Value)
            //{
            //    byte[] imageBuffer = (byte[])dt.Rows[0]["FIRMA_BINARY"];
            //    if (imageBuffer != null)
            //    {
            //        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
            //        imgprueba.ImageUrl = "~/HandlerFoto.ashx?ID=" + dt.Rows[0]["ID_DNI"].ToString();  // dtResultado.Rows[0]["icodpersonal"]; 
            //    }

            //}

        }
        //imgFoto.ImageUrl = "~/imagenes/Foto_Fondo.png";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        //SelecionarPerfil();
        string nomImagen = string.Empty;
        string nomFoto = string.Empty;
        string cleanMessage = string.Empty;
        Byte[] bytesFirma = null;
        Byte[] bytesFoto = null;
        int errorFoto = 0;
        int errorFirma = 0;
        if (fudFirma.HasFile)
        {
            Stream fs = fudFirma.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            bytesFirma = br.ReadBytes((Int32)fs.Length);

            string sExt = string.Empty;
            sExt = Path.GetExtension(fudFirma.PostedFile.FileName);
            if (ValidaExtension(sExt))
            {
                errorFirma = 0;
                string nomFir = (string.IsNullOrEmpty(lblfirma.Text) ? "" : lblfirma.Text);
                EliminarFirma(nomFir);
                nomImagen = DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(fudFirma.PostedFile.FileName);
                System.Drawing.Image img1 = RedimensionarImagen(fudFirma.PostedFile.InputStream, "300");
                img1.Save(Server.MapPath(FolderFirmas) + nomImagen);
            }
            else
            {
                errorFirma++;
            }

        }
        

        if ( FileFoto.HasFile)
        {
            Stream fs1 = FileFoto.PostedFile.InputStream;
            BinaryReader br1 = new BinaryReader(fs1);
            bytesFirma = br1.ReadBytes((Int32)fs1.Length);

            string sExt = string.Empty;
            sExt = Path.GetExtension(FileFoto.PostedFile.FileName);
            if (ValidaExtension(sExt))
            {
                errorFoto = 0;
                string _Foto = (string.IsNullOrEmpty(lblfoto.Text) ? "" : lblfoto.Text);
                EliminarFirma(_Foto);
                nomFoto = DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileFoto.PostedFile.FileName);
                System.Drawing.Image img2o = RedimensionarImagen(FileFoto.PostedFile.InputStream, "200");
            }
            else
            {
                errorFoto++;
            }

            

        }

        if (errorFirma > 1)
        {
            cleanMessage = "No se Permite este tipo de formato para las firmas";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if(errorFoto > 1)
        {
            cleanMessage = "No se Permite este tipo de formato para las foto";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            //////////////////////////////////////////////

            BL_RRHH_PERSONAL_EMPRESA obj = new BL_RRHH_PERSONAL_EMPRESA();
            obj.uspUPD_RRHH_PERSONAL_FOTOS(Session["IDE_USUARIO"].ToString(), nomImagen, nomFoto, bytesFirma, bytesFoto);


            cleanMessage = "Actualización satisfactoria";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            SelecionarPerfil();
        }
       

      
    }
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension)
        {
            case ".JPG":
            case ".PNG":
            case ".JPEG":
            case ".GIF":
            case ".BMP":
            case ".jpg":
            case ".png":
            case ".jpeg":
            case ".gif":
            case ".bmp":
                return true;
            default:
                return false;
        }
    }
    public void EliminarFirma(string nomImagen)
    {
        try
        {
            File.Delete(Server.MapPath(FolderFirmas + nomImagen));
            File.Delete(Server.MapPath(FolderFotos + nomImagen));
        }
        catch (Exception ex)
        {

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