<%@ WebHandler Language="C#" Class="HandlerLogo" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

public class HandlerLogo : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        SqlConnection Con = new SqlConnection();
        Con.ConnectionString = ConfigurationManager
                .ConnectionStrings["Conexion"].ConnectionString;
        //Con.Open();

        string image = context.Request.QueryString["ID"].ToString();
        SqlCommand com = new SqlCommand("Select TOP 1 LOGO_PARTIDO from TB_CANDIDATO where ID_CANDIDATO = '" + image + "'", Con);
        Con.Open();

        SqlDataReader Reader = com.ExecuteReader();
        while (Reader.Read())
        {
            if (Reader.GetValue(0) != System.DBNull.Value)
            {
                context.Response.BinaryWrite((byte[])Reader["LOGO_PARTIDO"]);
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