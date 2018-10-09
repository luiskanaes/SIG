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
   public  class DA_ENLACES_INTRANET
    {
        Util oUtilitarios = new Util();
        public DataTable LISTAR_EXAMENES_X_ESTUDIANTE()
        {
            return oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_ENLANCES_INTRANET");
        }
    }
}
