using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_CARTA_COBRAZAS_DETALLE
    {
        private int m_IDE_DETALLE;
        public int IDE_DETALLE
        {
            get { return m_IDE_DETALLE; }
            set { m_IDE_DETALLE = value; }
        }
        private int m_IDE_CARTA;
        public int IDE_CARTA
        {
            get { return m_IDE_CARTA; }
            set { m_IDE_CARTA = value; }
        }
        private string m_DOCUMENTO;
        public string DOCUMENTO
        {
            get { return m_DOCUMENTO; }
            set { m_DOCUMENTO = value; }
        }
        private string m_DETALLE;
        public string DETALLE
        {
            get { return m_DETALLE; }
            set { m_DETALLE = value; }
        }
        private string m_CUENTA_COSTO;
        public string CUENTA_COSTO
        {
            get { return m_CUENTA_COSTO; }
            set { m_CUENTA_COSTO = value; }
        }
        private int m_CANTIDAD;
        public int CANTIDAD
        {
            get { return m_CANTIDAD; }
            set { m_CANTIDAD = value; }
        }
        private Decimal m_PRECIO;
        public Decimal PRECIO
        {
            get { return m_PRECIO; }
            set { m_PRECIO = value; }
        }
        private string m_CUENTA_COSTO_ORIGEN;
        public string CUENTA_COSTO_ORIGEN
        {
            get { return m_CUENTA_COSTO_ORIGEN; }
            set { m_CUENTA_COSTO_ORIGEN = value; }
        }
    }
}
