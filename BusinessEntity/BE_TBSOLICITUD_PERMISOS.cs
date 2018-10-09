using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_TBSOLICITUD_PERMISOS
    {
        //atributos
        private int ide_permiso;
        private string ide_usuario;
        private int ide_motivo;
        private string inicio;
        private string fin;
        private int flg_estado;
        private string fecha_solicita;
        private string comentario;
        private string ccentro;
        private string usuario_atiende;
        private string observacion;

        //Propiedades
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }


        public string Usuario_atiende
        {
            get { return usuario_atiende; }
            set { usuario_atiende = value; }
        }


        public string Ccentro
        {
            get { return ccentro; }
            set { ccentro = value; }
        }

        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public string Fecha_solicita
        {
            get { return fecha_solicita; }
            set { fecha_solicita = value; }
        }


        public int Flg_estado
        {
            get { return flg_estado; }
            set { flg_estado = value; }
        }


        public string Fin
        {
            get { return fin; }
            set { fin = value; }
        }


        public string Inicio
        {
            get { return inicio; }
            set { inicio = value; }
        }


        public int Ide_motivo
        {
            get { return ide_motivo; }
            set { ide_motivo = value; }
        }


        public string Ide_usuario
        {
            get { return ide_usuario; }
            set { ide_usuario = value; }
        }


        public int Ide_permiso
        {
            get { return ide_permiso; }
            set { ide_permiso = value; }
        }
        private string m_FILE;
        public string FILE
        {
            get { return m_FILE; }
            set { m_FILE = value; }
        }
        private string m_URL;
        public string URL
        {
            get { return m_URL; }
            set { m_URL = value; }
        }
        private string m_NOMBRE_DIA;
        public string NOMBRE_DIA
        {
            get { return m_NOMBRE_DIA; }
            set { m_NOMBRE_DIA = value; }
        }
    }
}
