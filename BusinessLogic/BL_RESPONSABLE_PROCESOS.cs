using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using System.Data;

namespace BusinessLogic
{
   public  class BL_RESPONSABLE_PROCESOS
    {
        public int uspINS_RESPONSABLE_PROCESOS(BE_RESPONSABLE_PROCESOS oBE)
        {
            try
            {
                return new DA_RESPONSABLE_PROCESOS().uspINS_RESPONSABLE_PROCESOS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_POR_ID(string FLG_ESTADO, string NOMBRE, string PROCESO)
        {
            return new DA_RESPONSABLE_PROCESOS().uspSEL_RESPONSABLE_PROCESOS_POR_ID(FLG_ESTADO, NOMBRE, PROCESO);
        }
        public DataTable uspDEL_RESPONSABLE_PROCESOS_POR_ID(int IDE_RESPONSABLE)
        {
            return new DA_RESPONSABLE_PROCESOS().uspDEL_RESPONSABLE_PROCESOS_POR_ID(IDE_RESPONSABLE);
        }
    }
}
