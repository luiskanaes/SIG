using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public  class BE_OPE_TEMAS
    {
        private int id_temas, id_areas, id_detalle_minuta, id_minuta, id_parametro;
        private string dsc_nombre_tema, fch_fecha_original, fch_fecha_requerimiento, dsc_responsable, fch_compromiso;

        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public int Id_temas
        {
            get { return id_temas; }
            set { id_temas = value; }
        }
        public int Id_parametro
        {
            get { return id_parametro; }
            set { id_parametro = value; }
        }
        public int Id_areas
        {
            get { return id_areas; }
            set { id_areas = value; }
        }

        public int Id_detalle_minuta
        {
            get { return id_detalle_minuta; }
            set { id_detalle_minuta = value; }
        }
        public int Id_minuta
        {
            get { return id_minuta; }
            set { id_minuta = value; }
        }

        public string Dsc_nombre_tema
        {
            get { return dsc_nombre_tema; }
            set { dsc_nombre_tema = value; }
        }

        public string Fch_fecha_original
        {
            get { return fch_fecha_original; }
            set { fch_fecha_original = value; }
        }

        public string Fch_fecha_requerimiento
        {
            get { return fch_fecha_requerimiento; }
            set { fch_fecha_requerimiento = value; }
        }

        public string Fch_compromiso
        {
            get { return fch_compromiso; }
            set { fch_compromiso = value; }
        }
        public string Dsc_responsable
        {
            get { return dsc_responsable; }
            set { dsc_responsable = value; }
        }
    }
}
