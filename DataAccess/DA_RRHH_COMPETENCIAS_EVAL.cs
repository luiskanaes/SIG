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
    public class DA_RRHH_COMPETENCIAS_EVAL
    {
        Util oUtilitarios = new Util();
        public int Mant_Insert_Reconocimiento(BE_RRHH_COMPETENCIAS_EVAL  oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_COMPETENCIA ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_EVALUADO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_SUPERVISOR ,tgSQLFieldType.TEXT ),  
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FACTOR ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SUSTENTO ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_COMPETENCIAS_EVAL", Parametros));
        }
        public DataTable ListarPersonal_mando(string supervisor)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_POR_SUPERVISOR", supervisor);
        }
        public DataTable EnviarCorreoCompetencia(int IDE_COMPETENCIA)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_EnviarCorreo_NuevaCompetencia", IDE_COMPETENCIA);
        }
        public DataTable ListarRequerimiento(string estado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_COMPETENCIAS_EVAL_POR_ESTADOS", estado);
        }
        public DataTable ListarRequerimientoID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_COMPETENCIAS_EVAL_POR_ID", id);
        }
        public DataTable ProcesarKardex_Competencia(int id, string codigo, int punto, string sustento_rechazo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_COMPETENCIAS_PROCESAR", id,codigo, punto, sustento_rechazo);
        }
        public DataTable SEL_RRHH_PRODUCTO_TIPOS()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PRODUCTO_TIPOS");
        }
        public DataTable SEL_RRHH_PRODUCTO_POR_PUNTOS(decimal  pto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PRODUCTO_POR_PUNTOS", pto);
        }
        public DataTable SEL_RRHH_PERSONAL_EMPRESA_POR_ID(string  dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID", dni);
        }
        public DataTable KARDEX_PRODUCTO_SOLICITA(string dni, int producto, string descripcion, int entrada, int salida)
        {
            return oUtilitarios.EjecutaDatatable("dbo.spr_InsertarKardex", dni, producto, descripcion, entrada, salida);
        }
        public DataTable SEL_RRHH_KARDEX_PRODUCTOS(string estado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_KARDEX_PRODUCTOS", estado);
        }
        public DataTable ProcesarEntregaPremio(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_KARDEX_ENTREGA", id);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_TODO()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_TODO");
        }
        public DataTable uspSEL_RRHH_PERSONAL_TIPO_TODO(string TIPO_TRABAJADOR )
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_TIPO_TODO", TIPO_TRABAJADOR);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_POR_CORREO(string CORREO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_POR_CORREO", CORREO);
        }
        public DataTable uspUPD_RRHH_COMPETENCIAS_EVAL_SUSTENTO(int id, string sustento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspUPD_RRHH_COMPETENCIAS_EVAL_SUSTENTO", id, sustento);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_CC( string centro)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_CC", centro);
        }
        public DataTable USP_SEL_RESPONSABLE_DA(string centroCosto)
        {
            return oUtilitarios.EjecutaDatatable("USP_SEL_RESPONSABLE", centroCosto);
        }
        public DataTable uspLISTAR_GERENCIA_CENTROS(int TIPO, string GERENENCIA, int EMPRESA)
        {
            return oUtilitarios.EjecutaDatatable("uspLISTAR_GERENCIA_CENTROS", TIPO, GERENENCIA,EMPRESA );
        }

        public DataTable uspLISTAR_GERENCIA_X_CENTROS(string GERENENCIA, int EMPRESA)
        {
            return oUtilitarios.EjecutaDatatable("uspLISTAR_GERENCIA_X_CENTROS", GERENENCIA, EMPRESA);
        }

        public DataTable uspLISTAR_GERENCIA(int EMPRESA)
        {
            return oUtilitarios.EjecutaDatatable("uspLISTAR_GERENCIA", EMPRESA);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_CC_TIPO(string centro, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_CC_TIPO", centro, tipo);
        }
        public DataTable uspSEL_RRHH_PERSONAL_GERENCIA_TIPO(string GERENENCIA, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_GERENCIA_TIPO", GERENENCIA, tipo);
        }
        public DataTable uspSEL_RRHH_PERSONAL_GERENCIA_CARTA(string GERENENCIA, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_GERENCIA_CARTA", GERENENCIA, tipo);
        }
        public DataTable uspSEL_RRHH_PERSONAL_CARTA_COBRANZA(string centro, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_CARTA_COBRANZA", centro, tipo);
        }
    }
}
