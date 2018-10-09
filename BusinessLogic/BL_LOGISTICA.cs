using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using UserCode;
using System.Web;

namespace BusinessLogic
{
    public class BL_LOGISTICA
    {
        public DataTable Eliminar_Tabla_ReporteGeneral()
        {
            return new DA_LOGISTICA().Eliminar_Tabla_ReporteGeneral_DA();
        }
        public DataTable Eliminar_CargaIndicadoresRendimiento()
        {
            return new DA_LOGISTICA().Eliminar_CargaIndicadoresRendimiento_DA();
        }
        public DataTable ListarProcesao_ReporteGlobal(string  empresa)
        {
            return new DA_LOGISTICA().ListarProcesao_ReporteGlobal_DA(empresa);
        }
        public DataTable ListarReporte_ReporteGlobal(string empresa, string proyectos)
        {
            return new DA_LOGISTICA().ListarReporte_ReporteGlobal_DA(empresa,proyectos );
        }
        
        public DataTable Listar_Proyectos_ReporteGlobal(string empresa)
        {
            return new DA_LOGISTICA().Listar_Proyectos_ReporteGlobal_DA( empresa);
        }
        public DataTable Listar_Status_ReporteGlobal(string empresa, string proyecto)
        {
            return new DA_LOGISTICA().Listar_Status_ReporteGlobal_DA(empresa, proyecto);
        }
        public DataTable Listar_Compradores(string empresa, string proyecto)
        {
            return new DA_LOGISTICA().Listar_Compradores_DA(empresa, proyecto);
        }
        public DataTable Listar_Empresa_ReporteGlobal()
        {
            return new DA_LOGISTICA().Listar_Empresa_ReporteGlobal_DA();
        }
        public DataTable ListarSoped_ReporteGlobal(string empresa,string status, string proyecto)
        {
            return new DA_LOGISTICA().ListarSoped_ReporteGlobal_DA(empresa,status,proyecto );
        }
        public DataTable indicadores_registro(string usuario)
        {
            return new DA_LOGISTICA().indicadores_registro_DA(usuario );
        }
        public DataTable ListarReporteGlobal(string empresa, string proyecto, string comprador, string tipo)
        {
            return new DA_LOGISTICA().ListarReporteGlobal_DA(empresa, proyecto, comprador, tipo);
        }
        public DataTable ProcesarRegistros_Almacen(string empresa)
        {
            return new DA_LOGISTICA().Get_ProcesarRegistros_Almacen(empresa);
        }
        
    }
}
