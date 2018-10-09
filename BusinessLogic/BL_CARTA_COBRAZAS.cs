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
    public class BL_CARTA_COBRAZAS
    {
        public int uspINS_CARTA_COBRAZAS(BE_CARTA_COBRAZAS oBE)
        {
            try
            {
                return new DA_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_CARTA_COBRAZAS_APROBACIONES(BE_CARTA_COBRAZAS_APROBACIONES oBE)
        {
            try
            {
                return new DA_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS_APROBACIONES(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_CARTA_COBRAZAS(int ANIO, string C_USUARIO, string IPCENTRO, string ESTADO, string ticket, string txtD_CENTRO_H)
        {
            return new DA_CARTA_COBRAZAS().uspSEL_CARTA_COBRAZAS(ANIO, C_USUARIO, IPCENTRO , ESTADO , ticket, txtD_CENTRO_H);
        }
        public DataTable uspSEL_CARTA_COBRAZAS_ID(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS().uspSEL_CARTA_COBRAZAS_ID(IDE_CARTA);
        }
        public int uspINS_CARTA_COBRAZAS_FILE(BE_CARTA_COBRAZAS_FILE oBE)
        {
            try
            {
                return new DA_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS_FILE(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_CARTA_COBRAZAS_FILE_IDE_CARTA(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS().uspSEL_CARTA_COBRAZAS_FILE_IDE_CARTA(IDE_CARTA);
        }
        public DataTable uspDEL_CARTA_COBRAZAS_FILE_ID(int IDE_FILE)
        {
            return new DA_CARTA_COBRAZAS().uspDEL_CARTA_COBRAZAS_FILE_ID(IDE_FILE);
        }
        public DataTable uspSEL_CARTA_COBRAZAS_CECOS(int ANIO, string C_USUARIO)
        {
            return new DA_CARTA_COBRAZAS().uspSEL_CARTA_COBRAZAS_CECOS(ANIO, C_USUARIO);
        }

        public DataTable uspUPD_CARTA_COBRAZAS_SAP(int IDE_CARTA, string SAP, string USER, string RUTA, string FILE)
        {
            return new DA_CARTA_COBRAZAS().uspUPD_CARTA_COBRAZAS_SAP(IDE_CARTA,SAP , USER, RUTA , FILE );
        }
        public DataTable uspSEL_CARTA_COBRAZAS_APROBACIONES(int ANIO, string C_USUARIO, string Estado)
        {
            return new DA_CARTA_COBRAZAS().uspSEL_CARTA_COBRAZAS_APROBACIONES(ANIO, C_USUARIO, Estado);
        }
        public DataTable uspINS_CARTA_COBRAZAS_GENERAR(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS_GENERAR(IDE_CARTA);
        }
        public DataTable uspDEL_CARTA_COBRAZAS(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS().uspDEL_CARTA_COBRAZAS(IDE_CARTA);
        }

        public DataTable SP_CORREO_APROBADOR_CARTACOBRANZA_PENDIENTE(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS().SP_CORREO_APROBADOR_CARTACOBRANZA_PENDIENTE(IDE_CARTA);
        }
    }
}
