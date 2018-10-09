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
   public class BL_RRHH_DESEMPENIO_OBJETIVOS_MSG
    {
        public int uspINS_RRHH_DESEMPENIO_OBJETIVOS_MSG(BE_RRHH_DESEMPENIO_OBJETIVOS_MSG oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_OBJETIVOS_MSG().uspINS_RRHH_DESEMPENIO_OBJETIVOS_MSG(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG_POR_ID(int IDE_OBJETIVO)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS_MSG().uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG_POR_ID(IDE_OBJETIVO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG(int IDE_OBJETIVO)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS_MSG().uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG(IDE_OBJETIVO);
        }
        public DataTable uspUPD_RRHH_DESEMPENIO_AMPLIACION(int IDE_OBJETIVO, string COMENTARIOS,string USER_REGISTRO, string FECHA_AMPLIACION)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS_MSG().uspUPD_RRHH_DESEMPENIO_AMPLIACION(IDE_OBJETIVO, COMENTARIOS, USER_REGISTRO, FECHA_AMPLIACION);
        }
    }
}
