using BusinessEntity;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BL_TBSOLICITUD_PERMISOS_DIA
    {
        public int MANT_TBSOLICITUD_PERMISOS_DIA_INSERT_DATOS(BE_TBSOLICITUD_PERMISOS_DIA objSolDia)
        {
            try
            {
                return new DA_TBSOLICITUD_PERMISOS_DIA().MANT_TBSOLICITUD_PERMISOS_DIA_INSERT_DATOS(objSolDia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable MANT_TBSOLICITUD_PERMISOS_DIA_SELECT_DATOS(string ide_permiso)
        {
            return new DA_TBSOLICITUD_PERMISOS_DIA().MANT_TBSOLICITUD_PERMISOS_DIA_SELECT_DATOS(ide_permiso);
        }
        public DataTable MANT_TBSOLICITUD_PERMISOS_DIA_DELETE_DATOS(string ide_detalle)
        {
            return new DA_TBSOLICITUD_PERMISOS_DIA().MANT_TBSOLICITUD_PERMISOS_DIA_DELETE_DATOS(ide_detalle);
        }
    }
}
