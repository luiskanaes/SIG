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
    public class DA_RRHH_FORMATIVO_FICHA
    {
        Util oUtilitarios = new Util();
        public int uspINS_RRHH_FORMATIVO_FICHA(BE_RRHH_FORMATIVO_FICHA obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FICHA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.APELLIDOS,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.NOMBRES,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.DNI,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FECHA_NACIMIENTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_PROGRAMA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_CARRERA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_UNIVERSIDAD,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_NIVEL,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.NUM_COLEGIATURA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.RESIDENCIA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.ESTADO_CIVIL,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.TUTOR,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.TUTOR_CORREO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.CARGO_TUTOR,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.INT_SEGMENTACION,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FIN_CONTRATO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FORTALEZA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.OPORTUNIDAD,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.USER_REGISTRO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FOTO_RUTA,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FOTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.CORREO,tgSQLFieldType.TEXT),
                                         (object)UC_FormWeb.mSQLFieldOrNull(obj.TELEFONO,tgSQLFieldType.TEXT),


            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_FORMATIVO_FICHA", Parametros));

        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA_TODOS(string FLG_ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FICHA_TODOS", FLG_ESTADO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA(string FLG_ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FICHA", FLG_ESTADO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FICHA_ID", id);
        }
        public int uspINS_RRHH_FORMATIVO_PLANES(BE_RRHH_FORMATIVO_PLANES obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_PLANES,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.DURACION,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.DESCRIPCION,tgSQLFieldType.TEXT),
                                        
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FICHA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.USER_REGISTRO,tgSQLFieldType.TEXT),


            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_FORMATIVO_PLANES", Parametros));

        }
        public DataTable uspSEL_RRHH_FORMATIVO_PLANES_POR_FICHA(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_PLANES_POR_FICHA", id);
        }
        public DataTable uspDEL_RRHH_FORMATIVO_PLANES_POR_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspDEL_RRHH_FORMATIVO_PLANES_POR_ID", id);
        }
        public int uspINS_RRHH_FORMATIVO_FASE(BE_RRHH_FORMATIVO_FASE obj)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FASE,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.PROYECTO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.JEFE,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FECHA_INICIO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.FECHA_FIN,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.UBICACION,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.CARGO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.USER_REGISTRO,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.IDE_FICHA,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.OBSERVACIONES,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.CORREO_JEFE,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(obj.ID_EMPRESA,tgSQLFieldType.NUMERIC),


            };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_FORMATIVO_FASE", Parametros));

        }
        public DataTable uspSEL_RRHH_FORMATIVO_FASE_POR_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FASE_POR_ID", id);
        }
        public DataTable uspDEL_RRHH_FORMATIVO_FASE_POR_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspDEL_RRHH_FORMATIVO_FASE_POR_ID", id);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FASE_POR_FICHA(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FASE_POR_FICHA", id);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FASE_CONTROL(int IDE_FASE, int TIPO, int ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FASE_CONTROL", IDE_FASE, TIPO, ESTADO);
        }
        public DataTable uspSEL_RRHH_FORMATIVO_FICHA_DNI(string  dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_FORMATIVO_FICHA_DNI", dni);
        }
    }
}
