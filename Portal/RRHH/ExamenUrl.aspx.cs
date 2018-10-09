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
using System.Security.Cryptography;
using System.Text;

public partial class RRHH_ExamenUrl : System.Web.UI.Page
{
    string IDE_FASE, IDE_FICHA, IDE_EXAMEN;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string hash = HashEmailForGravatar(email);

        lblCorreo.Text = Request.QueryString["correo"];
        IDE_FASE = Request.QueryString["IDE_FASE"];
        IDE_FICHA = Request.QueryString["IDE_FICHA"];
        IDE_EXAMEN = Request.QueryString["IDE_EXAMEN"];
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
                    Session["IDE_FASE"] = IDE_FASE;
                    Session["IDE_FICHA"] = IDE_FICHA;
                    Session["IDE_EXAMEN"] = IDE_EXAMEN;
                    
                    Response.Redirect("~/RRHH/FormativoExamen.aspx");
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
                Session["IDE_EXAMEN"] = IDE_EXAMEN;
                Session["IDE_USUARIO"] = objUsuario_R.f_Usuario_E.ToString();
                Session["IDE_FASE"] = IDE_FASE;
                Session["IDE_FICHA"] = IDE_FICHA;
                Response.Redirect("~/RRHH/FormativoExamen.aspx");
            }
        }
    }
    public static string HashEmailForGravatar(string email)
    {
        // Create a new instance of the MD5CryptoServiceProvider object.  
        MD5 md5Hasher = MD5.Create();

        // Convert the input string to a byte array and compute the hash.  
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

        // Create a new Stringbuilder to collect the bytes  
        // and create a string.  
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data  
        // and format each one as a hexadecimal string.  
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();  // Return the hexadecimal string. 
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