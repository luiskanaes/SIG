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


public partial class OPERACIONES_Equipos : System.Web.UI.Page
{
    public string ListaCC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            Personal();

            Empresas();
          
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
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void gerenciasResponsable()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(1, "", Convert.ToInt32(ddlEmpresa.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerenciaResponsable.DataSource = dtResultado;
            ddlGerenciaResponsable.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerenciaResponsable.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerenciaResponsable.DataBind();
            ddlGerenciaResponsable.Items.Insert(0, new ListItem("--- TODOS ---", ""));

            ListarResponsables();
        }
        else
        {
            ddlGerenciaResponsable.DataSource = dtResultado;
            ddlGerenciaResponsable.DataBind();

        }
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

            gerenciasResponsable();
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

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
        gerenciasResponsable();
    }

    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListaCC = string.Empty;
        foreach (ListItem li in ddlCentro.Items)
        {
            if (li.Selected)
            {

                ListaCC += li.Text + "<br>";
                //ListaPersonal += li.Value.Replace(",", Environment.NewLine);
            }
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlPersonal.SelectedValue==string.Empty)
        {
             cleanMessage = "Falta indicar personal responsable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            int cantidad = 0;
            foreach (ListItem li in ddlCentro.Items)
            {
                if (li.Selected)
                {
                    string centro = li.Value;

                    //lblCodigo.Text = string.Empty;

                    BE_EQUIPO_RESPONSABLE oBESol = new BE_EQUIPO_RESPONSABLE();
                    oBESol.IDE_RESPONSABLE = 0;
                    oBESol.DNI_RESPONSABLE = ddlPersonal.SelectedValue.ToString();
                    oBESol.IP_CENTRO = ddlGerencia.SelectedValue;
                    oBESol.CENTRO = centro;
                    oBESol.FLG_ESTADO = 1;

                    int dtrpta = 0;
                    dtrpta = new BL_EQUIPO_RESPONSABLE().uspINS_EQUIPO_RESPONSABLE(oBESol);
                    if (dtrpta > 0)
                    {
                        cantidad++;
                        CargarTrabajadores(dtrpta, centro);
                    }

                    
                }

            }


            if (cantidad > 0)
            {
                ddlPersonal.Text = string.Empty;
                cleanMessage = "Registro exitoso";
                ListarResponsables();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
            else
            {
                cleanMessage = "Falta seleccionar centros de costos";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
           
        }
    }
    protected void CargarTrabajadores(int IDE_RESPONSABLE, string CENTRO)
    {
        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspINS_EQUIPO_TRABAJO_VARIOS(IDE_RESPONSABLE, CENTRO , Session["IDE_USUARIO"].ToString());
    }
    protected void Registrar()
    {
        string cleanMessage = string.Empty;
          BE_EQUIPO_RESPONSABLE oBESol = new BE_EQUIPO_RESPONSABLE();
        oBESol.IDE_RESPONSABLE = 0;
        oBESol.DNI_RESPONSABLE = ddlPersonal.SelectedValue.ToString();
        oBESol.IP_CENTRO = ddlGerencia.SelectedValue;
        oBESol.CENTRO = ddlCentro.SelectedValue;
        oBESol.FLG_ESTADO = 1;
    
        int dtrpta = 0;
        dtrpta = new BL_EQUIPO_RESPONSABLE().uspINS_EQUIPO_RESPONSABLE(oBESol);
        if (dtrpta > 0)
        {

            ddlPersonal.Text = string.Empty;
            cleanMessage = "Registro exitoso";
            ListarResponsables();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
           
    }

    protected void ListarResponsables()
    {
        string Gerenencia = string.Empty;
        if (ddlGerenciaResponsable.SelectedIndex == 0)
        {
            Gerenencia = string.Empty;
        }
        else
        {
            Gerenencia = ddlGerenciaResponsable.SelectedValue.ToString();
        }
        //BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspSEL_EQUIPO_RESPONSABLE_GERENCIA(Gerenencia);
        //if (dtResultado.Rows.Count > 0)
        //{
        //    GridView1.Visible = true;
        //    GridView1.DataSource = dtResultado;
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    GridView1.Visible = false;
        //    GridView1.DataSource = dtResultado;
        //    GridView1.DataBind();
        //}


        BL_EQUIPO_RESPONSABLE objGerencia = new BL_EQUIPO_RESPONSABLE();
        DataTable dt = new DataTable();
        dt = objGerencia.uspSEL_EQUIPO_GERENCIA(Gerenencia);
        if (dt.Rows.Count == 0)
        {
            dlCustomers.Visible = false;
            dlCustomers.DataSource = dt;
            dlCustomers.DataBind();
        }
        else
        {
            dlCustomers.Visible = true;
            dlCustomers.DataSource = dt;
            dlCustomers.DataBind();

        }
    }

    

    protected void ddlGerenciaResponsable_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarResponsables();
    }
    protected void ver_Equipo(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEquipo = ((ImageButton)sender);
        GridViewRow row = btnEquipo.NamingContainer as GridViewRow;
        //string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        Session["IDE_RESPONSABLE"] = btnEquipo.CommandArgument.ToString();
        Response.Redirect("~/Operaciones/EquipoTrabajo.aspx");

    }
    protected void EliminarResponsable(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;
        //string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();


        BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspUPD_EQUIPO_RESPONSABLE_ESTADO(Convert.ToInt32(btnEliminar.CommandArgument), 0);
        ListarResponsables();

    }

    protected void chkCentros_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCentros.Checked == true)
        {
            foreach (ListItem item in ddlCentro.Items)
            {
                item.Selected = true;

            }

        }

        if (chkCentros.Checked == false)
        {
            foreach (ListItem item in ddlCentro.Items)
            {
                item.Selected = false;

            }
        }
    }

    protected void btnLibre_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Equipolibre.aspx");
    }
    protected void dlCustomers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //string estado = string.Empty;
        //if (ddlLeyenda.SelectedIndex == 0)
        //{
        //    estado = string.Empty;
        //}
        //else
        //{
        //    estado = ddlLeyenda.SelectedValue.ToString();
        //}


        //BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspSEL_EQUIPO_RESPONSABLE_GERENCIA(Gerenencia);
        //if (dtResultado.Rows.Count > 0)
        //{
        //    GridView1.Visible = true;
        //    GridView1.DataSource = dtResultado;
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    GridView1.Visible = false;
        //    GridView1.DataSource = dtResultado;
        //    GridView1.DataBind();
        //}

        BL_EQUIPO_RESPONSABLE obj = new BL_EQUIPO_RESPONSABLE();
        DataRowView drv = e.Item.DataItem as DataRowView;
        GridView innerDataList = e.Item.FindControl("GridView1") as GridView;
        Label lbl = (Label)e.Item.FindControl("lblGerencia");
        DataTable _DataTable2 = new DataTable();
        _DataTable2 = obj.uspSEL_EQUIPO_RESPONSABLE_GERENCIA(lbl.Text);
        foreach (DataRow rw in _DataTable2.Rows)
        {
            innerDataList.DataSource = _DataTable2;
            innerDataList.DataBind();
        }
    }
}