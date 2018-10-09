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
    public class DA_RRHH_DESEMPENIO_OBJETIVOS_MSG
    {
        Util oUtilitarios = new Util();
        public int uspINS_RRHH_DESEMPENIO_OBJETIVOS_MSG(BE_RRHH_DESEMPENIO_OBJETIVOS_MSG oBE)
        {
            object[] Parametros = new[] {
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_MSG ,tgSQLFieldType.NUMERIC ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_USER ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_OBJETIVO ,tgSQLFieldType.NUMERIC ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.NUMERIC),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ASUNTO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.COMENTARIO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FILE ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.URL ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_DESEMPENIO_OBJETIVOS_MSG", Parametros));
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG_POR_ID(int IDE_OBJETIVO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG_POR_ID", IDE_OBJETIVO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG(int IDE_OBJETIVO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG", IDE_OBJETIVO);
        }
        public DataTable uspUPD_RRHH_DESEMPENIO_AMPLIACION(int IDE_OBJETIVO, string COMENTARIOS, string USER_REGISTRO, string FECHA_AMPLIACION)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_RRHH_DESEMPENIO_AMPLIACION", IDE_OBJETIVO, COMENTARIOS, USER_REGISTRO, FECHA_AMPLIACION);
        }
    }
}
