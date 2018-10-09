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
    public class DA_LOG_SOLPED_DETALLE
    {
        Util oUtilitarios = new Util();
        public int INS_LOG_SOLPED_DETALLE(BE_LOG_SOLPED_DETALLE oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_PEDIDO ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.MATERIAL ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CANTIDAD ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.COD_PEDIDO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_SOLICITUD ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.PEP,tgSQLFieldType.TEXT),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_LOG_SOLPED_DETALLE", Parametros));
        }
        public DataTable uspDEL_LOG_SOLPED_DETALLE_POR_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspDEL_LOG_SOLPED_DETALLE_POR_ID", id);
        }
        public DataTable uspDELETE_LOG_SOLPED_DETALLE_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspDELETE_LOG_SOLPED_DETALLE_ID", id);
        }
    }
}
