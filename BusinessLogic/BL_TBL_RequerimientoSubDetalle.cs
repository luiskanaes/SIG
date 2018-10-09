using BusinessEntity;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogic
{
    public class BL_TBL_RequerimientoSubDetalle
    {
        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR(string Requ_Numero,string D_ESTADO_PROCESO, string  anio, string centro, 
            string txtRequerimientosa_H,
    string txtFamilia_H,
    string txtSUBFAMILIA_H,
    string txtSOLPED_H,
    string txtPDC_H,
    string txtGPO_H, string ampliacion, string txtOR_H)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR(Requ_Numero, D_ESTADO_PROCESO, anio, centro,
                txtRequerimientosa_H,
        txtFamilia_H,
        txtSUBFAMILIA_H,
        txtSOLPED_H,
        txtPDC_H,
        txtGPO_H, ampliacion, txtOR_H
         );
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MENOR(string Requ_Numero, string D_ESTADO_PROCESO, string anio, string centro,
            string txtRequerimientosa_H,
    string txtFamilia_H,
    string txtSUBFAMILIA_H,
    string txtSOLPED_H,
    string txtPDC_H,
    string txtGPO_H,
    string Ampliacion,
    string txtOR_H)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MENOR(Requ_Numero, D_ESTADO_PROCESO, anio, centro,
                txtRequerimientosa_H,
        txtFamilia_H,
        txtSUBFAMILIA_H,
        txtSOLPED_H,
        txtPDC_H,
        txtGPO_H,
        Ampliacion,
        txtOR_H);
        }

        public DataTable SP_CONSULTAR_TBL_Proveedor()
        {
            return new DA_TBL_RequerimientoSubDetalle().SP_CONSULTAR_TBL_Proveedor();
        }
        public DataTable uspUPD_TBL_RequerimientoSubDetalle_Alquiler(BE_TBL_RequerimientoSubDetalle oBE)
        {
            try
            {
                return new DA_TBL_RequerimientoSubDetalle().uspUPD_TBL_RequerimientoSubDetalle_Alquiler(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_ID(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_ID(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        }
        public int uspINS_TBL_RequerimientoSubDetalle_PDC(BE_TBL_RequerimientoSubDetalle oBE)
        {
            try
            {
                return new DA_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_PDC(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_TBL_RequerimientoSubDetalle_PDC_OBSERVA(BE_TBL_RequerimientoSubDetalle oBE)
        {
            try
            {
                return new DA_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_PDC_OBSERVA(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string MENSAJE,string PROCESO ,string URL )
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, MENSAJE, PROCESO , URL);
        }
        public DataTable UPD_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_SOLPED(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string SOLPED, string pos1 , string pos2, string subfamilia, string marca, string modelo, string capacidad)
        {
            return new DA_TBL_RequerimientoSubDetalle().UPD_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_SOLPED(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, SOLPED, pos1 , pos2,  subfamilia,  marca,  modelo,  capacidad);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_ADJUNTO(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_ADJUNTO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        }
        public DataTable uspUPD_TBL_RequerimientoSubDetalle_FECHA_SALIDA(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string fecha)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspUPD_TBL_RequerimientoSubDetalle_FECHA_SALIDA(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, fecha);
        }
        public DataTable USP_SEL_TBL_PROYECTO_GESTOR()
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_PROYECTO_GESTOR();
        }
        public DataTable uspSEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, int tipo)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspSEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo ,tipo );
        }
        public DataTable uspDEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(int IDE_FILE)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspDEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(IDE_FILE);
        }
        public DataTable uspSEL_TBL_REQUERIMIENTOSUBDETALLE_PDC(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspSEL_TBL_REQUERIMIENTOSUBDETALLE_PDC(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO_PDC(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string MENSAJE, string PROCESO, string URL)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO_PDC(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, MENSAJE, PROCESO, URL);
        }
        public DataTable uspLISTAR_REQUERIMIENTOS_LIBRE(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string FLG_MENOR)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspLISTAR_REQUERIMIENTOS_LIBRE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, FLG_MENOR);
        }
        public DataTable uspREGISTRAR_LEGAJOFILE(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, int tipo, string requerimiento, string salida)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspREGISTRAR_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, tipo, requerimiento, salida);
        }
        public DataTable GRUPO_LEGAJOFILE(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, int tipo)
        {
            return new DA_TBL_RequerimientoSubDetalle().GRUPO_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, tipo);
        }
        public DataTable uspRETIRAR_GRUPOLEGAJO(string Requ_Numero)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspRETIRAR_GRUPOLEGAJO(Requ_Numero);
        }
        public DataTable LISTAR_GRUPO_LEGAJOFILE(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, int tipo)
        {
            return new DA_TBL_RequerimientoSubDetalle().LISTAR_GRUPO_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, tipo);
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_CORREO_SOLPED_VARIOS(string SOLPED, string MENSAJE, string PROCESO, string URL)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_CORREO_SOLPED_VARIOS(SOLPED, MENSAJE, PROCESO, URL);
        }

        public DataTable LISTAR_GRUPO_SOLPED(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, int tipo)
        {
            return new DA_TBL_RequerimientoSubDetalle().LISTAR_GRUPO_SOLPED(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, tipo);
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_REGULARIZACION(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_REGULARIZACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        }

        public DataTable USP_SEL_PROCESAR_REGULARIZACION(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string  tipo, string usuario, string texto, int proceso)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_PROCESAR_REGULARIZACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, tipo, usuario, texto, proceso);
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_REGULARIZACION_CORREO(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string MENSAJE, string PROCESO, string URL)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_REGULARIZACION_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, MENSAJE, PROCESO, URL);
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_EQUIPO_REG_INFORMACION(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string MENSAJE, string PROCESO, string URL)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_EQUIPO_REG_INFORMACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, MENSAJE, PROCESO, URL);
        }

        public DataTable uspInsertarArchivosAdicionales_Alquiler(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string ruta, string archivo, string nombreNuevo)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspInsertarArchivosAdicionales_Alquiler(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, ruta , archivo, nombreNuevo);
        }
        public DataTable USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR(string CC, string proveedor, string INICIO, string FIN)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR(CC, proveedor, INICIO, FIN);
        }

        public DataTable USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_V2(string CC, string proveedor, string anio, string mes,string pdc, string requerimiento)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_V2(CC, proveedor, anio, mes, pdc, requerimiento);
        }
        public DataTable USP_UPDATE_TBL_VALORIZACION_FECHA(string ide_valor, string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string fecha,string fin, string valor, string IDE_MONEDA, string TIPO_TARIFA, string DIA_I, string  DIA_F)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_UPDATE_TBL_VALORIZACION_FECHA(ide_valor,Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, fecha, fin ,valor, IDE_MONEDA, TIPO_TARIFA, DIA_I, DIA_F);
        }
        public DataTable USP_SEL_TBL_VALORIZACION_PROVEEDOR(string CENTRO, string ANIO, string MES)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VALORIZACION_PROVEEDOR(CENTRO, ANIO, MES);
        }
        public DataTable USP_SEL_TBL_VALORIZACION_CC(string proceso, string usuario)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VALORIZACION_CC(proceso, usuario);
        }
        public DataTable uspINS_valorizar_ValorPeriodo(int ide_valor ,
            string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, 
            string V_FECHA_INICIO_VAL, string V_TARIFA_DIA, string USER_REGISTRO, string PROYECTO)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspINS_valorizar_ValorPeriodo(ide_valor,
                Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, V_FECHA_INICIO_VAL, V_TARIFA_DIA, USER_REGISTRO, PROYECTO);
        }
        public DataTable uspSEL_VALORIZAR_VALORPERIODO_POR_ID(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspSEL_VALORIZAR_VALORPERIODO_POR_ID(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        }
        public DataTable USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_TODO(string CENTRO, string D_Prov_RUC, string FLG_TARIFA, string PDC, string REQ)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_TODO(CENTRO, D_Prov_RUC, FLG_TARIFA,PDC ,REQ);
        }
        public int uspINS_valorizar_equipoMenor(BE_valorizar_equipoMenor oBESOl)
        {
            try
            {
                return new DA_TBL_RequerimientoSubDetalle().uspINS_valorizar_equipoMenor(oBESOl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable USP_TBL_VALORIZACION_FECHA_TARIFA(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string fecha)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_TBL_VALORIZACION_FECHA_TARIFA(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, fecha);
        }

        public DataTable Ins_valorizar_equipoMenor_cierre(int anio, int mes, string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, int ide_valor)
        {
            return new DA_TBL_RequerimientoSubDetalle().Ins_valorizar_equipoMenor_cierre(anio, mes,Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, ide_valor);
        }
        public DataTable USP_SEL_TBL_VALORIZACION_CC_CONSOLIDADO(string proceso, string usuario, int anio, int mes)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VALORIZACION_CC_CONSOLIDADO(proceso, usuario, anio, mes);
        }

        public DataTable USP_SEL_TBL_VAL_PROVEEDOR_CONSOLIDADO(string CENTRO, string ANIO, string MES)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_VAL_PROVEEDOR_CONSOLIDADO(CENTRO, ANIO, MES);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_PDC_AMPLIAR(string centro, string D_PDC, string Flg_ampliacion, string Estado)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_PDC_AMPLIAR(centro, D_PDC, Flg_ampliacion, Estado);
        }
        public DataTable USP_SEL_TBL_ARRIENDO_CC(string PROCESO, string USUARIO, string PROYECTO)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_ARRIENDO_CC(PROCESO, USUARIO, PROYECTO);
        }
        public DataTable uspSEL_TBL_REQUERIMIENTOSUBDETALLE_PDC_HISTORIAL(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspSEL_TBL_REQUERIMIENTOSUBDETALLE_PDC_HISTORIAL(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        }

        public DataTable uspINS_TBL_GENERAR_AMPLIACION(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, 
            string fase, string posicion_anterior, string posicion_nueva, string valor, string total, string PDC, string Reqs_ItemSecuencia)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspINS_TBL_GENERAR_AMPLIACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, fase, posicion_anterior, posicion_nueva, valor,total, PDC, Reqs_ItemSecuencia);
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_CORREO_AMPLIACION(string CODIGO_REQ, string PROCESO, int ETAPA)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_CORREO_AMPLIACION(CODIGO_REQ, PROCESO, ETAPA);
        }
        public DataTable uspUPD_MONTO_AMPLIACION(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string Monto, string inicio, string fin,  string usuario)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspUPD_MONTO_AMPLIACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, Monto, inicio , fin, usuario);
        }

        public int uspINS_TBL_RequerimientoSubDetalle_PDC_AMPLIACION(BE_TBL_RequerimientoSubDetalle oBE)
        {
            try
            {
                return new DA_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_PDC_AMPLIACION(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int uspINS_TBL_RequerimientoSubDetalle_AMPLIACION(BE_TBL_RequerimientoSubDetalle oBE)
        {
            try
            {
                return new DA_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_AMPLIACION(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SP_BUSCAR_TBL_Modelo(string Marc_Codigo, string Mode_Codigo)
        {
            return new DA_TBL_RequerimientoSubDetalle().SP_BUSCAR_TBL_Modelo(Marc_Codigo, Mode_Codigo);
        }
        public DataTable SP_BUSCAR_TBL_MARCA(string Marc_Codigo)
        {
            return new DA_TBL_RequerimientoSubDetalle().SP_BUSCAR_TBL_MARCA(Marc_Codigo);
        }
        public DataTable SP_BUSCAR_TBL_SubFamilia(string Fami_Codigo)
        {
            return new DA_TBL_RequerimientoSubDetalle().SP_BUSCAR_TBL_SubFamilia(Fami_Codigo);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_FILE_AMPLIACION(string Requ_Numero, string Reqd_CodLinea, string Reqs_Correlativo, string file)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_FILE_AMPLIACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, file);
        }

        public DataTable UPD_SEL_TBL_REQUERIMIENTO_EQUIPO_OBRA(string  Reqs_ItemSecuencia,  string subfamilia, string marca, string modelo, string capacidad)
        {
            return new DA_TBL_RequerimientoSubDetalle().UPD_SEL_TBL_REQUERIMIENTO_EQUIPO_OBRA(Reqs_ItemSecuencia,subfamilia, marca, modelo, capacidad);
        }
        public DataTable LISTAR_DATOS_EQUIPO(string Reqs_ItemSecuencia)
        {
            return new DA_TBL_RequerimientoSubDetalle().LISTAR_DATOS_EQUIPO(Reqs_ItemSecuencia);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_CORREO_LIBERACION(string CODIGO_REQ, string PROCESO, int ETAPA)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_CORREO_LIBERACION(CODIGO_REQ, PROCESO, ETAPA);
        }

        public DataTable USP_SEL_TBL_REQUERIMIENTO_CONSULTAR_AMPLIAR(string centro, string D_PDC)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_CONSULTAR_AMPLIAR(centro, D_PDC);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_RPT_RESUMEN(string centro)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_RPT_RESUMEN(centro);
        }

        public DataTable USP_UPDATE_REGULARIZACION_EQUIPO_MENOR(string Reqs_ItemSecuencia, string D_ATENCION_TIPO)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_UPDATE_REGULARIZACION_EQUIPO_MENOR(Reqs_ItemSecuencia, D_ATENCION_TIPO);
        }

        public DataTable SP_LISTAR_ARCHIVOS_PDC(string PDC)
        {
            return new DA_TBL_RequerimientoSubDetalle().SP_LISTAR_ARCHIVOS_PDC(PDC);
        }
        public DataTable SP_LISTAR_ARCHIVOS_PDC_TODOS(string PDC)
        {
            return new DA_TBL_RequerimientoSubDetalle().SP_LISTAR_ARCHIVOS_PDC_TODOS(PDC);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_ItemSecuencia(string Requ_Numero)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_ItemSecuencia(Requ_Numero);
        }
        public DataTable USP_SEL_TBL_REQUERIMIENTO_ASIGNA_RESPONSABLE(string Requ_Numero, string responsable)
        {
            return new DA_TBL_RequerimientoSubDetalle().USP_SEL_TBL_REQUERIMIENTO_ASIGNA_RESPONSABLE(Requ_Numero, responsable);
        }
        public DataTable uspTBL_RequerimientoDetalle_EstadoAtencion(string Requ_Numero)
        {
            return new DA_TBL_RequerimientoSubDetalle().uspTBL_RequerimientoDetalle_EstadoAtencion(Requ_Numero);
        }
    }
}
