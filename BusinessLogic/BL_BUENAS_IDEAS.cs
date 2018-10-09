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
    public class BL_BUENAS_IDEAS
    {
        public int uspINS_BUENAS_IDEAS(BE_BUENAS_IDEAS oBE)
        {
            try
            {
                return new DA_BUENAS_IDEAS().uspINS_BUENAS_IDEAS(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_BUENAS_IDEAS_USER(string DNI_EVALUADOR)
        {
            return new DA_BUENAS_IDEAS().uspSEL_BUENAS_IDEAS_USER(DNI_EVALUADOR);
        }
        public DataTable uspSEL_BUENAS_IDEAS_TODOS(string FLG_ETAPAS)
        {
            return new DA_BUENAS_IDEAS().uspSEL_BUENAS_IDEAS_TODOS(FLG_ETAPAS);
        }
        public DataTable uspSEL_BUENA_IDEA_PROCESAR(int id, string codigo, int punto, string sustento_rechazo, string usuario)
        {
            return new DA_BUENAS_IDEAS().uspSEL_BUENA_IDEA_PROCESAR(id, codigo, punto, sustento_rechazo, usuario);
        }
        public DataTable SP_EnviarCorreo_BuenIdea(string id)
        {
            return new DA_BUENAS_IDEAS().SP_EnviarCorreo_BuenIdea(id);
        }
        public DataTable uspSEL_BUENAS_IDEAS_ID(int ID)
        {
            return new DA_BUENAS_IDEAS().uspSEL_BUENAS_IDEAS_ID(ID);
        }
        
    }
}
