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
public partial class RRHH_FormativoProyectoView : System.Web.UI.Page
{
    string IDE_FORMATIVO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {
            IDE_FORMATIVO = Session["IDE_FORMATIVO"].ToString();
            BL_RRHH_SOL_FORMATIVO Obj = new BL_RRHH_SOL_FORMATIVO();
            DataTable dtResultado = new DataTable();

            dtResultado = Obj.uspSEL_RRHH_SOL_FORMATIVO_POR_ID(Convert.ToInt32(Session["IDE_FORMATIVO"].ToString()));
            if (dtResultado.Rows.Count > 0)
            {
                lblCodigo.Text = dtResultado.Rows[0]["IDE_FORMATIVO"].ToString();

                ListarHito();
                ListarStakeholder();

                lblIdCargo.Text = dtResultado.Rows[0]["ESP_FORMACION"].ToString();
                txtProyecto.Text = dtResultado.Rows[0]["NOMBRE_PROYECTO"].ToString();

                //txtDuracion.Text = dtResultado.Rows[0]["NOMBRE_PROYECTO"].ToString();

                txtCargo.Text = dtResultado.Rows[0]["FORMACION"].ToString();
                txtCentro.Text = dtResultado.Rows[0]["CENTRO_COSTO"].ToString();

                txtDuracion.Text = dtResultado.Rows[0]["DURACION"].ToString();
                txtCantidad.Text = dtResultado.Rows[0]["CANTIDAD"].ToString();
                txtCentro.Text = dtResultado.Rows[0]["CENTRO_COSTO"].ToString();
                txtNombreCeco.Text = dtResultado.Rows[0]["NOMBRE_PROYECTO"].ToString();
                txtUbicacion.Text = dtResultado.Rows[0]["UBICACION"].ToString();
                txtSupervisor.Text = dtResultado.Rows[0]["SUPERVISOR_DIRECTO"].ToString();
                txtObjetivo.Text = dtResultado.Rows[0]["B_OBJETIVOS"].ToString();
                txtEntregable.Text = dtResultado.Rows[0]["B_ENTREGABLES"].ToString();
                txtIndicadores.Text = dtResultado.Rows[0]["B_INDICADORES"].ToString();
                txtIncluye.Text = dtResultado.Rows[0]["C_INCLUYE"].ToString();
                txtNoincluye.Text = dtResultado.Rows[0]["C_NO_INCLUYE"].ToString();

                txtRiesgo.Text = dtResultado.Rows[0]["C_RIESGO"].ToString();
                txtRestricciones.Text = dtResultado.Rows[0]["C_RESTRICCIONES"].ToString();
                txtRequisitos.Text = dtResultado.Rows[0]["D_REQUISITOS"].ToString();

                txtCosto.Text = dtResultado.Rows[0]["D_COSTO"].ToString();
                txtBeneficios.Text = dtResultado.Rows[0]["D_BENEFICIOS"].ToString();

                txtComentarioRRHH.Text = dtResultado.Rows[0]["COMENTARIOS_GENERAL"].ToString();
                txtComentarioArea.Text = dtResultado.Rows[0]["COMENTARIOS_AREA"].ToString();
                txtComentarioRRHH.Text = dtResultado.Rows[0]["COMENTARIOS_RRHH"].ToString();
                txtRechazo.Text = dtResultado.Rows[0]["COMENTARIO_RECHAZO"].ToString();

                try
                {
                    lblComentarioGeneral.Text = " Comentario Generalista : " + dtResultado.Rows[0]["PERSONA_GN"].ToString() + ' ' + dtResultado.Rows[0]["FECHA_GENERALISTA"].ToString();
                    lblComentarioArea.Text = " Comentario Gerente Area : " + dtResultado.Rows[0]["PERSONA_AREA"].ToString() + ' ' + dtResultado.Rows[0]["FECHA_AREA"].ToString();
                    lblComentarioRRHH.Text = " Comentario Gerente RRHH : " + dtResultado.Rows[0]["PERSONA_RRHH"].ToString() + ' ' + dtResultado.Rows[0]["FECHA_RRHH"].ToString();
                    lblRechazo.Text = " Comentario Rechazo : " + dtResultado.Rows[0]["PERSONA_RECHAZO"].ToString() + ' ' + dtResultado.Rows[0]["FECHA_RECHAZO"].ToString();

                }
                catch (Exception ex)
                {

                }

            }


        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ListarHito()
    {

        BL_RRHH_SOL_HITO obj = new BL_RRHH_SOL_HITO();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO(Convert.ToInt32(lblCodigo.Text), 1);
        if (dt.Rows.Count > 0)
        {
            gridHito.Visible = true;
            gridHito.DataSource = dt;
            gridHito.DataBind();
        }
        else
        {
            gridHito.Visible = false;
            gridHito.DataSource = dt;
            gridHito.DataBind();
        }
    }
    protected void ListarStakeholder()
    {

        BL_RRHH_SOL_HITO obj = new BL_RRHH_SOL_HITO();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO(Convert.ToInt32(lblCodigo.Text), 2);
        if (dt.Rows.Count > 0)
        {
            GridStakeholder.Visible = true;
            GridStakeholder.DataSource = dt;
            GridStakeholder.DataBind();
        }
        else
        {
            GridStakeholder.Visible = false;
            GridStakeholder.DataSource = dt;
            GridStakeholder.DataBind();
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (txtCargo.Text == string.Empty)
        {
            cleanMessage = "Ingresar tipo de cargo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtProyecto.Text == string.Empty)
        {
            cleanMessage = "Ingresar proyecto";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtDuracion.Text == string.Empty)
        {
            cleanMessage = "Ingresar duración";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtCantidad.Text == string.Empty)
        {
            cleanMessage = "Ingresar cantidad";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtCentro.Text == string.Empty)
        {
            cleanMessage = "Ingresar centro de costo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtNombreCeco.Text == string.Empty)
        {
            cleanMessage = "Ingresar nombre ceco";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtUbicacion.Text == string.Empty)
        {
            cleanMessage = "Ingresar ubicación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtSupervisor.Text == string.Empty)
        {
            cleanMessage = "Ingresar supervisor";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtObjetivo.Text == string.Empty)
        {
            cleanMessage = "Ingresar objetivo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtEntregable.Text == string.Empty)
        {
            cleanMessage = "Ingresar entregable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtIndicadores.Text == string.Empty)
        {
            cleanMessage = "Ingresar indicadores";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtIncluye.Text == string.Empty)
        {
            cleanMessage = "Ingresar alcance - incluye";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtNoincluye.Text == string.Empty)
        {
            cleanMessage = "Ingresar alcance - no incluye";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtRiesgo.Text == string.Empty)
        {
            cleanMessage = "Ingresar alcance riesgo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtRestricciones.Text == string.Empty)
        {
            cleanMessage = "Ingresar restricciones";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtRequisitos.Text == string.Empty)
        {
            cleanMessage = "Ingresar requisitos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtCosto.Text == string.Empty)
        {
            cleanMessage = "Ingresar costo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtBeneficios.Text == string.Empty)
        {
            cleanMessage = "Ingresar beneficios";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (gridHito.Rows.Count <= 0)
        {
            cleanMessage = "Falta ingresar principales hitos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (GridStakeholder.Rows.Count <= 0)
        {
            cleanMessage = "Falta ingresar definición de stakeholder";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            registroProyecto();
            BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
            DataTable dt = new DataTable();
            Session["FORMATIVO_PROYECTO"] = lblCodigo.Text;


        }

    }
    protected void registroProyecto()
    {
        int rpta;
        BE_RRHH_SOL_FORMATIVO objFormativo = new BE_RRHH_SOL_FORMATIVO();
        objFormativo.IDE_FORMATIVO = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        objFormativo.NOMBRE_PROYECTO = txtProyecto.Text;
        objFormativo.CENTRO_COSTO = txtCentro.Text;
        objFormativo.SUPERVISOR_DIRECTO = txtSupervisor.Text;
        objFormativo.UBICACION = txtUbicacion.Text;
        objFormativo.ESP_FORMACION = Convert.ToInt32(lblIdCargo.Text);
        objFormativo.CANTIDAD = Convert.ToInt32(string.IsNullOrEmpty(txtCantidad.Text) ? "0" : txtCantidad.Text);
        objFormativo.USER_SOLICITA = Session["IDE_USUARIO"].ToString();
        objFormativo.B_OBJETIVOS = txtObjetivo.Text.Trim();
        objFormativo.B_ENTREGABLES = txtEntregable.Text.Trim();
        objFormativo.B_INDICADORES = txtIndicadores.Text.Trim();
        objFormativo.C_INCLUYE = txtIncluye.Text.Trim();
        objFormativo.C_NO_INCLUYE = txtNoincluye.Text.Trim();
        objFormativo.C_RIESGO = txtRiesgo.Text.Trim();
        objFormativo.C_RESTRICCIONES = txtRestricciones.Text.Trim();
        objFormativo.D_REQUISITOS = txtRequisitos.Text.Trim();
        objFormativo.D_COSTO = txtCosto.Text.Trim();
        objFormativo.D_BENEFICIOS = txtBeneficios.Text.Trim();
        objFormativo.DURACION = txtDuracion.Text.Trim();
        rpta = new BL_RRHH_SOL_FORMATIVO().INS_RRHH_SOL_FORMATIVO(objFormativo);
        if (rpta > 0)
        {
            lblCodigo.Text = rpta.ToString();




            string cleanMessage = "Actualización satisfactoria";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
}