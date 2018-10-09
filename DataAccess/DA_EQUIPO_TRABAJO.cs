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
   
    public class DA_EQUIPO_TRABAJO
    {
        Util oUtilitarios = new Util();
        public int uspINS_EQUIPO_TRABAJO(BE_EQUIPO_TRABAJO oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_EQUIPO ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FLG_ESTADO  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_RESPONSABLE ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_TRABAJADOR ,tgSQLFieldType.NUMERIC ),
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_EQUIPO_TRABAJO", Parametros));
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_SUPERVISOR(int IDE_RESPONSABLE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_TRABAJO_SUPERVISOR", IDE_RESPONSABLE);
        }
        public DataTable uspUPD_EQUIPO_TRABAJADOR(int IDE_EQUIPO, int FLG_ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_EQUIPO_TRABAJADOR", IDE_EQUIPO,FLG_ESTADO);
        }
        public DataTable uspINS_EQUIPO_TRABAJO_VARIOS(int IDE_RESPONSABLE, string CENTRO_COSTO, string USER_REGISTRA)
        {
            return oUtilitarios.EjecutaDatatable("uspINS_EQUIPO_TRABAJO_VARIOS", IDE_RESPONSABLE, CENTRO_COSTO, USER_REGISTRA);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_SUPERVISOR_DNI(string DNI_RESPONSABLE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_TRABAJO_SUPERVISOR_DNI", DNI_RESPONSABLE);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_DNI(string DNI_TRABAJADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_TRABAJO_DNI", DNI_TRABAJADOR);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_LIBRE(string DNI_TRABAJADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_EQUIPO_TRABAJO_LIBRE", DNI_TRABAJADOR);
        }
    }
}
