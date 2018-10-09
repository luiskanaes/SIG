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
    public class BL_SOLPED
    {
        public int uspINS_SOLPED(BE_SOLPED oBE)
        {
            try
            {
                return new DA_SOLPED().uspINS_SOLPED(oBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable uspSEL_SOLPED_BANDEJA(string IDE_USUARIO,string ESTADO, string ANIO, string txtFecSol_F, string  txtNOM_CREADO_F, string txtNOM_SOLICITA_F, string  txtTICKET_F , string centro, string tipo,string  IDE_EMPRESA)
        {
            return new DA_SOLPED().uspSEL_SOLPED_BANDEJA(IDE_USUARIO, ESTADO, ANIO, txtFecSol_F, txtNOM_CREADO_F, txtNOM_SOLICITA_F, txtTICKET_F, centro, tipo, IDE_EMPRESA);
        }
        public DataTable uspSEL_SOLPED_USUARIO(string IDE_USUARIO, string ESTADO, string ANIO)
        {
            return new DA_SOLPED().uspSEL_SOLPED_USUARIO(IDE_USUARIO, ESTADO, ANIO);
        }
        public DataTable uspUPD_SOLPED_PROCESAR(int id, string codigo, string sustento_rechazo, string usuario, string solped, string tipo)
        {
            return new DA_SOLPED().uspUPD_SOLPED_PROCESAR(id, codigo, sustento_rechazo, usuario,solped,tipo );
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS( string usuario, string tipo, string empresa)
        {
            return new DA_SOLPED().uspSEL_RESPONSABLE_PROCESOS( usuario,  tipo, empresa);
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_EMPRESA(string usuario, string tipo)
        {
            return new DA_SOLPED().uspSEL_RESPONSABLE_PROCESOS_EMPRESA(usuario, tipo);
        }
        public DataTable SP_ENVIARCORREO_SOLPED(int IDE_SOLPED , string IDE_USUARIO)
        {
            return new DA_SOLPED().SP_ENVIARCORREO_SOLPED(IDE_SOLPED,IDE_USUARIO);
        }
        public DataTable SP_ENVIARCORREO_SOLPED_RPTA(int IDE_SOLPED )
        {
            return new DA_SOLPED().SP_ENVIARCORREO_SOLPED_RPTA(IDE_SOLPED);
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_CENTRO(string usuario, string tipo)
        {
            return new DA_SOLPED().uspSEL_RESPONSABLE_PROCESOS_CENTRO(usuario, tipo);
        }
        public DataTable uspSEL_RESPONSABLE_PROCESOS_SOLPED(string usuario, string tipo,string empresa)
        {
            return new DA_SOLPED().uspSEL_RESPONSABLE_PROCESOS_SOLPED(usuario, tipo, empresa);
        }
    }
}
