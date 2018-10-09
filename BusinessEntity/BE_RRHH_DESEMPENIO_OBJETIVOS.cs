using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public  class BE_RRHH_DESEMPENIO_OBJETIVOS
    {
        private int m_IDE_OBJETIVO;
        public int IDE_OBJETIVO
        {
            get { return m_IDE_OBJETIVO; }
            set { m_IDE_OBJETIVO = value; }
        }
        private int m_IDE_DESEMPENIO;
        public int IDE_DESEMPENIO
        {
            get { return m_IDE_DESEMPENIO; }
            set { m_IDE_DESEMPENIO = value; }
        }
        private string m_OBJETIVO;
        public string OBJETIVO
        {
            get { return m_OBJETIVO; }
            set { m_OBJETIVO = value; }
        }
        private string m_DNI_PERSONA;
        public string DNI_PERSONA
        {
            get { return m_DNI_PERSONA; }
            set { m_DNI_PERSONA = value; }
        }
        private Decimal m_PESO;
        public Decimal PESO
        {
            get { return m_PESO; }
            set { m_PESO = value; }
        }
        private string m_INDICADOR;
        public string INDICADOR
        {
            get { return m_INDICADOR; }
            set { m_INDICADOR = value; }
        }
        private string m_INICIO;
        public string INICIO
        {
            get { return m_INICIO; }
            set { m_INICIO = value; }
        }
        private string m_TERMINO;
        public string TERMINO
        {
            get { return m_TERMINO; }
            set { m_TERMINO = value; }
        }
        private int m_FLG_APROBADO;
        public int FLG_APROBADO
        {
            get { return m_FLG_APROBADO; }
            set { m_FLG_APROBADO = value; }
        }
        private DateTime m_FECHA_APROBADO;
        public DateTime FECHA_APROBADO
        {
            get { return m_FECHA_APROBADO; }
            set { m_FECHA_APROBADO = value; }
        }
        private int m_U_CALIFICACION_PERSONA;
        public int U_CALIFICACION_PERSONA
        {
            get { return m_U_CALIFICACION_PERSONA; }
            set { m_U_CALIFICACION_PERSONA = value; }
        }
        private string m_U_COMENTARIOS_PERSONA;
        public string U_COMENTARIOS_PERSONA
        {
            get { return m_U_COMENTARIOS_PERSONA; }
            set { m_U_COMENTARIOS_PERSONA = value; }
        }
        private DateTime m_U_FECHA_EVAL_PERSONA;
        public DateTime U_FECHA_EVAL_PERSONA
        {
            get { return m_U_FECHA_EVAL_PERSONA; }
            set { m_U_FECHA_EVAL_PERSONA = value; }
        }
        private int m_J_CALIFICACION_JEFE;
        public int J_CALIFICACION_JEFE
        {
            get { return m_J_CALIFICACION_JEFE; }
            set { m_J_CALIFICACION_JEFE = value; }
        }
        private string m_J_COMENTARIOS_JEFE;
        public string J_COMENTARIOS_JEFE
        {
            get { return m_J_COMENTARIOS_JEFE; }
            set { m_J_COMENTARIOS_JEFE = value; }
        }
        private DateTime m_J_FECHA_EVAL_JEFE;
        public DateTime J_FECHA_EVAL_JEFE
        {
            get { return m_J_FECHA_EVAL_JEFE; }
            set { m_J_FECHA_EVAL_JEFE = value; }
        }
        private string m_J_USER_JEFE;
        public string J_USER_JEFE
        {
            get { return m_J_USER_JEFE; }
            set { m_J_USER_JEFE = value; }
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
        private int m_ANIO;
        public int ANIO
        {
            get { return m_ANIO; }
            set { m_ANIO = value; }
        }
        private string m_FECHA_AMPLIACION;
        public string FECHA_AMPLIACION
        {
            get { return m_FECHA_AMPLIACION; }
            set { m_FECHA_AMPLIACION = value; }
        }
        private string m_APROBAR;
        public string APROBAR
        {
            get { return m_APROBAR; }
            set { m_APROBAR = value; }
        }
    }
}
