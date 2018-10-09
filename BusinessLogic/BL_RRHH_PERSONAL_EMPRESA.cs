using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using UserCode;
using System.Web;

namespace BusinessLogic
{
    public class BL_RRHH_PERSONAL_EMPRESA
    {
        public DataTable uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID(string ID_DNI)
        {
            return new DA_RRHH_PERSONAL_EMPRESA().uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID(ID_DNI);
        }
        public DataTable uspUPD_RRHH_PERSONAL_FOTOS(string ID_DNI,string FIRMA, string FOTO, byte[] Imgfirma, byte []Imgfoto )
        {
            return new DA_RRHH_PERSONAL_EMPRESA().uspUPD_RRHH_PERSONAL_FOTOS(ID_DNI, FIRMA,FOTO , Imgfirma, Imgfoto);
        }
    }
}
