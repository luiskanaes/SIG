using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntity
{
    public class BE_DETALLEREQUERIMIENTO
    {
        private string m_CENTRO_COSTO_ORIGEN;
        public string CENTRO_COSTO_ORIGEN
        {
            get { return m_CENTRO_COSTO_ORIGEN; }
            set { m_CENTRO_COSTO_ORIGEN = value; }
        }
        private string m_CENTRO_COSTO;
        public string CENTRO_COSTO
        {
            get { return m_CENTRO_COSTO; }
            set { m_CENTRO_COSTO = value; }
        }
        private int m_NUMERO_REQUISICION;
        public int NUMERO_REQUISICION
        {
            get { return m_NUMERO_REQUISICION; }
            set { m_NUMERO_REQUISICION = value; }
        }
        private int m_SECUENCIA;
        public int SECUENCIA
        {
            get { return m_SECUENCIA; }
            set { m_SECUENCIA = value; }
        }
        private string m_CODIGO_ORIGEN_SOLICITANTE;
        public string CODIGO_ORIGEN_SOLICITANTE
        {
            get { return m_CODIGO_ORIGEN_SOLICITANTE; }
            set { m_CODIGO_ORIGEN_SOLICITANTE = value; }
        }
        private string m_TIPO_TRABAJADOR_SOLICITANTE;
        public string TIPO_TRABAJADOR_SOLICITANTE
        {
            get { return m_TIPO_TRABAJADOR_SOLICITANTE; }
            set { m_TIPO_TRABAJADOR_SOLICITANTE = value; }
        }
        private string m_CODIGO_TRABAJADOR_SOLICITANTE;
        public string CODIGO_TRABAJADOR_SOLICITANTE
        {
            get { return m_CODIGO_TRABAJADOR_SOLICITANTE; }
            set { m_CODIGO_TRABAJADOR_SOLICITANTE = value; }
        }
        private string m_CATEGORIA_OBRERO;
        public string CATEGORIA_OBRERO
        {
            get { return m_CATEGORIA_OBRERO; }
            set { m_CATEGORIA_OBRERO = value; }
        }
        private string m_ESPECIALIDAD_TRABAJADOR;
        public string ESPECIALIDAD_TRABAJADOR
        {
            get { return m_ESPECIALIDAD_TRABAJADOR; }
            set { m_ESPECIALIDAD_TRABAJADOR = value; }
        }
        private DateTime m_INICIO_OBRA;
        public DateTime INICIO_OBRA
        {
            get { return m_INICIO_OBRA; }
            set { m_INICIO_OBRA = value; }
        }
        private string m_CODIGO_POSTULANTE;
        public string CODIGO_POSTULANTE
        {
            get { return m_CODIGO_POSTULANTE; }
            set { m_CODIGO_POSTULANTE = value; }
        }
        private string m_APELLIDO_PATERNO;
        public string APELLIDO_PATERNO
        {
            get { return m_APELLIDO_PATERNO; }
            set { m_APELLIDO_PATERNO = value; }
        }
        private string m_APELLIDO_MATERNO;
        public string APELLIDO_MATERNO
        {
            get { return m_APELLIDO_MATERNO; }
            set { m_APELLIDO_MATERNO = value; }
        }
        private string m_NOMBRES;
        public string NOMBRES
        {
            get { return m_NOMBRES; }
            set { m_NOMBRES = value; }
        }
        private int? m_NUMERO_PERSONAS;
        public int? NUMERO_PERSONAS
        {
            get { return m_NUMERO_PERSONAS; }
            set { m_NUMERO_PERSONAS = value; }
        }
        private string m_TELEFONO;
        public string TELEFONO
        {
            get { return m_TELEFONO; }
            set { m_TELEFONO = value; }
        }
        private string m_ORIGEN;
        public string ORIGEN
        {
            get { return m_ORIGEN; }
            set { m_ORIGEN = value; }
        }
        private string m_AUTORIZADO;
        public string AUTORIZADO
        {
            get { return m_AUTORIZADO; }
            set { m_AUTORIZADO = value; }
        }
        private string m_OBSERVACIONES;
        public string OBSERVACIONES
        {
            get { return m_OBSERVACIONES; }
            set { m_OBSERVACIONES = value; }
        }
        private DateTime m_FECHA_CREACION;
        public DateTime FECHA_CREACION
        {
            get { return m_FECHA_CREACION; }
            set { m_FECHA_CREACION = value; }
        }
        private string m_USUARIO_CREACION;
        public string USUARIO_CREACION
        {
            get { return m_USUARIO_CREACION; }
            set { m_USUARIO_CREACION = value; }
        }
        private string m_MAQUINA_CREACION;
        public string MAQUINA_CREACION
        {
            get { return m_MAQUINA_CREACION; }
            set { m_MAQUINA_CREACION = value; }
        }
        private DateTime m_FECHA_ACTUALIZACION;
        public DateTime FECHA_ACTUALIZACION
        {
            get { return m_FECHA_ACTUALIZACION; }
            set { m_FECHA_ACTUALIZACION = value; }
        }
        private string m_USUARIO_ACTUALIZACION;
        public string USUARIO_ACTUALIZACION
        {
            get { return m_USUARIO_ACTUALIZACION; }
            set { m_USUARIO_ACTUALIZACION = value; }
        }
        private string m_MAQUINA_ACTUALIZACION;
        public string MAQUINA_ACTUALIZACION
        {
            get { return m_MAQUINA_ACTUALIZACION; }
            set { m_MAQUINA_ACTUALIZACION = value; }
        }
        private string m_CODIGO_PROFESION;
        public string CODIGO_PROFESION
        {
            get { return m_CODIGO_PROFESION; }
            set { m_CODIGO_PROFESION = value; }
        }
        private string m_CODIGO_OCUPACION;
        public string CODIGO_OCUPACION
        {
            get { return m_CODIGO_OCUPACION; }
            set { m_CODIGO_OCUPACION = value; }
        }
        private string m_USUARIO_RESPONSABLE;
        public string USUARIO_RESPONSABLE
        {
            get { return m_USUARIO_RESPONSABLE; }
            set { m_USUARIO_RESPONSABLE = value; }
        }
        private string m_ESTADO_ATENCION;
        public string ESTADO_ATENCION
        {
            get { return m_ESTADO_ATENCION; }
            set { m_ESTADO_ATENCION = value; }
        }
        private string m_ESPECIALIDAD;
        public string ESPECIALIDAD
        {
            get { return m_ESPECIALIDAD; }
            set { m_ESPECIALIDAD = value; }
        }
        private string m_PLANILLA;
        public string PLANILLA
        {
            get { return m_PLANILLA; }
            set { m_PLANILLA = value; }
        }
        private DateTime m_FECHA_ATENCION;
        public DateTime FECHA_ATENCION
        {
            get { return m_FECHA_ATENCION; }
            set { m_FECHA_ATENCION = value; }
        }
        private string m_USUARIO_ATENCION;
        public string USUARIO_ATENCION
        {
            get { return m_USUARIO_ATENCION; }
            set { m_USUARIO_ATENCION = value; }
        }
        private string m_MAQUINA_ATENCION;
        public string MAQUINA_ATENCION
        {
            get { return m_MAQUINA_ATENCION; }
            set { m_MAQUINA_ATENCION = value; }
        }
        private string m_OBSERVACIONES_ESTADO;
        public string OBSERVACIONES_ESTADO
        {
            get { return m_OBSERVACIONES_ESTADO; }
            set { m_OBSERVACIONES_ESTADO = value; }
        }
        private string m_CODIGO_PROCEDENCIA;
        public string CODIGO_PROCEDENCIA
        {
            get { return m_CODIGO_PROCEDENCIA; }
            set { m_CODIGO_PROCEDENCIA = value; }
        }
    }

}
