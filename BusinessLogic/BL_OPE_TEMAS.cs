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
    public class BL_OPE_TEMAS
    {
        public int Mant_InsertarTemasBL(BE_OPE_TEMAS objTemas)
        {
            try
            {
                return new DA_OPE_TEMAS().Mant_InsertarTemasData(objTemas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BL_SELECIONAR_TEMAS(int cocodigoTema)
        {
            return new DA_OPE_TEMAS().DA_SELECIONAR_TEMAS(cocodigoTema);
        }
    }
}
