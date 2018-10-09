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
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;
using Microsoft.Reporting.WebForms;

public partial class OPERACIONES_Minuta : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    DataSet ds = new DataSet();
    DataTable dtMes = new DataTable();
    DataSet dsSelDate;
    SqlConnection mycn;
    SqlDataAdapter myda;
    int numOrden;
    string listVal="0";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            cargarEmpleados();
            TabContainer1.ActiveTabIndex = 0;

            DateTime miFecha = DateTime.Now;

            int year = miFecha.Year;
            int month = miFecha.Month;
            int day = miFecha.Day;
            int hour = miFecha.Hour;
            int hourFin = hour + 1;
            int minute = miFecha.Minute;
            int second = miFecha.Second;

            txtHoraInicio.Text = hour.ToString() + ':' + minute.ToString();
            txtHoraFin.Text = hourFin.ToString() + ':' + minute.ToString();
            txtFechaMinuta.Text = miFecha.ToString("dd/MM/yyyy");
            txtCodigoProyecto.Text   = BL_Session.CENTRO_COSTO.ToString ();
            txtObra.Text = BL_Session.PROYECTO;
            BandejaResponsable();
            Personal();
            cargarArea();
            cargarEstado();
            cargarDestino();
            cargarEstadoReporte();
            cargarAreaReporte();
            cargarAno();
            Bandeja_Historial();
            cargarReunion();
            Bandeja_Compromisos();
            tablaMinutas02Div.Visible = false;
            //Calendar1.TodaysDate = miFecha;
            //Calendar1.SelectedDate = Calendar1.TodaysDate;
            dtMes =GetDataReunionesMes(year, month);
        }

       
    }
    //protected void Seleccionar(object sender, ImageClickEventArgs e)
    //{
    // aqui el calendario
    //    ImageButton btnVerMas = ((ImageButton)sender);
    //    lblCodigoMinuta.Text  = btnVerMas.CommandArgument;
        
    //    mostrarMinuta();
    //    DateTime miFecha = Convert.ToDateTime ( Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
    //    int year = miFecha.Year;
    //    int month = miFecha.Month;
       
    //    dtMes = GetDataReunionesMes(year, month);
    //    TabContainer1.ActiveTabIndex = 1;

    //}
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("Upnl"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddl"))
            {
                string strSelectedValue = ddl.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Upnl_LoadPersonal(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("UpnlPersona"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersona"))
            {
                string strSelectedValue = ddlPersona.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Upnl_UsuarioComent(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("UpnlUsuarioComent"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlUsuarioComt"))
            {
                string strSelectedValue = ddlUsuarioComt.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    private DataTable GetDataReunionesMes(int  ANIO, int MES)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_CONSULTAR_TEMAS_MES", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Anio", SqlDbType.Int ).Value = ANIO;
        cmd.Parameters.Add("@Mes", SqlDbType.Int ).Value = MES ;
        cmd.Parameters.Add("@dsc_proyecto", SqlDbType.VarChar, 10).Value = txtCodigoProyecto.Text ;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void CalendarDRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            // If the month is CurrentMonth
            if (!e.Day.IsOtherMonth)
            {
                foreach (DataRow dr in dtMes.Rows)
                {
                    if ((dr["fch_fecha_registro"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTime dtEvent = (DateTime)dr["fch_fecha_registro"];
                        if (dtEvent.Equals(e.Day.Date))
                        {
                            e.Cell.BackColor = Color.PaleVioletRed;
                        }
                    }
                }
            }
            //If the month is not CurrentMonth then hide the Dates
            else
            {
                e.Cell.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    //{
    // aqui calendario
    //    DateTime miFecha = Convert.ToDateTime(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
    //    int year = miFecha.Year;
    //    int month = miFecha.Month;

    //    dtMes = GetDataReunionesMes(year, month);
    //    FechaReunion(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
    //}
    //protected void FechaReunion(string FECHA)
    //{
    //    DataTable dtResultado = new DataTable();
    //    BL_OPE_MINUTA obj = new BL_OPE_MINUTA();
    //  dtResultado = obj.USP_CONSULTAR_TEMAS_MES(FECHA,txtCodigoProyecto.Text );
    //    if (dtResultado.Rows.Count == 0)
    //    {
    //        ListView1.Visible = false;
    //        ListView1.DataSource = dtResultado;
    //        ListView1.DataBind();
    //    }
    //    else
    //    {
    //        ListView1.Visible = true;
    //        ListView1.DataSource = dtResultado;
    //        ListView1.DataBind();

    //    }
    //}
    private void cargarEmpleados()
    {

        DataTable dtResultado = new DataTable();
        BL_OPE_MINUTA obj = new BL_OPE_MINUTA();
        //dtResultado = obj.USP_CONSULTAR_TEMAS_MES(FECHA, txtCodigoProyecto.Text); verificando de calender
        dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_POR_CC(BL_Session.CENTRO_COSTO);
        if (dtResultado.Rows.Count > 0)
        {


            ddlPersonalAcargo.DataSource = dtResultado;
            ddlPersonalAcargo.DataTextField = dtResultado.Columns["NOMBRE_COMPLETO"].ToString();
            ddlPersonalAcargo.DataValueField = dtResultado.Columns["ID_DNI"].ToString();
            ddlPersonalAcargo.DataBind();
            ddlPersonalAcargo.Items.Insert(0, new ListItem("--- Seleccionar Encargado ---", ""));
        }
        else
        {
            ddlPersonalAcargo.Items.Insert(0, new ListItem("--- Seleccionar Encargado ---", ""));
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ValidarCampos();
    }

    private void ValidarCampos()
    {
        string cleanMessage = string.Empty;
        if (ddlRegistro.SelectedIndex == 0)
        {
            cleanMessage = "Seleccione Tipo Reunión";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if(ddlListaDeFechas.SelectedIndex == 0)
        {
            cleanMessage = "Seleccione el Período de Minuta";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtFechaMinuta.Text == string.Empty)
        {
            cleanMessage = "Selecionar Fecha";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtHoraInicio.Text == string.Empty)
        {
            cleanMessage = "Ingresar Hora Inicial";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtHoraFin.Text == string.Empty)
        {
            cleanMessage = "Ingresar Hora Final";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (ddlPersonalAcargo.SelectedIndex == 0)
        {
            cleanMessage = "Seleccione Personal acargo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtLugarSede.Text == string.Empty)
        {
            cleanMessage = "Ingresar Lugar(Sede)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtAsunto.Text == string.Empty)
        {
            cleanMessage = "Ingresar Asunto";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else if (txtCliente.Text == string.Empty)
        {
            cleanMessage = "Ingresar nombre del Cliente";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {
            GuardarMinuta();
        }
    }

    private void GuardarMinuta()
    {
        string cleanMessage = "";
        string iStringInicio = txtFechaMinuta.Text + " " + txtHoraInicio.Text;// "2005-05-05 22:12";
        string iStringFin = txtFechaMinuta.Text + " " + txtHoraFin.Text; ;
        DateTime oDate = DateTime.ParseExact(iStringInicio, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        DateTime oDateFin = DateTime.ParseExact(iStringFin, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

        if (oDate <= oDateFin)
        {
            BE_OPE_MINUTA objMinuta = new BE_OPE_MINUTA();
            objMinuta = CapturarDatos();

            int rpta;
            rpta = new BL_OPE_MINUTA().Mant_Insert_Minuta(objMinuta);

            if (rpta > 0)
            {
                lblCodigoMinuta.Text = rpta.ToString();
                mostrarMinuta();
                TabContainer1.ActiveTabIndex = 2;
                Insertar_Personal_Minuta();
                ListarPersonal();
                cleanMessage = "Registro Exitoso";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                Bandeja_Minutas(); //Actualizando bandeja principal

            }
            else
            {
                cleanMessage = "Actualización Exitosa";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                Bandeja_Minutas(); //Actualizando bandeja principal                    
            }
        }
        else
        {
            cleanMessage = "El horario de termino no puede ser menor al horario de inicio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    private BE_OPE_MINUTA CapturarDatos()
    {
        BE_OPE_MINUTA objMinuta = new BE_OPE_MINUTA();
        string cleanMessage = string.Empty;

        objMinuta.M_id_minuta = Convert.ToInt32(string.IsNullOrEmpty(lblCodigoMinuta.Text) ? "0" : lblCodigoMinuta.Text);
        objMinuta.Id_dni = ddlPersonalAcargo.SelectedValue;
        objMinuta.Fch_fecha_registro = txtFechaMinuta.Text;
        objMinuta.Num_numero_registro = txtRegistro.Text;
        objMinuta.Dsc_tipo_fecha = ddlListaDeFechas.SelectedItem.Text;
        objMinuta.Dsc_asunto = txtAsunto.Text;
        objMinuta.Dsc_lugar = txtLugarSede.Text;
        objMinuta.Dsc_nombre_cliente = txtCliente.Text;
        objMinuta.Dsc_contrato = (string.IsNullOrEmpty(txtContrato.Text) ? " " : txtContrato.Text);
        objMinuta.Dsc_obra = txtObra.Text;
        objMinuta.Dsc_proyecto = txtCodigoProyecto.Text;//  ddlCodigoProyecto.SelectedItem.Text;
        objMinuta.Fch_HoraInicial = txtHoraInicio.Text;
        objMinuta.Fch_HoraFinal = txtHoraFin.Text;
        objMinuta.Dsc_reunion = ddlRegistro.SelectedValue;
        objMinuta.Usuario = BL_Session.UsuarioNombre;
        return objMinuta;
    }
    private void mostrarMinuta()
    {

        BE_OPE_MINUTA objMinuta = new BE_OPE_MINUTA();
        DataTable objResultado = new DataTable();
        objResultado = new BL_OPE_MINUTA().SeleccionarMinuta_BL(Convert.ToInt32(lblCodigoMinuta.Text));

        if (objResultado.Rows.Count > 0)
        {
            lblCodigoMinuta.Text = objResultado.Rows[0]["id_minuta"].ToString();
            ddlPersonalAcargo.SelectedValue = objResultado.Rows[0]["id_dni"].ToString();
            txtFechaMinuta.Text = objResultado.Rows[0]["fch_fecha_registro"].ToString();
            txtRegistro.Text = objResultado.Rows[0]["num_numero_registro"].ToString();
            ddlListaDeFechas.SelectedValue = objResultado.Rows[0]["dsc_tipo_fecha"].ToString();
            txtAsunto.Text = objResultado.Rows[0]["dsc_asunto"].ToString();
            txtAsuntoReunion.Text  = objResultado.Rows[0]["dsc_asunto"].ToString();
            txtLugarSede.Text = objResultado.Rows[0]["dsc_lugar"].ToString();
            txtCliente.Text = objResultado.Rows[0]["dsc_nombre_cliente"].ToString();
            txtContrato.Text = objResultado.Rows[0]["dsc_contrato"].ToString();
            txtObra.Text = objResultado.Rows[0]["dsc_obra"].ToString();
            //ddlCodigoProyecto.SelectedItem.Text = objResultado.Rows[0]["dsc_proyecto"].ToString();
            txtCodigoProyecto.Text = objResultado.Rows[0]["dsc_proyecto"].ToString();
            txtHoraInicio.Text = objResultado.Rows[0]["fch_HoraInicial"].ToString();
            txtHoraFin.Text = objResultado.Rows[0]["fch_HoraFinal"].ToString();
            ddlRegistro.SelectedValue = objResultado.Rows[0]["dsc_reunion"].ToString();


            DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
            int year = miFecha.Year;
            int month = miFecha.Month;

            dtMes = GetDataReunionesMes(year, month);

            ListarPersonal();
            ListarAcuerdos();
            BandejaResponsable();
        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_TODO();
        if (dtResultado.Rows.Count > 0)
        {
            ddl.DataSource = dtResultado;
            ddl.DataTextField = "NOMBRE_COMPLETO";
            ddl.DataValueField = "ID_DNI";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            ddl.Visible = true;


            ddlPersona.DataSource = dtResultado;
            ddlPersona.DataTextField = "NOMBRE_COMPLETO";
            ddlPersona.DataValueField = "ID_DNI";
            ddlPersona.DataBind();
            ddlPersona.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            ddlPersona.Visible = true;

            ddlUsuarioComt.DataSource = dtResultado;
            ddlUsuarioComt.DataTextField = "NOMBRE_COMPLETO";
            ddlUsuarioComt.DataValueField = "ID_DNI";
            ddlUsuarioComt.DataBind();
            ddlUsuarioComt.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            ddlUsuarioComt.Visible = true;



        }
        else
        {
            ddl.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            ddlPersona.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            ddlUsuarioComt.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }

    protected void BandejaResponsable()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dt = new DataTable();
        dt = obj.USP_SEL_RESPONSABLE_BL(BL_Session.CENTRO_COSTO);
        if (dt.Rows.Count > 0)
        {
            ddlBanResponsable.DataSource = dt;
            ddlBanResponsable.DataTextField = "nombreCompleto";
            ddlBanResponsable.DataValueField = "idDni";
            ddlBanResponsable.DataBind();
            ddlBanResponsable.Items.Insert(0,new ListItem("TODO",""));            
        }
        else
        {
            ddlBanResponsable.Items.Insert(0, new ListItem("TODO", ""));
        }

    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
      

        int rpta;

        if (lblCodigoMinuta.Text != string.Empty)
        {

            if (ddl.SelectedValue  != string.Empty)
            {
                BE_OPE_DETALLE_PERSONAL objAgregarPersonal = new BE_OPE_DETALLE_PERSONAL();
                objAgregarPersonal = AgregarPersonal();


                 rpta= new BL_OPE_DETALLE_PERSONAL().Man_Insert_Datos_PersonalBL(objAgregarPersonal);


                if (rpta > 0)
                {
                    ListarPersonal();
                    ddl.SelectedIndex = 0;
                }
                else
                {
                    cleanMessage = "Ya se encuentra registrado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "Seleccionar Personal";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else
        {
            cleanMessage = "Falta registrar Minuta";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    private BE_OPE_DETALLE_PERSONAL AgregarPersonal()
    {
        BE_OPE_DETALLE_PERSONAL objAgregarPersonal = new BE_OPE_DETALLE_PERSONAL();
        objAgregarPersonal.Detalle_personal = 1;
        objAgregarPersonal.Id_dni = ddl.SelectedValue;
        objAgregarPersonal.Id_minuta = Convert.ToInt32(lblCodigoMinuta.Text);

        //objAgregarPersonal.Centro_Costo = txtCodigoProyecto.Text;

        return objAgregarPersonal;
    }
    protected void Insertar_Personal_Minuta() {
        int rpta;
        BE_OPE_MINUTA objInsertaParticipantes = new BE_OPE_MINUTA();
        objInsertaParticipantes = Insertar_Personal();
        
        rpta = new BL_OPE_MINUTA().Insertar_Personal_Minuta(objInsertaParticipantes);
    }
    private BE_OPE_MINUTA Insertar_Personal()
    {
        BE_OPE_MINUTA objInsertar_Personal = new BE_OPE_MINUTA();
        objInsertar_Personal.M_id_minuta = Convert.ToInt32(lblCodigoMinuta.Text);
        objInsertar_Personal.Dsc_reunion = ddlRegistro.SelectedValue;
        objInsertar_Personal.Dsc_proyecto = txtCodigoProyecto.Text;

        return objInsertar_Personal;
    }

    protected void ListarPersonal()
    {
        DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
        int year = miFecha.Year;
        int month = miFecha.Month;

        dtMes = GetDataReunionesMes(year, month);

        BL_OPE_DETALLE_PERSONAL obj = new BL_OPE_DETALLE_PERSONAL();
        DataTable dt = new DataTable();
        dt = obj.SELECCIONAR_PARTICIPANTES(Convert.ToInt32(lblCodigoMinuta.Text));
        if (dt.Rows.Count > 0)
        {

            lstRol.DataSource = dt;
            lstRol.DataBind();
        }
        else
        {
            lstRol.DataSource = dt;
            lstRol.DataBind();

        }
    }
    protected void EliminarPersonal(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEliminar = ((ImageButton)sender);

        BL_OPE_DETALLE_PERSONAL obj = new BL_OPE_DETALLE_PERSONAL();
        DataTable dt = new DataTable();
        dt = obj.ELIMINAR_PARCIPANTES(btnEliminar.CommandArgument, Convert.ToInt32(lblCodigoMinuta.Text));
        ListarPersonal();

     
    }



    protected void btnGuardarDatosGeneral_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (lblCodigoMinuta.Text == string.Empty  )
        {
            cleanMessage = "Falta Registra Minuta";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
            else
        {
            ValidarTemas();
        }
       
    }
    private void ValidarTemas()
    {

        string cleanMessage = string.Empty;
        if (ddlDestino.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar el tipo de Destino";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlArea.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar el tipo de Área";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if(txtTema.Text == string.Empty)
        {
            cleanMessage = "Ingresar Tema";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlPersona.SelectedIndex == 0)
        {
            cleanMessage = "Selecionar personal a cargo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtRequerimiento.Text == string.Empty)
        {
            cleanMessage = "Ingresar Fecha Requerimiento";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtCompromisoCierre.Text == string.Empty)
        {
            cleanMessage = "Ingresar Fecha de cierre";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {
            DateTime inicio = Convert.ToDateTime(txtRequerimiento.Text);
            DateTime fin = Convert.ToDateTime(txtCompromisoCierre.Text);

            if (inicio <= fin)
            {
                BE_OPE_TEMAS objTemas = new BE_OPE_TEMAS();
                objTemas = CapturarDatosTema();
                int rpta = new BL_OPE_TEMAS().Mant_InsertarTemasBL(objTemas);

                if (rpta > 0)
                {
                    lblCodigoTemas.Text = rpta.ToString();
                    MostrarTema();
                    ValidarAcuerdos();
                    //cbxcambiarTema.Checked = true;
                    //cbxcambiarMinuta.Checked = false;
                    Bandeja_Compromisos();
                    Bandeja_Minutas();
                    BandejaResponsable();
                }
                else
                {
                    cleanMessage = "Error al Registrar";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "Fecha de Cierre no puede ser menor a la fecha de requerimiento";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    private void MostrarTema()
    {
        string cleanMessage = string.Empty;
        BE_OPE_TEMAS objTema = new BE_OPE_TEMAS();
        DataTable objRespueta = new DataTable();
        objRespueta = new BL_OPE_TEMAS().BL_SELECIONAR_TEMAS(Convert.ToInt32(lblCodigoTemas.Text));

        if (objRespueta.Rows.Count > 0)
        {
            lblCodigoTemas.Text = objRespueta.Rows[0]["id_temas"].ToString();
            txtTema.Text = objRespueta.Rows[0]["dsc_nombre_tema"].ToString();
            txtCompromisoCierre.Text = objRespueta.Rows[0]["fch_fecha_original"].ToString();
            txtRequerimiento.Text = objRespueta.Rows[0]["fch_fecha_requerimiento"].ToString();
            //txtResponsable.Text = objRespueta.Rows[0]["dsc_responsable"].ToString();
            ddlArea.SelectedValue = objRespueta.Rows[0]["id_areas"].ToString();
            lblCodigoMinuta.Text = objRespueta.Rows[0]["id_minuta"].ToString();
            ddlDestino.SelectedValue = objRespueta.Rows[0]["id_parametro"].ToString();
            txtCompromisoCierre0.Text = objRespueta.Rows[0]["fch_compromiso"].ToString();
        }
        else
        {
            cleanMessage = "Se produjo un error al cargar la información";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
        int year = miFecha.Year;
        int month = miFecha.Month;

        dtMes = GetDataReunionesMes(year, month);
    }
    private BE_OPE_TEMAS CapturarDatosTema()
    {
        BE_OPE_TEMAS objTemas = new BE_OPE_TEMAS();

        

        objTemas.Id_temas = Convert.ToInt32(string.IsNullOrEmpty(lblCodigoTemas.Text) ? "0" : lblCodigoTemas.Text);
        objTemas.Dsc_nombre_tema = txtTema.Text;
        objTemas.Fch_fecha_original = txtCompromisoCierre.Text;
        objTemas.Fch_fecha_requerimiento = txtRequerimiento.Text;
        objTemas.Dsc_responsable = ddlPersona.SelectedValue;//  txtResponsable.Text;
        objTemas.Id_areas = Convert.ToInt32(ddlArea.SelectedValue);
        objTemas.Id_minuta = Convert.ToInt32(lblCodigoMinuta.Text);
        objTemas.Id_parametro = Convert.ToInt32(ddlDestino.SelectedValue);
        objTemas.Fch_compromiso = string.IsNullOrEmpty(txtCompromisoCierre0.Text) ? "" : txtCompromisoCierre0.Text;
        objTemas.Usuario = BL_Session.UsuarioNombre;

        return objTemas;
    }
    public void Bandeja_Compromisos() {
        //cbxcambiarTema.Visible = false;
        //tablaMinutas02Div.Visible = false;
        BL_OPE_DETALLE_ACUERDOS objCom = new BL_OPE_DETALLE_ACUERDOS();
        DataTable dtCom = new DataTable();
        string txtfiltroDatos;       
        int estadoFiltro = Convert.ToInt32(string.IsNullOrEmpty(ddlEstadoFiltro.SelectedValue) ? "0" : ddlEstadoFiltro.SelectedValue);
        if (txtFiltro.Text == string.Empty)
        {
            txtfiltroDatos = string.Empty;
        }
        else { txtfiltroDatos = txtFiltro.Text; }
        string fRequerimiento, fCierre,fCompromiso, treunion, fActualizado, codDestino,responsable;
        fRequerimiento = string.IsNullOrEmpty(txtBanRequerimiento.Text) ? "" : txtBanRequerimiento.Text;
        fCierre = string.IsNullOrEmpty(txtBanCierre.Text) ? "" : txtBanCierre.Text;
        fCompromiso = string.IsNullOrEmpty(txtBanCompromiso.Text) ? "" : txtBanCompromiso.Text;
        treunion = string.IsNullOrEmpty(ddlBanTipoReunion.SelectedValue) ? "" : ddlBanTipoReunion.SelectedValue;
        fActualizado = string.IsNullOrEmpty(txtBanModificado.Text) ? "" : txtBanModificado.Text;
        codDestino = string.IsNullOrEmpty(ddlBanDestino.SelectedValue) ? "" : ddlBanDestino.SelectedValue;
        responsable = string.IsNullOrEmpty(ddlBanResponsable.SelectedValue) ? "" : ddlBanResponsable.SelectedValue;
        dtCom = objCom.SELECIONAR_MINUTA_BANDEJA_COMPROMISO(BL_Session.CENTRO_COSTO, txtfiltroDatos,estadoFiltro, fRequerimiento, fCierre, fCompromiso, treunion, fActualizado, codDestino, responsable);
        if (dtCom.Rows.Count > 0)
        {
            ListView4.Visible = true;
            ListView4.DataSource = dtCom;
            ListView4.DataBind();
        }
        else
        {
            ListView4.Visible = false;
            ListView4.DataSource = dtCom;
            ListView4.DataBind();
        }
    }
    public void ListarAcuerdos()
    {
        lblCodigoTemas.Text = string.Empty;
        //lblCodigoAcuerdo.Text = string.Empty;
        DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
        int year = miFecha.Year;
        int month = miFecha.Month;
        int filtroFecha = 0;

        dtMes = GetDataReunionesMes(year, month);
        if (cbxFiltroVencidos.Checked)
        {
            filtroFecha = 1;
        }
        else { filtroFecha = 0; }

        BL_OPE_DETALLE_ACUERDOS objDetalle = new BL_OPE_DETALLE_ACUERDOS();
        DataTable dt = new DataTable();
        dt = objDetalle.SELECIONAR_ACUERDOS_MINUTA(Convert.ToInt32(lblCodigoMinuta.Text), Convert.ToInt32(filtroFecha),Convert.ToInt32(numOrden));
        listVal = lblCodigoAcuerdo.Text;
        if (dt.Rows.Count > 0)
        {
            ListView3.Visible = true;
            ListView3.DataSource = dt;
            ListView3.DataBind();
            lblCodigoAcuerdo.Text = string.Empty;
        }
        else
        {
            ListView3.Visible = false;
            ListView3.DataSource = dt;
            ListView3.DataBind();
        }
    }

    public void ListarAcuerdosCerrados()
    {
        lblCodigoTemas.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
        DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
        int year = miFecha.Year;
        int month = miFecha.Month;
        int filtroCerrado = 0;

        dtMes = GetDataReunionesMes(year, month);
        if (cbxFiltroCerrado.Checked)
        {
            filtroCerrado = 1;
        }
        else { filtroCerrado = 0; }

        BL_OPE_DETALLE_ACUERDOS objDetalle = new BL_OPE_DETALLE_ACUERDOS();
        DataTable dt = new DataTable();
        dt = objDetalle.SELECIONAR_ACUERDOS_CERRADOS_BL(Convert.ToInt32(lblCodigoMinuta.Text), Convert.ToInt32(filtroCerrado),Convert.ToInt32(numOrden));
        if (dt.Rows.Count > 0)
        {
            ListView3.Visible = true;
            ListView3.DataSource = dt;
            ListView3.DataBind();
        }
        else
        {
            ListView3.Visible = false;
            ListView3.DataSource = dt;
            ListView3.DataBind();
        }
    }
    private void ValidarAcuerdos()
    {
        string cleanMessage = string.Empty;

        if (txtAcuerdo.Text == string.Empty)
        {
            cleanMessage = "Ingresar Comentario";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlEstados.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar el Tipo de Estado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_OPE_DETALLE_ACUERDOS objAcuerdos = new BE_OPE_DETALLE_ACUERDOS();

            objAcuerdos.Id_detalle_acuerdo = Convert.ToInt32(string.IsNullOrEmpty(lblCodigoAcuerdo.Text) ? "0" : lblCodigoAcuerdo.Text);
            objAcuerdos.Id_temas = Convert.ToInt32(lblCodigoTemas.Text);
            objAcuerdos.Dsc_descripcion = txtAcuerdo.Text;
            objAcuerdos.Est_estado = string.IsNullOrEmpty(ddlEstados.SelectedValue) ?"1":ddlEstados.SelectedValue;
            objAcuerdos.Dsc_observacion = string.Empty;//txbObservacion.Text;
            objAcuerdos.Usuario = BL_Session.UsuarioNombre;
            int rpta2 = new BL_OPE_DETALLE_ACUERDOS().Mant_Insertar_AcuerdosBL(objAcuerdos);
            listVal = lblCodigoAcuerdo.Text;
            if (rpta2 > 0)
            {
                ListarAcuerdos();
                LimpiarAcuerdos();
            }
            else
            {
                
                cleanMessage = "Existe Inconsistencia en el registro";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    private void LimpiarAcuerdos()
    {
        lblCodigoAcuerdo.Text = string.Empty;
        txtAcuerdo.Text = string.Empty;
        txtRequerimiento.Text = string.Empty;
        txtCompromisoCierre.Text  = string.Empty;
        txtTema.Text = string.Empty;
        lblCodigoTemas.Text = string.Empty;
        txtCompromisoCierre0.Text = string.Empty;
        ddlEstados.SelectedIndex = 0;
        //txbObservacion.Text = string.Empty;

    }
    private void cargarDestino() {
        ddlDestino.DataSource = getData_Destino();
        ddlDestino.DataTextField = "DES_ASUNTO";
        ddlDestino.DataValueField = "ID_PARAMETRO";
        ddlDestino.DataBind();
        ddlDestino.Items.Insert(0, new ListItem("-DES-", ""));

        
        ddlBanDestino.DataSource = getData_Destino();
        ddlBanDestino.DataTextField = "DES_ASUNTO";
        ddlBanDestino.DataValueField = "ID_PARAMETRO";
        ddlBanDestino.DataBind();
        ddlBanDestino.Items.Insert(0, new ListItem("TODO", ""));
    }
    private DataTable getData_Destino() {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_OBTENER_DESTINO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);

        return dt;
    }
    private void cargarReunion() {
        ddlRegistro.DataSource = get_DataReunion();
        ddlRegistro.DataTextField = "des_asunto";
        ddlRegistro.DataValueField = "ID_PARAMETRO";
        ddlRegistro.DataBind();
        ddlRegistro.Items.Insert(0, new ListItem("--- Seleccionar ---",""));

        ddlMinReunion.DataSource = get_DataReunion();
        ddlMinReunion.DataTextField = "des_asunto";
        ddlMinReunion.DataValueField = "ID_PARAMETRO";
        ddlMinReunion.DataBind();
        ddlMinReunion.Items.Insert(0, new ListItem("-TODO-", ""));

        ddlBanTipoReunion.DataSource = get_DataReunion();
        ddlBanTipoReunion.DataTextField = "des_asunto";
        ddlBanTipoReunion.DataValueField = "ID_PARAMETRO";
        ddlBanTipoReunion.DataBind();
        ddlBanTipoReunion.Items.Insert(0, new ListItem("TODO", ""));
    }
    public DataTable get_DataReunion()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_OBTENER_REUNION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);
        return dt;
    }
    private void cargarArea()
    {
        ddlArea.DataSource = getData_Area();
        ddlArea.DataTextField = "des_asunto";
        ddlArea.DataValueField = "ID_PARAMETRO";
        ddlArea.DataBind();
        ddlArea.Items.Insert(0, new ListItem("--- ÁREAS ---", ""));
    }
    private DataTable getData_Area()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_OBTENER_AREA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private void cargarAreaReporte()
    {
        ddlAreaReporte.DataSource = getData_AreaReporte();
        ddlAreaReporte.DataTextField = "des_asunto";
        ddlAreaReporte.DataValueField = "ID_PARAMETRO";
        ddlAreaReporte.DataBind();
        ddlAreaReporte.Items.Insert(0, new ListItem("TODOS", "1"));
        //ddlAreaReporte.Items.Insert(0, new ListItem("---Seleccione Áreas ---", ""));
        
    }
    private DataTable getData_AreaReporte()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_OBTENER_AREA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private void cargarAno()
    {
        ddlAno.DataSource = getData_Ano();
        ddlAno.DataTextField = "fecha_ano";
        ddlAno.DataValueField = "fecha_ano";
        ddlAno.DataBind();
        if(getData_Ano().Rows.Count != 0)
        { 
        ddlAno.Items.Insert(0, new ListItem("TODOS", "1"));
        }
        //ddlAno.Items.Insert(0, new ListItem("--Seleccionar Año--",""));
    }
    //private void txtComentarioMultiple_KeyPress(object sender, event e)
    //{
    //    if ((int)e.KeyChar == (int)Keys.Enter)
    //    {
    //        //aqui codigo
    //    }

    //}


    private DataTable getData_Ano()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_SELECCIONAR_ANO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private void cargarEstado()
    {
        ddlEstados.DataSource = getData_Estado();
        ddlEstados.DataTextField = "des_asunto";
        ddlEstados.DataValueField = "ID_PARAMETRO";
        ddlEstados.DataBind();
        ddlEstados.Items.Insert(0, new ListItem("--- ESTADO ---",""));

        ddlEstadoFiltro.DataSource = getData_Estado();
        ddlEstadoFiltro.DataTextField = "des_asunto";
        ddlEstadoFiltro.DataValueField = "ID_PARAMETRO";
        ddlEstadoFiltro.DataBind();
        ddlEstadoFiltro.Items.Insert(0, new ListItem("--- TODO ---", ""));
    }
    private DataTable getData_Estado()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_OBTENER_ESTADO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }

    private void cargarEstadoReporte()
    {
        ddlEstadoReporte.DataSource = getData_EstadoReporte();
        ddlEstadoReporte.DataTextField = "des_asunto";
        ddlEstadoReporte.DataValueField = "ID_PARAMETRO";
        ddlEstadoReporte.DataBind();
        ddlEstadoReporte.Items.Insert(0, new ListItem("TODOS", "1"));
        //ddlEstadoReporte.Items.Insert(0, new ListItem("--Seleccione Estado--", ""));
    }
    private DataTable getData_EstadoReporte()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_OBTENER_ESTADO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }

    //protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    //{
    // aqui calendario
    //    int anio = Convert.ToInt32 ( Calendar1.VisibleDate.Year.ToString());
    //    int mes = Convert.ToInt32(Calendar1.VisibleDate.Month.ToString());

    //    dtMes = GetDataReunionesMes(anio, mes);
    //}

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {

        CleanControl(this.Controls);
        DateTime miFecha = DateTime.Now;

        int year = miFecha.Year;
        int month = miFecha.Month;
        int day = miFecha.Day;
        int hour = miFecha.Hour;
        int hourFin = hour + 1;
        int minute = miFecha.Minute;
        int second = miFecha.Second;

        lblCodigoMinuta.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
        lblCodigoTemas.Text = string.Empty;
        lstRol.Visible = false;
        ListView3.Visible = false;

        txtHoraInicio.Text = hour.ToString() + ':' + minute.ToString();
        txtHoraFin.Text = hourFin.ToString() + ':' + minute.ToString();
        txtFechaMinuta.Text = miFecha.ToString("dd/MM/yyyy");
        txtCodigoProyecto.Text = BL_Session.CENTRO_COSTO.ToString();
        txtObra.Text = BL_Session.PROYECTO;
        Personal();
        cargarArea();
        cargarEstado();
        cargarAno();
        cargarReunion();

        //Calendar1.TodaysDate = miFecha;
        //Calendar1.SelectedDate = Calendar1.TodaysDate;
        dtMes = GetDataReunionesMes(year, month);

    }
    public void CleanControl(ControlCollection controles)
    {
        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Text = string.Empty;
            else if (control is DropDownList)
                ((DropDownList)control).ClearSelection();
            else if (control is RadioButtonList)
                ((RadioButtonList)control).ClearSelection();
            else if (control is CheckBoxList)
                ((CheckBoxList)control).ClearSelection();
            else if (control is RadioButton)
                ((RadioButton)control).Checked = false;
            else if (control is CheckBox)
                ((CheckBox)control).Checked = false;
            else if (control.HasControls())
                //Esta linea detécta un Control que contenga otros Controles
                //Así ningún control se quedará sin ser limpiado.
                CleanControl(control.Controls);
        }
    }

    protected void btnNuevoDetalles_Click(object sender, ImageClickEventArgs e)
    {
        lblCodigoTemas.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
        ddlArea.SelectedIndex = 0;
        ddlDestino.SelectedIndex = 0;
        txtTema.Text = string.Empty;
        //txtResponsable.Text = string.Empty;
        txtCompromisoCierre.Text = string.Empty;
        txtCompromisoCierre0.Text = string.Empty;
        txtRequerimiento.Text = string.Empty;
        ddlEstados.SelectedIndex = 0;
        txtAcuerdo.Text = string.Empty;
        DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
        int year = miFecha.Year;
        int month = miFecha.Month;

        dtMes = GetDataReunionesMes(year, month);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAgregarAll_Click(object sender, ImageClickEventArgs e)
    {
        //DateTime miFecha = Convert.ToDateTime(txtFechaMinuta.Text);
        //int year = miFecha.Year;
        //int month = miFecha.Month;

        //dtMes = GetDataReunionesMes(year, month);
        string cleanMessage = string.Empty;
        if (lblCodigoMinuta.Text == string.Empty)
        {
            cleanMessage = "Falta Registra Minuta";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            ValidarTemas();
        }
    }
    protected void EliminarAcuerdos(object sender, ImageClickEventArgs e)
    {
        lblCodigoTemas.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
        ImageButton btnDelete = ((ImageButton)sender);

        BL_OPE_DETALLE_ACUERDOS obj = new BL_OPE_DETALLE_ACUERDOS();
        DataTable dt = new DataTable();
        dt = obj.ELIMINAR_MINUTA_ACUERDOS_ID(Convert.ToInt32 ( btnDelete.CommandArgument));
        ListarAcuerdos();
        lblCodigoTemas.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
    }
    protected void SelectCompromisoMinuta(object sender, ImageClickEventArgs e)
    {
        ImageButton btnMostrar = ((ImageButton)sender);
        lblCodigoMinuta.Text = btnMostrar.CommandArgument;
        mostrarMinuta();
        TabContainer1.ActiveTabIndex = 1;

    }
    protected void SelectCompromisoTemas(object sender, ImageClickEventArgs e)
    {   ImageButton btnMostrar = ((ImageButton)sender);
        ListViewItem row = btnMostrar.NamingContainer as ListViewItem;
        string pk1 = ListView4.DataKeys[row.DisplayIndex].Values[0].ToString();
        string pk2 = ListView4.DataKeys[row.DisplayIndex].Values["codAcuerdo"].ToString ();
        string cleanMessage = string.Empty;
        listVal = pk2;
        //cleanMessage = pk1 + "-" + pk2;
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        //ImageButton btnMostrar = ((ImageButton)sender);
        lblCodigoMinuta.Text = pk1;
        mostrarMinuta();

        BL_OPE_DETALLE_ACUERDOS obj = new BL_OPE_DETALLE_ACUERDOS();
        DataTable objResultado = new DataTable();
        objResultado = obj.SELECIONAR_ACUERDOS_MINUTA_ID(Convert.ToInt32(pk2));
        if (objResultado.Rows.Count > 0)
        {            
            lblCodigoTemas.Text = objResultado.Rows[0]["id_temas"].ToString();
            txtTema.Text = objResultado.Rows[0]["dsc_nombre_tema"].ToString();
            txtCompromisoCierre.Text = objResultado.Rows[0]["fch_fecha_original"].ToString();
            txtRequerimiento.Text = objResultado.Rows[0]["fch_fecha_requerimiento"].ToString();
            ddlPersona.SelectedValue = objResultado.Rows[0]["DNI"].ToString();
            ddlArea.SelectedValue = objResultado.Rows[0]["id_areas"].ToString();
            lblCodigoMinuta.Text = objResultado.Rows[0]["id_minuta"].ToString();
            ddlEstados.SelectedValue = objResultado.Rows[0]["est_estado"].ToString();
            lblCodigoAcuerdo.Text = objResultado.Rows[0]["id_detalle_acuerdo"].ToString();
            txtAcuerdo.Text = objResultado.Rows[0]["dsc_descripcion"].ToString();
            txtCompromisoCierre0.Text = objResultado.Rows[0]["fch_compromiso"].ToString();
            ddlDestino.SelectedValue = objResultado.Rows[0]["id_parametro"].ToString();            
        }
        TabContainer1.ActiveTabIndex = 3;
    }
    protected string GetBackColor(object evalObj)
    {
        string retVal = "none";
        int cant = ListView3.Items.Count;
        for (int i = 0; i < cant; i++) // aqui se recorre toda la lista
        {
            if (evalObj != null)
            {
                if (evalObj.ToString() == listVal)
                {
                    retVal = "LvColor";
                    i = cant + 1;
                }
            }
            //ListViewItem Fila = ListView4.Items[i];
            //bool rb = ((CheckBox)Fila.FindControl("cbxEnviarCorreo")).Checked = false; // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check

        }

        return retVal;
    }
    //protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
    //        if (DataBinder.Eval(dataitem.DataItem, "id_temas").ToString() == listVal)
    //        {
    //            System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)dataitem.FindControl("trItem");
    //            tr.BgColor = System.Drawing.Color.Red.ToString();
    //            //tr.BgColor = System.Drawing.Color.AliceBlue.ToString();
    //        }
    //    }
    //}
    protected void EnviarCorreoMinuta(object sender, ImageClickEventArgs e)
    {
        try
        {
            string cleanMessage = string.Empty;
            //int intContador = 0;
            int cant = ListView4.Items.Count;
            int codigoAcuerdo;
            //int[] codigos = new int[cant];
            int contador = 0;
            DataTable objResul = new DataTable();
            BL_OPE_MINUTA objMinuta = new BL_OPE_MINUTA();

            for (int i = 0; i < cant; i++) // aqui se recorre toda la lista
            {

                ListViewItem Fila = ListView4.Items[i];
                bool rb = ((CheckBox)Fila.FindControl("cbxEnviarCorreo")).Checked; // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check
                if (rb)
                {
                    codigoAcuerdo = Convert.ToInt32(ListView4.DataKeys[i].Values[1].ToString()); // extraer key
                    //codigos[contador] = Cod;
                    contador++;
                    objResul = objMinuta.SP_EnviarCorreo_Minuta(codigoAcuerdo,BL_Session.NombreCargo);

                }
            }
            if (contador > 0)
            {
                cleanMessage = "Se envio los correo de forma exitosa";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        catch (Exception ex)
        {

        }





        //for (int i = 0; i < ListView4.Items.Count; i++)
        //{
        //    ListView4.Items[i].Checked = true;
        //}

        //
        //ImageButton btnEnviarMinuta = ((ImageButton)sender);
        //int codigoAcuerdo = Convert.ToInt32(btnEnviarMinuta.CommandArgument);
        //DataTable objResul = new DataTable();
        //BL_OPE_MINUTA objMinuta = new BL_OPE_MINUTA();
        //objResul=objMinuta.SP_EnviarCorreo_Minuta(codigoAcuerdo);

        //if (objResul.Rows.Count > 0)
        //{

        //    cleanMessage = "Se envió el correo de manera exitosa. :)";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
        //else {
        //    cleanMessage = "Error al enviar el correo";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}

    }
    protected void SeleccionarAcuerdoID(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEditar = ((ImageButton)sender);

        BL_OPE_DETALLE_ACUERDOS obj = new BL_OPE_DETALLE_ACUERDOS();
        DataTable objResultado = new DataTable();
        objResultado = obj.SELECIONAR_ACUERDOS_MINUTA_ID(Convert.ToInt32(btnEditar.CommandArgument));
        if (objResultado.Rows.Count > 0)
        {
            lblCodigoTemas.Text= objResultado.Rows[0]["id_temas"].ToString();
            txtTema.Text = objResultado.Rows[0]["dsc_nombre_tema"].ToString();
            txtCompromisoCierre.Text = objResultado.Rows[0]["fch_fecha_original"].ToString();
            txtRequerimiento.Text = objResultado.Rows[0]["fch_fecha_requerimiento"].ToString();
            ddlPersona.SelectedValue = objResultado.Rows[0]["DNI"].ToString();
            ddlArea.SelectedValue = objResultado.Rows[0]["id_areas"].ToString();
            lblCodigoMinuta.Text = objResultado.Rows[0]["id_minuta"].ToString();
            ddlEstados.SelectedValue = objResultado.Rows[0]["est_estado"].ToString();
            lblCodigoAcuerdo.Text = objResultado.Rows[0]["id_detalle_acuerdo"].ToString();
            txtAcuerdo.Text = objResultado.Rows[0]["dsc_descripcion"].ToString();
            txtCompromisoCierre0.Text = objResultado.Rows[0]["fch_compromiso"].ToString();
            ddlDestino.SelectedValue = objResultado.Rows[0]["id_parametro"].ToString();
            
        }     
    }

    protected void ddlRegistro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlRegistro.SelectedIndex == 0)
        {
            txtRegistro.Text = string.Empty;
        }
        else
        {
            txtRegistro.Text = ddlRegistro.SelectedItem.Text + "-" + BL_Session.CENTRO_COSTO.ToString();
        }
    }



    protected void btnMostraReporte_Click(object sender, EventArgs e)
    {
        ValidarReporte();
    }
    protected void rpt_Cuadro()
    {
        string cleanMessage = string.Empty;
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/Rpt_Minuta.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSetMinuta", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

            int total = dsCustomers.Rows.Count;
            txtTotal.Text = total.ToString();

        }
        else
        {
            txtTotal.Text = string.Empty;
            ReportViewer1.Visible = false;
            ReportViewer1.LocalReport.DataSources.Clear();
            cleanMessage = "No se encontro ningun registro";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_LISTAR_MINUTA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AREA", SqlDbType.Int).Value = ddlAreaReporte.SelectedValue;
        cmd.Parameters.Add("@MES", SqlDbType.Int).Value = ddlMeses.SelectedValue;
        cmd.Parameters.Add("@ANO", SqlDbType.Int).Value = ddlAno.SelectedValue;
        cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = ddlEstadoReporte.SelectedValue;
        //cmd.Parameters.Add("@INT_EJERCICIO", SqlDbType.Int).Value = Convert.ToInt32(ddlAnio.SelectedValue);
        //cmd.Parameters.Add("@INT_PERIODO", SqlDbType.Int).Value = Convert.ToInt32(ddlMes.SelectedValue);
        //cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = ddlEstado.SelectedValue;
        //cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 30).Value = 1;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private void ValidarReporte()
    {
        rpt_Cuadro();
    }
    protected void RegistrarComentario(object sender, ImageClickEventArgs e)
    {
        lblCodigoTemas.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
        ImageButton btnComentario = ((ImageButton)sender);
        ListViewItem row = btnComentario.NamingContainer as ListViewItem;
        string codTema = ListView3.DataKeys[row.DisplayIndex].Values[0].ToString();
        string codAcuerdo = ListView3.DataKeys[row.DisplayIndex].Values[1].ToString();
        lblCodigoTemas.Text = codTema;
        lblCodigoAcuerdo.Text = codAcuerdo;
        ModalRegistro.Show();
        
    }

    protected void RegistrarComentario(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlUsuarioComt.SelectedIndex == 0 && txtComentarioUser.Text == string.Empty)
        {
            cleanMessage = "Ingrese los datos solicitados";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            ModalRegistro.Show();
        }
        else
        {
            BE_OPE_DETALLE_ACUERDOS objAcu = new BE_OPE_DETALLE_ACUERDOS();

            objAcu.Id_temas = Convert.ToInt32(lblCodigoTemas.Text);
            objAcu.Dsc_descripcion = txtComentarioUser.Text;
            objAcu.Est_estado = string.Empty;
            objAcu.Dsc_observacion = string.Empty;//txbObservacion.Text; 
            objAcu.InicialUsuario = Convert.ToInt32(ddlUsuarioComt.SelectedValue);

            int rpta = new BL_OPE_DETALLE_ACUERDOS().MANT_USP_AGREGAR_COMENTARIO_BL(objAcu);
            ListarAcuerdos();
            //ddlUsuarioComt.SelectedValue = string.Empty;
            txtComentarioUser.Text = string.Empty;
            lblCodigoTemas.Text = string.Empty;
            //lblCodigoAcuerdo.Text = string.Empty;
        }

    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblCodigoTemas.Text = string.Empty;
        lblCodigoAcuerdo.Text = string.Empty;
    }

    protected void cbxFiltroCerrado_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxFiltroCerrado.Checked)
        {
            ListarAcuerdosCerrados();
        }
        else
        {
            ListarAcuerdos();
        }
        
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        Bandeja_Compromisos();
    }

    protected void cbxFiltroVencidos_CheckedChanged(object sender, EventArgs e)
    {
        ListarAcuerdos();
    }

    protected void Ordenar_Click1(object sender, EventArgs e)
    {
        numOrden = 1;
        ListarAcuerdos();
    }
    protected void Ordenar_Click2(object sender, EventArgs e)
    {
        numOrden = 2;
        ListarAcuerdos();
    }
    protected void Ordenar_Click3(object sender, EventArgs e)
    {
        numOrden = 3;
        ListarAcuerdos();
    }
    protected void Ordenar_Click4(object sender, EventArgs e)
    {
        numOrden = 4;
        ListarAcuerdos();
    }
    protected void Ordenar_Click5(object sender, EventArgs e)
    {
        numOrden = 5;
        ListarAcuerdos();
    }
    protected void Ordenar_Click6(object sender, EventArgs e)
    {
        numOrden = 6;
        ListarAcuerdos();
    }
    protected void Ordenar_Click7(object sender, EventArgs e)
    {
        numOrden = 7;
        ListarAcuerdos();
    }

    protected void cbxcambiar_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxcambiarMinuta.Checked == true)
        {
            tablaTemas1.Visible = false;
            tablaMinutas02Div.Visible = true;
            cbxcambiarTema.Checked = false;
            //cbxcambiarTema.Visible = true;
            //cbxcambiarMinuta.Visible = false;
            lblEnviar.Visible = false;
            cbxTodos.Visible = false;
            cbxNinguno.Visible = false;
            Bandeja_Minutas();
        }
        
    }

    protected void cbxcambiarTema_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxcambiarTema.Checked == true)
        {
            tablaTemas1.Visible = true;
            tablaMinutas02Div.Visible = false;
            cbxcambiarMinuta.Checked = false;
            //Bandeja_Compromisos();
            //cbxcambiarMinuta.Visible = true;
            //cbxcambiarTema.Visible = false;
            lblEnviar.Visible = true;
            cbxTodos.Visible = true;
            cbxNinguno.Visible = true;
        }
    }

    public void Bandeja_Minutas()
    {        
        BL_OPE_MINUTA objMinu = new BL_OPE_MINUTA();
        DataTable dtMinu = new DataTable();
        string minuta, reunion,encargado, lugar, fecha, periodo, fechaModificado;
        minuta = string.IsNullOrEmpty(txtMinAsunto.Text) ? "" : txtMinAsunto.Text;
        reunion = string.IsNullOrEmpty(ddlMinReunion.SelectedValue) ? "0" : ddlMinReunion.SelectedValue;
        encargado = string.IsNullOrEmpty(txtMinEncargado.Text) ? "" : txtMinEncargado.Text;
        lugar = string.IsNullOrEmpty(txtMinLugar.Text) ? "" : txtMinLugar.Text;
        fecha = string.IsNullOrEmpty(txtMinFecha.Text) ? "" : txtMinFecha.Text;
        periodo = string.IsNullOrEmpty(ddlMinPeriodo.SelectedValue) ? "0" : ddlMinPeriodo.SelectedValue;
        fechaModificado = string.IsNullOrEmpty(txtMinfechaModificado.Text) ? "" : txtMinfechaModificado.Text;

        dtMinu = objMinu.MOSTRAR_MINUTAS_BL(BL_Session.CENTRO_COSTO, minuta, reunion, encargado, lugar, fecha, periodo, fechaModificado);
        if (dtMinu.Rows.Count > 0)
        {
            ListViewMinutaBandeja.Visible = true;
            ListViewMinutaBandeja.DataSource = dtMinu;
            ListViewMinutaBandeja.DataBind();
        }
        else
        {
            ListViewMinutaBandeja.Visible = false;
            ListViewMinutaBandeja.DataSource = dtMinu;
            ListViewMinutaBandeja.DataBind();
        }
    }
    public void Bandeja_Historial()
    {
        BL_OPE_MINUTA objMinu = new BL_OPE_MINUTA();
        DataTable dtMinu = new DataTable();
        //string minuta, reunion, encargado, lugar, fecha, periodo, fechaModificado;
        //minuta = string.IsNullOrEmpty(txtMinAsunto.Text) ? "" : txtMinAsunto.Text;
        //reunion = string.IsNullOrEmpty(ddlMinReunion.SelectedValue) ? "0" : ddlMinReunion.SelectedValue;
        //encargado = string.IsNullOrEmpty(txtMinEncargado.Text) ? "" : txtMinEncargado.Text;
        //lugar = string.IsNullOrEmpty(txtMinLugar.Text) ? "" : txtMinLugar.Text;
        //fecha = string.IsNullOrEmpty(txtMinFecha.Text) ? "" : txtMinFecha.Text;
        //periodo = string.IsNullOrEmpty(ddlMinPeriodo.SelectedValue) ? "0" : ddlMinPeriodo.SelectedValue;
        //fechaModificado = string.IsNullOrEmpty(txtMinfechaModificado.Text) ? "" : txtMinfechaModificado.Text;

        //dtMinu = objMinu.MOSTRAR_MINUTAS_BL(BL_Session.CENTRO_COSTO, minuta, reunion, encargado, lugar, fecha, periodo, fechaModificado);
        dtMinu = objMinu.Mant_Mostrar_Historial(BL_Session.CENTRO_COSTO);
        if (dtMinu.Rows.Count > 0)
        {
            ltvHistorial.Visible = true;
            ltvHistorial.DataSource = dtMinu;
            ltvHistorial.DataBind();
        }
        else
        {
            ltvHistorial.Visible = false;
            ltvHistorial.DataSource = dtMinu;
            ltvHistorial.DataBind();
        }
    }

    protected void btnBuscarminutas1_Click(object sender, ImageClickEventArgs e)
    {
        Bandeja_Minutas();        
    }

    protected void cbxLimpiarBandeja_Click(object sender, ImageClickEventArgs e)
    {
        if(tablaTemas1.Visible == true)
        {
            txtFiltro.Text = string.Empty;
            ddlBanDestino.SelectedIndex = 0;
            ddlBanResponsable.SelectedIndex = 0;
            ddlBanTipoReunion.SelectedIndex = 0;
            txtBanRequerimiento.Text = string.Empty;
            txtBanCompromiso.Text = string.Empty;
            txtBanModificado.Text = string.Empty;
            ddlEstadoFiltro.SelectedIndex = 0;
            txtBanCierre.Text = string.Empty;         
            Bandeja_Compromisos();

            for (int i = 0; i < ListView4.Items.Count; i++) // aqui se recorre toda la lista
            {

                ListViewItem Fila = ListView4.Items[i];
                bool rb = ((CheckBox)Fila.FindControl("cbxEnviarCorreo")).Checked = false; // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check

            }
        }
        else if(tablaMinutas02Div.Visible == true)
        {
            txtMinAsunto.Text = string.Empty;
            txtMinEncargado.Text = string.Empty;
            txtMinFecha.Text = string.Empty;
            txtMinLugar.Text = string.Empty;
            txtMinfechaModificado.Text = string.Empty;
            ddlMinPeriodo.SelectedIndex = 0;
            ddlMinReunion.SelectedIndex = 0;
            Bandeja_Minutas();
            tablaMinutas02Div.Visible = true;
        }
    }



    protected void cbxTodos_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < ListView4.Items.Count; i++) // aqui se recorre toda la lista
        {

            ListViewItem Fila = ListView4.Items[i];
            bool rb = ((CheckBox)Fila.FindControl("cbxEnviarCorreo")).Checked = true; // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check
        }
        cbxNinguno.Checked = false;
    }

    protected void cbxNinguno_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < ListView4.Items.Count; i++) // aqui se recorre toda la lista
        {

            ListViewItem Fila = ListView4.Items[i];
            bool rb = ((CheckBox)Fila.FindControl("cbxEnviarCorreo")).Checked = false; // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check
        }
        cbxTodos.Checked = false;
    }

    protected void btnGuardarHistorial_Click(object sender, ImageClickEventArgs e)
    {
        //Creando Objeto BL_minuta
        BL_OPE_MINUTA objBl = new BL_OPE_MINUTA();
        string mensaje = "";
        
    }
}