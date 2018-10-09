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
    public class DA_CARTA_COBRAZAS_DETALLE
    {
        Util oUtilitarios = new Util();
        public int uspUPD_CARTA_COBRAZAS_DETALLE(BE_CARTA_COBRAZAS_DETALLE oBE)
        {
            object[] Parametros = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_DETALLE  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_CARTA  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DOCUMENTO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DETALLE ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CUENTA_COSTO ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CANTIDAD ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.PRECIO  ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.CUENTA_COSTO_ORIGEN ,tgSQLFieldType.TEXT ),


            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspUPD_CARTA_COBRAZAS_DETALLE", Parametros));
        }
        public DataTable uspSEL_CARTA_COBRAZAS_DETALLE(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS_DETALLE", IDE_CARTA);
        }
        public DataTable uspSEL_CARTA_COBRAZAS_DETALLE_ID(int IDE_DETALLE)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CARTA_COBRAZAS_DETALLE_ID", IDE_DETALLE);
        }
        public DataTable uspDEL_CARTA_COBRAZAS_DETALLE_POR_ID(int IDE_DETALLE)
        {
            return oUtilitarios.EjecutaDatatable("uspDEL_CARTA_COBRAZAS_DETALLE_POR_ID", IDE_DETALLE);
        }
        public DataTable uspUPD_CARTA_COBRAZAS_APROBACIONES(int IDE_APROBACION,int TIPO , string SUSTENTO)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_CARTA_COBRAZAS_APROBACIONES", IDE_APROBACION, TIPO, SUSTENTO);
        }
        public DataTable SP_CORREO_APROBACIONES_CARTACOBRANZA(int IDE_CARTA, int TIPO_CARGO, string URLSSK, string ENVIA_CORREO,string APROBADOR, int FLG_APROBAR)
        {
            return oUtilitarios.EjecutaDatatable("SP_CORREO_APROBACIONES_CARTACOBRANZA", IDE_CARTA, TIPO_CARGO, URLSSK, ENVIA_CORREO, APROBADOR, FLG_APROBAR);
        }
        public DataTable SP_CORREO_APROBACIONES_CARTACOBRANZA_SAP(int IDE_CARTA)
        {
            return oUtilitarios.EjecutaDatatable("SP_CORREO_APROBACIONES_CARTACOBRANZA_SAP", IDE_CARTA);
        }
        public DataTable SP_CORREO_APROBACIONES_CARTACOBRANZA_TODOS(int IDE_CARTA, int TIPO_CARGO)
        {
            return oUtilitarios.EjecutaDatatable("SP_CORREO_APROBACIONES_CARTACOBRANZA_TODOS", IDE_CARTA, TIPO_CARGO);
        }
    }
}
