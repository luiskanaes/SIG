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
public partial class RRHH_frmPersonal : System.Web.UI.Page
{
    public string ControlUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        //MaintainScrollPositionOnPostBack = true;
         if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        this.Master.RegisterTrigger(btnRegistrar);
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            ControlBotones();
            ParametrosCivil();
            TipoDocumento();
            cargos();
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
        this.ddlCargos.Items.Insert(0, new ListItem("--------TODOS--------", "0"));

    }

    protected void TipoDocumento()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlDocumento.DataSource = obj.ListarParametros("DOCUMENTO_IDENTIDAD", "RRHH_EMPLEADOS");
        ddlDocumento.DataTextField = "DES_ASUNTO";
        ddlDocumento.DataValueField = "ID_PARAMETRO";
        ddlDocumento.DataBind();
    }
    protected void ParametrosCivil()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCivil.DataSource = obj.ListarParametros("IDE_ESTADO_CIVIL", "RRHH_EMPLEADO");
        ddlCivil.DataTextField = "DES_ASUNTO";
        ddlCivil.DataValueField = "ID_PARAMETRO";
        ddlCivil.DataBind();
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
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        

        string cleanMessage = string.Empty;
        BE_PERSONAL oBEPersonal = new BE_PERSONAL();
        oBEPersonal = f_CapturarDatos();
        int rpta;
        rpta = new BL_PERSONAL().Mant_Insert_registroPersonal(oBEPersonal);
        if (rpta == 1)
        {
            cleanMessage = "Registro Exitoso";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            limpiar();
        }
        else
        {

            cleanMessage = "Actualización Exitosa";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            limpiar();
        }
    }
    private BE_PERSONAL f_CapturarDatos()
    {

        BE_PERSONAL oBEPersonal = new BE_PERSONAL();
        if (!(lblCodigoPersonal.Text).Equals(""))
        {
            oBEPersonal.i_IDE_EMPLEADO_E = Convert.ToInt32(lblCodigoPersonal.Text);
        }
        //else { oBEPersonal.i_IDE_EMPLEADO_E = null}
        oBEPersonal.v_DES_NOMBRE_E = txtNombre.Text;

        oBEPersonal.v_DES_APEPAT_E = txtApePat.Text;
        oBEPersonal.v_DES_APEMAT_E = txtApeMat.Text;

        oBEPersonal.f_FEC_FECHA_NACIMIENTO_E = txtNacimiento.Text;
      // oBEPersonal.f_FEC_FECHA_INGRESO_E = txtIngreso.Text;
        oBEPersonal.v_DES_TELEFONO_E = txtFono.Text;
        oBEPersonal.v_DES_CORREO_E = txtCorreo.Text;
        oBEPersonal.v_DES_DIRECCION_E = txtDireccion.Text;
        oBEPersonal.i_DES_ESTADO_CIVIL_E = Convert.ToInt32(ddlCivil.SelectedValue);
        oBEPersonal.v_DES_CARGO_E = Convert.ToInt32(ddlCargos.SelectedValue);

        oBEPersonal.v_DES_USUARIO_CREACION_E = Convert.ToString(Session["IDE_USUARIO"]);

        Byte[] bytes = null;
        string cleanMessage = string.Empty;
        if (FileUpload1.HasFile)
        {
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            bytes = br.ReadBytes((Int32)fs.Length);

            string sExt = string.Empty;
            sExt = Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (ValidaExtension(sExt))
            {
                oBEPersonal.b_foto_e = bytes;
            }
            else
            {
                cleanMessage = "No se Permite este Tipo de Formato";
               // limpiar();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else
        {
            oBEPersonal.b_foto_e = bytes;

        }
        oBEPersonal.i_DES_TIPO_DOCUMENTO_E = Convert.ToInt32(ddlDocumento.SelectedValue);
        oBEPersonal.v_DES_DNI_E = txtDNI.Text; 
      
        return oBEPersonal;
    }
   
    protected void limpiar()
    {
        txtDNI.Text = string.Empty;
        txtCorreo.Text = string.Empty;
        txtDireccion.Text = string.Empty;
        txtFono.Text = string.Empty;
      //  txtIngreso.Text = string.Empty;
        txtNacimiento.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtDNI.ReadOnly = false;
        txtDNI.Focus();
        ImgFoto.ImageUrl = "~/imagenes/Foto_Fondo.png";
        txtPersonal.Text = string.Empty;
        GridPersonal.Visible = false;
        lblCodigoPersonal.Text = "0";

        txtApeMat.Text = string.Empty;
        txtApePat.Text = string.Empty;
        ddlCargos.SelectedIndex = 0;
    }
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension)
        {
            case ".JPG":
            case ".PNG":
            case ".JPEG":
            case ".GIF":
            case ".BMP":
            case ".jpg":
            case ".png":
            case ".jpeg":
            case ".gif":
            case ".bmp":
                return true;
            default:
                return false;
        }
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
            SqlCommand cmd = new SqlCommand("USP_PERSONAL_BUSCAR_NOMBRE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("DES_NOMBRE", SqlDbType.VarChar, 100).Value = prefixText;
            //cmd.Parameters.Add("IDE_OBRA_OBA", SqlDbType.Int).Value = BL_Session.Area;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            // adapter.Fill(dataset);
            IDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(lector.GetString(1) + " - " + lector.GetString(0));
                
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
        string cleanMessage = string.Empty;
        if (txtPersonal.Text == string.Empty)
        {
            cleanMessage = "Ingresar Nombre a consultar";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dtResultado = new DataTable();
          
            //int i = 0; int j = 0;
            //char[] letras = dni.ToCharArray();
            //for (i = 0; i < dni.Length; i++)
            //{
            //    if (letras[i] == '-')
            //    {
            //        j = i;
            //        break;
            //    }
            //}
            dtResultado = obj.BuscarDNI_TC(txtPersonal.Text);
            if (dtResultado.Rows.Count > 0)
            {
                GridPersonal.Visible = true;
                GridPersonal.DataSource = dtResultado;
                GridPersonal.DataBind();
            }
            else
            {
                GridPersonal.Visible = false;
                GridPersonal.DataSource = null ;
                GridPersonal.DataBind();
            }

        }
    }

    protected void txtDNI_TextChanged(object sender, EventArgs e)
    {
        if (txtDNI.Text.Length == 8)
        {

            BuscaPersonalInicio(txtDNI.Text);
        }
        else if (txtDNI.Text.Length == 12)
        {
            BuscaPersonalInicio(txtDNI.Text);
        }


    }

    protected void SeleccionarPersonal(object sender, ImageClickEventArgs e)
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();

        ImageButton btnSeleccionar = ((ImageButton)sender);
        BuscaPersonal(btnSeleccionar.CommandArgument );
        txtPersonal.Text = string.Empty;
    }
    protected void btnLimpiador_Click(object sender, ImageClickEventArgs e)
    {
        limpiar();
        txtDNI.ReadOnly = false;
        txtDNI.Focus();
        ImgFoto.ImageUrl = "~/imagenes/Foto_Fondo.png";
    }
    protected void btnBuscador_Click(object sender, ImageClickEventArgs e)
    {
        BuscaPersonal(txtDNI.Text);
    }
    protected void BuscaPersonal(string Nombre)
    {
        string cleanMessage = string.Empty;
        if (Nombre == string.Empty)
        {
            cleanMessage = "Ingresar Numero de DNI";
            limpiar();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.BuscarDNI_TC(Nombre);
            if (dtResultado.Rows.Count > 0)
            {
                txtDNI.ReadOnly = true;
                lblCodigoPersonal.Text = dtResultado.Rows[0]["IDE_EMPLEADO"].ToString();
                txtDNI.Text = dtResultado.Rows[0]["DES_DNI"].ToString();
                txtNombre.Text = dtResultado.Rows[0]["DES_NOMBRE"].ToString();
                txtApePat.Text = dtResultado.Rows[0]["DES_APEPAT"].ToString();
                txtApeMat.Text = dtResultado.Rows[0]["DES_APEMAT"].ToString();
                txtFono.Text = dtResultado.Rows[0]["DES_TELEFONO"].ToString();
                txtCorreo.Text = dtResultado.Rows[0]["DES_CORREO"].ToString();
                txtDireccion.Text = dtResultado.Rows[0]["DES_DIRECCION"].ToString();
                txtNacimiento.Text = dtResultado.Rows[0]["FEC_FECHA_NACIMIENTO"].ToString();
              //  txtIngreso.Text = dtResultado.Rows[0]["FEC_FECHA_INGRESO"].ToString();
                ddlCivil.SelectedValue = dtResultado.Rows[0]["DES_ESTADO_CIVIL"].ToString();
                ddlCargos.SelectedValue = dtResultado.Rows[0]["DES_CARGO"].ToString();
                ParametrosCivil();
                ddlDocumento.SelectedValue = dtResultado.Rows[0]["DES_TIPO_DOCUMENTO"].ToString();
                TipoDocumento();
                if (dtResultado.Rows[0]["FOTO"] != DBNull.Value)
                {
                    byte[] imageBuffer = (byte[])dtResultado.Rows[0]["FOTO"];
                    if (imageBuffer != null)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        ImgFoto.ImageUrl = "~/HandlerFoto.ashx?ID=" + Nombre; // dtResultado.Rows[0]["icodpersonal"]; 
                    }

                }

            }
            else
            {
                cleanMessage = "No existe informacion sobre el DNI ingresado";
                limpiar();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }

    protected void BuscaPersonalInicio(string Nombre)
    {
        {
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.BuscarDNI(Nombre);
            if (dtResultado.Rows.Count > 0)
            {
                txtDNI.ReadOnly = true;
                lblCodigoPersonal.Text = dtResultado.Rows[0]["IDE_EMPLEADO"].ToString();
                txtDNI.Text = dtResultado.Rows[0]["DES_DNI"].ToString();
                txtNombre.Text = dtResultado.Rows[0]["DES_NOMBRE"].ToString();
                txtApePat.Text = dtResultado.Rows[0]["DES_APEPAT"].ToString();
                txtApeMat.Text = dtResultado.Rows[0]["DES_APEMAT"].ToString();
                txtFono.Text = dtResultado.Rows[0]["DES_TELEFONO"].ToString();
                txtCorreo.Text = dtResultado.Rows[0]["DES_CORREO"].ToString();
                txtDireccion.Text = dtResultado.Rows[0]["DES_DIRECCION"].ToString();
                txtNacimiento.Text = dtResultado.Rows[0]["FEC_FECHA_NACIMIENTO"].ToString();
                // txtIngreso.Text = dtResultado.Rows[0]["FEC_FECHA_INGRESO"].ToString();
                ddlCivil.SelectedValue = dtResultado.Rows[0]["DES_ESTADO_CIVIL"].ToString();
                ParametrosCivil();
                ddlDocumento.SelectedValue = dtResultado.Rows[0]["DES_TIPO_DOCUMENTO"].ToString();
                ddlCargos.SelectedValue = dtResultado.Rows[0]["DES_CARGO"].ToString();
                TipoDocumento();
                if (dtResultado.Rows[0]["FOTO"] != DBNull.Value)
                {
                    byte[] imageBuffer = (byte[])dtResultado.Rows[0]["FOTO"];
                    if (imageBuffer != null)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        ImgFoto.ImageUrl = "~/HandlerFoto.ashx?ID=" + Nombre; // dtResultado.Rows[0]["icodpersonal"]; 
                    }

                }

            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string   cleanMessage = "Registros Exitoso";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
       
    }
}