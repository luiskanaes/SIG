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
    public class DA_OPE_DETALLE_ACUERDOS
    {
        Util oUtilitarios = new Util();

        public int Mant_Insertar_AcuerdosData(BE_OPE_DETALLE_ACUERDOS objAcuerdos)
        {
            object[] Parametro = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcuerdos.Id_detalle_acuerdo,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcuerdos.Id_temas,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcuerdos.Dsc_descripcion,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcuerdos.Est_estado,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcuerdos.Dsc_observacion,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcuerdos.Usuario,tgSQLFieldType.TEXT),


        };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.USP_INSERT_ACUERDOS", Parametro));
        }

        public DataTable DA_SELECIONAR_DETALLES(int codigoTema)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_DETALLES", codigoTema);
        }
        public DataTable Mant_ModificarTema_DA(int codDetalleAcuerdo, int codigoTema)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_PROGRAMA_DIARIO", codDetalleAcuerdo, codigoTema);
        }
        public DataTable SELECIONAR_ACUERDOS_MINUTA(int codigoMinuta, int filtroFecha,int ordenar)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_MINUTA_ACUERDOS", codigoMinuta, filtroFecha, ordenar);
        }
        public DataTable SELECIONAR_ACUERDOS_CERRADOS(int codigoMinuta, int filtroCerrrado, int ordenar)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_MINUTA_CERRADOS", codigoMinuta, filtroCerrrado, ordenar);
        }

        public DataTable SELECIONAR_MINUTA_BANDEJA_COMPROMISO(string centroCosto,string filtroDatos,int estadoFiltro,string fRequerimiento,string fCierre,string fCompromiso,string treunion,string fActualizado, string codDestino, string responsable)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_MINUTA_BANDEJA_COMPROMISO", centroCosto, filtroDatos, estadoFiltro, fRequerimiento, fCierre, fCompromiso, treunion, fActualizado, codDestino, responsable);
        }

        public DataTable SELECIONAR_ACUERDOS_MINUTA_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_SELECIONAR_MINUTA_ACUERDOS_ID", id);
        }
        public DataTable ELIMINAR_MINUTA_ACUERDOS_ID(int id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_ELIMINAR_MINUTA_ACUERDOS_ID", id);
        }

        public int MANT_USP_AGREGAR_COMENTARIO_DA(BE_OPE_DETALLE_ACUERDOS objAcu)
        {
            object[] Param = new[] {
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcu.Id_temas,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcu.Dsc_descripcion,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcu.Est_estado,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcu.Dsc_observacion,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(objAcu.InicialUsuario,tgSQLFieldType.NUMERIC),


        };
            return Convert.ToInt32(new Utilitarios().ExecuteScalar("dbo.USP_AGREGAR_COMENTARIO", Param));
        }
    }
}
