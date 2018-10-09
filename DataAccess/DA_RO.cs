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
    public class DA_RO
    {
        Util oUtilitarios = new Util();
        public DataTable previsto_TipoDA()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PREVISTO_TIPO");
        }
        public DataTable listar_empresaDA()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_EMPRESAS");
        }
        public DataTable previsto_ListarDA(string tipoPrevisto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PREVISTO_LISTAR", tipoPrevisto);
        }
        public DataTable Listar_PEP_DA(int indicador)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_INDICADORES_PEP", indicador);
        }
        public DataTable actualizar_Indicador_PEP_DA(int indicadorPep, int previsto, string descripcion)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_INDICADORES_PEP_ACTUALIZAR", indicadorPep, previsto, descripcion);
        }
        public DataTable registro_Proyectos_DA(
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
            decimal monto, decimal montoContractual)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PROYECTOS_REGISTRO", 
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
        public DataTable ListarProyectos_RODA(int empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PROYECTOS_LISTAR_RO", empresa);
        }
        public DataTable ListarProyectos_Estados_RODA(int empresa,string estado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PROYECTOS_ESTADOS_LISTAR_RO", empresa, estado );
        }
        public DataTable ListarProyecto_Individual_RODA(string  proyecto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PROYECTOS_INDIVIDUAL_RO", proyecto);
        }
        public DataTable Eliminar_Proyectos_DA(string  proyecto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PROYECTOS_ELIMINAR_RO", proyecto);
        }
        public DataTable registro_resultado_RoDA(string proyecto, int anio, int mes, decimal valor, int previsto, string pep, string usuario, decimal proyeccion,decimal inicio)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REGISTRAR_RESULTADO_RO", proyecto, anio, mes, valor, previsto, pep, usuario,  proyeccion, inicio );
        }
        public DataTable monto_resultado_RODA(string proyecto, int anio, int mes, int previsto,string  pep)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_MOSTRAR_MONTOS_RO", proyecto, anio, mes, previsto, pep);
        }
    }
}
