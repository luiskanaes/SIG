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
    public class DA_PERSONAL
    {
        Util oUtilitarios = new Util();
        public DataTable registroPersonal_DA(
            string idEmpleado,
            string nombre,
            string fechaNacimiento,
            string fechaIngreso,
            string telefono,
            string correo,
            string direccion,
            int civil, byte[] imagen)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_REGISTRAR_PERSONAL_RRHH", idEmpleado, nombre, fechaNacimiento, fechaIngreso, telefono, correo, direccion,civil ,imagen );
        }
        public DataTable Get_ListarOrigen(string codigo, int tipo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_UBIGEO_POR_ORIGEN", codigo, tipo);
        }
        public DataTable ListarParametros_DA(string descripcion, string tabla)
        { 
        return oUtilitarios.EjecutaDatatable("dbo.USP_PARAMETRO_LISTAR",descripcion ,tabla );
        }
        public DataTable USP_PARAMETRO_LISTAR_MDP(string descripcion, string tabla)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PARAMETRO_LISTAR_MDP", descripcion, tabla);
        }
        public DataTable ListarParametros_Orden_DA(string descripcion, string tabla)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PARAMETRO_LISTAR_ORDEN", descripcion, tabla);
        }
        public DataTable BuscarDNI_DA(string Dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_BUSCAR_DNI", Dni);
        }

        public DataTable BuscarDNI_MOD_DA(string Dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_BUSCAR_DNI_MOD", Dni);
        }

        public DataTable BuscarDNI_TC_DA(string Dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_BUSCAR_DNI_TC", Dni);
        }

        public DataTable BuscarPersonal_DA(string personal)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_BUSCAR_NOMBRE", personal);
        }
        public DataTable EmpresaProyectos_DA(int empresa, string proyecto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_EMPRESA_PROYECTOS", empresa, proyecto);
        }

        
        public DataTable Listar_centro_Costos_DA(string obra)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_OBRAS_CENTRO_COSTOS", obra);
        }

        public DataTable Listar_centro_Costos_Correo_DA(string id)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_OBRAS_CENTRO_COSTOS_ID", id);
        }

        public int Mant_Insert_Operarios(BE_MOI oBEPersonal)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_ID_MOI_E ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_FLG_ESTADO_E ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_ID_EMPRESA_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_IDOBRA_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_DES_UBICACION,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_ID_CENTROCOSTO_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_AREA_E,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_CARGO_E,tgSQLFieldType.NUMERIC ),
                                        
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_FUENTE_E,tgSQLFieldType.NUMERIC),

                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_TIPO_PROCESO_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_FUENTE_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_ORIGEN_POSICION_E,tgSQLFieldType.NUMERIC  ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_RESPONSABLE_E,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_REQUERIMIENTO_E,tgSQLFieldType.TEXT  ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_ITEM_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_APROBACION_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_IDE_EMPLEADO_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_VIAJE_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_EXAMEN_MED_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_PRIMERENVIO_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_FEEDBACK_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_SALIDA_EMPRESA_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_COMENTARIOS_E,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_SALIDA_EMPRESA_E,tgSQLFieldType.NUMERIC    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_INGRESO_E,tgSQLFieldType.TEXT    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_NRO_PROCESO_E,tgSQLFieldType.NUMERIC    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_ATENCION_E,tgSQLFieldType.TEXT    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_ID_DETALLE_REQUERIMIENTO_PERSONAL_E,tgSQLFieldType.NUMERIC    ),

                                       
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_REGISTRO_CONTROL_MOI", Parametros));
        }
        public DataTable BuscarDNI_MOI_DA(int idPersona)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOI_BUSQUEDA_PERSONAL", idPersona);
        }
        public DataTable Datos_MOI_DA(int codigo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOI_DATOS_PERSONAL", codigo);
        }
        public DataTable ConsultarControl_MOI_DA(string  fecInicio, string  fecFin, int estado, string analistas)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOI_CONSULTAS", fecInicio, fecFin, estado, analistas);
        }

        public DataTable ConsultarControlReporte_MOI_DA(string fecInicio, string fecFin, string danalistas, string dcargos, string dceco, string destados, string dmano, string dfecha)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOI_CONSULTAS_REPORTE", fecInicio, fecFin, danalistas, dcargos, dceco, destados,dmano,dfecha);
        }


        public DataTable ConsultarControlReporte_MOD_DA(string fecInicio, string fecFin, string danalistas, string dcargos, string dceco, string destados, string dmano, string dfecha, string despecialidad)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOD_CONSULTAS_REPORTE", fecInicio, fecFin, danalistas, dcargos, dceco, destados, dmano, dfecha, despecialidad);
        }

        public int Mant_Insert_registroPersonal_DA(BE_PERSONAL oBEPersonal)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_EMPLEADO_E ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_NOMBRE_E ,tgSQLFieldType.TEXT ),

                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_APEPAT_E ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_APEMAT_E ,tgSQLFieldType.TEXT ),


                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_NACIMIENTO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_INGRESO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_TELEFONO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_CORREO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_DIRECCION_E,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_DES_ESTADO_CIVIL_E,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.b_foto_e,tgSQLFieldType.Binary),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_DES_TIPO_DOCUMENTO_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_DNI_E,tgSQLFieldType.TEXT),   
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_USUARIO_CREACION_E,tgSQLFieldType.TEXT), 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_CARGO_E,tgSQLFieldType.NUMERIC), 
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_REGISTRAR_PERSONAL_RRHH", Parametros));
        }
        public DataTable EstadosMOI_Personal_DA(int codigoMOI)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_MOI_ESTADOS_PERSONAL", codigoMOI);
        }




        //MOD

        public int Mant_Insert_registroPersonalMOD_DA(BE_PERSONAL oBEPersonal)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_EMPLEADO_E ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_NOMBRE_E ,tgSQLFieldType.TEXT ),

                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_APEPAT_E ,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_APEMAT_E ,tgSQLFieldType.TEXT ), 

                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_NACIMIENTO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_INGRESO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_TELEFONO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_CORREO_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_DIRECCION_E,tgSQLFieldType.TEXT ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_DES_ESTADO_CIVIL_E,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.b_foto_e,tgSQLFieldType.Binary),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_DES_TIPO_DOCUMENTO_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_DNI_E,tgSQLFieldType.TEXT),   
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_USUARIO_CREACION_E,tgSQLFieldType.TEXT), 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_CARGO_E,tgSQLFieldType.NUMERIC), 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_ESPECIALIDAD_E,tgSQLFieldType.NUMERIC), 
           
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_REGISTRAR_PERSONAL_RRHH_MOD", Parametros));
        }


        public DataTable BuscarDNI_MOD_DA(int idPersona)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOD_BUSQUEDA_PERSONAL", idPersona);
        }

        public DataTable EstadosMOD_Personal_DA(int codigoMOI)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_MOD_ESTADOS_PERSONAL", codigoMOI);
        }

        public DataTable Datos_MOD_DA(int codigo)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOD_DATOS_PERSONAL", codigo);
        }


        public int Mant_Insert_Operarios_Mod(BE_MOD oBEPersonal)
        {
            object[] Parametros = new[] { 
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_ID_MOD_E ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_FLG_ESTADO_E ,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_ID_EMPRESA_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_IDOBRA_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_DES_UBICACION,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_ID_CENTROCOSTO_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_AREA_E,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_CARGO_E,tgSQLFieldType.NUMERIC ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_FUENTE_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_TIPO_PROCESO_E,tgSQLFieldType.NUMERIC),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_FUENTE_E,tgSQLFieldType.TEXT),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_ORIGEN_POSICION_E,tgSQLFieldType.NUMERIC  ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_RESPONSABLE_E,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_REQUERIMIENTO_E,tgSQLFieldType.TEXT  ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_ITEM_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_APROBACION_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_IDE_EMPLEADO_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_VIAJE_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_EXAMEN_MED_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_PRIMERENVIO_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_FECHA_FEEDBACK_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_SALIDA_EMPRESA_E ,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_DES_COMENTARIOS_E,tgSQLFieldType.TEXT   ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_SALIDA_EMPRESA_E,tgSQLFieldType.NUMERIC    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_INGRESO_E,tgSQLFieldType.TEXT    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.i_IDE_NRO_PROCESO_E,tgSQLFieldType.NUMERIC    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.f_FEC_ATENCION_E,tgSQLFieldType.TEXT    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_ID_DETALLE_REQUERIMIENTO_PERSONAL_E,tgSQLFieldType.NUMERIC    ),
                                        (object)UC_FormWeb.mSQLFieldOrNull(oBEPersonal.v_ID_CMOD_E,tgSQLFieldType.TEXT    ),
                                       
            };

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_REGISTRO_CONTROL_MOD", Parametros));
        }

        public DataTable ConsultarControl_MOD_DA(string fecInicio, string fecFin, int estado, string analistas)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_CONTROL_MOD_CONSULTAS", fecInicio, fecFin, estado, analistas);
        }

        public DataTable buscar_PersonalDisponible(int ID_REQUERIMIENTO_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAL_DISPONIBLE", ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_PersonalDisponible_Cargo(int ID_CARGO, string DES_NOMBRE)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAL_DISPONIBLE_CARGO", ID_CARGO, DES_NOMBRE);
        }

        public DataTable buscar_PersonalDisponible_Cargo_MOD(int ID_CARGO, string DES_NOMBRE, string DES_ESPECIALIDAD)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAL_DISPONIBLE_CARGO_MOD", ID_CARGO, DES_NOMBRE, DES_ESPECIALIDAD);
        }

        public DataTable buscar_PersonalDisponible_Cargo_MOI(int ID_CARGO, string DES_NOMBRE)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAL_DISPONIBLE_CARGO_MOI", ID_CARGO, DES_NOMBRE);
        }

        public DataTable buscar_PersonalDisponible_MOD(int ID_REQUERIMIENTO_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAL_DISPONIBLE_MOD", ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_PersonalDisponible_MOI(int ID_REQUERIMIENTO_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAL_DISPONIBLE_MOI", ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_CantidadRequerimientos_MOD(int ID_REQUERIMIENTO_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_CANTIDAD_REQ_PERSONAS_MOD", ID_REQUERIMIENTO_PERSONAL);
        }

        public DataTable buscar_CantidadRequerimientos_MOI(int ID_REQUERIMIENTO_PERSONAL)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_CANTIDAD_REQ_PERSONAS_MOI", ID_REQUERIMIENTO_PERSONAL);
        }

        public int Mant_Insert_OperariosNuevosMoi(string requerimiento, string dni , string responsable)
        {

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_REGISTRO_CONTROL_NUEVO_REQUERIMIENTO", requerimiento, dni, responsable));
        }

        public DataTable BuscarDNIMOI_DA(string Dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_BUSCAR_DNI_MOI", Dni);
        }

        public DataTable Anular_Moi_DA(int codigoMOI)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_ANULAR_MOI", codigoMOI);
        }

        public DataTable Anular_Mod_DA(int codigoMOD)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_ANULAR_MOD", codigoMOD);
        }

        public DataTable Mant_Bus_Det_Req_Mod(string requerimiento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_DET_REQ_PERSONAS_MOD", requerimiento);
        }

        public DataTable Mant_Bus_Det_Req_Moi(string requerimiento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_DET_REQ_PERSONAS_MOI", requerimiento);
        }

        public DataTable Mant_Bus_Det_Req_Per_Mod(string requerimiento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAS_REQ_MOD", requerimiento);
        }

        public DataTable Mant_Bus_Det_Req_Per_Moi(string requerimiento)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_BUS_PERSONAS_REQ_MOI", requerimiento);
        }

        public int Mant_Insert_OperariosNuevosMod(string requerimiento, string dni)
        {

            return Convert.ToInt32(new Utilitarios().ExecuteScalar("USP_REGISTRO_CONTROL_NUEVO_REQUERIMIENTO_MOD", requerimiento, dni));
        }

        public DataTable BuscarDNIMOD_DA(string Dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PERSONAL_BUSCAR_DNI_MOD", Dni);
        }

        public DataTable ListarManoObraID_DA(string Dni)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_TIP_MANO_ID", Dni);
        }
        public DataTable uspSEL_RRHH_PERSONAL_GERENCIA(string IP_CENTRO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_GERENCIA", IP_CENTRO);
        }
        public DataTable uspCONSULTAR_CECOS_GERENCIA(string  IDE_CENTRO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspCONSULTAR_CECOS_GERENCIA", IDE_CENTRO);
        }
        public DataTable USP_PARAMETRO_LISTAR_IDE(int ID_PARAMETRO)
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_PARAMETRO_LISTAR_IDE", ID_PARAMETRO);
        }
    }
}
