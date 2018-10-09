using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public  class BE_TBUSUARIO
    {


        private string m_IDE_USUARIO;
        public string IDE_USUARIO
        {
            get { return m_IDE_USUARIO; }
            set { m_IDE_USUARIO = value; }
        }
        private string m_NOMBRE_USUARIO;
        public string NOMBRE_USUARIO
        {
            get { return m_NOMBRE_USUARIO; }
            set { m_NOMBRE_USUARIO = value; }
        }
        private string m_DES_PASSWORD;
        public string DES_PASSWORD
        {
            get { return m_DES_PASSWORD; }
            set { m_DES_PASSWORD = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private string m_CreatedBy;
        public string CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
        private DateTime m_CreatedDate;
        public DateTime CreatedDate
        {
            get { return m_CreatedDate; }
            set { m_CreatedDate = value; }
        }
        private string m_APE_PATERNO;
        public string APE_PATERNO
        {
            get { return m_APE_PATERNO; }
            set { m_APE_PATERNO = value; }
        }
        private string m_APE_MATERNO;
        public string APE_MATERNO
        {
            get { return m_APE_MATERNO; }
            set { m_APE_MATERNO = value; }
        }
        private string m_DES_NOMBRES;
        public string DES_NOMBRES
        {
            get { return m_DES_NOMBRES; }
            set { m_DES_NOMBRES = value; }
        }
        private string m_DES_CORREO;
        public string DES_CORREO
        {
            get { return m_DES_CORREO; }
            set { m_DES_CORREO = value; }
        }
        private string m_DES_DNI;
        public string DES_DNI
        {
            get { return m_DES_DNI; }
            set { m_DES_DNI = value; }
        }
        private int m_IdSistema;
        public int IdSistema
        {
            get { return m_IdSistema; }
            set { m_IdSistema = value; }
        }

    }
}
