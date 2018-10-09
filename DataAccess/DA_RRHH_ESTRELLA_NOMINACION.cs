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
    public class DA_RRHH_ESTRELLA_NOMINACION
    {
        Util oUtilitarios = new Util();
        public DataTable USP_ESTRELLA_CC_EVALUADOR(string  DNI_PERSONAL, string CC_PERSONA)
        {
            return oUtilitarios.EjecutaDatatable("USP_ESTRELLA_CC_EVALUADOR", DNI_PERSONAL,CC_PERSONA);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO(string DNI_EVALUADO, string DNI_EVALUADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO", DNI_EVALUADO, DNI_EVALUADOR);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO_OBRA(string DNI_EVALUADO, string DNI_EVALUADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO_OBRA", DNI_EVALUADO, DNI_EVALUADOR);
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION(BE_RRHH_ESTRELLA_NOMINACION oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_NOMINACION ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_EVALUADO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_SUPERVISOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FACTOR ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SUSTENTO ,tgSQLFieldType.TEXT ),
                                  

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_ESTRELLA_NOMINACION", Parametros));
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION_OBRA(BE_RRHH_ESTRELLA_NOMINACION_OBRA oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_NOMINACION ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_EVALUADO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_SUPERVISOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FACTOR ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SUSTENTO ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_ESTRELLA_NOMINACION_OBRA", Parametros));
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS(BE_RRHH_ESTRELLA_NOMINACION oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_NOMINACION ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_EVALUADO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_SUPERVISOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FACTORES ,tgSQLFieldType.TEXT),
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS", Parametros));
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS_OBRA(BE_RRHH_ESTRELLA_NOMINACION_OBRA oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_NOMINACION ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_EVALUADO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_SUPERVISOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FACTORES ,tgSQLFieldType.TEXT),
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS_OBRA", Parametros));
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID(int IDE_NOMINACION)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID", IDE_NOMINACION);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID_OBRA(int IDE_NOMINACION)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID_OBRA", IDE_NOMINACION);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACIONES(string DNI_EVALUADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_NOMINACIONES",  DNI_EVALUADOR);
        }
        public DataTable USP_ESTRELLA_CC_PERSONAL(string DNI_PERSONAL, string CC_PERSONA)
        {
            return oUtilitarios.EjecutaDatatable("USP_ESTRELLA_CC_PERSONAL", DNI_PERSONAL, CC_PERSONA);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ESTADOS(string FLG_ETAPAS, string CENTRO, string EVALUADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ESTADOS", FLG_ETAPAS, CENTRO, EVALUADOR);
        }
        public DataTable uspSEL_RRHH_NOMINACION_PROCESAR(int id, string codigo, int punto, string sustento_rechazo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_NOMINACION_PROCESAR", id, codigo, punto, sustento_rechazo);
        }
        public DataTable uspUPD_RRHH_NOMINACION_SUSTENTO(int id, string sustento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspUPD_RRHH_NOMINACION_SUSTENTO", id, sustento);
        }
        public DataTable USP_ESTRELLA_LISTAR_EVALUADOR()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ESTRELLA_LISTAR_EVALUADOR" );
        }
        public DataTable USP_ESTRELLA_LISTAR_EVALUADOR_CC(string DNI_PERSONAL, string CC_PERSONA)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ESTRELLA_LISTAR_EVALUADOR_CC", DNI_PERSONAL, CC_PERSONA);
        }
        public DataTable CORREO_NOMINACION(int IDE_NOMINACION)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_EnviarCorreo_NuevaNominacion", IDE_NOMINACION);
        }
        public DataTable SP_CORREO_NUEVA_NOMINACION_OBRA(int IDE_NOMINACION)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_CORREO_NUEVA_NOMINACION_OBRA", IDE_NOMINACION);
        }
        public DataTable uspSEL_TIPO_EVALUACION_ESTRELLA(string DNI_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_TIPO_EVALUACION_ESTRELLA", DNI_PERSONAL);
        }
        public DataTable SP_CORREO_NOMINACION_IDE(int IDE_NOMINACION)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_CORREO_NOMINACION_IDE", IDE_NOMINACION);
        }
        public DataTable SP_CORREO_NOMINACION_IDE_OBRA(int IDE_NOMINACION)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_CORREO_NOMINACION_IDE_OBRA", IDE_NOMINACION);
        }
    }
}
