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
   public  class BL_OPE_DETALLE_PERSONAL
    {
        public int Man_Insert_Datos_PersonalBL(BE_OPE_DETALLE_PERSONAL objDatosParticipante)
        {
            try
            {
                return new DA_OPE_DETALLE_PERSONAL().Man_Insert_Datos_Personal_Data(objDatosParticipante);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SELECCIONAR_PARTICIPANTES(int codigoMinuta)
        {
            try
            {
                return new DA_OPE_DETALLE_PERSONAL().SELECCIONAR_PARTICIPANTES(codigoMinuta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ELIMINAR_PARCIPANTES(string id_dni, int codMinuta)
        {
            try
            {
                return new DA_OPE_DETALLE_PERSONAL().ELIMINAR_PARCIPANTES(id_dni, codMinuta);
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
