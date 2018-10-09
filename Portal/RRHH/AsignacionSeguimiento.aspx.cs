
using BusinessEntity;
using BusinessLogic;
using UserCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Diagnostics;
using System.Data.SqlClient;

public partial class RRHH_AsignacionSeguimiento : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Session["Usuario"] =  Request.QueryString["usuario"].ToString();
            
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            ConsultaUsuario();
            Estados();
        }
    }
    public string ConsultaUsuario()
    {

        string result = string.Empty;
        string sql = "SELECT TOP 1 DES_NOMBRE FROM [ASIGNACION_ENCARGADOS] WHERE DES_USUARIO =" + "'" + Session["Usuario"].ToString() + "'";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //result = Convert.ToInt32(cmd.ExecuteScalar());
                //cmd.Dispose();
                //conn.Close();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result = reader.GetValue(i).ToString();
                        }
                        lblPersona.Text = result;
                    }
                }
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return result;
    }
    protected void Estados()
    {
        CheckEstados.DataSource = GetEstado();
        CheckEstados.DataTextField = "ValueMember";
        CheckEstados.DataValueField = "DisplayMember";
        CheckEstados.DataBind();
    }

    private DataTable GetEstado()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dt.Rows.Add(1, "Pendiente");
        dt.Rows.Add(2, "En Proceso");
        dt.Rows.Add(3, "Atendido");


        return dt;

    }
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        LlenarDatos();
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
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        foreach (DataListItem FilaFactor in DataListRecursos.Items)
        {

            int id = (int)DataListRecursos.DataKeys[FilaFactor.ItemIndex];
            RadioButtonList RadioEstados = ((RadioButtonList)FilaFactor.FindControl("RadioEstados"));
            obj.UPD_ASIGNACION_ESTADO(id, Session["Usuario"].ToString(), Convert.ToInt32(RadioEstados.SelectedValue));
        }
        cleanMessage = "Actualizacion Satifactoria";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        LlenarDatos();
    }
    protected void LlenarDatos()
    {

        String estado = string.Empty;
        DataTable dtRecursos = new DataTable();
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        if (CheckEstados.SelectedIndex != -1)
        {
            foreach (ListItem li in CheckEstados.Items)
            {
                if (li.Selected)
                {

                    estado += li.Value + ",";
                }

            }

        }

        if (estado == string.Empty)
        {
            string cleanMessage = "Seleccionar algun estado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            dtRecursos = obj.Datos_requerimiento_encargadosTotal(Session["Usuario"].ToString(), estado);

            if (dtRecursos.Rows.Count > 0)
            {
                PanelDatos.Visible = true;
                DataListRecursos.DataSource = dtRecursos;
                DataListRecursos.DataBind();
                LlenarEstados();
            }
            else
            {
                PanelDatos.Visible = false ;
            }
        }
    }
    protected void Seleccionar(object sender, ImageClickEventArgs e)
    {
        ModalRegistro.Show(); 
        ImageButton btnDatos = ((ImageButton)sender);
         DataTable dtResultado = new DataTable();
         BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        dtResultado = obj.Datos_requerimiento_Ticket(btnDatos.CommandArgument );
        if (dtResultado.Rows.Count > 0)
        {
            
            lblcandidato.Text = dtResultado.Rows[0]["CANDIDATO"].ToString();
            lblProyecto.Text = dtResultado.Rows[0]["PROYECTO"].ToString();
            lblEmpresa.Text = dtResultado.Rows[0]["EMPRESA"].ToString();
            lblCentro.Text = dtResultado.Rows[0]["CENTROCOSTO"].ToString();
            lblCargo.Text = dtResultado.Rows[0]["CARGO"].ToString();
            lblFechaIngreso.Text = dtResultado.Rows[0]["FECHA_INGRESO"].ToString();
            lblDni.Text = dtResultado.Rows[0]["DNI"].ToString();
            lblUbicacion.Text = dtResultado.Rows[0]["UBICACION"].ToString();
            txtObserva.Text = dtResultado.Rows[0]["OBSERVA"].ToString();
 
        }
    }
}