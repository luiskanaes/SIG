using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessEntity;
using UserCode;
using System.Data.SqlClient;
using DataAccess.Conexion;
using System.Data;


namespace DataAccess
{
    public class DA_RRHH_PERSONAL_EMPRESA
    {
        Util oUtilitarios = new Util();
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID(string ID_DNI)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID", ID_DNI);
        }
        public DataTable uspUPD_RRHH_PERSONAL_FOTOS(string ID_DNI, string FIRMA, string FOTO, byte[] Imgfirma, byte[] Imgfoto)
        {
            return oUtilitarios.EjecutaDatatable("dbo.uspUPD_RRHH_PERSONAL_FOTOS", ID_DNI,FIRMA , FOTO, Imgfirma, Imgfoto);
        }
    }
}
