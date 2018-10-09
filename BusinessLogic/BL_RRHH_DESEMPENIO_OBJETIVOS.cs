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
   public class BL_RRHH_DESEMPENIO_OBJETIVOS
    {
        public int uspINS_RRHH_DESEMPENIO_OBJETIVOS(BE_RRHH_DESEMPENIO_OBJETIVOS oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspINS_RRHH_DESEMPENIO_OBJETIVOS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA(string dni, int anio, string IDE_OBJETIVO)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA(dni,anio, IDE_OBJETIVO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_ID(int IDE_OBJETIVO)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspSEL_RRHH_DESEMPENIO_OBJETIVOS_ID(IDE_OBJETIVO);
        }
        public DataTable uspDEL_RRHH_DESEMPENIO_OBJETIVOS_ID(int IDE_OBJETIVO)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspDEL_RRHH_DESEMPENIO_OBJETIVOS_ID(IDE_OBJETIVO);
        }
        public DataTable SP_EnviarCorreo_objetivoNuevo(string dni, int anio, string comentarios)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().SP_EnviarCorreo_objetivoNuevo(dni, anio, comentarios);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_LIKE(int IDE_OBJETIVO, string DNI, int ANIO, int ETAPA)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspSEL_RRHH_DESEMPENIO_OBJETIVOS_LIKE(IDE_OBJETIVO, DNI,ANIO,ETAPA );
        }
        public DataTable uspUPD_RRHH_DESEMPENIO_AVANCE(int IDE_OBJETIVO, int U_CALIFICACION_PERSONA)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspUPD_RRHH_DESEMPENIO_AVANCE(IDE_OBJETIVO, U_CALIFICACION_PERSONA);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_GRAFICO(string DNI_PERSONA,  int ANIO, string  IDE_OBJETIVO)
        {
            return new DA_RRHH_DESEMPENIO_OBJETIVOS().uspSEL_RRHH_DESEMPENIO_GRAFICO(DNI_PERSONA,  ANIO,  IDE_OBJETIVO);
        }
    }
}
