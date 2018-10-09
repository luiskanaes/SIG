using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Web.SessionState;
using System.Collections.Generic;
using BusinessEntity;
namespace BusinessLogic
{
    public class BL_Session
    {
        static public void ClearSession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }
        static public string Usuario
        {
            get
            {
                return HttpContext.Current.Session["IDE_USUARIO"] != null ? HttpContext.Current.Session["IDE_USUARIO"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["IDE_USUARIO"] = value.Trim();
            }
        }
        
        static public string NombreCargo
        {
            get
            {
                return HttpContext.Current.Session["DES_USUARIO"] != null ? HttpContext.Current.Session["DES_USUARIO"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["DES_USUARIO"] = value.Trim();
            }
        }
        static public int Perfil
        {
            get
            {
                return int.Parse(HttpContext.Current.Session["IdPerfil"] == null ? "0" : (HttpContext.Current.Session["IdPerfil"].ToString()));
            }
            set
            {
                HttpContext.Current.Session["IdPerfil"] = value;
            }
        }
        static public string PerfilNombre
        {
            get
            {
                return HttpContext.Current.Session["Descripcion"] != null ? HttpContext.Current.Session["Descripcion"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["Descripcion"] = value.Trim();
            }
        }
        static public string UsuarioNombre
        {
            get
            {
                return HttpContext.Current.Session["NOMBRE_USUARIO"] != null ? HttpContext.Current.Session["NOMBRE_USUARIO"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["NOMBRE_USUARIO"] = value.Trim();
            }
        }
        static public string Controles
        {
            get
            {
                return HttpContext.Current.Session["CONTROL"] != null ? HttpContext.Current.Session["CONTROL"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["CONTROL"] = value.Trim();
            }
        }
        static public string CENTRO_COSTO
        {
            get
            {
                return HttpContext.Current.Session["CENTRO_COSTO"] != null ? HttpContext.Current.Session["CENTRO_COSTO"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["CENTRO_COSTO"] = value.Trim();
            }
        }
        static public string PROYECTO
        {
            get
            {
                return HttpContext.Current.Session["PROYECTO"] != null ? HttpContext.Current.Session["PROYECTO"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["PROYECTO"] = value.Trim();
            }
        }
        static public string IP_CENTRO
        {
            get
            {
                return HttpContext.Current.Session["IP_CENTRO"] != null ? HttpContext.Current.Session["IP_CENTRO"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["IP_CENTRO"] = value.Trim();
            }
        }

        static public string ID_EMPRESA
        {
            get
            {
                return HttpContext.Current.Session["ID_EMPRESA"] != null ? HttpContext.Current.Session["ID_EMPRESA"].ToString() : string.Empty;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Session["ID_EMPRESA"] = value.Trim();
            }
        }

        static public int FLG_COMUNICADO
        {
            get
            {
                return int.Parse(HttpContext.Current.Session["FLG_COMUNICADO"] == null ? "0" : (HttpContext.Current.Session["FLG_COMUNICADO"].ToString()));
            }
            set
            {
                HttpContext.Current.Session["FLG_COMUNICADO"] = value;
            }
        }
    }
}
