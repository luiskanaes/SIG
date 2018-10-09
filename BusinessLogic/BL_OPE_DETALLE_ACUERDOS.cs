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
   public  class BL_OPE_DETALLE_ACUERDOS
    {
        public int Mant_Insertar_AcuerdosBL(BE_OPE_DETALLE_ACUERDOS objAcuerdos)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().Mant_Insertar_AcuerdosData(objAcuerdos);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable BL_SELECIONAR_DETALLES(int codigoTema)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().DA_SELECIONAR_DETALLES(codigoTema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SELECIONAR_ACUERDOS_MINUTA(int Minuta,int filtroFecha,int ordenar)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().SELECIONAR_ACUERDOS_MINUTA(Minuta, filtroFecha, ordenar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SELECIONAR_ACUERDOS_CERRADOS_BL(int codigoMinuta, int filtroCerrrado, int ordenar)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().SELECIONAR_ACUERDOS_CERRADOS(codigoMinuta, filtroCerrrado, ordenar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SELECIONAR_MINUTA_BANDEJA_COMPROMISO(string centroCosto,string filtroDatos, int estadoFiltro, string fRequerimiento,string fCierre,string fCompromiso, string treunion, string fActualizado,string codDestino, string responsable)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().SELECIONAR_MINUTA_BANDEJA_COMPROMISO(centroCosto, filtroDatos, estadoFiltro, fRequerimiento, fCierre, fCompromiso, treunion, fActualizado, codDestino, responsable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Mant_ModificarTema_BL(int codDetalleAcuerdo, int codigoTema)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().Mant_ModificarTema_DA(codDetalleAcuerdo, codigoTema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable SELECIONAR_ACUERDOS_MINUTA_ID(int ID)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().SELECIONAR_ACUERDOS_MINUTA_ID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ELIMINAR_MINUTA_ACUERDOS_ID(int ID)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().ELIMINAR_MINUTA_ACUERDOS_ID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int MANT_USP_AGREGAR_COMENTARIO_BL(BE_OPE_DETALLE_ACUERDOS objAcu)
        {
            try
            {
                return new DA_OPE_DETALLE_ACUERDOS().MANT_USP_AGREGAR_COMENTARIO_DA(objAcu);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
