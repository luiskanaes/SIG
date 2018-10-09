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
    public class DA_RRHH_FORMATIVO_EXAMEN
    {
        Util oUtilitarios = new Util();
        public DataTable USP_CORREO_EXAMEN_FORMATIVO(int IN_ORDEN, int IDE_FASE, int IDE_FICHA)
        {
            return oUtilitarios.EjecutaDatatable("USP_CORREO_EXAMEN_FORMATIVO", IN_ORDEN, IDE_FASE, IDE_FICHA);
        }
        public DataTable USP_VER_EXAMEN_FORMATIVO(int IN_ORDEN, int IDE_FASE, int IDE_FICHA)
        {
            return oUtilitarios.EjecutaDatatable("USP_VER_EXAMEN_FORMATIVO", IN_ORDEN, IDE_FASE, IDE_FICHA);
        }
        public DataTable USP_EXAMEN_FORMATIVO(int ID, int IDE_FASE, int IDE_FICHA)
        {
            return oUtilitarios.EjecutaDatatable("USP_EXAMEN_FORMATIVO", ID, IDE_FASE, IDE_FICHA);
        }
        public int uspINS_RRHH_FORMATIVO_EXAMEN(BE_RRHH_FORMATIVO_EXAMEN obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_EVAL_EXAMEN,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FICHA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.DNI_EVALUADOR,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.DNI_EVALUADO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FORTALEZAS,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.MEJORAS,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.COMPROMISOS,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_TIPO_EXA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FASE,tgSQLFieldType.TEXT),

            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_FORMATIVO_EXAMEN", Parametros));

        }
        public DataTable uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN(int IDE_EVAL_EXAMEN, int IDE_FICHA, int IDE_TIPO_EXA, int IDE_FASE)
        {
            return oUtilitarios.EjecutaDatatable("uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN", IDE_EVAL_EXAMEN, IDE_FICHA, IDE_TIPO_EXA, IDE_FASE);
        }
        public DataTable USP_CORREO_EXAMEN_EJECUTADO(string mensaje, string usuario)
        {
            return oUtilitarios.EjecutaDatatable("USP_CORREO_EXAMEN_EJECUTADO", mensaje, usuario);
        }
    }
}
