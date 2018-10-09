using BusinessEntity;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BL_TBSOLICITUD_PERMISOS
    {
        public int MANT_TBSOLICITUD_PERMISOS_INSERT_DATOS(BE_TBSOLICITUD_PERMISOS objSol)
        {
            try
            {
                return new DA_TBSOLICITUD_PERMISOS().MANT_TBSOLICITUD_PERMISOS_INSERT_DATOS(objSol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable MANT_TBSOLICITUD_PERMISOS_SELECT_DATOS(string ide_usuario, string anio)
        {
            return new DA_TBSOLICITUD_PERMISOS().MANT_TBSOLICITUD_PERMISOS_SELECT_DATOS(ide_usuario, anio);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_SELECT_DATOS_ID(int IDE_PERMISO)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_PERMISOS_SELECT_DATOS_ID(IDE_PERMISO);
        }
        public DataTable MANT_TBSOLICITUD_PERMISOS_DELETE_DATOS(int permiso)
        {
            return new DA_TBSOLICITUD_PERMISOS().MANT_TBSOLICITUD_PERMISOS_DELETE_DATOS(permiso);
        }
        public DataTable correo_solicitud(int ID, string Usuario)
        {
            return new DA_TBSOLICITUD_PERMISOS().correo_solicitud(ID, Usuario);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_TODOS(string anio, string estado, string ccentro)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_PERMISOS_TODOS(anio, estado, ccentro);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS(int id, string codigo, string sustento_rechazo, string usuario)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS(id, codigo, sustento_rechazo, usuario);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_MES(int anio, int mes, string fecha, string ccentro)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_PERMISOS_MES(anio, mes, fecha, ccentro);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_MES_DETALLE(string fecha, string ccentro)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_PERMISOS_MES_DETALLE( fecha, ccentro);
        }

        public DataTable SP_TBSOLICITUD_VER_PERMISOS_MES(int anio, int mes, string estado, string motivo, string ccentro)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_VER_PERMISOS_MES(anio, mes, estado , motivo, ccentro);
        }

        public DataTable SP_TBSOLICITUD_PERMISOS_MES_TOOLTIP(string fecha, string ccentro)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_TBSOLICITUD_PERMISOS_MES_TOOLTIP(fecha, ccentro);
        }
        public DataTable SP_EnviarCorreo_PermisoRHH(int ID, string CORREO)
        {
            return new DA_TBSOLICITUD_PERMISOS().SP_EnviarCorreo_PermisoRHH(ID, CORREO);
        }
    }
}
