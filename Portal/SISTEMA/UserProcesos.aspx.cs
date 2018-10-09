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


public partial class SISTEMA_UserProcesos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Parametros();
            Empresas();
            Personal();
            //gerencias();
            Estado();
            Listar("","");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
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
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Parametros()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlProceso.DataSource = obj.ListarParametros("PERFILES", "SOLICITUDES");
        ddlProceso.DataTextField = "DES_ASUNTO";
        ddlProceso.DataValueField = "ID_PARAMETRO";
        ddlProceso.DataBind();

      
        //Listar();
    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresas.DataSource = dtResultado;
            ddlEmpresas.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresas.DataBind();

            gerencias();



        }
    }
    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA(Convert.ToInt32( ddlEmpresas.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerencia.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerencia.DataBind();
            centros();

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
        //dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(2, ddlGerencia.SelectedValue.ToString(), 1);
        dtResultado = obj.uspLISTAR_GERENCIA_X_CENTROS(ddlGerencia.SelectedValue.ToString(), Convert.ToInt32(ddlEmpresas.SelectedValue));
     
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

    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
    }
    protected void Estado()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableEstado();
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstados.DataSource = dtResultado;
            ddlEstados.DataTextField = "DESCRIPCION";
            ddlEstados.DataValueField = "ID";
            ddlEstados.DataBind();
            ddlEstados.Items.Insert(0, new ListItem("--- Todos ---", ""));
        }
    }
    static DataTable GetTableEstado()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("DESCRIPCION", typeof(string));


        table.Rows.Add("1", "Activos");
        table.Rows.Add("0", "Bloqueados");




        return table;
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));



        }
    }
    protected void Listar(string nombre, string proceso)
    {
        string estado = string.Empty;
        if (ddlEstados.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString();
        }
        BL_RESPONSABLE_PROCESOS obj = new BL_RESPONSABLE_PROCESOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_POR_ID(estado, nombre, proceso);
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        int todos;

        if(CheckTodos.Checked)
        {
            todos = 1;
        }
        else
        {
            todos = 0;
        }

        BE_RESPONSABLE_PROCESOS oBESol = new BE_RESPONSABLE_PROCESOS();
        oBESol.IDE_RESPONSABLE = 0;
        oBESol.DNI_RESPONSABLE = ddlPersonal.SelectedValue.ToString();
        oBESol.IP_CENTRO = ddlGerencia.SelectedValue;
        oBESol.GERENCIA = ddlGerencia.SelectedValue;
        oBESol.CENTRO = ddlCentro.SelectedValue;
        oBESol.TIPO = ddlProceso.SelectedItem.Text;
        oBESol.IDE_PROCESO = Convert.ToInt32( ddlProceso.SelectedValue.ToString());
        oBESol.TODOS = todos;
        oBESol.IDE_EMPRESA = Convert.ToInt32(ddlEmpresas.SelectedValue.ToString());
        int dtrpta = 0;
        dtrpta = new BL_RESPONSABLE_PROCESOS().uspINS_RESPONSABLE_PROCESOS(oBESol);
        if (dtrpta > 0)
        {
            string cleanMessage = "Registro satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Listar("","");
        }
    }

    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("","");
    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        new BL_RESPONSABLE_PROCESOS().uspDEL_RESPONSABLE_PROCESOS_POR_ID(Convert.ToInt32(pk));
        Listar("", "");
    }

    protected void btnImagen_Click(object sender, ImageClickEventArgs e)
    {
        


        TextBox txtNOMBRE_F = (TextBox)GridView1.HeaderRow.FindControl("txtNOMBRE_F");
        TextBox txtPROCESO_F = (TextBox)GridView1.HeaderRow.FindControl("txtPROCESO_F");


        Listar(txtNOMBRE_F.Text.Trim(), txtPROCESO_F.Text.Trim());

    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string cleanMessage = string.Empty;
            int intContador = 0;

            if (GridView1.Rows.Count == 0)
            {
                cleanMessage = "No existe registros";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

            foreach (GridViewRow Fila in GridView1.Rows)
            {
                CheckBox ChkBoxCell = ((CheckBox)Fila.FindControl("chkSelect"));
                if (ChkBoxCell.Checked == true)
                {
                    intContador += 1;
                }
            }

            if (intContador == 0)
            {


                cleanMessage = "Debe seleccionar al menos un registro.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }



            foreach (GridViewRow row in GridView1.Rows)
            {

                CheckBox ChkBoxCell = ((CheckBox)row.FindControl("chkSelect"));
                if (ChkBoxCell.Checked)
                {

                    string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

                    new BL_RESPONSABLE_PROCESOS().uspDEL_RESPONSABLE_PROCESOS_POR_ID(Convert.ToInt32(pk));
                    intContador++;
                }
                ChkBoxCell = null;
            }

            if (intContador > 0)
            {
                

                cleanMessage = "Accesos eliminados correctamente";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
            Listar("","");

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
    }
}