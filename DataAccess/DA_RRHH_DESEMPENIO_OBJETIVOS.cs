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
    public class DA_RRHH_DESEMPENIO_OBJETIVOS
    {
        Util oUtilitarios = new Util();
        public int uspINS_RRHH_DESEMPENIO_OBJETIVOS(BE_RRHH_DESEMPENIO_OBJETIVOS oBE)
        {
            object[] Parametros = new[] {
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_OBJETIVO ,tgSQLFieldType.NUMERIC ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_DESEMPENIO ,tgSQLFieldType.NUMERIC ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.OBJETIVO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.INDICADOR ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_PERSONA ,tgSQLFieldType.TEXT),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.PESO ,tgSQLFieldType.NUMERIC ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.INICIO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TERMINO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.J_COMENTARIOS_JEFE ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.J_USER_JEFE  ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_AMPLIACION ,tgSQLFieldType.TEXT ),
                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.APROBAR ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_DESEMPENIO_OBJETIVOS", Parametros));
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA(string dni, int anio,string IDE_OBJETIVO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA", dni, anio, IDE_OBJETIVO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_ID(int IDE_OBJETIVO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OBJETIVOS_ID", IDE_OBJETIVO);
        }
        public DataTable uspDEL_RRHH_DESEMPENIO_OBJETIVOS_ID(int IDE_OBJETIVO)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_RRHH_DESEMPENIO_OBJETIVOS_ID", IDE_OBJETIVO);
        }
        public DataTable SP_EnviarCorreo_objetivoNuevo(string dni, int anio, string comentarios)
        {
            return oUtilitarios.EjecutaDatatable("SP_EnviarCorreo_objetivoNuevo", dni, anio, comentarios);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OBJETIVOS_LIKE(int IDE_OBJETIVO, string DNI, int ANIO, int ETAPA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OBJETIVOS_LIKE", IDE_OBJETIVO,DNI, ANIO, ETAPA);
        }
        public DataTable uspUPD_RRHH_DESEMPENIO_AVANCE(int IDE_OBJETIVO, int U_CALIFICACION_PERSONA)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_RRHH_DESEMPENIO_AVANCE", IDE_OBJETIVO, U_CALIFICACION_PERSONA);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_GRAFICO(string DNI_PERSONA, int ANIO, string  IDE_OBJETIVO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_GRAFICO", DNI_PERSONA, ANIO, IDE_OBJETIVO);
        }
    }
}
