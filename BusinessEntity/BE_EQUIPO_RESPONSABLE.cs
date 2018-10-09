using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_EQUIPO_RESPONSABLE
    {
        private int m_IDE_RESPONSABLE;
        public int IDE_RESPONSABLE
        {
            get { return m_IDE_RESPONSABLE; }
            set { m_IDE_RESPONSABLE = value; }
        }
        private string m_DNI_RESPONSABLE;
        public string DNI_RESPONSABLE
        {
            get { return m_DNI_RESPONSABLE; }
            set { m_DNI_RESPONSABLE = value; }
        }
        private string m_IP_CENTRO;
        public string IP_CENTRO
        {
            get { return m_IP_CENTRO; }
            set { m_IP_CENTRO = value; }
        }
        private string m_CENTRO;
        public string CENTRO
        {
            get { return m_CENTRO; }
            set { m_CENTRO = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
    }
}
