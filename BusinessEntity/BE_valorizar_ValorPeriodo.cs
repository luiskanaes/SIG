using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_valorizar_ValorPeriodo
    {
        private int m_ide_valor;
        public int ide_valor
        {
            get { return m_ide_valor; }
            set { m_ide_valor = value; }
        }
        private string m_Requ_Numero;
        public string Requ_Numero
        {
            get { return m_Requ_Numero; }
            set { m_Requ_Numero = value; }
        }
        private string m_Reqd_CodLinea;
        public string Reqd_CodLinea
        {
            get { return m_Reqd_CodLinea; }
            set { m_Reqd_CodLinea = value; }
        }
        private string m_Reqs_Correlativo;
        public string Reqs_Correlativo
        {
            get { return m_Reqs_Correlativo; }
            set { m_Reqs_Correlativo = value; }
        }
        private string m_V_FECHA_INICIO_VAL;
        public string V_FECHA_INICIO_VAL
        {
            get { return m_V_FECHA_INICIO_VAL; }
            set { m_V_FECHA_INICIO_VAL = value; }
        }
        private string m_V_FECHA_FIN_VAL;
        public string V_FECHA_FIN_VAL
        {
            get { return m_V_FECHA_FIN_VAL; }
            set { m_V_FECHA_FIN_VAL = value; }
        }
        private Decimal m_V_TARIFA_DIA;
        public Decimal V_TARIFA_DIA
        {
            get { return m_V_TARIFA_DIA; }
            set { m_V_TARIFA_DIA = value; }
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


    }
}
