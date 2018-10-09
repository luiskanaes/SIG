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
    public class BL_MOD_REQUERIMIENTO
    {
        public int uspINS_MOD_REQUERIMIENTO(BE_MOD_REQUERIMIENTO oBE)
        {
            try
            {
                return new DA_MOD_REQUERIMIENTO().uspINS_MOD_REQUERIMIENTO(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_MOD_REQUERIMIENTO_DETALLE(BE_MOD_REQUERIMIENTO_DETALLE oBE)
        {
            try
            {
                return new DA_MOD_REQUERIMIENTO().uspINS_MOD_REQUERIMIENTO_DETALLE(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspUPD_MOD_REQUERIMIENTO_PERSONAL(BE_MOD_REQUERIMIENTO_PERSONAL oBE)
        {
            try
            {
                return new DA_MOD_REQUERIMIENTO().uspUPD_MOD_REQUERIMIENTO_PERSONAL(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_DETALLE_IDE_MOD(int IDE_MOD)
        {
            return new DA_MOD_REQUERIMIENTO().uspSEL_MOD_REQUERIMIENTO_DETALLE_IDE_MOD(IDE_MOD);
        }
        public DataTable uspDEL_MOD_REQUERIMIENTO(int IDE_REQUERIMIENTO ,int IDE_MOD)
        {
            return new DA_MOD_REQUERIMIENTO().uspDEL_MOD_REQUERIMIENTO(IDE_REQUERIMIENTO , IDE_MOD);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_BANDEJA(string TICKET, string  IP_CENTRO, int ANIO)
        {
            return new DA_MOD_REQUERIMIENTO().uspSEL_MOD_REQUERIMIENTO_BANDEJA(TICKET, IP_CENTRO, ANIO);
        }
        public DataTable uspDEL_MOD_REQUERIMIENTO_IDE(int IDE_REQUERIMIENTO, int IDE_MOD)
        {
            return new DA_MOD_REQUERIMIENTO().uspDEL_MOD_REQUERIMIENTO_IDE(IDE_REQUERIMIENTO, IDE_MOD);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_IDE(int IDE_MOD)
        {
            return new DA_MOD_REQUERIMIENTO().uspSEL_MOD_REQUERIMIENTO_IDE(IDE_MOD);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_REQ(int IDE_REQUERIMIENTO)
        {
            return new DA_MOD_REQUERIMIENTO().uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_REQ(IDE_REQUERIMIENTO);
        }
        public DataTable uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_IDE(int REQ_PERSONAL)
        {
            return new DA_MOD_REQUERIMIENTO().uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_IDE(REQ_PERSONAL);
        }
        public DataTable uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(int REQ_PERSONAL,string VALOR, int ORDEN)
        {
            return new DA_MOD_REQUERIMIENTO().uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(REQ_PERSONAL, VALOR, ORDEN);
        }
        public DataTable uspUPD_MOD_REQUERIMIENTO_RESPONSABLE(int IDE_MOD, string VALOR, int ORDEN)
        {
            return new DA_MOD_REQUERIMIENTO().uspUPD_MOD_REQUERIMIENTO_RESPONSABLE(IDE_MOD, VALOR, ORDEN);
        }
    }
}
