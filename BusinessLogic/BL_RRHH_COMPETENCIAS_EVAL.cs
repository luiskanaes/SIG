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
   public  class BL_RRHH_COMPETENCIAS_EVAL
    {
        public int Mant_Insert_Reconocimiento(BE_RRHH_COMPETENCIAS_EVAL oBEReconocimiento)
        {
            try
            {
                return new DA_RRHH_COMPETENCIAS_EVAL().Mant_Insert_Reconocimiento(oBEReconocimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ListarPersonal_mando(string supervisor)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().ListarPersonal_mando(supervisor);
        }
        public DataTable EnviarCorreoCompetencia(int IDE_COMPETENCIA)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().EnviarCorreoCompetencia(IDE_COMPETENCIA);
        }
        public DataTable ListarRequerimiento(string estado)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().ListarRequerimiento(estado);
        }
        public DataTable ListarRequerimientoID(int id)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().ListarRequerimientoID(id);
        }
        public DataTable ProcesarKardex_Competencia(int id, string codigo, int punto, string sustento_rechazo)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().ProcesarKardex_Competencia(id, codigo, punto, sustento_rechazo);
        }
        public DataTable SEL_RRHH_PRODUCTO_TIPOS()
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().SEL_RRHH_PRODUCTO_TIPOS();
        }
        public DataTable SEL_RRHH_PRODUCTO_POR_PUNTOS(decimal punto)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().SEL_RRHH_PRODUCTO_POR_PUNTOS(punto);
        }
        public DataTable SEL_RRHH_PERSONAL_EMPRESA_POR_ID(string dni)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().SEL_RRHH_PERSONAL_EMPRESA_POR_ID(dni);
        }
        public DataTable KARDEX_PRODUCTO_SOLICITA(string dni, int producto, string descripcion ,int entrada ,int salida)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().KARDEX_PRODUCTO_SOLICITA(dni, producto,  descripcion,  entrada,  salida);
        }
        public DataTable SEL_RRHH_KARDEX_PRODUCTOS(string estado)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().SEL_RRHH_KARDEX_PRODUCTOS(estado);
        }
        public DataTable ProcesarEntregaPremio(int id)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().ProcesarEntregaPremio(id);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_TODO()
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_EMPRESA_TODO();
        }
        public DataTable uspSEL_RRHH_PERSONAL_TIPO_TODO(string TIPO_TRABAJADOR)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_TIPO_TODO(TIPO_TRABAJADOR);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_POR_CORREO(string CORREO)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_EMPRESA_POR_CORREO(CORREO);
        }
        public DataTable uspUPD_RRHH_COMPETENCIAS_EVAL_SUSTENTO(int id, string sustento)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspUPD_RRHH_COMPETENCIAS_EVAL_SUSTENTO(id, sustento);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_CC(string centro)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_EMPRESA_CC(centro);
        }
        public DataTable USP_SEL_RESPONSABLE_BL(string centroCosto)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().USP_SEL_RESPONSABLE_DA(centroCosto);
        }
        public DataTable uspLISTAR_GERENCIA_CENTROS( int TIPO , string GERENENCIA, int EMPRESA )
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspLISTAR_GERENCIA_CENTROS(TIPO,  GERENENCIA,  EMPRESA);
        }

        public DataTable uspLISTAR_GERENCIA( int EMPRESA)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspLISTAR_GERENCIA(EMPRESA);
        }

        public DataTable uspLISTAR_GERENCIA_X_CENTROS( string GERENENCIA, int EMPRESA)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspLISTAR_GERENCIA_X_CENTROS( GERENENCIA, EMPRESA);
        }

        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_CC_TIPO(string centro,string tipo)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_EMPRESA_CC_TIPO(centro, tipo);
        }
        public DataTable uspSEL_RRHH_PERSONAL_GERENCIA_TIPO(string GERENENCIA, string tipo)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_GERENCIA_TIPO(GERENENCIA, tipo);
        }
        public DataTable uspSEL_RRHH_PERSONAL_GERENCIA_CARTA(string GERENENCIA, string tipo)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_GERENCIA_CARTA(GERENENCIA, tipo);
        }

        public DataTable uspSEL_RRHH_PERSONAL_CARTA_COBRANZA(string centro, string tipo)
        {
            return new DA_RRHH_COMPETENCIAS_EVAL().uspSEL_RRHH_PERSONAL_CARTA_COBRANZA(centro, tipo);
        }
    }
}
