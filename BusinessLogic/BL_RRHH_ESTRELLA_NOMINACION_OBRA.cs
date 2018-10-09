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
   public  class BL_RRHH_ESTRELLA_NOMINACION_OBRA
    {
        public DataTable uspSEL_RRHH_ESTRELLA_OBRA_POR_ESTADOS(string FLG_ETAPAS, string CENTRO, string EVALUADOR, string ENCARGADO)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION_OBRA().uspSEL_RRHH_ESTRELLA_OBRA_POR_ESTADOS(FLG_ETAPAS, CENTRO, EVALUADOR, ENCARGADO);
        }
        public DataTable uspSEL_RRHH_NOMINACION_PROCESAR_OBRA(int id, string codigo, int punto, string sustento_rechazo)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION_OBRA().uspSEL_RRHH_NOMINACION_PROCESAR_OBRA(id, codigo, punto, sustento_rechazo);
        }
        public DataTable USP_ESTRELLA_LISTAR_EVALUADOR_OBRA(string encargado)
        {
            return new DA_RRHH_ESTRELLA_NOMINACION_OBRA().USP_ESTRELLA_LISTAR_EVALUADOR_OBRA(encargado);
        }

    }
}
