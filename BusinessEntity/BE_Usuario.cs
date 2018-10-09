using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BusinessEntity
{
    [Serializable]
    public class BE_Usuario
    {
        public BE_Usuario()
        {
        }

        public BE_Usuario(string Usuario, string Password)
        {
            f_Usuario_E = Usuario;
            f_Password_E = Password;
        }
        #region propiedades
        [Description("IDE_USUARIO")]
        public String f_Usuario_E { get; set; }

        [Description("NOMBRE_USUARIO")]
        public String f_NombreUsuario_E { get; set; }

        [Description("PASSWORD")]
        public String f_Password_E { get; set; }


        [Description("FLG_ESTADO")]
        public Boolean f_Bestado_E { get; set; }

        [Description("DES_USUARIO")]
        public String f_cargo_E { get; set; }

        [Description("CENTRO_COSTO")]
        public String f_CENTRO_COSTO_E { get; set; }
        [Description("PROYECTO")]
        public String f_PROYECTO_E { get; set; }
        [Description("IP_CENTRO")]
        public String f_IP_CENTRO_E { get; set; }

        [Description("ID_EMPRESA")]
        public String f_ID_EMPRESA_E { get; set; }

        [Description("FLG_COMUNICADO")]
        public int f_FLG_COMUNICADO_E { get; set; }
        #endregion


        public BE_Perfil oBE_Perfil { get; set; }

        [Description("UrlDefault")]
        public String f_UrlDefault_E { get; set; }
    }
}
