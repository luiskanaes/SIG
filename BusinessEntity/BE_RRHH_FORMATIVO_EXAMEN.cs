using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_FORMATIVO_EXAMEN
    {
        private int m_IDE_EVAL_EXAMEN;
        public int IDE_EVAL_EXAMEN
        {
            get { return m_IDE_EVAL_EXAMEN; }
            set { m_IDE_EVAL_EXAMEN = value; }
        }
        private int m_IDE_FICHA;
        public int IDE_FICHA
        {
            get { return m_IDE_FICHA; }
            set { m_IDE_FICHA = value; }
        }
        private string m_DNI_EVALUADOR;
        public string DNI_EVALUADOR
        {
            get { return m_DNI_EVALUADOR; }
            set { m_DNI_EVALUADOR = value; }
        }
        private string m_DNI_EVALUADO;
        public string DNI_EVALUADO
        {
            get { return m_DNI_EVALUADO; }
            set { m_DNI_EVALUADO = value; }
        }
        private string m_INTRODUCCION;
        public string INTRODUCCION
        {
            get { return m_INTRODUCCION; }
            set { m_INTRODUCCION = value; }
        }
        private string m_FECHA_EXAMEN;
        public string  FECHA_EXAMEN
        {
            get { return m_FECHA_EXAMEN; }
            set { m_FECHA_EXAMEN = value; }
        }
        private string m_CCENTRO;
        public string CCENTRO
        {
            get { return m_CCENTRO; }
            set { m_CCENTRO = value; }
        }
        private string m_FORTALEZAS;
        public string FORTALEZAS
        {
            get { return m_FORTALEZAS; }
            set { m_FORTALEZAS = value; }
        }
        private string m_MEJORAS;
        public string MEJORAS
        {
            get { return m_MEJORAS; }
            set { m_MEJORAS = value; }
        }
        private string m_COMPROMISOS;
        public string COMPROMISOS
        {
            get { return m_COMPROMISOS; }
            set { m_COMPROMISOS = value; }
        }
        private Decimal m_PUNTO;
        public Decimal PUNTO
        {
            get { return m_PUNTO; }
            set { m_PUNTO = value; }
        }
        private int m_IDE_TIPO_EXA;
        public int IDE_TIPO_EXA
        {
            get { return m_IDE_TIPO_EXA; }
            set { m_IDE_TIPO_EXA = value; }
        }
        private int m_IDE_FASE;
        public int IDE_FASE
        {
            get { return m_IDE_FASE; }
            set { m_IDE_FASE = value; }
        }

    }
}
