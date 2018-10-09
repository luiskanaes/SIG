using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_SOL_HITO
    {
        private int m_IDE_HITOS;
        public int IDE_HITOS
        {
            get { return m_IDE_HITOS; }
            set { m_IDE_HITOS = value; }
        }
        private int m_IDE_FORMATIVO;
        public int IDE_FORMATIVO
        {
            get { return m_IDE_FORMATIVO; }
            set { m_IDE_FORMATIVO = value; }
        }
        private string m_DESCRIPCION;
        public string DESCRIPCION
        {
            get { return m_DESCRIPCION; }
            set { m_DESCRIPCION = value; }
        }
        private string m_FECHA_HITO;
        public string FECHA_HITO
        {
            get { return m_FECHA_HITO; }
            set { m_FECHA_HITO = value; }
        }
        private string m_FECHA_REGISTRO;
        public string FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private int m_TIPO;
        public int TIPO
        {
            get { return m_TIPO; }
            set { m_TIPO = value; }
        }
        private string m_HOLDER;
        public string HOLDER
        {
            get { return m_HOLDER; }
            set { m_HOLDER = value; }
        }
        private string m_ROL;
        public string ROL
        {
            get { return m_ROL; }
            set { m_ROL = value; }
        }
        private string m_INTERACCION;
        public string INTERACCION
        {
            get { return m_INTERACCION; }
            set { m_INTERACCION = value; }
        }

    }
}
