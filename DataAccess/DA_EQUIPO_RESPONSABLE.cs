using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;
using System.Data;

namespace DataAccess
{
   public class DA_EQUIPO_RESPONSABLE
    {
        Util oUtilitarios = new Util();
        public int uspINS_EQUIPO_RESPONSABLE(BE_EQUIPO_RESPONSABLE oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_RESPONSABLE ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_RESPONSABLE ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IP_CENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CENTRO ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FLG_ESTADO ,tgSQLFieldType.NUMERIC ),
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_EQUIPO_RESPONSABLE", Parametros));
        }
        public DataTable uspSEL_EQUIPO_RESPONSABLE_GERENCIA(string GERENCIA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_RESPONSABLE_GERENCIA", GERENCIA);
        }
        public DataTable uspUPD_EQUIPO_RESPONSABLE_ESTADO(int IDE_RESPONSABLE, int FLG_ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_EQUIPO_RESPONSABLE_ESTADO", IDE_RESPONSABLE,FLG_ESTADO);
        }
        public DataTable uspSEL_EQUIPO_RESPONSABLE_POR_ID(int IDE_RESPONSABLE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_RESPONSABLE_POR_ID", IDE_RESPONSABLE);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_ACTIVO(string DNI_TRABAJADOR, int FLG_ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_TRABAJO_ACTIVO", DNI_TRABAJADOR, FLG_ESTADO);
        }
        public DataTable uspSEL_EQUIPO_GERENCIA(string GERENCIA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_GERENCIA", GERENCIA);
        }

        public DataTable uspUPD_LIBERAR_EQUIPO_RESPONSABLE(int IDE_RESPONSABLE, string DNI_RESPONSABLE)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_LIBERAR_EQUIPO_RESPONSABLE", IDE_RESPONSABLE, DNI_RESPONSABLE);
        }
    }
}
