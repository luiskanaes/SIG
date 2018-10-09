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
    public class DA_TBSOLICITUD_PERMISOS_DIA
    {
        Util oUtilitarios = new Util();
        public int MANT_TBSOLICITUD_PERMISOS_DIA_INSERT_DATOS(BE_TBSOLICITUD_PERMISOS_DIA obj)
        {
            object[] Parametro = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Ide_detalle,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Ide_permiso,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.Fecha,tgSQLFieldType.TEXT),                                        
                                        };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.SP_TBSOLICITUD_PERMISOS_DIA_INSERT_DATOS", Parametro));
        }

        public DataTable MANT_TBSOLICITUD_PERMISOS_DIA_SELECT_DATOS(string ide_permiso)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_TBSOLICITUD_PERMISOS_DIA_SELECT_DATOS", ide_permiso);
        }

        public DataTable MANT_TBSOLICITUD_PERMISOS_DIA_DELETE_DATOS(string ide_detalle)
        {
            return oUtilitarios.EjecutaDatatable("SP_TBSOLICITUD_PERMISOS_DIA_DELETE_DATOS", ide_detalle);
        }

    }
}
