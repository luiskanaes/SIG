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
    public class DA_RRHH_SOL_FORMATIVO
    {
        Util oUtilitarios = new Util();
        public int INS_RRHH_SOL_FORMATIVO(BE_RRHH_SOL_FORMATIVO obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FORMATIVO,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.NOMBRE_PROYECTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.CENTRO_COSTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.SUPERVISOR_DIRECTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.UBICACION,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.ESP_FORMACION,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.CANTIDAD,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.USER_SOLICITA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.B_OBJETIVOS,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.B_ENTREGABLES,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.B_INDICADORES,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.C_INCLUYE,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.C_NO_INCLUYE,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.C_RIESGO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.C_RESTRICCIONES,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.D_REQUISITOS,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.D_COSTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.D_BENEFICIOS,tgSQLFieldType.TEXT),
                                          (object)UC_FormWeb.mSQLFieldOrNull(obj.DURACION,tgSQLFieldType.TEXT),


            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_SOL_FORMATIVO", Parametros));

        }
        public DataTable uspSEL_RRHH_SOL_FORMATIVO_POR_ESTADO(string ESTADOS)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_SOL_FORMATIVO_POR_ESTADO", ESTADOS);
        }
        public DataTable uspSEL_RRHH_SOL_FORMATIVO_PROCESAR(int id, string estado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_SOL_FORMATIVO_PROCESAR", id, estado );
        }
        public DataTable uspSEL_RRHH_SOL_FORMATIVO_POR_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_SOL_FORMATIVO_POR_ID", id);
        }
        public DataTable SP_CORREO_NUEVA_SOL_FORMATIVO(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_CORREO_NUEVA_SOL_FORMATIVO", id);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_PROCESAR_AREAS(int id, string COMENTARIOS, string ESTADO,string USUARIO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_PROCESAR_AREAS", id, COMENTARIOS, ESTADO, USUARIO);
        }
        public DataTable SP_CORREO_FORMATIVO_APROBACIONES(int IDE_FORMATIVO, string ESTADO, string EMAIL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_CORREO_FORMATIVO_APROBACIONES", IDE_FORMATIVO, ESTADO, EMAIL);
        }
    }
}
