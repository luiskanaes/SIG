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
      //  txtDniBusqueda.Attributes.Add("onkeypress", "javascript:return SoloNum(event); "); 

        string sesion = Convert.ToString(Session["DES_DNI"]);
        PanelBuscar.DefaultButton = btnBuscador.ID;

        if (sesion != "")
        {
            RegistrarNuevoMOI();
            consultarEmpleadoLoad(sesion);
            Session["DES_DNI"] = null;
        }
        //if Session["xx"] == null
            
        //else
        //objectbucas(Session["xx"] )

        //lblPersonal.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
        //            int idPersona =Convert.ToInt32 (dtResul.Rows[0]["IDE_EMPLEADO"].ToString());
        //            lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();




        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            ControlBotones();
            Empresas();
            ParametrosUbicacion();
            Proyectos();
            CentroCostos();
            areas();
            cargo();
            //Proceso();
            Fuente();
            TipoProceso();
            Posicion();
            
            Salida();
            
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
        Response.Redirect("~/RRHH/frmReporteMOI.aspx");
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
        consultarEmpleado(txtPersonal.Text );
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
                  //                      dtResul = objPersona.BuscarDNI(dni.Substring(0, j - 1));
                    dtResul = objPersona.BuscarDNIMOI(dni.Substring(0, j - 1));
                }
                else
                {
                  //  dtResul = objPersona.BuscarDNI(dni);
                    dtResul = objPersona.BuscarDNIMOI(dni);
                  //  RegistrarNuevoMOI();
                }
               
                limpiar();
                if (dtResul.Rows.Count > 0)
                {
                    lblPersonal.Text = dtResul.Rows[0]["DES_NOMBRE_APES"].ToString();
                    int idPersona =Convert.ToInt32 (dtResul.Rows[0]["IDE_EMPLEADO"].ToString());
                    lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();
                    BL_PERSONAL obj = new BL_PERSONAL();
                    DataTable dtResultado = new DataTable();
                    dtResultado = obj.BuscarDNI_MOI(idPersona);

                    if (dtResultado.Rows.Count > 0)
                    {
                        ModalRegistro.Show();
                        int EnProcesos = Convert.ToInt32(dtResultado.Rows[0]["EN_PROCESO"].ToString());
                        if (EnProcesos > 0)
                        {
                            btnAsignar.Visible = false;
                            btnNo.Visible = false;
                            btnCerrar.Visible = true;
                        }
                        else
                        {
                            btnAsignar.Visible = true;
                            btnNo.Visible = true;
                            btnCerrar.Visible = false;
                        }
                        gridPersonal.DataSource = dtResultado;
                        gridPersonal.DataBind();

                    }
                    else
                    {
                        // REGISTRO NUEVOS
                        lblIde_MOI.Text = "0";
                   
                        Estado();
                        PanelDatos.Visible = true;
                        txtDNI.ReadOnly = true;
                        txtNombre.ReadOnly = true; 
                        txtDNI.Text = dtResul.Rows[0]["DES_DNI"].ToString();
                        txtNombre.Text = dtResul.Rows[0]["DES_NOMBRE_APES"].ToString();
                        lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();
                        txtDniBusqueda.Text = string.Empty;
                        txtPersonal.Text = string.Empty;
                        chkEstado.Checked = true;
                        chkAtendido.Checked = false;
                        //restricciones();
                    }
                }
                else
                {
                    PanelDatos.Visible = false;
                    txtDniBusqueda.Text = string.Empty;
                    txtPersonal.Text = string.Empty;
                    cleanMessage = "No se registra informacion";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }


        }
    }
    protected void btnBuscador_Click(object sender, ImageClickEventArgs e)
    {
        if(txtPersonal.Text.Trim() != string.Empty)
        {
            consultarEmpleado(txtPersonal.Text);
        }
        else
        {
            consultarEmpleado(txtDniBusqueda.Text);
        }
       
    }
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

    protected void ProyectosTodos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.EmpresaProyectos(0, "RRHH");
        if (dtResultado.Rows.Count > 0)
        {
            ddlObra.DataSource = dtResultado;
            ddlObra.DataTextField = "DES_NOMBRE_OBRA";
            ddlObra.DataValueField = "IDOBRA";
            ddlObra.DataBind();
           // CentroCostos();
        }
    }
    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        Proyectos();
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

    protected void CentroCostosTodos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_centro_Costos("0");
        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = "DES_CCOSTO";
            ddlCentro.DataValueField = "ID_CENTROCOSTO";
            ddlCentro.DataBind();
        }
    }

    protected void ddlObra_SelectedIndexChanged(object sender, EventArgs e)
    {
        CentroCostos();
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
        if (dtResultado.Rows.Count > 0)
        {
            ddlFuente.DataSource = obj.ListarParametros("IDE_FUENTE", "RRHH_MOI");
            ddlFuente.DataTextField = "DES_ASUNTO";
            ddlFuente.DataValueField = "ID_PARAMETRO";
            ddlFuente.DataBind();
        }
    }
    protected void TipoProceso()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarParametros("IDE_PROCESO", "RRHH_MOI");
        if (dtResultado.Rows.Count > 0)
        {
            ddlTipoProceso.DataSource = obj.ListarParametros("IDE_PROCESO", "RRHH_MOI");
            ddlTipoProceso.DataTextField = "DES_ASUNTO";
            ddlTipoProceso.DataValueField = "ID_PARAMETRO";
            ddlTipoProceso.DataBind();
        }
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
        dtResultado = obj.EstadosMOI_Personal(Convert.ToInt32 (lblIde_MOI.Text));
     
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstado.DataSource = dtResultado;
            ddlEstado.DataTextField = "DES_ASUNTO";
            ddlEstado.DataValueField = "ID_PARAMETRO";
            ddlEstado.DataBind();
    
            
            
            int AprobacionPendiente = Convert.ToInt32(dtResultado.Rows[0]["PENDIENTE_APROBACION"].ToString());
            int nro_proceso = Convert.ToInt32(dtResultado.Rows[0]["PROCESO"].ToString());
            lblProcesoActual.Text = ddlEstado.SelectedValue; 
            if (AprobacionPendiente == 1)
            {
                chkEstado.Checked = false ;
                EstadosProcesos_controles(nro_proceso);
            }
            else
            {
                chkEstado.Checked = true ;
                EstadosProcesos_controles(0);
            }
            if (nro_proceso == 7)
            {
                chkAtendido.Checked = true;
                chkAtendido.Enabled = false;
            }

 
            
        }
    }
    protected void Salida()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlSalida.DataSource = obj.ListarParametros("IDE_SALIDA_EMPRESA", "RRHH_MOI");
        ddlSalida.DataTextField = "DES_ASUNTO";
        ddlSalida.DataValueField = "ID_PARAMETRO";
        ddlSalida.DataBind();
    }
    private BE_MOI f_CapturarDatos()
    {
        DateTime now = DateTime.Now;
        string snow = now.ToString("dd/MM/yyyy");

        BE_MOI oBEPersonal = new BE_MOI();
        oBEPersonal.i_ID_MOI_E = Convert.ToInt32(lblIde_MOI.Text);
        oBEPersonal.f_FEC_FECHA_APROBACION_E = txtFechaAprobacion.Text;
        oBEPersonal.f_FEC_FECHA_EXAMEN_MED_E = txtMedico.Text;
        oBEPersonal.f_FEC_FECHA_FEEDBACK_E = txtFeedback.Text;
        oBEPersonal.f_FEC_FECHA_PRIMERENVIO_E = txtEnvia.Text;
        oBEPersonal.f_FEC_FECHA_VIAJE_E = txtViaje.Text;
        oBEPersonal.f_FEC_SALIDA_EMPRESA_E = txtTermino.Text;
        oBEPersonal.i_DES_UBICACION = Convert.ToInt32(ddlUbicacion.SelectedValue);

        //if (chkEstado.Checked)
        //{
        //    oBEPersonal.i_FLG_ESTADO_E = Convert.ToInt32(lblEstado_ProcesoSgte.Text);
        //}
        //else
        //{
            oBEPersonal.i_FLG_ESTADO_E = Convert.ToInt32(ddlEstado.SelectedValue);
        //}
        oBEPersonal.i_ID_CENTROCOSTO_E = Convert.ToInt32(ddlCentro.SelectedValue);
        oBEPersonal.i_ID_EMPRESA_E = Convert.ToInt32(ddlEmpresas.SelectedValue);
        oBEPersonal.i_IDE_AREA_E = Convert.ToInt32(ddlArea.SelectedValue);
        oBEPersonal.i_IDE_CARGO_E = Convert.ToInt32(ddlCargo.SelectedValue);
        oBEPersonal.i_IDE_ORIGEN_POSICION_E = Convert.ToInt32(ddlOrigen.SelectedValue);
        //oBEPersonal.i_IDE_PROCESO_E = Convert.ToInt32(ddlProceso.SelectedValue);
        oBEPersonal.i_IDE_FUENTE_E = Convert.ToInt32(ddlFuente.SelectedValue);
        oBEPersonal.i_IDE_TIPO_PROCESO_E = Convert.ToInt32(ddlTipoProceso.SelectedValue);
        oBEPersonal.v_DES_COMENTARIOS_E = txtComentarios.Text;
        oBEPersonal.v_DES_FUENTE_E = txtFuente.Text;
        oBEPersonal.v_DES_ITEM_E = txtItem.Text;
        oBEPersonal.v_DES_REQUERIMIENTO_E = txtRequerimiento.Text;
        oBEPersonal.v_IDE_EMPLEADO_E = Convert.ToInt32(lblIdPersonal.Text);
        oBEPersonal.v_IDOBRA_E = ddlObra.SelectedValue;
        oBEPersonal.v_DES_RESPONSABLE_E = Session["IDE_USUARIO"].ToString();
        oBEPersonal.i_IDE_SALIDA_EMPRESA_E = Convert.ToInt32(ddlSalida.SelectedValue);
        oBEPersonal.f_FEC_INGRESO_E = txtIngreso.Text;

        oBEPersonal.f_FEC_ATENCION_E = ((chkAtendido.Checked == true) ? snow : "");

        oBEPersonal.i_IDE_NRO_PROCESO_E = Convert.ToInt32(1) ;

        oBEPersonal.v_ID_DETALLE_REQUERIMIENTO_PERSONAL_E = Convert.ToString((Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"]));

        return oBEPersonal;
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        string query = "SELECT [ID_PARAMETRO] FROM [PARAMETROS] WHERE [DES_DESCRIPCION] ='FLG_ESTADO' AND [DES_TABLA] ='RRHH_MOI' AND [DES_ASUNTO] ='ANULADO'";
        SqlCommand cmd = new SqlCommand(query, con);
        DataTable t1 = new DataTable();
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(t1);
        }
        con.Close();
        string cleanMessage;
        if (chkEstado.Checked )
        {
            if (txtIngreso.Text != string.Empty )
            {
                if (txtFechaAprobacion.Text == string.Empty)
                {
                    cleanMessage = "Falta Ingresa Fecha de Aprobacion";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    //txtIngreso.Text = string.Empty;
                }
                else
                {
                    RegistrarMOI();
                  
                }
                
            }
            else
            {
                RegistrarMOI();
            }


        }
        else
        {
            if (txtRequerimiento.Text == string.Empty || txtItem.Text == string.Empty)
            {
                cleanMessage = "Ingresar Requerimiento y/o Item";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                Datos(Convert.ToInt32(lblIde_MOI.Text ));
            }
            else
            {
                string EstadoAnulado = t1.Rows[0]["ID_PARAMETRO"].ToString();
                if (ddlEstado.SelectedValue != EstadoAnulado)
                {
                    RegistrarMOI();

                }
                else
                {

                    RegistrarMOI();
                }
            }
        }




    }


    protected void RegistrarMOI()
    {
        string cleanMessage = string.Empty;
        BE_MOI oBEPersonal = new BE_MOI();
        oBEPersonal = f_CapturarDatos();
        int rpta;
        rpta = new BL_PERSONAL().Mant_Insert_Operarios(oBEPersonal);

        if (rpta > 0)
        {
            // consultarEmpleado(txtDNI.Text);
            cleanMessage = "Registro Satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            lblIde_MOI.Text = rpta.ToString();
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.BuscarDNI_MOI(Convert.ToInt32(lblIde_MOI.Text));
            Estado();
            PanelDatos.Visible = true;
            txtDNI.ReadOnly = true;
            txtNombre.ReadOnly = true;
            //Response.Redirect("frmRequerimiento.aspx");//?id=123
            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('frmMOI.aspx', '_self', null);", true);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "closePage",  "window.open('frmMOI.aspx', '_self', null);", true);

        }
    }

    protected void RegistrarNuevoMOI()
    {
        string cleanMessage = string.Empty;        
        int rpta;

        string requerimiento = Convert.ToString(Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"]);
        string dni = Convert.ToString(Session["DES_DNI"]);
        string responsable = Convert.ToString(Session["IDE_USUARIO"]);

        rpta = new BL_PERSONAL().Mant_Insert_OperariosNuevosMoi(requerimiento, dni, responsable);

        if (rpta > 0)
        {
            // consultarEmpleado(txtDNI.Text);
            cleanMessage = "Registro Satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            lblIde_MOI.Text = rpta.ToString();
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.BuscarDNI_MOI(Convert.ToInt32(lblIde_MOI.Text));
            Estado();
            PanelDatos.Visible = true;
            txtDNI.ReadOnly = true;
            txtNombre.ReadOnly = true;
        }
    }
    protected void Datos(int codigo)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
     
        dtResultado = obj.Datos_MOI(codigo);
        if (dtResultado.Rows.Count > 0)
        {
            PanelDatos.Visible = true;
            txtDNI.ReadOnly = true;
            txtNombre.ReadOnly = true;
            lblIdPersonal.Text =  dtResultado.Rows[0]["IDE_EMPLEADO"].ToString();
            txtDNI.Text = dtResultado.Rows[0]["DES_DNI"].ToString();
            txtNombre.Text = dtResultado.Rows[0]["DES_NOMBRE"].ToString();
           
            string xEstado = dtResultado.Rows[0]["FLG_ESTADO"].ToString();
            
            Estado();
         //   ddlEstado.SelectedValue = xEstado;
            Estado();
            string obra = dtResultado.Rows[0]["IDOBRA"].ToString();
            string cc = dtResultado.Rows[0]["ID_CENTROCOSTO"].ToString();
            string empresa = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            Empresas();
            ddlEmpresas.SelectedValue = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            ProyectosTodos();
            Fuente();
            CentroCostosTodos();
            ddlObra.SelectedValue = dtResultado.Rows[0]["IDOBRA"].ToString().Trim();
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
            ddlTipoProceso.SelectedValue = dtResultado.Rows[0]["IDE_TIPO_PROCESO"].ToString();
            TipoProceso();
            ddlUbicacion.SelectedValue = dtResultado.Rows[0]["DES_UBICACION"].ToString();
            ParametrosUbicacion();
            txtFuente.Text  = dtResultado.Rows[0]["DES_FUENTE"].ToString();
            //ddlOrigen.SelectedValue = dtResultado.Rows[0]["IDE_ORIGEN_POSICION"].ToString();
            txtRequerimiento.Text = dtResultado.Rows[0]["DES_REQUERIMIENTO"].ToString();
            txtItem.Text = dtResultado.Rows[0]["DES_ITEM"].ToString();
            txtFechaAprobacion.Text = dtResultado.Rows[0]["FEC_FECHA_APROBACION"].ToString();
            txtViaje.Text = dtResultado.Rows[0]["FEC_FECHA_VIAJE"].ToString();
            txtMedico.Text = dtResultado.Rows[0]["FEC_FECHA_EXAMEN_MED"].ToString();
            txtEnvia.Text = dtResultado.Rows[0]["FEC_FECHA_PRIMERENVIO"].ToString();
            txtFeedback.Text = dtResultado.Rows[0]["FEC_FECHA_FEEDBACK"].ToString();
            txtTermino.Text = dtResultado.Rows[0]["FEC_SALIDA_EMPRESA"].ToString();
            txtIngreso.Text = dtResultado.Rows[0]["FEC_FECHA_INGRESO"].ToString();
            txtComentarios.Text = dtResultado.Rows[0]["DES_COMENTARIOS"].ToString();
            //ddlSalida.SelectedValue = dtResultado.Rows[0]["IDE_SALIDA_EMPRESA"].ToString();
            Salida(); 
            
            txtDniBusqueda.Text = string.Empty;
            txtPersonal.Text = string.Empty;

            desabilitarControles();
            
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
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = false;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = false;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 2)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = true;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = false;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = false;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 3)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = true;
            txtMedico.Enabled = false;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = false;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 4)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = true;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = false;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 5)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = false;
            txtViaje.Enabled = true;
            txtIngreso.Enabled = false;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 6)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = false;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = true;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = true;
        }
        else if (nro_proceso == 7)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = false;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = true;
            txtTermino.Enabled = true;
            btnRegistrar.Visible = true;

        }
        else if (nro_proceso == 8)
        {
            txtFechaAprobacion.Enabled = false;
            txtEnvia.Enabled = false;
            txtFeedback.Enabled = false;
            txtMedico.Enabled = false;
            txtViaje.Enabled = false;
            txtIngreso.Enabled = false;
            txtTermino.Enabled = false;
            btnRegistrar.Visible = false;
        }
        else if (nro_proceso == 0)
        {
            txtFechaAprobacion.Enabled = true;
            txtEnvia.Enabled = true;
            txtFeedback.Enabled = true;
            txtMedico.Enabled = true;
            txtViaje.Enabled = true;
            txtIngreso.Enabled = true;
            txtTermino.Enabled = true;
            btnRegistrar.Visible = true;
        }

        txtFechaAprobacion.Enabled = true;
        txtEnvia.Enabled = true;
        txtFeedback.Enabled = true;
        txtMedico.Enabled = true;
        txtViaje.Enabled = true;
        txtIngreso.Enabled = true;
        txtTermino.Enabled = true;
        btnRegistrar.Visible = true;

    }
    protected void Seleccionar_MOI(object sender, ImageClickEventArgs e)
    {
        PanelDatos.Visible = true;
        ImageButton btnSeleccionar = ((ImageButton)sender);
        lblIde_MOI.Text = btnSeleccionar.CommandArgument;
        Datos(Convert.ToInt32 (btnSeleccionar.CommandArgument ));
        Estado();
    }

    protected void Anular_MOI(object sender, ImageClickEventArgs e)
    {
        PanelDatos.Visible = true;
        ImageButton btnSeleccionar = ((ImageButton)sender);
        lblIde_MOI.Text = btnSeleccionar.CommandArgument;

        BL_PERSONAL objPersona = new BL_PERSONAL();
        DataTable dtResul = new DataTable();
        chkEstado.Checked = true;
        string Personal = gridPersonal.DataKeys[0].Values[0].ToString();
        dtResul = objPersona.Anular_Moi((Convert.ToInt32(btnSeleccionar.CommandArgument)));

        string cleanMessage = string.Empty;
        if (dtResul.Rows.Count == 0)
        {
            cleanMessage = "Movimiento anulado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        Response.Redirect("~/RRHH/frmRequerimiento.aspx");


    }

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        lblIde_MOI.Text = "0";
        limpiar();
        PanelDatos.Visible = true;
        BL_PERSONAL objPersona = new BL_PERSONAL();
        DataTable dtResul = new DataTable();
        chkEstado.Checked = true;
        string Personal =  gridPersonal.DataKeys[0].Values[0].ToString();
        dtResul = objPersona.BuscarDNI(Personal);

        PanelDatos.Visible = true;
        txtDNI.ReadOnly = true;
        txtDNI.Text = dtResul.Rows[0]["DES_DNI"].ToString();
        txtNombre.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
        txtDniBusqueda.Text = string.Empty;
        txtPersonal.Text = string.Empty;
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
        txtComentarios.Text = string.Empty;
        txtEnvia.Text = string.Empty;
        txtFechaAprobacion.Text = string.Empty;
        txtFeedback.Text = string.Empty;
        txtFuente.Text = string.Empty;
        txtItem.Text = string.Empty;
        txtRequerimiento.Text = string.Empty;
        txtMedico.Text = string.Empty;
        txtSueldo.Text = string.Empty;
        txtTermino.Text = string.Empty;
        txtViaje.Text = string.Empty;
        lblIde_MOI.Text = "0";
        txtIngreso.Text = string.Empty;
        txtNombre.Text = string.Empty;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        limpiar();
        PanelDatos.Visible = false; 
        txtDniBusqueda.Text = string.Empty;
        txtPersonal.Text = string.Empty;

    }
    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
          
    }
    protected void chkAtendido_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        txtPersonal.Text = string.Empty;
    }

    protected void consultarEmpleadoLoad(string dni)
    {

        string cleanMessage = string.Empty;
        
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
                //                      dtResul = objPersona.BuscarDNI(dni.Substring(0, j - 1));
                dtResul = objPersona.BuscarDNIMOI(dni.Substring(0, j - 1));
            }
            else
            {
                //  dtResul = objPersona.BuscarDNI(dni);
                dtResul = objPersona.BuscarDNIMOI(dni);
                //  RegistrarNuevoMOI();
            }

            limpiar();
            if (dtResul.Rows.Count > 0)
            {
                lblPersonal.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
                int idPersona = Convert.ToInt32(dtResul.Rows[0]["IDE_EMPLEADO"].ToString());
                lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();
                BL_PERSONAL obj = new BL_PERSONAL();
                DataTable dtResultado = new DataTable();
                dtResultado = obj.BuscarDNI_MOI(idPersona);
                
                if (dtResultado.Rows.Count > 0)
                {
                    ModalRegistro.Show();
                    int EnProcesos = Convert.ToInt32(dtResultado.Rows[0]["EN_PROCESO"].ToString());
                    if (EnProcesos > 0)
                    {
                        btnAsignar.Visible = false;
                        btnNo.Visible = false;
                        btnCerrar.Visible = true;
                    }
                    else
                    {
                        btnAsignar.Visible = true;
                        btnNo.Visible = true;
                        btnCerrar.Visible = false;
                    }
                    gridPersonal.DataSource = dtResultado;
                    gridPersonal.DataBind();

                }
                else
                {
                    // REGISTRO NUEVOS
                    lblIde_MOI.Text = "0";

                    Estado();
                    PanelDatos.Visible = true;
                    txtDNI.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    txtDNI.Text = dtResul.Rows[0]["DES_DNI"].ToString();
                    txtNombre.Text = dtResul.Rows[0]["DES_NOMBRE"].ToString();
                    lblIdPersonal.Text = dtResul.Rows[0]["IDE_EMPLEADO"].ToString();
                    txtDniBusqueda.Text = string.Empty;
                    txtPersonal.Text = string.Empty;
                    chkEstado.Checked = true;
                    chkAtendido.Checked = false;
                    //restricciones();
                }
            }
            else
            {
                PanelDatos.Visible = false;
                txtDniBusqueda.Text = string.Empty;
                txtPersonal.Text = string.Empty;
                //cleanMessage = "No se registra informacion";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }


        }
    }

    protected void desabilitarControles()
    {
        txtNombre.Enabled = false;
        //txtDNI.Enabled = false;
        txtItem.Enabled = false;
        txtRequerimiento.Enabled = false;
        ddlObra.Enabled = false;
        ddlCentro.Enabled = false;
        ddlEmpresas.Enabled = false;
        ddlArea.Enabled = false;  
        ddlCargo.Enabled = false;  
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {

       
    }
}