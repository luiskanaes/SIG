using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntity
{
    public class BE_ASIGNACION_RECURSOS
    {
        private int m_IDE_ASIGNACION;
        public int IDE_ASIGNACION
        {
            get { return m_IDE_ASIGNACION; }
            set { m_IDE_ASIGNACION = value; }
        }
        private int m_ID_DETALLE_REQUERIMIENTO_PERSONAL;
        public int ID_DETALLE_REQUERIMIENTO_PERSONAL
        {
            get { return m_ID_DETALLE_REQUERIMIENTO_PERSONAL; }
            set { m_ID_DETALLE_REQUERIMIENTO_PERSONAL = value; }
        }
        private string m_DES_OBSERVACIONES;
        public string DES_OBSERVACIONES
        {
            get { return m_DES_OBSERVACIONES; }
            set { m_DES_OBSERVACIONES = value; }
        }
        private int m_FLG_ESTADO;
        public int FLG_ESTADO
        {
            get { return m_FLG_ESTADO; }
            set { m_FLG_ESTADO = value; }
        }
        private string  m_FECHA_ERGISTRO;
        public string FECHA_ERGISTRO
        {
            get { return m_FECHA_ERGISTRO; }
            set { m_FECHA_ERGISTRO = value; }
        }
        private int m_IDE_UBICACION;
        public int IDE_UBICACION
        {
            get { return m_IDE_UBICACION; }
            set { m_IDE_UBICACION = value; }
        }
    }

}
