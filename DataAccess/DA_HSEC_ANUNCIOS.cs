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
   public class DA_HSEC_ANUNCIOS
    {
        Util oUtilitarios = new Util();
        public int uspINS_HSEC_ANUNCIOS(BE_HSEC_ANUNCIOS oBESOl)
        {
            object[] Parametros = new[] {
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_ANUNCIO ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ARCHIVO ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ARCHIVO_NOMBRE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ARCHIVO_URL  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ARCHIVO_EXTESION ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.TIPO_ANUNCIO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.COMENTARIOS  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.URL  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ORDEN  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FECHA_INICIO ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FECHA_FIN  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_VISIBLE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.USER_REGISTRO  ,tgSQLFieldType.TEXT),

            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_HSEC_ANUNCIOS", Parametros));
        }
        public DataTable uspSEL_HSEC_ANUNCIOS_POR_TIPO(string TIPO_ANUNCIO, string FLG_VISIBLE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_HSEC_ANUNCIOS_POR_TIPO", TIPO_ANUNCIO, FLG_VISIBLE);
        }
        public DataTable uspSEL_HSEC_ANUNCIOS_BUSCAR(string ANUNCIO, string FLG_VISIBLE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_HSEC_ANUNCIOS_BUSCAR", ANUNCIO, FLG_VISIBLE);
        }
        public DataTable uspSEL_HSEC_ANUNCIOS_POR_ID(string IDE_ANUNCIO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_HSEC_ANUNCIOS_POR_ID", IDE_ANUNCIO);
        }
        public DataTable uspCONSULTAR_POPUP_ANUNCIOS()
        {
            return oUtilitarios.EjecutaDatatable("uspCONSULTAR_POPUP_ANUNCIOS");
        }
        public DataTable uspUPDATE_POPUP_LECTURA(string USUARIO)
        {
            return oUtilitarios.EjecutaDatatable("uspUPDATE_POPUP_LECTURA", USUARIO);
        }

    }
}
