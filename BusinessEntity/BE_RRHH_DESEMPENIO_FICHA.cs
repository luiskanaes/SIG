using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_DESEMPENIO_FICHA
    {
        private int m_IDE_DESEMPENIO;
        public int IDE_DESEMPENIO
        {
            get { return m_IDE_DESEMPENIO; }
            set { m_IDE_DESEMPENIO = value; }
        }
        private string m_DNI;
        public string DNI
        {
            get { return m_DNI; }
            set { m_DNI = value; }
        }
        private int m_ANIO;
        public int ANIO
        {
            get { return m_ANIO; }
            set { m_ANIO = value; }
        }
        private int m_IDE_PERFIL;
        public int IDE_PERFIL
        {
            get { return m_IDE_PERFIL; }
            set { m_IDE_PERFIL = value; }
        }
        private int m_IDE_ETAPA;
        public int IDE_ETAPA
        {
            get { return m_IDE_ETAPA; }
            set { m_IDE_ETAPA = value; }
        }
        private int m_IDE_FAMILIA;
        public int IDE_FAMILIA
        {
            get { return m_IDE_FAMILIA; }
            set { m_IDE_FAMILIA = value; }
        }
        private string m_CODIGO_GERENCIA;
        public string CODIGO_GERENCIA
        {
            get { return m_CODIGO_GERENCIA; }
            set { m_CODIGO_GERENCIA = value; }
        }
        private string m_IP_CENTRO;
        public string IP_CENTRO
        {
            get { return m_IP_CENTRO; }
            set { m_IP_CENTRO = value; }
        }
        private string m_CCENTRO;
        public string CCENTRO
        {
            get { return m_CCENTRO; }
            set { m_CCENTRO = value; }
        }
        private string m_CARGO;
        public string CARGO
        {
            get { return m_CARGO; }
            set { m_CARGO = value; }
        }
        private string m_DNI_JEFE;
        public string DNI_JEFE
        {
            get { return m_DNI_JEFE; }
            set { m_DNI_JEFE = value; }
        }
        private string m_DNI_GERENTE;
        public string DNI_GERENTE
        {
            get { return m_DNI_GERENTE; }
            set { m_DNI_GERENTE = value; }
        }
        private int m_IDE_ETAPAS;
        public int IDE_ETAPAS
        {
            get { return m_IDE_ETAPAS; }
            set { m_IDE_ETAPAS = value; }
        }
        private string m_COMENTARIOS;
        public string COMENTARIOS
        {
            get { return m_COMENTARIOS; }
            set { m_COMENTARIOS = value; }
        }
        private string m_USER_REGISTRA;
        public string USER_REGISTRA
        {
            get { return m_USER_REGISTRA; }
            set { m_USER_REGISTRA = value; }
        }
        private DateTime m_FECHA_REGISTRO;
        public DateTime FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }

    }
}
