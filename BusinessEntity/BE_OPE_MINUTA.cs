using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_OPE_MINUTA
    {
        private int m_id_minuta;
        private string id_dni, fch_fecha_registro, dsc_tipo_fecha, dsc_asunto,
                        dsc_lugar, dsc_proyecto, dsc_nombre_cliente, dsc_contrato, dsc_obra,
                        num_numero_registro, fch_HoraInicial, fch_HoraFinal, dsc_reunion, usuario;
        public int M_id_minuta
        {
            get { return m_id_minuta; }
            set { m_id_minuta = value; }
        }
        public string Dsc_reunion
        {
            get { return dsc_reunion; }
            set { dsc_reunion = value; }
        }

        public string Fch_fecha_registro
        {
            get { return fch_fecha_registro; }
            set { fch_fecha_registro = value; }
        }


        public string Id_dni
        {
            get { return id_dni; }
            set { id_dni = value; }
        }

        public string Dsc_tipo_fecha
        {
            get { return dsc_tipo_fecha; }
            set { dsc_tipo_fecha = value; }
        }
        public string Dsc_asunto
        {
            get { return dsc_asunto; }
            set { dsc_asunto = value; }
        }
        public string Dsc_lugar
        {
            get { return dsc_lugar; }
            set { dsc_lugar = value; }
        }
        public string Dsc_proyecto
        {
            get { return dsc_proyecto; }
            set { dsc_proyecto = value; }
        }
        public string Dsc_nombre_cliente
        {
            get { return dsc_nombre_cliente; }
            set { dsc_nombre_cliente = value; }
        }

        public string Dsc_contrato
        {
            get { return dsc_contrato; }
            set { dsc_contrato = value; }
        }
        public string Dsc_obra
        {
            get { return dsc_obra; }
            set { dsc_obra = value; }
        }

        public string Num_numero_registro
        {
            get { return num_numero_registro; }
            set { num_numero_registro = value; }
        }

        public string Fch_HoraInicial
        {
            get { return fch_HoraInicial; }
            set { fch_HoraInicial = value; }
        }
        public string Fch_HoraFinal
        {
            get { return fch_HoraFinal; }
            set { fch_HoraFinal = value; }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

    }
}
