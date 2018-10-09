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
    public class BL_RRHH_DESEMPENIO_ETAPAS
    {
        public int uspINS_RRHH_DESEMPENIO_ETAPAS(BE_RRHH_DESEMPENIO_ETAPAS oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_ETAPAS().uspINS_RRHH_DESEMPENIO_ETAPAS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_RRHH_DESEMPENIO_ETAPAS_ID(BE_RRHH_DESEMPENIO_ETAPAS oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_ETAPAS().uspINS_RRHH_DESEMPENIO_ETAPAS_ID(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_ETAPAS_POR_ANIO(int anio,string CECOS)
        {
            return new DA_RRHH_DESEMPENIO_ETAPAS().uspSEL_RRHH_DESEMPENIO_ETAPAS_POR_ANIO(anio, CECOS);
        }
        public DataTable uspUPD_RRHH_DESEMPENIO_ETAPAS(int IDE_ETAPAS,string  INICIO, string FIN , int FLG_ESTADO )
        {
            return new DA_RRHH_DESEMPENIO_ETAPAS().uspUPD_RRHH_DESEMPENIO_ETAPAS(IDE_ETAPAS, INICIO, FIN, FLG_ESTADO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_ETAPA_PERSONA(int anio, string dni, string ip_centro)
        {
            return new DA_RRHH_DESEMPENIO_ETAPAS().uspSEL_RRHH_DESEMPENIO_ETAPA_PERSONA(anio, dni, ip_centro);
        }
    }
}
