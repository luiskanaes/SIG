using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_FORMATIVO_FASE
    {
        private int m_IDE_FASE;
        public int IDE_FASE
        {
            get { return m_IDE_FASE; }
            set { m_IDE_FASE = value; }
        }
        private string m_PROYECTO;
        public string PROYECTO
        {
            get { return m_PROYECTO; }
            set { m_PROYECTO = value; }
        }
        private string m_CENTRO_COSTO;
        public string CENTRO_COSTO
        {
            get { return m_CENTRO_COSTO; }
            set { m_CENTRO_COSTO = value; }
        }
        private string m_JEFE;
        public string JEFE
        {
            get { return m_JEFE; }
            set { m_JEFE = value; }
        }
        private string m_JEFE_DNI;
        public string JEFE_DNI
        {
            get { return m_JEFE_DNI; }
            set { m_JEFE_DNI = value; }
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
        private string m_UBICACION;
        public string UBICACION
        {
            get { return m_UBICACION; }
            set { m_UBICACION = value; }
        }
        private string m_CARGO;
        public string CARGO
        {
            get { return m_CARGO; }
            set { m_CARGO = value; }
        }
        private Decimal m_PTO_DESEMPENIO;
        public Decimal PTO_DESEMPENIO
        {
            get { return m_PTO_DESEMPENIO; }
            set { m_PTO_DESEMPENIO = value; }
        }
        private Decimal m_PTO_SEGUIMIENTO;
        public Decimal PTO_SEGUIMIENTO
        {
            get { return m_PTO_SEGUIMIENTO; }
            set { m_PTO_SEGUIMIENTO = value; }
        }
        private int m_FLG_EXAMEN;
        public int FLG_EXAMEN
        {
            get { return m_FLG_EXAMEN; }
            set { m_FLG_EXAMEN = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }
        private DateTime m_FECHA_REGISTRO;
        public DateTime FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private int m_IDE_FICHA;
        public int IDE_FICHA
        {
            get { return m_IDE_FICHA; }
            set { m_IDE_FICHA = value; }
        }
        private string m_OBSERVACIONES;
        public string OBSERVACIONES
        {
            get { return m_OBSERVACIONES; }
            set { m_OBSERVACIONES = value; }
        }
        private string m_CORREO_JEFE;
        public string CORREO_JEFE
        {
            get { return m_CORREO_JEFE; }
            set { m_CORREO_JEFE = value; }
        }
        private int m_ID_EMPRESA;
        public int ID_EMPRESA
        {
            get { return m_ID_EMPRESA; }
            set { m_ID_EMPRESA = value; }
        }
    }
}
