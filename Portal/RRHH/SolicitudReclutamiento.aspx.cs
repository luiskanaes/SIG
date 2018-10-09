using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;
using System.Data.Odbc;
using BusinessEntity;
using BusinessLogic;
using UserCode;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class RRHH_SolicitudReclutamiento : System.Web.UI.Page
{
    string FolderMOI = ConfigurationManager.AppSettings["FolderMOI"];
    string UrlSSK = ConfigurationManager.AppSettings["UrlSSK"];
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardar);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Personal();
            Empresas();
            cargos();
            //gerencias();
            TipoProceso();
            OrigenPosicion();
            ReclutamientoObra();
            ReclutamientoLima();
            AcademicoNivel();
            ParametrosCarrera();
            Ingles();
            ParametrosCivil();
            Pasajes();
            equipo();
            Software();
            Otros();

            if (Session["IDE_ASIGNACION"]!=null)
            {
                hdcodigo.Value = Session["IDE_ASIGNACION"].ToString();
                if(hdcodigo.Value != string.Empty )
                {
                    Datos(hdcodigo.Value);
                }
               
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresas.DataSource = dtResultado;
            ddlEmpresas.DataTextField = dtResultado.Columns["DES_ABREV"].ToString();
            ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresas.DataBind();

            gerencias();
        }
    }
    protected void Datos(string IDE_ASIGNACION)
    {
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_SOLICITUD_ASIGNACION_ID(IDE_ASIGNACION);
        if (dtResultado.Rows.Count > 0)
        {
            hdcodigo.Value = dtResultado.Rows[0]["IDE_ASIGNACION"].ToString();
            string IDE_POSTULANTE = dtResultado.Rows[0]["IDE_POSTULANTE"].ToString();
            hdPersonal.Value = dtResultado.Rows[0]["IDE_POSTULANTE"].ToString();
            if (IDE_POSTULANTE != string.Empty)
            {
                ddlPersonal.SelectedValue = IDE_POSTULANTE;
                ddlPersonal.Enabled = false ;
            }
            else
            {
                ddlPersonal.Enabled = true  ;
            }

            txtDni.Text = dtResultado.Rows[0]["DNI_TMP"].ToString();
            txtNombre.Text = dtResultado.Rows[0]["NOMBRE_TMP"].ToString();
            txtPaterno.Text = dtResultado.Rows[0]["APE_PAT_TMP"].ToString();
            txtMaterno.Text = dtResultado.Rows[0]["APE_MAT_TMP"].ToString();

            string ID_EMPRESA = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            if (ID_EMPRESA != string.Empty)
            {
                ddlEmpresas .SelectedValue = ID_EMPRESA;
            }

            ddlGerencia.SelectedValue = dtResultado.Rows[0]["IDE_GERENCIA"].ToString();
            centros();
            ddlCentro.SelectedValue = dtResultado.Rows[0]["IDE_CENTRO"].ToString();
            ddlCargos.SelectedValue = dtResultado.Rows[0]["IDE_CARGO"].ToString();
            txtarea.Text = dtResultado.Rows[0]["AREA"].ToString();


            string JEFE_DNI = dtResultado.Rows[0]["JEFE_DNI"].ToString();
            if (JEFE_DNI != string.Empty)
            {
                ddlJefe.SelectedValue = JEFE_DNI;
            }

            string IDE_GERENTE = dtResultado.Rows[0]["IDE_GERENTE"].ToString();
            if (IDE_GERENTE != string.Empty)
            {
                ddlAprobador.SelectedValue = IDE_GERENTE;
            }

            RdoTipoProceso.SelectedValue = dtResultado.Rows[0]["TIPO_PROCESO"].ToString();
            rdoOrigen.SelectedValue = dtResultado.Rows[0]["ORIGEN_POSICION"].ToString();
            string TIPO_RECLUT_OBRA = dtResultado.Rows[0]["TIPO_RECLUT_OBRA"].ToString();
            if(TIPO_RECLUT_OBRA != string.Empty)
            {
                rdoRecObra.SelectedValue = dtResultado.Rows[0]["TIPO_RECLUT_OBRA"].ToString();
                ReclutamientoObra();

            }

            string TIPO_RECLUT_LIMA = dtResultado.Rows[0]["TIPO_RECLUT_LIMA"].ToString();
            if (TIPO_RECLUT_LIMA != string.Empty)
            {
             
                rdoRecLima.SelectedValue = dtResultado.Rows[0]["TIPO_RECLUT_LIMA"].ToString();
                ReclutamientoLima();
            }
          
          

            ddlNivelAcademico.SelectedValue = dtResultado.Rows[0]["IDE_NIVEL_ACADEMICO"].ToString();
            ddlcarrera.SelectedValue = dtResultado.Rows[0]["IDE_CARRERA"].ToString();

            txtColegiatura.Text = dtResultado.Rows[0]["NRO_COLEGIATURA"].ToString();

            
            Boolean FLG_COLEGIATURA  =Convert.ToBoolean( dtResultado.Rows[0]["FLG_COLEGIATURA"].ToString());
            if (FLG_COLEGIATURA == false )
            {
                RdoColegiatura.SelectedIndex = 0;
            }
            else
            {
                RdoColegiatura.SelectedIndex = 1;
            }
            

            rdoIngles.SelectedValue = dtResultado.Rows[0]["NIVEL_EXP_INGLES"].ToString();


            Boolean FLG_MAESTRIA = Convert.ToBoolean(dtResultado.Rows[0]["FLG_MAESTRIA"].ToString());
            if (FLG_MAESTRIA==false )
            {
                rdoMaestria.SelectedIndex = 0;
            }
            else
            {
                rdoMaestria.SelectedIndex = 1;
            }
           
            rdoSoftware.SelectedValue = dtResultado.Rows[0]["NIVEL_EXP_SOFTWARE"].ToString();

            int IDE_SEXO = Convert.ToInt32(dtResultado.Rows[0]["IDE_SEXO"].ToString());
            if (IDE_SEXO == 0 )
            {
                rdoSexo.SelectedIndex = 0;
            }
            else
            {
                rdoSexo.SelectedIndex = 1;
            }


            ddlcivil.SelectedValue = dtResultado.Rows[0]["IDE_ESTADO_CIVIL"].ToString();
            txtFuncionesPuesto.Text = dtResultado.Rows[0]["FUNCIONES_PUESTO"].ToString();
            txtRemuneracion.Text = dtResultado.Rows[0]["SUELDO"].ToString();
            txtComisiones.Text = dtResultado.Rows[0]["COMISIONES"].ToString();


            Boolean FLG_GRATIFICACIONES = Convert.ToBoolean(dtResultado.Rows[0]["FLG_GRATIFICACIONES"].ToString());
            if (FLG_GRATIFICACIONES == false )
            {
                rdoGratificaciones.SelectedIndex = 0;
            }
            else
            {
                rdoGratificaciones.SelectedIndex = 1;
            }

          

            Boolean FLG_PREMIO_OBRA = Convert.ToBoolean(dtResultado.Rows[0]["FLG_PREMIO_OBRA"].ToString());
            if (FLG_PREMIO_OBRA == false )
            {
                rdoPremioObra.SelectedIndex = 0;
            }
            else
            {
                rdoPremioObra.SelectedIndex = 1;
            }
           

 
            txtinicio.Text = dtResultado.Rows[0]["INICIO_CONTRATO"].ToString();
            txtfin.Text = dtResultado.Rows[0]["TERMINO_CONTRATO"].ToString();

            Boolean FLG_VALE_ALIMENTO = Convert.ToBoolean(dtResultado.Rows[0]["FLG_VALE_ALIMENTO"].ToString());
            if (FLG_VALE_ALIMENTO == false )
            {
                rdoValesAlimento.SelectedIndex = 0;
            }
            else
            {
                rdoValesAlimento.SelectedIndex = 1;
            }


            Boolean FLG_SEGURO_VIDA = Convert.ToBoolean(dtResultado.Rows[0]["FLG_SEGURO_VIDA"].ToString());
            if (FLG_SEGURO_VIDA == false )
            {
                rdoSeguroVida.SelectedIndex = 0;
            }
            else
            {
                rdoSeguroVida.SelectedIndex = 1;
            }


            Boolean FLG_ASIG_MOVILIDAD = Convert.ToBoolean(dtResultado.Rows[0]["FLG_ASIG_MOVILIDAD"].ToString());
            if (FLG_ASIG_MOVILIDAD == false )
            {
                rdoAsignacionMovil.SelectedIndex = 0;
            }
            else
            {
                rdoAsignacionMovil.SelectedIndex = 1;
            }
           
           

            txtregimen.Text = dtResultado.Rows[0]["REGIMEN_TRABAJO"].ToString();
            txtHorarioTrabajo.Text = dtResultado.Rows[0]["HORARIO_TRABAJO"].ToString();

            Boolean FLG_BONO_DESTAQUE = Convert.ToBoolean(dtResultado.Rows[0]["FLG_BONO_DESTAQUE"].ToString());
            if (FLG_BONO_DESTAQUE == false )
            {
                rdoBonoDestaque.SelectedIndex = 0;
            }
            else
            {
                rdoBonoDestaque.SelectedIndex = 1;
            }

            string IDE_PASAJE = dtResultado.Rows[0]["IDE_PASAJE"].ToString();
            if (IDE_PASAJE!= string.Empty )
            {
                rdoPasaje.SelectedValue = dtResultado.Rows[0]["IDE_PASAJE"].ToString();
            }
        
            txtComentarioGnral.Text = dtResultado.Rows[0]["COMENTARIOS_GNRAL"].ToString();
            //= dtResultado.Rows[0]["IDE_SOLICITANTE"].ToString();
            //= dtResultado.Rows[0]["IDE_GERENTE "].ToString();
            //= dtResultado.Rows[0]["USER_REGISTRO "].ToString();
            //= dtResultado.Rows[0]["IDE_GERENCIA"].ToString();
            AsignacionDetalle();

            int FLG_ESTADO =Convert.ToInt32( dtResultado.Rows[0]["FLG_ESTADO"].ToString());
            hdEstado.Value = dtResultado.Rows[0]["FLG_ESTADO"].ToString();
            //FLG_ESTADO = 1 pendiente de envio
            //FLG_ESTADO = 2 enviado
            //FLG_ESTADO = 3 atendido
            //FLG_ESTADO = 4 anulad
            if (FLG_ESTADO ==1)
            {
                //btnGuardar.Visible = true;
                btnEnviar.Visible = true;
                controles(true);
                btnNotificar.Visible = false;
            }
            else if (FLG_ESTADO == 2)
            {
                //btnGuardar.Visible = false ;
                btnEnviar.Visible = false ;
                controles(false);
                btnNotificar.Visible = true;

            }
            else if (FLG_ESTADO == 3)
            {
                //btnGuardar.Visible = false;
                btnEnviar.Visible = false;
                controles(false);
                btnNotificar.Visible = true  ;
            }

            int FLG_URL = Convert.ToInt32( dtResultado.Rows[0]["FLG_URL"].ToString());
            if(FLG_URL == 1)
            {
                HyperLink1.Visible = true;
                HyperLink1.NavigateUrl = dtResultado.Rows[0]["FILE_URL"].ToString();
                HyperLink1.Text = "Descargar solicitud";
            }
            else
            {
                HyperLink1.Visible = false ;
            }
        }
    }

    protected void controles (Boolean a)
    {
        rdoEquipo.Enabled = a;
        CheckSoftware.Enabled = a;
        CheckOtros.Enabled = a;
    }
    protected void AsignacionDetalle()
    {
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtEquipos = new DataTable();
        DataTable dtSoftware = new DataTable();
      
        dtEquipos = obj.uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS(hdcodigo.Value, "COMPUTO", "ASIGNACION_DETALLE");
        if (dtEquipos.Rows.Count > 0)
        {
            rdoEquipo.SelectedValue = dtEquipos.Rows[0]["IDE_EQUIPOS"].ToString();
            equipo();
        }

        ListarSoftware();
        ListarOtros();
    }

    protected void ListarSoftware()
    {

        // read previously chosen items from database
        con.Open();
        SqlCommand cmd = new SqlCommand("uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("IDE_ASIGNACION ", SqlDbType.Int).Value = Convert.ToInt32(hdcodigo.Value );
        cmd.Parameters.Add("DES_DESCRIPCION", SqlDbType.VarChar, 100).Value = "SOFTWARE";
        cmd.Parameters.Add("DES_TABLA", SqlDbType.VarChar, 100).Value = "ASIGNACION_DETALLE";
        SqlDataReader reader = cmd.ExecuteReader();

        // iterate through saved entries and add to Hashtable
        Hashtable savedEntries = new Hashtable();
        while (reader.Read())
        {
            string hobbyID = reader["IDE_EQUIPOS"].ToString();
            savedEntries[hobbyID] = true;
        }
        con.Close();

        // check the corresponding boxes
        CheckSoftware.DataBind();
        //cblHobbies.DataBind();
        foreach (ListItem li in CheckSoftware.Items)
        {
            if (savedEntries.ContainsKey(li.Value))
            {
                li.Selected = true;
            }
        }
    }
    protected void ListarOtros()
    {

        // read previously chosen items from database
        con.Open();
        SqlCommand cmd = new SqlCommand("uspSEL_LISTAR_RRHH_SOLICITUD_RECURSOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("IDE_ASIGNACION ", SqlDbType.Int).Value = Convert.ToInt32(hdcodigo.Value);
        cmd.Parameters.Add("DES_DESCRIPCION", SqlDbType.VarChar, 100).Value = "OTROS";
        cmd.Parameters.Add("DES_TABLA", SqlDbType.VarChar, 100).Value = "ASIGNACION_DETALLE";
        SqlDataReader reader = cmd.ExecuteReader();

        // iterate through saved entries and add to Hashtable
        Hashtable savedEntries = new Hashtable();
        while (reader.Read())
        {
            string hobbyID = reader["IDE_EQUIPOS"].ToString();
            savedEntries[hobbyID] = true;
        }
        con.Close();

        // check the corresponding boxes
        CheckOtros.DataBind();
        foreach (ListItem li in CheckOtros.Items)
        {
            if (savedEntries.ContainsKey(li.Value))
            {
                li.Selected = true;
            }
        }
    }
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersonal"))
            {
                string strSelectedValue = ddlPersonal.SelectedValue;
                //Your code here ... 
            }


        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));


            ddlJefe.DataSource = dtResultado;
            ddlJefe.DataTextField = "NOMBRE_COMPLETO";
            ddlJefe.DataValueField = "ID_DNI";
            ddlJefe.DataBind();
            ddlJefe.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));


            ddlAprobador.DataSource = dtResultado;
            ddlAprobador.DataTextField = "NOMBRE_COMPLETO";
            ddlAprobador.DataValueField = "ID_DNI";
            ddlAprobador.DataBind();
            ddlAprobador.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));


            string cleanMessage = "No existe personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void cargos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCargos.DataSource = obj.ListarParametros("IDE_CARGO", "RRHH_MOI");
        ddlCargos.DataTextField = "DES_ASUNTO";
        ddlCargos.DataValueField = "ID_PARAMETRO";
        ddlCargos.DataBind();
        this.ddlCargos.Items.Insert(0, new ListItem("--- Seleccionar ---", "0"));

    }
    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(1, "", 1);
        dtResultado = obj.uspLISTAR_GERENCIA(Convert.ToInt32(ddlEmpresas.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerencia.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerencia.DataBind();
            centros();

        }
        else
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataBind();

            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(2, ddlGerencia.SelectedValue.ToString(), 1);
        dtResultado = obj.uspLISTAR_GERENCIA_X_CENTROS(ddlGerencia.SelectedValue.ToString(), 1);

        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlCentro.DataBind();

        }
        else
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
    }

    protected void TipoProceso()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        RdoTipoProceso.DataSource = obj.ListarParametros("TIPO_PROCESO", "RRHH_SOLICITUD_ASIGNACION");
        RdoTipoProceso.DataTextField = "DES_ASUNTO";
        RdoTipoProceso.DataValueField = "ID_PARAMETRO";
        RdoTipoProceso.DataBind();
    }
    protected void OrigenPosicion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        rdoOrigen.DataSource = obj.ListarParametros("ORIGEN_POSICION", "RRHH_SOLICITUD_ASIGNACION");
        rdoOrigen.DataTextField = "DES_ASUNTO";
        rdoOrigen.DataValueField = "ID_PARAMETRO";
        rdoOrigen.DataBind();
    }
    protected void ReclutamientoObra()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        rdoRecObra.DataSource = obj.ListarParametros("TIPO_RECLUT_OBRA", "RRHH_SOLICITUD_ASIGNACION");
        rdoRecObra.DataTextField = "DES_ASUNTO";
        rdoRecObra.DataValueField = "ID_PARAMETRO";
        rdoRecObra.DataBind();
    }

    protected void ReclutamientoLima()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        rdoRecLima.DataSource = obj.ListarParametros_orden("TIPO_RECLUT_LIMA", "RRHH_SOLICITUD_ASIGNACION");
        rdoRecLima.DataTextField = "DES_ASUNTO";
        rdoRecLima.DataValueField = "ID_PARAMETRO";
        rdoRecLima.DataBind();
    }
    protected void AcademicoNivel()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlNivelAcademico.DataSource = obj.ListarParametros_orden("NIVEL", "RRHH_FORMATIVO_FICHA");
        ddlNivelAcademico.DataTextField = "DES_ASUNTO";
        ddlNivelAcademico.DataValueField = "ID_PARAMETRO";
        ddlNivelAcademico.DataBind();

        ddlNivelAcademico.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void ParametrosCarrera()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlcarrera.DataSource = obj.ListarParametros_orden("CARRERA", "RRHH_FORMATIVO_FICHA");
        ddlcarrera.DataTextField = "DES_ASUNTO";
        ddlcarrera.DataValueField = "ID_PARAMETRO";
        ddlcarrera.DataBind();

        ddlcarrera.Items.Insert(0, new ListItem("--- Seleccionar carrera---", ""));
    }

    protected void Ingles()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros_orden("NIVEL_EXPERIENCIA", "RRHH_SOLICITUD_ASIGNACION");
        rdoIngles.DataSource = dtResultado;
        rdoIngles.DataTextField = "DES_ASUNTO";
        rdoIngles.DataValueField = "ID_PARAMETRO";
        rdoIngles.DataBind();

        rdoSoftware.DataSource = dtResultado;
        rdoSoftware.DataTextField = "DES_ASUNTO";
        rdoSoftware.DataValueField = "ID_PARAMETRO";
        rdoSoftware.DataBind();

    }
    protected void ParametrosCivil()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlcivil.DataSource = obj.ListarParametros_orden("ESTADO_CIVIL", "RRHH_FORMATIVO_FICHA");
        ddlcivil.DataTextField = "DES_ASUNTO";
        ddlcivil.DataValueField = "ID_PARAMETRO";
        ddlcivil.DataBind();

        ddlcivil.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void Pasajes()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        rdoPasaje.DataSource = obj.ListarParametros_orden("IDE_PASAJE", "RRHH_SOLICITUD_ASIGNACION");
        rdoPasaje.DataTextField = "DES_ASUNTO";
        rdoPasaje.DataValueField = "ID_PARAMETRO";
        rdoPasaje.DataBind();
    }
    protected void equipo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        rdoEquipo.DataSource = obj.ListarParametros("COMPUTO", "ASIGNACION_DETALLE");
        rdoEquipo.DataTextField = "DES_ASUNTO";
        rdoEquipo.DataValueField = "ID_PARAMETRO";
        rdoEquipo.DataBind();
        //AsignacionDetalle();
    }
    protected void Software()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        CheckSoftware.DataSource = obj.ListarParametros("SOFTWARE", "ASIGNACION_DETALLE");
        CheckSoftware.DataTextField = "DES_ASUNTO";
        CheckSoftware.DataValueField = "ID_PARAMETRO";
        CheckSoftware.DataBind();
    }
    protected void Otros()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        CheckOtros.DataSource = obj.ListarParametros("OTROS", "ASIGNACION_DETALLE");
        CheckOtros.DataTextField = "DES_ASUNTO";
        CheckOtros.DataValueField = "ID_PARAMETRO";
        CheckOtros.DataBind();
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarRecurso();
    }
    protected void GuardarRecurso()
    {
        string cleanMessage = string.Empty;
        if (ddlCargos.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar cargo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (RdoTipoProceso.SelectedIndex < 0)
        {
            cleanMessage = "Indicar tipo proceso";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (rdoOrigen.SelectedIndex < 0)
        {
            cleanMessage = "Indicar origen posición";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (rdoRecObra.SelectedIndex < 0)
        {
            cleanMessage = "Indicar reclutamiento";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (rdoRecLima.SelectedIndex < 0)
        {
            cleanMessage = "Indicar reclutamiento lugar";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }


        else if (ddlNivelAcademico.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar formación academica";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlcarrera.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar especialidad y/o carrera";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (rdoIngles.SelectedIndex < 0)
        {
            cleanMessage = "Indicar nivel de ingles";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (rdoSoftware.SelectedIndex < 0)
        {
            cleanMessage = "Indicar nivel de software";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (ddlcivil.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar estado civil";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtinicio.Text == string.Empty )
        {
            cleanMessage = "Ingresar fecha de inicio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {


            // Si el directorio no existe, crearlo
            if (!Directory.Exists(Server.MapPath(FolderMOI)))
                Directory.CreateDirectory(FolderMOI);

            String fileExtension = string.Empty;
            Boolean fileOK = false;
            string fileArchivo = string.Empty;
            if (FileUpload1.HasFile)
            {

                string fileName = FileUpload1.FileName;
                int length = FileUpload1.PostedFile.ContentLength;

                fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension.ToUpper() == allowedExtensions[i].ToUpper())
                    {
                        fileOK = true;
                    }
                }
            }
            string archivo = string.Empty;
            if (fileOK)
            {
                try
                {

                    // Se carga la ruta física de la carpeta temp del sitio
                    string ruta = Server.MapPath(FolderMOI);

                    // Si el directorio no existe, crearlo
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                     archivo = String.Format("{0}\\{1}", ruta, FileUpload1.FileName);

                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        archivo = ruta + fileArchivo;
                        FileUpload1.SaveAs(archivo);
                    }

                    else
                    {
                        fileArchivo = FileUpload1.PostedFile.FileName;
                        FileUpload1.SaveAs(archivo);
                    }
                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }

            BE_RRHH_SOLICITUD_ASIGNACION oBESol = new BE_RRHH_SOLICITUD_ASIGNACION();
            oBESol.IDE_ASIGNACION = Convert.ToInt32(string.IsNullOrEmpty(hdcodigo.Value) ? "0" : hdcodigo.Value);

            string IDE_POSTULANTE = string.Empty;
            if (ddlPersonal.SelectedIndex > 0)
            {
                IDE_POSTULANTE = ddlPersonal.SelectedValue.ToString();
            }
            oBESol.IDE_POSTULANTE = IDE_POSTULANTE;
            oBESol.COD_CENTRO = ddlCentro.SelectedValue;
            oBESol.IDE_CARGO = Convert.ToInt32(ddlCargos.SelectedValue);
            oBESol.AREA = txtarea.Text.Trim();

            string JEFE_DNI = string.Empty;
            if (ddlJefe.SelectedIndex > 0)
            {
                JEFE_DNI = ddlJefe.SelectedValue.ToString();
            }

            oBESol.JEFE_DNI = JEFE_DNI;
            oBESol.UBICACION = string.Empty;
            oBESol.TIPO_PROCESO = Convert.ToInt32(RdoTipoProceso.SelectedValue);
            oBESol.ORIGEN_POSICION = Convert.ToInt32(rdoOrigen.SelectedValue);
            oBESol.TIPO_RECLUT_OBRA = Convert.ToInt32(rdoRecObra.SelectedValue);
            oBESol.TIPO_RECLUT_LIMA = Convert.ToInt32(rdoRecLima.SelectedValue); ;
            oBESol.PISO = string.Empty;
            oBESol.IDE_NIVEL_ACADEMICO = Convert.ToInt32(ddlNivelAcademico.SelectedValue);
            oBESol.IDE_CARRERA = Convert.ToInt32(ddlcarrera.SelectedValue);
            oBESol.CARRERA_COMENTARIOS = string.Empty; ;
            oBESol.NRO_COLEGIATURA = txtColegiatura.Text;
            oBESol.FLG_COLEGIATURA = Convert.ToInt32(RdoColegiatura.SelectedValue);
            oBESol.NIVEL_EXP_INGLES = Convert.ToInt32(rdoIngles.SelectedValue);
            oBESol.FLG_MAESTRIA = Convert.ToInt32(rdoMaestria.SelectedValue);
            oBESol.NIVEL_EXP_SOFTWARE = Convert.ToInt32(rdoSoftware.SelectedValue);
            oBESol.IDE_SEXO = Convert.ToInt32(rdoSexo.SelectedValue);
            oBESol.IDE_ESTADO_CIVIL = Convert.ToInt32(ddlcivil.SelectedValue);
            oBESol.FUNCIONES_PUESTO = txtFuncionesPuesto.Text.Trim();
            oBESol.SUELDO = Convert.ToDecimal(string.IsNullOrEmpty(txtRemuneracion.Text.Trim()) ? "0" : txtRemuneracion.Text.Trim());
            oBESol.COMISIONES = Convert.ToDecimal(string.IsNullOrEmpty(txtComisiones.Text.Trim()) ? "0" : txtComisiones.Text.Trim());
            oBESol.FLG_GRATIFICACIONES = Convert.ToInt32(rdoGratificaciones.SelectedValue);
            oBESol.FLG_PREMIO_OBRA = Convert.ToInt32(rdoPremioObra.SelectedValue);
            oBESol.INICIO_CONTRATO = txtinicio.Text;
            oBESol.TERMINO_CONTRATO = txtfin.Text;
            oBESol.FLG_VALE_ALIMENTO = Convert.ToInt32(rdoValesAlimento.SelectedValue);
            oBESol.FLG_SEGURO_VIDA = Convert.ToInt32(rdoSeguroVida.SelectedValue);
            oBESol.FLG_ASIG_MOVILIDAD = Convert.ToInt32(rdoAsignacionMovil.SelectedValue);
            oBESol.OTROS_BENEFICIOS = string.Empty;
            oBESol.REGIMEN_TRABAJO = txtregimen.Text;
            oBESol.HORARIO_TRABAJO = txtHorarioTrabajo.Text;
            oBESol.FLG_BONO_DESTAQUE = 0;

            int IDE_PASAJE = Convert.ToInt32(string.IsNullOrEmpty(rdoPasaje.SelectedValue) ? "0" : rdoPasaje.SelectedValue);

            oBESol.IDE_PASAJE = IDE_PASAJE;
            oBESol.COMENTARIOS_GNRAL = txtComentarioGnral.Text.Trim();
            oBESol.IDE_SOLICITANTE = string.Empty;
            oBESol.IDE_GERENTE = ddlAprobador.SelectedValue.ToString();
            oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
            oBESol.IDE_GERENCIA = ddlGerencia.SelectedValue.ToString();
            oBESol.DNI_TMP = txtDni.Text.Trim();
            oBESol.NOMBRE_TMP = txtNombre.Text.Trim();
            oBESol.APE_PAT_TMP = txtPaterno.Text.Trim();
            oBESol.APE_MAT_TMP = txtMaterno.Text.Trim();
            oBESol.ID_EMPRESA = ddlEmpresas.SelectedValue;
            oBESol.FILE_SOL = fileArchivo;

            oBESol.FILE_URL= UrlSSK + FolderMOI.Replace("~", "") + fileArchivo;
            oBESol.FILE_DIR = Server.MapPath(FolderMOI); 
            DataTable dtrpta = new DataTable();
            dtrpta = new BL_RRHH_SOLICITUD_ASIGNACION().uspINS_RRHH_SOLICITUD_ASIGNACION(oBESol);
            if (dtrpta.Rows.Count > 0)
            {
                BL_RRHH_SOLICITUD_ASIGNACION Xobj = new BL_RRHH_SOLICITUD_ASIGNACION();
                DataTable dtResultado = new DataTable();

                hdEstado.Value = dtrpta.Rows[0]["FLG_ESTADO"].ToString();
                hdcodigo.Value = dtrpta.Rows[0]["IDE_ASIGNACION"].ToString();
                string OPERACION = dtrpta.Rows[0]["OPERACION"].ToString();
                string CODIGO_CARE_PADRE = dtrpta.Rows[0]["CODIGO_CARE_PADRE"].ToString();
                string NOMBRE_COMPLETO = dtrpta.Rows[0]["NOMBRE_COMPLETO"].ToString();
                string FILE_URL = dtrpta.Rows[0]["FILE_URL"].ToString();

                //SELECCIONAR EQUIPO
                string equipo = rdoEquipo.SelectedValue;

                //seleccionar sofware
                string s = string.Empty;
                int cantidadSoft = 0;
                for (int i = 0; i < CheckSoftware.Items.Count; i++)
                {
                    if (CheckSoftware.Items[i].Selected)
                    {
                        cantidadSoft++;
                        s += CheckSoftware.Items[i].Value + ",";
                    }
                 

                }

                // seleccionar Otros
                string Otros = string.Empty;
                int cantidadOtros = 0;
                for (int i = 0; i < CheckOtros.Items.Count; i++)
                {
                    if (CheckOtros.Items[i].Selected)
                    {
                        cantidadOtros++;
                        Otros += CheckOtros.Items[i].Value + ",";
                    }

                }

               
                
                //FLG_ESTADO = 1 pendiente de envio
                //FLG_ESTADO = 2 enviado
                //FLG_ESTADO = 3 atendido
                //FLG_ESTADO = 4 anulad

                if(hdEstado.Value == string.Empty || hdEstado.Value == "")
                {
                    hdEstado.Value = "1";
                }

                if(hdEstado.Value  =="1")
                {
                    if(cantidadOtros + cantidadSoft > 0)
                    {
                        dtResultado = Xobj.uspINS_RRHH_SOLICITUD_RECURSOS(hdcodigo.Value, equipo + "," + s + Otros, Session["IDE_USUARIO"].ToString());
                    }
                   
                   
                }
                if (OPERACION == "INSERT")
                {
                    string CODIGO_CARE = string.Empty;
                    // insertamos en el care

                    //insertamos en el mobile
                    //BL_MOBILE Mobj = new BL_MOBILE();
                    //DataTable MdtResultado = new DataTable();

                    //MdtResultado = Mobj.usp_CARE_InsertarSolicitud_Generico
                    //    (
                    //     DateTime.Today.ToString(),
                    //     ddlEmpresas.SelectedValue,
                    //     RdoTipoProceso.SelectedItem.ToString(),
                    //     txtPersonalNuevo.Text.Trim(),
                    //     BL_Session.UsuarioNombre,
                    //     CODIGO_CARE

                    //    );

                }

                //ACTUALIZACION DE NOMBRE PENDIETE
                else if (OPERACION == "UPDATE")
                {
                    string IDE_POSTULANTE_ = dtrpta.Rows[0]["IDE_POSTULANTE"].ToString();
                    if (IDE_POSTULANTE_ != string.Empty && hdPersonal.Value == string.Empty)
                    {

                    }

                    //OBTENER DNI DEL PERSONAL MOBILE

                    BL_MOBILE objPer = new BL_MOBILE();
                    DataTable dtPers = new DataTable();
                    if (txtDni.Text.Trim() != string.Empty)
                    {
                        dtPers = objPer.usp_Trabajador_x_dni(txtDni.Text.Trim(), txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), txtNombre.Text.Trim());

                    }
                    else
                    {
                        dtPers = objPer.usp_Trabajador_x_dni(ddlPersonal.SelectedValue, "", "", "");
                    }
                    string IdTrabajador = string.Empty;
                    if (dtPers.Rows.Count > 0)
                    {
                        IdTrabajador = dtPers.Rows[0]["IdTrabajador"].ToString();
                    }
                    objPer.usp_NombreTrabajador_reqMovil(IdTrabajador, NOMBRE_COMPLETO, CODIGO_CARE_PADRE, FILE_URL);



                    //////// FIN MOBILE PERSONA //////////////////

                    //enviamos correo de personal agregado

                    BL_RRHH_SOLICITUD_ASIGNACION _obj = new BL_RRHH_SOLICITUD_ASIGNACION();
                    DataTable _dtResultado = new DataTable();
                    _dtResultado = _obj.uspUPD_CANDIDADTO_RECURSOS_CARE_NUEVO(hdcodigo.Value, IdTrabajador);
                    //_dtResultado = _obj.usp_correo_responsable_recursos(hdcodigo.Value, "2");


                }

                Datos(hdcodigo.Value);

                cleanMessage = "Registro exitoso.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {

        Session.Remove("IDE_ASIGNACION");
        hdcodigo.Value = string.Empty;

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void btnBandeeja_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/SolicitudReclutamientoAll");
    }



    protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();
        if (hdcodigo.Value != string.Empty)
        {
            if (hdEstado.Value == "1")
            {

                BL_RRHH_SOLICITUD_ASIGNACION obj_ = new BL_RRHH_SOLICITUD_ASIGNACION();
                DataTable dtResultado_ = new DataTable();

                //REVISAMOS SI HAY RECURSOS PARA EL MOBILE
                dtResultado_ = obj_.uspSEL_LISTAR_RECURSOS_SOLMOBILE(hdcodigo.Value, "RECURSOS MOVIL");
                if (dtResultado_.Rows.Count > 0)
                {
                    if(ddlAprobador.SelectedValue == string.Empty )
                    {
                        cleanMessage = "Indicar aprobador";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                    }
                    else
                    {

                        BL_RRHH_SOLICITUD_ASIGNACION _objAprobador = new BL_RRHH_SOLICITUD_ASIGNACION();
                         _objAprobador.uspUPD_RRHH_SOLICITUD_ASIGNACION_APROBADOR(hdcodigo.Value, ddlAprobador.SelectedValue.ToString());

                        //RECURSOS MOVIL ENVIAMOS NOTIFICACION AL APROBADOR
                        BL_RRHH_SOLICITUD_ASIGNACION _obj = new BL_RRHH_SOLICITUD_ASIGNACION();
                        DataTable _dtResultado = new DataTable();

                        _dtResultado = _obj.usp_correo_notificar_apobrador_asignacion(hdcodigo.Value, "RECURSOS MOVIL",1);

                        //ENVIAR DATOS AL CARE
                        dtResultado = obj.uspSEL_ENVIAR_RECURSOS_CARE_NUEVO(hdcodigo.Value);
                        
                        //NOTIFICAR TODOS EXCEPTO MOBILE
                        obj.usp_correo_responsable_recursos_excepcion(hdcodigo.Value, "1", "RECURSOS MOVIL");

                        cleanMessage = "Envio satisfactorio";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                    }
                }

                else
                {
                    //proceso tal cual
                    EnviarRecursos();
                }
            }
        }
        else
        {
            cleanMessage = "No se puede procesar operación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void EnviarRecursos()
    {
        //GuardarRecurso();
        string cleanMessage = string.Empty;
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();
        if (hdcodigo.Value != string.Empty)
        {

            //FLG_ESTADO = 1 pendiente de envio
            //FLG_ESTADO = 2 enviado
            //FLG_ESTADO = 3 atendido
            //FLG_ESTADO = 4 anulad



            // ENVIAR DATOS AL CARE Y MOBILE 
            if (hdEstado.Value == "1")
            {
                //ENVIAR DATOS AL CARE
                dtResultado = obj.uspSEL_ENVIAR_RECURSOS_CARE_NUEVO(hdcodigo.Value);
                //NOTIFICAR
                obj.usp_correo_responsable_recursos(hdcodigo.Value, "1");

                cleanMessage = "Envio satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                if (hdEstado.Value == "1")
                {
                    BL_RRHH_SOLICITUD_ASIGNACION obj_ = new BL_RRHH_SOLICITUD_ASIGNACION();
                    DataTable dtResultado_ = new DataTable();

                    //REVISAMOS SI HAY RECURSOS PARA EL MOBILE
                    dtResultado_ = obj_.uspSEL_LISTAR_RECURSOS_SOLMOBILE(hdcodigo.Value, "RECURSOS MOVIL");
                    if (dtResultado_.Rows.Count > 0)
                    {
                        string personal = txtPaterno.Text.Trim() + ' ' + txtMaterno.Text.Trim() + ' ' + txtNombre.Text.Trim();
                        string CODIGO_CARE_PADRE = dtResultado_.Rows[0]["CODIGO_CARE_PADRE"].ToString();
                        string IdTrabajador = string.Empty;
                        //OBTENER DNI DEL PERSONAL MOBILE

                        BL_MOBILE objPer = new BL_MOBILE();
                        DataTable dtPers = new DataTable();
                        if (txtDni.Text.Trim() != string.Empty)
                        {
                            dtPers = objPer.usp_Trabajador_x_dni(txtDni.Text.Trim(), txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), txtNombre.Text.Trim());

                        }
                        else
                        {
                            dtPers = objPer.usp_Trabajador_x_dni(ddlPersonal.SelectedValue, "", "", "");
                        }

                        if (dtPers.Rows.Count > 0)
                        {
                            IdTrabajador = dtPers.Rows[0]["IdTrabajador"].ToString();
                        }
                        //////// FIN MOBILE PERSONA //////////////////

                        ///insertamos los recursos al care y mobile
                        for (int i = 0; i < dtResultado_.Rows.Count; i++)
                        {

                            string CODIGO_EQUIPO = dtResultado_.Rows[i]["CODIGO_EXT"].ToString();//codigo del recurso en mobile
                            string FILE_URL = dtResultado_.Rows[i]["FILE_URL"].ToString();
                            string TICKET = dtResultado_.Rows[i]["TICKET"].ToString();
                            string TIPO_EQUIPO = string.Empty;
                            string CODIGO_CARE = string.Empty;
                            //ENVIAR RECURSOS CARE 

                            BE_RequerimientoMovil Obj = new BE_RequerimientoMovil();
                            Obj.IdRequerimiento = 0;
                            Obj.FechaSolicitud = txtinicio.Text;
                            Obj.IdEmpresaPK = Convert.ToInt32(ddlEmpresas.SelectedValue);//analizar
                            Obj.centro_costo = ddlCentro.SelectedValue;
                            Obj.Requ_Numero = CODIGO_CARE_PADRE;
                            DataTable dtrpta = new DataTable();

                            dtrpta = new BL_RRHH_SOLICITUD_ASIGNACION().uspINS_RequerimientoMovil(Obj);
                            if (dtrpta.Rows.Count > 0)
                            {
                                BE_RequerimientoMovil_Detalle ObjD = new BE_RequerimientoMovil_Detalle();

                                if (CODIGO_EQUIPO == "1")
                                {
                                    TIPO_EQUIPO = "CELULAR";
                                }
                                else
                                {
                                    TIPO_EQUIPO = "MODEM";
                                }
                                ObjD.id_detalle = 0;
                                ObjD.NombreSolicitante = personal;
                                ObjD.FechaRequerida = txtinicio.Text;
                                ObjD.MesesRequerido = 12;
                                ObjD.LugarEntrega = RdoTipoProceso.SelectedItem.ToString();
                                ObjD.IdTipoEquipo = Convert.ToInt32(CODIGO_EQUIPO);
                                ObjD.IdRequerimiento = Convert.ToInt32(dtrpta.Rows[0]["IdRequerimiento"].ToString());
                                ObjD.Dni_Trabajador = txtDni.Text;
                                ObjD.cantidad = Convert.ToInt32(1);
                                ObjD.USER_CREACION = "RECURSOS HUMANOS";
                                ObjD.IdTrabajador = Convert.ToInt32(IdTrabajador);
                                ObjD.TipoEquipo = TIPO_EQUIPO;
                                ObjD.IdOperadorMovil = 1;
                                ObjD.Operador = "CLARO";

                                DataTable dtrpta_detalle = new DataTable();
                                dtrpta_detalle = new BL_RRHH_SOLICITUD_ASIGNACION().uspINS_RequerimientoMovil_Detalle_SIG(ObjD);
                                if (dtrpta_detalle.Rows.Count > 0)
                                {
                                    CODIGO_CARE = dtrpta_detalle.Rows[0]["GUID_CODIGO"].ToString();
                                }
                            }

                            //MOBILE
                            BL_MOBILE Mobj = new BL_MOBILE();
                            DataTable MdtResultado = new DataTable();


                            MdtResultado = Mobj.usp_CARE_InsertarSolicitud_Generico
                                (
                                 DateTime.Today.ToString("dd/MM/yyyy"),
                                 ddlEmpresas.SelectedValue,
                                 txtinicio.Text,
                                 RdoTipoProceso.SelectedItem.ToString(),
                                 personal,
                                 BL_Session.UsuarioNombre,
                                 CODIGO_CARE,
                                 CODIGO_EQUIPO,
                                 CODIGO_CARE_PADRE,
                                 FILE_URL, TICKET,"1"
                                );

                            //UNION CODIGO CARE-MOBILE
                            DataTable dtrpta_Mob_care = new DataTable();
                            dtrpta_Mob_care = new BL_RRHH_SOLICITUD_ASIGNACION().uspSEL_REQUERIMIENTOMOVIL_UPDATE_MOBILE(dtrpta.Rows[0]["IdRequerimiento"].ToString());
                        }

                    }
                }

                Datos(hdcodigo.Value);

            }

        }
        else
        {
            cleanMessage = "No se puede procesar operación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

    }

    protected void Checkpersonal_CheckedChanged(object sender, EventArgs e)
    {
        if(Checkpersonal.Checked)
        {
            ddlPersonal.Enabled = true;
        }
        else
        {
            ddlPersonal.Enabled = false ;
        }
    }

    protected void btnNotificar_Click(object sender, ImageClickEventArgs e)
    {
        //..
        if (hdcodigo.Value != string.Empty )
        {
            BL_RRHH_SOLICITUD_ASIGNACION _obj = new BL_RRHH_SOLICITUD_ASIGNACION();
            DataTable _dtResultado = new DataTable();

            _dtResultado = _obj.usp_correo_responsable_recursos(hdcodigo.Value, "2");
            string cleanMessage = "Envio satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
       
    }

    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
    }
}