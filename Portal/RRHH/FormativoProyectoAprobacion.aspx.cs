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
public partial class RRHH_FormativoProyectoAprobacion : System.Web.UI.Page
{
    string IDE_FORMATIVO = string.Empty;
    string ESTADO = string.Empty;
    public string variableG = string.Empty ;
    public string variableGNombre = string.Empty;
    public string variableGComentario = string.Empty;

    public string variableA= string.Empty;
    public string variableANombre = string.Empty;
    public string variableAComentario = string.Empty;

    public string variableR = string.Empty;
    public string variableRNombre = string.Empty;
    public string variableRComentario = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {

            IDE_FORMATIVO = Session["IDE_FORMATIVO"].ToString();
            Datos(Session["IDE_FORMATIVO"].ToString());
            ESTADO = Session["ESTADO"].ToString();
            
        }
    }
    protected void Datos(string IDE)
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

            variableG = "(*) Comentario Generalista : " + ' ' + dtResultado.Rows[0]["FECHA_GENERALISTA"].ToString();
            variableGNombre = dtResultado.Rows[0]["PERSONA_GN"].ToString();
            variableGComentario= dtResultado.Rows[0]["COMENTARIOS_GENERAL"].ToString();

            variableA = "(*) Comentario Gerente de Area : " + ' ' + dtResultado.Rows[0]["FECHA_AREA"].ToString();
            variableANombre = dtResultado.Rows[0]["PERSONA_AREA"].ToString();
            variableAComentario= dtResultado.Rows[0]["COMENTARIOS_AREA"].ToString();

            variableR = "(*) Comentario Rechazo : " + ' ' + dtResultado.Rows[0]["FECHA_RECHAZO"].ToString();
            variableRNombre = dtResultado.Rows[0]["PERSONA_RECHAZO"].ToString();
            variableRComentario = dtResultado.Rows[0]["COMENTARIO_RECHAZO"].ToString();


            string SITUACION_RESUMEN = dtResultado.Rows[0]["SITUACION_RESUMEN"].ToString();

            if (Session["ESTADO"].ToString() == "A2")
            {
                txtObservaciones.Text = dtResultado.Rows[0]["COMENTARIOS_AREA"].ToString();

                string ESTADO_AREA = dtResultado.Rows[0]["ESTADO_AREA"].ToString();
                lblEstado.Text = dtResultado.Rows[0]["MSJ_AREA"].ToString();

                if (ESTADO_AREA == string.Empty)
                {
                    rdoOpcion.Visible = true;
                    btnProcesar.Visible = true;
                }
                else if (ESTADO_AREA == "1")
                {
                    btnProcesar.Visible = true;
                    rdoOpcion.Visible = true;
                }
                else
                {
                    rdoOpcion.Visible = false;
                    btnProcesar.Visible = false;
                }
            }
            else if (Session["ESTADO"].ToString() == "A3")
            {
                txtObservaciones.Text = dtResultado.Rows[0]["COMENTARIOS_RRHH"].ToString();

                string ESTADO_RRHH = dtResultado.Rows[0]["ESTADO_RRHH"].ToString();
                lblEstado.Text = dtResultado.Rows[0]["MSJ_RRHH"].ToString();

                if (ESTADO_RRHH == string.Empty)
                {
                    rdoOpcion.Visible = true;
                    btnProcesar.Visible = true;
                }
                else if (ESTADO_RRHH == "1")
                {
                    btnProcesar.Visible = true;
                    rdoOpcion.Visible = true;
                }
                else
                {
                    rdoOpcion.Visible = false;
                    btnProcesar.Visible = false;
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

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        BL_RRHH_SOL_FORMATIVO obj = new BL_RRHH_SOL_FORMATIVO();
        DataTable dt = new DataTable();
        DataTable dtCorreo = new DataTable();
        string valor = string.Empty;

        if (rdoOpcion.SelectedValue != string.Empty)
        {
            if (rdoOpcion.SelectedValue == "APROBADO")
            {
                valor = Session["ESTADO"].ToString();
            }
            else
            {
                valor = "R";
            }

            dt = obj.uspSEL_RRHH_FORMATIVO_PROCESAR_AREAS(Convert.ToInt32(lblCodigo.Text), txtObservaciones.Text, valor, Session["IDE_USUARIO"].ToString ());

            if (Session["ESTADO"].ToString() == "A2" && valor != "R")
            {
                valor = "A3";
            }

            dtCorreo = obj.SP_CORREO_FORMATIVO_APROBACIONES(Convert.ToInt32(lblCodigo.Text), valor, "");

            string cleanMessage = "Solicitud enviada a la Gerencia de RRHH";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Datos(lblCodigo.Text);
        }
        else
        {
            string cleanMessage = "Indicar situación de la solicitud";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
}