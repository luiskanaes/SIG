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

public partial class RRHH_FormativoProyecto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["IDE_CATEGORIA"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            lblIdCargo.Text = Session["IDE_CATEGORIA"].ToString();
            txtCargo.Text = Session["NOMBRE_CATEGORIA"].ToString();
            txtCentro.Text = BL_Session.CENTRO_COSTO.ToString();
            txtNombreCeco.Text = BL_Session.PROYECTO;
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
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
            dt = obj.SP_CORREO_NUEVA_SOL_FORMATIVO(Convert.ToInt32(lblCodigo.Text));
            Response.Redirect("~/RRHH/FormativoSolicitud.aspx");

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
        objFormativo.UBICACION = txtUbicacion.Text ;
        objFormativo.ESP_FORMACION = Convert.ToInt32 (lblIdCargo.Text  );
        objFormativo.CANTIDAD = Convert.ToInt32(string.IsNullOrEmpty(txtCantidad.Text) ? "0" : txtCantidad.Text);
        objFormativo.USER_SOLICITA = Session["IDE_USUARIO"].ToString ();
        objFormativo.B_OBJETIVOS = txtObjetivo.Text.Trim();
        objFormativo.B_ENTREGABLES= txtEntregable.Text.Trim();
        objFormativo.B_INDICADORES = txtIndicadores.Text.Trim();
        objFormativo.C_INCLUYE = txtIncluye.Text.Trim ();
        objFormativo.C_NO_INCLUYE = txtNoincluye.Text.Trim();
        objFormativo.C_RIESGO = txtRiesgo.Text.Trim ();
        objFormativo.C_RESTRICCIONES = txtRestricciones.Text.Trim ();
        objFormativo.D_REQUISITOS = txtRequisitos.Text.Trim ();
        objFormativo.D_COSTO = txtCosto.Text.Trim ();
        objFormativo.D_BENEFICIOS = txtBeneficios.Text.Trim ();
        objFormativo.DURACION = txtDuracion.Text.Trim();
        rpta = new BL_RRHH_SOL_FORMATIVO().INS_RRHH_SOL_FORMATIVO(objFormativo);
        if (rpta > 0)
        {
            lblCodigo.Text = rpta.ToString();

           
            

            //string cleanMessage = "registro satisfactorio";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        
    }

    protected void btnHito_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty ;
        registroProyecto();
        if (lblCodigo.Text != string.Empty)
        {
            if (txtHito.Text == string.Empty)
            {
                cleanMessage = "Ingresar etapa";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtFechaHito.Text == string.Empty)
            {
                cleanMessage = "Ingresar fecha de etapa";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                BE_RRHH_SOL_HITO obj = new BE_RRHH_SOL_HITO();
                obj.IDE_HITOS = 0;
                obj.IDE_FORMATIVO = Convert.ToInt32(lblCodigo.Text);
                obj.DESCRIPCION = txtHito.Text;
                obj.FECHA_HITO = txtFechaHito.Text;
                obj.TIPO = 1;
                obj.HOLDER = string.Empty;
                obj.ROL = string.Empty;
                obj.INTERACCION = string.Empty;
                int rpta = new BL_RRHH_SOL_HITO().INS_RRHH_SOL_HITO(obj);
                if (rpta > 0)
                {
                    ListarHito();
                    cleanMessage = "Registro satisfactorio (Etapa)";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    txtHito.Text = string.Empty;
                    txtFechaHito.Text = string.Empty;
                }
            }
        }
       
    }
    protected void ListarHito()
    {

        BL_RRHH_SOL_HITO obj = new BL_RRHH_SOL_HITO();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_RRHH_SOL_HITO_FORMATIVO_TIPO(Convert.ToInt32(lblCodigo.Text), 1);
        if(dt.Rows.Count > 0)
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
    protected void btnStakeholder_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        registroProyecto();
        if (lblCodigo.Text != string.Empty)
        {
            if (txtStakeholder.Text == string.Empty)
            {
                cleanMessage = "Ingresar nombre";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtRol.Text == string.Empty)
            {
                cleanMessage = "Ingresar cargo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtInteracion.Text == string.Empty)
            {
                cleanMessage = "Ingresar interacción";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                BE_RRHH_SOL_HITO obj = new BE_RRHH_SOL_HITO();
                obj.IDE_HITOS = 0;
                obj.IDE_FORMATIVO = Convert.ToInt32(lblCodigo.Text);
                obj.DESCRIPCION = string.Empty;
                obj.FECHA_HITO = string.Empty;
                obj.TIPO = 2;
                obj.HOLDER = txtStakeholder.Text;
                obj.ROL = txtRol.Text;
                obj.INTERACCION = txtInteracion.Text;
                int rpta = new BL_RRHH_SOL_HITO().INS_RRHH_SOL_HITO(obj);
                if (rpta > 0)
                {
                     cleanMessage = "Registro satisfactorio (Relación clave)";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    ListarStakeholder();

                    txtInteracion.Text = string.Empty;
                    txtRol.Text = string.Empty;
                    txtStakeholder.Text = string.Empty;

                }
            }
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
    protected void Eliminar_hito(object sender, ImageClickEventArgs e)
    {
        BL_RRHH_SOL_HITO obj = new BL_RRHH_SOL_HITO();
        DataTable dt = new DataTable();
        ImageButton btnEliminar = ((ImageButton)sender);
        dt = obj.uspDEL_RRHH_SOL_HITO_ID(Convert.ToInt32(btnEliminar.CommandArgument ));
        ListarHito();
        ListarStakeholder();
    }
}