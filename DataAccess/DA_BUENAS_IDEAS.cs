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
    public class DA_BUENAS_IDEAS
    {
        Util oUtilitarios = new Util();
        public int uspINS_BUENAS_IDEAS(BE_BUENAS_IDEAS oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_IDEAS ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DESCRIPCION_PROPUESTA ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.SOLUCION ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.VENTAJAS ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.AREAS ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.USER_REGISTRO ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.TITULO  ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.FILE   ,tgSQLFieldType.TEXT ),
                                         (object)UC_FormWeb.mSQLFieldOrNull(oBE.URL   ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_BUENAS_IDEAS", Parametros));
        }
        public DataTable uspSEL_BUENAS_IDEAS_USER(string DNI_EVALUADOR)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_BUENAS_IDEAS_USER", DNI_EVALUADOR);
        }
        public DataTable uspSEL_BUENAS_IDEAS_TODOS(string FLG_ETAPAS)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_BUENAS_IDEAS_TODOS", FLG_ETAPAS);
        }
        public DataTable uspSEL_BUENA_IDEA_PROCESAR(int id, string codigo, int punto, string sustento_rechazo, string usuario)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_BUENA_IDEA_PROCESAR", id, codigo, punto, sustento_rechazo, usuario);
        }
        public DataTable SP_EnviarCorreo_BuenIdea(string id)
        {
            return oUtilitarios.EjecutaDatatable("SP_EnviarCorreo_BuenIdea", id);
        }
        public DataTable uspSEL_BUENAS_IDEAS_ID(int ID)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_BUENAS_IDEAS_ID", ID);
        }
        
    }
}
