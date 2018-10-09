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
   public  class DA_RRHH_FORMATIVO_EXAM_CARTILLA
    {
        Util oUtilitarios = new Util();
        public int uspINS_RRHH_FORMATIVO_EXAM_CARTILLA(BE_RRHH_FORMATIVO_EXAM_CARTILLA obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_EXAMEN,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_PREGUNTA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_NOTA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.EVALUADOR,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.EVALUADO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_EVAL_EXAMEN,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_PROGRAMA,tgSQLFieldType.NUMERIC),
                                        

            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_FORMATIVO_EXAM_CARTILLA", Parametros));

        }
        public DataTable uspSEL_RRHH_FORMATIVO_EXAMEN_POR_ID(int IDE_FICHA, int IDE_FASE, int IDE_TIPO_EXA)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_EXAMEN_POR_ID", IDE_FICHA, IDE_FASE, IDE_TIPO_EXA);
        }
        public DataTable uspSEL_RESPUESTA_EXAM_CARTILLA(int IDE_PREGUNTA, string EVALUADOR, string EVALUADO, int IDE_EVAL_EXAMEN, int IDE_PROGRAMA)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RESPUESTA_EXAM_CARTILLA", IDE_PREGUNTA, EVALUADOR, EVALUADO, IDE_EVAL_EXAMEN, IDE_PROGRAMA);
        }
    }
}
