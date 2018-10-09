using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;
namespace DataAccess
{
  public   class DA_OPE_DETALLE_PERSONAL
    {
        Util oUtilitarios = new Util();

        public int Man_Insert_Datos_Personal_Data(BE_OPE_DETALLE_PERSONAL objDatosParticipante)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(objDatosParticipante.Detalle_personal,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objDatosParticipante.Id_dni,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objDatosParticipante.Id_minuta,tgSQLFieldType.TEXT),
                                        //(object)UC_FormWeb.mSQLFieldOrNull(objDatosParticipante.Centro_Costo,tgSQLFieldType.TEXT),
            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.SP_INSERTAR_DATOS_PARTICIPANTES", Parametros));

        }
        public DataTable SELECCIONAR_PARTICIPANTES(int codMinuta)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_SELECCIONAR_PARTICIPANTES", codMinuta);
        }

        public DataTable ELIMINAR_PARCIPANTES(string id_dni, int codMinuta)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ELIMINAR_PARCIPANTES", id_dni, codMinuta);
        }

    }
}
