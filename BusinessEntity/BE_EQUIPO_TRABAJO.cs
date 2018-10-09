using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_EQUIPO_TRABAJO
    {
        private int m_IDE_EQUIPO;
        public int IDE_EQUIPO
        {
            get { return m_IDE_EQUIPO; }
            set { m_IDE_EQUIPO = value; }
        }
        private string m_DNI_SUPERVISOR;
        public string DNI_SUPERVISOR
        {
            get { return m_DNI_SUPERVISOR; }
            set { m_DNI_SUPERVISOR = value; }
        }
        private string m_DNI_TRABAJADOR;
        public string DNI_TRABAJADOR
        {
            get { return m_DNI_TRABAJADOR; }
            set { m_DNI_TRABAJADOR = value; }
        }
        private string m_CC_SUPERVISOR;
        public string CC_SUPERVISOR
        {
            get { return m_CC_SUPERVISOR; }
            set { m_CC_SUPERVISOR = value; }
        }
        private string m_CC_TRABAJADOR;
        public string CC_TRABAJADOR
        {
            get { return m_CC_TRABAJADOR; }
            set { m_CC_TRABAJADOR = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private DateTime m_FECHA;
        public DateTime FECHA
        {
            get { return m_FECHA; }
            set { m_FECHA = value; }
        }
        private string m_USER_REGISTRA;
        public string USER_REGISTRA
        {
            get { return m_USER_REGISTRA; }
            set { m_USER_REGISTRA = value; }
        }
        private string m_IP_CENTRO;
        public string IP_CENTRO
        {
            get { return m_IP_CENTRO; }
            set { m_IP_CENTRO = value; }
        }
        private int m_IDE_RESPONSABLE;
        public int IDE_RESPONSABLE
        {
            get { return m_IDE_RESPONSABLE; }
            set { m_IDE_RESPONSABLE = value; }
        }
    }
}
