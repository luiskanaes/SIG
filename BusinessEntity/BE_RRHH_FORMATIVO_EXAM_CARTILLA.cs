using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_FORMATIVO_EXAM_CARTILLA
    {
        private int m_IDE_EXAMEN;
        public int IDE_EXAMEN
        {
            get { return m_IDE_EXAMEN; }
            set { m_IDE_EXAMEN = value; }
        }
        private int m_IDE_PREGUNTA;
        public int IDE_PREGUNTA
        {
            get { return m_IDE_PREGUNTA; }
            set { m_IDE_PREGUNTA = value; }
        }
        private int m_IDE_NOTA;
        public int IDE_NOTA
        {
            get { return m_IDE_NOTA; }
            set { m_IDE_NOTA = value; }
        }
        private int m_VALOR;
        public int VALOR
        {
            get { return m_VALOR; }
            set { m_VALOR = value; }
        }
        private string m_EVALUADOR;
        public string EVALUADOR
        {
            get { return m_EVALUADOR; }
            set { m_EVALUADOR = value; }
        }
        private string m_EVALUADO;
        public string EVALUADO
        {
            get { return m_EVALUADO; }
            set { m_EVALUADO = value; }
        }
        private string  m_FECHA_EVAL;
        public string FECHA_EVAL
        {
            get { return m_FECHA_EVAL; }
            set { m_FECHA_EVAL = value; }
        }
        private int m_IDE_EVAL_EXAMEN;
        public int IDE_EVAL_EXAMEN
        {
            get { return m_IDE_EVAL_EXAMEN; }
            set { m_IDE_EVAL_EXAMEN = value; }
        }
        private int m_IDE_PROGRAMA;
        public int IDE_PROGRAMA
        {
            get { return m_IDE_PROGRAMA; }
            set { m_IDE_PROGRAMA = value; }
        }
    }
}
