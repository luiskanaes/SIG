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
  public  class DA_MOBILE
    {
        UtilMobile oUtilitarios = new UtilMobile();
        public DataTable usp_CARE_InsertarSolicitud_Generico(string FechaSolicitud, string IdEmpresaPK,string FechaRequerida, string LugarEntrega, string NombreSolicitante, string UsuarioCreacion, string CODIGO_CARE,string CODIGO_EQUIPO, string CODIGO_CARE_PADRE, string FILE_URL, string TICKET,string IdEstadoRequerimiento )
        {
            return oUtilitarios.EjecutaDatatable("usp_CARE_InsertarSolicitud_Generico", FechaSolicitud, IdEmpresaPK, FechaRequerida, LugarEntrega, NombreSolicitante, UsuarioCreacion,CODIGO_CARE, CODIGO_EQUIPO, CODIGO_CARE_PADRE, FILE_URL, TICKET, IdEstadoRequerimiento);
        }
        public DataTable usp_CARE_InsertarSolicitud_Generico_Aprobador(string FechaSolicitud, string IdEmpresaPK, string FechaRequerida, string LugarEntrega, string NombreSolicitante, string UsuarioCreacion, string CODIGO_CARE, string CODIGO_EQUIPO, string CODIGO_CARE_PADRE, string FILE_URL, string TICKET, string IdEstadoRequerimiento, string aprobador)
        {
            return oUtilitarios.EjecutaDatatable("usp_CARE_InsertarSolicitud_Generico_Aprobador", FechaSolicitud, IdEmpresaPK, FechaRequerida, LugarEntrega, NombreSolicitante, UsuarioCreacion, CODIGO_CARE, CODIGO_EQUIPO, CODIGO_CARE_PADRE, FILE_URL, TICKET, IdEstadoRequerimiento, aprobador);
        }
        public DataTable usp_CARE_ActulizaSolicitud_Generico(string CodigoRef, string NumeroDocumento, string IdEmpresaPK, string CODIGO_CARE)
        {
            return oUtilitarios.EjecutaDatatable("usp_CARE_ActulizaSolicitud_Generico", CodigoRef, NumeroDocumento, IdEmpresaPK, CODIGO_CARE);
        }
        public DataTable usp_CARE_AnulaSolicitud(string CODIGO_CARE, string IdEmpresaPK)
        {
            return oUtilitarios.EjecutaDatatable("usp_CARE_AnulaSolicitud", CODIGO_CARE, IdEmpresaPK);
        }
        public DataTable usp_CARE_ReasignacionEquipo(string IdEmpresaPK, string CodigoRef, string NumeroDocumento, string NumeroLinea,string CODIGO_CARE)
        {
            return oUtilitarios.EjecutaDatatable("usp_CARE_ReasignacionEquipo", IdEmpresaPK, CodigoRef, NumeroDocumento, NumeroLinea, CODIGO_CARE);
        }
        public DataTable usp_CARE_DevolucionEquipo(string IdEmpresaPK, string CodigoRef, string NumeroDocumento, string NumeroLinea, string CODIGO_CARE)
        {
            return oUtilitarios.EjecutaDatatable("usp_CARE_DevolucionEquipo", IdEmpresaPK, CodigoRef, NumeroDocumento, NumeroLinea, CODIGO_CARE);
        }
        public DataTable usp_Trabajador_x_dni(string dni, string ape_paterno, string ape_materno, string nombres)
        {
            return oUtilitarios.EjecutaDatatable("usp_Trabajador_x_dni", dni, ape_paterno, ape_materno, nombres);
        }
        public DataTable uspANULAR_RRHH_SOLICITUD_ASIGNACION(string Codigo_sol_sigcare)
        {
            return oUtilitarios.EjecutaDatatable("uspANULAR_RRHH_SOLICITUD_ASIGNACION", Codigo_sol_sigcare);
        }
        public DataTable usp_NombreTrabajador_reqMovil(string codigoPersona, string nombres, string codigo, string FILE_URL)
        {
            return oUtilitarios.EjecutaDatatable("usp_NombreTrabajador_reqMovil", codigoPersona, nombres, codigo, FILE_URL);
        }
        public DataTable usp_RequerimientoListado_codigoCare(string Codigo_sol_sigcare)
        {
            return oUtilitarios.EjecutaDatatable("usp_RequerimientoListado_codigoCare", Codigo_sol_sigcare);
        }
    }
}
