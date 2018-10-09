using DataAccess.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DA_OPE_TEMAS
    {
        Util oUtilitarios = new Util();

       public int Mant_InsertarTemasData(BE_OPE_TEMAS objTemas)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Id_temas, tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Dsc_nombre_tema, tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Fch_fecha_original,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Fch_fecha_requerimiento,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Dsc_responsable,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Id_areas,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Id_minuta,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Id_parametro,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Fch_compromiso,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objTemas.Usuario,tgSQLFieldType.TEXT),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.USP_INSERT_TEMAS", Parametros));
        }
        public DataTable DA_SELECIONAR_TEMAS(int codigoTema)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_TEMAS", codigoTema);
        }
    }
}
