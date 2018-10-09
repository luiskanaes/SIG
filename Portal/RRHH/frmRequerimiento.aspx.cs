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
public partial class RRHH_frmMOI : System.Web.UI.Page
{
    public string ControlUsuario;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        txtRequerimiento.Attributes.Add("onkeypress", "javascript:return SoloNum(event); ");
        txtItem.Attributes.Add("onkeypress", "javascript:return SoloNum(event); "); 
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {

            chkCentros.Checked = true;
            chkEmpresas.Checked = true;
            chkEstados.Checked = true;
            chkReclutadores.Checked = true;
            chkProyectos.Checked = true;
            chkCargos.Checked = true;
            
            ControlBotones();
            Empresas();
            ParametrosUbicacion();
            Proyectos();
            ProyectosB();
            CentroCostos();
            areas();
            cargo();
            cargos();
            //Proceso();
            Fuente();
            TipoProceso();
            Posicion();
            EstadosB();
            Salida();
            Analistas();
           
        }
    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {

        }
        else
        {

        }
    }
    protected void btnSeguimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/SeguimientoReporteMOI.aspx");
    }
    protected void btnPersonal_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmPersonal.aspx");
    }
    protected void btnControl_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmMOI.aspx");
    }
    protected void btnReportes_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/SeguimientoReporteMOI.aspx");
    }
    protected void btnRequerimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmRequerimiento.aspx");
    }
    [WebMethod]
    [ScriptMethod]
    public static List<string> GetPersonal(string prefixText)
    {
        List<string> lista = new List<string>();
        try
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager
                    .ConnectionStrings["Conexion"].ConnectionString;
            con.Open();

            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_PERSONAL_BUSQUEDA_NOMBRE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("DES_NOMBRE", SqlDbType.VarChar, 100).Value = prefixText;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
   
            IDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(lector.GetString(0) + " - " + lector.GetString(1));
            }

            lector.Close();
            return lista;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            return null;
        }
    }
    protected void txtPersonal_TextChanged(object sender, EventArgs e)
    {
        //consultarEmpleado(txtPersonal.Text );
    }
    protected void consultarEmpleado(string dni)
    {

        string cleanMessage = string.Empty;
        if (dni == string.Empty)
        {
            cleanMessage = "Ingresar Nombre o DNI a consultar";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

                BL_PERSONAL objPersona = new BL_PERSONAL();
                DataTable dtResul = new DataTable();
                
                int i = 0; int j = 0;
                char[] letras = dni.ToCharArray();
                for (i = 0; i < dni.Length; i++)
                {
                    if (letras[i] == '-')
                    {
                        j = i;
                        break;
                    }
                }
                if (j > 0)
                {
                    dtResul = objPersona.BuscarDNI(dni.Substring(0, j - 1));
                }
                else
                {
                    dtResul = objPersona.BuscarDNI(dni);
                }
               
                limpiar();
                if (dtResul.Rows.Count > 0)
                {
                    //lblPersonal.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
                    int idPersona =Convert.ToInt32 (dtResul.Rows[0]["IDE_EMPLEADO"].ToString());
                    //lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();
                    BL_PERSONAL obj = new BL_PERSONAL();
                    DataTable dtResultado = new DataTable();
                    dtResultado = obj.BuscarDNI_MOI(idPersona);

                    if (dtResultado.Rows.Count > 0)
                    {
                        ModalRegistro.Show();
                        int EnProcesos = Convert.ToInt32(dtResultado.Rows[0]["EN_PROCESO"].ToString());
                        if (EnProcesos > 0)
                        {
                           // btnAsignar.Visible = false;
                            //////btnNo.Visible = false;
                            btnCerrar.Visible = true;
                        }
                        else
                        {
                            // btnAsignar.Visible = true;
                            //btnNo.Visible = true;
                            btnCerrar.Visible = false;
                        }
                        gridPersonal.DataSource = dtResultado;
                        gridPersonal.DataBind();

                    }
                    else
                    {
                        // REGISTRO NUEVOS
                        //lblIde_MOI.Text = "0";
                   
                        Estado();
                        //PanelDatos.Visible = true;
                        //txtDNI.ReadOnly = true;
                        //txtNombre.ReadOnly = true; 
                        //txtDNI.Text = dtResul.Rows[0]["DES_DNI"].ToString();
                        //txtNombre.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
                        //lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();
                        //txtDniBusqueda.Text = string.Empty;
                        //txtPersonal.Text = string.Empty;
                        //chkEstado.Checked = true;
                        //chkAtendido.Checked = false;
                        //restricciones();
                    }
                }
                else
                {
                    //PanelDatos.Visible = false;
                    //txtDniBusqueda.Text = string.Empty;
                    //txtPersonal.Text = string.Empty;
                    cleanMessage = "No se registra informacion";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }


        }
    }
    protected void btnBuscador_Click(object sender, ImageClickEventArgs e)
    {
       
        //consultarEmpleado(txtDniBusqueda.Text  );
    }

    #region 
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresas.DataSource = dtResultado;
            ddlEmpresas.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresas.DataBind(); 

            ddlEmpresasB.DataSource = dtResultado;
            ddlEmpresasB.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresasB.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresasB.DataBind();
            //this.ddlEmpresasB.Items.Insert(0, new ListItem("--------TODOS--------", "0")); 
           
            foreach (ListItem item in ddlEmpresasB.Items)
            {
                item.Selected = true;

            }
        }
    }
    protected void ParametrosUbicacion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlUbicacion.DataSource = obj.ListarParametros("DES_UBICACION", "RRHH_MOI");
        ddlUbicacion.DataTextField = "DES_ASUNTO";
        ddlUbicacion.DataValueField = "ID_PARAMETRO";
        ddlUbicacion.DataBind();
    }
    protected void Proyectos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.EmpresaProyectos(Convert.ToInt32(ddlEmpresas.SelectedValue), "RRHH");
        if (dtResultado.Rows.Count > 0)
        {
            ddlObra.DataSource = dtResultado;
            ddlObra.DataTextField = "DES_NOMBRE_OBRA";
            ddlObra.DataValueField = "IDOBRA";
            ddlObra.DataBind();
            CentroCostos();


             
        }
    }

    protected void ProyectosB()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.EmpresaProyectos(Convert.ToInt32((ddlEmpresasB.SelectedValue == "" ? 0 : Convert.ToInt32(ddlEmpresasB.SelectedValue))), "RRHH");
        if (dtResultado.Rows.Count > 0)
        { 
            ddlObraB.DataSource = dtResultado;
            ddlObraB.DataTextField = "DES_NOMBRE_OBRA";
            ddlObraB.DataValueField = "IDOBRA";
            ddlObraB.DataBind();
            //this.ddlObraB.Items.Insert(0, new ListItem("--------TODOS--------", "0"));
            CentroCostosB();

            foreach (ListItem item in ddlObraB.Items)
            {
                item.Selected = true;

            }
            
        }
    }

    protected void EstadosB()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEstadoB.DataSource = obj.ListarParametros("ID_ESTADO_PROCESO", "TMP_DET_REQ_PER");
        ddlEstadoB.DataTextField = "DES_ASUNTO";
        ddlEstadoB.DataValueField = "IN_ORDEN";
        ddlEstadoB.DataBind();
        //this.ddlEstadoB.Items.Insert(0, new ListItem("--------TODOS--------", "0"));
        foreach (ListItem item in ddlEstadoB.Items)
        {
            item.Selected = true;

        } 
    }

    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        Proyectos();
    }

    protected void ddlEmpresasB_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProyectosB();
        gridPersonal.DataSource = null;
        gridPersonal.DataBind();
        buscarRequerimiento();

    }

    protected void CentroCostos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_centro_Costos(ddlObra.SelectedValue);
        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = "DES_CCOSTO";
            ddlCentro.DataValueField = "ID_CENTROCOSTO";
            ddlCentro.DataBind(); 
        }
    }

    protected void CentroCostosB()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        string dobra = "";
        foreach (ListItem li in ddlObraB.Items)
        {
            //if (li.Selected)
            //{

                dobra += li.Value + ",";
            //}
            //else
            //{

            //}

        }

        dtResultado = obj.Listar_centro_Costos(dobra);
        if (dtResultado.Rows.Count > 0)
        {
            ddlCentroB.DataSource = dtResultado;
            ddlCentroB.DataTextField = "DES_CCOSTO";
            ddlCentroB.DataValueField = "ID_CENTROCOSTO";
            ddlCentroB.DataBind();
            //this.ddlCentroB.Items.Insert(0, new ListItem("--------TODOS--------", "0"));
            foreach (ListItem item in ddlCentroB.Items)
            {
                item.Selected = true;

            }
        }
    }


    protected void ddlObra_SelectedIndexChanged(object sender, EventArgs e)
    {
        CentroCostos();
    }
    protected void ddlObraB_SelectedIndexChanged(object sender, EventArgs e)
    {
        CentroCostosB();
        gridPersonal.DataSource = null;
        gridPersonal.DataBind();
        buscarRequerimiento();
    }
    protected void areas()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlArea.DataSource = obj.ListarParametros("IDE_AREA", "RRHH_MOI");
        ddlArea.DataTextField = "DES_ASUNTO";
        ddlArea.DataValueField = "ID_PARAMETRO";
        ddlArea.DataBind();

        
    }
    protected void cargo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCargo.DataSource = obj.ListarParametros("IDE_CARGO", "RRHH_MOI");
        ddlCargo.DataTextField = "DES_ASUNTO";
        ddlCargo.DataValueField = "ID_PARAMETRO";
        ddlCargo.DataBind();
         
    }
   /* protected void Proceso()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros("IDE_PROCESO", "RRHH_MOI");
        if (dtResultado.Rows.Count > 0)
        {
            ddlProceso.DataSource = obj.ListarParametros("IDE_PROCESO", "RRHH_MOI");
            ddlProceso.DataTextField = "DES_ASUNTO";
            ddlProceso.DataValueField = "ID_PARAMETRO";
            ddlProceso.DataBind();
        }
    }*/
    protected void Fuente()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros("IDE_FUENTE", "RRHH_MOI");
        //if (dtResultado.Rows.Count > 0)
        //{
        //    ddlFuente.DataSource = obj.ListarParametros("IDE_FUENTE", "RRHH_MOI");
        //    ddlFuente.DataTextField = "DES_ASUNTO";
        //    ddlFuente.DataValueField = "ID_PARAMETRO";
        //    ddlFuente.DataBind();
        //}
    }
    protected void TipoProceso()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros("IDE_PROCESO", "RRHH_MOI");
       
    }
    protected void Posicion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros("IDE_ORIGEN_POSICION", "RRHH_MOI");
        if (dtResultado.Rows.Count > 0)
        {

            ddlOrigen.DataSource = dtResultado;
            ddlOrigen.DataTextField = "DES_ASUNTO";
            ddlOrigen.DataValueField = "ID_PARAMETRO";
            ddlOrigen.DataBind();
        }
    }
    protected void Estado()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.EstadosMOI_Personal(Convert.ToInt32 (lblIde_MOI.Text));
     
        if (dtResultado.Rows.Count > 0)
        {
            //ddlEstado.DataSource = dtResultado;
            //ddlEstado.DataTextField = "DES_ASUNTO";
            //ddlEstado.DataValueField = "ID_PARAMETRO";
            //ddlEstado.DataBind();
    
            
            
            int AprobacionPendiente = Convert.ToInt32(dtResultado.Rows[0]["PENDIENTE_APROBACION"].ToString());
            int nro_proceso = Convert.ToInt32(dtResultado.Rows[0]["PROCESO"].ToString());
            //lblProcesoActual.Text = ddlEstado.SelectedValue; 
            if (AprobacionPendiente == 1)
            {
                //chkEstado.Checked = false ;
                EstadosProcesos_controles(nro_proceso);
            }
            else
            {
                //chkEstado.Checked = true ;
                EstadosProcesos_controles(0);
            }
            if (nro_proceso == 7)
            {
                //chkAtendido.Checked = true;
                //chkAtendido.Enabled = false;
            }

 
            
        }
    }
    protected void Salida()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        //ddlSalida.DataSource = obj.ListarParametros("IDE_SALIDA_EMPRESA", "RRHH_MOI");
        //ddlSalida.DataTextField = "DES_ASUNTO";
        //ddlSalida.DataValueField = "ID_PARAMETRO";
        //ddlSalida.DataBind();
    }
    #endregion

    private BE_REQUERIMIENTO f_CapturarDatos()
    {
        DateTime now = DateTime.Now;
        string snow = now.ToString("dd/MM/yyyy");

        BE_REQUERIMIENTO oBERequerimiento = new BE_REQUERIMIENTO();
        //oBEPersonal.i_ID_MOI_E = Convert.ToInt32(lblIde_MOI.Text);
        oBERequerimiento.EMPRESA_ORIGEN = (ddlEmpresas.SelectedValue);
        oBERequerimiento.CENTRO_COSTO_ORIGEN = (ddlEmpresas.SelectedValue);
        oBERequerimiento.CENTRO_COSTO = (ddlCentro.SelectedValue);
        oBERequerimiento.NUMERO_REQUISICION = Convert.ToInt32(txtRequerimiento.Text); // Convert.ToInt32((txtRequerimiento.Text == "") ? "0" : txtRequerimiento.Text);
        oBERequerimiento.SECUENCIA =Convert.ToInt32((txtItem.Text == "") ? "1" : txtItem.Text);
        oBERequerimiento.TIPO_TRABAJADOR = "01";
        oBERequerimiento.FECHA = (txtFechaAprobacion.Text);
        oBERequerimiento.OBRA = ddlObra.SelectedValue;
        oBERequerimiento.CATEGORIA_OBRERO = ddlArea.SelectedValue;
        oBERequerimiento.ESPECIALIDAD_TRABAJADOR = ddlCargo.SelectedValue;
        oBERequerimiento.USUARIO_CREACION = Session["IDE_USUARIO"].ToString();
        oBERequerimiento.USUARIO_ACTUALIZACION = Session["IDE_USUARIO"].ToString();
        return oBERequerimiento;
    }

    private BE_REQUERIMIENTO f_CapturarDatosB()
    {
        DateTime now = DateTime.Now;
        string snow = now.ToString("dd/MM/yyyy");

        BE_REQUERIMIENTO oBERequerimiento = new BE_REQUERIMIENTO();
        //oBEPersonal.i_ID_MOI_E = Convert.ToInt32(lblIde_MOI.Text);
        //oBERequerimiento.EMPRESA_ORIGEN = (ddlEmpresasB.SelectedValue);
        //oBERequerimiento.CENTRO_COSTO_ORIGEN = (ddlCentroB.SelectedValue);
        //oBERequerimiento.CENTRO_COSTO = (ddlCentroB.SelectedValue);
        oBERequerimiento.NUMERO_REQUISICION = Convert.ToInt32((txtRequerimientoB.Text == "") ? "0" : txtRequerimientoB.Text);
        oBERequerimiento.SECUENCIA = Convert.ToInt32((txtItemB.Text == "") ? "0" : txtItemB.Text);
        //oBERequerimiento.ESTADO_PROCESO = (ddlEstadoB.SelectedValue);
        //oBERequerimiento.CARGO = Convert.ToInt32( (ddlCargos.SelectedValue));

        String danalistas = string.Empty;
        String destados = string.Empty;
        String dempresas = string.Empty;
        String dcentros = string.Empty;
        String dobras = string.Empty;
        String dcargos = string.Empty;

        if (ddlEmpresasB.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlEmpresasB.Items)
            {
                if (li.Selected)
                {

                    dempresas += li.Value + ",";
                }  
            }
            oBERequerimiento.EMPRESA_ORIGEN = dempresas;
        }

        if (ddlObraB.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlObraB.Items)
            {
                if (li.Selected)
                {

                    dobras += li.Value + ",";
                }
            }
            oBERequerimiento.CENTRO_COSTO_ORIGEN = dobras;
        }

        if (ddlCentroB.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlCentroB.Items)
            {
                if (li.Selected)
                {

                    dcentros += li.Value + ",";
                }
            }
            oBERequerimiento.CENTRO_COSTO = dcentros;
        }

        if (ddlAnalistas.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlAnalistas.Items)
            {
                if (li.Selected)
                {

                    danalistas += li.Value + ",";
                }
                else
                {

                }

            }
            oBERequerimiento.ANALISTAS = danalistas;

        }

        if (ddlEstadoB.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlEstadoB.Items)
            {
                if (li.Selected)
                {

                    destados += li.Value + ",";
                } 
            } 
            oBERequerimiento.ESTADOS = destados;
        }

        if (ddlCargos.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlCargos.Items)
            {
                if (li.Selected)
                {

                    dcargos += li.Value + ",";
                }
            }
            oBERequerimiento.CARGO = dcargos;
        }

        return oBERequerimiento;
    }
 
    

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {       
        RegistrarRequerimiento();
    }


    protected void RegistrarRequerimiento()
    {
        string cleanMessage = string.Empty;
        BE_REQUERIMIENTO oBERequerimiento = new BE_REQUERIMIENTO();
        oBERequerimiento = f_CapturarDatos();
        int rpta;
        rpta = new BL_REQUERIMIENTO().Mant_Insert_Requerimiento(oBERequerimiento);

        if (rpta > 0)
        {
            cleanMessage = "Registro Satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //lblIde_MOI.Text = rpta.ToString();
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dtResultado = new DataTable();
            //dtResultado = obj.BuscarDNI_MOI(Convert.ToInt32(lblIde_MOI.Text));
            Estado();
            //limpiar();
            //PanelDatos.Visible = true;
            //txtDNI.ReadOnly = true;
            //txtNombre.ReadOnly = true; 
        }
    }
    protected void Datos(int codigo)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
     
        dtResultado = obj.Datos_MOI(codigo);
        if (dtResultado.Rows.Count > 0)
        {
            //PanelDatos.Visible = true;
            //txtDNI.ReadOnly = true;
            //txtNombre.ReadOnly = true;
            //lblIdPersonal.Text =  dtResultado.Rows[0]["IDE_EMPLEADO"].ToString();
            //txtDNI.Text = dtResultado.Rows[0]["DES_DNI"].ToString();
            //txtNombre.Text = dtResultado.Rows[0]["DES_NOMBRE"].ToString();
           
            string xEstado = dtResultado.Rows[0]["FLG_ESTADO"].ToString();
            
            Estado();
         //   ddlEstado.SelectedValue = xEstado;
            Estado();
            string obra = dtResultado.Rows[0]["IDOBRA"].ToString();
            string cc = dtResultado.Rows[0]["ID_CENTROCOSTO"].ToString();
            string empresa = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            Empresas();
            ddlEmpresas.SelectedValue = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            Proyectos();
            ProyectosB();
            Fuente();
            ddlObra.SelectedValue = dtResultado.Rows[0]["IDOBRA"].ToString();
            CentroCostos();
            ddlCentro.SelectedValue = dtResultado.Rows[0]["ID_CENTROCOSTO"].ToString();
            ddlArea.SelectedValue = dtResultado.Rows[0]["IDE_AREA"].ToString();
            areas();
            ddlCargo.SelectedValue = dtResultado.Rows[0]["IDE_CARGO"].ToString();
            cargo();
            //string idproceso = dtResultado.Rows[0]["IDE_PROCESO"].ToString();
            //ddlTipoProceso.SelectedValue = dtResultado.Rows[0]["IDE_PROCESO"].ToString();
            //ddlFuente.SelectedValue = dtResultado.Rows[0]["IDE_FUENTE"].ToString();
            Fuente();
            //ddlTipoProceso.SelectedValue = dtResultado.Rows[0]["IDE_TIPO_PROCESO"].ToString();
            TipoProceso();
            ddlUbicacion.SelectedValue = dtResultado.Rows[0]["DES_UBICACION"].ToString();
            ParametrosUbicacion();
            //txtFuente.Text  = dtResultado.Rows[0]["DES_FUENTE"].ToString();
            //ddlOrigen.SelectedValue = dtResultado.Rows[0]["IDE_ORIGEN_POSICION"].ToString();
            //txtRequerimiento.Text = dtResultado.Rows[0]["DES_REQUERIMIENTO"].ToString();
            //txtItem.Text = dtResultado.Rows[0]["DES_ITEM"].ToString();
            //txtFechaAprobacion.Text = dtResultado.Rows[0]["FEC_FECHA_APROBACION"].ToString();
            //txtViaje.Text = dtResultado.Rows[0]["FEC_FECHA_VIAJE"].ToString();
            //txtMedico.Text = dtResultado.Rows[0]["FEC_FECHA_EXAMEN_MED"].ToString();
            //txtEnvia.Text = dtResultado.Rows[0]["FEC_FECHA_PRIMERENVIO"].ToString();
            //txtFeedback.Text = dtResultado.Rows[0]["FEC_FECHA_FEEDBACK"].ToString();
            //txtTermino.Text = dtResultado.Rows[0]["FEC_SALIDA_EMPRESA"].ToString();
            //txtIngreso.Text = dtResultado.Rows[0]["FEC_FECHA_INGRESO"].ToString();
            //ddlSalida.SelectedValue = dtResultado.Rows[0]["IDE_SALIDA_EMPRESA"].ToString();
            Salida(); 
            
            //txtDniBusqueda.Text = string.Empty;
            //txtPersonal.Text = string.Empty;
            
        }
        else
        { 
        
        }

    }
    protected void EstadosProcesos_controles(int nro_proceso)
    {
        if (nro_proceso == 1)
        {
            txtFechaAprobacion.Enabled = true;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = false;
            //txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 2)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = true;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = false;
            ////txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 3)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = true;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = false;
            //txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 4)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = true;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = false;
            //txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 5)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = true;
            //txtIngreso.Enabled = false;
            ////txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 6)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = true;
            //txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 7)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = true;
            //txtTermino.Enabled = true;
            btnRegistrar.Visible = true;

        }
        else if (nro_proceso == 8)
        {
            txtFechaAprobacion.Enabled = false;
            //txtEnvia.Enabled = false;
            //txtFeedback.Enabled = false;
            //txtMedico.Enabled = false;
            //txtViaje.Enabled = false;
            //txtIngreso.Enabled = false;
            //txtTermino.Enabled = false;
            btnRegistrar.Visible = false;
        }
        else if (nro_proceso == 0)
        {
            txtFechaAprobacion.Enabled = true;
            //txtEnvia.Enabled = true;
            //txtFeedback.Enabled = true;
            //txtMedico.Enabled = true;
            //txtViaje.Enabled = true;
            //txtIngreso.Enabled = true;
            //txtTermino.Enabled = true;
            btnRegistrar.Visible = true;
        }

        txtFechaAprobacion.Enabled = true;
        //txtEnvia.Enabled = true;
        //txtFeedback.Enabled = true;
        //txtMedico.Enabled = true;
        //txtViaje.Enabled = true;
        //txtIngreso.Enabled = true;
        //txtTermino.Enabled = true;
        btnRegistrar.Visible = true;

    }
    protected void Seleccionar_REQUERIMIENTO(object sender, ImageClickEventArgs e)
    { 
        //string cleanMessage = string.Empty;
        //ImageButton btnSeleccionarRequerimiento = ((ImageButton)sender);
        //int ID_REQUERIMIENTO_PERSONAL = Convert.ToInt32(btnSeleccionarRequerimiento.CommandArgument);
        //Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"] = btnSeleccionarRequerimiento.CommandArgument;
        //BL_PERSONAL obj = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.buscar_PersonalDisponible(ID_REQUERIMIENTO_PERSONAL);

        //if (dtResultado.Rows.Count > 0)
        //{
        //    //ModalRegistro.Dispose();
        //    ModalRegistro.Show();

        //    //btnAsignar.Visible = false;
        //    //btnNo.Visible = false;
        //    btnCerrar.Visible = true;

        //}
        //else
        //{
        //    cleanMessage = "No existen postulantes matriculados";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
     
        //}
                    
        //gridPersonalDisponible.DataSource = dtResultado;
        //gridPersonalDisponible.DataBind();



        string cleanMessage = string.Empty;
        ImageButton btnSeleccionarRequerimiento = ((ImageButton)sender);
        int ID_REQUERIMIENTO_PERSONAL = Convert.ToInt32(btnSeleccionarRequerimiento.CommandArgument);
        Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"] = btnSeleccionarRequerimiento.CommandArgument;
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        DataTable dtIResultado = new DataTable();

        dtIResultado = obj.buscar_CantidadRequerimientos_MOI(ID_REQUERIMIENTO_PERSONAL);
        string x = dtIResultado.Rows[0]["TOTAL"].ToString();
        if (dtIResultado.Rows[0]["TOTAL"].ToString() =="0")
        {

            dtResultado = obj.buscar_PersonalDisponible_MOI(ID_REQUERIMIENTO_PERSONAL);

            cargos(); 

            if (dtResultado.Rows.Count > 0)
            {
                ModalRegistro.Show();
                btnCerrar.Visible = true;

            }
            else
            {
                cleanMessage = "No existen postulantes matriculados";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }

            gridPersonalDisponible.DataSource = dtResultado;
            gridPersonalDisponible.DataBind();
        }
        else
        {
            Response.Redirect("~/RRHH/frmRequerimientoDetalleMOI.aspx");
        }



    }
    protected void Seleccionar_Personal(object sender, ImageClickEventArgs e)
    {
        //PanelDatos.Visible = true;
        ImageButton btnSeleccionar = ((ImageButton)sender);
        ////lblIde_MOI.Text = btnSeleccionar.CommandArgument;
        //Datos(Convert.ToInt32 (btnSeleccionar.CommandArgument ));
        //Estado();

        //BL_PERSONAL obj = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.buscar_PersonalDisponible();

        //if (dtResultado.Rows.Count > 0)
        //{
        //    ModalRegistro.Show();

        //    //btnAsignar.Visible = false;
        //    btnNo.Visible = false;
        //    btnCerrar.Visible = true;

        //}

        //gridPersonalDisponible.DataSource = dtResultado;
        //gridPersonalDisponible.DataBind();

        Session["DES_DNI"] = btnSeleccionar.CommandArgument;
        string script = "window.open('frmMOI.aspx', '')";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

        ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('frmMOI.aspx','Title','height=1600,width=1800,left=10, top=10,scrollbars, resizable=no, location=center, toolbar=0')</script>", false);

        //Response.Redirect("frmMOI.aspx");//?id=123
    }

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        //lblIde_MOI.Text = "0";
        //limpiar();
        //PanelDatos.Visible = true;
        //BL_PERSONAL objPersona = new BL_PERSONAL();
        //DataTable dtResul = new DataTable();
        //chkEstado.Checked = true;
        //string Personal =  gridPersonal.DataKeys[0].Values[0].ToString();
        //dtResul = objPersona.BuscarDNI(Personal.Substring(0, 8));

        //PanelDatos.Visible = true;
        //txtDNI.ReadOnly = true;
        //txtDNI.Text = dtResul.Rows[0]["DES_DNI"].ToString();
        //txtNombre.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
        //txtDniBusqueda.Text = string.Empty;
        //txtPersonal.Text = string.Empty;
        Estado();
       
        EstadosProcesos_controles(0);
    }
    protected void limpiar()
    {
        Empresas();
        Proyectos();
        CentroCostos();
        ParametrosUbicacion();
        areas();
        cargo();
        Fuente();
        TipoProceso();
        Posicion();
        //txtComentarios.Text = string.Empty;
        //txtEnvia.Text = string.Empty;
        txtFechaAprobacion.Text = string.Empty;
        //txtFeedback.Text = string.Empty;
        //txtFuente.Text = string.Empty;
        txtItem.Text = string.Empty;
        txtRequerimiento.Text = string.Empty;
        //txtMedico.Text = string.Empty;
        //txtSueldo.Text = string.Empty;
        //txtTermino.Text = string.Empty;
        //txtViaje.Text = string.Empty;
        //lblIde_MOI.Text = "0";
        //txtIngreso.Text = string.Empty;
        //txtNombre.Text = string.Empty;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        limpiar();
        //PanelDatos.Visible = false; 
        //txtDniBusqueda.Text = string.Empty;
        //txtPersonal.Text = string.Empty;

    }
    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
          
    }
    protected void chkAtendido_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        //txtPersonal.Text = string.Empty;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        buscarRequerimiento();
        
    }

    protected void buscarRequerimiento()
    {
        string cleanMessage = string.Empty;
        BE_REQUERIMIENTO oBERequerimiento = new BE_REQUERIMIENTO();
        oBERequerimiento = f_CapturarDatosB();
        DataTable dtResultado = new DataTable();
       
        dtResultado = new BL_REQUERIMIENTO().Mant_Buscar_Requerimiento(oBERequerimiento);

        if (dtResultado.Rows.Count > 0)
        {
            //ModalRegistro.Show();
            int EnProcesos = 2;//Convert.ToInt32(dtResultado.Rows[0]["EN_PROCESO"].ToString());
            if (EnProcesos > 0)
            {
                //  btnAsignar.Visible = false;
                //btnNo.Visible = false;
                btnCerrar.Visible = true;
            }
            else
            {
                //btnAsignar.Visible = true;
                //btnNo.Visible = true;
                btnCerrar.Visible = false;
            }
            gridPersonal.DataSource = dtResultado;
            gridPersonal.DataBind();

        }
        else
        {
            gridPersonal.DataSource = null;
            gridPersonal.DataBind();
        }
        
    }

    protected void ddlCentroB_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridPersonal.DataSource = null;
        gridPersonal.DataBind();
        buscarRequerimiento();
    }
    protected void ddlEstadoB_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridPersonal.DataSource = null;
        gridPersonal.DataBind();
        buscarRequerimiento();
    }

    protected void ddlCargos_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridPersonal.DataSource = null;
        gridPersonal.DataBind();
        buscarRequerimiento();
    }

    protected void Anular_REQUERIMIENTO(object sender, ImageClickEventArgs e)
    {
         
        string cleanMessage = string.Empty;
        ImageButton btnSeleccionarRequerimiento = ((ImageButton)sender);
        int ID_REQUERIMIENTO_PERSONAL = Convert.ToInt32(btnSeleccionarRequerimiento.CommandArgument);
        BL_REQUERIMIENTO obj = new BL_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.anular_Requerimiento(ID_REQUERIMIENTO_PERSONAL);

        if (dtResultado.Rows.Count > 0)
        {
            cleanMessage = "Requerimiento Anulado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            buscarRequerimiento();
        }
    }

    protected void Eliminar_REQUERIMIENTO(object sender, ImageClickEventArgs e)
    {

        string cleanMessage = string.Empty;
        ImageButton btnSeleccionarRequerimiento = ((ImageButton)sender);
        int ID_REQUERIMIENTO_PERSONAL = Convert.ToInt32(btnSeleccionarRequerimiento.CommandArgument);
        BL_REQUERIMIENTO obj = new BL_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.eliminar_Requerimiento(ID_REQUERIMIENTO_PERSONAL);

        if (dtResultado.Rows.Count > 0)
        {
            cleanMessage = "Requerimiento Eliminado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            buscarRequerimiento();
        }
    }

    protected void GvCities_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = (GridView)sender;
        gv.PageIndex = e.NewPageIndex;
        //bindCitiesGrid();
          BL_PERSONAL obj = new BL_PERSONAL();
          DataTable dtResultado = new DataTable();
          dtResultado = obj.buscar_PersonalDisponible(Convert.ToInt32(Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"]));

          if (dtResultado.Rows.Count > 0)
          {
              //ModalRegistro.Dispose();
              ModalRegistro.Show();

              //btnAsignar.Visible = false;
              //btnNo.Visible = false;
              btnCerrar.Visible = true;

          }
          else
          {
             
          }

          gridPersonalDisponible.DataSource = dtResultado;
          gridPersonalDisponible.DataBind();
    }


    protected void Analistas()
    {
        try
        {
            con.Open();
            //string query = "SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOI] R WHERE  YEAR([FEC_FECHA_APROBACION]) =" + ddlAnio.SelectedValue;
            string query = "SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOI] R WHERE DES_RESPONSABLE IS NOT NULL";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            con.Close();
            if (t1.Rows.Count > 0)
            {
                ddlAnalistas.DataSource = t1;
                ddlAnalistas.DataTextField = "RESPONSABLE";
                ddlAnalistas.DataValueField = "DES_RESPONSABLE";
                ddlAnalistas.DataBind();
                this.ddlAnalistas.Items.Insert(0, new ListItem("PENDIENTE", "PENDIENTE"));
                foreach (ListItem item in ddlAnalistas.Items)
                {
                    item.Selected = true;

                }
            }
        }
        catch (Exception ex)
        {

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
        //this.ddlCargos.Items.Insert(0, new ListItem("--------TODOS--------", "0"));

        ddlCargosB.DataSource = obj.ListarParametros("IDE_CARGO", "RRHH_MOI");
        ddlCargosB.DataTextField = "DES_ASUNTO";
        ddlCargosB.DataValueField = "ID_PARAMETRO";
        ddlCargosB.DataBind();
        this.ddlCargosB.Items.Insert(0, new ListItem("--------TODOS--------", "0"));

        foreach (ListItem item in ddlCargos.Items)
        {
            item.Selected = true;

        } 

    }
    protected void ddlCargosB_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BL_PERSONAL obj = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.buscar_PersonalDisponible_Cargo(Convert.ToInt32(ddlCargo.SelectedValue));

        //if (dtResultado.Rows.Count > 0)
        //{
        //    //ModalRegistro.Dispose();
        //    ModalRegistro.Show();

        //    //btnAsignar.Visible = false;
        //    btnNo.Visible = false;
        //    btnCerrar.Visible = true;

        //}
        //else
        //{

        //}

        //gridPersonalDisponible.DataSource = dtResultado;
        //gridPersonalDisponible.DataBind();

    }
    protected void btnBuscarModal_Click(object sender, EventArgs e)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.buscar_PersonalDisponible_Cargo(Convert.ToInt32(ddlCargosB.SelectedValue),txtNombre.Text);
        string cleanMessage = string.Empty;

        if (dtResultado.Rows.Count == 0)
        {
            //ModalRegistro.Dispose();
            //

            ModalRegistro.Show();
            gridPersonalDisponible.DataSource = null;
            gridPersonalDisponible.DataBind();
            btnCerrar.Visible = true;

            //btnAsignar.Visible = false;
            //btnNo.Visible = false;
            //btnCerrar.Visible = true;

        }
        else
        {
         ModalRegistro.Show();
         gridPersonalDisponible.DataSource = dtResultado;
         gridPersonalDisponible.DataBind();
         btnCerrar.Visible = true;
        }

       
    }
    protected void chkEmpresas_CheckedChanged(object sender, EventArgs e)
    {

        if (chkEmpresas.Checked == true)
        {
            foreach (ListItem item in ddlEmpresas.Items)
            {
                item.Selected = true;

            }

        }

        if (chkEmpresas.Checked == false)
        {
            foreach (ListItem item in ddlEmpresas.Items)
            {
                item.Selected = false;

            }
        }

    }

    protected void chkProyectos_CheckedChanged(object sender, EventArgs e)
    {
        if (chkProyectos.Checked == true)
        {
            foreach (ListItem item in ddlObraB.Items)
            {
                item.Selected = true;

            }

        }

        if (chkProyectos.Checked == false)
        {
            foreach (ListItem item in ddlObraB.Items)
            {
                item.Selected = false;

            }
        }
    }

    protected void chkCentros_CheckedChanged(object sender, EventArgs e)
    {

        if (chkCentros.Checked == true)
        {
            foreach (ListItem item in ddlCentroB.Items)
            {
                item.Selected = true;

            }

        }

        if (chkCentros.Checked == false)
        {
            foreach (ListItem item in ddlCentroB.Items)
            {
                item.Selected = false;

            }
        }

    }

    protected void chkEstados_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEstados.Checked == true)
        {
            foreach (ListItem item in ddlEstadoB.Items)
            {
                item.Selected = true;

            }

        }

        if (chkEstados.Checked == false)
        {
            foreach (ListItem item in ddlEstadoB.Items)
            {
                item.Selected = false;

            }
        }

    }
    protected void chkReclutadores_CheckedChanged(object sender, EventArgs e)
    {
        if (chkReclutadores.Checked == true)
        {
            foreach (ListItem item in ddlAnalistas.Items)
            {
                item.Selected = true;

            }

        }

        if (chkReclutadores.Checked == false)
        {
            foreach (ListItem item in ddlAnalistas .Items)
            {
                item.Selected = false;

            }
        } 
         
    }



    protected void chkCargos_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCargos.Checked == true)
        {
            foreach (ListItem item in ddlCargos.Items)
            {
                item.Selected = true;

            }

        }

        if (chkCargos.Checked == false)
        {
            foreach (ListItem item in ddlCargos.Items)
            {
                item.Selected = false;

            }
        } 
        
    }

    protected void ddlObraB_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCargos.Checked == true)
        {
            foreach (ListItem item in ddlCargosB.Items)
            {
                item.Selected = true;

            }

        }
    
    }
    protected void AsignarRecursos(object sender, ImageClickEventArgs e)
    {

        ImageButton btnAsignar = ((ImageButton)sender);
        Session["Requerimiento"] = btnAsignar.CommandArgument;
        Session["Proceso"] = "MOI";

        //ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('AsignarRecurso.aspx?ID_DETALLE_REQUERIMIENTO_PERSONAL='" + Session["Requerimiento"].ToString () + "','Title','width=755,height=480,left=10, top=10,scrollbars, resizable=no, location=0, toolbar=0')</script>", false);


        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup(" + Session["Requerimiento"].ToString() + "," + 600 + "," + 520 + ");", true);
    }
}