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
   public class DA_RRHH_SOLICITUD_ASIGNACION
    {
        Util oUtilitarios = new Util();
        UtilMobile oUtilMobile = new UtilMobile();
        UtilCareMenor oUtilMenor = new UtilCareMenor();
        public DataTable uspSEL_RRHH_SOLICITUD_ASIGNACION(string NOMBRE_COMPLETO, string COD_CENTRO, string TICKET)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_SOLICITUD_ASIGNACION", NOMBRE_COMPLETO, COD_CENTRO, TICKET);
        }
        public DataTable uspSEL_RRHH_SOLICITUD_ASIGNACION_ID(string IDE_ASIGNACION)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_RRHH_SOLICITUD_ASIGNACION_ID", IDE_ASIGNACION);
        }

        public DataTable uspINS_RRHH_SOLICITUD_ASIGNACION(BE_RRHH_SOLICITUD_ASIGNACION oBESOl)
        {
            object[] Parametros = new[] {
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_ASIGNACION ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_POSTULANTE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.COD_CENTRO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_CARGO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.AREA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.JEFE_DNI  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.UBICACION  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.TIPO_PROCESO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ORIGEN_POSICION  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.TIPO_RECLUT_OBRA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.TIPO_RECLUT_LIMA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.PISO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_NIVEL_ACADEMICO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_CARRERA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.CARRERA_COMENTARIOS  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.NRO_COLEGIATURA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_COLEGIATURA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.NIVEL_EXP_INGLES  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_MAESTRIA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.NIVEL_EXP_SOFTWARE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_SEXO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_ESTADO_CIVIL  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FUNCIONES_PUESTO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.SUELDO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.COMISIONES  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_GRATIFICACIONES  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_PREMIO_OBRA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.INICIO_CONTRATO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.TERMINO_CONTRATO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_VALE_ALIMENTO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_SEGURO_VIDA  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_ASIG_MOVILIDAD  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.OTROS_BENEFICIOS  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.REGIMEN_TRABAJO ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.HORARIO_TRABAJO ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FLG_BONO_DESTAQUE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_PASAJE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.COMENTARIOS_GNRAL  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_SOLICITANTE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_GERENTE  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.USER_REGISTRO  ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IDE_GERENCIA ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.DNI_TMP ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.NOMBRE_TMP ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.APE_PAT_TMP ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.APE_MAT_TMP ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.ID_EMPRESA ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FILE_SOL ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FILE_URL ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FILE_DIR ,tgSQLFieldType.TEXT),

            };

            return new Utilitarios().EjecutaDatatable("uspINS_RRHH_SOLICITUD_ASIGNACION", Parametros);
        }
        public DataTable uspINS_RRHH_SOLICITUD_RECURSOS(string IDE_ASIGNACION, string IDE_RECURSO, string USER_REGISTRO)
        {
            return oUtilitarios.EjecutaDatatable("uspINS_RRHH_SOLICITUD_RECURSOS", IDE_ASIGNACION, IDE_RECURSO  , USER_REGISTRO);
        }
        public DataTable uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS(string IDE_ASIGNACION, string DES_DESCRIPCION, string DES_TABLA)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS", IDE_ASIGNACION, DES_DESCRIPCION, DES_TABLA);
        }
        public DataTable uspSEL_ENVIAR_RECURSOS_CARE_NUEVO(string IDE_ASIGNACION)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_ENVIAR_RECURSOS_CARE_NUEVO", IDE_ASIGNACION);
        }
        public DataTable uspUPD_CANDIDADTO_RECURSOS_CARE_NUEVO(string IDE_ASIGNACION, string IdTrabajador)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_CANDIDADTO_RECURSOS_CARE_NUEVO", IDE_ASIGNACION, IdTrabajador);
        }
        public DataTable usp_correo_responsable_recursos(string IDE_ASIGNACION, string TIPO)
        {
            return oUtilitarios.EjecutaDatatable("usp_correo_responsable_recursos", IDE_ASIGNACION, TIPO);
        }
        public DataTable uspSEL_LISTAR_RECURSOS_SOLMOBILE(string IDE_ASIGNACION, string DES_DESCRIPCION)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_LISTAR_RECURSOS_SOLMOBILE", IDE_ASIGNACION, DES_DESCRIPCION);
        }
        public DataTable uspINS_RequerimientoMovil(BE_RequerimientoMovil oBESOl)
        {
            object[] Parametros = new[] {
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IdRequerimiento ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FechaSolicitud ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IdEmpresaPK ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.centro_costo ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.Requ_Numero ,tgSQLFieldType.TEXT),

            };

            return new UtilCareMenor().EjecutaDatatable("uspINS_RequerimientoMovil", Parametros);
        }

        public DataTable uspINS_RequerimientoMovil_Detalle_SIG(BE_RequerimientoMovil_Detalle oBESOl)
        {
            object[] Parametros = new[] {
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.id_detalle ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.NombreSolicitante ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.FechaRequerida ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.MesesRequerido ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.LugarEntrega ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IdTipoEquipo ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IdRequerimiento ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.Dni_Trabajador ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.cantidad ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.USER_CREACION ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IdTrabajador ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.TipoEquipo ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.IdOperadorMovil ,tgSQLFieldType.TEXT),
                    (object)UC_FormWeb.mSQLFieldOrNull(oBESOl.Operador ,tgSQLFieldType.TEXT),
            };

            return new UtilCareMenor().EjecutaDatatable("uspINS_RequerimientoMovil_Detalle_SIG", Parametros);
        }
        public DataTable uspSEL_REQUERIMIENTOMOVIL_UPDATE_MOBILE(string IdRequerimiento)
        {
            return oUtilMenor.EjecutaDatatable("uspSEL_REQUERIMIENTOMOVIL_UPDATE_MOBILE", IdRequerimiento);
        }
        public DataTable uspANULAR_RRHH_SOLICITUD_ASIGNACION(string IDE_ASIGNACION, string USUARIO)
        {
            return oUtilitarios.EjecutaDatatable("uspANULAR_RRHH_SOLICITUD_ASIGNACION", IDE_ASIGNACION, USUARIO);
        }
        public DataTable uspANULAR_RRHH_SOLICITUD_ASIGNACION_C(string var, string IdRequerimiento_mobile)
        {
            return oUtilMenor.EjecutaDatatable("uspANULAR_RRHH_SOLICITUD_ASIGNACION_C", var, IdRequerimiento_mobile);
        }
        public DataTable usp_correo_notificar_apobrador_asignacion(string IDE_ASIGNACION, string TIPO_EQUIPO, int tipo_proceso)
        {
            return oUtilitarios.EjecutaDatatable("usp_correo_notificar_apobrador_asignacion", IDE_ASIGNACION, TIPO_EQUIPO, tipo_proceso);
        }

        public DataTable usp_correo_responsable_recursos_excepcion(string IDE_ASIGNACION, string TIPO_EQUIPO,  string GPO_EQUIPO)
        {
            return oUtilitarios.EjecutaDatatable("usp_correo_responsable_recursos_excepcion", IDE_ASIGNACION, TIPO_EQUIPO, GPO_EQUIPO);
        }
        public DataTable uspSEL_APROBAR_RECURSOS_SOLMOBILE(string IDE_ASIGNACION,string IDE_RECURSOS, string USUARIO, string FLG_APROBADO)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_APROBAR_RECURSOS_SOLMOBILE", IDE_ASIGNACION, IDE_RECURSOS, USUARIO, FLG_APROBADO);
        }
        public DataTable uspSEL_LISTAR_RECURSOS_SOLMOBILE_ITEM(string IDE_ASIGNACION, string DES_DESCRIPCION, string IDE_RECURSOS)
        {
            return oUtilitarios.EjecutaDatatable("uspSEL_LISTAR_RECURSOS_SOLMOBILE_ITEM", IDE_ASIGNACION, DES_DESCRIPCION, IDE_RECURSOS);
        }
        public DataTable usp_RRHH_SOLICITUD_RECURSOS(string IDE_ASIGNACION)
        {
            return oUtilitarios.EjecutaDatatable("usp_RRHH_SOLICITUD_RECURSOS", IDE_ASIGNACION);
        }
        public DataTable uspUPD_RRHH_SOLICITUD_ASIGNACION_APROBADOR(string IDE_ASIGNACION, string IDE_GERENCIA)
        {
            return oUtilitarios.EjecutaDatatable("uspUPD_RRHH_SOLICITUD_ASIGNACION_APROBADOR", IDE_ASIGNACION, IDE_GERENCIA);
        }

    }
}
