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
public partial class RRHH_Reconocimiento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ParametrosCompetencia();
            Personal();
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
            if (arg.ToString().Equals("ddlPersonal"))
            {
                string strSelectedValue = ddlPersonal.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ParametrosCompetencia()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCompetencia.DataSource = obj.ListarParametros("COMPETENCIAS", "RRHH_COMPETENCIAS_EVAL");
        ddlCompetencia.DataTextField = "DES_ASUNTO";
        ddlCompetencia.DataValueField = "ID_PARAMETRO";
        ddlCompetencia.DataBind();

        ddlCompetencia.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarPersonal_mando(Session["IDE_USUARIO"].ToString ());
        dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_TODO();
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            btnIngresar.Visible = true ;


            //cboCountry.DataSource = dtResultado;
            //cboCountry.DataTextField = "NOMBRE_COMPLETO";
            //cboCountry.DataValueField = "ID_DNI";
            //cboCountry.DataBind();
            //cboCountry.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            btnIngresar.Visible = false;

            string cleanMessage = "No existe personal a reconocer";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlPersonal.SelectedIndex < 1)
        {
            
            cleanMessage = "Seleccionar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            
        }
        else if (ddlCompetencia.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar competencia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtSustento.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar sustento";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_RRHH_COMPETENCIAS_EVAL oBESol = new BE_RRHH_COMPETENCIAS_EVAL();
            oBESol.IDE_COMPETENCIA = 0;
            oBESol.DNI_EVALUADO = ddlPersonal.SelectedValue.ToString();
            oBESol.DNI_SUPERVISOR = Session["IDE_USUARIO"].ToString();
            oBESol.IDE_FACTOR = Convert.ToInt32(ddlCompetencia.SelectedValue);
            oBESol.SUSTENTO = txtSustento.Text.Trim();

            int dtrpta = 0;
            dtrpta = new BL_RRHH_COMPETENCIAS_EVAL().Mant_Insert_Reconocimiento(oBESol);
            if (dtrpta > 0)
            {
               
                BL_RRHH_COMPETENCIAS_EVAL ob = new BL_RRHH_COMPETENCIAS_EVAL();
                ob.EnviarCorreoCompetencia(dtrpta);
                cleanMessage = "Registro exitoso, en poco tiempo estaremos informando sobre la situación de este reconocimiento, gracias";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
              
                Limipiar();
            }
        }

        //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "JSFunction();", true);
    }
    protected void Limipiar()
    {
        txtSustento.Text = string.Empty;
        ParametrosCompetencia();
        Personal();
    }




}