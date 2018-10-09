using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;
using System.Data;

namespace DataAccess
{
    public class DA_RRHH_ESTRELLA_NOMINACION_OBRA
    {
        Util oUtilitarios = new Util();
        public DataTable uspSEL_RRHH_ESTRELLA_OBRA_POR_ESTADOS(string FLG_ETAPAS, string CENTRO, string EVALUADOR, string ENCARGADO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_ESTRELLA_OBRA_POR_ESTADOS", FLG_ETAPAS, CENTRO, EVALUADOR, ENCARGADO);
        }
        public DataTable uspSEL_RRHH_NOMINACION_PROCESAR_OBRA(int id, string codigo, int punto, string sustento_rechazo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_NOMINACION_PROCESAR_OBRA", id, codigo, punto, sustento_rechazo);
        }
        public DataTable USP_ESTRELLA_LISTAR_EVALUADOR_OBRA(string encargado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ESTRELLA_LISTAR_EVALUADOR_OBRA", encargado);
        }

    }
}
