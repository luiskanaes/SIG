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
using System.Text.RegularExpressions;

public partial class RRHH_FormativoFicha : System.Web.UI.Page
{
    string FolderTrainee = ConfigurationManager.AppSettings["FolderTrainee"];
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {
            ParametrosPrograma();
            ParametrosCarrera();
            ParametrosUniversidad();
            ParametrosNivel();
            ParametrosCivil();
            ParametrosSegmentacion();
            ParametrosEmpresa();
            if (Session["FICHA"] != null)
            {
                lblCodigoFicha.Text = Session["FICHA"].ToString();
                datosFicha(Convert.ToInt32(lblCodigoFicha.Text));
                ListarFases();
            }
        }
       
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ParametrosEmpresa()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();

        ddlEmpresa.DataSource = obj.USP_EMPRESAS();
        ddlEmpresa.DataTextField = "DES_ABREV";
        ddlEmpresa.DataValueField = "ID_EMPRESA";
        ddlEmpresa.DataBind();

        ddlEmpresa.Items.Insert(0, new ListItem("--- Seleccionar empresa---", ""));
    }
    protected void ParametrosCecos()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();
        try
        {
            dtResultado = obj.uspSEL_CECOS_RRHH(Convert.ToInt32(ddlEmpresa.SelectedValue));
            if (dtResultado.Rows.Count > 0)
            {
                ddlProyecto.DataSource = dtResultado;
                ddlProyecto.DataTextField = "CENTRO_COSTO";
                ddlProyecto.DataValueField = "COD_CECOS";
                ddlProyecto.DataBind();

                ddlProyecto.Items.Insert(0, new ListItem("--- Seleccionar centro costo ---", ""));
            }
            else
            {
                ddlProyecto.Items.Clear();
                ddlProyecto.Items.Insert(0, new ListItem("--- Seleccionar centro costo ---", ""));
            }
        }
        catch (Exception ex)
        {
            ddlProyecto.Items.Clear();
            ddlProyecto.Items.Insert(0, new ListItem("--- Seleccionar centro costo ---", ""));
        }
    }
    protected void ParametrosPrograma()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlPrograma.DataSource = obj.ListarParametros("CATEGORIA", "RRHH_SOLICITUD_TALENTO");
        ddlPrograma.DataTextField = "DES_ASUNTO";
        ddlPrograma.DataValueField = "ID_PARAMETRO";
        ddlPrograma.DataBind();

        ddlPrograma.Items.Insert(0, new ListItem("--- Seleccionar programa---", ""));
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
    protected void ParametrosUniversidad()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddluniversidad.DataSource = obj.ListarParametros_orden("UNIVERSIDAD", "RRHH_FORMATIVO_FICHA");
        ddluniversidad.DataTextField = "DES_ASUNTO";
        ddluniversidad.DataValueField = "ID_PARAMETRO";
        ddluniversidad.DataBind();

        ddluniversidad.Items.Insert(0, new ListItem("--- Seleccionar universidad---", ""));
    }
    protected void ParametrosNivel()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlNivel.DataSource = obj.ListarParametros_orden("NIVEL", "RRHH_FORMATIVO_FICHA");
        ddlNivel.DataTextField = "DES_ASUNTO";
        ddlNivel.DataValueField = "ID_PARAMETRO";
        ddlNivel.DataBind();

        ddlNivel.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
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
    protected void ParametrosSegmentacion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlsegmentacion.DataSource = obj.ListarParametros_orden("SEGMENTACION", "RRHH_FORMATIVO_FICHA");
        ddlsegmentacion.DataTextField = "DES_ASUNTO";
        ddlsegmentacion.DataValueField = "ID_PARAMETRO";
        ddlsegmentacion.DataBind();

        ddlsegmentacion.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
       
        if (lblCodigoFicha.Text != string.Empty)
        {
            capturarDatos();
        }
        else
        {
            capturarDatos();
        }
    }
    protected void capturarDatos()
    {
        string cleanMessage = string.Empty;
        if (txtApellidos.Text == string.Empty)
        {
            cleanMessage = "Ingresar apellidos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtNombres.Text == string.Empty)
        {
            cleanMessage = "Ingresar nombres";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtDni.Text == string.Empty)
        {
            cleanMessage = "Ingresar número de DNI";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtFecNacimiento.Text == string.Empty)
        {
            cleanMessage = "Ingresar fecha de nacimiento";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlPrograma.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar programa";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlcarrera.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar carrera";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddluniversidad.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar universidad";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlNivel.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar segmentación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        //else if (txtcolegiatura.Text == string.Empty)
        //{
        //    cleanMessage = "Ingresar número de colegiatura";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
        else if (txtdireccion.Text == string.Empty)
        {
            cleanMessage = "Ingresar dirección";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlcivil.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar estado civil";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtTutor.Text == string.Empty)
        {
            cleanMessage = "Ingresar tutor";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtTutorCorreo.Text == string.Empty)
        {
            cleanMessage = "Ingresar correo tutor";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        
        else if (txtCargo.Text == string.Empty)
        {
            cleanMessage = "Ingresar cargo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlsegmentacion.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar segmentación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtFin.Text == string.Empty)
        {
            cleanMessage = "Ingresar fecha fin de contrato";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtFortalezas.Text == string.Empty)
        {
            cleanMessage = "Ingresar fortalezas";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtoportunidades.Text == string.Empty)
        {
            cleanMessage = "Ingresar oportunidades de mejora";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtCorreo.Text == string.Empty)
        {
            cleanMessage = "Ingresar correo institucional";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            Boolean correo = email_bien_escrito(txtCorreo.Text);

            if (correo == true)
            {

                BE_RRHH_FORMATIVO_FICHA obj = new BE_RRHH_FORMATIVO_FICHA();
                obj.IDE_FICHA = Convert.ToInt32(string.IsNullOrEmpty(lblCodigoFicha.Text) ? "0" : lblCodigoFicha.Text);
                obj.APELLIDOS = txtApellidos.Text;
                obj.NOMBRES = txtNombres.Text;
                obj.DNI = txtDni.Text;
                obj.FECHA_NACIMIENTO = txtFecNacimiento.Text;
                obj.IDE_PROGRAMA = Convert.ToInt32(ddlPrograma.SelectedValue);
                obj.IDE_CARRERA = Convert.ToInt32(ddlcarrera.SelectedValue);
                obj.IDE_UNIVERSIDAD = Convert.ToInt32(ddluniversidad.SelectedValue);
                obj.IDE_NIVEL = Convert.ToInt32(ddlNivel.SelectedValue);
                obj.NUM_COLEGIATURA = txtcolegiatura.Text;
                obj.RESIDENCIA = txtdireccion.Text.Trim();
                obj.ESTADO_CIVIL = Convert.ToInt32(ddlcivil.SelectedValue);
                obj.TUTOR = txtTutor.Text;
                obj.TUTOR_CORREO = txtTutorCorreo.Text.Trim();
                obj.CARGO_TUTOR = txtCargo.Text;
                obj.INT_SEGMENTACION = Convert.ToInt32(ddlsegmentacion.SelectedValue);
                obj.FIN_CONTRATO = txtFin.Text;
                obj.FORTALEZA = txtFortalezas.Text.Trim();
                obj.OPORTUNIDAD = txtoportunidades.Text.Trim();
                obj.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
                obj.CORREO = txtCorreo.Text.Trim();
                obj.TELEFONO = txtfono.Text.Trim(); 

                Boolean fileOK = false;
                String path = Server.MapPath(FolderTrainee);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (FileUpload1.HasFile)
                {
                    String fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }
                }

                if (fileOK)
                {
                    try
                    {

                        string archivo = txtDni.Text.Trim() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.PostedFile.SaveAs(path + archivo);
                        obj.FOTO_RUTA = path + archivo;
                        obj.FOTO = archivo;
                    }
                    catch (Exception ex)
                    {

                        string mensajeUpl = "File could not be uploaded.";
                    }
                }
                else
                {
                    obj.FOTO_RUTA = string.Empty;
                    obj.FOTO = string.Empty;
                }


                int rpta = 0;
                rpta = new BL_RRHH_FORMATIVO_FICHA().uspINS_RRHH_FORMATIVO_FICHA(obj);
                if (rpta > 0)
                {
                    lblCodigoFicha.Text = rpta.ToString();
                    datosFicha(Convert.ToInt32(lblCodigoFicha.Text));
                    cleanMessage = "registro satisfactorio";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                cleanMessage = "Correo no permitido, verificar";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    protected void datosFicha(int id)
    {
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_FORMATIVO_FICHA_ID(id);
        if (dtResultado.Rows.Count > 0)
        {
            
            txtApellidos.Text = dtResultado.Rows[0]["APELLIDOS"].ToString();
            txtNombres.Text = dtResultado.Rows[0]["NOMBRES"].ToString();
            txtDni.Text = dtResultado.Rows[0]["DNI"].ToString();
            txtFecNacimiento.Text = dtResultado.Rows[0]["FECHA_NACIMIENTO"].ToString();
            ddlPrograma.SelectedValue = dtResultado.Rows[0]["IDE_PROGRAMA"].ToString();
            ddlcarrera.SelectedValue = dtResultado.Rows[0]["IDE_CARRERA"].ToString();
            ddluniversidad.SelectedValue = dtResultado.Rows[0]["IDE_UNIVERSIDAD"].ToString();
            ddlNivel.SelectedValue  = dtResultado.Rows[0]["IDE_NIVEL"].ToString();
            txtcolegiatura.Text = dtResultado.Rows[0]["NUM_COLEGIATURA"].ToString();
            txtdireccion.Text = dtResultado.Rows[0]["RESIDENCIA"].ToString();
            ddlcivil.SelectedValue  = dtResultado.Rows[0]["ESTADO_CIVIL"].ToString();
            txtTutor.Text = dtResultado.Rows[0]["TUTOR"].ToString();
            txtCargo.Text = dtResultado.Rows[0]["CARGO_TUTOR"].ToString();
            ddlsegmentacion.SelectedValue  = dtResultado.Rows[0]["INT_SEGMENTACION"].ToString();
            txtFin.Text = dtResultado.Rows[0]["FIN_CONTRATO"].ToString();
            txtFortalezas.Text = dtResultado.Rows[0]["FORTALEZA"].ToString();
            txtoportunidades.Text = dtResultado.Rows[0]["OPORTUNIDAD"].ToString();
            txtedad.Text = dtResultado.Rows[0]["EDAD"].ToString();
            string foto = dtResultado.Rows[0]["FOTO"].ToString();  
            txtCorreo.Text = dtResultado.Rows[0]["CORREO"].ToString();
            txtTutorCorreo.Text = dtResultado.Rows[0]["TUTOR_CORREO"].ToString();
            txtfono.Text = dtResultado.Rows[0]["TELEFONO"].ToString();
            if (foto == string.Empty )
            {
                imgFotos.ImageUrl = "~/imagenes/Foto_Fondo.png";
            }
            else
            {
                imgFotos.ImageUrl = FolderTrainee + foto;
            }
            ListarPlan();

        }
    }

    protected void btnPlanes_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (lblCodigoFicha.Text == string.Empty )
        {
            capturarDatos();
            //cleanMessage = "Falta registrar ficha";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            if (txtDuracion.Text == string.Empty )
            {
                cleanMessage = "Ingresar periodo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtDescripcion.Text == string.Empty)
            {
                cleanMessage = "Ingresar descripción";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                
                capturarDatos();

                if (lblCodigoFicha.Text != string.Empty)
                {
                    BE_RRHH_FORMATIVO_PLANES obj = new BE_RRHH_FORMATIVO_PLANES();
                    obj.IDE_PLANES = 0;
                    obj.DURACION = txtDuracion.Text;
                    obj.DESCRIPCION = txtDescripcion.Text;
                    obj.IDE_FICHA = Convert.ToInt32(lblCodigoFicha.Text);
                    obj.USER_REGISTRO = Session["IDE_USUARIO"].ToString();

                    int rpta = 0;
                    rpta = new BL_RRHH_FORMATIVO_FICHA().uspINS_RRHH_FORMATIVO_PLANES(obj);
                    if (rpta > 0)
                    {
                        ListarPlan();
                        cleanMessage = "registro satisfactorio";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }
                else
                {
                    cleanMessage = "Falta registrar ficha";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }

            }
        }
    }
    protected void Eliminar_Plan(object sender, ImageClickEventArgs e)
    {
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        ImageButton btnEliminar = ((ImageButton)sender);
        dt = obj.uspDEL_RRHH_FORMATIVO_PLANES_POR_ID(Convert.ToInt32(btnEliminar.CommandArgument));
        ListarPlan();
       
    }
    protected void ListarPlan()
    {

        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_RRHH_FORMATIVO_PLANES_POR_FICHA(Convert.ToInt32(lblCodigoFicha.Text));
        if (dt.Rows.Count > 0)
        {
            gridPlan.Visible = true;
            gridPlan.DataSource = dt;
            gridPlan.DataBind();
        }
        else
        {
            gridPlan.Visible = false;
            gridPlan.DataSource = dt;
            gridPlan.DataBind();
        }
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ParametrosCecos();
    }

    protected void btnFases_Click(object sender, EventArgs e)
    {
        int IDE_FASE = 0;
        if (lblIDE_FASE.Text == string.Empty )
        {
            IDE_FASE = 0;
        }
        else
        {
            IDE_FASE = Convert.ToInt32(lblIDE_FASE.Text);
        }
        GuardarFase(IDE_FASE);
    }
    protected void GuardarFase(int IDE_FASE)
    {
        string cleanMessage = string.Empty;
        if (lblCodigoFicha.Text == string.Empty)
        {
            capturarDatos();
            //cleanMessage = "Falta registrar ficha";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            if (ddlEmpresa.SelectedIndex < 1)
            {
                cleanMessage = "Seleccionar empresa";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (ddlProyecto.SelectedIndex < 1)
            {
                cleanMessage = "Seleccionar centro costo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtInicio.Text == string.Empty)
            {
                cleanMessage = "Ingresar fecha de inicio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtTermino.Text == string.Empty)
            {
                cleanMessage = "Ingresar fecha final";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtUbicacion.Text == string.Empty)
            {
                cleanMessage = "Ingresar ubicación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtCargoPracticante.Text == string.Empty)
            {
                cleanMessage = "Ingresar cargo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtJefe.Text == string.Empty)
            {
                cleanMessage = "Ingresar jefe directo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtCorreoJefe.Text == string.Empty)
            {
                cleanMessage = "Ingresar correo jefe";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                capturarDatos();

                if (lblCodigoFicha.Text != string.Empty)
                {
                    BE_RRHH_FORMATIVO_FASE obj = new BE_RRHH_FORMATIVO_FASE();
                    obj.IDE_FASE = IDE_FASE;
                    obj.PROYECTO = ddlProyecto.SelectedValue.ToString();
                    obj.JEFE = txtJefe.Text;
                    obj.FECHA_INICIO = txtInicio.Text;
                    obj.FECHA_FIN = txtTermino.Text;
                    obj.UBICACION = txtUbicacion.Text;
                    obj.CARGO = txtCargoPracticante.Text;
                    obj.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
                    obj.IDE_FICHA = Convert.ToInt32(lblCodigoFicha.Text);
                    obj.OBSERVACIONES = txtObjetivos.Text;
                    obj.CORREO_JEFE = txtCorreoJefe.Text.Trim();
                    obj.ID_EMPRESA = Convert.ToInt32(ddlEmpresa.SelectedValue);
                    int rpta = 0;
                    rpta = new BL_RRHH_FORMATIVO_FICHA().uspINS_RRHH_FORMATIVO_FASE(obj);
                    if (rpta > 0)
                    {
                        lblIDE_FASE.Text = string.Empty;
                        txtJefe.Text = string.Empty;
                        txtInicio.Text = string.Empty;
                        txtTermino.Text = string.Empty;
                        txtUbicacion.Text = string.Empty;
                        txtCargoPracticante.Text = string.Empty;
                        txtObjetivos.Text = string.Empty;
                        txtCorreoJefe.Text = string.Empty;
                        ListarFases();
                        cleanMessage = "Registro satisfactorio";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }
            }
        }
    }
    protected void ListarFases()
    {
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_RRHH_FORMATIVO_FASE_POR_FICHA(Convert.ToInt32(lblCodigoFicha.Text ));
        if (dt.Rows.Count > 0)
        {
            ListView1.Visible = true;
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
        else
        {
            ListView1.Visible = false ;

        }
    }
    protected void EliminarFase(object sender, EventArgs e)
    {

        ImageButton  btnEliminarFase = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEliminarFase.CommandArgument);
        ListViewItem CommentItem = btnEliminarFase.NamingContainer as ListViewItem;
        //decimal pto = (Decimal)ListView1.DataKeys[CommentItem.DisplayIndex].Values["Puntos"];
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspDEL_RRHH_FORMATIVO_FASE_POR_ID(Convert.ToInt32 (btnEliminarFase.CommandArgument ));
        if (dtResultado.Rows.Count > 0)
        {
            ListarFases();
        }
        else
        {
           String  cleanMessage = "Fase contiene examenes desarrollados";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
       
    }
    private Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    protected void EnviarExamen_MitadDesempenio(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnDesempenioCM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioCM.CommandArgument);
     
        ListViewItem CommentItem = btnDesempenioCM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN ();
        DataTable dt = new DataTable();
   
//Ev.Seguimiento(Mitad) 2
        dt = Obj.USP_CORREO_EXAMEN_FORMATIVO(2, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0 )
        {
            cleanMessage = "Envio de correo satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            cleanMessage = "Existen incosistencias para el envio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void EnviarExamen_MitadSeguimiento(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnDesempenioCM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioCM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioCM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();
       
        //Ev.Desempeño(Mitad)   1
        dt = Obj.USP_CORREO_EXAMEN_FORMATIVO(1, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            cleanMessage = "Envio de correo satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            cleanMessage = "Existen incosistencias para el envio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void EnviarExamen_FinalDesempenio(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnDesempenioCM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioCM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioCM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();

        
        //Ev.Seguimiento(Final) 4
        dt = Obj.USP_CORREO_EXAMEN_FORMATIVO(4, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            cleanMessage = "Envio de correo satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            cleanMessage = "Existen incosistencias para el envio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void EnviarExamen_FinalSeguimiento(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnDesempenioCM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioCM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioCM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();


//Ev.Desempeño(Final)   3
        dt = Obj.USP_CORREO_EXAMEN_FORMATIVO(3, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            cleanMessage = "Envio de correo satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            cleanMessage = "Existen incosistencias para el envio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_MitadDesempenio(object sender, EventArgs e)
    {

        ImageButton btnDesempenioM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();

       
//Ev.Seguimiento(Mitad) 2
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(2, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_MitadSeguimiento(object sender, EventArgs e)
    {

        ImageButton btnDesempenioM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();

     
//Ev.Desempeño(Mitad)   1
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(1, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_FinalDesempenio(object sender, EventArgs e)
    {

        ImageButton btnDesempenioM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();

        //Ev.Seguimiento(Final) 4
        dt = Obj.USP_VER_EXAMEN_FORMATIVO(4, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void View_FinalSeguimiento(object sender, EventArgs e)
    {

        ImageButton btnDesempenioM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();
 
        //Ev.Desempeño(Final)   3

        dt = Obj.USP_VER_EXAMEN_FORMATIVO(3, IDE_FASE, IDE_FICHA);
        if (dt.Rows.Count > 0)
        {
            Session["IDE_FASE"] = IDE_FASE;
            Session["IDE_FICHA"] = IDE_FICHA;
            Session["EVALUADOR"] = dt.Rows[0]["DNI_EVALUADOR"].ToString();
            Session["IDE_EXAMEN"] = dt.Rows[0]["IDE_TIPO_EXA"].ToString();
            Response.Redirect("~/RRHH/FormativoExamenView.aspx");
        }
        else
        {
            string cleanMessage = "Examen pendiente de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void Abrir_ExamenMitad(object sender, EventArgs e)
    {

        ImageButton btnAbrirMitad = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnAbrirMitad.CommandArgument);

        ListViewItem CommentItem = btnAbrirMitad.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_FICHA Obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = Obj.uspSEL_RRHH_FORMATIVO_FASE_CONTROL( IDE_FASE, 1, 1);
        ListarFases();
    }
    protected void Cerrar_ExamenMitad(object sender, EventArgs e)
    {

        ImageButton btnCerrarMitad = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnCerrarMitad.CommandArgument);

        ListViewItem CommentItem = btnCerrarMitad.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_FICHA Obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = Obj.uspSEL_RRHH_FORMATIVO_FASE_CONTROL(IDE_FASE, 1, 0);
        ListarFases();
    }
    protected void Abrir_ExamenFinal(object sender, EventArgs e)
    {

        ImageButton btnAbrirFinal = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnAbrirFinal.CommandArgument);

        ListViewItem CommentItem = btnAbrirFinal.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_FICHA Obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = Obj.uspSEL_RRHH_FORMATIVO_FASE_CONTROL(IDE_FASE, 2, 1);
        ListarFases();
    }
    protected void Cerrar_ExamenFinal(object sender, EventArgs e)
    {

        ImageButton btnCerrarFinal = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnCerrarFinal.CommandArgument);

        ListViewItem CommentItem = btnCerrarFinal.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_FICHA Obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = Obj.uspSEL_RRHH_FORMATIVO_FASE_CONTROL(IDE_FASE, 2, 0);
        ListarFases();
    }
    protected void ActualizarFase(object sender, EventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEditar.CommandArgument);
        ListViewItem CommentItem = btnEditar.NamingContainer as ListViewItem;
        //decimal pto = (Decimal)ListView1.DataKeys[CommentItem.DisplayIndex].Values["Puntos"];
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_FORMATIVO_FASE_POR_ID(Convert.ToInt32(btnEditar.CommandArgument));
        if (dtResultado.Rows.Count > 0)
        {
            lblIDE_FASE.Text = dtResultado.Rows[0]["IDE_FASE"].ToString();
            ddlEmpresa.SelectedValue = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            ParametrosCecos();
            ddlProyecto.SelectedValue = dtResultado.Rows[0]["PROYECTO"].ToString();
            txtJefe.Text = dtResultado.Rows[0]["JEFE"].ToString();
            txtInicio.Text  = dtResultado.Rows[0]["FECHA_INICIO"].ToString();
            txtTermino.Text   = dtResultado.Rows[0]["FECHA_FIN"].ToString();
            txtUbicacion.Text  = dtResultado.Rows[0]["UBICACION"].ToString();
            txtCargoPracticante.Text  = dtResultado.Rows[0]["CARGO"].ToString();
       
            //IDE_FICHA = Convert.ToInt32(lblCodigoFicha.Text);
            txtObjetivos.Text   = dtResultado.Rows[0]["OBSERVACIONES"].ToString();
            txtCorreoJefe.Text = dtResultado.Rows[0]["CORREO_JEFE"].ToString();
            btnFases.Focus();
        }
        else
        {
            lblIDE_FASE.Text = string.Empty; 
        }

    }

    protected void btnCancelarfase_Click(object sender, EventArgs e)
    {
        lblIDE_FASE.Text = string.Empty;
        txtJefe.Text = string.Empty;
        txtInicio.Text = string.Empty;
        txtTermino.Text = string.Empty;
        txtUbicacion.Text = string.Empty;
        txtCargoPracticante.Text = string.Empty;
        txtObjetivos.Text = string.Empty;
        txtCorreoJefe.Text = string.Empty;
    }
}