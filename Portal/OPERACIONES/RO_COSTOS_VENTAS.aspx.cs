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


public partial class OPERACIONES_RO_COSTOS_VENTAS : System.Web.UI.Page
{

    public string ControlUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            ControlBotones();
            Empresas();
            Previstos();
            ddlMes.SelectedValue = DateTime.Now.Month.ToString();
            Meses();
            Anio();
            //txtCodigo.Text = DateTime.Now.Month.ToString();
        }

    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = true;
            btnProyecto.Visible = true;
            btnReporte.Visible = true;
        }
        else
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = false;
            btnProyecto.Visible = false;
            btnReporte.Visible = true;
        }
    }
    protected void btnPEP_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PEP.aspx");
    }
    protected void btnProyecto_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PROYECTOS.aspx");
    }
    protected void btnMantenimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_COSTOS_VENTAS.aspx");
    }
    protected void btnReporte_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_REPORTE.aspx");
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
            ListarProyectos();
        }
    }
    protected void ListarProyectos()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        // lista solo los proyecto en cierre y ejecucion
        dtResultado = obj.ListarProyectos_Estados_RO(Convert.ToInt32(ddlEmpresas.SelectedValue), "1,2,");
        if (dtResultado.Rows.Count > 0)
        {
            ddlProyecto.Visible = true;
            btnAgregar.Visible = true;
            ddlProyecto.DataSource = dtResultado;
            ddlProyecto.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlProyecto.DataValueField = dtResultado.Columns["IDE_PROYECTO"].ToString();
            ddlProyecto.DataBind();

        }
        else
        {
            ddlProyecto.Visible = false;
            btnAgregar.Visible = false;
            lblProyecto.Text = string.Empty;
            //ddlProyecto.DataSource = null;
            //ddlProyecto.DataBind();

            string cleanMessage = "No se registra informacion";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }


    }
    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarProyectos();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        ModalRegistro.Show();
        lblMsg.Text = string.Empty;
        CheckPopup.Checked = false;
        //txtValor.Text = string.Empty;
        CheckDetalle.Checked = false;
        lblEstado.Text = "0";
        lblProyecto.Text = ddlProyecto.SelectedItem.ToString();
        Monto_RO();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {


    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        RegistrarRO();
        if (CheckPopup.Checked)
        {
            ModalRegistro.Show();
        }

    }
    protected void RegistrarRO()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        string Pep = string.Empty;
        if (lblEstado.Text != "1")
        {
            Pep = string.Empty;
        }
        else
        {
            Pep = ddlDetalle.SelectedValue;
        }
        string Valor = string.IsNullOrEmpty(txtValor.Text) ? "0.00" : txtValor.Text;
        string proyeccion = string.IsNullOrEmpty(txtProyeccion.Text) ? "0.00" : txtProyeccion.Text;
        string MontoInicio = string.IsNullOrEmpty(txtMontoInicio.Text) ? "0.00" : txtMontoInicio.Text;
        dtResultado = obj.registro_resultado_Ro(ddlProyecto.SelectedValue, Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue), Convert.ToDecimal(Valor), Convert.ToInt32(ddlIndicador.SelectedValue), Pep, Session["IDE_USUARIO"].ToString(), Convert.ToDecimal(proyeccion), Convert.ToDecimal(MontoInicio));
        if (dtResultado.Rows.Count > 0)
        {
            if (CheckPopup.Checked)
            {
                lblMsg.Visible = true;
                txtValor.Text = string.Empty;
                txtProyeccion.Text = string.Empty;
                txtMontoInicio.Text = string.Empty; 
                if (lblEstado.Text != "1")
                {
                    lblMsg.Text = "Indicador registrado correctamente : " + ddlIndicador.SelectedItem;
                }
                else
                {
                    lblMsg.Text = "Indicador registrado correctamente : " + ddlIndicador.SelectedItem + "(" + ddlDetalle.SelectedItem + ")";
                }


            }
            else
            {
                lblMsg.Visible = false;
                lblMsg.Text = string.Empty;
                string cleanMessage = "Indicador registrado correctamente";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                txtValor.Text = string.Empty;
                txtProyeccion.Text = string.Empty;
            }
        }
        else
        {

            string cleanMessage = "Error! Verificar los datos Ingresados";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }

    }
    protected void Previstos()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.previsto_Tipo();

        if (dtResultado.Rows.Count > 0)
        {
            ddlPrevisto.DataSource = dtResultado;
            ddlPrevisto.DataTextField = dtResultado.Columns["TIPO_PREVISTO"].ToString();
            ddlPrevisto.DataValueField = dtResultado.Columns["DES_TIPO_PREVISTO"].ToString();
            ddlPrevisto.DataBind();
            IndicadoresPEP();
        }
    }
    protected void IndicadoresPEP()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.previsto_ListaR(ddlPrevisto.SelectedValue);

        if (dtResultado.Rows.Count > 0)
        {
            ddlIndicador.DataSource = dtResultado;
            ddlIndicador.DataTextField = dtResultado.Columns["DES_PREVISTO"].ToString();
            ddlIndicador.DataValueField = dtResultado.Columns["IDE_PREVISTO"].ToString();
            ddlIndicador.DataBind();
            Listar_IndicadoresPEP();

        }
    }
    protected void Listar_IndicadoresPEP()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_PEP(Convert.ToInt32(ddlIndicador.SelectedValue));

        if (dtResultado.Rows.Count > 0)
        {
            CheckDetalle.Visible = true;
            ddlDetalle.DataSource = dtResultado;
            ddlDetalle.DataTextField = dtResultado.Columns["DES_NOMBRE_PEP"].ToString();
            ddlDetalle.DataValueField = dtResultado.Columns["IDE_PEP"].ToString();
            ddlDetalle.DataBind();

        }
        else
        {
            CheckDetalle.Visible = false;
            ddlDetalle.Visible = false;
        }
    }
    protected void ddlPrevisto_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalRegistro.Show();
        IndicadoresPEP();
        Monto_RO();

    }
    protected void ddlIndicador_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalRegistro.Show();
        Listar_IndicadoresPEP();
        txtValor.Focus();
        CheckDetalle.Checked = false;
        ddlDetalle.Visible = false;
        lblEstado.Text = "0";
        Monto_RO();
    }
    protected void CheckDetalle_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckDetalle.Checked)
        {
            ddlDetalle.Visible = true;
            lblEstado.Text = "1";
            ModalRegistro.Show();
            Listar_IndicadoresPEP();
        }
        else
        {
            ModalRegistro.Show();
            ddlDetalle.Visible = false;
            lblEstado.Text = "0";

        }
    }
    private DataTable GetMeses()
    {
        DataTable dtMes = new DataTable();

        //Add Columns to Table
        dtMes.Columns.Add(new DataColumn("DisplayMember"));
        dtMes.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dtMes.Rows.Add(1, "ENERO");
        dtMes.Rows.Add(2, "FEBRERO");
        dtMes.Rows.Add(3, "MARZO");
        dtMes.Rows.Add(4, "ABRIL");
        dtMes.Rows.Add(5, "MAYO");
        dtMes.Rows.Add(6, "JUNIO");
        dtMes.Rows.Add(7, "JULIO");
        dtMes.Rows.Add(8, "AGOSTO");
        dtMes.Rows.Add(9, "SETIEMBRE");
        dtMes.Rows.Add(10, "OCTUBRE");
        dtMes.Rows.Add(11, "NOVIEMBRE");
        dtMes.Rows.Add(12, "DICIEMBRE");

        return dtMes;

    }
    private DataTable GetAnio()
    {
        DataTable dtMes = new DataTable();

        //Add Columns to Table
        dtMes.Columns.Add(new DataColumn("DisplayMember"));
        dtMes.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dtMes.Rows.Add(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString());


        return dtMes;

    }
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();

    }
    protected void Anio()
    {
        ddlAnio.DataSource = GetAnio();
        ddlAnio.DataTextField = "ValueMember";
        ddlAnio.DataValueField = "DisplayMember";
        ddlAnio.DataBind();
    }
    protected void ddlDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalRegistro.Show();
        txtValor.Focus();
        Monto_RO();
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {

        ModalRegistro.Show();
        Monto_RO();
    }
    protected void Monto_RO()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        string Pep = string.Empty;
        if (lblEstado.Text != "1")
        {
            Pep = string.Empty;
        }
        else
        {
            Pep = ddlDetalle.SelectedValue;
        }

        //dtResultado = obj.monto_resultado_Ro(ddlProyecto.SelectedValue, Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue),  Convert.ToInt32(ddlIndicador.SelectedValue),Pep);
        dtResultado = obj.monto_resultado_Ro(ddlProyecto.SelectedValue, Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue), Convert.ToInt32(ddlIndicador.SelectedValue), Pep);

        if (dtResultado.Rows.Count > 0)
        {
            txtProyeccion.Text = Convert.ToDecimal(dtResultado.Rows[0]["MONTO_PROYECTADO"].ToString()).ToString();
            txtValor.Text = Convert.ToDecimal(dtResultado.Rows[0]["MONTO_REAL"].ToString()).ToString();
            txtMontoInicio.Text = Convert.ToDecimal(dtResultado.Rows[0]["MONTO_INICIO"].ToString()).ToString();
        }

    }
}