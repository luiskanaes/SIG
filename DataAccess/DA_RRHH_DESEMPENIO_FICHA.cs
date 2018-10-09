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
    public class DA_RRHH_DESEMPENIO_FICHA
    {
        Util oUtilitarios = new Util();
        public int uspINS_RRHH_DESEMPENIO_PERFIL(BE_RRHH_DESEMPENIO_FICHA oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_DESEMPENIO ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_PERFIL ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO_GERENCIA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CCENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FAMILIA ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CARGO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRA ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_RRHH_DESEMPENIO_PERFIL", Parametros));
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_FICHA_PERFIL(int anio, int perfil)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_FICHA_PERFIL", anio, perfil);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_FICHA_ID(int IDE_DESEMPENIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_FICHA_ID", IDE_DESEMPENIO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OPCION_PERFIL(string DNI, string ANIO, int PADRE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OPCION_PERFIL", DNI, ANIO, PADRE);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_OPCIONES(string DNI, string ANIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_OPCIONES", DNI, ANIO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_FICHA_DNI(string DNI, string ANIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_FICHA_DNI", DNI, ANIO);
        }
        public int uspSEL_RRHH_DESEMPENIO_INSERT_VARIOS(BE_RRHH_DESEMPENIO_FICHA oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CCENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO_GERENCIA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IP_CENTRO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_JEFE ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_GERENTE ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRA ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspSEL_RRHH_DESEMPENIO_INSERT_VARIOS", Parametros));
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_COLABORADORES(string DNI, string ANIO, int TIPO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_COLABORADORES", DNI, ANIO, TIPO);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_ADICIONAR(BE_RRHH_DESEMPENIO_FICHA oBE)
        {
            object[] Parametros = new[] {
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI  ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.CCENTRO ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO_GERENCIA ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.IP_CENTRO ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANIO ,tgSQLFieldType.NUMERIC ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_JEFE ,tgSQLFieldType.TEXT),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.DNI_GERENTE ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRA ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_FAMILIA ,tgSQLFieldType.TEXT ),
                                (object)UC_FormWeb.mSQLFieldOrNull(oBE.COMENTARIOS ,tgSQLFieldType.TEXT ),
            };

            return new Utilitarios().EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_ADICIONAR", Parametros);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_PERSONAL_CARGOS(int ANIO, int TIPO, string COD_GERENCIA, string COD_CC)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_PERSONAL_CARGOS", ANIO, TIPO, COD_GERENCIA, COD_CC);
        }
        public DataTable uspSEL_RRHH_DESEMPENIO_PERSONAL_LIBRE(int ANIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_DESEMPENIO_PERSONAL_LIBRE", ANIO);
        }
    }
}
