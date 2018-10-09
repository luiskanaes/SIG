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

public partial class CAREMENOR_FileFtp : System.Web.UI.Page
{
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string FolderAlquilerBackups = ConfigurationManager.AppSettings["FolderAlquilerBackups"];
    string FolderFTP = ConfigurationManager.AppSettings["FolderFTP"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string ruta = Server.MapPath(FolderAlquiler);
            BL_TBL_RequerimientoSubDetalle objx = new BL_TBL_RequerimientoSubDetalle();
            DataTable dt= new DataTable();
            dt= objx.SP_LISTAR_ARCHIVOS_PDC_TODOS("");
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                ////**********************************************************
                //******** CREAR DIRECTORIO PROYECTO ******************************
                string Proyecto = dt.Rows[j]["PROYECTO"].ToString();
                string PDC = dt.Rows[j]["PDC"].ToString();
                string DIRECTORIO_PDC = dt.Rows[j]["DIRECTORIO"].ToString();
                string rutaOBRA = FolderFTP + Proyecto.Substring(0, 5);



                // Si el directorio no existe, crearlo
                if (!Directory.Exists(rutaOBRA))//directorio OBRA
                    Directory.CreateDirectory(rutaOBRA);

                //DIRECTORIO  SAT
                string rutaSAT = Path.Combine(rutaOBRA, "SAT");
                if (!Directory.Exists(rutaSAT))//directorio final
                    Directory.CreateDirectory(rutaSAT);


                //DIRECTORIO  CODIGO DE PDC
                string rutaPDC_CODIGO = Path.Combine(rutaSAT, DIRECTORIO_PDC);
                if (!Directory.Exists(rutaPDC_CODIGO))//directorio final
                    Directory.CreateDirectory(rutaPDC_CODIGO);

                BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                DataTable dtResultado = new DataTable();
                dtResultado = obj.SP_LISTAR_ARCHIVOS_PDC(PDC);
                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {


                    string adjunto = dtResultado.Rows[i]["ARCHIVO"].ToString();
                    if (File.Exists(Path.Combine(ruta, adjunto)))
                    {
                        File.Copy(Path.Combine(ruta, adjunto), Path.Combine(rutaPDC_CODIGO, adjunto), true);
                    }

                }

            }
            string cleanMessage = "Registro exitoso.";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
}