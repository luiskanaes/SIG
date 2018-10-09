using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_CARTA_COBRAZAS_FILE
    {
        private int m_IDE_FILE;
        public int IDE_FILE
        {
            get { return m_IDE_FILE; }
            set { m_IDE_FILE = value; }
        }
        private string m_ARCHIVO;
        public string ARCHIVO
        {
            get { return m_ARCHIVO; }
            set { m_ARCHIVO = value; }
        }
        private string m_RUTA;
        public string RUTA
        {
            get { return m_RUTA; }
            set { m_RUTA = value; }
        }
        private DateTime m_FECHA_CARGA;
        public DateTime FECHA_CARGA
        {
            get { return m_FECHA_CARGA; }
            set { m_FECHA_CARGA = value; }
        }
        private int m_IDE_CARTA;
        public int IDE_CARTA
        {
            get { return m_IDE_CARTA; }
            set { m_IDE_CARTA = value; }
        }
        private string m_NOMBRE_ORIGINAL;
        public string NOMBRE_ORIGINAL
        {
            get { return m_NOMBRE_ORIGINAL; }
            set { m_NOMBRE_ORIGINAL = value; }
        }
    }
}
