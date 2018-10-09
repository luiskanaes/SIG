using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_CARTA_COBRAZAS_APROBACIONES
    {
        private int m_IDE_APROBACION;
        public int IDE_APROBACION
        {
            get { return m_IDE_APROBACION; }
            set { m_IDE_APROBACION = value; }
        }
        private int m_IDE_CARTA;
        public int IDE_CARTA
        {
            get { return m_IDE_CARTA; }
            set { m_IDE_CARTA = value; }
        }
        private string m_D_IPE_CENTRO;
        public string D_IPE_CENTRO
        {
            get { return m_D_IPE_CENTRO; }
            set { m_D_IPE_CENTRO = value; }
        }
        private string m_D_CENTRO;
        public string D_CENTRO
        {
            get { return m_D_CENTRO; }
            set { m_D_CENTRO = value; }
        }
        private string m_DNI_APRUEBA;
        public string DNI_APRUEBA
        {
            get { return m_DNI_APRUEBA; }
            set { m_DNI_APRUEBA = value; }
        }
        private DateTime m_FECHA_APRUEBA;
        public DateTime FECHA_APRUEBA
        {
            get { return m_FECHA_APRUEBA; }
            set { m_FECHA_APRUEBA = value; }
        }
        private int m_TIPO_CARGO;
        public int TIPO_CARGO
        {
            get { return m_TIPO_CARGO; }
            set { m_TIPO_CARGO = value; }
        }
        private string m_CARGO;
        public string CARGO
        {
            get { return m_CARGO; }
            set { m_CARGO = value; }
        }
    }
}
