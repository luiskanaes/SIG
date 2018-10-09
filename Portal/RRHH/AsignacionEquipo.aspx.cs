
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
public partial class RRHH_AsignacionEquipo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {

        }
    }
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string cleanMessage = string.Empty;
            if (txtNroTicket.Text == string.Empty)
            {
                cleanMessage = "Ingresar Numero de Atención";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                LlenarDatos();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void LlenarDatos()
    {
        string cleanMessage = string.Empty;
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        DataTable dtEncargados = new DataTable();
        dtResultado = obj.Datos_requerimiento_Ticket(txtNroTicket.Text);
        if (dtResultado.Rows.Count > 0)
        {
            PanelTicket.Visible = true;
            lblcandidato.Text = dtResultado.Rows[0]["CANDIDATO"].ToString();
            lblProyecto.Text = dtResultado.Rows[0]["PROYECTO"].ToString();
            lblEmpresa.Text = dtResultado.Rows[0]["EMPRESA"].ToString();
            lblCentro.Text = dtResultado.Rows[0]["CENTROCOSTO"].ToString();
            lblCargo.Text = dtResultado.Rows[0]["CARGO"].ToString();
            lblFechaIngreso.Text = dtResultado.Rows[0]["FECHA_INGRESO"].ToString();
            lblDni.Text = dtResultado.Rows[0]["DNI"].ToString();
            lblUbicacion.Text = dtResultado.Rows[0]["UBICACION"].ToString();
            txtObserva.Text = dtResultado.Rows[0]["OBSERVA"].ToString();
            int CodAsignacion = Convert.ToInt32(dtResultado.Rows[0]["IDE_ASIGNACION"].ToString());
            lblIdAsignacion.Text = CodAsignacion.ToString();
            lblRequerimientoPersonal.Text = dtResultado.Rows[0]["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
            Session["Requerimiento"] = lblRequerimientoPersonal.Text;
            int FLG_ESTADO = Convert.ToInt32(dtResultado.Rows[0]["REQ_ESTADO"].ToString());
            if (FLG_ESTADO == 4)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Requerimiento Anulado";
                btnGuardar.Visible = false;
            }
            {
                lblMensaje.Text = "";
                btnGuardar.Visible = true;
            }

            Ubicacion();
            equipo();
            Software();
            Otros();

            AsignacionCabecera();
            AsignacionDetalle();
            ListarSoftware();
            ListarOtros();


        }
        else
        {
            PanelTicket.Visible = false;
            cleanMessage = "No existe el número de atención ingresado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void AsignacionCabecera()
    {
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        string cleanMessage = string.Empty;
        dtResultado = obj.Listar_CabeceraAsignacion(Convert.ToInt32(Session["Requerimiento"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {
            RdoUbicacion.SelectedValue = dtResultado.Rows[0]["IDE_UBICACION"].ToString();
            Ubicacion();
            int estado = Convert.ToInt32(string.IsNullOrEmpty(dtResultado.Rows[0]["FLG_ESTADO"].ToString()) ? "1" : dtResultado.Rows[0]["FLG_ESTADO"].ToString());
            lblMensaje.Text = dtResultado.Rows[0]["ESTADO"].ToString();
        }

    }
    protected void AsignacionDetalle()
    {
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtEquipos = new DataTable();
        DataTable dtSoftware = new DataTable();
        dtEquipos = obj.Listar_recursosAsignados(Convert.ToInt32(Session["Requerimiento"].ToString()), "COMPUTO", "ASIGNACION_DETALLE");
        if (dtEquipos.Rows.Count > 0)
        {
            rdoEquipo.SelectedValue = dtEquipos.Rows[0]["IDE_RECURSO"].ToString();
            equipo();
        }


    }
    protected void ListarSoftware()
    {

        // read previously chosen items from database
        con.Open();
        SqlCommand cmd = new SqlCommand("uspSEL_LISTAR_RECURSOS_ASIGNADOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("ID_DETALLE_REQUERIMIENTO_PERSONAL", SqlDbType.Int).Value = Convert.ToInt32(Session["Requerimiento"].ToString());
        cmd.Parameters.Add("DES_DESCRIPCION", SqlDbType.VarChar, 100).Value = "SOFTWARE";
        cmd.Parameters.Add("DES_TABLA", SqlDbType.VarChar, 100).Value = "ASIGNACION_DETALLE";
        SqlDataReader reader = cmd.ExecuteReader();

        // iterate through saved entries and add to Hashtable
        Hashtable savedEntries = new Hashtable();
        while (reader.Read())
        {
            string hobbyID = reader["IDE_RECURSO"].ToString();
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
        SqlCommand cmd = new SqlCommand("uspSEL_LISTAR_RECURSOS_ASIGNADOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("ID_DETALLE_REQUERIMIENTO_PERSONAL", SqlDbType.Int).Value = Convert.ToInt32(Session["Requerimiento"].ToString());
        cmd.Parameters.Add("DES_DESCRIPCION", SqlDbType.VarChar, 100).Value = "OTROS";
        cmd.Parameters.Add("DES_TABLA", SqlDbType.VarChar, 100).Value = "ASIGNACION_DETALLE";
        SqlDataReader reader = cmd.ExecuteReader();

        // iterate through saved entries and add to Hashtable
        Hashtable savedEntries = new Hashtable();
        while (reader.Read())
        {
            string hobbyID = reader["IDE_RECURSO"].ToString();
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
    protected void Ubicacion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        RdoUbicacion.DataSource = obj.ListarParametros("UBICACION", "ASIGNACION_DETALLE");
        RdoUbicacion.DataTextField = "DES_ASUNTO";
        RdoUbicacion.DataValueField = "ID_PARAMETRO";
        RdoUbicacion.DataBind();
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
        string cleanMessage = string.Empty;
        string cleanMessageCare = string.Empty;
        string cleanMessageCGO = string.Empty;
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Datos_Controlrequerimiento_id(Convert.ToInt32(lblRequerimientoPersonal.Text));
        if (dtResultado.Rows.Count >0)
        {
            int ESTADO_EQUIPO, ESTADO_RECURSOS;

            ESTADO_EQUIPO = Convert.ToInt32(string.IsNullOrEmpty(dtResultado.Rows[0]["ESTADO_EQUIPO"].ToString()) ? "0" : dtResultado.Rows[0]["ESTADO_EQUIPO"].ToString());
            ESTADO_RECURSOS = Convert.ToInt32(string.IsNullOrEmpty(dtResultado.Rows[0]["ESTADO_RECURSOS"].ToString()) ? "0" : dtResultado.Rows[0]["ESTADO_RECURSOS"].ToString());

            cleanMessageCare = "No se puede renombrar Equipos de Computo (En proceso de atencion - Care), ";
            cleanMessageCGO = "Algunos recursos se encuentran en proceso de atención (No se pueden Realizar Cambios)";
            if (ESTADO_EQUIPO == 0) //ELIMINAR RECURSOS DEL CARE 
            {
                cleanMessageCare = "Equipo de Computo Actualizado ";
                obj.Eliminar_Requerimiento_care(Convert.ToInt32(lblRequerimientoPersonal.Text));

               
            }

            if (ESTADO_RECURSOS == 0)//ELIMINAR RECURSOS CGO 
            {
                obj.Eliminar_Requerimiento_cgo(Convert.ToInt32(lblRequerimientoPersonal.Text));
                cleanMessageCGO = "Recursos actualizados";
            }

            registrar();


            cleanMessage = cleanMessageCare + cleanMessageCGO;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            Ubicacion();
            equipo();
            Software();
            Otros();

            AsignacionCabecera();
            AsignacionDetalle();
            ListarSoftware();
            ListarOtros();
        }
    }

    protected void registrar()
    {
        string cleanMessage = string.Empty;
        BE_ASIGNACION_RECURSOS obj = new BE_ASIGNACION_RECURSOS();
        BL_ASIGNACION_RECURSOS Xobj = new BL_ASIGNACION_RECURSOS();
        int rpta;
        DataTable dtResultado = new DataTable();
        rpta = Convert.ToInt32(lblIdAsignacion.Text);



        //SELECCIONAR EQUIPO

        string equipo = rdoEquipo.SelectedValue;


        //seleccionar sofware
        string s = string.Empty;

        for (int i = 0; i < CheckSoftware.Items.Count; i++)
        {

            if (CheckSoftware.Items[i].Selected)
            {
                s += CheckSoftware.Items[i].Value + ",";
            }

        }

        // seleccionar Otros
        string Otros = string.Empty;
        for (int i = 0; i < CheckOtros.Items.Count; i++)
        {

            if (CheckOtros.Items[i].Selected)
            {
                Otros += CheckOtros.Items[i].Value + ",";
            }

        }

        dtResultado = Xobj.Insertar_AsignacionDetalle(rpta, equipo + "," + s + Otros, "", Session["IDE_USUARIO"].ToString ());

        if (dtResultado.Rows.Count > 0)
        {
            //ENVIAR AL CARE
            string x = lblRequerimientoPersonal.Text;
            Xobj.Enviar_care_Correo(Convert.ToInt32(lblRequerimientoPersonal.Text));

  
            AsignacionCabecera();
        }
        else
        {
            cleanMessage = "Existe problemas con las asignación de recursos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
}