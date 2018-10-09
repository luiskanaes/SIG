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
    public class BL_SOL_ANDAMIOS
    {
        public int uspINS_SOL_ANDAMIOS(BE_SOL_ANDAMIOS oBE)
        {
            try
            {
                return new DA_SOL_ANDAMIOS().uspINS_SOL_ANDAMIOS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_SOL_ANDAMIOS_USUARIO(string IDE_USUARIO, string ESTADO, string ANIO)
        {
            return new DA_SOL_ANDAMIOS().uspSEL_SOL_ANDAMIOS_USUARIO(IDE_USUARIO, ESTADO, ANIO);
        }
        public DataTable SP_ENVIARCORREO_SOL_ANDAMIOS(int p, string IDE_USUARIO)
        {
            return new DA_SOL_ANDAMIOS().SP_ENVIARCORREO_SOL_ANDAMIOS(p, IDE_USUARIO);
        }
        public DataTable uspSEL_SOL_ANDAMIOS_BANDEJA(string IDE_USUARIO, string ESTADO, string ANIO, string ticket, string campo, string direccion, string centro, string area, string especialidad)
        {
            return new DA_SOL_ANDAMIOS().uspSEL_SOL_ANDAMIOS_BANDEJA(IDE_USUARIO, ESTADO, ANIO, ticket, campo, direccion, centro, area, especialidad );
        }
        public DataTable uspUPD_SOL_ANDAMIOS_PROCESAR(int id, string codigo, string sustento_rechazo, string usuario)
        {
            return new DA_SOL_ANDAMIOS().uspUPD_SOL_ANDAMIOS_PROCESAR(id, codigo, sustento_rechazo, usuario);
        }
        public DataTable SP_ENVIARCORREO_SOL_ANDAMIOS_RPTA(int IDE_ANDAMIOS)
        {
            return new DA_SOL_ANDAMIOS().SP_ENVIARCORREO_SOL_ANDAMIOS_RPTA(IDE_ANDAMIOS);
        }
        public int uspUPD_SOL_ANDAMIOS(BE_SOL_ANDAMIOS oBE)
        {
            try
            {
                return new DA_SOL_ANDAMIOS().uspUPD_SOL_ANDAMIOS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_SOL_ANDAMIOS_IDE(int IDE_ANDAMIOS)
        {
            return new DA_SOL_ANDAMIOS().uspSEL_SOL_ANDAMIOS_IDE(IDE_ANDAMIOS);
        }
        public DataTable uspSEL_SOL_ANDAMIOS_PRIORIDAD(int PRIORIDAD, int IDE_ANDAMIOS)
        {
            return new DA_SOL_ANDAMIOS().uspSEL_SOL_ANDAMIOS_PRIORIDAD( PRIORIDAD , IDE_ANDAMIOS);
        }
    }
}
