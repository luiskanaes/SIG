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


public partial class RRHH_DesempenioFijarObjetivoNuevo : System.Web.UI.Page
{
    string URL_DESEMPENIO = string.Empty;
    string IDE_DESEMPENIO = string.Empty;
    string FolderFotos = ConfigurationManager.AppSettings["FolderFotos"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            URL_DESEMPENIO = Session["URL_DESEMPENIO"].ToString();
            IDE_DESEMPENIO = Session["IDE_DESEMPENIO"].ToString();
            Familia();
            datosPersona();
            
        }
    }
    protected void ListarObjetivos(string dni)
    {
        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA(dni,Convert.ToInt32(Session["ANIO"]),"" );
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            lblPesos.Text = "0";
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
            
        }
    }
    protected void Familia()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlFamilia.DataSource = obj.ListarParametros("IDE_FAMILIA", "RRHH_DESEMPENIO_FICHA");
        ddlFamilia.DataTextField = "RESUMEN1";
        ddlFamilia.DataValueField = "ID_PARAMETRO";
        ddlFamilia.DataBind();

        ddlFamilia.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void datosPersona()
    {

        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_FICHA_ID(Convert.ToInt32 (Session["IDE_DESEMPENIO"].ToString()));
        if (dtResultado.Rows.Count > 0)
        {
            string foto = dtResultado.Rows[0]["FOTO"].ToString();
            if (foto == string.Empty)
            {
                imgFotos.ImageUrl = "~/imagenes/Foto_Fondo.png";
            }
            else
            {
                imgFotos.ImageUrl = FolderFotos + foto;
            }
            lblnombre.Text = dtResultado.Rows[0]["PERSONAL"].ToString();
            lblcargo.Text = dtResultado.Rows[0]["CARGO"].ToString();
            ddlFamilia.SelectedValue = dtResultado.Rows[0]["IDE_FAMILIA"].ToString();
            Session["DNI"] = dtResultado.Rows[0]["DNI"].ToString();
            Session["ANIO"] = dtResultado.Rows[0]["ANIO"].ToString();

            ListarObjetivos(Session["DNI"].ToString ());
            //Session["GERENCIA"] = dtResultado.Rows[0]["GERENCIA"].ToString();
            //Session["IP_CENTRO"] = dtResultado.Rows[0]["IP_CENTRO"].ToString();
            //Session["CCENTRO"] = dtResultado.Rows[0]["CCENTRO"].ToString();
            //Session["CENTRO"] = dtResultado.Rows[0]["CENTRO"].ToString();
            //Session["DNI_JEFE"] = dtResultado.Rows[0]["DNI_JEFE"].ToString();
            //Session["DNI_GERENTE"] = dtResultado.Rows[0]["DNI_GERENTE"].ToString();

            //Session["GERENTE"] = dtResultado.Rows[0]["GERENTE"].ToString();
            //Session["JEFE"] = dtResultado.Rows[0]["JEFE"].ToString();
        }
    }
    public void CleanControl(ControlCollection controles)
    {
        lblCodigo.Text = string.Empty;
        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Text = string.Empty;
            //else if (control is DropDownList)
            //    ((DropDownList)control).ClearSelection();
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

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        
        CleanControl(this.Controls);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlFamilia.SelectedIndex < 1)
        {

            cleanMessage = "Seleccionar familia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }
       
        else if (txtObjetivos.Text  == string.Empty)
        {
            cleanMessage = "Ingresar objetivo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtIndicador.Text  == string.Empty)
        {
            cleanMessage = "Ingresar indicador";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtPeso.Text == string.Empty)
        {
            cleanMessage = "Ingresar peso";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtInicio.Text == string.Empty)
        {
            cleanMessage = "Ingresar fecha de inicio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtfin.Text == string.Empty)
        {
            cleanMessage = "Ingresar fecha de termino";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            DateTime inicio = Convert.ToDateTime(txtInicio.Text);
            DateTime fin = Convert.ToDateTime(txtfin.Text);

            if (inicio >= fin)
            {
                cleanMessage = "La fecha de inicio no puede ser mayor o igual a la fecha de termino";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            //else if (100 < Convert.ToDecimal(lblPesos.Text  ) + Convert.ToDecimal(txtPeso.Text))
            //{
            //    cleanMessage = "El peso total de los objetivos no debe exceder al 100%";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            else
            {
                //if (CheckAmpliar.Checked )
                //{
                //    if (txtAmpliarFecha.Text == string.Empty)
                //    {
                //        cleanMessage = "Ingresar fecha de ampliación";
                //        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                //    }
                   
                //    else
                //    {
                //        GuardarObjetivo();
                //    }
                //}
                //else
                //{

                //    GuardarObjetivo();
                //}

                GuardarObjetivo();
            }
        }
    }

    protected void GuardarObjetivo()
    {
        BE_RRHH_DESEMPENIO_OBJETIVOS oBESol = new BE_RRHH_DESEMPENIO_OBJETIVOS();
        oBESol.IDE_OBJETIVO = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        oBESol.IDE_DESEMPENIO = Convert.ToInt32(Session["IDE_DESEMPENIO"].ToString());
        oBESol.OBJETIVO = txtObjetivos.Text.Trim();
        oBESol.INDICADOR = txtIndicador.Text.Trim();
        oBESol.DNI_PERSONA = Session["DNI"].ToString();
        oBESol.PESO = Convert.ToDecimal(txtPeso.Text);

        oBESol.INICIO = txtInicio.Text;
        oBESol.TERMINO = txtfin.Text;
        oBESol.J_COMENTARIOS_JEFE = string.Empty;
        oBESol.J_USER_JEFE = Session["IDE_USUARIO"].ToString();
        oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
        oBESol.ANIO = Convert.ToInt32(Session["ANIO"].ToString());
        oBESol.FECHA_AMPLIACION = txtAmpliarFecha.Text;

        string estado = string.IsNullOrEmpty(rdoAprobar.SelectedValue.ToString()) ? "" : rdoAprobar.SelectedValue.ToString();
        oBESol.APROBAR  = estado;
        int dtrpta = 0;
        dtrpta = new BL_RRHH_DESEMPENIO_OBJETIVOS().uspINS_RRHH_DESEMPENIO_OBJETIVOS(oBESol);
        if (dtrpta > 0)
        {
            lblCodigo.Text = string.Empty;
            CleanControl(this.Controls);
           string  cleanMessage = "Registro exitoso";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
           
            ListarObjetivos(Session["DNI"].ToString());
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Session["URL_DESEMPENIO"].ToString());
    }

    private decimal _Total = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PESO"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "TOTAL:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = _Total.ToString();
                lblPesos.Text = _Total.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                if (_Total < 100 || _Total >100)
                {
                    e.Row.Cells[2].BackColor  = System.Drawing.Color.Red  ;
                }
                    else
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                }

            }
        }
        catch (Exception err)
        {
            string error = err.Message.ToString() + " - " + err.Source.ToString();
        }
    }
    protected void Update(object sender, ImageClickEventArgs e)
    {
        ModalObserva.Show();
       
        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_ID( Convert.ToInt32( pk));
        if (dtResultado.Rows.Count > 0)
        {
       
            btnGuardar.Focus();
            txtObjetivos.Text = dtResultado.Rows[0]["OBJETIVO"].ToString();
            lblCodigo.Text = dtResultado.Rows[0]["IDE_OBJETIVO"].ToString();
            txtIndicador.Text = dtResultado.Rows[0]["INDICADOR"].ToString();
            txtInicio.Text = dtResultado.Rows[0]["INICIO"].ToString();
            txtfin.Text = dtResultado.Rows[0]["TERMINO"].ToString();
            txtPeso.Text = dtResultado.Rows[0]["PESO"].ToString();
            txtAmpliarFecha.Text = dtResultado.Rows[0]["FECHA_AMPLIACION"].ToString();
            string AMPLIACION = dtResultado.Rows[0]["FLG_AMPLIACION"].ToString();
            if (txtAmpliarFecha.Text != string.Empty && AMPLIACION =="True")
            {
                PanelAmpliacion.Visible = true;
            }
            else
            {
                PanelAmpliacion.Visible = false ;
            }
        }
        

    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspDEL_RRHH_DESEMPENIO_OBJETIVOS_ID(Convert.ToInt32(pk));
        ListarObjetivos(Session["DNI"].ToString ());
        CleanControl(this.Controls);
    }

    protected void btnCorreo_Click(object sender, EventArgs e)
    {
        string cleanMessage =string.Empty ;
        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        if (txtComentarios.Text == string.Empty)
        {
             cleanMessage = "Ingresar comentarios";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            if (GridView1.Rows.Count == 0)
            {
                cleanMessage = "No existen objetivos a notificar";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                if (Convert.ToDecimal(lblPesos.Text) < 100 || Convert.ToDecimal(lblPesos.Text) > 100)
                {
                    cleanMessage = "El peso total de objetivo(s) debe ser igual a 100%";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else
                {
                    DataTable dtResultado = new DataTable();
                    dtResultado = obj.SP_EnviarCorreo_objetivoNuevo(Session["DNI"].ToString(), Convert.ToInt32(Session["ANIO"]), txtComentarios.Text.Trim());
                    if (dtResultado.Rows.Count > 0)
                    {
                        txtComentarios.Text = string.Empty;
                        cleanMessage = "Envio satisfactorio";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }
            }
        }
    }
    protected void VerChat(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
      
      

        ModalRegistro.Show();

        BL_RRHH_DESEMPENIO_OBJETIVOS_MSG obj = new BL_RRHH_DESEMPENIO_OBJETIVOS_MSG();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG(Convert.ToInt32(pk));
        lblCodigo.Text = pk;
        DataListChat.DataSource = dtResultado;
        DataListChat.DataBind();
        //dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG_POR_ID(Convert.ToInt32(pk));
        //if (dtResultado.Rows.Count > 0)
        //{
        //    //txtChat.Text = dtResultado.Rows[0]["MENSAJE"].ToString();
        //    lblCodigo.Text = pk;
        //}
        //else
        //{
        //    lblCodigo.Text = string.Empty;
        //    //txtChat.Text = string.Empty;
        //    txtRespuesta.Text = string.Empty;
        //}

    }

    protected void btnEnvia_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txtRespuesta.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar comentarios";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_RRHH_DESEMPENIO_OBJETIVOS_MSG oBESol = new BE_RRHH_DESEMPENIO_OBJETIVOS_MSG();
            oBESol.IDE_MSG = 0;// Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.DNI_USER = Session["IDE_USUARIO"].ToString();
            oBESol.IDE_OBJETIVO = Convert.ToInt32(lblCodigo.Text);
            oBESol.TIPO = "RESPUESTA";
            oBESol.ANIO = Convert.ToInt32(Session["ANIO"]);
            oBESol.ASUNTO = String.Empty;
            oBESol.COMENTARIO = txtRespuesta.Text.Trim();
            int dtrpta = 0;
            dtrpta = new BL_RRHH_DESEMPENIO_OBJETIVOS_MSG().uspINS_RRHH_DESEMPENIO_OBJETIVOS_MSG(oBESol);
            if (dtrpta > 0)
            {
                lblCodigo.Text = string.Empty;
                txtRespuesta.Text = String.Empty;
                cleanMessage = "Envio satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
               

            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblCodigo.Text = string.Empty;
        //txtChat.Text = string.Empty;
        txtRespuesta.Text = string.Empty;
       
    }

 

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        PanelAmpliacion.Visible = false;
    
        ModalObserva.Show();
    }
}