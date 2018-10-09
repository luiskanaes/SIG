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
    public class DA_LOGISTICA
    {
        Util oUtilitarios = new Util();
        public DataTable Eliminar_Tabla_ReporteGeneral_DA()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ELIMINAR_CARGA_REPORTE_GLOBAL");
        }
        public DataTable Eliminar_CargaIndicadoresRendimiento_DA()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ELIMINAR_CARGA_INDICADORES");
        }
        public DataTable ListarProcesao_ReporteGlobal_DA(string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_PROCESAR", empresa);
        }
        public DataTable ListarReporte_ReporteGlobal_DA(string empresa, string proyectos)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_LISTAR", empresa, proyectos);
        }

        public DataTable Listar_Proyectos_ReporteGlobal_DA(string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_LISTAR_PROYECTOS", empresa);
        }
        public DataTable Listar_Status_ReporteGlobal_DA(string empresa, string proyecto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_STATUS", empresa, proyecto);
        }
        public DataTable Listar_Compradores_DA(string empresa, string proyecto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_COMPRADORES", empresa, proyecto);
        }
        public DataTable Listar_Empresa_ReporteGlobal_DA()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_LISTAR_EMPRESAS");
        }
        public DataTable ListarSoped_ReporteGlobal_DA(string empresa, string status, string proyecto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_SOLPED", empresa, status, proyecto);
        }
        public DataTable indicadores_registro_DA(string usuario)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_INDICADORES_ASIGNAR", usuario);
        }
        public DataTable ListarReporteGlobal_DA(string empresa, string proyecto, string comprador, string tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REPORTE_GLOBAL_LISTAR", empresa, proyecto, comprador, tipo);
        }
        public DataTable Get_ProcesarRegistros_Almacen(string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PROCESAR_REGISTROS_ALMACEN", empresa );
        }
    }
}
