using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BE_TBL_RequerimientoSubDetalle
    {
        private string m_Requ_Numero;
        public string Requ_Numero
        {
            get { return m_Requ_Numero; }
            set { m_Requ_Numero = value; }
        }
        private string m_Reqd_CodLinea;
        public string Reqd_CodLinea
        {
            get { return m_Reqd_CodLinea; }
            set { m_Reqd_CodLinea = value; }
        }
        private string m_Reqs_Correlativo;
        public string Reqs_Correlativo
        {
            get { return m_Reqs_Correlativo; }
            set { m_Reqs_Correlativo = value; }
        }
        private string m_Equi_Codigo;
        public string Equi_Codigo
        {
            get { return m_Equi_Codigo; }
            set { m_Equi_Codigo = value; }
        }
        private int m_Reqs_CtdSolicitada;
        public int Reqs_CtdSolicitada
        {
            get { return m_Reqs_CtdSolicitada; }
            set { m_Reqs_CtdSolicitada = value; }
        }
        private int m_Reqs_CtdReservada;
        public int Reqs_CtdReservada
        {
            get { return m_Reqs_CtdReservada; }
            set { m_Reqs_CtdReservada = value; }
        }
        private string m_Reqs_ComentarioCEQ;
        public string Reqs_ComentarioCEQ
        {
            get { return m_Reqs_ComentarioCEQ; }
            set { m_Reqs_ComentarioCEQ = value; }
        }
        private DateTime m_Reqs_FechaDisponibilidad;
        public DateTime Reqs_FechaDisponibilidad
        {
            get { return m_Reqs_FechaDisponibilidad; }
            set { m_Reqs_FechaDisponibilidad = value; }
        }
        private int m_Reqs_CtdComprar;
        public int Reqs_CtdComprar
        {
            get { return m_Reqs_CtdComprar; }
            set { m_Reqs_CtdComprar = value; }
        }
        private int m_Reqs_CtdInicial;
        public int Reqs_CtdInicial
        {
            get { return m_Reqs_CtdInicial; }
            set { m_Reqs_CtdInicial = value; }
        }
        private string m_Cval_Codigo;
        public string Cval_Codigo
        {
            get { return m_Cval_Codigo; }
            set { m_Cval_Codigo = value; }
        }
        private int m_Reqs_ItemSecuencia;
        public int Reqs_ItemSecuencia
        {
            get { return m_Reqs_ItemSecuencia; }
            set { m_Reqs_ItemSecuencia = value; }
        }
        private string m_Reqs_Estado;
        public string Reqs_Estado
        {
            get { return m_Reqs_Estado; }
            set { m_Reqs_Estado = value; }
        }
        private Decimal m_Reqs_TarifaDiaria;
        public Decimal Reqs_TarifaDiaria
        {
            get { return m_Reqs_TarifaDiaria; }
            set { m_Reqs_TarifaDiaria = value; }
        }
        private Decimal m_Reqs_ValorReposicion;
        public Decimal Reqs_ValorReposicion
        {
            get { return m_Reqs_ValorReposicion; }
            set { m_Reqs_ValorReposicion = value; }
        }
        private string m_Reqs_Modelo;
        public string Reqs_Modelo
        {
            get { return m_Reqs_Modelo; }
            set { m_Reqs_Modelo = value; }
        }
        private string m_Reqs_DescripcionEquipo;
        public string Reqs_DescripcionEquipo
        {
            get { return m_Reqs_DescripcionEquipo; }
            set { m_Reqs_DescripcionEquipo = value; }
        }
        private string m_Reqs_ComentarioValidacion;
        public string Reqs_ComentarioValidacion
        {
            get { return m_Reqs_ComentarioValidacion; }
            set { m_Reqs_ComentarioValidacion = value; }
        }
        private string m_Reqs_CotiArchivo;
        public string Reqs_CotiArchivo
        {
            get { return m_Reqs_CotiArchivo; }
            set { m_Reqs_CotiArchivo = value; }
        }
        private string m_Reqs_CotiExtension;
        public string Reqs_CotiExtension
        {
            get { return m_Reqs_CotiExtension; }
            set { m_Reqs_CotiExtension = value; }
        }
        private string m_Reqs_CotiTDoc;
        public string Reqs_CotiTDoc
        {
            get { return m_Reqs_CotiTDoc; }
            set { m_Reqs_CotiTDoc = value; }
        }
        private string m_Reqs_ComentarioObra;
        public string Reqs_ComentarioObra
        {
            get { return m_Reqs_ComentarioObra; }
            set { m_Reqs_ComentarioObra = value; }
        }
        private string m_Reqs_ComentarioRechazados;
        public string Reqs_ComentarioRechazados
        {
            get { return m_Reqs_ComentarioRechazados; }
            set { m_Reqs_ComentarioRechazados = value; }
        }
        private string m_Reqs_FormaPago;
        public string Reqs_FormaPago
        {
            get { return m_Reqs_FormaPago; }
            set { m_Reqs_FormaPago = value; }
        }
        private Decimal m_Reqs_Monto;
        public Decimal Reqs_Monto
        {
            get { return m_Reqs_Monto; }
            set { m_Reqs_Monto = value; }
        }
        private DateTime m_Reqs_FechaEntrega;
        public DateTime Reqs_FechaEntrega
        {
            get { return m_Reqs_FechaEntrega; }
            set { m_Reqs_FechaEntrega = value; }
        }
        private string m_Reqs_Certificado;
        public string Reqs_Certificado
        {
            get { return m_Reqs_Certificado; }
            set { m_Reqs_Certificado = value; }
        }
        private string m_Prov_RUC;
        public string Prov_RUC
        {
            get { return m_Prov_RUC; }
            set { m_Prov_RUC = value; }
        }
        private string m_Reqs_TipoStock;
        public string Reqs_TipoStock
        {
            get { return m_Reqs_TipoStock; }
            set { m_Reqs_TipoStock = value; }
        }
        private int m_Reqs_Compra;
        public int Reqs_Compra
        {
            get { return m_Reqs_Compra; }
            set { m_Reqs_Compra = value; }
        }
        private int m_Reqs_AsignarEquipo;
        public int Reqs_AsignarEquipo
        {
            get { return m_Reqs_AsignarEquipo; }
            set { m_Reqs_AsignarEquipo = value; }
        }
        private string m_Reqs_EstadoAprobar;
        public string Reqs_EstadoAprobar
        {
            get { return m_Reqs_EstadoAprobar; }
            set { m_Reqs_EstadoAprobar = value; }
        }
        private string m_Reqs_Reactivar;
        public string Reqs_Reactivar
        {
            get { return m_Reqs_Reactivar; }
            set { m_Reqs_Reactivar = value; }
        }
        private string m_Reqs_EstadoAprobarObra;
        public string Reqs_EstadoAprobarObra
        {
            get { return m_Reqs_EstadoAprobarObra; }
            set { m_Reqs_EstadoAprobarObra = value; }
        }
        private string m_Reqs_RqmBase;
        public string Reqs_RqmBase
        {
            get { return m_Reqs_RqmBase; }
            set { m_Reqs_RqmBase = value; }
        }
        private int m_Reqs_CtdDespachado;
        public int Reqs_CtdDespachado
        {
            get { return m_Reqs_CtdDespachado; }
            set { m_Reqs_CtdDespachado = value; }
        }
        private string m_Reqs_UsuarioCreacion;
        public string Reqs_UsuarioCreacion
        {
            get { return m_Reqs_UsuarioCreacion; }
            set { m_Reqs_UsuarioCreacion = value; }
        }
        private string m_Reqs_UsuarioEdicion;
        public string Reqs_UsuarioEdicion
        {
            get { return m_Reqs_UsuarioEdicion; }
            set { m_Reqs_UsuarioEdicion = value; }
        }
        private DateTime m_Reqs_FechaCreacion;
        public DateTime Reqs_FechaCreacion
        {
            get { return m_Reqs_FechaCreacion; }
            set { m_Reqs_FechaCreacion = value; }
        }
        private DateTime m_Reqs_FechaEdicion;
        public DateTime Reqs_FechaEdicion
        {
            get { return m_Reqs_FechaEdicion; }
            set { m_Reqs_FechaEdicion = value; }
        }
        private int m_Reqs_CtdRsrvSaldoBorrador;
        public int Reqs_CtdRsrvSaldoBorrador
        {
            get { return m_Reqs_CtdRsrvSaldoBorrador; }
            set { m_Reqs_CtdRsrvSaldoBorrador = value; }
        }
        private string m_Movi_NroMov;
        public string Movi_NroMov
        {
            get { return m_Movi_NroMov; }
            set { m_Movi_NroMov = value; }
        }
        private string m_Movd_CodLinea;
        public string Movd_CodLinea
        {
            get { return m_Movd_CodLinea; }
            set { m_Movd_CodLinea = value; }
        }
        private string m_Tmov_Codigo;
        public string Tmov_Codigo
        {
            get { return m_Tmov_Codigo; }
            set { m_Tmov_Codigo = value; }
        }
        private string m_Tbie_Codigo;
        public string Tbie_Codigo
        {
            get { return m_Tbie_Codigo; }
            set { m_Tbie_Codigo = value; }
        }
        private string m_Reqs_ComentMayor;
        public string Reqs_ComentMayor
        {
            get { return m_Reqs_ComentMayor; }
            set { m_Reqs_ComentMayor = value; }
        }
        private int m_Reqs_TipAsiganacion;
        public int Reqs_TipAsiganacion
        {
            get { return m_Reqs_TipAsiganacion; }
            set { m_Reqs_TipAsiganacion = value; }
        }
        private DateTime m_Reqs_FechEdicUltima;
        public DateTime Reqs_FechEdicUltima
        {
            get { return m_Reqs_FechEdicUltima; }
            set { m_Reqs_FechEdicUltima = value; }
        }
        private DateTime m_Reqs_IteArchivo;
        public DateTime Reqs_IteArchivo
        {
            get { return m_Reqs_IteArchivo; }
            set { m_Reqs_IteArchivo = value; }
        }
        private string m_Reqs_IteExtension;
        public string Reqs_IteExtension
        {
            get { return m_Reqs_IteExtension; }
            set { m_Reqs_IteExtension = value; }
        }
        private string m_Reqs_Certificado2;
        public string Reqs_Certificado2
        {
            get { return m_Reqs_Certificado2; }
            set { m_Reqs_Certificado2 = value; }
        }
        private string m_Reqs_Certificado3;
        public string Reqs_Certificado3
        {
            get { return m_Reqs_Certificado3; }
            set { m_Reqs_Certificado3 = value; }
        }
        private string m_descripcion_alquiler;
        public string descripcion_alquiler
        {
            get { return m_descripcion_alquiler; }
            set { m_descripcion_alquiler = value; }
        }
        private int m_D_DOCUMENTO_TIPO;
        public int D_DOCUMENTO_TIPO
        {
            get { return m_D_DOCUMENTO_TIPO; }
            set { m_D_DOCUMENTO_TIPO = value; }
        }
        private int m_D_ESTADO_PROCESO;
        public int D_ESTADO_PROCESO
        {
            get { return m_D_ESTADO_PROCESO; }
            set { m_D_ESTADO_PROCESO = value; }
        }
        private string m_D_DOCUMENTO_RUTA;
        public string D_DOCUMENTO_RUTA
        {
            get { return m_D_DOCUMENTO_RUTA; }
            set { m_D_DOCUMENTO_RUTA = value; }
        }
        private string m_D_DOCUMENTO_FILE;
        public string D_DOCUMENTO_FILE
        {
            get { return m_D_DOCUMENTO_FILE; }
            set { m_D_DOCUMENTO_FILE = value; }
        }
        private int m_D_DOC_MOVILIZACION;
        public int D_DOC_MOVILIZACION
        {
            get { return m_D_DOC_MOVILIZACION; }
            set { m_D_DOC_MOVILIZACION = value; }
        }
        private string  m_D_DOCUMENTO_FECHA;
        public string D_DOCUMENTO_FECHA
        {
            get { return m_D_DOCUMENTO_FECHA; }
            set { m_D_DOCUMENTO_FECHA = value; }
        }
        private string m_D_SOLPED;
        public string D_SOLPED
        {
            get { return m_D_SOLPED; }
            set { m_D_SOLPED = value; }
        }
        private DateTime m_D_SOLPED_FECHA;
        public DateTime D_SOLPED_FECHA
        {
            get { return m_D_SOLPED_FECHA; }
            set { m_D_SOLPED_FECHA = value; }
        }
        private string  m_D_FECHA_ENVIO_LOG;
        public string D_FECHA_ENVIO_LOG
        {
            get { return m_D_FECHA_ENVIO_LOG; }
            set { m_D_FECHA_ENVIO_LOG = value; }
        }
        private string m_D_PDC;
        public string D_PDC
        {
            get { return m_D_PDC; }
            set { m_D_PDC = value; }
        }
        private string  m_D_FECHA_ENVIO_OBRA;
        public string  D_FECHA_ENVIO_OBRA
        {
            get { return m_D_FECHA_ENVIO_OBRA; }
            set { m_D_FECHA_ENVIO_OBRA = value; }
        }
        private string m_D_PDC_FECHA;
        public string D_PDC_FECHA
        {
            get { return m_D_PDC_FECHA; }
            set { m_D_PDC_FECHA = value; }
        }
        private int m_D_ATENCION_TIPO;
        public int D_ATENCION_TIPO
        {
            get { return m_D_ATENCION_TIPO; }
            set { m_D_ATENCION_TIPO = value; }
        }
        private string m_D_ATENCION_COMENTARIOS;
        public string D_ATENCION_COMENTARIOS
        {
            get { return m_D_ATENCION_COMENTARIOS; }
            set { m_D_ATENCION_COMENTARIOS = value; }
        }

        private string m_D_Prov_RUC;
        public string D_Prov_RUC
        {
            get { return m_D_Prov_RUC; }
            set { m_D_Prov_RUC = value; }
        }
        private string m_D_CODIGO_CARE;
        public string D_CODIGO_CARE
        {
            get { return m_D_CODIGO_CARE; }
            set { m_D_CODIGO_CARE = value; }
        }

        private string m_D_COMENTARIOS;
        public string D_COMENTARIOS
        {
            get { return m_D_COMENTARIOS; }
            set { m_D_COMENTARIOS = value; }
        }
        private string m_D_OBSERVACION_RUTA;
        public string D_OBSERVACION_RUTA
        {
            get { return m_D_OBSERVACION_RUTA; }
            set { m_D_OBSERVACION_RUTA = value; }
        }
        private string m_D_OBSERVACION_FILE;
        public string D_OBSERVACION_FILE
        {
            get { return m_D_OBSERVACION_FILE; }
            set { m_D_OBSERVACION_FILE = value; }
        }
        private string m_D_GUIA_RUTA;
        public string D_GUIA_RUTA
        {
            get { return m_D_GUIA_RUTA; }
            set { m_D_GUIA_RUTA = value; }
        }
        private string m_D_GUIA_FILE;
        public string D_GUIA_FILE
        {
            get { return m_D_GUIA_FILE; }
            set { m_D_GUIA_FILE = value; }
        }
        private int m_D_PDC_MONEDA;
        public int D_PDC_MONEDA
        {
            get { return m_D_PDC_MONEDA; }
            set { m_D_PDC_MONEDA = value; }
        }

        private decimal  m_D_PDC_MONTO;
        public decimal D_PDC_MONTO
        {
            get { return m_D_PDC_MONTO; }
            set { m_D_PDC_MONTO = value; }
        }
        private string m_D_FECHA_SALE_OBRA;
        public string D_FECHA_SALE_OBRA
        {
            get { return m_D_FECHA_SALE_OBRA; }
            set { m_D_FECHA_SALE_OBRA = value; }
        }
        private int m_TIPO_OPERACION;
        public int TIPO_OPERACION
        {
            get { return m_TIPO_OPERACION; }
            set { m_TIPO_OPERACION = value; }
        }
        private string m_D_DOCUMENTO_FILENAME;
        public string D_DOCUMENTO_FILENAME
        {
            get { return m_D_DOCUMENTO_FILENAME; }
            set { m_D_DOCUMENTO_FILENAME = value; }
        }

        private int m_TIPO;
        public int D_TIPO
        {
            get { return m_TIPO; }
            set { m_TIPO = value; }
        }

        private int m_AMPLIACION;
        public int D_AMPLIACION
        {
            get { return m_AMPLIACION; }
            set { m_AMPLIACION = value; }
        }
        private string  m_TIPO_FILE;
        public string  TIPO_FILE
        {
            get { return m_TIPO_FILE; }
            set { m_TIPO_FILE = value; }
        }

        private string m_COD_GUID;
        public string COD_GUID
        {
            get { return m_COD_GUID; }
            set { m_COD_GUID = value; }
        }
        private decimal m_D_HRAS_MIN;
        public decimal D_HRAS_MIN
        {
            get { return m_D_HRAS_MIN; }
            set { m_D_HRAS_MIN = value; }
        }
        private decimal m_D_COSTO_HORA;
        public decimal D_COSTO_HORA
        {
            get { return m_D_COSTO_HORA; }
            set { m_D_COSTO_HORA = value; }
        }

        private decimal m_D_PDC_MONTO_TOTAL;
        public decimal D_PDC_MONTO_TOTAL
        {
            get { return m_D_PDC_MONTO_TOTAL; }
            set { m_D_PDC_MONTO_TOTAL = value; }
        }

        private decimal m_D_PDC_MONTO_MOVIL;
        public decimal D_PDC_MONTO_MOVIL
        {
            get { return m_D_PDC_MONTO_MOVIL; }
            set { m_D_PDC_MONTO_MOVIL = value; }
        }
        private string  m_GUID;
        public string GUID
        {
            get { return m_GUID; }
            set { m_GUID = value; }
        }

        private string m_FLG_OPERARIO;
        public string D_FLG_OPERARIO
        {
            get { return m_FLG_OPERARIO; }
            set { m_FLG_OPERARIO = value; }
        }
        private string m_A_GUIA_INGRESO;
        public string A_GUIA_INGRESO
        {
            get { return m_A_GUIA_INGRESO; }
            set { m_A_GUIA_INGRESO = value; }
        }
        private string m_A_SERIE;
        public string A_SERIE
        {
            get { return m_A_SERIE; }
            set { m_A_SERIE = value; }
        }
        private string m_A_PLACA;
        public string A_PLACA
        {
            get { return m_A_PLACA; }
            set { m_A_PLACA = value; }
        }
        private string m_A_GUIA_SALIDA;
        public string A_GUIA_SALIDA
        {
            get { return m_A_GUIA_SALIDA; }
            set { m_A_GUIA_SALIDA = value; }
        }

        private string m_A_FASES_AMPLIACION;
        public string A_FASES_AMPLIACION
        {
            get { return m_A_FASES_AMPLIACION; }
            set { m_A_FASES_AMPLIACION = value; }
        }

        private string m_USUARIO_ATIENDE;
        public string USUARIO_ATIENDE
        {
            get { return m_USUARIO_ATIENDE; }
            set { m_USUARIO_ATIENDE = value; }
        }


        private string m_FECHA_DESPACHO;
        public string FECHA_DESPACHO
        {
            get { return m_FECHA_DESPACHO; }
            set { m_FECHA_DESPACHO = value; }
        }
    }
}
