using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RequerimientoMovil
    {
        private int m_IdRequerimiento;
        public int IdRequerimiento
        {
            get { return m_IdRequerimiento; }
            set { m_IdRequerimiento = value; }
        }
        private string m_FechaSolicitud;
        public string FechaSolicitud
        {
            get { return m_FechaSolicitud; }
            set { m_FechaSolicitud = value; }
        }
        private int m_IdEmpresaPK;
        public int IdEmpresaPK
        {
            get { return m_IdEmpresaPK; }
            set { m_IdEmpresaPK = value; }
        }
        private string m_centro_costo;
        public string centro_costo
        {
            get { return m_centro_costo; }
            set { m_centro_costo = value; }
        }
        private string m_Cod_registro;
        public string Cod_registro
        {
            get { return m_Cod_registro; }
            set { m_Cod_registro = value; }
        }
        private string m_Requ_Numero;
        public string Requ_Numero
        {
            get { return m_Requ_Numero; }
            set { m_Requ_Numero = value; }
        }
    }
}
