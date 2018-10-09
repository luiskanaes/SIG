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

public partial class OPERACIONES_MOD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

      
        if (!Page.IsPostBack)
        {
            ControlCecos();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Personal();
            Especialidad();
            Categoria();

            if (Session["IDE_MOD"] != null)
            {
                string IDE_MOD = Session["IDE_MOD"].ToString();
                if (IDE_MOD.Length > 0)
                {
                    CartaDatos(Session["IDE_MOD"].ToString());
                }

            }

            //lblCentro.Text = BL_Session.CENTRO_COSTO.ToString();
        }
    }
    protected void CartaDatos(string IDE_MOD)
    {
        DataTable dtResultado = new DataTable();
        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        dtResultado = obj.uspSEL_MOD_REQUERIMIENTO_IDE(Convert.ToInt32(IDE_MOD));
        if (dtResultado.Rows.Count > 0)
        {
            txtTicket.Text = dtResultado.Rows[0]["TICKET"].ToString();
            lblCodigo.Text = dtResultado.Rows[0]["IDE_MOD"].ToString();

            txtSolicitado.Text = dtResultado.Rows[0]["NOM_SOLICITANTE"].ToString();
            txtVerificado.Text = dtResultado.Rows[0]["NOM_JEFE_AREA"].ToString();
            txtadministrador.Text = dtResultado.Rows[0]["NOM_ADMINISTRADOR"].ToString();
            txtGerente.Text = dtResultado.Rows[0]["NOM_GERENTE"].ToString();

            Listar();
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
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void Especialidad()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlespecialidad.DataSource = obj.ListarParametros("IDE_ESPECIALIDAD", "RRHH_MOD");
        ddlespecialidad.DataTextField = "DES_ASUNTO";
        ddlespecialidad.DataValueField = "ID_PARAMETRO";
        ddlespecialidad.DataBind();
    }
    protected void Categoria()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCategoria.DataSource = obj.ListarParametros("IDE_CATEGORIA", "RRHH_MOD");
        ddlCategoria.DataTextField = "DES_ASUNTO";
        ddlCategoria.DataValueField = "ID_PARAMETRO";
        ddlCategoria.DataBind();
    }
    protected void ControlCecos()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
    
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE SELECCION MOD", BL_Session.ID_EMPRESA.ToString());

        if (dtResultado.Rows.Count > 0)
        {
            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "CENTRO";
            ddlcentro.DataValueField = "CENTRO";
            ddlcentro.DataBind();
            //ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
        else
        {

        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        //dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_CC(ddlcentro.SelectedValue.ToString());
        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.Items.Clear();
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

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txtCantidad.Text == string.Empty)
        {
            cleanMessage = "Ingresar cantidad";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_MOD_REQUERIMIENTO oBESol = new BE_MOD_REQUERIMIENTO();
            oBESol.IDE_MOD = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.CODIGO = string.Empty;
            oBESol.FECHA_REQUERIMIENTO = txtFecha.Text.Trim();
            oBESol.IP_CENTRO = ddlcentro.SelectedValue.ToString();
            oBESol.CENTRO = ddlcentro.SelectedValue.ToString();
            oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();

            int dtrpta = 0;
            dtrpta = new BL_MOD_REQUERIMIENTO().uspINS_MOD_REQUERIMIENTO(oBESol);
            if (dtrpta > 0)
            {
                lblCodigo.Text = dtrpta.ToString();
                BE_MOD_REQUERIMIENTO_DETALLE oBESolD = new BE_MOD_REQUERIMIENTO_DETALLE();
                oBESolD.IDE_REQUERIMIENTO = 0;
                oBESolD.IDE_MOD = dtrpta;
                oBESolD.IDE_CATEGORIA = Convert.ToInt32(ddlCategoria.SelectedValue);
                oBESolD.IDE_ESPECIALIDAD = Convert.ToInt32(ddlespecialidad.SelectedValue);
                oBESolD.CANTIDAD = Convert.ToInt32(string.IsNullOrEmpty(txtCantidad.Text) ? "0" : txtCantidad.Text);
                oBESolD.IP_CENTRO = ddlcentro.SelectedValue.ToString();
                oBESolD.USER_REGISTRO = Session["IDE_USUARIO"].ToString();
                int dtrpta2 = 0;
                dtrpta2 = new BL_MOD_REQUERIMIENTO().uspINS_MOD_REQUERIMIENTO_DETALLE(oBESolD);

                cleanMessage = "Registro exitoso.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                //lblCodigo.Text = string.Empty;
                Listar();

            }
        }
    }
    protected void Listar()
    {

        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_MOD_REQUERIMIENTO_DETALLE_IDE_MOD(Convert.ToInt32(lblCodigo.Text));
        if (dtResultado.Rows.Count > 0)
        {
            //GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            //GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEliminar.CommandArgument);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;
        string IDE_REQUERIMIENTO = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        string IDE_MOD = GridView1.DataKeys[row.RowIndex].Values[1].ToString();

        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspDEL_MOD_REQUERIMIENTO_IDE(Convert.ToInt32(IDE_REQUERIMIENTO), Convert.ToInt32(IDE_MOD));
        Listar();
    }
    protected void Ver(object sender, ImageClickEventArgs e)
    {

        ImageButton btnVer = ((ImageButton)sender);
        int item = Convert.ToInt32(btnVer.CommandArgument);
        GridViewRow row = btnVer.NamingContainer as GridViewRow;
        string IDE_REQUERIMIENTO = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        string IDE_MOD = GridView1.DataKeys[row.RowIndex].Values[1].ToString();
        Session["IDE_REQUERIMIENTO"] = IDE_REQUERIMIENTO;
        Session["IDE_MOD"] = IDE_MOD;
        Response.Redirect("~/OPERACIONES/MOD_PERSONAL.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/MOD_BANDEJA.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["IDE_MOD"] = null;
        Session["IDE_MOD"] = string.Empty;
        Session.Remove("IDE_MOD");

        Session["IDE_REQUERIMIENTO"] = null;
        Session["IDE_REQUERIMIENTO"] = string.Empty;
        Session.Remove("IDE_REQUERIMIENTO");

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();

        string cleanMessage = string.Empty;

        if (ddlPersonal.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar nombre de personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_MOD_REQUERIMIENTO oBESol = new BE_MOD_REQUERIMIENTO();
            oBESol.IDE_MOD = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.CODIGO = string.Empty;
            oBESol.FECHA_REQUERIMIENTO = txtFecha.Text.Trim();
            oBESol.IP_CENTRO = ddlcentro.SelectedValue.ToString();
            oBESol.CENTRO = ddlcentro.SelectedValue.ToString();
            oBESol.USER_REGISTRO = Session["IDE_USUARIO"].ToString();

            int dtrpta = 0;
            dtrpta = new BL_MOD_REQUERIMIENTO().uspINS_MOD_REQUERIMIENTO(oBESol);
            if (dtrpta > 0)
            {
                lblCodigo.Text = dtrpta.ToString();
                dtResultado = obj.uspUPD_MOD_REQUERIMIENTO_RESPONSABLE(Convert.ToInt32(lblCodigo.Text), ddlPersonal.SelectedValue.ToString(), Convert.ToInt32(ddlResponsable.SelectedValue));

                CartaDatos(dtrpta.ToString());
                cleanMessage = "Registro exitoso.";
                ddlPersonal.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

        }
        
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //int value = Convert.ToInt32(DataBinder.Eval(GridView1.DataItem, "Your Columnname in the datasource")),


        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "DATOS REQUERIMIENTO";
            HeaderCell.ColumnSpan = 7;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);


           

            



            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }


    }
}