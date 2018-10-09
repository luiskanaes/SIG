<%@ WebHandler Language="C#" Class="HandlerAlquilerSolicitud" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

public class HandlerAlquilerSolicitud : IHttpHandler {

    public void ProcessRequest (HttpContext context)
    {
        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationManager
                .ConnectionStrings["CareMenor"].ConnectionString;
        //Con.Open();

        string Requ_Numero = context.Request.QueryString["Requ_Numero"].ToString();
        string Reqd_CodLinea = context.Request.QueryString["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = context.Request.QueryString["Reqs_Correlativo"].ToString();
        SqlCommand com = new SqlCommand("SELECT top 1 Reqa_CotiArchivo FROM TBL_RequerimientoSubDetalle_Adjuntos  A where Requ_Numero = '" + Requ_Numero + "' and Reqd_CodLinea ='" + Reqd_CodLinea + "' and Reqs_Correlativo='"  + Reqs_Correlativo + "' and A.Reqa_Estado=1 ", Con);
        Con.Open();


        SqlDataReader Reader = com.ExecuteReader();
        while (Reader.Read())
        {
            if (Reader.GetValue(0) != System.DBNull.Value)
            {
                context.Response.BinaryWrite((byte[])Reader["Reqa_CotiArchivo"]);
            }
        }
        Con.Close();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}