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
public partial class OPERACIONES_PERMISOS_VARIOS : System.Web.UI.Page
{
    public string ListaPersonal = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            privilegios();
            ParametrosMotivo();
            cargarEmpleados();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void privilegios()
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE MDP", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count < 1)
        {
            Response.Redirect("~/operaciones/PermisosMenu.aspx");

            //string cleanMessage = "No cuenta con permisos";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void ParametrosMotivo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlmotivo.DataSource = obj.USP_PARAMETRO_LISTAR_MDP("IDE_MOTIVO", "TBSOLICITUD_PERMISOS");
        ddlmotivo.DataTextField = "DES_ASUNTO";
        ddlmotivo.DataValueField = "ID_PARAMETRO";
        ddlmotivo.DataBind();


    }
    private void cargarEmpleados()
    {

        DataTable dtResultado = new DataTable();
        //BL_OPE_MINUTA obj = new BL_OPE_MINUTA();

        //dtResultado = obj.uspSEL_RRHH_PERSONAL_POR_CENTRO(BL_Session.CENTRO_COSTO);
        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        dtResultado = obj.uspSEL_EQUIPO_TRABAJO_SUPERVISOR_DNI(Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonalAcargo.DataSource = dtResultado;
            ddlPersonalAcargo.DataTextField = dtResultado.Columns["NOMBRE_COMPLETO"].ToString();
            ddlPersonalAcargo.DataValueField = dtResultado.Columns["DNI_TRABAJADOR"].ToString();
            ddlPersonalAcargo.DataBind();
          
        }
        else
        {
            ddlPersonalAcargo.Items.Insert(0, new ListItem("--- Seleccionar personal ---", ""));
        }
    }
    protected void chkEstados_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEstados.Checked == true)
        {
            foreach (ListItem item in ddlPersonalAcargo.Items)
            {
                item.Selected = true;

            }

        }

        if (chkEstados.Checked == false)
        {
            foreach (ListItem item in ddlPersonalAcargo.Items)
            {
                item.Selected = false;

            }
        }
    }
    public String dayOfWeek(DateTime? date)
    {
        return date.Value.ToString("dddd");

    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        

        string cleanMessage = string.Empty;
      
        if (txtInicio.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de inicio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtfin.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de termino";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {
            if (Convert.ToDateTime(txtInicio.Text) > Convert.ToDateTime(txtfin.Text))
            {
                cleanMessage = "La fecha de inicio no puede ser mayor a la fecha de termino";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                int cantidad = 0;
                foreach (ListItem li in ddlPersonalAcargo.Items)
                {
                    if (li.Selected)
                    {
                        string usuario = li.Value;

                        lblCodigo.Text = string.Empty;
                        BE_TBSOLICITUD_PERMISOS oBESol = new BE_TBSOLICITUD_PERMISOS();
                        oBESol.Ide_permiso = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
                        oBESol.Ide_usuario = usuario;
                        oBESol.Ide_motivo = Convert.ToInt32(ddlmotivo.SelectedValue);
                        oBESol.Inicio = txtInicio.Text.Trim();
                        oBESol.Fin = txtfin.Text.Trim();
                        oBESol.Comentario = txtcomentarios.Text.Trim();
                        oBESol.FILE = "";
                        oBESol.URL = "";
                        oBESol.NOMBRE_DIA = dayOfWeek(Convert.ToDateTime(txtInicio.Text.Trim()));
                        int dtrpta = 0;
                        dtrpta = new BL_TBSOLICITUD_PERMISOS().MANT_TBSOLICITUD_PERMISOS_INSERT_DATOS(oBESol);
                        if (dtrpta > 0)
                        {
                            cantidad++;
                            //BL_TBSOLICITUD_PERMISOS oB = new BL_TBSOLICITUD_PERMISOS();
                            //oB.correo_solicitud(dtrpta);
                        }
                        //lblpersonal.Text = 
                    }

                }


                if(cantidad>0)
                {
                    cleanMessage = "Registro exitoso";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                    txtcomentarios.Text = string.Empty;
                    txtfin.Text = string.Empty;
                    txtInicio.Text = string.Empty;
                }
                else
                {
                    cleanMessage = "Falta seleccionar personal";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
        }
    }

    protected void ddlPersonalAcargo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListaPersonal = string.Empty;
        foreach (ListItem li in ddlPersonalAcargo.Items)
        {
            if (li.Selected)
            {

                ListaPersonal += li.Text + "<br>";
                //ListaPersonal += li.Value.Replace(",", Environment.NewLine);
            }
        }
    }
}