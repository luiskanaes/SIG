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
    public class BL_EQUIPO_TRABAJO
    {
        public int uspINS_EQUIPO_TRABAJO(BE_EQUIPO_TRABAJO oBE)
        {
            try
            {
                return new DA_EQUIPO_TRABAJO().uspINS_EQUIPO_TRABAJO(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_SUPERVISOR( int IDE_RESPONSABLE)
        {
            return new DA_EQUIPO_TRABAJO().uspSEL_EQUIPO_TRABAJO_SUPERVISOR( IDE_RESPONSABLE);
        }
        public DataTable uspUPD_EQUIPO_TRABAJADOR(int IDE_EQUIPO , int FLG_ESTADO)
        {
            return new DA_EQUIPO_TRABAJO().uspUPD_EQUIPO_TRABAJADOR(IDE_EQUIPO, FLG_ESTADO);
        }
        public DataTable uspINS_EQUIPO_TRABAJO_VARIOS(int IDE_RESPONSABLE, string CENTRO_COSTO, string USER_REGISTRA)
        {
            return new DA_EQUIPO_TRABAJO().uspINS_EQUIPO_TRABAJO_VARIOS(IDE_RESPONSABLE, CENTRO_COSTO, USER_REGISTRA);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_SUPERVISOR_DNI(string DNI_RESPONSABLE)
        {
            return new DA_EQUIPO_TRABAJO().uspSEL_EQUIPO_TRABAJO_SUPERVISOR_DNI(DNI_RESPONSABLE);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_DNI(string DNI_TRABAJADOR)
        {
            return new DA_EQUIPO_TRABAJO().uspSEL_EQUIPO_TRABAJO_DNI(DNI_TRABAJADOR);
        }
        public DataTable uspSEL_EQUIPO_TRABAJO_LIBRE(string DNI_TRABAJADOR)
        {
            return new DA_EQUIPO_TRABAJO().uspSEL_EQUIPO_TRABAJO_LIBRE(DNI_TRABAJADOR);
        }
    }
}
