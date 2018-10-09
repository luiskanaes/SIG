using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BusinessEntity
{
    [Serializable]
    public class BE_MOD
    {
        [Description("ID_MOD")]
        public Int32 i_ID_MOD_E { get; set; }
        [Description("FLG_ESTADO")]
        public Int32 i_FLG_ESTADO_E { get; set; }
        [Description("ID_EMPRESA")]
        public Int32 i_ID_EMPRESA_E { get; set; }
        [Description("IDOBRA")]
        public String v_IDOBRA_E { get; set; }
        [Description("DES_UBICACION")]
        public Int32 i_DES_UBICACION { get; set; }
        [Description("ID_CENTROCOSTO")]
        public Int32 i_ID_CENTROCOSTO_E { get; set; }
        [Description("IDE_AREA")]
        public Int32 i_IDE_AREA_E { get; set; }
        [Description("IDE_CARGO")]
        public Int32 i_IDE_CARGO_E { get; set; }
        [Description("IDE_PROCESO")]
        public Int32 i_IDE_PROCESO_E { get; set; }
        [Description("IDE_TIPO_PROCESO")]
        public Int32 i_IDE_TIPO_PROCESO_E { get; set; }
        [Description("DES_FUENTE")]
        public String v_DES_FUENTE_E { get; set; }
        [Description("IDE_ORIGEN_POSICION")]
        public Int32 i_IDE_ORIGEN_POSICION_E { get; set; }
        [Description("DES_RESPONSABLE")]
        public String v_DES_RESPONSABLE_E { get; set; }
        [Description("DES_REQUERIMIENTO")]
        public String v_DES_REQUERIMIENTO_E { get; set; }
        [Description("DES_ITEM")]
        public String v_DES_ITEM_E { get; set; }
        [Description("FEC_FECHA_APROBACION")]
        public String f_FEC_FECHA_APROBACION_E { get; set; }
        [Description("IDE_EMPLEADO")]
        public Int32 v_IDE_EMPLEADO_E { get; set; }
        [Description("FEC_FECHA_VIAJE")]
        public String f_FEC_FECHA_VIAJE_E { get; set; }
        [Description("FEC_FECHA_EXAMEN_MED")]
        public String f_FEC_FECHA_EXAMEN_MED_E { get; set; }
        [Description("FEC_FECHA_PRIMERENVIO")]
        public String f_FEC_FECHA_PRIMERENVIO_E { get; set; }
        [Description("FEC_FECHA_FEEDBACK")]
        public String f_FEC_FECHA_FEEDBACK_E { get; set; }
        [Description("FEC_SALIDA_EMPRESA")]
        public String f_FEC_SALIDA_EMPRESA_E { get; set; }
        [Description("DES_COMENTARIOS")]
        public String v_DES_COMENTARIOS_E { get; set; }
        [Description("IDE_SALIDA_EMPRESA")]
        public Int32 i_IDE_SALIDA_EMPRESA_E { get; set; }
        [Description("FEC_INGRESO")]
        public String f_FEC_INGRESO_E { get; set; }
        [Description("IDE_NRO_PROCESO")]
        public Int32 i_IDE_NRO_PROCESO_E { get; set; }

        [Description("FEC_ATENCION")]
        public String f_FEC_ATENCION_E { get; set; }

        [Description("IDE_FUENTE")]
        public Int32 i_IDE_FUENTE_E { get; set; }

        [Description("ID_DETALLE_REQUERIMIENTO_PERSONAL")]
        public String v_ID_DETALLE_REQUERIMIENTO_PERSONAL_E { get; set; }

        [Description("ID_CMOD")]
        public String v_ID_CMOD_E { get; set; }
    }

}


