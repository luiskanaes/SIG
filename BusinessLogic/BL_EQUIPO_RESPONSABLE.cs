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
    public class BL_EQUIPO_RESPONSABLE
    {
        public int uspINS_EQUIPO_RESPONSABLE(BE_EQUIPO_RESPONSABLE oBE)
        {
            try
            {
                return new DA_EQUIPO_RESPONSABLE().uspINS_EQUIPO_RESPONSABLE(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_EQUIPO_RESPONSABLE_GERENCIA(string GERENCIA)
        {
            return new DA_EQUIPO_RESPONSABLE().uspSEL_EQUIPO_RESPONSABLE_GERENCIA(GERENCIA);
        }
        public DataTable uspUPD_EQUIPO_RESPONSABLE_ESTADO(int IDE_RESPONSABLE, int FLG_ESTADO)
        {
            return new DA_EQUIPO_RESPONSABLE().uspUPD_EQUIPO_RESPONSABLE_ESTADO(IDE_RESPONSABLE, FLG_ESTADO);
        }
        public DataTable uspSEL_EQUIPO_RESPONSABLE_POR_ID(int IDE_RESPONSABLE)
        {
            return new DA_EQUIPO_RESPONSABLE().uspSEL_EQUIPO_RESPONSABLE_POR_ID(IDE_RESPONSABLE);
        }

        public DataTable uspSEL_EQUIPO_TRABAJO_ACTIVO(string DNI_TRABAJADOR, int FLG_ESTADO)
        {
            return new DA_EQUIPO_RESPONSABLE().uspSEL_EQUIPO_TRABAJO_ACTIVO(DNI_TRABAJADOR, FLG_ESTADO);
        }
        public DataTable uspSEL_EQUIPO_GERENCIA(string GERENCIA)
        {
            return new DA_EQUIPO_RESPONSABLE().uspSEL_EQUIPO_GERENCIA(GERENCIA);
        }
        public DataTable uspUPD_LIBERAR_EQUIPO_RESPONSABLE(int IDE_RESPONSABLE, string DNI_RESPONSABLE)
        {
            return new DA_EQUIPO_RESPONSABLE().uspUPD_LIBERAR_EQUIPO_RESPONSABLE(IDE_RESPONSABLE, DNI_RESPONSABLE);
        }
    }
}
