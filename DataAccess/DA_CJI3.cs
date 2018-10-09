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
    public class DA_CJI3
    {
        Util oUtilitarios = new Util();
        public DataTable Listar_ConductoresDA()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_ANIO");
        }
        public DataTable Registrar_CJI3(int  anio, int mes)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REGISTRAR_CJI3", anio, mes);
        }
        public DataTable USP_REGISTRAR_ESTADO_CONTRATO(int anio, int mes)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REGISTRAR_ESTADO_CONTRATO", anio, mes);
        }
        public DataTable ListarProyecto_CJI3_DA(int anio, int mes)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_PROYECTOS_CJI3", anio ,mes);
        }
        public DataTable eliminar_periodoDA(int anio, int mes)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ELIMINAR_CJI3_PERIODO", anio, mes);
        }
        public DataTable Get_ListarTipodeCambio()
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_CJI3_TC");
        }
        public DataTable Registrar_CJI3_TC(int id, decimal tc, int anio, int mes)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspINS_CJI3_TC", id, tc, anio, mes);
        }
        public DataTable Listar_CJI3_TC(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_CJI3_TC_POR_ID", id);
        }
        public DataTable uspSEL_CJI3_CENTROS_X_EMPRESA(string IDE_EMPRESA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_CJI3_CENTROS_X_EMPRESA", IDE_EMPRESA);
        }

    }
}
