using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using UserCode;
using System.Web;

namespace BusinessLogic
{
    public class BL_Seguridad
    {
        public BE_Usuario f_LogeoUsuario_B(BE_Usuario oBE_Usuario, ref string pMesajeResp, string ShowAplicacion = "WEB")
        {
            BE_Usuario oBE_Usuario_New = new BE_Usuario();
            try
            {

                if (string.IsNullOrEmpty(oBE_Usuario.f_Password_E) || string.IsNullOrEmpty(oBE_Usuario.f_Usuario_E))
                {
                    pMesajeResp = "El Usuario o Contrase\x00f1a no debe ser vacio";
                    return oBE_Usuario_New;
                }
                oBE_Usuario_New = new DA_Seguridad().f_LogeoUsuario_D(oBE_Usuario);
                if (!string.IsNullOrEmpty(oBE_Usuario_New.f_Usuario_E))
                {
                    if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                    {
                        BL_Session.Usuario = oBE_Usuario_New.f_Usuario_E;
                        BL_Session.NombreCargo = oBE_Usuario_New.f_cargo_E;
                      
                        BL_Session.Perfil = oBE_Usuario_New.oBE_Perfil.f_Perfil_E;
                        BL_Session.Controles  = oBE_Usuario_New.oBE_Perfil.f_Control_E;
                        BL_Session.PerfilNombre = oBE_Usuario_New.oBE_Perfil.f_Vnomperfil_E;
                        BL_Session.UsuarioNombre = oBE_Usuario_New.f_NombreUsuario_E;
                        BL_Session.CENTRO_COSTO = oBE_Usuario_New.f_CENTRO_COSTO_E ;
                        BL_Session.PROYECTO = oBE_Usuario_New.f_PROYECTO_E;
                        BL_Session.IP_CENTRO = oBE_Usuario_New.f_IP_CENTRO_E;
                        BL_Session.ID_EMPRESA = oBE_Usuario_New.f_ID_EMPRESA_E;
                        BL_Session.FLG_COMUNICADO  = oBE_Usuario_New.f_FLG_COMUNICADO_E;
                    }
                    return oBE_Usuario_New;
                }
                pMesajeResp = "Error en Usuario o Contrase\x00f1a.";
                //if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                //{
                //    BL_Session.ClearSession();
                //}
            }
            catch (Exception ex)
            {
                if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                {
                    Logger.Write(ex);
                }
                pMesajeResp = "Error al realizar la consulta con la BD";
            }
            return oBE_Usuario_New;
        }
        public BE_Usuario f_LogeoUsuarioSinRol_B(BE_Usuario oBE_Usuario, ref string pMesajeResp, string ShowAplicacion = "WEB")
        {
            BE_Usuario oBE_Usuario_New = new BE_Usuario();
            try
            {

                if (string.IsNullOrEmpty(oBE_Usuario.f_Password_E) || string.IsNullOrEmpty(oBE_Usuario.f_Usuario_E))
                {
                    pMesajeResp = "El Usuario o Contrase\x00f1a no debe ser vacio";
                    return oBE_Usuario_New;
                }
                oBE_Usuario_New = new DA_Seguridad().f_LogeoUsuarioSinRol_D(oBE_Usuario);
                if (!string.IsNullOrEmpty(oBE_Usuario_New.f_Usuario_E))
                {
                    if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                    {
                        BL_Session.Usuario = oBE_Usuario_New.f_Usuario_E;
                        BL_Session.NombreCargo = oBE_Usuario_New.f_cargo_E;

                        BL_Session.Perfil = oBE_Usuario_New.oBE_Perfil.f_Perfil_E;
                        BL_Session.Controles = oBE_Usuario_New.oBE_Perfil.f_Control_E;
                        BL_Session.PerfilNombre = oBE_Usuario_New.oBE_Perfil.f_Vnomperfil_E;
                        BL_Session.UsuarioNombre = oBE_Usuario_New.f_NombreUsuario_E;
                        BL_Session.CENTRO_COSTO = oBE_Usuario_New.f_CENTRO_COSTO_E;
                        BL_Session.PROYECTO = oBE_Usuario_New.f_PROYECTO_E;
                        BL_Session.IP_CENTRO = oBE_Usuario_New.f_IP_CENTRO_E;
                        BL_Session.ID_EMPRESA = oBE_Usuario_New.f_ID_EMPRESA_E;
                    }
                    return oBE_Usuario_New;
                }
                pMesajeResp = "Error en Usuario o Contrase\x00f1a.";
                if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                {
                    BL_Session.ClearSession();
                }
            }
            catch (Exception ex)
            {
                if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                {
                    Logger.Write(ex);
                }
                pMesajeResp = "Error al realizar la consulta con la BD";
            }
            return oBE_Usuario_New;
        }
        public BE_Usuario f_LogeoUsuarioExterno(BE_Usuario oBE_Usuario, ref string pMesajeResp, string ShowAplicacion = "WEB")
        {
            BE_Usuario oBE_Usuario_New = new BE_Usuario();
            try
            {

                if ( string.IsNullOrEmpty(oBE_Usuario.f_Usuario_E))
                {
                    pMesajeResp = "El Usuario o Contrase\x00f1a no debe ser vacio";
                    return oBE_Usuario_New;
                }
                oBE_Usuario_New = new DA_Seguridad().f_LogeoUsuarioExterno_D(oBE_Usuario);
                if (!string.IsNullOrEmpty(oBE_Usuario_New.f_Usuario_E))
                {
                    if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                    {
                        BL_Session.Usuario = oBE_Usuario_New.f_Usuario_E;
                        BL_Session.NombreCargo = oBE_Usuario_New.f_cargo_E;

                        BL_Session.Perfil = oBE_Usuario_New.oBE_Perfil.f_Perfil_E;
                        BL_Session.Controles = oBE_Usuario_New.oBE_Perfil.f_Control_E;
                        BL_Session.PerfilNombre = oBE_Usuario_New.oBE_Perfil.f_Vnomperfil_E;
                        BL_Session.UsuarioNombre = oBE_Usuario_New.f_NombreUsuario_E;
                        BL_Session.CENTRO_COSTO = oBE_Usuario_New.f_CENTRO_COSTO_E;
                        BL_Session.PROYECTO = oBE_Usuario_New.f_PROYECTO_E;
                        BL_Session.IP_CENTRO = oBE_Usuario_New.f_IP_CENTRO_E;
                        BL_Session.ID_EMPRESA = oBE_Usuario_New.f_ID_EMPRESA_E;
                    }
                    return oBE_Usuario_New;
                }
                pMesajeResp = "Error en Usuario o Contrase\x00f1a.";
                if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                {
                    BL_Session.ClearSession();
                }
            }
            catch (Exception ex)
            {
                if (ShowAplicacion.Equals(UserCode.UC_Constante.Web))
                {
                    Logger.Write(ex);
                }
                pMesajeResp = "Error al realizar la consulta con la BD";
            }
            return oBE_Usuario_New;
        }
        public DataTable DatosUsuario(string Usuario, string password )
        {
            return new DA_Seguridad().DatosUsuario( Usuario, password);
        }
        public DataTable ListarMenu(int idPerfil)
        {
            return new DA_Seguridad().ListarMenu_DA(idPerfil);
        }
        public DataTable UptadePassword(string password, string Usuario)
        {
            return new DA_Seguridad().UptadePasswordDA(password, Usuario);
        }
        public DataTable auditoria_procesos(string proceso, string Usuario,string sustento)
        {
            return new DA_Seguridad().auditoria_procesosDA(proceso, Usuario, sustento);
        }
        public DataTable uspSEL_TBOPCIONES_MENU()
        {
            return new DA_Seguridad().uspSEL_TBOPCIONES_MENU();
        }
        public DataTable SP_OPCIONES_MODULO_PERMISOS(string Usuario, int grupo,string IDE_OPCIONES)
        {
            return new DA_Seguridad().SP_OPCIONES_MODULO_PERMISOS(Usuario, grupo, IDE_OPCIONES);
        }
        public DataTable SP_LISTAR_FELIZ_CUMPLE()
        {
            return new DA_Seguridad().SP_LISTAR_FELIZ_CUMPLE();
        }
    }
}
