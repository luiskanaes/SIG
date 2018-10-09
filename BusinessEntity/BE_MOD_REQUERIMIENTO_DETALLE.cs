using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_MOD_REQUERIMIENTO_DETALLE
    {
        private int m_IDE_REQUERIMIENTO;
        public int IDE_REQUERIMIENTO
        {
            get { return m_IDE_REQUERIMIENTO; }
            set { m_IDE_REQUERIMIENTO = value; }
        }
        private int m_IDE_MOD;
        public int IDE_MOD
        {
            get { return m_IDE_MOD; }
            set { m_IDE_MOD = value; }
        }
        private int m_IDE_CATEGORIA;
        public int IDE_CATEGORIA
        {
            get { return m_IDE_CATEGORIA; }
            set { m_IDE_CATEGORIA = value; }
        }
        private int m_IDE_ESPECIALIDAD;
        public int IDE_ESPECIALIDAD
        {
            get { return m_IDE_ESPECIALIDAD; }
            set { m_IDE_ESPECIALIDAD = value; }
        }
        private int m_CANTIDAD;
        public int CANTIDAD
        {
            get { return m_CANTIDAD; }
            set { m_CANTIDAD = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private string m_IP_CENTRO;
        public string IP_CENTRO
        {
            get { return m_IP_CENTRO; }
            set { m_IP_CENTRO = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }
    }
}