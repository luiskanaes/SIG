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
   public  class BL_RRHH_FORMATIVO_EXAMEN
    {
        public DataTable USP_CORREO_EXAMEN_FORMATIVO(int IN_ORDEN, int IDE_FASE, int IDE_FICHA)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAMEN().USP_CORREO_EXAMEN_FORMATIVO(IN_ORDEN,  IDE_FASE, IDE_FICHA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable USP_EXAMEN_FORMATIVO(int ID, int IDE_FASE, int IDE_FICHA)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAMEN().USP_EXAMEN_FORMATIVO(ID, IDE_FASE, IDE_FICHA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_RRHH_FORMATIVO_EXAMEN(BE_RRHH_FORMATIVO_EXAMEN obj)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAMEN().uspINS_RRHH_FORMATIVO_EXAMEN(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN(int IDE_EVAL_EXAMEN, int IDE_FICHA, int IDE_TIPO_EXA, int IDE_FASE)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAMEN().uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN(IDE_EVAL_EXAMEN, IDE_FICHA, IDE_TIPO_EXA, IDE_FASE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable USP_VER_EXAMEN_FORMATIVO(int IN_ORDEN, int IDE_FASE, int IDE_FICHA)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAMEN().USP_VER_EXAMEN_FORMATIVO(IN_ORDEN, IDE_FASE, IDE_FICHA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable USP_CORREO_EXAMEN_EJECUTADO(string mensaje, string usuario)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_EXAMEN().USP_CORREO_EXAMEN_EJECUTADO(mensaje, usuario );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
