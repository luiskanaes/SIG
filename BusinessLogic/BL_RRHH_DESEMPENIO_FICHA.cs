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
    public class BL_RRHH_DESEMPENIO_FICHA
    {
        public int uspINS_RRHH_DESEMPENIO_PERFIL(BE_RRHH_DESEMPENIO_FICHA oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_FICHA().uspINS_RRHH_DESEMPENIO_PERFIL(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_FICHA_PERFIL( int anio, int perfil)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_FICHA_PERFIL(anio, perfil);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_FICHA_ID(int IDE_DESEMPENIO)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_FICHA_ID(IDE_DESEMPENIO);
        }

        public DataTable uspSEL_RRHH_DESEMPENIO_OPCION_PERFIL(string DNI, string ANIO, int PADRE)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_OPCION_PERFIL(DNI,ANIO, PADRE);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OPCIONES(string DNI, string ANIO)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_OPCIONES(DNI, ANIO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_FICHA_DNI(string DNI, string ANIO)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_FICHA_DNI(DNI, ANIO);
        }
        public int uspSEL_RRHH_DESEMPENIO_INSERT_VARIOS(BE_RRHH_DESEMPENIO_FICHA oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_INSERT_VARIOS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable uspSEL_RRHH_DESEMPENIO_COLABORADORES(string DNI, string ANIO, int TIPO)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_COLABORADORES(DNI, ANIO, TIPO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_ADICIONAR(BE_RRHH_DESEMPENIO_FICHA oBE)
        {
            try
            {
                return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_ADICIONAR(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_PERSONAL_CARGOS(int ANIO, int TIPO, string COD_GERENCIA, string COD_CC)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_PERSONAL_CARGOS(ANIO, TIPO, COD_GERENCIA, COD_CC);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_PERSONAL_LIBRE(int ANIO)
        {
            return new DA_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_PERSONAL_LIBRE(ANIO);
        }
    }
}
