using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public  class BE_OPE_DETALLE_PERSONAL
    {
        private int detalle_personal, id_minuta;
        private string id_dni, centro_Costo;

        public int Detalle_personal
        {
            get { return detalle_personal; }
            set { detalle_personal = value; }
        }
        public string Centro_Costo
        {
            get { return centro_Costo; }
            set { centro_Costo = value; }
        }
        public int Id_minuta
        {
            get { return id_minuta; }
            set { id_minuta = value; }
        }

        public string Id_dni
        {
            get { return id_dni; }
            set { id_dni = value; }
        }
    }
}
