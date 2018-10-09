using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace BusinessEntity
{
    [Serializable]
    public class BE_PERSONAL
    {
        [Description("IDE_EMPLEADO")]
        public Int32 i_IDE_EMPLEADO_E { get; set; }
        [Description("DES_DNI")]
        public String v_DES_DNI_E { get; set; }
        [Description("DES_NOMBRE")]
        public String v_DES_NOMBRE_E { get; set; }
        [Description("FEC_FECHA_NACIMIENTO")]
        public String f_FEC_FECHA_NACIMIENTO_E { get; set; }
        [Description("FEC_FECHA_INGRESO")]
        public String f_FEC_FECHA_INGRESO_E { get; set; }
        [Description("DES_TELEFONO")]
        public String v_DES_TELEFONO_E { get; set; }
        [Description("DES_CORREO")]
        public String v_DES_CORREO_E { get; set; }
        [Description("DES_DIRECCION")]
        public String v_DES_DIRECCION_E { get; set; }
        [Description("DES_ESTADO_CIVIL")]
        public Int32 i_DES_ESTADO_CIVIL_E { get; set; }
        [Description("FOTO")]
        public byte[] b_foto_e { get; set; }
        [Description("DES_CV")]
        public byte[] b_Cv_e { get; set; }
        [Description("FLG_ESTADO")]
        public Boolean FLG_ESTADO { get; set; }
        [Description("DES_MANO_OBRA")]
        public String v_DES_MANO_OBRA_E { get; set; }
        [Description("DES_TIPO_DOCUMENTO")]
        public Int32 i_DES_TIPO_DOCUMENTO_E { get; set; }

        [Description("DES_APEPAT")]
        public String v_DES_APEPAT_E { get; set; }
        [Description("DES_APEMAT")]
        public String v_DES_APEMAT_E { get; set; }

        [Description("DES_USUARIO_CREACION")]
        public String v_DES_USUARIO_CREACION_E { get; set; }

        [Description("DES_CARGO")]
        public Int32 v_DES_CARGO_E { get; set; }

        [Description("DES_ESPECIALIDAD")]
        public Int32 v_DES_ESPECIALIDAD_E { get; set; }
 
    }
}
