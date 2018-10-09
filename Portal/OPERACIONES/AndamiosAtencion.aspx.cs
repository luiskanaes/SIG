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
using System.Data.SqlClient;


public partial class OPERACIONES_AndamiosAtencion : System.Web.UI.Page
{
    String GERENCIA, CECOS_GESTOR, IDE_ANDAMIOS;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblCodigo.Text = Request.QueryString["IDE_ANDAMIOS"].ToString();
            CECOS_GESTOR = Request.QueryString["CECOS_GESTOR"].ToString();
            IDE_ANDAMIOS = Request.QueryString["IDE_ANDAMIOS"].ToString();
            SUPERVISOR();
            CAPATAZ();
            Listar();
        }

    }
    protected void Upnl_LoadOrigen(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlSupervisor"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlCapataz"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }

    protected void SUPERVISOR()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        CECOS_GESTOR = Request.QueryString["CECOS_GESTOR"].ToString();
        dtResultado = obj.uspSEL_RRHH_PERSONAL_GERENCIA_TIPO(CECOS_GESTOR, "01");


        if (dtResultado.Rows.Count > 0)
        {
            ddlSupervisor.DataSource = dtResultado;

            ddlSupervisor.DataTextField = "NOMBRE_COMPLETO";
            ddlSupervisor.DataValueField = "ID_DNI";
            ddlSupervisor.DataBind();

            ddlSupervisor.Items.Insert(0, new ListItem("---", ""));
        }

    }
    protected void CAPATAZ()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        CECOS_GESTOR = Request.QueryString["CECOS_GESTOR"].ToString();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_GERENCIA_TIPO(CECOS_GESTOR, "02");


        if (dtResultado.Rows.Count > 0)
        {
            ddlCapataz.DataSource = dtResultado;

            ddlCapataz.DataTextField = "NOMBRE_COMPLETO";
            ddlCapataz.DataValueField = "ID_DNI";
            ddlCapataz.DataBind();
            ddlCapataz.Items.Insert(0, new ListItem("---", ""));
        }

    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;


        //DateTime.Now.ToString("dd/MM/yyyy");

        string txtFechaDesmontaje = txtTermino.Text.ToString();
        string txtFechaEntrega = txtFecEntrega.Text;
        string txtFecDesmontaje1= txtDesmontaje.Text.ToString();

        if (txtTermino.Text.ToString()== string.Empty &&  txtFecEntrega.Text == string.Empty && txtDesmontaje.Text == string.Empty)
        {
            cleanMessage = "ingresar fecha de inicio de atención";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtTermino.Text != string.Empty && txtDesmontaje.Text == string.Empty)
        {
            cleanMessage = "ingresar fecha estimada de desmontaje";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtTermino.Text == string.Empty && txtDesmontaje.Text != string.Empty)
        {
            cleanMessage = "ingresar fecha termino de atención";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtTermino.Text != string.Empty && txtDesmontaje.Text != string.Empty && ddlEstados.SelectedIndex < 2)
        {
            if (Convert.ToDateTime(txtTermino.Text) > Convert.ToDateTime(txtDesmontaje.Text))
            {
                cleanMessage = "Fecha desmontaje no puede ser menor a la fecha de termino de atención.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                cleanMessage = "Seleccionar estado SOLICITUD TERMINADA";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
               
        }
        else if (txtTermino.Text == string.Empty && txtDesmontaje.Text == string.Empty && ddlEstados.SelectedIndex == 2)
        {
            cleanMessage = "ingresar fecha de termino de atención y fecha de desmontaje ";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            Guardar();
        }

        //if (Convert.ToDateTime(txtFechaEntrega.Text) <= Convert.ToDateTime(txtFechaDesmontaje.Text))
        //{

            //}
            //else

            //{


            //else
            //{
            //    cleanMessage = "Fecha desmontaje no puede ser menor a la fecha de entrega.";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            //}
    }
    protected void Guardar()
    {
        string cleanMessage = string.Empty;
        BE_SOL_ANDAMIOS oBESol = new BE_SOL_ANDAMIOS();
        oBESol.IDE_ANDAMIOS = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        oBESol.USUARIO_ATENCION = Session["IDE_USUARIO"].ToString();
        oBESol.SUPERVIDOR_DNI = ddlSupervisor.SelectedValue.ToString();
        oBESol.SUPERVIDOR = ddlSupervisor.SelectedItem.ToString();
        oBESol.CAPATAZ_DNI = ddlCapataz.SelectedValue.ToString();
        oBESol.CAPATAZ = ddlCapataz.SelectedItem.ToString();
        oBESol.DURACION = Convert.ToInt32(string.IsNullOrEmpty(txtDuracion.Text) ? "0" : txtDuracion.Text);
        oBESol.HORAS = Convert.ToDecimal(string.IsNullOrEmpty(txtHoras.Text) ? "0" : txtHoras.Text);
        oBESol.FECHA_ENTREGA = txtFecEntrega.Text.ToString();
        oBESol.FECHA_TERMINO = txtTermino.Text.ToString();
        oBESol.FECHA_DESMONTAJE = txtDesmontaje.Text.ToString();
        oBESol.OBSERVACIONES = txtObservacion.Text;
        oBESol.ESTADO = Convert.ToInt32(ddlEstados.SelectedValue.ToString());
        int dtrpta = 0;
        dtrpta = new BL_SOL_ANDAMIOS().uspUPD_SOL_ANDAMIOS(oBESol);
        if (dtrpta > 0)
        {
            int estado = Convert.ToInt32(ddlEstados.SelectedValue.ToString());
            if (estado == 4 || estado == 5)
            {
                BL_SOL_ANDAMIOS objCorreo = new BL_SOL_ANDAMIOS();
                objCorreo.SP_ENVIARCORREO_SOL_ANDAMIOS_RPTA(Convert.ToInt32(lblCodigo.Text));
            }

            Listar();
            cleanMessage = "Registro exitoso.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


        }
    }
    protected void Listar()
    {
        BL_SOL_ANDAMIOS Obj = new BL_SOL_ANDAMIOS();
        DataTable dtResultado = new DataTable();
        dtResultado = Obj.uspSEL_SOL_ANDAMIOS_IDE(Convert.ToInt32(lblCodigo.Text));
        if (dtResultado.Rows.Count > 0)
        {
            

            txtDuracion.Text = dtResultado.Rows[0]["DURACION"].ToString();
            ddlCapataz.SelectedValue = dtResultado.Rows[0]["CAPATAZ_DNI"].ToString();
            ddlSupervisor.SelectedValue = dtResultado.Rows[0]["SUPERVIDOR_DNI"].ToString();
            txtRequerimiento.Text = dtResultado.Rows[0]["FECHA_REQUERIDA"].ToString();
            txtFecEntrega.Text  = dtResultado.Rows[0]["FECHA_ENTREGA"].ToString();
            txtTermino.Text = dtResultado.Rows[0]["FECHA_TERMINO"].ToString();
            txtDesmontaje.Text = dtResultado.Rows[0]["FECHA_DESMONTAJE"].ToString();
            txtObservacion.Text = dtResultado.Rows[0]["OBSERVACIONES"].ToString();
            txtHoras.Text = dtResultado.Rows[0]["HORAS"].ToString();
            string estado = dtResultado.Rows[0]["FLG_VISIBLE"].ToString();
            ddlEstados.SelectedValue = dtResultado.Rows[0]["ESTADO"].ToString();
            lblTicket.Text = dtResultado.Rows[0]["TICKET"].ToString();

            if (estado == "4" || estado == "5")
                {
                   
                    btn.Visible = false ;
                }
                else
                {
                    btn.Visible = true ;
                }


           
        }
    }
}