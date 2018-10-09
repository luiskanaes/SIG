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
   public  class BL_RRHH_SOL_HITO
    {
        public int INS_RRHH_SOL_HITO(BE_RRHH_SOL_HITO obj)
        {
            try
            {
                return new DA_RRHH_SOL_HITO().INS_RRHH_SOL_HITO(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO(int IDE_FORMATIVO, int TIPO )
        {
            return new DA_RRHH_SOL_HITO().uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO(IDE_FORMATIVO,TIPO);
        }
        public DataTable uspDEL_RRHH_SOL_HITO_ID(int IDE_HITOS)
        {
            return new DA_RRHH_SOL_HITO().uspDEL_RRHH_SOL_HITO_ID(IDE_HITOS);
        }
    }
}
