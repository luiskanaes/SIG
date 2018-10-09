using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public class BE_RRHH_DESEMPENIO_OBJETIVOS_MSG
    {
        private int m_IDE_MSG;
        public int IDE_MSG
        {
            get { return m_IDE_MSG; }
            set { m_IDE_MSG = value; }
        }
        private string m_DNI_USER;
        public string DNI_USER
        {
            get { return m_DNI_USER; }
            set { m_DNI_USER = value; }
        }
        private DateTime m_FECHA_ENVIO;
        public DateTime FECHA_ENVIO
        {
            get { return m_FECHA_ENVIO; }
            set { m_FECHA_ENVIO = value; }
        }
        private int m_IDE_OBJETIVO;
        public int IDE_OBJETIVO
        {
            get { return m_IDE_OBJETIVO; }
            set { m_IDE_OBJETIVO = value; }
        }
        private string m_TIPO;
        public string TIPO
        {
            get { return m_TIPO; }
            set { m_TIPO = value; }
        }
        private int m_ANIO;
        public int ANIO
        {
            get { return m_ANIO; }
            set { m_ANIO = value; }
        }
        private string m_ASUNTO;
        public string ASUNTO
        {
            get { return m_ASUNTO; }
            set { m_ASUNTO = value; }
        }
        private string m_COMENTARIO;
        public string COMENTARIO
        {
            get { return m_COMENTARIO; }
            set { m_COMENTARIO = value; }
        }
        private int m_FLG_LEIDO;
        public int FLG_LEIDO
        {
            get { return m_FLG_LEIDO; }
            set { m_FLG_LEIDO = value; }
        }
        private int m_RPT_IDE_MSG;
        public int RPT_IDE_MSG
        {
            get { return m_RPT_IDE_MSG; }
            set { m_RPT_IDE_MSG = value; }
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
