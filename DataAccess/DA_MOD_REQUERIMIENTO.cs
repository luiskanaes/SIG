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
   public class DA_MOD_REQUERIMIENTO
    {
        Util oUtilitarios = new Util();
        public int uspINS_MOD_REQUERIMIENTO(BE_MOD_REQUERIMIENTO oBE)
        {
            object[] Parametros = new[] {
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_MOD ,tgSQLFieldType.NUMERIC ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_REQUERIMIENTO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IP_CENTRO ,tgSQLFieldType.TEXT),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.CENTRO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRO ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_MOD_REQUERIMIENTO", Parametros));
        }
        public int uspINS_MOD_REQUERIMIENTO_DETALLE(BE_MOD_REQUERIMIENTO_DETALLE oBE)
        {
            object[] Parametros = new[] {
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_REQUERIMIENTO  ,tgSQLFieldType.NUMERIC ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_MOD ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_CATEGORIA ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_ESPECIALIDAD ,tgSQLFieldType.TEXT),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.CANTIDAD ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IP_CENTRO ,tgSQLFieldType.TEXT),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRO ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_MOD_REQUERIMIENTO_DETALLE", Parametros));
        }
        public int uspUPD_MOD_REQUERIMIENTO_PERSONAL(BE_MOD_REQUERIMIENTO_PERSONAL oBE)
        {
            object[] Parametros = new[] {
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.REQ_PERSONAL  ,tgSQLFieldType.NUMERIC ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_RESPONSABLE ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_ETAPAS ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI ,tgSQLFieldType.TEXT),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.APE_PATERNO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.APE_MATERNO ,tgSQLFieldType.TEXT),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.NOMBRES ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_NACIMIENTO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.UBIGEO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.TELEFONOS  ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_MANO_OBRA ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_CONDICION ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_EXA_MEDICO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_CHARLA_TR ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_CHARLA_SSK ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_CHARLA_ALTURA ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_CHARLA_ESP_CONFINADO ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_CHARL_CALIENTE ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_ENTREGA_FILE_TR ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.FEC_ACCESO_PLANTA ,tgSQLFieldType.TEXT ),
                            (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FOTOCHECK  ,tgSQLFieldType.TEXT ),
                             (object)UC_FormWeb.mSQLFieldOrNull(oBE.OBSERVACIONES ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspUPD_MOD_REQUERIMIENTO_PERSONAL", Parametros));
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_DETALLE_IDE_MOD(int IDE_MOD)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_MOD_REQUERIMIENTO_DETALLE_IDE_MOD", IDE_MOD);
        }
        public DataTable uspDEL_MOD_REQUERIMIENTO(int IDE_REQUERIMIENTO, int IDE_MOD)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_MOD_REQUERIMIENTO", IDE_REQUERIMIENTO,IDE_MOD);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_BANDEJA(string TICKET, string  IP_CENTRO, int ANIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_MOD_REQUERIMIENTO_BANDEJA", TICKET, IP_CENTRO, ANIO);
        }
        public DataTable uspDEL_MOD_REQUERIMIENTO_IDE(int IDE_REQUERIMIENTO, int IDE_MOD)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_MOD_REQUERIMIENTO_IDE", IDE_REQUERIMIENTO, IDE_MOD);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_IDE(int IDE_MOD)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_MOD_REQUERIMIENTO_IDE",  IDE_MOD);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_REQ(int IDE_REQUERIMIENTO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_REQ", IDE_REQUERIMIENTO);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_IDE(int REQ_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_IDE", REQ_PERSONAL);
        }
        public DataTable uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(int REQ_PERSONAL, string VALOR, int ORDEN)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO", REQ_PERSONAL, VALOR, ORDEN);
        }

        public DataTable uspUPD_MOD_REQUERIMIENTO_RESPONSABLE(int IDE_MOD, string VALOR, int ORDEN)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_MOD_REQUERIMIENTO_RESPONSABLE", IDE_MOD, VALOR, ORDEN);
        }
    }
}
