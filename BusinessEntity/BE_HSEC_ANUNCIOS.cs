using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public class BE_HSEC_ANUNCIOS
    {
        private int m_IDE_ANUNCIO;
        public int IDE_ANUNCIO
        {
            get { return m_IDE_ANUNCIO; }
            set { m_IDE_ANUNCIO = value; }
        }
        private string m_ARCHIVO;
        public string ARCHIVO
        {
            get { return m_ARCHIVO; }
            set { m_ARCHIVO = value; }
        }
        private string m_ARCHIVO_NOMBRE;
        public string ARCHIVO_NOMBRE
        {
            get { return m_ARCHIVO_NOMBRE; }
            set { m_ARCHIVO_NOMBRE = value; }
        }
        private string m_ARCHIVO_URL;
        public string ARCHIVO_URL
        {
            get { return m_ARCHIVO_URL; }
            set { m_ARCHIVO_URL = value; }
        }
        private string m_ARCHIVO_EXTESION;
        public string ARCHIVO_EXTESION
        {
            get { return m_ARCHIVO_EXTESION; }
            set { m_ARCHIVO_EXTESION = value; }
        }
        private int m_TIPO_ANUNCIO;
        public int TIPO_ANUNCIO
        {
            get { return m_TIPO_ANUNCIO; }
            set { m_TIPO_ANUNCIO = value; }
        }
        private string m_COMENTARIOS;
        public string COMENTARIOS
        {
            get { return m_COMENTARIOS; }
            set { m_COMENTARIOS = value; }
        }
        private string m_URL;
        public string URL
        {
            get { return m_URL; }
            set { m_URL = value; }
        }
        private int m_ORDEN;
        public int ORDEN
        {
            get { return m_ORDEN; }
            set { m_ORDEN = value; }
        }
        private string m_FECHA_INICIO;
        public string FECHA_INICIO
        {
            get { return m_FECHA_INICIO; }
            set { m_FECHA_INICIO = value; }
        }
        private string m_FECHA_FIN;
        public string FECHA_FIN
        {
            get { return m_FECHA_FIN; }
            set { m_FECHA_FIN = value; }
        }
        private int m_FLG_VISIBLE;
        public int FLG_VISIBLE
        {
            get { return m_FLG_VISIBLE; }
            set { m_FLG_VISIBLE = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }
        private string m_FECHA_REGISTRO;
        public string FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }

    }
}
