using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public class BE_BUENAS_IDEAS
    {
        private int m_IDE_IDEAS;
        public int IDE_IDEAS
        {
            get { return m_IDE_IDEAS; }
            set { m_IDE_IDEAS = value; }
        }
        private string m_DESCRIPCION_PROPUESTA;
        public string DESCRIPCION_PROPUESTA
        {
            get { return m_DESCRIPCION_PROPUESTA; }
            set { m_DESCRIPCION_PROPUESTA = value; }
        }
        private string m_SOLUCION;
        public string SOLUCION
        {
            get { return m_SOLUCION; }
            set { m_SOLUCION = value; }
        }
        private string m_LIMITACIONES;
        public string LIMITACIONES
        {
            get { return m_LIMITACIONES; }
            set { m_LIMITACIONES = value; }
        }
        private string m_VENTAJAS;
        public string VENTAJAS
        {
            get { return m_VENTAJAS; }
            set { m_VENTAJAS = value; }
        }
        private string m_AREAS;
        public string AREAS
        {
            get { return m_AREAS; }
            set { m_AREAS = value; }
        }
        private int? m_FLG_ESTADO;
        public int? FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private int? m_FLG_ETAPAS;
        public int? FLG_ETAPAS
        {
            get { return m_FLG_ETAPAS; }
            set { m_FLG_ETAPAS = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }
        private DateTime m_REGISTRO_FECHA;
        public DateTime REGISTRO_FECHA
        {
            get { return m_REGISTRO_FECHA; }
            set { m_REGISTRO_FECHA = value; }
        }
        private string m_USER_APRUEBA;
        public string USER_APRUEBA
        {
            get { return m_USER_APRUEBA; }
            set { m_USER_APRUEBA = value; }
        }
        private DateTime m_FECHA_ENVIO;
        public DateTime FECHA_ENVIO
        {
            get { return m_FECHA_ENVIO; }
            set { m_FECHA_ENVIO = value; }
        }
        private int m_FLG_ENVIADO;
        public int FLG_ENVIADO
        {
            get { return m_FLG_ENVIADO; }
            set { m_FLG_ENVIADO = value; }
        }
        private string m_TITULO;
        public string TITULO
        {
            get { return m_TITULO; }
            set { m_TITULO = value; }
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
    }
}
