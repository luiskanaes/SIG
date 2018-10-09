using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace BusinessLogic
{
    public class BL_ENLACES_INTRANET
    {
        public DataTable LISTAR_EXAMENES_LISTAR_ENLANCES_INTRANET()
        {
            try
            {
                return new DA_ENLACES_INTRANET().LISTAR_EXAMENES_X_ESTUDIANTE();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
