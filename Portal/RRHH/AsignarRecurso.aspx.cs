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

public partial class RRHH_AsignarRecurso : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        int IdRequerimiento;
        string proceso;
    
        if (!Page.IsPostBack)
        {
            IdRequerimiento = Convert.ToInt32(Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString());
            Session["Requerimiento"] = Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
            proceso = Session["Proceso"].ToString();
            DatosPersonal();
            Ubicacion();
            equipo();
            Software();
            Otros();
            AsignacionCabecera();
            AsignacionDetalle();
            ListarSoftware();
            ListarOtros();
            Controles(true );
        }
    }
    protected void Controles(Boolean estado)
    {
        txtRequerimiento.ReadOnly = estado;
        txtCentro.ReadOnly = estado;
        txtFecha.ReadOnly = estado;
        txtIngreso.ReadOnly = estado;
        //txtObervacion.ReadOnly = estado;
        txtOcupacion.ReadOnly = estado;
        txtPersonal.ReadOnly = estado;
        txtProyecto.ReadOnly = estado;
        
    }
    protected void AsignacionCabecera()
    {
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        string cleanMessage = string.Empty;
        Session["Requerimiento"] = Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
        dtResultado = obj.Listar_CabeceraAsignacion(Convert.ToInt32(Session["Requerimiento"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {
            RdoUbicacion.SelectedValue = dtResultado.Rows[0]["IDE_UBICACION"].ToString();
            Ubicacion();
            txtObervacion.Text = dtResultado.Rows[0]["DES_OBSERVACIONES"].ToString();
            lblIdAsignacion.Text = dtResultado.Rows[0]["IDE_ASIGNACION"].ToString();
            int estado = Convert.ToInt32(string.IsNullOrEmpty(dtResultado.Rows[0]["FLG_ESTADO"].ToString()) ? "1" : dtResultado.Rows[0]["FLG_ESTADO"].ToString());
            Session["NRO_TICKET"] = dtResultado.Rows[0]["NRO_TICKET"].ToString();
            lblTicket.Text = "Ticket de Atención : " + dtResultado.Rows[0]["NRO_TICKET"].ToString();
            lblMensaje.Text = dtResultado.Rows[0]["ESTADO"].ToString();
            //txtObervacion.Text  = dtResultado.Rows[0]["OBSERVA"].ToString();
            LlenarDatos();
            
                if (estado == 1)
                {
                    btnEnviar.Visible = true;
                    btnCancelar.Visible = false;
                    btnRegistrar.Visible = true;
                }
                else if (estado == 2)
                {
                    btnEnviar.Visible = true;
                    btnRegistrar.Visible = false;
                    btnCancelar.Visible = true;
                }
                else if (estado == 3)
                {
                    btnEnviar.Visible = false;
                    btnCancelar.Visible = true;
                    btnRegistrar.Visible = false;
                }
                else if (estado == 4)
                {
                    btnEnviar.Visible = false;
                    btnCancelar.Visible = false;
                    btnRegistrar.Visible = false;
                }
            

        }
        else
        {
            //if (txtIngreso.Text == string.Empty)
            //{
            //    btnEnviar.Visible = false;
            //    btnCancelar.Visible = false;
            //    btnRegistrar.Visible = false;
            //    cleanMessage = "Falta indicar la Fecha de ingreso";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            //else
            //{
                btnRegistrar.Visible = true;
            //}
        }




    }
    protected void AsignacionDetalle()
    {
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtEquipos = new DataTable();
        DataTable dtSoftware = new DataTable();
        Session["Requerimiento"] = Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
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
    protected void DatosPersonal()
    {
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_datosRequerimiento(Convert.ToInt32(Session["Requerimiento"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {
            txtRequerimiento.Text = dtResultado.Rows[0]["NUMERO_REQUISICION"].ToString();
            txtFecha.Text = dtResultado.Rows[0]["FECHA_APROBACION"].ToString();
            txtCentro.Text = dtResultado.Rows[0]["CENTROCOSTO"].ToString();
            txtProyecto.Text = dtResultado.Rows[0]["PROYECTO"].ToString();

            txtPersonal.Text = dtResultado.Rows[0]["CANDIDATO_FINALISTA"].ToString();
            txtOcupacion.Text = dtResultado.Rows[0]["CARGO"].ToString();
            txtIngreso.Text = dtResultado.Rows[0]["FEC_INGRESO"].ToString();
            txtDNI.Text = dtResultado.Rows[0]["DNI"].ToString();
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

    protected void btnRegistrar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        BE_ASIGNACION_RECURSOS obj = new BE_ASIGNACION_RECURSOS();
        BL_ASIGNACION_RECURSOS Xobj = new BL_ASIGNACION_RECURSOS();

        obj = f_CapturarDatos(1);
        int rpta;
        DataTable dtResultado = new DataTable();
        rpta = new BL_ASIGNACION_RECURSOS().Mant_Insert_Asignacion(obj);
        lblIdAsignacion.Text = Convert.ToString(rpta);
        if (rpta > 0)
        {


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

             dtResultado = Xobj.Insertar_AsignacionDetalle(rpta, equipo +","+s + Otros, "", Session["IDE_USUARIO"].ToString());

            cleanMessage = "Registro Satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);



            AsignacionCabecera();
        }
    }
    private BE_ASIGNACION_RECURSOS f_CapturarDatos(int estado)
    {
        Session["Requerimiento"] = Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
        BE_ASIGNACION_RECURSOS oBE = new BE_ASIGNACION_RECURSOS();
        oBE.IDE_ASIGNACION = Convert.ToInt32(string.IsNullOrEmpty(lblIdAsignacion.Text) ? "0" : lblIdAsignacion.Text);
        oBE.ID_DETALLE_REQUERIMIENTO_PERSONAL = Convert.ToInt32(Session["Requerimiento"].ToString());
        oBE.DES_OBSERVACIONES = txtObervacion.Text;
        oBE.FLG_ESTADO = estado;
        oBE.IDE_UBICACION = Convert.ToInt32(RdoUbicacion.SelectedValue);
        return oBE;
    }
    protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        //BE_ASIGNACION_RECURSOS obj = new BE_ASIGNACION_RECURSOS();
        BL_ASIGNACION_RECURSOS Xobj = new BL_ASIGNACION_RECURSOS();
        //obj =f_CapturarDatos(2);
        Session["Requerimiento"] = Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
        DataTable dtResultado = new DataTable();
        dtResultado = Xobj.Enviar_care_Correo(Convert.ToInt32(Session["Requerimiento"].ToString()));
  
        if (dtResultado.Rows.Count  > 0)
        {   string ticket =  dtResultado.Rows[0]["ticket"].ToString();

            if (Convert.ToInt32(dtResultado.Rows[0]["REGISTRO"].ToString())> 0)
            {
                cleanMessage = "Se ha generado el requerimiento con el Nro de atención " + ticket;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                AsignacionCabecera();
            }

            else
            {
                cleanMessage = "Se ha presentado un incoveniente, error de asignación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        BL_ASIGNACION_RECURSOS Xobj = new BL_ASIGNACION_RECURSOS();
        //obj =f_CapturarDatos(2);
        Session["Requerimiento"] = Request.QueryString["ID_DETALLE_REQUERIMIENTO_PERSONAL"].ToString();
        DataTable dtResultado = new DataTable();
        dtResultado = Xobj.Enviar_Anulacion_Correo(Convert.ToInt32(Session["Requerimiento"].ToString()));
        AsignacionCabecera();
    }
    protected void LlenarDatos()
    {
        string cleanMessage = string.Empty;
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        DataTable dtEncargados = new DataTable();
        dtResultado = obj.Datos_requerimiento_Ticket(Session["NRO_TICKET"].ToString ());
        if (dtResultado.Rows.Count > 0)
        {
            PanelEstados.Visible = true;

            int CodAsignacion = Convert.ToInt32(dtResultado.Rows[0]["IDE_ASIGNACION"].ToString());

            dtEncargados = obj.Datos_requerimiento_encargados(CodAsignacion, "");

            if (dtEncargados.Rows.Count > 0)
            {
                DataListRecursos.DataSource = dtEncargados;
                DataListRecursos.DataBind();
                LlenarEstados();
            }

        }
        else
        {
            PanelEstados.Visible = false;
         
        }
    }
    private void LlenarEstados()
    {
        try
        {
            BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
            DataTable dtResultado = new DataTable();

            foreach (DataListItem FilaFactor in DataListRecursos.Items)
            {

                int id = (int)DataListRecursos.DataKeys[FilaFactor.ItemIndex];
                dtResultado = obj.LIST_ASIGNACION_DETALLE_POR_ID(id);

                RadioButtonList RadioEstados = ((RadioButtonList)FilaFactor.FindControl("RadioEstados"));
                RadioEstados.SelectedValue = dtResultado.Rows[0]["FLG_ATENDIDO"].ToString();

                RadioEstados.DataSource = GetEstado();
                RadioEstados.DataTextField = GetEstado().Columns["ValueMember"].ToString();
                RadioEstados.DataValueField = GetEstado().Columns["DisplayMember"].ToString();
                RadioEstados.DataBind();

            }

        }
        catch (Exception ex)
        {
            UC_MessageBox.Show(Page, Page.GetType(), "Ocurrio un error :" + ex.Message);
            return;
        }

    }
    private DataTable GetEstado()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add(1, "Pendiente"); //0
        dt.Rows.Add(2, "En Proceso");//1
        dt.Rows.Add(3, "Atendido");//1

        return dt;
    }
}