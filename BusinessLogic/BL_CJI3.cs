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
    public class BL_CJI3
    {
        public DataTable Listar_anio()
        {
            return new DA_CJI3().Listar_ConductoresDA();
        }
        public DataTable Registrar_CJI3(int anio, int mes)
        {
            return new DA_CJI3().Registrar_CJI3(anio,mes );
        }
        public DataTable USP_REGISTRAR_ESTADO_CONTRATO(int anio, int mes)
        {
            return new DA_CJI3().USP_REGISTRAR_ESTADO_CONTRATO(anio, mes);
        }
        public DataTable ListarProyecto_CJI3(int anio, int mes)
        {
            return new DA_CJI3().ListarProyecto_CJI3_DA(anio, mes);
        }
        public DataTable eliminar_periodo(int anio, int mes)
        {
            return new DA_CJI3().eliminar_periodoDA(anio, mes);
        }
        public DataTable ListarTipodeCambio()
        {
            return new DA_CJI3().Get_ListarTipodeCambio();
        }
        public DataTable Registrar_CJI3_TC(int id, decimal tc,int anio, int mes)
        {
            return new DA_CJI3().Registrar_CJI3_TC(id, tc, anio, mes);
        }
        public DataTable Listar_CJI3_TC(int id)
        {
            return new DA_CJI3().Listar_CJI3_TC(id);
        }
        public DataTable uspSEL_CJI3_CENTROS_X_EMPRESA(string IDE_EMPRESA)
        {
            return new DA_CJI3().uspSEL_CJI3_CENTROS_X_EMPRESA(IDE_EMPRESA);
        }

    }
}
