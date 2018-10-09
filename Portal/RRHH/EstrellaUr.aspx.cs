using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using UserCode;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
public partial class RRHH_EstrellaUr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCorreo.Text = Request.QueryString["Usuario"];

        Boolean correo = email_bien_escrito(lblCorreo.Text);

        if (correo == true)
        {
            BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_POR_CORREO(lblCorreo.Text);
            if (dtResultado.Rows.Count > 0)
            {
                //Session["IDE_USUARIO"] =

                string pMesajeResp = string.Empty;
                BE_Usuario oBE_Usuario = new BE_Usuario();
                oBE_Usuario.f_Usuario_E = dtResultado.Rows[0]["ID_DNI"].ToString();

                BE_Usuario objUsuario_R = new BE_Usuario();

                BL_Seguridad obj_Usuario = new BL_Seguridad();
                objUsuario_R = new BL_Seguridad().f_LogeoUsuarioExterno(oBE_Usuario, ref pMesajeResp);
                if (string.IsNullOrEmpty(objUsuario_R.f_Usuario_E))
                {
                    UC_MessageBox.Show(Page, this.GetType(), pMesajeResp);
                }
                else
                {
                    Session["IDE_USUARIO"] = objUsuario_R.f_Usuario_E.ToString();
                    //string url = objUsuario_R.f_UrlDefault_E;
                    //Response.Redirect(url);
                    Response.Redirect("~/RRHH/EstrellaSSK.aspx");
                }
            }
            else
            {
                Response.Redirect("~/RRHH/Contacto.aspx");
            }
        }
        else
        {
            string pMesajeResp = string.Empty;
            BE_Usuario oBE_Usuario = new BE_Usuario();
            oBE_Usuario.f_Usuario_E = lblCorreo.Text;

            BE_Usuario objUsuario_R = new BE_Usuario();

            BL_Seguridad obj_Usuario = new BL_Seguridad();
            objUsuario_R = new BL_Seguridad().f_LogeoUsuarioExterno(oBE_Usuario, ref pMesajeResp);
            if (string.IsNullOrEmpty(objUsuario_R.f_Usuario_E))
            {
                UC_MessageBox.Show(Page, this.GetType(), pMesajeResp);
            }
            else
            {
                Session["IDE_USUARIO"] = objUsuario_R.f_Usuario_E.ToString();
                //string url = objUsuario_R.f_UrlDefault_E;
                //Response.Redirect(url);
                Response.Redirect("~/RRHH/EstrellaSSK.aspx");
            }
        }
    }
    private Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}