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
    public class DA_REQUERIMIENTO
    {
        Util oUtilitarios = new Util();

        public int Mant_Insert_Requerimiento(BE_REQUERIMIENTO oBERequerimiento)
        { 
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.EMPRESA_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.NUMERO_REQUISICION ,tgSQLFieldType.NUMERIC ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.SECUENCIA ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.TIPO_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.FECHA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.OBRA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CATEGORIA_OBRERO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESPECIALIDAD_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.USUARIO_CREACION ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.USUARIO_ACTUALIZACION,tgSQLFieldType.TEXT ),


                                      
 //oBERequerimiento.EMPRESA_ORIGEN = (ddlEmpresas.SelectedValue);
 //       oBERequerimiento.CENTRO_COSTO_ORIGEN = (ddlCentro.SelectedValue);
 //       oBERequerimiento.CENTRO_COSTO = (ddlCentro.SelectedValue);
 //       oBERequerimiento.NUMERO_REQUISICION = Convert.ToInt32(txtRequerimiento.Text);
 //       oBERequerimiento.TIPO_TRABAJADOR = (ddlCentro.SelectedValue);
 //       oBERequerimiento.FECHA = Convert.ToDateTime(txtFechaAprobacion.Text);
                                       
            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_INS_TMP_REQUERIMIENTO_PERSONA", Parametros));
        }
        public DataTable Mant_Buscar_Requerimiento(BE_REQUERIMIENTO oBERequerimiento)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.EMPRESA_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.NUMERO_REQUISICION ,tgSQLFieldType.NUMERIC ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.SECUENCIA ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.TIPO_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.FECHA ,tgSQLFieldType.DATETIME ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESTADO_PROCESO ,tgSQLFieldType.NUMERIC ),
                                       (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ANALISTAS ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESTADOS ,tgSQLFieldType.TEXT ),
                                          (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CARGO ,tgSQLFieldType.TEXT ),

                                          };
            return oUtilitarios.EjecutaDatatable("USP_BUS_TMP_REQUERIMIENTO_PERSONA",Parametros);
        }

        public int Mant_Insert_RequerimientoMOD(BE_REQUERIMIENTO oBERequerimiento)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.EMPRESA_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.NUMERO_REQUISICION ,tgSQLFieldType.NUMERIC ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.SECUENCIA ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.TIPO_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.FECHA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.OBRA ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CATEGORIA_OBRERO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESPECIALIDAD_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                      
 //oBERequerimiento.EMPRESA_ORIGEN = (ddlEmpresas.SelectedValue);
 //       oBERequerimiento.CENTRO_COSTO_ORIGEN = (ddlCentro.SelectedValue);
 //       oBERequerimiento.CENTRO_COSTO = (ddlCentro.SelectedValue);
 //       oBERequerimiento.NUMERO_REQUISICION = Convert.ToInt32(txtRequerimiento.Text);
 //       oBERequerimiento.TIPO_TRABAJADOR = (ddlCentro.SelectedValue);
 //       oBERequerimiento.FECHA = Convert.ToDateTime(txtFechaAprobacion.Text);
                                       
            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_INS_TMP_REQUERIMIENTO_PERSONA_MOD", Parametros));
        }

        public DataTable Mant_Buscar_RequerimientoMOD(BE_REQUERIMIENTO oBERequerimiento)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.EMPRESA_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO_ORIGEN ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CENTRO_COSTO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.NUMERO_REQUISICION ,tgSQLFieldType.NUMERIC ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.SECUENCIA ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.TIPO_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.FECHA ,tgSQLFieldType.DATETIME ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESTADO_PROCESO ,tgSQLFieldType.NUMERIC ),
                                       (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ANALISTAS ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESTADOS ,tgSQLFieldType.TEXT ),
                                          (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.CARGO ,tgSQLFieldType.TEXT ),
                                            (object)UC_FormWeb.mSQLFieldOrNull(oBERequerimiento.ESPECIALIDAD_TRABAJADOR ,tgSQLFieldType.TEXT ),
                                          };
            return oUtilitarios.EjecutaDatatable("USP_BUS_TMP_REQUERIMIENTO_PERSONA_MOD", Parametros);
        }


        public DataTable anular_Requerimiento(int requerimiento)
        {

            return oUtilitarios.EjecutaDatatable("USP_ANULAR_REQUERIMIENTO", requerimiento);
        }

        public DataTable eliminar_Requerimiento(int ID_DETALLE_REQUERIMIENTO_PERSONAL)
        {

            return oUtilitarios.EjecutaDatatable("USP_ELIMINAR_REQUERIMIENTO", ID_DETALLE_REQUERIMIENTO_PERSONAL);
        }
       
    }
}
