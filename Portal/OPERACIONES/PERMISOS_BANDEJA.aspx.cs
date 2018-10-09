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


public partial class OPERACIONES_PERMISOS_BANDEJA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportar);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            privilegios();
            ParametrosEstados();
            Anios();

            ddlanio.SelectedValue = DateTime.Today.Year.ToString();
            Listar();
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
        anio = DateTime.Today.Year + 1;
        anioActual = DateTime.Today.Year + 1;
        for (int i = 0; i < 5; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
    }
    
    protected void ParametrosEstados()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEstados.DataSource = obj.ListarParametros("FLG_ESTADO", "TBSOLICITUD_PERMISOS");
        ddlEstados.DataTextField = "DES_ASUNTO";
        ddlEstados.DataValueField = "ID_PARAMETRO";
        ddlEstados.DataBind();

        ddlEstados.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        //Listar();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        BL_PERSONAL ObjEstado = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            dtResultado = ObjEstado.ListarParametros("FLG_ESTADO", "TBSOLICITUD_PERMISOS");
            if (dtResultado.Rows.Count > 0)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
            }


        }
    }

    protected void Listar()
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
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SP_TBSOLICITUD_PERMISOS_TODOS(ddlanio.SelectedValue.ToString (),estado, BL_Session.CENTRO_COSTO);
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



    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    protected void Procesar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

        //string z= pk + " - " + rb.SelectedValue;
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS ();
        DataTable dt = new DataTable();
        lblrpta.Text = rb.SelectedValue;
        if (rb.SelectedValue == "R")
        {
            ModalRegistro.Show();
            lblCodigo.Text = pk.ToString();
            lblmsg.Text = "Observación de rechazo";
        }
        else if (rb.SelectedValue == "A")
        {

            //dt = obj.SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS(Convert.ToInt32(pk), rb.SelectedValue, "", Session["IDE_USUARIO"].ToString());

            //string cleanMessage = "Registro procesado";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            //Listar();
            lblmsg.Text = "Comentarios de aprobación";

            ModalRegistro.Show();
            lblCodigo.Text = pk.ToString();

        }

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
        DataTable dt = new DataTable();
        
        if (lblrpta.Text == "R")
        {
            if (txtSustento.Text != string.Empty)
            {
                dt = obj.SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS(Convert.ToInt32(lblCodigo.Text), lblrpta.Text, txtSustento.Text.Trim(), Session["IDE_USUARIO"].ToString());

                string cleanMessage = "Registro procesado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                limpiar();
                Listar();
            }
            else
            {
                string cleanMessage = "ingresar sustento de rechazo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else if (lblrpta.Text == "A")
        {

            dt = obj.SP_TBSOLICITUD_PERMISOS_PROCESAR_DATOS(Convert.ToInt32(lblCodigo.Text), lblrpta.Text, txtSustento.Text.Trim(), Session["IDE_USUARIO"].ToString());

            string cleanMessage = "Registro procesado";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            limpiar();
            Listar();

        }
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    protected void Formato(object sender, ImageClickEventArgs e)
    {

        ImageButton btnMdp = ((ImageButton)sender);
        GridViewRow row = btnMdp.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        limpiar();
    }
    protected void limpiar()
    {
        lblCodigo.Text = string.Empty;
        lblrpta.Text = string.Empty;
        txtSustento.Text = string.Empty;
    }

    protected void EnviarEmail(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEnviar = ((ImageButton)sender);
  

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string IDE_PERMISO = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_PERMISO"].ToString();



        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup('" + IDE_PERMISO + "',"  + 600 + "," + 400 + ");", true);


    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
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
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SP_TBSOLICITUD_PERMISOS_TODOS(ddlanio.SelectedValue.ToString(), estado, BL_Session.CENTRO_COSTO);
        if (dtResultado.Rows.Count > 0)
        {
            GridView2.Visible = true;
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();

            GridViewExportUtil.Export("MDP_" + DateTime.Now + ".xls", GridView2);
            return;
        }
        else
        {
            GridView2.Visible = false;
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
    }
}