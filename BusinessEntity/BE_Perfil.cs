using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace BusinessEntity
{
    [Serializable]
    public class BE_Perfil
    {
        public BE_Perfil()
        {
        }
        [Description("IdPerfil")]
        public Int32 f_Perfil_E { get; set; }
        [Description("Descripcion")]
        public String f_Vnomperfil_E { get; set; }
        [Description("Estado")]
        public Boolean f_Bestado_E { get; set; }
        [Description("UrlDefault")]
        public String f_url_E { get; set; }
        [Description("Control")]
        public String f_Control_E { get; set; }
    }
}
