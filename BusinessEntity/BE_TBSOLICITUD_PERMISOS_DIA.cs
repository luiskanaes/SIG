using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_TBSOLICITUD_PERMISOS_DIA
    {
        //atributos
        private int ide_detalle;
        private int ide_permiso;
        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }


        public int Ide_permiso
        {
            get { return ide_permiso; }
            set { ide_permiso = value; }
        }


        public int Ide_detalle
        {
            get { return ide_detalle; }
            set { ide_detalle = value; }
        }

    }
}
