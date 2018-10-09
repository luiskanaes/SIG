using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Text;

public class Helper : Page
{
    public static void MostrarAlerta(string alerta, Page pagina)
    {

        alerta = HttpUtility.UrlEncode(alerta).Replace("\n", "<br/>"); alerta = alerta.Replace('(', ' '); alerta = alerta.Replace(')', ' '); alerta = alerta.Replace('"', ' ');

        StringBuilder strinng = new StringBuilder();
        strinng.AppendFormat("alert(\"{0}\");", alerta.Trim());
        strinng.Replace("'", " ");

        ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), "Scripts", strinng.ToString(), true);
    }

    private static String[] arEvento = new String[] { "OnClick", "OnDblClick", "OnMouseUp", "OnMouseDown", "OnContextMenu", "OnMouseout", "OnMouseover" };

    
    public enum Evento
    {
        OnClick = 0,
        OnDblClick = 1,
        OnMouseUp = 2,
        OnMouseDown = 3,
        OnContextMenu = 4,
        OnMouseout = 5,
        OnMouseover = 6
    }
    
    public static void MostrarAlertaCorrecto(string alerta, Page pagina)
    {
        alerta = HttpUtility.UrlEncode(alerta).Replace("\n", "<br>"); alerta = alerta.Replace('(', ' '); alerta = alerta.Replace(')', ' '); alerta = alerta.Replace('"', ' ');

        StringBuilder strinng = new StringBuilder();
        strinng.AppendFormat("correcto(\"{0}\");", alerta.Trim());
        strinng.Replace("'", " ");

        ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), "Scripts", strinng.ToString(), true);
    }

    public void EjecutarJavaScript(string key, string javaScript)
    {


        ClientScriptManager script = Page.ClientScript;

        if (!script.IsClientScriptBlockRegistered(this.GetType(), key))
        {
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), key, javaScript, true);
        }
    }
    public class LoginUsuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}