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
    public class BL_ASIGNACION_RECURSOS
    {
        public DataTable Listar_datosRequerimiento(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Listar_datosRequerimiento(p_id);
        }
        public int Mant_Insert_Asignacion(BE_ASIGNACION_RECURSOS oBE)
        {
            try
            {
                return new DA_ASIGNACION_RECURSOS().Mant_Insert_Asignacion(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Listar_CabeceraAsignacion(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Listar_CabeceraAsignacion(p_id);
        }
        public DataTable Insertar_AsignacionDetalle(int p_id,string recursos, string recursos_negados, string user)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Insertar_AsignacionDetalle(p_id, recursos, recursos_negados, user);
        }
        public DataTable Listar_recursosAsignados(int p_id, string descripcion, string tabla)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Listar_recursosAsignados(p_id, descripcion, tabla);
        }
        public DataTable Enviar_care_Correo(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Enviar_care_Correo(p_id);
        }
        public DataTable Enviar_Anulacion_Correo(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Enviar_Anulacion_Correo(p_id);
        }
        public DataTable Datos_requerimiento_Ticket(string   p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Datos_requerimiento_Ticket(p_id);
        }
        public DataTable Datos_requerimiento_encargados(int cod, string usuario)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Datos_requerimiento_encargados(cod, usuario);
        }
        public DataTable LIST_ASIGNACION_DETALLE_POR_ID(int cod)
        {
            return new DA_ASIGNACION_RECURSOS().Get_ASIGNACION_DETALLE_POR_ID(cod);
        }
        public DataTable UPD_ASIGNACION_ESTADO(int p_id, string usuario, int estado)
        {
            return new DA_ASIGNACION_RECURSOS().Get_UPD_ASIGNACION_ESTADO(p_id, usuario, estado);
        }
        public DataTable Datos_Controlrequerimiento_id(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Datos_Controlrequerimiento_id(p_id);
        }
        public DataTable Eliminar_Requerimiento_care(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Eliminar_Requerimiento_care(p_id);
        }
        public DataTable Eliminar_Requerimiento_cgo(int p_id)
        {
            return new DA_ASIGNACION_RECURSOS().Eliminar_Requerimiento_cgo(p_id);
        }
        public DataTable Datos_requerimiento_encargadosTotal(string usuario, string estado)
        {
            return new DA_ASIGNACION_RECURSOS().Get_Datos_requerimiento_encargadosTotal(usuario, estado);
        }
    }
}
