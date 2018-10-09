using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BusinessEntity
{
    public class BE_REQUERIMIENTO 
    {
        private int m_ID_REQUERIMIENTO_PERSONAL;
        public int ID_REQUERIMIENTO_PERSONAL
        {
            get { return m_ID_REQUERIMIENTO_PERSONAL; }
            set { m_ID_REQUERIMIENTO_PERSONAL = value; }
        }
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
        private string m_TIPO_TRABAJADOR;
        public string TIPO_TRABAJADOR
        {
            get { return m_TIPO_TRABAJADOR; }
            set { m_TIPO_TRABAJADOR = value; }
        }
        private string m_FECHA;
        public string FECHA
        {
            get { return m_FECHA; }
            set { m_FECHA = value; }
        }
        private string m_JEFE_PERSONAL;
        public string JEFE_PERSONAL
        {
            get { return m_JEFE_PERSONAL; }
            set { m_JEFE_PERSONAL = value; }
        }
        private string m_JEFE_OBRA;
        public string JEFE_OBRA
        {
            get { return m_JEFE_OBRA; }
            set { m_JEFE_OBRA = value; }
        }
        private string m_RESIDENTE_OBRA;
        public string RESIDENTE_OBRA
        {
            get { return m_RESIDENTE_OBRA; }
            set { m_RESIDENTE_OBRA = value; }
        }
        private string m_OPERACIONES;
        public string OPERACIONES
        {
            get { return m_OPERACIONES; }
            set { m_OPERACIONES = value; }
        }
        private string m_ESTADO_REQUERIMIENTO;
        public string ESTADO_REQUERIMIENTO
        {
            get { return m_ESTADO_REQUERIMIENTO; }
            set { m_ESTADO_REQUERIMIENTO = value; }
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
        private string m_USUARIO_OPERACIONES;
        public string USUARIO_OPERACIONES
        {
            get { return m_USUARIO_OPERACIONES; }
            set { m_USUARIO_OPERACIONES = value; }
        }
        private string m_USUARIO_PERSONAL;
        public string USUARIO_PERSONAL
        {
            get { return m_USUARIO_PERSONAL; }
            set { m_USUARIO_PERSONAL = value; }
        }
        private string m_USUARIO_REQUERIMIENTO;
        public string USUARIO_REQUERIMIENTO
        {
            get { return m_USUARIO_REQUERIMIENTO; }
            set { m_USUARIO_REQUERIMIENTO = value; }
        }
        private string m_USUARIO_ADMINISTRADOR;
        public string USUARIO_ADMINISTRADOR
        {
            get { return m_USUARIO_ADMINISTRADOR; }
            set { m_USUARIO_ADMINISTRADOR = value; }
        }
        private string m_USUARIO_PERSONAL_OBRA;
        public string USUARIO_PERSONAL_OBRA
        {
            get { return m_USUARIO_PERSONAL_OBRA; }
            set { m_USUARIO_PERSONAL_OBRA = value; }
        }
        private DateTime m_FECHA_OPERACIONES;
        public DateTime FECHA_OPERACIONES
        {
            get { return m_FECHA_OPERACIONES; }
            set { m_FECHA_OPERACIONES = value; }
        }
        private DateTime m_FECHA_PERSONAL;
        public DateTime FECHA_PERSONAL
        {
            get { return m_FECHA_PERSONAL; }
            set { m_FECHA_PERSONAL = value; }
        }
        private DateTime m_FECHA_REQUERIMIENTO;
        public DateTime FECHA_REQUERIMIENTO
        {
            get { return m_FECHA_REQUERIMIENTO; }
            set { m_FECHA_REQUERIMIENTO = value; }
        }
        private DateTime m_FECHA_ADMINISTRADOR;
        public DateTime FECHA_ADMINISTRADOR
        {
            get { return m_FECHA_ADMINISTRADOR; }
            set { m_FECHA_ADMINISTRADOR = value; }
        }
        private DateTime m_FECHA_PERSONAL_OBRA;
        public DateTime FECHA_PERSONAL_OBRA
        {
            get { return m_FECHA_PERSONAL_OBRA; }
            set { m_FECHA_PERSONAL_OBRA = value; }
        }

        private string m_EMPRESA_ORIGEN;
        public string EMPRESA_ORIGEN
        {
            get { return m_EMPRESA_ORIGEN; }
            set { m_EMPRESA_ORIGEN = value; }
        }
        private int m_SECUENCIA;
        public int SECUENCIA
        {
            get { return m_SECUENCIA; }
            set { m_SECUENCIA = value; }
        }

        private string m_OBRA;
        public string OBRA
        {
            get { return m_OBRA; }
            set { m_OBRA = value; }
        }

        private string m_ESTADO_PROCESO;
        public string ESTADO_PROCESO
        {
            get { return m_ESTADO_PROCESO; }
            set { m_ESTADO_PROCESO = value; }
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


        private string m_ANALISTAS;
        public string ANALISTAS
        {
            get { return m_ANALISTAS; }
            set { m_ANALISTAS = value; }
        }

        private string m_ESTADOS;
        public string ESTADOS
        {
            get { return m_ESTADOS; }
            set { m_ESTADOS = value; }
        }

        private string m_CARGO;
        public string CARGO
        {
            get { return m_CARGO; }
            set { m_CARGO = value; }
        }



        }
     
    }


