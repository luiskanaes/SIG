using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using Portal;
using BusinessEntity;
using BusinessLogic;
using UserCode;
using System.Data;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BL_Session.ClearSession();
        //Banner();
        //Response.Redirect("~/Account/Login.aspx");
        Response.Redirect("~/Intranet/Login.aspx");
      
    }
    protected void Banner()
    {
        BL_INTRANET obj = new BL_INTRANET();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SP_LISTAR_BANNER(4,"1");

        this.Rgallary.DataSource = dtResultado;
        this.Rgallary.DataBind();
    }
    private BE_TBUSUARIO f_CapturarDatos()
    {

        BE_TBUSUARIO Usuario = new BE_TBUSUARIO();
        Usuario.IDE_USUARIO = Email.Text.Trim();
        Usuario.DES_PASSWORD = Password.Text.Trim();
        return Usuario;
    }
    protected void LogIn(object sender, EventArgs e)
    {
        string pMesajeResp = string.Empty;
        BE_Usuario oBE_Usuario = new BE_Usuario(Email.Text.Trim(), Password.Text.Trim());
        BE_Usuario oBE_Usuario_R = new BE_Usuario();
        BE_Usuario objUsuario_R = new BE_Usuario();

        BL_Seguridad obj_Usuario = new BL_Seguridad();
        DataTable dt = new DataTable();

        oBE_Usuario_R = new BL_Seguridad().f_LogeoUsuario_B(oBE_Usuario, ref pMesajeResp);
        if (string.IsNullOrEmpty(oBE_Usuario_R.f_Usuario_E))
        {
            objUsuario_R = new BL_Seguridad().f_LogeoUsuarioSinRol_B(oBE_Usuario, ref pMesajeResp);
            if (string.IsNullOrEmpty(objUsuario_R.f_Usuario_E))
            {
                //UC_MessageBox.Show(Page, this.GetType(), pMesajeResp);

                string cleanMessage = pMesajeResp;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
            else
            {
                Session["IDE_USUARIO"] = objUsuario_R.f_Usuario_E.ToString();
                //string url = objUsuario_R.f_UrlDefault_E;
                string url = "~/main.aspx";
                Response.Redirect(url);

            }
        }
        else 
        {
            //string url = oBE_Usuario_R.f_UrlDefault_E;
            string url = "~/main.aspx";
            Response.Redirect(url);

        }

    }
}