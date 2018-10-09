using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using UserCode;
using System.Web;

namespace BusinessLogic
{
   public  class BL_RRHH_SOL_FORMATIVO
    {
        public int INS_RRHH_SOL_FORMATIVO(BE_RRHH_SOL_FORMATIVO objFormativo)
        {
            try
            {
                return new DA_RRHH_SOL_FORMATIVO().INS_RRHH_SOL_FORMATIVO(objFormativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable uspSEL_RRHH_SOL_FORMATIVO_POR_ESTADO(string  ESTADOS)
        {
            return new DA_RRHH_SOL_FORMATIVO().uspSEL_RRHH_SOL_FORMATIVO_POR_ESTADO(ESTADOS );
        }
        public DataTable uspSEL_RRHH_SOL_FORMATIVO_PROCESAR(int id, string estado)
        {
            return new DA_RRHH_SOL_FORMATIVO().uspSEL_RRHH_SOL_FORMATIVO_PROCESAR(id, estado);
        }
        public DataTable uspSEL_RRHH_SOL_FORMATIVO_POR_ID(int codigo)
        {
            return new DA_RRHH_SOL_FORMATIVO().uspSEL_RRHH_SOL_FORMATIVO_POR_ID(codigo);
        }
        public DataTable SP_CORREO_NUEVA_SOL_FORMATIVO(int IDE_FORMATIVO)
        {
            return new DA_RRHH_SOL_FORMATIVO().SP_CORREO_NUEVA_SOL_FORMATIVO(IDE_FORMATIVO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_PROCESAR_AREAS(int id, string COMENTARIOS, string AREA, string  USUARIO)
        {
            return new DA_RRHH_SOL_FORMATIVO().uspSEL_RRHH_FORMATIVO_PROCESAR_AREAS(id, COMENTARIOS, AREA, USUARIO);
        }
        public DataTable SP_CORREO_FORMATIVO_APROBACIONES(int IDE_FORMATIVO,string ESTADO, string EMAIL)
        {
            return new DA_RRHH_SOL_FORMATIVO().SP_CORREO_FORMATIVO_APROBACIONES(IDE_FORMATIVO, ESTADO, EMAIL);
        }
    }
}
