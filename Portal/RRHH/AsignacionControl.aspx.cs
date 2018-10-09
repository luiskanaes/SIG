
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
public partial class RRHH_AsignacionControl : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PanelBuscar.DefaultButton = btnBuscar.ID;
            Session["Usuario"] = Request.QueryString["usuario"].ToString();
            //Session["Usuario"] = Decrypt(HttpUtility.UrlDecode(Request.QueryString["usuario"]));
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            ConsultaUsuario();

        }
    }
    public string  ConsultaUsuario()
    {

        string result = string.Empty ;
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
                            result =reader.GetValue(i).ToString ();
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
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        if (cipherText.Length % 4 > 0)
            cipherText = cipherText.PadRight(cipherText.Length + 4 - cipherText.Length % 4, '=');
        //byte[] byteArray = Convert.FromBase64String(dummyData);

        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
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
            int FLG_ESTADO = Convert.ToInt32(dtResultado.Rows[0]["REQ_ESTADO"].ToString());
            if (FLG_ESTADO == 4)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Requerimiento Anulado";
                btnGuardar.Visible = false; 
            }
            {
                lblMensaje.Text = "";
                btnGuardar.Visible = true ; 
            }

            dtEncargados = obj.Datos_requerimiento_encargados(CodAsignacion, Session["Usuario"].ToString());

            if (dtEncargados.Rows.Count > 0)
            {
                DataListRecursos.DataSource = dtEncargados;
                DataListRecursos.DataBind();
                LlenarEstados();
            }

        }
        else
        {
            PanelTicket.Visible = false;
            cleanMessage = "No existe el Numero de Atención ingresado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
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
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        BL_ASIGNACION_RECURSOS obj = new BL_ASIGNACION_RECURSOS();
        DataTable dtResultado = new DataTable();
        foreach (DataListItem FilaFactor in DataListRecursos.Items)
        {

            int id = (int)DataListRecursos.DataKeys[FilaFactor.ItemIndex];
            RadioButtonList RadioEstados = ((RadioButtonList)FilaFactor.FindControl("RadioEstados"));
            obj.UPD_ASIGNACION_ESTADO(id, Session["Usuario"].ToString(), Convert.ToInt32  ( RadioEstados.SelectedValue));
        }
        cleanMessage = "Actualizacion Satifactoria";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        LlenarDatos();
    }
}