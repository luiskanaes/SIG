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
    public class BL_LOG_SOLPED
    {
        public DataTable SEL_LOG_MATERIALES_POR_GRUPO()
        {
            try
            {
                return new DA_LOG_SOLPED().SEL_LOG_MATERIALES_POR_GRUPO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SEL_LOG_MATERIALES_FILTRO_GRUPO(string grupo)
        {
            try
            {
                return new DA_LOG_SOLPED().SEL_LOG_MATERIALES_FILTRO_GRUPO(grupo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SEL_LOG_MATERIALES_TODO()
        {
            try
            {
                return new DA_LOG_SOLPED().SEL_LOG_MATERIALES_TODO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Mant_Insert_LOG_SOLPED(BE_LOG_SOLPED oBESOl)
        {
            try
            {
                return new DA_LOG_SOLPED().Mant_Insert_LOG_SOLPED(oBESOl);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public DataTable SEL_LISTAR_PEDIDOS_SOLPED(string   COD_PEDIDO, int  IDE_SOLICITUD)
        {
            try
            {
                return new DA_LOG_SOLPED().SEL_LISTAR_PEDIDOS_SOLPED(COD_PEDIDO,   IDE_SOLICITUD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspBUSCAR_SOLPED_LISTA_ID(string COD_PEDIDO)
        {
            try
            {
                return new DA_LOG_SOLPED().uspBUSCAR_SOLPED_LISTA_ID(COD_PEDIDO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_LOG_MATERIALES_PEP_IDE_MATERIAL(string IDE_MATERIAL)
        {
            try
            {
                return new DA_LOG_SOLPED().uspSEL_LOG_MATERIALES_PEP_IDE_MATERIAL(IDE_MATERIAL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspUPDATE_LOG_MATERIALES_PEP(string IDE_MATERIAL, string PEP_NUEVO, string PEP_ANTERIOR, string USUARIO_REGISTRO)
        {
            try
            {
                return new DA_LOG_SOLPED().uspUPDATE_LOG_MATERIALES_PEP(IDE_MATERIAL, PEP_NUEVO, PEP_ANTERIOR, USUARIO_REGISTRO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_PEP_CTA_CONTABLE()
        {
            try
            {
                return new DA_LOG_SOLPED().uspSEL_PEP_CTA_CONTABLE();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspCONSULTAR_SOLPED_LISTA_ID(int IDE_SOLICITUD)
        {
            try
            {
                return new DA_LOG_SOLPED().uspCONSULTAR_SOLPED_LISTA_ID(IDE_SOLICITUD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_PEP_CTA_CONTABLE_POR_OBRA(string PROYECTO)
        {
            try
            {
                return new DA_LOG_SOLPED().uspSEL_PEP_CTA_CONTABLE_POR_OBRA(PROYECTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
