using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_LOG_SOLPED
    {
        private int m_IDE_SOLICITUD;
        public int IDE_SOLICITUD
        {
            get { return m_IDE_SOLICITUD; }
            set { m_IDE_SOLICITUD = value; }
        }
        private string m_COD_PEDIDO;
        public string COD_PEDIDO
        {
            get { return m_COD_PEDIDO; }
            set { m_COD_PEDIDO = value; }
        }
        private string m_IMPUTACION;
        public string IMPUTACION
        {
            get { return m_IMPUTACION; }
            set { m_IMPUTACION = value; }
        }
        private string m_SOCIEDAD;
        public string SOCIEDAD
        {
            get { return m_SOCIEDAD; }
            set { m_SOCIEDAD = value; }
        }
        private string m_OBRA;
        public string OBRA
        {
            get { return m_OBRA; }
            set { m_OBRA = value; }
        }
        private string m_FECHA;
        public string FECHA
        {
            get { return m_FECHA; }
            set { m_FECHA = value; }
        }
        private Decimal m_VALOR;
        public Decimal VALOR
        {
            get { return m_VALOR; }
            set { m_VALOR = value; }
        }
        private string m_MONEDA;
        public string MONEDA
        {
            get { return m_MONEDA; }
            set { m_MONEDA = value; }
        }
        private string m_CENTRO;
        public string CENTRO
        {
            get { return m_CENTRO; }
            set { m_CENTRO = value; }
        }
        private string m_GR_COMPRA;
        public string GR_COMPRA
        {
            get { return m_GR_COMPRA; }
            set { m_GR_COMPRA = value; }
        }
        private string m_CENTRO_COSTE;
        public string CENTRO_COSTE
        {
            get { return m_CENTRO_COSTE; }
            set { m_CENTRO_COSTE = value; }
        }
        private string m_SOLICITANTE;
        public string SOLICITANTE
        {
            get { return m_SOLICITANTE; }
            set { m_SOLICITANTE = value; }
        }
        private DateTime m_FECHA_REGISTRO;
        public DateTime FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
    }
}
