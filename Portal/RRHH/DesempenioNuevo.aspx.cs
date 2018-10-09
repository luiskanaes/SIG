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

public partial class RRHH_DesempenioNuevo : System.Web.UI.Page
{
    string ANIO;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Anios();
            Personal();
            Familia();
            Empresas();
            int anio = DateTime.Today.Year;
            ddlanio.SelectedValue = anio.ToString();
            //ANIO = Session["ANIO"].ToString();
        }
    }
    protected void Anios()
    {
       
        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableAnio();
        if (dtResultado.Rows.Count > 0)
        {
            ddlanio.DataSource = dtResultado;
            ddlanio.DataTextField = "ANIO1";
            ddlanio.DataValueField = "ANIO";
            ddlanio.DataBind();

        }
    }
    static DataTable GetTableAnio()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ANIO", typeof(int));
        table.Columns.Add("ANIO1", typeof(string));

        int anio = 0;
        int anioActual = 0;
        anio = DateTime.Today.Year;
        anioActual = DateTime.Today.Year;
        for (int i = 0; i < 6; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
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

                //txtDni.Text = strSelectedValue;
                //txtCargo.Text = dtResultado.Rows[0]["CARGO_DESCRIPCION"].ToString();
                //Your code here ... 
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
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
   
    protected void Personal()
    {
        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_PERSONAL_LIBRE(Convert.ToInt32(ddlanio.SelectedValue));
        lblcantidad.Text = " (" + dtResultado.Rows.Count.ToString() + ")";
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));


            GridView1.DataSource = dtResultado;
            GridView1.DataBind();

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));


            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }




    protected void btnAgregar_Click(object sender, EventArgs e)
    {

        string cleanMessage = string.Empty;
        if (ddlPersonal.SelectedIndex < 1)
        {

            cleanMessage = "Seleccionar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }
        else if (ddlFamilia.SelectedIndex < 1)
        {
            cleanMessage = "Seleccionar familia";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_RRHH_DESEMPENIO_FICHA oBESol = new BE_RRHH_DESEMPENIO_FICHA();
            oBESol.DNI = ddlPersonal.SelectedValue.ToString();
            oBESol.CCENTRO = ddlCentro.SelectedValue.ToString();
            oBESol.CODIGO_GERENCIA = ddlGerencia.SelectedValue.ToString();
            oBESol.IP_CENTRO = string.Empty ;
            oBESol.ANIO = Convert.ToInt32(ddlanio.SelectedValue );
            oBESol.DNI_JEFE = ddlJefe.SelectedValue.ToString();
            oBESol.DNI_GERENTE = ddlGerencia.SelectedValue.ToString();
            oBESol.USER_REGISTRA = Session["IDE_USUARIO"].ToString();

            oBESol.IDE_FAMILIA = Convert.ToInt32(ddlFamilia.SelectedValue);
            oBESol.COMENTARIOS = txtcomentario.Text.Trim();
            DataTable dtrpta = new DataTable();

            BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
            dtrpta = obj.uspSEL_RRHH_DESEMPENIO_ADICIONAR(oBESol);
            if (dtrpta.Rows.Count > 0)
            {

                int codigo = Convert.ToInt32(dtrpta.Rows[0]["CODIGO"].ToString());
                if (codigo == 0)
                {
                    string _gerencia = dtrpta.Rows[0]["CODIGO_GERENCIA"].ToString();
                    string _cc = dtrpta.Rows[0]["CCENTRO"].ToString();
                    string _jefe = dtrpta.Rows[0]["JEFE"].ToString();
                  
                    cleanMessage = "Personal ya se encuentra asignado al:  <br /> - Centro de costo : " + _cc + "<br /> - Jefe directo : " + _jefe;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else
                {
                    Session["IDE_DESEMPENIO"] = dtrpta.Rows[0]["IDE_DESEMPENIO"].ToString();

                    ddlPersonal.Text = "";
                    txtcomentario.Text = string.Empty;
                    cleanMessage = "Registro exitoso";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                   


                }
            }
            else
            {
                cleanMessage = "Error!! volver a intentarlo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ddlPersonal.Text = "";
        txtcomentario.Text = string.Empty;
       
    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresa.DataSource = dtResultado;
            ddlEmpresa.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresa.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresa.DataBind();
            gerencias();
        }

    }
    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(1, "", Convert.ToInt32(ddlEmpresa.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlGerencia.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerencia.DataBind();
            centros();
            PersonalCargo(2);
            PersonalCargo(3);
        }
        else
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataBind();

            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(2, ddlGerencia.SelectedValue.ToString(), Convert.ToInt32(ddlEmpresa.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlCentro.DataBind();

        }
        else
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Personal();
    }

    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
        PersonalCargo(2);
        PersonalCargo(3);
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
        PersonalCargo(2);
        PersonalCargo(3);
    }
    protected void PersonalCargo(int tipo)
    {
      
        if (tipo == 2)
        {
            BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.uspSEL_RRHH_DESEMPENIO_PERSONAL_CARGOS(Convert.ToInt32(ddlanio.SelectedValue), tipo, ddlGerencia.SelectedValue.ToString(), ddlCentro.SelectedValue.ToString());

            if (dtResultado.Rows.Count > 0)
            {
                ddlGerente.DataSource = dtResultado;
                ddlGerente.DataTextField = dtResultado.Columns["NOMBRE_COMPLETO"].ToString();
                ddlGerente.DataValueField = dtResultado.Columns["DNI"].ToString();
                ddlGerente.DataBind();

            }
            else
            {
                ddlGerente.DataSource = dtResultado;
                ddlGerente.DataBind();
            }
        }
        else
        {
            BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
            DataTable dtResul = new DataTable();
            dtResul = obj.uspSEL_RRHH_DESEMPENIO_PERSONAL_CARGOS(Convert.ToInt32(ddlanio.SelectedValue), tipo, ddlGerencia.SelectedValue.ToString(), ddlCentro.SelectedValue.ToString());

            if (dtResul.Rows.Count > 0)
            {
                ddlJefe.DataSource = dtResul;
                ddlJefe.DataTextField = dtResul.Columns["NOMBRE_COMPLETO"].ToString();
                ddlJefe.DataValueField = dtResul.Columns["DNI"].ToString();
                ddlJefe.DataBind();

            }
            else
            {
                ddlJefe.DataSource = dtResul;
                ddlJefe.DataBind();
            }
        }
        

    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        PersonalCargo(3);
    }





    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Personal();
    }
}