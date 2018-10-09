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
    public class DA_INTRANET
    {
        Util oUtilitarios = new Util();
        public DataTable SP_LISTAR_BANNER(int tipo, string ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("SP_LISTAR_BANNER", tipo , ESTADO );
        }
        public DataTable SP_LISTAR_BANNER_FILE(int tipo, string ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("SP_LISTAR_BANNER_FILE", tipo, ESTADO);
        }
        public DataTable SP_ELIMINAR_LISTAR_BANNER(int IDE_BANNER)
        {
            return oUtilitarios.EjecutaDatatable("SP_ELIMINAR_LISTAR_BANNER", IDE_BANNER);
        }
        public DataTable SP_LISTAR_BANNER_IDE(int IDE_BANNER)
        {
            return oUtilitarios.EjecutaDatatable("SP_LISTAR_BANNER_IDE", IDE_BANNER);
        }
        public DataTable SP_LISTAR_BANNER_DESCRIPCION(int tipo, string ESTADO, string DESCRIPCION)
        {
            return oUtilitarios.EjecutaDatatable("SP_LISTAR_BANNER_DESCRIPCION", tipo, ESTADO, DESCRIPCION);
        }

        public DataTable SP_UPDATE_ANUNCIOS_PERSONAL(int tipo, string DNI, string DESCRIPCION, string ESTADO)
        {
            return oUtilitarios.EjecutaDatatable("SP_UPDATE_ANUNCIOS_PERSONAL", tipo, DNI, DESCRIPCION, ESTADO);
        }
        public DataTable SP_LISTAR_ANUNCIOS_PERSONAL(int tipo)
        {
            return oUtilitarios.EjecutaDatatable("SP_LISTAR_ANUNCIOS_PERSONAL", tipo);
        }
        public DataTable SP_LISTAR_ANIVERSARIO(int tipo)
        {
            return oUtilitarios.EjecutaDatatable("SP_LISTAR_ANIVERSARIO", tipo);
        }
        public DataTable SP_Listar_Directorio_Corporativo(string nombre)
        {
            return oUtilitarios.EjecutaDatatable("SP_Listar_Directorio_Corporativo", nombre);
        }
    }
}
