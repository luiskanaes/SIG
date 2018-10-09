using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_ESTRELLA_NOMINACION
    {
        private int m_IDE_NOMINACION;
        public int IDE_NOMINACION
        {
            get { return m_IDE_NOMINACION; }
            set { m_IDE_NOMINACION = value; }
        }
        private string m_DNI_EVALUADO;
        public string DNI_EVALUADO
        {
            get { return m_DNI_EVALUADO; }
            set { m_DNI_EVALUADO = value; }
        }
        private string m_DNI_SUPERVISOR;
        public string DNI_SUPERVISOR
        {
            get { return m_DNI_SUPERVISOR; }
            set { m_DNI_SUPERVISOR = value; }
        }
        private string  m_FECHA_EVALUACION;
        public string FECHA_EVALUACION
        {
            get { return m_FECHA_EVALUACION; }
            set { m_FECHA_EVALUACION = value; }
        }
        private int m_IDE_FACTOR;
        public int IDE_FACTOR
        {
            get { return m_IDE_FACTOR; }
            set { m_IDE_FACTOR = value; }
        }
        private string m_DES_FACTOR;
        public string DES_FACTOR
        {
            get { return m_DES_FACTOR; }
            set { m_DES_FACTOR = value; }
        }
        private string m_SUSTENTO;
        public string SUSTENTO
        {
            get { return m_SUSTENTO; }
            set { m_SUSTENTO = value; }
        }
        private Decimal m_PUNTOS;
        public Decimal PUNTOS
        {
            get { return m_PUNTOS; }
            set { m_PUNTOS = value; }
        }
        private int m_FLG_ETAPAS;
        public int FLG_ETAPAS
        {
            get { return m_FLG_ETAPAS; }
            set { m_FLG_ETAPAS = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private string m_SUSTENTO_RECHAZO;
        public string SUSTENTO_RECHAZO
        {
            get { return m_SUSTENTO_RECHAZO; }
            set { m_SUSTENTO_RECHAZO = value; }
        }
        private string m_FACTORES;
        public string FACTORES
        {
            get { return m_FACTORES; }
            set { m_FACTORES = value; }
        }
        private string m_CCENTRO;
        public string CCENTRO
        {
            get { return m_CCENTRO; }
            set { m_CCENTRO = value; }
        }
    }
}
