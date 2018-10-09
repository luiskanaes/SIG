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
   public  class BL_HSEC_ANUNCIOS
    {
        public int uspINS_HSEC_ANUNCIOS(BE_HSEC_ANUNCIOS oBESOl)
        {
            try
            {
                return new DA_HSEC_ANUNCIOS().uspINS_HSEC_ANUNCIOS(oBESOl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_HSEC_ANUNCIOS_POR_TIPO(string TIPO_ANUNCIO, string FLG_VISIBLE)
        {
            return new DA_HSEC_ANUNCIOS().uspSEL_HSEC_ANUNCIOS_POR_TIPO(TIPO_ANUNCIO, FLG_VISIBLE);
        }
        public DataTable uspSEL_HSEC_ANUNCIOS_BUSCAR(string ANUNCIO, string FLG_VISIBLE)
        {
            return new DA_HSEC_ANUNCIOS().uspSEL_HSEC_ANUNCIOS_BUSCAR(ANUNCIO, FLG_VISIBLE);
        }
        public DataTable uspSEL_HSEC_ANUNCIOS_POR_ID(string IDE_ANUNCIO)
        {
            return new DA_HSEC_ANUNCIOS().uspSEL_HSEC_ANUNCIOS_POR_ID(IDE_ANUNCIO);
        }
        public DataTable uspCONSULTAR_POPUP_ANUNCIOS()
        {
            return new DA_HSEC_ANUNCIOS().uspCONSULTAR_POPUP_ANUNCIOS();
        }

        public DataTable uspUPDATE_POPUP_LECTURA(string USUARIO)
        {
            return new DA_HSEC_ANUNCIOS().uspUPDATE_POPUP_LECTURA(USUARIO);
        }

    }
}
