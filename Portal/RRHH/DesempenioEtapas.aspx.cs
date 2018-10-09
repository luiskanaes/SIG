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

public partial class RRHH_DesempenioEtapas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Anios();
            int anio = DateTime.Today.Year ;
            ddlanio.SelectedValue = anio.ToString();
           
            centros();
            Proyecto();
            ParametrosEtapas();
            Listar();
        }
    }
    protected void Proyecto()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(3, "", 1);

        if (dtResultado.Rows.Count > 0)
        {
            ddlcecos.DataSource = dtResultado;
            ddlcecos.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlcecos.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlcecos.DataBind();

        }
        else
        {
            ddlcecos.DataSource = dtResultado;
            ddlcecos.DataBind();
        }

    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(3, "",1);

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
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
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
    protected void Listar()
    {

        BL_RRHH_DESEMPENIO_ETAPAS obj = new BL_RRHH_DESEMPENIO_ETAPAS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_ETAPAS_POR_ANIO(Convert.ToInt32(ddlanio.SelectedValue.ToString()),ddlcecos.SelectedValue.ToString() );
        if (dtResultado.Rows.Count > 0)
        {
         
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
           
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }


  
    protected void Procesar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");
        TextBox txtInicio = (TextBox)row.FindControl("txtInicio");
        TextBox txtfin = (TextBox)row.FindControl("txtfin");


        BL_RRHH_DESEMPENIO_ETAPAS obj = new BL_RRHH_DESEMPENIO_ETAPAS();
        DataTable dt = new DataTable();
        int Estado = 0;
        if (rb.SelectedValue == "CERRADO")
        {
            Estado = 0;
        }
        else 
        {
            Estado = 1;
        }
        string a= txtInicio.Text.Trim();
        if (txtInicio.Text.Trim () == string.Empty  || txtfin .Text.Trim() == string.Empty)
        {
            string cleanMessage = "Ingresar la fecha de inicio y fin de la etapa";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtInicio.Text.Trim() == "" || txtfin.Text.Trim() =="")
        {
            string cleanMessage = "Ingresar la fecha de inicio y fin de la etapa";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            DateTime inicio = Convert.ToDateTime (txtInicio.Text.Trim());
            DateTime final = Convert.ToDateTime(txtfin.Text.Trim());

            if (inicio > final)
            {
                string cleanMessage = "La fecha de inicio no puede ser mayor a la fecha final";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                dt = obj.uspUPD_RRHH_DESEMPENIO_ETAPAS(Convert.ToInt32(pk), inicio.ToString("dd/M/yyyy"), final.ToString("dd/M/yyyy"), Estado);
                string cleanMessage = "Actualización satisfactoria";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                Listar();
            }
            
        }
       

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        DataTable dtResultado = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            dtResultado = GetTableEstado();
            if (dtResultado.Rows.Count > 0)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
            }


        }
    }
    static DataTable GetTableEstado()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("IDE", typeof(string));
        table.Columns.Add("DESCRIPCION", typeof(string));

        table.Rows.Add("CERRADO", "CERRADO");
        table.Rows.Add("ABIERTO", "ABIERTO");
        return table;
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        BE_RRHH_DESEMPENIO_ETAPAS oBESol = new BE_RRHH_DESEMPENIO_ETAPAS();
        oBESol.ANIO = Convert.ToInt32(ddlanio.SelectedValue);
        oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();

        int dtrpta = 0;
        dtrpta = new BL_RRHH_DESEMPENIO_ETAPAS().uspINS_RRHH_DESEMPENIO_ETAPAS(oBESol);
        Listar();
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    protected void ParametrosEtapas()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlestapas.DataSource = obj.ListarParametros_orden("IDE_ETAPAS", "RRHH_DESEMPENIO_FICHA");
        ddlestapas.DataTextField = "DES_ASUNTO";
        ddlestapas.DataValueField = "ID_PARAMETRO";
        ddlestapas.DataBind();


    }

    protected void chkEstados_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEstados.Checked == true)
        {
            foreach (ListItem item in ddlestapas.Items)
            {
                item.Selected = true;

            }

        }

        if (chkEstados.Checked == false)
        {
            foreach (ListItem item in ddlestapas.Items)
            {
                item.Selected = false;

            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        //if (CheckCompetencia.SelectedIndex != -1)
        int cantidad = 0;
        if (ddlestapas.SelectedIndex > -1)
        {

            foreach (ListItem li in ddlestapas.Items)
            {
                if (li.Selected)
                {
                   

                    DataTable dt = new DataTable();
                    BE_RRHH_DESEMPENIO_ETAPAS oBESol = new BE_RRHH_DESEMPENIO_ETAPAS();
                    oBESol.ANIO = Convert.ToInt32(ddlanio.SelectedValue);
                    oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
                    oBESol.CODIGO_ETAPA = Convert.ToInt32(li.Value);
                    oBESol.CECOS = ddlCentro.SelectedValue.ToString(); 
                    int dtrpta = 0;
                    dtrpta = new BL_RRHH_DESEMPENIO_ETAPAS().uspINS_RRHH_DESEMPENIO_ETAPAS_ID(oBESol);
                    if (dtrpta > 0 )
                    {
                        cantidad++;
                    }
                    

                }
            }

            if (cantidad > 0)
            {
                Listar();
                string cleanMessage = "Registro satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                string cleanMessage = "Algunas etapas ya se encuetran registradas";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

        }
        else
        {
            string cleanMessage = "Debe seleccionar alguna etapa";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcecos.SelectedValue = ddlCentro.SelectedValue.ToString();
        Listar();
    }

    protected void ddlcecos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
}