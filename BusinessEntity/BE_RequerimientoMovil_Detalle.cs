using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
  public  class BE_RequerimientoMovil_Detalle
    {
        private int m_cantidad;
        public int cantidad
        {
            get { return m_cantidad; }
            set { m_cantidad = value; }
        }

        private int m_id_detalle;
        public int id_detalle
        {
            get { return m_id_detalle; }
            set { m_id_detalle = value; }
        }
        private int m_IdTrabajador;
        public int IdTrabajador
        {
            get { return m_IdTrabajador; }
            set { m_IdTrabajador = value; }
        }
        private string m_Dni_Trabajador;
        public string Dni_Trabajador
        {
            get { return m_Dni_Trabajador; }
            set { m_Dni_Trabajador = value; }
        }
        private string m_NombreSolicitante;
        public string NombreSolicitante
        {
            get { return m_NombreSolicitante; }
            set { m_NombreSolicitante = value; }
        }
        private string m_FechaRequerida;
        public string FechaRequerida
        {
            get { return m_FechaRequerida; }
            set { m_FechaRequerida = value; }
        }
        private int m_MesesRequerido;
        public int MesesRequerido
        {
            get { return m_MesesRequerido; }
            set { m_MesesRequerido = value; }
        }
        private string m_LugarEntrega;
        public string LugarEntrega
        {
            get { return m_LugarEntrega; }
            set { m_LugarEntrega = value; }
        }
        private int m_IdTipoEquipo;
        public int IdTipoEquipo
        {
            get { return m_IdTipoEquipo; }
            set { m_IdTipoEquipo = value; }
        }
        private string m_TipoEquipo;
        public string TipoEquipo
        {
            get { return m_TipoEquipo; }
            set { m_TipoEquipo = value; }
        }
        private string m_FechaCreacion;
        public string FechaCreacion
        {
            get { return m_FechaCreacion; }
            set { m_FechaCreacion = value; }
        }
        private string m_USER_CREACION;
        public string USER_CREACION
        {
            get { return m_USER_CREACION; }
            set { m_USER_CREACION = value; }
        }
        private int m_IdRequerimiento;
        public int IdRequerimiento
        {
            get { return m_IdRequerimiento; }
            set { m_IdRequerimiento = value; }
        }
        private string m_CODIGO_GUID;
        public string CODIGO_GUID
        {
            get { return m_CODIGO_GUID; }
            set { m_CODIGO_GUID = value; }
        }
        private string m_URL_CARGO;
        public string URL_CARGO
        {
            get { return m_URL_CARGO; }
            set { m_URL_CARGO = value; }
        }
        private int m_FLG_APROBADO;
        public int FLG_APROBADO
        {
            get { return m_FLG_APROBADO; }
            set { m_FLG_APROBADO = value; }
        }
        private string m_FECHA_APROBADO;
        public string FECHA_APROBADO
        {
            get { return m_FECHA_APROBADO; }
            set { m_FECHA_APROBADO = value; }
        }
        private string m_USER_APRUEBA;
        public string USER_APRUEBA
        {
            get { return m_USER_APRUEBA; }
            set { m_USER_APRUEBA = value; }
        }
        private int m_FLG_ENVIO;
        public int FLG_ENVIO
        {
            get { return m_FLG_ENVIO; }
            set { m_FLG_ENVIO = value; }
        }
        private string m_FECHA_ENVIO;
        public string FECHA_ENVIO
        {
            get { return m_FECHA_ENVIO; }
            set { m_FECHA_ENVIO = value; }
        }
        private int m_FLG_ATENDIDO;
        public int FLG_ATENDIDO
        {
            get { return m_FLG_ATENDIDO; }
            set { m_FLG_ATENDIDO = value; }
        }
        private string m_FECHA_ATENIDO;
        public string FECHA_ATENIDO
        {
            get { return m_FECHA_ATENIDO; }
            set { m_FECHA_ATENIDO = value; }
        }
        private int m_FLG_ENTREGADO;
        public int FLG_ENTREGADO
        {
            get { return m_FLG_ENTREGADO; }
            set { m_FLG_ENTREGADO = value; }
        }
        private string m_FECHA_ENTREGA;
        public string FECHA_ENTREGA
        {
            get { return m_FECHA_ENTREGA; }
            set { m_FECHA_ENTREGA = value; }
        }
        private string m_USER_ENTREGA;
        public string USER_ENTREGA
        {
            get { return m_USER_ENTREGA; }
            set { m_USER_ENTREGA = value; }
        }
        private int m_IdLinea;
        public int IdLinea
        {
            get { return m_IdLinea; }
            set { m_IdLinea = value; }
        }
        private string m_Numero;
        public string Numero
        {
            get { return m_Numero; }
            set { m_Numero = value; }
        }
        private string m_IMEI;
        public string IMEI
        {
            get { return m_IMEI; }
            set { m_IMEI = value; }
        }
        private string m_Modelo;
        public string Modelo
        {
            get { return m_Modelo; }
            set { m_Modelo = value; }
        }
        private string m_Marca;
        public string Marca
        {
            get { return m_Marca; }
            set { m_Marca = value; }
        }
        private string m_SIM_CARD;
        public string SIM_CARD
        {
            get { return m_SIM_CARD; }
            set { m_SIM_CARD = value; }
        }
        private string m_Operador;
        public string Operador
        {
            get { return m_Operador; }
            set { m_Operador = value; }
        }
        private int m_IdEquipo;
        public int IdEquipo
        {
            get { return m_IdEquipo; }
            set { m_IdEquipo = value; }
        }
        private int m_IdOperadorMovil;
        public int IdOperadorMovil
        {
            get { return m_IdOperadorMovil; }
            set { m_IdOperadorMovil = value; }
        }
        private int m_FLG_DEVOLUCION;
        public int FLG_DEVOLUCION
        {
            get { return m_FLG_DEVOLUCION; }
            set { m_FLG_DEVOLUCION = value; }
        }
        private string m_FECHA_DEVOLUCION;
        public string FECHA_DEVOLUCION
        {
            get { return m_FECHA_DEVOLUCION; }
            set { m_FECHA_DEVOLUCION = value; }
        }
        private string m_CARGO_DEVOLUCION;
        public string CARGO_DEVOLUCION
        {
            get { return m_CARGO_DEVOLUCION; }
            set { m_CARGO_DEVOLUCION = value; }
        }
    }
}
