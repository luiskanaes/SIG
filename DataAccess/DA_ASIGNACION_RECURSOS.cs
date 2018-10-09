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
    public class DA_ASIGNACION_RECURSOS
    {
        Util oUtilitarios = new Util();
        public DataTable Get_Listar_datosRequerimiento(int P_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REQUERIMIENTO_PERSONA_CARE", P_id);
        }
        public int Mant_Insert_Asignacion(BE_ASIGNACION_RECURSOS oBE)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_ASIGNACION ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.ID_DETALLE_REQUERIMIENTO_PERSONAL ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.DES_OBSERVACIONES ,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.FLG_ESTADO ,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBE.IDE_UBICACION,tgSQLFieldType.NUMERIC),
                                       
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("uspINS_ASIGNACION_RECURSOS", Parametros));
        }
        public DataTable Get_Listar_CabeceraAsignacion(int p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_ASIGNACION_RECURSOS_POR_ID_DETALLE", p_id);
        }
        public DataTable Get_Insertar_AsignacionDetalle(int p_id, string recursos, string recursos_negados, string user )
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspINS_ASIGNACION_DETALLE", p_id, recursos, recursos_negados, user);
        }
        public DataTable Get_Listar_recursosAsignados(int p_id, string descripcion, string tabla)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_LISTAR_RECURSOS_ASIGNADOS", p_id, descripcion, tabla);
        }
        public DataTable Get_Enviar_care_Correo(int p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_ENVIAR_RECURSOS_ASIGNADOS", p_id);
        }
        public DataTable Get_Enviar_Anulacion_Correo(int p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_ENVIAR_RECURSOS_ANULAR", p_id);
        }
        public DataTable Get_Datos_requerimiento_Ticket(string p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_DATOS_REQUERIMIENTO_TICKET", p_id);
        }
        public DataTable Get_Datos_requerimiento_encargados(int cod, string usuario)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_DATOS_RECURSOS_ENCARGADOS", cod, usuario);
        }
        public DataTable Get_ASIGNACION_DETALLE_POR_ID(int cod)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_ASIGNACION_DETALLE_POR_ID", cod);
        }
        public DataTable Get_UPD_ASIGNACION_ESTADO(int p_id, string usuario, int  estado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspUPD_ASIGNACION_DETALLE_ESTADO", p_id, usuario, estado);
        }
     
        public DataTable Get_Datos_Controlrequerimiento_id(int p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_LISTAR_CONTROL_RECURSOS", p_id);
        }
        public DataTable Eliminar_Requerimiento_care(int p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspMODIFICAR_RECURSOS_ASIGNADOS_CARE", p_id);
        }
        public DataTable Eliminar_Requerimiento_cgo(int p_id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspMODIFICAR_RECURSOS_ASIGNADOS_CGO", p_id);
        }
        public DataTable Get_Datos_requerimiento_encargadosTotal(string usuario, string estado)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_DATOS_RECURSOS_CGO", usuario, estado);
        }
    }
}
