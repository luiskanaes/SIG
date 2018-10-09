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
    public  class DA_CECOS
    {
        Util oUtilitarios = new Util();
        public DataTable SEL_CECOS_POR_CATEGORIA_EMPRESA(string categoria, string ip)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_CECOS_POR_CATEGORIA_EMPRESA", categoria,  ip);
        }
        public DataTable uspDELETE_LOG_SOLPED_USER_REGISTRO( string ip)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspDELETE_LOG_SOLPED_USER_REGISTRO",  ip);
        }
        public DataTable uspCONSULTAR_LOG_SOLPED_USER(string ip)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspCONSULTAR_LOG_SOLPED_USER", ip);
        }
        public DataTable SEL_CECOS_CENTRO_LOGISTICO(string sociedad, string imputacion)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_CECOS_CENTRO_LOGISTICO", sociedad, imputacion);
        }
        public DataTable SEL_GRUPO_COMPRAS( string obra)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_GRUPO_COMPRAS", obra);
        }
        public DataTable uspSEL_CECOS_RRHH( int empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_CECOS", empresa);
        }
        public DataTable USP_EMPRESAS()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_EMPRESAS");
        }
        public int uspUPD_CECOS_PRESUPUESTO(BE_CECOS oBESOl)
        {
            object[] Parametros = new[] {
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.COD_CENTRO ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.V_PO_FACTURADO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.V_PO_PROVISION  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.V_PO_DESAJUSTE  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.V_POM_FACTURADO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.V_POM_PROVISION  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.V_POM_DESAJUSTE  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_PO_PROVISION  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_PO_DIRECTO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_PO_INDIRECTO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_PO_MATERIAL  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_PO_SUBCONTRATO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_POM_PROVISION  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_POM_DIRECTO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_POM_INDIRECTO  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_POM_MATERIAL  ,tgSQLFieldType.TEXT),
                (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.C_POM_SUBCONTRATO   ,tgSQLFieldType.TEXT),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspUPD_CECOS_PRESUPUESTO", Parametros));
        }
        public DataTable uspSEL_CECOS_POR_ID(string COD_CENTRO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CECOS_POR_ID", COD_CENTRO);
        }

    }
}
