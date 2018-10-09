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
    public  class BL_RO
    {
        public DataTable previsto_Tipo()
        {
            return new DA_RO().previsto_TipoDA();
        }
        public DataTable listar_empresa()
        {
            return new DA_RO().listar_empresaDA();
        }
        public DataTable previsto_ListaR(string tipoPrevisto)
        {
            return new DA_RO().previsto_ListarDA(tipoPrevisto);
        }
        public DataTable Listar_PEP(int indicador)
        {
            return new DA_RO().Listar_PEP_DA(indicador);
        }
        public DataTable actualizar_Indicador_PEP(int indicadorPep, int previsto, string descripcion)
        {
            return new DA_RO().actualizar_Indicador_PEP_DA(indicadorPep, previsto, descripcion);
        }
        public DataTable registro_Proyectos(
            string cod_proyecto,
            string proyecto,
            string contrato,
            string moneda,
            string cliente,
            string fechaInicio,
            string fechaFin,
            decimal tipocambio,
            string fechaContractual,
            string centroGasto,
            string usuario,
            int empresa,
            string estado,
            decimal monto,
            decimal montoContractual
            )
        {
            return new DA_RO().registro_Proyectos_DA(
            cod_proyecto,
            proyecto,
            contrato,
            moneda,
            cliente,
            fechaInicio,
            fechaFin,
            tipocambio,
            fechaContractual,
            centroGasto,
            usuario,
            empresa,
            estado,
            monto, montoContractual);
        }
        public DataTable ListarProyectos_RO(int empresa)
        {
            return new DA_RO().ListarProyectos_RODA(empresa);
        }
        public DataTable ListarProyectos_Estados_RO(int empresa, string estado)
        {
            return new DA_RO().ListarProyectos_Estados_RODA(empresa, estado);
        }
        public DataTable ListarProyecto_Individual_RO(string  proyecto)
        {
            return new DA_RO().ListarProyecto_Individual_RODA(proyecto);
        }
        public DataTable Eliminar_Proyectos(string proyecto)
        {
            return new DA_RO().Eliminar_Proyectos_DA(proyecto);
        }
        public DataTable registro_resultado_Ro(string proyecto, int anio, int mes, decimal valor, int previsto, string pep, string usuario,decimal proyeccion, decimal inicio)
        {
            return new DA_RO().registro_resultado_RoDA(proyecto, anio, mes, valor, previsto, pep, usuario,proyeccion ,inicio );
        }
        public DataTable monto_resultado_Ro(string proyecto, int anio, int mes, int previsto, string pep)
        {
            return new DA_RO().monto_resultado_RODA(proyecto, anio, mes, previsto, pep);
        }
    }
}
