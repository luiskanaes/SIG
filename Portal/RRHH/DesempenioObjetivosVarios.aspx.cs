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


public partial class RRHH_DesempenioObjetivosVarios : System.Web.UI.Page
{
    public string ListaPersonal = string.Empty;
    string URL_DESEMPENIO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            URL_DESEMPENIO = Session["URL_DESEMPENIO"].ToString();
            Listar();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Listar()
    {
        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_COLABORADORES(Session["IDE_USUARIO"].ToString(), Session["ANIO"].ToString(), 3);
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonalAcargo.DataSource = dtResultado;
            ddlPersonalAcargo.DataTextField = dtResultado.Columns["NOMBRE_COMPLETO"].ToString();
            ddlPersonalAcargo.DataValueField = dtResultado.Columns["DNI"].ToString();
            ddlPersonalAcargo.DataBind();

        }
        else
        {
            ddlPersonalAcargo.Items.Insert(0, new ListItem("--- Seleccionar personal ---", ""));
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Session["URL_DESEMPENIO"].ToString());
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        CleanControl(this.Controls);
        chkEstados.Checked = false;
        foreach (ListItem item in ddlPersonalAcargo.Items)
        {
            item.Selected = false;

        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txtObjetivos.Text == string.Empty)
        {
            cleanMessage = "Ingresar objetivo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtIndicador.Text == string.Empty)
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
          
            else
            {
                int cantidad = 0;
                foreach (ListItem li in ddlPersonalAcargo.Items)
                {
                    if (li.Selected)
                    {
                        string usuario = li.Value;

                        BE_RRHH_DESEMPENIO_OBJETIVOS oBESol = new BE_RRHH_DESEMPENIO_OBJETIVOS();
                        oBESol.IDE_OBJETIVO = 0;
                        oBESol.IDE_DESEMPENIO = Convert.ToInt32(Session["IDE_DESEMPENIO"].ToString());
                        oBESol.OBJETIVO = txtObjetivos.Text.Trim();
                        oBESol.INDICADOR = txtIndicador.Text.Trim();
                        oBESol.DNI_PERSONA = li.Value;
                        oBESol.PESO = Convert.ToDecimal(txtPeso.Text);

                        oBESol.INICIO = txtInicio.Text;
                        oBESol.TERMINO = txtfin.Text;
                        oBESol.J_COMENTARIOS_JEFE = string.Empty;
                        oBESol.J_USER_JEFE = Session["IDE_USUARIO"].ToString();
                        oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
                        oBESol.ANIO = Convert.ToInt32(Session["ANIO"].ToString());
                        oBESol.FECHA_AMPLIACION = string.Empty;
                        oBESol.APROBAR = string.Empty;
                        int dtrpta = 0;
                        dtrpta = new BL_RRHH_DESEMPENIO_OBJETIVOS().uspINS_RRHH_DESEMPENIO_OBJETIVOS(oBESol);
                        if (dtrpta > 0)
                        {
                            cantidad++;
                            //BL_TBSOLICITUD_PERMISOS oB = new BL_TBSOLICITUD_PERMISOS();
                            //oB.correo_solicitud(dtrpta);
                        }
                      
                    }

                }


                if (cantidad > 0)
                {
                    CleanControl(this.Controls);
                    cleanMessage = "Registro exitoso, los obejtivos fueron asignados correctamente";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                   
                }
                else
                {
                    cleanMessage = "Falta seleccionar personal";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }

                
            }
        }
    }
    public void CleanControl(ControlCollection controles)
    {
       
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
}