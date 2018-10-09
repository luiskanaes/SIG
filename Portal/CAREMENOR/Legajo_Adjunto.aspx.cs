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
public partial class CAREMENOR_Legajo_Adjunto : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnCargar);
        if (!Page.IsPostBack)
        {


            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            datos();
            file();
        }
    }
    protected void cargar()
    {
        string cleanMessage = string.Empty;

        if (txtFechaLegajo.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de legajo";
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
            string Name = string.Empty;
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
                    string ruta = Server.MapPath(FolderAlquiler);

                    // Si el directorio no existe, crearlo
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.PostedFile.FileName);
                    Name = FileUpload1.PostedFile.FileName;
                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = BL_Session.CENTRO_COSTO + "LEG_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = BL_Session.CENTRO_COSTO + "LEG_" + Path.GetFileName(FileUpload1.FileName);
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
            ////////////////////////////////
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();


            BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
            oBESol.Requ_Numero = Requ_Numero;
            oBESol.Reqd_CodLinea = Reqd_CodLinea;
            oBESol.Reqs_Correlativo = Reqs_Correlativo;
            oBESol.D_DOCUMENTO_TIPO =0;
            oBESol.D_DOCUMENTO_RUTA = FolderAlquiler;
            oBESol.D_DOCUMENTO_FILE = fileArchivo;
            oBESol.D_DOC_MOVILIZACION =0;
            oBESol.D_DOCUMENTO_FECHA = txtFechaLegajo.Text.Trim();
            oBESol.D_Prov_RUC = "";
            oBESol.D_ATENCION_TIPO = 0;
            oBESol.TIPO_OPERACION = 1;
            oBESol.D_DOCUMENTO_FILENAME = Name;
            oBESol.TIPO_FILE = "";
            oBESol.D_ATENCION_COMENTARIOS = "";
            oBESol.D_FLG_OPERARIO = "";
            oBESol.COD_GUID = string.Empty;
            oBESol.D_FECHA_SALE_OBRA ="";
            DataTable dtrpta = new DataTable();
            dtrpta = new BL_TBL_RequerimientoSubDetalle().uspUPD_TBL_RequerimientoSubDetalle_Alquiler(oBESol);
           
            if (dtrpta.Rows.Count > 0)
            {
                //if (dtrpta.Rows[0]["PROCESO"].ToString() == "1")
                //{
                //    datos();
                //}
                //else
                //{

                //    cleanMessage = "Registro existo!";
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                //}

                datos();
                file();
                cleanMessage = "Registro existo!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
        }
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        cargar();
    }
    protected void Eliminar(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderAlquiler);
       

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_FILE = GridView1.DataKeys[grdrow.RowIndex].Values["ide_LegajoFile"].ToString();
        string Archivo = GridView1.DataKeys[grdrow.RowIndex].Values["FILE_ARCHIVO"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        try
        {
            if (File.Exists(path + Archivo))
            {
                File.Delete(path + Archivo);

            }
        }
        catch (Exception ex)
        {

        }

        //dtResultado = obj.uspDEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Convert.ToInt32(IDE_FILE));

        file();
    }
    protected void file()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

       
        dtResultado = obj.uspSEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
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

    protected void datos()
    {
        
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


                txtFechaLegajo.Text = dtResultado.Rows[0]["D_FECHA_ENVIO_LOG"].ToString();
                
                if (dtResultado.Rows[0]["PROCESO_BOTON"].ToString() == "1")
                {
                    btnCargar.Visible = true;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                        btnEliminar.Visible = true ;
                    }
                }
                else
                {
                    btnCargar.Visible = false;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                        btnEliminar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}