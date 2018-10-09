using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_FORMATIVO_PLANES
    {
        private int m_IDE_PLANES;
        public int IDE_PLANES
        {
            get { return m_IDE_PLANES; }
            set { m_IDE_PLANES = value; }
        }
        private string m_DURACION;
        public string DURACION
        {
            get { return m_DURACION; }
            set { m_DURACION = value; }
        }
        private string m_DESCRIPCION;
        public string DESCRIPCION
        {
            get { return m_DESCRIPCION; }
            set { m_DESCRIPCION = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private int m_IDE_FICHA;
        public int IDE_FICHA
        {
            get { return m_IDE_FICHA; }
            set { m_IDE_FICHA = value; }
        }
        private DateTime m_FECHA_REGISTRO;
        public DateTime FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }

    }
}
