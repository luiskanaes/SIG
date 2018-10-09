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
   public class BL_REQUERIMIENTO
    {

        public int Mant_Insert_Requerimiento(BE_REQUERIMIENTO  oBERequerimiento)
        {
            try
            {
                return new DA_REQUERIMIENTO().Mant_Insert_Requerimiento(oBERequerimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Mant_Buscar_Requerimiento(BE_REQUERIMIENTO oBERequerimiento)
        {
            return new DA_REQUERIMIENTO().Mant_Buscar_Requerimiento(oBERequerimiento);
        }


        public int Mant_Insert_RequerimientoMOD(BE_REQUERIMIENTO oBERequerimiento)
        {
            try
            {
                return new DA_REQUERIMIENTO().Mant_Insert_RequerimientoMOD(oBERequerimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Mant_Buscar_RequerimientoMOD(BE_REQUERIMIENTO oBERequerimiento)
        {
            return new DA_REQUERIMIENTO().Mant_Buscar_RequerimientoMOD(oBERequerimiento);
        }

        public DataTable anular_Requerimiento(int requerimiento)
        {
            return new DA_REQUERIMIENTO().anular_Requerimiento(requerimiento);
        }

        public DataTable eliminar_Requerimiento(int requerimiento)
        {
            return new DA_REQUERIMIENTO().eliminar_Requerimiento(requerimiento);
        }
        
    }
}
