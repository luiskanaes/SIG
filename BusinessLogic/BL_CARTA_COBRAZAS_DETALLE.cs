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
    public class BL_CARTA_COBRAZAS_DETALLE
    {
        public int uspUPD_CARTA_COBRAZAS_DETALLE(BE_CARTA_COBRAZAS_DETALLE oBE)
        {
            try
            {
                return new DA_CARTA_COBRAZAS_DETALLE().uspUPD_CARTA_COBRAZAS_DETALLE(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_CARTA_COBRAZAS_DETALLE(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().uspSEL_CARTA_COBRAZAS_DETALLE(IDE_CARTA);
        }
        public DataTable uspSEL_CARTA_COBRAZAS_DETALLE_ID(int IDE_DETALLE)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().uspSEL_CARTA_COBRAZAS_DETALLE_ID(IDE_DETALLE);
        }
        public DataTable uspDEL_CARTA_COBRAZAS_DETALLE_POR_ID(int IDE_DETALLE)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().uspDEL_CARTA_COBRAZAS_DETALLE_POR_ID(IDE_DETALLE);
        }
        public DataTable uspUPD_CARTA_COBRAZAS_APROBACIONES(int IDE_APROBACION, int TIPO , string SUSTENTO)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().uspUPD_CARTA_COBRAZAS_APROBACIONES(IDE_APROBACION,TIPO , SUSTENTO);
        }
        public DataTable SP_CORREO_APROBACIONES_CARTACOBRANZA(int IDE_CARTA, int TIPO_CARGO ,string URLSSK, string ENVIA_CORREO,string APROBADOR, int FLG_APROBAR)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().SP_CORREO_APROBACIONES_CARTACOBRANZA(IDE_CARTA,TIPO_CARGO, URLSSK,ENVIA_CORREO, APROBADOR, FLG_APROBAR);
        }
        public DataTable SP_CORREO_APROBACIONES_CARTACOBRANZA_SAP(int IDE_CARTA)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().SP_CORREO_APROBACIONES_CARTACOBRANZA_SAP(IDE_CARTA);
        }
        public DataTable SP_CORREO_APROBACIONES_CARTACOBRANZA_TODOS(int IDE_CARTA, int TIPO_CARGO)
        {
            return new DA_CARTA_COBRAZAS_DETALLE().SP_CORREO_APROBACIONES_CARTACOBRANZA_TODOS(IDE_CARTA, TIPO_CARGO);
        }
    }
}
