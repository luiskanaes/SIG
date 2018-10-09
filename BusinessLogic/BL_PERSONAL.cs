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
    public class BL_PERSONAL
    {
        public DataTable registroPersonal(
            string idEmpleado,
            string nombre, 
            string fechaNacimiento,
            string fechaIngreso, 
            string telefono,
            string correo,
            string direccion,
            int civil, byte[] imagen)
        {
            return new DA_PERSONAL().registroPersonal_DA(idEmpleado, nombre, fechaNacimiento, fechaIngreso, telefono, correo, direccion, civil, imagen);
        }

        public int Mant_Insert_registroPersonal(BE_PERSONAL oBEPersonal)
        {
            try
            {
                return new DA_PERSONAL().Mant_Insert_registroPersonal_DA(oBEPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarParametros(string descripcion, string tabla)
        {
            return new DA_PERSONAL().ListarParametros_DA(descripcion , tabla);
        }
        public DataTable USP_PARAMETRO_LISTAR_MDP(string descripcion, string tabla)
        {
            return new DA_PERSONAL().USP_PARAMETRO_LISTAR_MDP(descripcion, tabla);
        }
        public DataTable ListarParametros_orden(string descripcion, string tabla)
        {
            return new DA_PERSONAL().ListarParametros_Orden_DA(descripcion, tabla);
        }
        public DataTable BuscarDNI(string Dni)
        {
            return new DA_PERSONAL().BuscarDNI_DA(Dni);
        }

        public DataTable BuscarDNIMOD(string Dni)
        {
            return new DA_PERSONAL().BuscarDNI_MOD_DA(Dni);
        }

        public DataTable BuscarDNI_TC(string Dni)
        {
            return new DA_PERSONAL().BuscarDNI_TC_DA(Dni);
        }

        public DataTable BuscarPersonal(string Personal)
        {
            return new DA_PERSONAL().BuscarPersonal_DA(Personal);
        }
        public DataTable EmpresaProyectos(int empresa, string proyecto)
        {
            return new DA_PERSONAL().EmpresaProyectos_DA(empresa, proyecto);
        }

        
        public DataTable Listar_centro_Costos(string obra)
        {
            return new DA_PERSONAL().Listar_centro_Costos_DA(obra);
        }

         public DataTable Listar_centro_Costos_Correo(string id)
        {
            return new DA_PERSONAL().Listar_centro_Costos_Correo_DA(id);
        }

        public int Mant_Insert_Operarios(BE_MOI oBEPersonal)
        {
            try
            {
                return new DA_PERSONAL().Mant_Insert_Operarios(oBEPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BuscarDNI_MOI(int idPersona)
        {
            return new DA_PERSONAL().BuscarDNI_MOI_DA(idPersona);
        }
        public DataTable Datos_MOI(int codigo)
        {
            return new DA_PERSONAL().Datos_MOI_DA(codigo);
        }
        public DataTable ConsultarControlReporte_MOI(string fecInicio, string fecFin, string danalistas, string dcargos, string dceco, string destados, string dmano, string dfecha)
        {
            return new DA_PERSONAL().ConsultarControlReporte_MOI_DA(fecInicio, fecFin, danalistas, dcargos, dceco, destados, dmano, dfecha);
        }

        public DataTable ConsultarControlReporte_MOD(string fecInicio, string fecFin, string danalistas, string dcargos, string dceco, string destados, string dmano, string dfecha, string despecialidad)
        {
            return new DA_PERSONAL().ConsultarControlReporte_MOD_DA(fecInicio, fecFin, danalistas, dcargos, dceco, destados, dmano, dfecha, despecialidad);
        }

        public DataTable ConsultarControl_MOI(string fecInicio, string fecFin, int estado, string analistas)
        {
            return new DA_PERSONAL().ConsultarControl_MOI_DA(fecInicio, fecFin, estado, analistas);
        }

        public DataTable EstadosMOI_Personal(int codigoMOI)
        {
            return new DA_PERSONAL().EstadosMOI_Personal_DA(codigoMOI);
        }


        //MOD
        public int Mant_Insert_registroPersonalMOD(BE_PERSONAL oBEPersonal)
        {
            try
            {
                return new DA_PERSONAL().Mant_Insert_registroPersonalMOD_DA(oBEPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarDNI_MOD(int idPersona)
        {
            return new DA_PERSONAL().BuscarDNI_MOD_DA(idPersona);
        }


        public DataTable EstadosMOD_Personal(int codigoMOI)
        {
            return new DA_PERSONAL().EstadosMOD_Personal_DA(codigoMOI);
        }


        public DataTable Datos_MOD(int codigo)
        {
            return new DA_PERSONAL().Datos_MOD_DA(codigo);
        }

        public int Mant_Insert_Operarios_Mod(BE_MOD oBEPersonal)
        {
            try
            {
                return new DA_PERSONAL().Mant_Insert_Operarios_Mod(oBEPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ConsultarControl_MOD(string fecInicio, string fecFin, int estado, string analistas)
        {
            return new DA_PERSONAL().ConsultarControl_MOD_DA(fecInicio, fecFin, estado, analistas);
        }

        public DataTable buscar_PersonalDisponible(int ID_REQUERIMIENTO_PERSONAL)
        {
            return new DA_PERSONAL().buscar_PersonalDisponible( ID_REQUERIMIENTO_PERSONAL);

        }

        public DataTable buscar_PersonalDisponible_Cargo(int ID_CARGO, string DES_NOMBRE)
        {
            return new DA_PERSONAL().buscar_PersonalDisponible_Cargo(ID_CARGO, DES_NOMBRE);
        }

        public DataTable buscar_PersonalDisponible_Cargo_MOD(int ID_CARGO, string DES_NOMBRE, string DES_ESPECIALIDAD)
        {
            return new DA_PERSONAL().buscar_PersonalDisponible_Cargo_MOD(ID_CARGO, DES_NOMBRE, DES_ESPECIALIDAD);
        }


        public DataTable buscar_PersonalDisponible_Cargo_MOI(int ID_CARGO, string DES_NOMBRE )
        {
            return new DA_PERSONAL().buscar_PersonalDisponible_Cargo_MOI(ID_CARGO, DES_NOMBRE );
        }

        public DataTable buscar_PersonalDisponible_MOD(int ID_REQUERIMIENTO_PERSONAL)
        {
            return new DA_PERSONAL().buscar_PersonalDisponible_MOD(ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_PersonalDisponible_MOI(int ID_REQUERIMIENTO_PERSONAL)
        {
            return new DA_PERSONAL().buscar_PersonalDisponible_MOI(ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_CantidadRequerimientos_MOD(int ID_REQUERIMIENTO_PERSONAL)
        {
            return new DA_PERSONAL().buscar_CantidadRequerimientos_MOD(ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_CantidadRequerimientos_MOI(int ID_REQUERIMIENTO_PERSONAL)
        {
            return new DA_PERSONAL().buscar_CantidadRequerimientos_MOI(ID_REQUERIMIENTO_PERSONAL);
        }
         
        public int Mant_Insert_OperariosNuevosMoi(string requerimiento, string dni, string responsable)
        {
            try
            {
                return new DA_PERSONAL().Mant_Insert_OperariosNuevosMoi(requerimiento, dni, responsable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BuscarDNIMOI(string Dni)
        {
            return new DA_PERSONAL().BuscarDNIMOI_DA(Dni);
        }

        public DataTable Anular_Moi(int codigoMOI)
        {
            return new DA_PERSONAL().Anular_Moi_DA(codigoMOI);
        }

        public DataTable Anular_Mod(int codigoMOD)
        {
            return new DA_PERSONAL().Anular_Mod_DA(codigoMOD);
        }

        public DataTable BuscarDNI_MOD(string Dni)
        {
            return new DA_PERSONAL().BuscarDNI_MOD_DA(Dni);
        }
         
        public DataTable Mant_Bus_Det_Req_Mod(string requerimiento)
        {
            try
            {
                return new DA_PERSONAL().Mant_Bus_Det_Req_Mod(requerimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable Mant_Bus_Det_Req_Moi(string requerimiento)
        {
            try
            {
                return new DA_PERSONAL().Mant_Bus_Det_Req_Moi(requerimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Mant_Bus_Det_Req_Per_Mod(string requerimiento)
        {
            try
            {
                return new DA_PERSONAL().Mant_Bus_Det_Req_Per_Mod(requerimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Mant_Bus_Det_Req_Per_Moi(string requerimiento)
        {
            try
            {
                return new DA_PERSONAL().Mant_Bus_Det_Req_Per_Moi(requerimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Mant_Insert_OperariosNuevosMod(string requerimiento, string dni)
        {
            try
            {
                return new DA_PERSONAL().Mant_Insert_OperariosNuevosMod(requerimiento, dni);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarManoObraID(string dni)
        {
            return new DA_PERSONAL().ListarManoObraID_DA(dni);
        }

        public DataTable uspSEL_RRHH_PERSONAL_GERENCIA(string IP_CENTRO)
        {
            return new DA_PERSONAL().uspSEL_RRHH_PERSONAL_GERENCIA(IP_CENTRO);
        }
        public DataTable USP_PARAMETRO_LISTAR_IDE(int ID_PARAMETRO)
        {
            return new DA_PERSONAL().USP_PARAMETRO_LISTAR_IDE(ID_PARAMETRO);
        }
        public DataTable uspCONSULTAR_CECOS_GERENCIA(string  IDE_CENTRO)
        {
            return new DA_PERSONAL().uspCONSULTAR_CECOS_GERENCIA(IDE_CENTRO);
        }
        public DataTable ListarOrigen(string codigo, int tipo)
        {
            try
            {
                return new DA_PERSONAL().Get_ListarOrigen(codigo, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
