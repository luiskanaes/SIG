using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_LOG_SOLPED_DETALLE
    {
        private int m_IDE_PEDIDO;
        public int IDE_PEDIDO
        {
            get { return m_IDE_PEDIDO; }
            set { m_IDE_PEDIDO = value; }
        }
        private string m_MATERIAL;
        public string MATERIAL
        {
            get { return m_MATERIAL; }
            set { m_MATERIAL = value; }
        }
        private string m_MATERIAL_TEXTO;
        public string MATERIAL_TEXTO
        {
            get { return m_MATERIAL_TEXTO; }
            set { m_MATERIAL_TEXTO = value; }
        }
        private int m_CANTIDAD;
        public int CANTIDAD
        {
            get { return m_CANTIDAD; }
            set { m_CANTIDAD = value; }
        }
        private string m_UNIDAD;
        public string UNIDAD
        {
            get { return m_UNIDAD; }
            set { m_UNIDAD = value; }
        }
        private string m_GRUPO;
        public string GRUPO
        {
            get { return m_GRUPO; }
            set { m_GRUPO = value; }
        }
        private string m_CUENTA_MAYOR;
        public string CUENTA_MAYOR
        {
            get { return m_CUENTA_MAYOR; }
            set { m_CUENTA_MAYOR = value; }
        }
        private string m_PEP;
        public string PEP
        {
            get { return m_PEP; }
            set { m_PEP = value; }
        }
        private DateTime m_FECHA_REGISTRO;
        public DateTime FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private string m_COD_PEDIDO;
        public string COD_PEDIDO
        {
            get { return m_COD_PEDIDO; }
            set { m_COD_PEDIDO = value; }
        }
        private int m_IDE_SOLICITUD;
        public int IDE_SOLICITUD
        {
            get { return m_IDE_SOLICITUD; }
            set { m_IDE_SOLICITUD = value; }
        }
    }
}
