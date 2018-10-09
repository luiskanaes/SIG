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
   public class BL_INTRANET_BANER
    {
        public int uspINS_INTRANET_BANER(BE_INTRANET_BANER oBE)
        {
            try
            {
                return new DA_INTRANET_BANER().uspINS_INTRANET_BANER(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
