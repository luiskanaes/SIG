using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using System.Data;


namespace BusinessLogic
{
  public  class BL_TAREO_EMPLEADOS
    {
        public DataTable SP_BUSCAR_LISTA_UBICACIONES(string EMPRESA)
        {
            return new DA_TAREO_EMPLEADOS().SP_BUSCAR_LISTA_UBICACIONES(EMPRESA);
        }
        public DataTable SP_BUSCAR_EMP_DISPONIBLES(string EMPRESA)
        {
            return new DA_TAREO_EMPLEADOS().SP_BUSCAR_EMP_DISPONIBLES(EMPRESA);
        }
        public DataTable SP_INSERTAR_EMP_LIDER_GRUPO(string EMPRESA, string dni)
        {
            return new DA_TAREO_EMPLEADOS().SP_INSERTAR_EMP_LIDER_GRUPO(EMPRESA, dni);
        }
        public DataTable SP_LISTA_EMP_LIDER_GRUPO(string EMPRESA)
        {
            return new DA_TAREO_EMPLEADOS().SP_LISTA_EMP_LIDER_GRUPO(EMPRESA);
        }
        
        
    }
}
