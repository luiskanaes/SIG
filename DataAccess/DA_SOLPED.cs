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
    public class DA_SOLPED
    {
        Util oUtilitarios = new Util();
        public int uspINS_SOLPED(BE_SOLPED oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_SOLPED ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FECHA  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_USUARIO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FILE_SOLPED ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FILE_RUTA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.COMENTARIOS ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IPCENTRO  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CODIGO_SI  ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SOLICITANTE   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.TIPO_SOLICITUD   ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_EMPRESA   ,tgSQLFieldType.TEXT ),
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_SOLPED", Parametros));
        }
        public DataTable uspSEL_SOLPED_BANDEJA(string IDE_USUARIO, string ESTADO, string ANIO, string txtFecSol_F, string txtNOM_CREADO_F, string txtNOM_SOLICITA_F, string txtTICKET_F, string centro, string tipo,string IDE_EMPRESA)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_SOLPED_BANDEJA", IDE_USUARIO, ESTADO, ANIO, txtFecSol_F, txtNOM_CREADO_F, txtNOM_SOLICITA_F, txtTICKET_F, centro, tipo, IDE_EMPRESA);
        }
        public DataTable uspSEL_SOLPED_USUARIO(string IDE_USUARIO, string ESTADO, string ANIO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_SOLPED_USUARIO", IDE_USUARIO, ESTADO, ANIO);
        }
        public DataTable uspUPD_SOLPED_PROCESAR(int id, string codigo, string sustento_rechazo, string usuario, string solped, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_SOLPED_PROCESAR", id, codigo, sustento_rechazo, usuario,solped, tipo );
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS(string usuario, string tipo, string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RESPONSABLE_PROCESOS", usuario, tipo, empresa);
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_EMPRESA(string usuario, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RESPONSABLE_PROCESOS_EMPRESA", usuario, tipo);
        }
        public DataTable SP_ENVIARCORREO_SOLPED(int IDE_SOLPED, string IDE_USUARIO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_ENVIARCORREO_SOLPED", IDE_SOLPED, IDE_USUARIO);
        }
        public DataTable SP_ENVIARCORREO_SOLPED_RPTA(int IDE_SOLPED)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_ENVIARCORREO_SOLPED_RPTA", IDE_SOLPED);
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_CENTRO(string usuario, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RESPONSABLE_PROCESOS_CENTRO", usuario, tipo);
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_SOLPED(string usuario, string tipo, string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RESPONSABLE_PROCESOS_SOLPED", usuario, tipo, empresa);
        }
    }
}
