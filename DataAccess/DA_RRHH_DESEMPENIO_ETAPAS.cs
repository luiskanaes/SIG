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
   public class DA_RRHH_DESEMPENIO_ETAPAS
    {
        Util oUtilitarios = new Util();
        public int uspINS_RRHH_DESEMPENIO_ETAPAS(BE_RRHH_DESEMPENIO_ETAPAS oBE)
        {
            object[] Parametros = new[] {
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.NUMERIC ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRO ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_DESEMPENIO_ETAPAS", Parametros));
        }
        public int uspINS_RRHH_DESEMPENIO_ETAPAS_ID(BE_RRHH_DESEMPENIO_ETAPAS oBE)
        {
            object[] Parametros = new[] {
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.NUMERIC ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRO ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO_ETAPA ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.CECOS ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_DESEMPENIO_ETAPAS_ID", Parametros));
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_ETAPAS_POR_ANIO(int anio,string CECOS)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_ETAPAS_POR_ANIO", anio, CECOS);
        }
        public DataTable uspUPD_RRHH_DESEMPENIO_ETAPAS(int IDE_ETAPAS, string INICIO, string FIN, int FLG_ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_RRHH_DESEMPENIO_ETAPAS", IDE_ETAPAS, INICIO, FIN, FLG_ESTADO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_ETAPA_PERSONA(int anio, string dni,string ip_centro)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_ETAPA_PERSONA", anio,dni, ip_centro);
        }
       
    }
}
