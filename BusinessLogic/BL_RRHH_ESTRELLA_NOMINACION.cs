using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using UserCode;
using System.Web;


namespace BusinessLogic
{
    public class BL_RRHH_ESTRELLA_NOMINACION
    {
        public DataTable USP_ESTRELLA_CC_EVALUADOR(string DNI_PERSONAL, string CC_PERSONA)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().USP_ESTRELLA_CC_EVALUADOR(DNI_PERSONAL, CC_PERSONA);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO(string DNI_EVALUADO, string DNI_EVALUADOR)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO(DNI_EVALUADO, DNI_EVALUADOR);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO_OBRA(string DNI_EVALUADO, string DNI_EVALUADOR)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_ESTRELLA_EVAL_EVALUADO_OBRA(DNI_EVALUADO, DNI_EVALUADOR);
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION(BE_RRHH_ESTRELLA_NOMINACION oBEReconocimiento)
        {
            try
            {
                return new DA_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION(oBEReconocimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION_OBRA(BE_RRHH_ESTRELLA_NOMINACION_OBRA oBEReconocimiento)
        {
            try
            {
                return new DA_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION_OBRA(oBEReconocimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS(BE_RRHH_ESTRELLA_NOMINACION oBEReconocimiento)
        {
            try
            {
                return new DA_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS(oBEReconocimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS_OBRA(BE_RRHH_ESTRELLA_NOMINACION_OBRA oBEReconocimiento)
        {
            try
            {
                return new DA_RRHH_ESTRELLA_NOMINACION().uspINS_RRHH_ESTRELLA_NOMINACION_VARIOS_OBRA(oBEReconocimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID(int IDE_NOMINACION)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID(IDE_NOMINACION);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID_OBRA(int IDE_NOMINACION)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID_OBRA(IDE_NOMINACION);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACIONES(string DNI_EVALUADOR)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_ESTRELLA_NOMINACIONES( DNI_EVALUADOR);
        }
        public DataTable USP_ESTRELLA_CC_PERSONAL(string DNI_PERSONAL, string CC_PERSONA)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().USP_ESTRELLA_CC_PERSONAL(DNI_PERSONAL, CC_PERSONA);
        }
        public DataTable uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ESTADOS(string FLG_ETAPAS, string CENTRO, string EVALUADOR)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ESTADOS(FLG_ETAPAS,  CENTRO,  EVALUADOR);
        }
        public DataTable uspSEL_RRHH_NOMINACION_PROCESAR(int id, string codigo, int punto, string sustento_rechazo)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_RRHH_NOMINACION_PROCESAR(id, codigo, punto, sustento_rechazo);
        }
        public DataTable uspUPD_RRHH_NOMINACION_SUSTENTO(int id, string sustento)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspUPD_RRHH_NOMINACION_SUSTENTO(id, sustento);
        }
        public DataTable USP_ESTRELLA_LISTAR_EVALUADOR()
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().USP_ESTRELLA_LISTAR_EVALUADOR();
        }
        public DataTable USP_ESTRELLA_LISTAR_EVALUADOR_CC(string DNI_PERSONAL, string CC_PERSONA)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().USP_ESTRELLA_LISTAR_EVALUADOR_CC(DNI_PERSONAL, CC_PERSONA);
        }
        public DataTable CORREO_NOMINACION(int IDE_NOMINACION)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().CORREO_NOMINACION(IDE_NOMINACION);
        }
        public DataTable uspSEL_TIPO_EVALUACION_ESTRELLA(string DNI_PERSONAL)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().uspSEL_TIPO_EVALUACION_ESTRELLA(DNI_PERSONAL);
        }
        public DataTable SP_CORREO_NOMINACION_IDE(int IDE_NOMINACION)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().SP_CORREO_NOMINACION_IDE(IDE_NOMINACION);
        }
        public DataTable SP_CORREO_NOMINACION_IDE_OBRA(int IDE_NOMINACION)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().SP_CORREO_NOMINACION_IDE_OBRA(IDE_NOMINACION);
        }
        public DataTable SP_CORREO_NUEVA_NOMINACION_OBRA(int IDE_NOMINACION)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION().SP_CORREO_NUEVA_NOMINACION_OBRA(IDE_NOMINACION);
        }
    }
}
     