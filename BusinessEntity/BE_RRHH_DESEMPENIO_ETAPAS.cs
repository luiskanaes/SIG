using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_DESEMPENIO_ETAPAS
    {
        private int m_IDE_ETAPAS;
        public int IDE_ETAPAS
        {
            get { return m_IDE_ETAPAS; }
            set { m_IDE_ETAPAS = value; }
        }
        private int m_ANIO;
        public int ANIO
        {
            get { return m_ANIO; }
            set { m_ANIO = value; }
        }
        private string m_DETALLE_ETAPA;
        public string DETALLE_ETAPA
        {
            get { return m_DETALLE_ETAPA; }
            set { m_DETALLE_ETAPA = value; }
        }
        private string m_DESCRIPCION;
        public string DESCRIPCION
        {
            get { return m_DESCRIPCION; }
            set { m_DESCRIPCION = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
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
        private int m_FLG_VISIBLE;
        public int FLG_VISIBLE
        {
            get { return m_FLG_VISIBLE; }
            set { m_FLG_VISIBLE = value; }
        }
        private int m_CODIGO_ETAPA;
        public int CODIGO_ETAPA
        {
            get { return m_CODIGO_ETAPA; }
            set { m_CODIGO_ETAPA = value; }
        }
        private string m_CECOS;
        public string CECOS
        {
            get { return m_CECOS; }
            set { m_CECOS = value; }
        }
    }
}
