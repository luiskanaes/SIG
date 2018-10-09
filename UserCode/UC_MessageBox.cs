using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;

namespace UserCode
{
   public class UC_MessageBox
   {
       public static string[] sTipoMensaje = {string.Empty,
                                            "Registro guardado correctamente",
											"Registro modificado correctamente",
											"Registro eliminado correctamente",
											"Registro anulado correctamente",
											"Registro activado correctamente",
											"Ocurrio un error en la operación"};
       private static string smensajeError = string.Empty;
       private static int imensaje = 0;
       public static String f_MsgErrorDefault
       {
           get { return "Error en la Transacción"; }
       }
       public static int f_TipoMensaje
       {
           get { return imensaje; }
           set { imensaje = value; }
       }
       public static String f_MsgError
       {
           get { return smensajeError == String.Empty ? "Error en la Transacción" : smensajeError; }
           set { smensajeError = value; }
       }

       public static void Show(System.Web.UI.Page pagina, System.Type tipo, int imensaje)
       {
           ScriptManager.RegisterStartupScript(pagina, tipo, "Mensaje", "alert('" + sTipoMensaje[imensaje] + "');", true);
       }
       public static void Show(System.Web.UI.Page pagina, System.Type tipo, String mensaje)
       {
           ScriptManager.RegisterStartupScript(pagina, tipo, "Mensaje", "alert('" + mensaje.Replace("'", "\"") + "');", true);
       }

       public static void ShowPup(System.Web.UI.Page pagina, System.Type tipo, String mensaje)
       {

           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           sb.Append(@"<script language='javascript'>");
           sb.Append(@"var lbl = document.getElementById('lblDisplayDate');");
           sb.Append(@"lbl.style.color='red';");
           sb.Append(@"</script>");


           ScriptManager.RegisterStartupScript(pagina, tipo, "JSScript", "alert('" + mensaje.Replace("'", "\"") + "');", true);
           //ScriptManager.RegisterStartupScript(pagina, tipo, "key", "alert('" + mensaje.Replace("'", "\"") + "');", true);
       }

       public static void Script(System.Web.UI.Page pagina, System.Type tipo, String script)
       {
           ScriptManager.RegisterStartupScript(pagina, tipo, "Script", script, true);
       }
       public static void Script(System.Web.UI.Page pagina, System.Type tipo, String script, String snombre)
       {
           ScriptManager.RegisterStartupScript(pagina, tipo, snombre, script, true);
       }
       public static void Script(System.Web.UI.Page pagina, System.Type tipo, String script, String snombre, Boolean estado)
       {
           ScriptManager.RegisterStartupScript(pagina, tipo, snombre, script, estado);
       }
    //ScriptManager.RegisterStartupScript(pagina, tipo, "key", "alert('" + mensaje.Replace("'", "\"") + "');", true);
       
   }
}
