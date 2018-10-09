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
    public class DA_RESPONSABLE_PROCESOS
    {
        Util oUtilitarios = new Util();
        public int uspINS_RESPONSABLE_PROCESOS(BE_RESPONSABLE_PROCESOS oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_RESPONSABLE  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_RESPONSABLE   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IP_CENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.GERENCIA ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_PROCESO  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TODOS   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_EMPRESA   ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RESPONSABLE_PROCESOS", Parametros));
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_POR_ID(string  FLG_ESTADO, string NOMBRE, string PROCESO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RESPONSABLE_PROCESOS_POR_ID", FLG_ESTADO, NOMBRE, PROCESO);
        }
        public DataTable uspDEL_RESPONSABLE_PROCESOS_POR_ID(int IDE_RESPONSABLE)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_RESPONSABLE_PROCESOS_POR_ID", IDE_RESPONSABLE);
        }
    }
}
