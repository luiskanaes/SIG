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
    public class BL_RRHH_FORMATIVO_FICHA
    {
        public int uspINS_RRHH_FORMATIVO_FICHA(BE_RRHH_FORMATIVO_FICHA obj)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_FICHA().uspINS_RRHH_FORMATIVO_FICHA(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA_TODOS(string FLG_ESTADO)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FICHA_TODOS(FLG_ESTADO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA(string FLG_ESTADO)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FICHA(FLG_ESTADO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA_ID(int id)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FICHA_ID(id );
        }
        public int uspINS_RRHH_FORMATIVO_PLANES(BE_RRHH_FORMATIVO_PLANES obj)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_FICHA().uspINS_RRHH_FORMATIVO_PLANES(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspDEL_RRHH_FORMATIVO_PLANES_POR_ID(int id)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspDEL_RRHH_FORMATIVO_PLANES_POR_ID(id);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_PLANES_POR_FICHA(int id)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_PLANES_POR_FICHA(id);
        }

        public int uspINS_RRHH_FORMATIVO_FASE(BE_RRHH_FORMATIVO_FASE obj)
        {
            try
            {
                return new DA_RRHH_FORMATIVO_FICHA().uspINS_RRHH_FORMATIVO_FASE(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspSEL_RRHH_FORMATIVO_FASE_POR_FICHA(int id)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FASE_POR_FICHA(id);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FASE_POR_ID(int id)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FASE_POR_ID(id);
        }
        public DataTable uspDEL_RRHH_FORMATIVO_FASE_POR_ID(int id)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspDEL_RRHH_FORMATIVO_FASE_POR_ID(id);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FASE_CONTROL(int IDE_FASE  , int TIPO, int ESTADO)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FASE_CONTROL(IDE_FASE, TIPO, ESTADO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA_DNI(string  dni)
        {
            return new DA_RRHH_FORMATIVO_FICHA().uspSEL_RRHH_FORMATIVO_FICHA_DNI(dni);
        }
    }
}
