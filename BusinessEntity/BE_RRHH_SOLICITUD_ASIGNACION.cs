using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public  class BE_RRHH_SOLICITUD_ASIGNACION
    {
        private int m_IDE_ASIGNACION;
        public int IDE_ASIGNACION
        {
            get { return m_IDE_ASIGNACION; }
            set { m_IDE_ASIGNACION = value; }
        }
        private string m_IDE_POSTULANTE;
        public string IDE_POSTULANTE
        {
            get { return m_IDE_POSTULANTE; }
            set { m_IDE_POSTULANTE = value; }
        }
        private string m_COD_CENTRO;
        public string COD_CENTRO
        {
            get { return m_COD_CENTRO; }
            set { m_COD_CENTRO = value; }
        }
        private int m_IDE_CARGO;
        public int IDE_CARGO
        {
            get { return m_IDE_CARGO; }
            set { m_IDE_CARGO = value; }
        }
        private string m_AREA;
        public string AREA
        {
            get { return m_AREA; }
            set { m_AREA = value; }
        }
        private string m_JEFE_DNI;
        public string JEFE_DNI
        {
            get { return m_JEFE_DNI; }
            set { m_JEFE_DNI = value; }
        }
        private string m_UBICACION;
        public string UBICACION
        {
            get { return m_UBICACION; }
            set { m_UBICACION = value; }
        }
        private int m_TIPO_PROCESO;
        public int TIPO_PROCESO
        {
            get { return m_TIPO_PROCESO; }
            set { m_TIPO_PROCESO = value; }
        }
        private int m_ORIGEN_POSICION;
        public int ORIGEN_POSICION
        {
            get { return m_ORIGEN_POSICION; }
            set { m_ORIGEN_POSICION = value; }
        }
        private int m_TIPO_RECLUT_OBRA;
        public int TIPO_RECLUT_OBRA
        {
            get { return m_TIPO_RECLUT_OBRA; }
            set { m_TIPO_RECLUT_OBRA = value; }
        }
        private int m_TIPO_RECLUT_LIMA;
        public int TIPO_RECLUT_LIMA
        {
            get { return m_TIPO_RECLUT_LIMA; }
            set { m_TIPO_RECLUT_LIMA = value; }
        }
        private string m_PISO;
        public string PISO
        {
            get { return m_PISO; }
            set { m_PISO = value; }
        }
        private int m_IDE_NIVEL_ACADEMICO;
        public int IDE_NIVEL_ACADEMICO
        {
            get { return m_IDE_NIVEL_ACADEMICO; }
            set { m_IDE_NIVEL_ACADEMICO = value; }
        }
        private int m_IDE_CARRERA;
        public int IDE_CARRERA
        {
            get { return m_IDE_CARRERA; }
            set { m_IDE_CARRERA = value; }
        }
        private string m_CARRERA_COMENTARIOS;
        public string CARRERA_COMENTARIOS
        {
            get { return m_CARRERA_COMENTARIOS; }
            set { m_CARRERA_COMENTARIOS = value; }
        }
        private string m_NRO_COLEGIATURA;
        public string NRO_COLEGIATURA
        {
            get { return m_NRO_COLEGIATURA; }
            set { m_NRO_COLEGIATURA = value; }
        }
        private int m_FLG_COLEGIATURA;
        public int FLG_COLEGIATURA
        {
            get { return m_FLG_COLEGIATURA; }
            set { m_FLG_COLEGIATURA = value; }
        }
        private int m_NIVEL_EXP_INGLES;
        public int NIVEL_EXP_INGLES
        {
            get { return m_NIVEL_EXP_INGLES; }
            set { m_NIVEL_EXP_INGLES = value; }
        }
        private int m_FLG_MAESTRIA;
        public int FLG_MAESTRIA
        {
            get { return m_FLG_MAESTRIA; }
            set { m_FLG_MAESTRIA = value; }
        }
        private int m_NIVEL_EXP_SOFTWARE;
        public int NIVEL_EXP_SOFTWARE
        {
            get { return m_NIVEL_EXP_SOFTWARE; }
            set { m_NIVEL_EXP_SOFTWARE = value; }
        }
        private int m_IDE_SEXO;
        public int IDE_SEXO
        {
            get { return m_IDE_SEXO; }
            set { m_IDE_SEXO = value; }
        }
        private int m_IDE_ESTADO_CIVIL;
        public int IDE_ESTADO_CIVIL
        {
            get { return m_IDE_ESTADO_CIVIL; }
            set { m_IDE_ESTADO_CIVIL = value; }
        }
        private string m_FUNCIONES_PUESTO;
        public string FUNCIONES_PUESTO
        {
            get { return m_FUNCIONES_PUESTO; }
            set { m_FUNCIONES_PUESTO = value; }
        }
        private Decimal m_SUELDO;
        public Decimal SUELDO
        {
            get { return m_SUELDO; }
            set { m_SUELDO = value; }
        }
        private Decimal m_COMISIONES;
        public Decimal COMISIONES
        {
            get { return m_COMISIONES; }
            set { m_COMISIONES = value; }
        }
        private int m_FLG_GRATIFICACIONES;
        public int FLG_GRATIFICACIONES
        {
            get { return m_FLG_GRATIFICACIONES; }
            set { m_FLG_GRATIFICACIONES = value; }
        }
        private int m_FLG_PREMIO_OBRA;
        public int FLG_PREMIO_OBRA
        {
            get { return m_FLG_PREMIO_OBRA; }
            set { m_FLG_PREMIO_OBRA = value; }
        }
        private string m_INICIO_CONTRATO;
        public string INICIO_CONTRATO
        {
            get { return m_INICIO_CONTRATO; }
            set { m_INICIO_CONTRATO = value; }
        }
        private string m_TERMINO_CONTRATO;
        public string TERMINO_CONTRATO
        {
            get { return m_TERMINO_CONTRATO; }
            set { m_TERMINO_CONTRATO = value; }
        }
        private int m_FLG_VALE_ALIMENTO;
        public int FLG_VALE_ALIMENTO
        {
            get { return m_FLG_VALE_ALIMENTO; }
            set { m_FLG_VALE_ALIMENTO = value; }
        }
        private int m_FLG_SEGURO_VIDA;
        public int FLG_SEGURO_VIDA
        {
            get { return m_FLG_SEGURO_VIDA; }
            set { m_FLG_SEGURO_VIDA = value; }
        }
        private int m_FLG_ASIG_MOVILIDAD;
        public int FLG_ASIG_MOVILIDAD
        {
            get { return m_FLG_ASIG_MOVILIDAD; }
            set { m_FLG_ASIG_MOVILIDAD = value; }
        }
        private string m_OTROS_BENEFICIOS;
        public string OTROS_BENEFICIOS
        {
            get { return m_OTROS_BENEFICIOS; }
            set { m_OTROS_BENEFICIOS = value; }
        }
        private string m_REGIMEN_TRABAJO;
        public string REGIMEN_TRABAJO
        {
            get { return m_REGIMEN_TRABAJO; }
            set { m_REGIMEN_TRABAJO = value; }
        }
        private string m_HORARIO_TRABAJO;
        public string HORARIO_TRABAJO
        {
            get { return m_HORARIO_TRABAJO; }
            set { m_HORARIO_TRABAJO = value; }
        }
        private int m_FLG_BONO_DESTAQUE;
        public int FLG_BONO_DESTAQUE
        {
            get { return m_FLG_BONO_DESTAQUE; }
            set { m_FLG_BONO_DESTAQUE = value; }
        }
        private int m_IDE_PASAJE;
        public int IDE_PASAJE
        {
            get { return m_IDE_PASAJE; }
            set { m_IDE_PASAJE = value; }
        }
        private string m_COMENTARIOS_GNRAL;
        public string COMENTARIOS_GNRAL
        {
            get { return m_COMENTARIOS_GNRAL; }
            set { m_COMENTARIOS_GNRAL = value; }
        }
        private string m_IDE_SOLICITANTE;
        public string IDE_SOLICITANTE
        {
            get { return m_IDE_SOLICITANTE; }
            set { m_IDE_SOLICITANTE = value; }
        }
        private string m_FECHA_FIRMA_SOL;
        public string FECHA_FIRMA_SOL
        {
            get { return m_FECHA_FIRMA_SOL; }
            set { m_FECHA_FIRMA_SOL = value; }
        }
        private string m_IDE_GERENTE;
        public string IDE_GERENTE
        {
            get { return m_IDE_GERENTE; }
            set { m_IDE_GERENTE = value; }
        }
        private string m_FECHA_FIRMA_GER;
        public string FECHA_FIRMA_GER
        {
            get { return m_FECHA_FIRMA_GER; }
            set { m_FECHA_FIRMA_GER = value; }
        }
        private string m_FECHA_REGISTRO;
        public string FECHA_REGISTRO
        {
            get { return m_FECHA_REGISTRO; }
            set { m_FECHA_REGISTRO = value; }
        }
        private string m_USER_REGISTRO;
        public string USER_REGISTRO
        {
            get { return m_USER_REGISTRO; }
            set { m_USER_REGISTRO = value; }
        }
        private string m_TICKET;
        public string TICKET
        {
            get { return m_TICKET; }
            set { m_TICKET = value; }
        }
        private string m_IDE_GERENCIA;
        public string IDE_GERENCIA
        {
            get { return m_IDE_GERENCIA; }
            set { m_IDE_GERENCIA = value; }
        }


        private string m_DNI_TMP;
        public string DNI_TMP
        {
            get { return m_DNI_TMP; }
            set { m_DNI_TMP = value; }
        }

        private string m_NOMBRE_TMP;
        public string NOMBRE_TMP
        {
            get { return m_NOMBRE_TMP; }
            set { m_NOMBRE_TMP = value; }
        }
        private string m_APE_PAT_TMP;
        public string APE_PAT_TMP
        {
            get { return m_APE_PAT_TMP; }
            set { m_APE_PAT_TMP = value; }
        }
        private string m_APE_MAT_TMP;
        public string APE_MAT_TMP
        {
            get { return m_APE_MAT_TMP; }
            set { m_APE_MAT_TMP = value; }
        }
        private string m_ID_EMPRESA;
        public string ID_EMPRESA
        {
            get { return m_ID_EMPRESA; }
            set { m_ID_EMPRESA = value; }
        }

        private string m_FILE_SOL;
        public string FILE_SOL
        {
            get { return m_FILE_SOL; }
            set { m_FILE_SOL = value; }
        }

        private string m_FILE_URL;
        public string FILE_URL
        {
            get { return m_FILE_URL; }
            set { m_FILE_URL = value; }
        }
        private string m_FILE_DIR;
        public string FILE_DIR
        {
            get { return m_FILE_DIR; }
            set { m_FILE_DIR = value; }
        }
    }
}
