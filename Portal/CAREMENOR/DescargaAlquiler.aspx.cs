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
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;

public partial class CAREMENOR_DescargaAlquiler : System.Web.UI.Page
{
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    byte[] imageBuffer;
    string Nombre;
    string extension;
    protected void Page_Load(object sender, EventArgs e)
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();

        BL_TBL_RequerimientoSubDetalle Obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = Obj.USP_SEL_TBL_REQUERIMIENTO_ADJUNTO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        if (dtResultado.Rows.Count > 0)
        {
            if (dtResultado.Rows[0]["Reqa_CotiArchivo"] != DBNull.Value)
            {
                imageBuffer = (byte[])dtResultado.Rows[0]["Reqa_CotiArchivo"];
                Nombre = DateTime.UtcNow.ToFileTimeUtc().ToString() + "."+ dtResultado.Rows[0]["Reqa_CotiExtension"].ToString();
                extension = dtResultado.Rows[0]["Reqa_CotiExtension"].ToString();
               
                if (imageBuffer != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                    //ImgFoto.ImageUrl = "~/HandlerAlquilerSolicitud.ashx?ID=" + Nombre; // dtResultado.Rows[0]["icodpersonal"]; 
                }

            }
        }

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", Nombre));

        switch (Path.GetExtension(Nombre).ToLower())
        {
            case ".jpg":
                Response.ContentType = "image/jpg";
                break; // TODO: might not be correct. Was : Exit Select

               
            case ".gif":
                Response.ContentType = "image/gif";
                break; // TODO: might not be correct. Was : Exit Select

            
            case ".png":
                Response.ContentType = "image/png";
                break; // TODO: might not be correct. Was : Exit Select

              
            case ".doc":
                Response.ContentType = "application/msword";
                break; // TODO: might not be correct. Was : Exit Select

           
            case ".docx":
                Response.ContentType = "application/msword";
                break; // TODO: might not be correct. Was : Exit Select

            case ".xls":
                Response.ContentType = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                break; // TODO: might not be correct. Was : Exit Select

            case ".xlsx":
                Response.ContentType = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                break; // TODO: might not be correct. Was : Exit Select

            case ".pdf":
                Response.ContentType = "Archivos PDF  (*.pdf)|*.pdf";
                break; // TODO: might not be correct. Was : Exit Select

            case "xls":
                Response.ContentType = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                break; // TODO: might not be correct. Was : Exit Select
        }

        Response.BinaryWrite(imageBuffer);
        Response.End();
    }
}