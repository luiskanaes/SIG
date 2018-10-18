using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;

namespace DataAccess
{
   
    public class DA_TAREO_EMPLEADOS
    {
        Util oUtilitarios = new Util();

        public DataTable SP_BUSCAR_LISTA_UBICACIONES(string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_BUSCAR_LISTA_UBICACIONES", empresa);
        }
        public DataTable SP_BUSCAR_EMP_DISPONIBLES(string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_BUSCAR_EMP_DISPONIBLES", empresa);
        }
        public DataTable SP_LISTA_EMP_LIDER_GRUPO(string empresa)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_LISTA_EMP_LIDER_GRUPO", empresa);
        }
        public DataTable SP_INSERTAR_EMP_LIDER_GRUPO(string empresa, string DNI)
        {
            return oUtilitarios.EjecutaDatatable("dbo.SP_INSERTAR_EMP_LIDER_GRUPO", empresa,DNI);
        }
        

        

    }
}
