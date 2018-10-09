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
   public  class DA_LOG_SOLPED
    {
        Util oUtilitarios = new Util();
        public DataTable SEL_LOG_MATERIALES_POR_GRUPO()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_LOG_MATERIALES_POR_GRUPO");
        }
        public DataTable SEL_LOG_MATERIALES_FILTRO_GRUPO(string grupo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_LOG_MATERIALES_FILTRO_GRUPO", grupo);
        }
        public DataTable SEL_LOG_MATERIALES_TODO()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_LOG_MATERIALES_TODOS");
        }
        public DataTable  Mant_Insert_LOG_SOLPED(BE_LOG_SOLPED oBESOl)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_SOLICITUD ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.COD_PEDIDO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IMPUTACION ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.SOCIEDAD ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.OBRA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FECHA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.VALOR,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.MONEDA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.CENTRO,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.GR_COMPRA,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.CENTRO_COSTE,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.SOLICITANTE,tgSQLFieldType.TEXT),
            };

            return new Util().EjecutaDatatable("uspINS_LOG_SOLPED", Parametros);
        }
        public DataTable SEL_LISTAR_PEDIDOS_SOLPED(string COD_PEDIDO, int IDE_SOLICITUD)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspINS_SOLPED_LISTA_ID", COD_PEDIDO, IDE_SOLICITUD);
        }
        public DataTable uspBUSCAR_SOLPED_LISTA_ID(string COD_PEDIDO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspBUSCAR_SOLPED_LISTA_ID", COD_PEDIDO);
        }
        public DataTable uspSEL_LOG_MATERIALES_PEP_IDE_MATERIAL(string IDE_MATERIAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_LOG_MATERIALES_PEP_IDE_MATERIAL", IDE_MATERIAL);
        }
        public DataTable uspUPDATE_LOG_MATERIALES_PEP(string IDE_MATERIAL, string  PEP_NUEVO, string PEP_ANTERIOR, string USUARIO_REGISTRO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspUPDATE_LOG_MATERIALES_PEP", IDE_MATERIAL, PEP_NUEVO, PEP_ANTERIOR, USUARIO_REGISTRO);
        }
        public DataTable uspSEL_PEP_CTA_CONTABLE()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_PEP_CTA_CONTABLE");
        }
        public DataTable uspCONSULTAR_SOLPED_LISTA_ID(int IDE_SOLICITUD)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspCONSULTAR_SOLPED_LISTA_ID", IDE_SOLICITUD);
        }
        public DataTable uspSEL_PEP_CTA_CONTABLE_POR_OBRA(string PROYECTO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_PEP_CTA_CONTABLE_POR_OBRA", PROYECTO);
        }
    }
}
