using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;

namespace DataAccess
{
    public class DA_Seguridad
    {
        Util oUtilitarios = new Util();
        public BE_Usuario f_LogeoUsuario_D(BE_Usuario oBE_Usuario)
        {
            try
            {
                BE_Usuario oBE_Usuario_R = new BE_Usuario();
                using (IDataReader dr = oUtilitarios.EjecutaDataReader("dbo.USP_USUARIO_LOGIN", oBE_Usuario.f_Usuario_E, oBE_Usuario.f_Password_E))
                {
                    BE_Perfil oBE_Perfil_R = new BE_Perfil();
                 

                    while (dr.Read())
                    {
                        new UC_Mapeador().ReaderToObject(dr, oBE_Usuario_R);
                        new UC_Mapeador().ReaderToObject(dr, oBE_Perfil_R);

                    }
                    dr.Close();
                    oBE_Usuario_R.oBE_Perfil = oBE_Perfil_R;
                    GC.SuppressFinalize(oBE_Perfil_R);

                }

                return oBE_Usuario_R;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BE_Usuario f_LogeoUsuarioSinRol_D(BE_Usuario oBE_Usuario)
        {
            try
            {
                BE_Usuario oBE_Usuario_R = new BE_Usuario();
                using (IDataReader dr = oUtilitarios.EjecutaDataReader("dbo.USP_USUARIO_LOGEO", oBE_Usuario.f_Usuario_E, oBE_Usuario.f_Password_E))
                {
                    BE_Perfil oBE_Perfil_R = new BE_Perfil();


                    while (dr.Read())
                    {
                        new UC_Mapeador().ReaderToObject(dr, oBE_Usuario_R);
                        new UC_Mapeador().ReaderToObject(dr, oBE_Perfil_R);

                    }
                    dr.Close();
                    oBE_Usuario_R.oBE_Perfil = oBE_Perfil_R;
                    GC.SuppressFinalize(oBE_Perfil_R);

                }

                return oBE_Usuario_R;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BE_Usuario f_LogeoUsuarioExterno_D(BE_Usuario oBE_Usuario)
        {
            try
            {
                BE_Usuario oBE_Usuario_R = new BE_Usuario();
                using (IDataReader dr = oUtilitarios.EjecutaDataReader("dbo.USP_USUARIO_LOGEOEXTERNO", oBE_Usuario.f_Usuario_E))
                {
                    BE_Perfil oBE_Perfil_R = new BE_Perfil();


                    while (dr.Read())
                    {
                        new UC_Mapeador().ReaderToObject(dr, oBE_Usuario_R);
                        new UC_Mapeador().ReaderToObject(dr, oBE_Perfil_R);

                    }
                    dr.Close();
                    oBE_Usuario_R.oBE_Perfil = oBE_Perfil_R;
                    GC.SuppressFinalize(oBE_Perfil_R);

                }

                return oBE_Usuario_R;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListarMenu_DA(int idPerfil)
        {
            return oUtilitarios.EjecutaDatatable("dbo.usp_SeleccionarMenuPrincipal", idPerfil);
        }
        public DataTable UptadePasswordDA(string password, string Usuario)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_UPDATE_PASSWORD", password, Usuario);
        }
        public DataTable auditoria_procesosDA(string proceso, string Usuario, string sustento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REGISTRO_AUDITORIA_PROCESOS", proceso, Usuario, sustento);
        }
        public DataTable DatosUsuario(string Usuario, string password)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_USUARIO_LOGEO", Usuario, password);
        }
        public DataTable uspSEL_TBOPCIONES_MENU()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_TBOPCIONES_MENU");
        }
        public DataTable SP_OPCIONES_MODULO_PERMISOS(string Usuario, int grupo, string  IDE_OPCIONES)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_OPCIONES_MODULO_PERMISOS", Usuario,grupo, IDE_OPCIONES);
        }
        public DataTable SP_LISTAR_FELIZ_CUMPLE()
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_LISTAR_FELIZ_CUMPLE");
        }
    }
}
