using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_OPE_DETALLE_ACUERDOS
    {
        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private int id_detalle_acuerdo;

        public int Id_detalle_acuerdo
        {
            get { return id_detalle_acuerdo; }
            set { id_detalle_acuerdo = value; }
        }
        private int id_temas;
        public int Id_temas
        {
            get { return id_temas; }
            set { id_temas = value; }
        }
        private string dsc_descripcion;
        public string Dsc_descripcion
        {
            get { return dsc_descripcion; }
            set { dsc_descripcion = value; }
        }
        private string est_estado;

        public string Est_estado
        {
            get { return est_estado; }
            set { est_estado = value; }
        }
        private string dsc_observacion;
        public string Dsc_observacion
        {
            get { return dsc_observacion; }
            set { dsc_observacion = value; }
        }
        private int inicialUsuario;
        public int InicialUsuario
        {
            get { return inicialUsuario; }
            set { inicialUsuario = value; }
        }

        private int numOrden;
        public int NumOrden
        {
            get { return numOrden; }
            set { numOrden = value; }
        }
    }
}
