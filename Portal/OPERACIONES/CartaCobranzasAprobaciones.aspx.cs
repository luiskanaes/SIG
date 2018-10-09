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

public partial class OPERACIONES_CartaCobranzasAprobaciones : System.Web.UI.Page
{
    decimal dTotal = 0;
    DataTable dtPersonalOperaciones = new DataTable();
    DataTable dtPersonalDestino = new DataTable();
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnAnular);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardar);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaDestino.Text = DateTime.Now.ToString("dd/MM/yyyy");
            gerencias();

            if (Session["IDE_CARTA_APROBAR"] != null)
            {
                string IDE_CARTA = Session["IDE_CARTA_APROBAR"].ToString();
                string IDE_APROBACION = Session["IDE_APROBACION"].ToString();
                if (IDE_CARTA.Length > 0)
                {
                    CartaDatos(Session["IDE_CARTA_APROBAR"].ToString());
                }

            }
        }

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void CartaDatos(string IDE_CARTA)
    {
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_ID(Convert.ToInt32(IDE_CARTA));
        if (dtResultado.Rows.Count > 0)
        {

            lblcodigo.Text = dtResultado.Rows[0]["IDE_CARTA"].ToString();
            txtFecha.Text = dtResultado.Rows[0]["C_FECHA"].ToString();
            txtFechaDestino.Text = dtResultado.Rows[0]["D_FECHA"].ToString();
            string D_IPE_CENTRO = dtResultado.Rows[0]["D_IPE_CENTRO"].ToString();
            string D_CENTRO = dtResultado.Rows[0]["D_CENTRO"].ToString();
            //gerencias();
            ddlGerenciaDestino.SelectedValue = D_IPE_CENTRO;
            centros();
            ddlCentro.SelectedValue = D_CENTRO;

          
            lblpersonal.Text = dtResultado.Rows[0]["RESPONSABLE"].ToString();
            int dtrpta = Convert.ToInt32(lblcodigo.Text);
            ListarDetalle(dtrpta);
            txtNota.Text = dtResultado.Rows[0]["NOTA"].ToString();
            lblticket.Text = dtResultado.Rows[0]["TICKET"].ToString();
            lblOCosto.Text = dtResultado.Rows[0]["RESP_ING_OPE"].ToString();
            lbl1.Text = dtResultado.Rows[0]["ING_OPE"].ToString();

            lblOGerencia.Text = dtResultado.Rows[0]["RESP_GERENTE_OPE"].ToString();
            lbl2.Text = dtResultado.Rows[0]["GERENTE_OPE"].ToString();

            lblCostoDestino.Text = dtResultado.Rows[0]["RESP_ING_DEST"].ToString();
            lbl3.Text = dtResultado.Rows[0]["ING_DEST"].ToString();

    
            lblGerenciaDestino2.Text = dtResultado.Rows[0]["RESP_GERENTE_DEST"].ToString();
            lbl4.Text = dtResultado.Rows[0]["GERENTE_DEST"].ToString();

            lblMigerencia.Text = dtResultado.Rows[0]["C_IPE_CENTRO"].ToString();
            lblMicentro.Text = dtResultado.Rows[0]["C_CENTRO"].ToString();
            lblsolicitante.Text = dtResultado.Rows[0]["SOLICITA"].ToString();

            txtComentarios.Text = dtResultado.Rows[0]["COMENTARIOS"].ToString();
        }
    }
    

    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(4, "", Convert.ToInt32(BL_Session.ID_EMPRESA));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerenciaDestino.DataSource = dtResultado;
            ddlGerenciaDestino.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerenciaDestino.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerenciaDestino.DataBind();
            centros();
      
        }
        else
        {
            ddlGerenciaDestino.DataSource = dtResultado;
            ddlGerenciaDestino.DataBind();

            //ddlCentro.DataSource = dtResultado;
            //ddlCentro.DataBind();
        }

    }
    protected void ddlGerenciaDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        centros();


    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(5, ddlGerenciaDestino.SelectedValue.ToString(), Convert.ToInt32(BL_Session.ID_EMPRESA));

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

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    

    protected void ListarDetalle(int IDE_CARTA)
    {
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_DETALLE(IDE_CARTA);
        if (dtResultado.Rows.Count == 0)
        {

            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
            lblMonto.Text = string.Empty;
            //lblTotal.Text = string.Empty;
        }
        else
        {

            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
            //lblTotal.Text = dtResultado.Rows[0]["monto"].ToString();
            lblMonto.Text = "SON : " + dtResultado.Rows[0]["LETRA"].ToString();
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        string IDE_APROBACION = Session["IDE_APROBACION"].ToString();
        DataTable dtResultado = new DataTable();
        DataTable dt = new DataTable();
        BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
        dtResultado = obj.uspUPD_CARTA_COBRAZAS_APROBACIONES(Convert.ToInt32(Session["IDE_APROBACION"].ToString()),1,txtComentarios.Text.Trim ());
        if (dtResultado.Rows.Count > 0)
        {
            int TIPO_CARGO = Convert.ToInt32 ( dtResultado.Rows[0]["TIPO_CARGO"].ToString());
            int IDE_CARTA = Convert.ToInt32(dtResultado.Rows[0]["IDE_CARTA"].ToString());
            string  APROBADOR = dtResultado.Rows[0]["DNI_APRUEBA"].ToString();
            dt = obj.SP_CORREO_APROBACIONES_CARTACOBRANZA(IDE_CARTA, TIPO_CARGO, URLSSK, Session["IDE_USUARIO"].ToString(), APROBADOR, 1);
            if (dtResultado.Rows.Count > 0)
            {
                CartaDatos(Session["IDE_CARTA_APROBAR"].ToString());
                cleanMessage = "Aprobación satisfactoria";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
                
        }
        else
        {
            cleanMessage = "Carta cobranza " + lblticket.Text +", ya fue revisada";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }


    protected void btnAnular_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        if (txtComentarios.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar comentarios de rechazo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            string IDE_APROBACION = Session["IDE_APROBACION"].ToString();
            DataTable dtResultado = new DataTable();
            DataTable dt = new DataTable();
            BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
            dtResultado = obj.uspUPD_CARTA_COBRAZAS_APROBACIONES(Convert.ToInt32(Session["IDE_APROBACION"].ToString()), 0, txtComentarios.Text.Trim());
            if (dtResultado.Rows.Count > 0)
            {
                int TIPO_CARGO = Convert.ToInt32(dtResultado.Rows[0]["TIPO_CARGO"].ToString());
                int IDE_CARTA = Convert.ToInt32(dtResultado.Rows[0]["IDE_CARTA"].ToString());

                dt = obj.SP_CORREO_APROBACIONES_CARTACOBRANZA_TODOS(IDE_CARTA, TIPO_CARGO);
                CartaDatos(Session["IDE_CARTA_APROBAR"].ToString());
                cleanMessage = "Anulación procesada";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                cleanMessage = "Carta cobranza " + lblticket.Text + ", ya fue revisada";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }

    protected void btnAdjunto_Click(object sender, EventArgs e)
    {
        if (lblcodigo.Text != string.Empty)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup2(" + lblcodigo.Text + "," + 500 + "," + 320 + ");", true);
        }
        else
        {
            string cleanMessage = "Falta registrar carta cobranza";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void btnValidar_Click(object sender, EventArgs e)
    {

        string IDE_CARTA = Session["IDE_CARTA_APROBAR"].ToString();
        string IDE_APROBACION = Session["IDE_APROBACION"].ToString();


        Session["IDE_APROBACION"] = null;
        Session["IDE_APROBACION"] = string.Empty;

        Session["IDE_CARTA_APROBAR"] = null;
        Session["IDE_CARTA_APROBAR"] = string.Empty;

        Session.Remove("IDE_CARTA_APROBAR");
        Session.Remove("IDE_APROBACION");

        Response.Redirect("~/OPERACIONES/CartaCobranzasBandejaAprobaciones.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTAL"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = "Total:";
            e.Row.Cells[6].Text =  dTotal.ToString("###,###,##0.00");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }
}