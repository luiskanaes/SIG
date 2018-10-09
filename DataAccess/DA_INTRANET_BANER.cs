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
   public  class DA_INTRANET_BANER
    {
        Util oUtilitarios = new Util();
        public int uspINS_INTRANET_BANER(BE_INTRANET_BANER oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_BANNER ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DESCRIPCION ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IMG_URL ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IMG_ZOOM ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.RUTA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.URL ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.FLG_ESTADO  ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.ORDEN   ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO   ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO_DESCRIPCION   ,tgSQLFieldType.TEXT ),
                                          (object)UC_FormWeb.mSQLFieldOrNull(oBE.DESCRIPCION_ADICIONAL   ,tgSQLFieldType.TEXT ),



            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_INTRANET_BANER", Parametros));
        }
    }
}
