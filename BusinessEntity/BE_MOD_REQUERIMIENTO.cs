using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public class BE_MOD_REQUERIMIENTO
    {
        private int m_IDE_MOD;
        public int IDE_MOD
        {
            get { return m_IDE_MOD; }
            set { m_IDE_MOD = value; }
        }
        private string m_CODIGO;
        public string CODIGO
        {
            get { return m_CODIGO; }
            set { m_CODIGO = value; }
        }
        private string  m_FECHA_REQUERIMIENTO;
        public string FECHA_REQUERIMIENTO
        {
            get { return m_FECHA_REQUERIMIENTO; }
            set { m_FECHA_REQUERIMIENTO = value; }
        }
        private string m_SOLICITANTE;
        public string SOLICITANTE
        {
            get { return m_SOLICITANTE; }
            set { m_SOLICITANTE = value; }
        }
        private DateTime m_FEC_SOLICITANTE;
        public DateTime FEC_SOLICITANTE
        {
            get { return m_FEC_SOLICITANTE; }
            set { m_FEC_SOLICITANTE = value; }
        }
        private string m_JEFE_AREA;
        public string JEFE_AREA
        {
            get { return m_JEFE_AREA; }
            set { m_JEFE_AREA = value; }
        }
        private DateTime m_FEC_JEFE;
        public DateTime FEC_JEFE
        {
            get { return m_FEC_JEFE; }
            set { m_FEC_JEFE = value; }
        }
        private string m_ADMINISTRADOR;
        public string ADMINISTRADOR
        {
            get { return m_ADMINISTRADOR; }
            set { m_ADMINISTRADOR = value; }
        }
        private DateTime m_FEC_ADM;
        public DateTime FEC_ADM
        {
            get { return m_FEC_ADM; }
            set { m_FEC_ADM = value; }
        }
        private string m_GERENTE;
        public string GERENTE
        {
            get { return m_GERENTE; }
            set { m_GERENTE = value; }
        }
        private DateTime m_FEC_GERENTE;
        public DateTime FEC_GERENTE
        {
            get { return m_FEC_GERENTE; }
            set { m_FEC_GERENTE = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
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
        private DateTime m_FECHA_REGISTRO;
        public DateTime FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }

    }
}
