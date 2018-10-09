using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;
using System.Data;

namespace DataAccess
{
    public class DA_OPE_MINUTA
    {
        Util oUtilitarios = new Util();
        public int Mant_Insert_minutaData(BE_OPE_MINUTA objMinuta)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.M_id_minuta,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Id_dni ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Fch_fecha_registro ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Num_numero_registro ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_tipo_fecha ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_asunto,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_lugar,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_nombre_cliente,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_contrato,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_obra,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_proyecto,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Fch_HoraInicial,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Fch_HoraFinal,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Dsc_reunion,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objMinuta.Usuario,tgSQLFieldType.TEXT),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.USP_INSERTAR_DATOS_MINUTA", Parametros));
        }

        public DataTable SeleccionarMinuta_DA(int codigoMinuta)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_MINUTA", codigoMinuta);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_POR_CC(string centro)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_POR_CC", centro);
        }
        public DataTable uspSEL_RRHH_PERSONAL_POR_CENTRO(string centro)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_POR_CENTRO", centro);
        }
        public DataTable USP_CONSULTAR_TEMAS_MES(string FECHA, string centro)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONSULTAR_TEMAS_DEL_DIA", FECHA,centro);
        }
        public int Insertar_Personal_Minuta(BE_OPE_MINUTA objInsertaParticipantes)
        {
            object[] Parametros = new[] {
                                (object)UC_FormWeb.mSQLFieldOrNull(objInsertaParticipantes.M_id_minuta,tgSQLFieldType.TEXT),
                                (object)UC_FormWeb.mSQLFieldOrNull(objInsertaParticipantes.Dsc_reunion,tgSQLFieldType.TEXT),
                                (object)UC_FormWeb.mSQLFieldOrNull(objInsertaParticipantes.Dsc_proyecto,tgSQLFieldType.TEXT),

            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_INSERTAR_PERSONAL_MINUTA", Parametros));
        }

        public DataTable SP_EnviarCorreo_Minuta(int codigoAcuerdo, string usuario)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_EnviarCorreo_Minuta", codigoAcuerdo, usuario);
        }

        public DataTable MOSTRAR_MINUTAS_DA(string centroCosto,string minuta,string reunion,string encargado, string lugar,string fecha,string periodo, string fechaModificado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_MOSTRAR_MINUTAS", centroCosto,minuta, reunion, encargado, lugar, fecha, periodo, fechaModificado);
        }        
        public DataTable Mant_Mostrar_Historial(string centroCosto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_MOSTRAR_HISTORIAL", centroCosto);
        }
    }
}
