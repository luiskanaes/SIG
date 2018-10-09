using BusinessEntity;
using DataAccess.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCode;

namespace DataAccess
{
    
    public class DA_TBSOLICITUD_PERMISOS
    {
        Util oUtilitarios = new Util();
        public int MANT_TBSOLICITUD_PERMISOS_INSERT_DATOS(BE_TBSOLICITUD_PERMISOS obj)
        {
            object[] Parametro = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Ide_permiso,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Ide_usuario,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Ide_motivo,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Inicio,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Fin,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Comentario,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FILE,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.URL,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.NOMBRE_DIA,tgSQLFieldType.TEXT),

                                        };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.SP_TBSOLICITUD_PERMISOS_INSERT_DATOS", Parametro));
        }

        public DataTable MANT_TBSOLICITUD_PERMISOS_SELECT_DATOS(string ide_usuario,string anio)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_TBSOLICITUD_PERMISOS_SELECT_DATOS",ide_usuario, anio);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_SELECT_DATOS_ID(int IDE_PERMISO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_TBSOLICITUD_PERMISOS_SELECT_DATOS_ID", IDE_PERMISO);
        }
        public DataTable MANT_TBSOLICITUD_PERMISOS_DELETE_DATOS(int permiso)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_DELETE_DATOS", permiso);
        }
        public DataTable correo_solicitud(int ID, string Usuario)
        {
            return oUtilitarios.EjecutaDatatable("SP_EnviarCorreo_Permiso", ID, Usuario);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_TODOS(string anio, string estado, string ccentro)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_TODOS", anio, estado , ccentro);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS(int id, string codigo, string sustento_rechazo, string usuario)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS", id, codigo, sustento_rechazo, usuario);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_MES(int anio, int mes, string fecha, string ccentro)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_MES", anio, mes, fecha, ccentro);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_MES_DETALLE(string fecha, string ccentro)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_MES_DETALLE", fecha, ccentro);
        }
        public DataTable SP_TBSOLICITUD_VER_PERMISOS_MES(int anio, int mes, string estado, string motivo, string ccentro)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_VER_PERMISOS_MES", anio, mes, estado, motivo, ccentro);
        }
        public DataTable SP_TBSOLICITUD_PERMISOS_MES_TOOLTIP(string fecha, string ccentro)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_MES_TOOLTIP", fecha, ccentro);
        }
        public DataTable SP_EnviarCorreo_PermisoRHH(int ID,string CORREO)
        {
            return oUtilitarios.EjecutaDatatable("SP_EnviarCorreo_PermisoRHH", ID, CORREO);
        }
    }
    
    
}
