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
    public class BL_INTRANET
    {
        public DataTable SP_LISTAR_BANNER(int tipo, string ESTADO)
        {
            return new DA_INTRANET().SP_LISTAR_BANNER(tipo, ESTADO);
        }
        public DataTable SP_ELIMINAR_LISTAR_BANNER(int IDE_BANNER)
        {
            return new DA_INTRANET().SP_ELIMINAR_LISTAR_BANNER(IDE_BANNER);
        }
        public DataTable SP_LISTAR_BANNER_IDE(int IDE_BANNER)
        {
            return new DA_INTRANET().SP_LISTAR_BANNER_IDE(IDE_BANNER);
        }
        public DataTable SP_LISTAR_BANNER_FILE(int tipo, string ESTADO)
        {
            return new DA_INTRANET().SP_LISTAR_BANNER_FILE(tipo, ESTADO);
        }
        public DataTable SP_LISTAR_BANNER_DESCRIPCION(int tipo, string ESTADO, string DESCRIPCION)
        {
            return new DA_INTRANET().SP_LISTAR_BANNER_DESCRIPCION(tipo, ESTADO, DESCRIPCION);
        }
        public DataTable SP_UPDATE_ANUNCIOS_PERSONAL(int tipo, string  DNI, string DESCRIPCION , string ESTADO)
        {
            return new DA_INTRANET().SP_UPDATE_ANUNCIOS_PERSONAL(tipo, DNI, DESCRIPCION, ESTADO);
        }
        public DataTable SP_LISTAR_ANUNCIOS_PERSONAL(int tipo)
        {
            return new DA_INTRANET().SP_LISTAR_ANUNCIOS_PERSONAL(tipo);
        }
        public DataTable SP_LISTAR_ANIVERSARIO(int tipo)
        {
            return new DA_INTRANET().SP_LISTAR_ANIVERSARIO(tipo);
        }
        public DataTable SP_Listar_Directorio_Corporativo(string  nombre)
        {
            return new DA_INTRANET().SP_Listar_Directorio_Corporativo(nombre);
        }
    }
}
