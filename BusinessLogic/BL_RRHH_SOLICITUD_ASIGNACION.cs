using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using System.Data;

namespace BusinessLogic
{
  public  class BL_RRHH_SOLICITUD_ASIGNACION
    {
        public DataTable uspSEL_RRHH_SOLICITUD_ASIGNACION(string NOMBRE_COMPLETO, string COD_CENTRO, string TICKET)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_RRHH_SOLICITUD_ASIGNACION(NOMBRE_COMPLETO, COD_CENTRO, TICKET);
        }
        public DataTable  uspINS_RRHH_SOLICITUD_ASIGNACION(BE_RRHH_SOLICITUD_ASIGNACION oBESOl)
        {
            try
            {
                return new DA_RRHH_SOLICITUD_ASIGNACION().uspINS_RRHH_SOLICITUD_ASIGNACION(oBESOl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_RRHH_SOLICITUD_ASIGNACION_ID(string IDE_ASIGNACION)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_RRHH_SOLICITUD_ASIGNACION_ID(IDE_ASIGNACION);
        }
        public DataTable uspINS_RRHH_SOLICITUD_RECURSOS(string IDE_ASIGNACION, string IDE_RECURSO,  string USER_REGISTRO)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspINS_RRHH_SOLICITUD_RECURSOS(IDE_ASIGNACION, IDE_RECURSO, USER_REGISTRO);
        }
        public DataTable uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS(string IDE_ASIGNACION, string DES_DESCRIPCION, string DES_TABLA)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS(IDE_ASIGNACION, DES_DESCRIPCION, DES_TABLA);
        }
        public DataTable uspSEL_ENVIAR_RECURSOS_CARE_NUEVO(string IDE_ASIGNACION)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_ENVIAR_RECURSOS_CARE_NUEVO(IDE_ASIGNACION);
        }
        public DataTable uspUPD_CANDIDADTO_RECURSOS_CARE_NUEVO(string IDE_ASIGNACION, string IdTrabajador)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspUPD_CANDIDADTO_RECURSOS_CARE_NUEVO(IDE_ASIGNACION, IdTrabajador);
        }
        public DataTable usp_correo_responsable_recursos(string IDE_ASIGNACION, string TIPO)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().usp_correo_responsable_recursos(IDE_ASIGNACION, TIPO);
        }
        public DataTable uspSEL_LISTAR_RECURSOS_SOLMOBILE(string IDE_ASIGNACION, string DES_DESCRIPCION)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_LISTAR_RECURSOS_SOLMOBILE(IDE_ASIGNACION, DES_DESCRIPCION);
        }
        public DataTable uspINS_RequerimientoMovil(BE_RequerimientoMovil oBESOl)
        {
            try
            {
                return new DA_RRHH_SOLICITUD_ASIGNACION().uspINS_RequerimientoMovil(oBESOl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable uspINS_RequerimientoMovil_Detalle_SIG(BE_RequerimientoMovil_Detalle oBESOl)
        {
            try
            {
                return new DA_RRHH_SOLICITUD_ASIGNACION().uspINS_RequerimientoMovil_Detalle_SIG(oBESOl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_REQUERIMIENTOMOVIL_UPDATE_MOBILE(string IdRequerimiento)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_REQUERIMIENTOMOVIL_UPDATE_MOBILE(IdRequerimiento);
        }
        public DataTable uspANULAR_RRHH_SOLICITUD_ASIGNACION(string IDE_ASIGNACION, string USUARIO)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspANULAR_RRHH_SOLICITUD_ASIGNACION(IDE_ASIGNACION, USUARIO);
        }
        public DataTable uspANULAR_RRHH_SOLICITUD_ASIGNACION_C(string var, string IdRequerimiento_mobile)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspANULAR_RRHH_SOLICITUD_ASIGNACION_C(var, IdRequerimiento_mobile);
        }
        public DataTable usp_correo_notificar_apobrador_asignacion(string IDE_ASIGNACION, string tipo_equipo, int tipo_proceso)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().usp_correo_notificar_apobrador_asignacion(IDE_ASIGNACION, tipo_equipo, tipo_proceso);
        }

        public DataTable usp_correo_responsable_recursos_excepcion(string IDE_ASIGNACION, string TIPO_EQUIPO, string GPO_EQUIPO)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().usp_correo_responsable_recursos_excepcion(IDE_ASIGNACION, TIPO_EQUIPO, GPO_EQUIPO);
        }
        public DataTable uspSEL_APROBAR_RECURSOS_SOLMOBILE(string IDE_ASIGNACION,string IDE_RECURSOS, string USUARIO, string FLG_APROBADO)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_APROBAR_RECURSOS_SOLMOBILE(IDE_ASIGNACION, IDE_RECURSOS, USUARIO, FLG_APROBADO);
        }
        public DataTable uspSEL_LISTAR_RECURSOS_SOLMOBILE_ITEM(string IDE_ASIGNACION, string DES_DESCRIPCION, string IDE_RECURSOS)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspSEL_LISTAR_RECURSOS_SOLMOBILE_ITEM(IDE_ASIGNACION, DES_DESCRIPCION, IDE_RECURSOS);
        }
        public DataTable usp_RRHH_SOLICITUD_RECURSOS(string IDE_ASIGNACION)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().usp_RRHH_SOLICITUD_RECURSOS(IDE_ASIGNACION);
        }
        public DataTable uspUPD_RRHH_SOLICITUD_ASIGNACION_APROBADOR(string IDE_ASIGNACION, string IDE_GERENCIA)
        {
            return new DA_RRHH_SOLICITUD_ASIGNACION().uspUPD_RRHH_SOLICITUD_ASIGNACION_APROBADOR(IDE_ASIGNACION, IDE_GERENCIA);
        }

    }
}
