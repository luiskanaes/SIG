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
    public class DA_CARTA_COBRAZAS
    {
        Util oUtilitarios = new Util();
        public int uspINS_CARTA_COBRAZAS(BE_CARTA_COBRAZAS oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_CARTA  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_SOLICITA  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.C_USUARIO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.C_IPE_CENTRO ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.C_CENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.C_FECHA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.D_IPE_CENTRO  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.D_CENTRO   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.D_FECHA   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.D_USUARIO    ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_CREACION   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.NOTA   ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_CARTA_COBRAZAS", Parametros));
        }
        public int uspINS_CARTA_COBRAZAS_APROBACIONES(BE_CARTA_COBRAZAS_APROBACIONES oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_APROBACION  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_CARTA   ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.D_IPE_CENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.D_CENTRO ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_APRUEBA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO_CARGO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CARGO  ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_CARTA_COBRAZAS_APROBACIONES", Parametros));
        }
        public DataTable uspSEL_CARTA_COBRAZAS(int ANIO, string C_USUARIO, string IPCENTRO, string ESTADO, string ticket, string txtD_CENTRO_H)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS", ANIO,C_USUARIO,IPCENTRO ,ESTADO, ticket, txtD_CENTRO_H);
        }
        public DataTable uspSEL_CARTA_COBRAZAS_ID(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS_ID", IDE_CARTA);
        }
        public int uspINS_CARTA_COBRAZAS_FILE(BE_CARTA_COBRAZAS_FILE oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FILE  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ARCHIVO  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.RUTA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_CARTA ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.NOMBRE_ORIGINAL ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_CARTA_COBRAZAS_FILE", Parametros));
        }
        public DataTable uspSEL_CARTA_COBRAZAS_FILE_IDE_CARTA(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS_FILE_IDE_CARTA", IDE_CARTA);
        }
        public DataTable uspDEL_CARTA_COBRAZAS_FILE_ID(int IDE_FILE)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_CARTA_COBRAZAS_FILE_ID", IDE_FILE);
        }
        public DataTable uspSEL_CARTA_COBRAZAS_CECOS(int ANIO, string C_USUARIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS_CECOS", ANIO, C_USUARIO);
        }

        public DataTable uspUPD_CARTA_COBRAZAS_SAP(int IDE_CARTA, string SAP, string USER, string RUTA, string FILE)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_CARTA_COBRAZAS_SAP", IDE_CARTA,SAP ,USER, RUTA, FILE );
        }
        public DataTable uspSEL_CARTA_COBRAZAS_APROBACIONES(int ANIO, string C_USUARIO,string Estado)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS_APROBACIONES", ANIO, C_USUARIO, Estado);
        }
        public DataTable uspINS_CARTA_COBRAZAS_GENERAR(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("uspINS_CARTA_COBRAZAS_GENERAR", IDE_CARTA);
        }
        public DataTable uspDEL_CARTA_COBRAZAS(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_CARTA_COBRAZAS", IDE_CARTA);
        }
        public DataTable SP_CORREO_APROBADOR_CARTACOBRANZA_PENDIENTE(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("SP_CORREO_APROBADOR_CARTACOBRANZA_PENDIENTE", IDE_CARTA);
        }
    }
}
