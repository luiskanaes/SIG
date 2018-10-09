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
    public class DA_SOL_ANDAMIOS
    {
        Util oUtilitarios = new Util();
        public int uspINS_SOL_ANDAMIOS(BE_SOL_ANDAMIOS oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_ANDAMIOS  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ANDAMIOS  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_USUARIO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SOLICITANTE ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FILE_ANDAMIOS ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FILE_RUTA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.COMENTARIOS ,tgSQLFieldType.TEXT ),

                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ESPECIALIDAD   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.AREA   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SOLICITUD   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_REQUERIDA  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IPCENTRO  ,tgSQLFieldType.TEXT ),
                                        

                                        
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_SOL_ANDAMIOS", Parametros));
        }
        public DataTable uspSEL_SOL_ANDAMIOS_USUARIO(string IDE_USUARIO, string ESTADO, string ANIO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_SOL_ANDAMIOS_USUARIO", IDE_USUARIO, ESTADO, ANIO);
        }
        public DataTable SP_ENVIARCORREO_SOL_ANDAMIOS(int p, string IDE_USUARIO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_ENVIARCORREO_SOL_ANDAMIOS", p, IDE_USUARIO);
        }
        public DataTable uspSEL_SOL_ANDAMIOS_BANDEJA(string IDE_USUARIO, string ESTADO, string ANIO, string ticket, string campo, string direccion, string centro, string area, string especialidad)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_SOL_ANDAMIOS_BANDEJA", IDE_USUARIO, ESTADO, ANIO, ticket, campo, direccion, centro,area, especialidad);
        }
        public DataTable uspUPD_SOL_ANDAMIOS_PROCESAR(int id, string codigo, string sustento_rechazo, string usuario)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_SOL_ANDAMIOS_PROCESAR", id, codigo, sustento_rechazo, usuario);
        }
        public DataTable SP_ENVIARCORREO_SOL_ANDAMIOS_RPTA(int IDE_ANDAMIOS)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_ENVIARCORREO_SOL_ANDAMIOS_RPTA", IDE_ANDAMIOS);
        }
        public int uspUPD_SOL_ANDAMIOS(BE_SOL_ANDAMIOS oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_ANDAMIOS  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USUARIO_ATENCION  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SUPERVIDOR_DNI  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SUPERVIDOR ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CAPATAZ_DNI ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CAPATAZ ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DURACION ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.HORAS ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_ENTREGA  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_TERMINO  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA_DESMONTAJE  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.OBSERVACIONES   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ESTADO   ,tgSQLFieldType.TEXT ),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspUPD_SOL_ANDAMIOS", Parametros));
        }
        public DataTable uspSEL_SOL_ANDAMIOS_IDE(int IDE_ANDAMIOS)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_SOL_ANDAMIOS_IDE", IDE_ANDAMIOS);
        }
        public DataTable uspSEL_SOL_ANDAMIOS_PRIORIDAD(int PRIORIDAD,  int IDE_ANDAMIOS)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_SOL_ANDAMIOS_PRIORIDAD", PRIORIDAD, IDE_ANDAMIOS);
        }
    }
}
