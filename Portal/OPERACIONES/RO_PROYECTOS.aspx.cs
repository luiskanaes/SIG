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
public partial class OPERACIONES_RO_PROYECTOS : System.Web.UI.Page
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
            Monedas();
            Estados();
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



            ddlEmpresaEditar.DataSource = dtResultado;
            ddlEmpresaEditar.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
            ddlEmpresaEditar.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresaEditar.DataBind();
        }
    }
    protected void Limpiar()
    {

        txtCodigo.Text = string.Empty;
        txtdescripcion.Text = string.Empty;
        txtContrato.Text = string.Empty;
        txtCliente.Text = string.Empty;
        txtInicio.Text = string.Empty;
        txtTermino.Text = string.Empty;
        txtProgramado.Text = string.Empty;
        txtTipoCambio.Text = string.Empty;
        txtMonto.Text = string.Empty;
        txtMontoContractual.Text = string.Empty;

    }
    private DataTable GetMoneda()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add("D", "Dolares Americanos");
        dt.Rows.Add("S", "Nuevos Soles");
        return dt;

    }
    private DataTable GetEstado()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add("1", "Ejecucion");
        dt.Rows.Add("2", "En Cierre");
        dt.Rows.Add("3", "Cerrado");
        return dt;

    }
    protected void Monedas()
    {
        ddlMoneda.DataSource = GetMoneda();
        ddlMoneda.DataTextField = "ValueMember";
        ddlMoneda.DataValueField = "DisplayMember";
        ddlMoneda.DataBind();
    }
    protected void Estados()
    {
        ddlEstado.DataSource = GetEstado();
        ddlEstado.DataTextField = "ValueMember";
        ddlEstado.DataValueField = "DisplayMember";
        ddlEstado.DataBind();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();

        string tipoCambio = string.IsNullOrEmpty(txtTipoCambio.Text) ? "0.00" : txtTipoCambio.Text;
        string Monto = string.IsNullOrEmpty(txtMonto.Text) ? "0.00" : txtMonto.Text;
        string MontoContractual = string.IsNullOrEmpty(txtMontoContractual.Text) ? "0.00" : txtMontoContractual.Text;
        string Estado = ddlEstado.SelectedValue;


        dtResultado = obj.registro_Proyectos(
            txtCodigo.Text,
            txtdescripcion.Text,
            txtContrato.Text,
            ddlMoneda.SelectedValue,
            txtCliente.Text,
            txtInicio.Text,
            txtTermino.Text,
            Convert.ToDecimal(tipoCambio),
            txtProgramado.Text,
            string.Empty,
            Session["IDE_USUARIO"].ToString(),
            Convert.ToInt32(ddlEmpresaEditar.SelectedValue),
            Estado,
            Convert.ToDecimal(Monto),
            Convert.ToDecimal(MontoContractual)
            );

        if (dtResultado.Rows.Count > 0)
        {
            string estado = dtResultado.Rows[0]["ESTADO"].ToString();
            if (estado == "1")
            {
                Limpiar();
                ListarProyectos();
                string cleanMessage = "Proyecto registrado correctamente";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
            else if (estado == "2")
            {
                Limpiar();
                ListarProyectos();
                string cleanMessage = "Proyecto Actualizado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                string cleanMessage = "Proyecto no registrado, Verificar Datos ingresados";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }

        }
        else
        {

            txtCodigo.Focus();
            UC_MessageBox.Show(Page, this.GetType(), "Proyecto no registrado, Verificar Datos ingresados");
            return;
        }
    }
    protected void btnListar_Click(object sender, EventArgs e)
    {

        ListarProyectos();

    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigo.Focus();
        ModalRegistro.Show();
        Limpiar();
    }
    protected void ListarProyectos()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarProyectos_RO(Convert.ToInt32(ddlEmpresas.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            GridProyectos.DataSource = dtResultado;
            GridProyectos.DataBind();
        }
        else
        {
            GridProyectos.DataSource = null;
            GridProyectos.DataBind();
            //    string cleanMessage = "No se registra informacion";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }


    }
    protected void Actualizar_Proyecto(object sender, ImageClickEventArgs e)
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();

        ImageButton btnEditar = ((ImageButton)sender);

        ModalRegistro.Show();

        dtResultado = obj.ListarProyecto_Individual_RO(btnEditar.CommandArgument);
        if (dtResultado.Rows.Count > 0)
        {

            txtCodigo.Text = dtResultado.Rows[0]["IDE_PROYECTO"].ToString();
            txtdescripcion.Text = dtResultado.Rows[0]["DES_NOMBRE"].ToString();
            txtContrato.Text = dtResultado.Rows[0]["DES_TIPO_CONTRATO"].ToString();
            string moneda = dtResultado.Rows[0]["DES_MONEDA"].ToString();
            ddlMoneda.SelectedValue = moneda;


            txtCliente.Text = dtResultado.Rows[0]["DES_CLIENTE"].ToString();
            txtInicio.Text = dtResultado.Rows[0]["FECHA_INICIO"].ToString();
            txtTermino.Text = dtResultado.Rows[0]["FECHA_TERMINO"].ToString();
            txtMonto.Text = dtResultado.Rows[0]["DEC_MONTO"].ToString();
            txtMontoContractual.Text = dtResultado.Rows[0]["DEC_MONTO_CONTRACTUAL"].ToString();
            txtProgramado.Text = dtResultado.Rows[0]["FECHA_CONTRALTUAL"].ToString();
            txtTipoCambio.Text = dtResultado.Rows[0]["DEC_TIPO_CAMBIO_ACTUAL"].ToString();
            string estado = dtResultado.Rows[0]["FLG_SITUACION"].ToString();
            ddlEstado.SelectedValue = estado;
        }
        else
        {
            Limpiar();
        }


    }
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        int intContador = 0;

        if (GridProyectos.Rows.Count == 0)
        {
            string cleanMessage = "No existe Registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }

        foreach (GridViewRow Fila in GridProyectos.Rows)
        {
            CheckBox ChkBoxCell = ((CheckBox)Fila.FindControl("chkEliminar"));
            if (ChkBoxCell.Checked == true)
            {
                intContador += 1;
            }
        }

        if (intContador == 0)
        {

            string cleanMessage = "Debe seleccionar al menos un registro.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }
        else
        {
            string Cod;
            foreach (GridViewRow row in GridProyectos.Rows)
            {
                BL_RO obj = new BL_RO();
                DataTable dtResultado = new DataTable();

                CheckBox ChkBoxCell = ((CheckBox)row.FindControl("chkEliminar"));

                if (ChkBoxCell.Checked)
                {
                    Cod = GridProyectos.DataKeys[row.RowIndex].Value.ToString(); // extrae key
                    obj.Eliminar_Proyectos(Cod);

                }
                ChkBoxCell = null;
            }
            ListarProyectos();
            string Message = "Proyectos Eliminados";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + Message + "');", true);
        }



    }
    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarProyectos();
    }
}