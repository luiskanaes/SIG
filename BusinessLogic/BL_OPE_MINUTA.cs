using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity;
using DataAccess;
using System.Data;
namespace BusinessLogic
{
    public class BL_OPE_MINUTA
    {
        public int Mant_Insert_Minuta(BE_OPE_MINUTA objMinuta)
        {
            try
            {
                return new DA_OPE_MINUTA().Mant_Insert_minutaData(objMinuta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SeleccionarMinuta_BL(int codigoMinuta)
        {
            return new DA_OPE_MINUTA().SeleccionarMinuta_DA(codigoMinuta);
        }
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_POR_CC(string centro)
        {
            return new DA_OPE_MINUTA().uspSEL_RRHH_PERSONAL_EMPRESA_POR_CC(centro);
        }
        public DataTable uspSEL_RRHH_PERSONAL_POR_CENTRO(string centro)
        {
            return new DA_OPE_MINUTA().uspSEL_RRHH_PERSONAL_POR_CENTRO(centro);
        }
        public DataTable USP_CONSULTAR_TEMAS_MES(string FECHA, string Centro)
        {
            return new DA_OPE_MINUTA().USP_CONSULTAR_TEMAS_MES(FECHA, Centro);
        }
        public int Insertar_Personal_Minuta(BE_OPE_MINUTA objInsertaParticipantes)
        {
            try
            {
                return new DA_OPE_MINUTA().Insertar_Personal_Minuta(objInsertaParticipantes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SP_EnviarCorreo_Minuta(int codigoAcuerdo, string usuario)
        {
            return new DA_OPE_MINUTA().SP_EnviarCorreo_Minuta(codigoAcuerdo, usuario);
        }

        public DataTable MOSTRAR_MINUTAS_BL(string centroCosto,string minuta,string reunion, string encargado, string lugar,string fechaCreacion, string periodos,string fechaModificado)
        {
            try
            {
                return new DA_OPE_MINUTA().MOSTRAR_MINUTAS_DA(centroCosto, minuta, reunion, encargado, lugar, fechaCreacion, periodos, fechaModificado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Mant_Mostrar_Historial(string centroCosto)
        {
            try
            {
                return new DA_OPE_MINUTA().Mant_Mostrar_Historial(centroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
