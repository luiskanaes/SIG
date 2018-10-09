using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_RRHH_PERSONAL_EMPRESA
    {
        private string m_ID_DNI;
        public string ID_DNI
        {
            get { return m_ID_DNI; }
            set { m_ID_DNI = value; }
        }
        private string m_NOMBRE_COMPLETO;
        public string NOMBRE_COMPLETO
        {
            get { return m_NOMBRE_COMPLETO; }
            set { m_NOMBRE_COMPLETO = value; }
        }
        private string m_FECHA_NACIMIENTO;
        public string FECHA_NACIMIENTO
        {
            get { return m_FECHA_NACIMIENTO; }
            set { m_FECHA_NACIMIENTO = value; }
        }
        private string m_CORREO;
        public string CORREO
        {
            get { return m_CORREO; }
            set { m_CORREO = value; }
        }
        private string m_GERENCIA_CODIGO;
        public string GERENCIA_CODIGO
        {
            get { return m_GERENCIA_CODIGO; }
            set { m_GERENCIA_CODIGO = value; }
        }
        private string m_GERENCIA_DESCRIPCION;
        public string GERENCIA_DESCRIPCION
        {
            get { return m_GERENCIA_DESCRIPCION; }
            set { m_GERENCIA_DESCRIPCION = value; }
        }
        private string m_CENTRO_COSTO;
        public string CENTRO_COSTO
        {
            get { return m_CENTRO_COSTO; }
            set { m_CENTRO_COSTO = value; }
        }
        private string m_FECHA_INGRESO;
        public string FECHA_INGRESO
        {
            get { return m_FECHA_INGRESO; }
            set { m_FECHA_INGRESO = value; }
        }
        private string m_CARGO_CODIGO;
        public string CARGO_CODIGO
        {
            get { return m_CARGO_CODIGO; }
            set { m_CARGO_CODIGO = value; }
        }
        private string m_CARGO_DESCRIPCION;
        public string CARGO_DESCRIPCION
        {
            get { return m_CARGO_DESCRIPCION; }
            set { m_CARGO_DESCRIPCION = value; }
        }
        private string m_UBICACION_DESCRIPCION;
        public string UBICACION_DESCRIPCION
        {
            get { return m_UBICACION_DESCRIPCION; }
            set { m_UBICACION_DESCRIPCION = value; }
        }
        private string m_UBICACION_CODIGO;
        public string UBICACION_CODIGO
        {
            get { return m_UBICACION_CODIGO; }
            set { m_UBICACION_CODIGO = value; }
        }
        private string m_SUPERVISOR;
        public string SUPERVISOR
        {
            get { return m_SUPERVISOR; }
            set { m_SUPERVISOR = value; }
        }
        private string m_SUPERVISOR_DNI;
        public string SUPERVISOR_DNI
        {
            get { return m_SUPERVISOR_DNI; }
            set { m_SUPERVISOR_DNI = value; }
        }
        private string m_SUPERSIVOR_EMAIL;
        public string SUPERSIVOR_EMAIL
        {
            get { return m_SUPERSIVOR_EMAIL; }
            set { m_SUPERSIVOR_EMAIL = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private string m_FECHA_REGISTRO;
        public string FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private int m_IDE_EMPRESA;
        public int IDE_EMPRESA
        {
            get { return m_IDE_EMPRESA; }
            set { m_IDE_EMPRESA = value; }
        }
        private Decimal m_PUNTO_BRAVO;
        public Decimal PUNTO_BRAVO
        {
            get { return m_PUNTO_BRAVO; }
            set { m_PUNTO_BRAVO = value; }
        }

    }
}
