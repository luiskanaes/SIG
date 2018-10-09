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
    public  class BL_LOG_SOLPED_DETALLE
    {
        public int INS_LOG_SOLPED_DETALLE(BE_LOG_SOLPED_DETALLE oBE)
        {
            try
            {
                return new DA_LOG_SOLPED_DETALLE().INS_LOG_SOLPED_DETALLE(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspDEL_LOG_SOLPED_DETALLE_POR_ID(int id)
        {
            try
            {
                return new DA_LOG_SOLPED_DETALLE().uspDEL_LOG_SOLPED_DETALLE_POR_ID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspDELETE_LOG_SOLPED_DETALLE_ID(int id)
        {
            try
            {
                return new DA_LOG_SOLPED_DETALLE().uspDELETE_LOG_SOLPED_DETALLE_ID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
