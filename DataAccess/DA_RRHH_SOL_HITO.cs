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
    public class DA_RRHH_SOL_HITO
    {
        Util oUtilitarios = new Util();
        public int INS_RRHH_SOL_HITO(BE_RRHH_SOL_HITO obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_HITOS,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FORMATIVO,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.DESCRIPCION,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FECHA_HITO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.TIPO,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.HOLDER,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.ROL,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.INTERACCION,tgSQLFieldType.TEXT),

            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_SOL_HITO", Parametros));

        }
        public DataTable uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO(int IDE_FORMATIVO, int TIPO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO", IDE_FORMATIVO, TIPO);
        }
        public DataTable uspDEL_RRHH_SOL_HITO_ID(int IDE_HITOS)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspDEL_RRHH_SOL_HITO_ID", IDE_HITOS);
        }
    }
}
