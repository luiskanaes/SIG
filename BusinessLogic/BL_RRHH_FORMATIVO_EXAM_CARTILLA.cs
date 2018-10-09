using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity;
using DataAccess;
using System.Data;
namespace BusinessLogic
{
    public class BL_RRHH_FORMATIVO_EXAM_CARTILLA
    {
        public int uspINS_RRHH_FORMATIVO_EXAM_CARTILLA(BE_RRHH_FORMATIVO_EXAM_CARTILLA obj)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAM_CARTILLA().uspINS_RRHH_FORMATIVO_EXAM_CARTILLA(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspSEL_RRHH_FORMATIVO_EXAMEN_POR_ID(int IDE_FICHA, int IDE_FASE, int IDE_TIPO_EXA)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAM_CARTILLA().uspSEL_RRHH_FORMATIVO_EXAMEN_POR_ID(IDE_FICHA, IDE_FASE, IDE_TIPO_EXA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RESPUESTA_EXAM_CARTILLA(int IDE_PREGUNTA, string EVALUADOR, string EVALUADO, int IDE_EVAL_EXAMEN, int IDE_PROGRAMA)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAM_CARTILLA().uspSEL_RESPUESTA_EXAM_CARTILLA(IDE_PREGUNTA,  EVALUADOR,  EVALUADO,  IDE_EVAL_EXAMEN,  IDE_PROGRAMA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
