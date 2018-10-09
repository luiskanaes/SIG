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

public partial class Logistica_frmSolped : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExcel);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        //this.Master.RegisterTrigger(btnProcesar);
        if (!Page.IsPostBack)
        {
            Imputacion();
            Sociedad();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Moneda();
            //GrupoCompra();
            ListarCodigos();
            //ListarMateriales();
        }

    }
    //protected void Upnl_Load(object sender, EventArgs e)
    //{
    //    string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

    //    if (string.IsNullOrEmpty(eventTarget)) return;
    //    if (eventTarget.Equals("Upnl"))
    //    {
    //        var arg = Request.Params.Get("__EVENTARGUMENT");
    //        if (arg == null) return;
    //        if (arg.ToString().Equals("ddl"))
    //        {
    //            string strSelectedValue = ddl.SelectedValue;
    //            //Your code here ... 
    //        }
    //    }
    //}
    protected void ListarCodigos()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspCONSULTAR_LOG_SOLPED_USER(Session["IDE_USUARIO"].ToString ());
        if (dtResultado.Rows.Count > 0)
        {
            ddlCodigo.DataSource = dtResultado;
            ddlCodigo.DataTextField = dtResultado.Columns["COD_PEDIDO"].ToString();
            ddlCodigo.DataValueField = dtResultado.Columns["IDE_SOLICITUD"].ToString();
            ddlCodigo.DataBind();
            ddlCodigo.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlCodigo.Items.Clear();

            ddlCodigo.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Moneda()
    {
        ddlMoneda.DataSource = GetMoneda();
        ddlMoneda.DataTextField = "ValueMember";
        ddlMoneda.DataValueField = "DisplayMember";
        ddlMoneda.DataBind();
    }
    private DataTable GetMoneda()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add("0", "--- Seleccionar ---");
        dt.Rows.Add("PEN", "SOLES");
        dt.Rows.Add("USD", "DOLARES");
        dt.Rows.Add("CLP", "PESOS");
        return dt;
    }
    protected void Imputacion()
    {
        ddlImputacion.DataSource = GetImputacion();
        ddlImputacion.DataTextField = "ValueMember";
        ddlImputacion.DataValueField = "DisplayMember";
        ddlImputacion.DataBind();
    }
    private DataTable GetImputacion()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add("0", "--- Seleccionar ---");
        dt.Rows.Add("K", "K - OF. CENTRAL");
        dt.Rows.Add("Q", "Q - OBRA");
        return dt;

    }
    protected void Sociedad()
    {
        ddlSociedad.DataSource = GetSociedad();
        ddlSociedad.DataTextField = "ValueMember";
        ddlSociedad.DataValueField = "DisplayMember";
        ddlSociedad.DataBind();
        Obra();
        Centro();
        CentroCoste();
    }
    private DataTable GetSociedad()
    {
        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("DisplayMember"));
        dt.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values
        dt.Rows.Add("0", "--- Seleccionar ---");
        dt.Rows.Add("IP03", "SSK");
        dt.Rows.Add("IP04", "SKEX");
        return dt;

    }

    protected void Obra()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SEL_CECOS_POR_CATEGORIA_EMPRESA("OBRA", ddlSociedad.SelectedValue);
        if (ddlImputacion.SelectedValue == "K")
        {
            ddlObra.Items.Clear();
        }
        else
        {
            if (dtResultado.Rows.Count > 0)
            {
                ddlObra.DataSource = dtResultado;
                ddlObra.DataTextField = dtResultado.Columns["COD_CECOS"].ToString();
                ddlObra.DataValueField = dtResultado.Columns["COD_CECOS"].ToString();
                ddlObra.DataBind();

                ddlObra.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            }
        }
    }

    protected void ddlSociedad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Obra();
        Centro();
        CentroCoste();
    }

    protected void Centro()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();

        if (ddlSociedad.SelectedIndex > 0)
        {
            dtResultado = obj.SEL_CECOS_CENTRO_LOGISTICO(ddlSociedad.SelectedValue,ddlImputacion.SelectedValue );

            if (dtResultado.Rows.Count > 0)
            {
                ddlCentro.DataSource = dtResultado;
                ddlCentro.DataTextField = dtResultado.Columns["LOGISTICO"].ToString();
                ddlCentro.DataValueField = dtResultado.Columns["CENTRO"].ToString();
                ddlCentro.DataBind();
                ddlCentro.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
            }
        }
        else
        {
            ddlCentro.Items.Clear();
        }
    }
    protected void GrupoCompra()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.SEL_GRUPO_COMPRAS(ddlObra.SelectedValue.ToString() );
        if (dtResultado.Rows.Count > 0)
        {
            ddlGpoCompra.Visible = true;
            ddlGpoCompra.DataSource = dtResultado;
            ddlGpoCompra.DataTextField = dtResultado.Columns["DESCRIPCION"].ToString();
            ddlGpoCompra.DataValueField = dtResultado.Columns["ID"].ToString();
            ddlGpoCompra.DataBind();
            ddlGpoCompra.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlGpoCompra.Visible = false;
        }

        //if (ddlImputacion.SelectedIndex > 0)
        //{
        //    if (ddlImputacion.SelectedValue == "Q")
        //    {
        //        ddlGpoCompra.Items.Clear();
        //    }
        //    else
        //    {
        //        dtResultado = obj.SEL_GRUPO_COMPRAS();
        //        if (dtResultado.Rows.Count > 0)
        //        {
        //            ddlGpoCompra.DataSource = dtResultado;
        //            ddlGpoCompra.DataTextField = dtResultado.Columns["DESCRIPCION"].ToString();
        //            ddlGpoCompra.DataValueField = dtResultado.Columns["ID"].ToString();
        //            ddlGpoCompra.DataBind();
        //            ddlGpoCompra.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        //        }
        //    }
        //}
        //else
        //{
        //    ddlGpoCompra.Items.Clear();
        //}

       
    }
    protected void CentroCoste()
    {
        BL_CECOS obj = new BL_CECOS();
        DataTable dtResultado = new DataTable();
        if (ddlImputacion.SelectedIndex > 0)
        {
            if (ddlImputacion.SelectedValue == "Q")
            {
                ddlCoste.Items.Clear();
            }
            else
            {
                dtResultado = obj.SEL_CECOS_POR_CATEGORIA_EMPRESA("Deysu,Proyectos,Corporativo", ddlSociedad.SelectedValue);
                if (dtResultado.Rows.Count > 0)
                {
                    ddlCoste.DataSource = dtResultado;
                    ddlCoste.DataTextField = dtResultado.Columns["DESCRIPCION"].ToString();
                    ddlCoste.DataValueField = dtResultado.Columns["COD_CENTRO"].ToString();
                    ddlCoste.DataBind();
                    ddlCoste.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
                }
            }
        }
        else
        {
            ddlCoste.Items.Clear();
        }
    }

    protected void ddlImputacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlImputacion.SelectedIndex > 0)
        //{
        CentroCoste();
        Obra();
        //GrupoCompra();
        //}
    }

    protected void btnMateriales_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('Materiales.aspx','Title','width=775,height=480,left=10, top=10,scrollbars, resizable=no, location=0, toolbar=0')</script>", false);
    }

    private DataTable GetDataBusqueda()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_LOG_MATERIALES_BUSQUEDA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@IDE_MATERIAL", SqlDbType.VarChar, 150).Value = txtMaterial.Text.Trim();
        cmd.Parameters.Add("@DES_MATERIAL", SqlDbType.VarChar, 150).Value = txtDescripcion.Text.Trim();
        cmd.Parameters.Add("@UNIDAD", SqlDbType.VarChar, 150).Value = txtUnidad.Text.Trim();
        cmd.Parameters.Add("@GRUPO_ARTICULO", SqlDbType.VarChar, 150).Value = txtGrupo.Text.Trim();
        cmd.Parameters.Add("@CLASE_COSTE", SqlDbType.VarChar, 150).Value = "";
        cmd.Parameters.Add("@PEP", SqlDbType.VarChar, 150).Value = txtPep.Text.Trim();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void ListarMateriales()
    {
        BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
        DataTable dt = new DataTable();
        //dt = obj.SEL_LOG_MATERIALES_TODO();
        dt = GetDataBusqueda();
        if (dt.Rows.Count > 0)
        {
            lstRol.Visible = true;
            lstRol.DataSource = dt;
            lstRol.DataBind();
            ListarPep();
        }
        else
        {
            lstRol.Visible = false ;
            lstRol.DataSource = dt;
            lstRol.DataBind();
        }

    }

    private void ListarPep()
    {
        try
        {
            BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
            DataTable dtResultado = new DataTable();

            foreach (ListViewItem FilaFactor in lstRol.Items)
            {
                DropDownList ddlPep = ((DropDownList)FilaFactor.FindControl("ddlPep"));
                string codigo = lstRol.DataKeys[FilaFactor.DisplayIndex].Value.ToString();

                dtResultado = obj.uspSEL_LOG_MATERIALES_PEP_IDE_MATERIAL(codigo);

                ddlPep.DataSource = dtResultado;
                ddlPep.DataTextField = dtResultado.Columns["PEP_DETALLE"].ToString();
                ddlPep.DataValueField = dtResultado.Columns["PEP"].ToString();
                ddlPep.DataBind();

                //dtResultado.AsDataView
                //DataView dv = ds.Tables[0].DefaultView;
                DataView dv = new DataView(dtResultado);
                //dv.RowFilter = "FLG_CAMBIO='" + false + "'";
                foreach (DataRowView dr in dv)
                {
                    foreach (ListItem item in ddlPep.Items)
                    {
                        if ( Convert.ToBoolean ( dr["FLG_CAMBIO"].ToString()) == false )
                        {
                            item.Attributes.Add("style", "background-color:#3399FF;color:white;font-weight:bold;");
                        }
                        else
                        {
                            item.Attributes.Add("style", "background-color:#3399FF;color:red;font-weight:bold;");
                        }
                    }
                }

                //foreach (ListItem item in ddlPep.Items)
                //{

                //    string x = dtResultado.Columns["FLG_CAMBIO"].ToString();
                //    if (x == "0")
                //    {
                //        item.Attributes.Add("style", "background-color:#3399FF;color:white;font-weight:bold;");
                //    }
                //}
            }
            // ModalRegistro.Show();

           
        }
        catch (Exception ex)
        {
            UC_MessageBox.Show(Page, Page.GetType(), "Ocurrio un error al Consultar :" + ex.Message);
            return;
        }

    }

    protected void txtDescripcion_TextChanged(object sender, EventArgs e)
    {
        ListarMateriales();
    }

    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        ListarMateriales();
    }

    protected void txtUnidad_TextChanged(object sender, EventArgs e)
    {
        ListarMateriales();
    }

    protected void txtGrupo_TextChanged(object sender, EventArgs e)
    {
        ListarMateriales();
    }

  

    protected void txtPep_TextChanged(object sender, EventArgs e)
    {
        ListarMateriales();
    }
    protected void Seleccionar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnAdd = ((ImageButton)sender);
        int item = Convert.ToInt32(btnAdd.CommandArgument);


        ListViewItem itemx = lstRol.Items[item - 1];
        TextBox Cantidad = ((TextBox)itemx.FindControl("txtCantidad"));
        Label Material = ((Label)itemx.FindControl("lblMaterial"));
        DropDownList ddlPep = ((DropDownList)itemx.FindControl("ddlPep"));

        if (Cantidad.Text == string.Empty)
        {
            string cleanMessage = "Ingresar Cantidad";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_LOG_SOLPED_DETALLE oBESol = new BE_LOG_SOLPED_DETALLE();
            oBESol.IDE_PEDIDO = 0;
            oBESol.MATERIAL = Material.Text;
            oBESol.CANTIDAD = Convert.ToInt32(Cantidad.Text);
            oBESol.COD_PEDIDO = ddlCodigo.SelectedValue.ToString ();
            oBESol.IDE_SOLICITUD = Convert.ToInt32(lblId.Text);
            oBESol.PEP  = ddlPep.SelectedValue.ToString () ;
            int dtrpta = 0;
            dtrpta = new BL_LOG_SOLPED_DETALLE().INS_LOG_SOLPED_DETALLE(oBESol);
            if (dtrpta > 0)
            {
                //string cleanMessage = "Registro Exitoso";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                ListarPedido();
            }
        }

        //foreach (var item in lstRol.Items)
        //{

        //    TextBox Cantidad = ((TextBox)item.FindControl("txtCantidad"));
        //    String a = Cantidad.Text;
        //}
    }
    private BE_LOG_SOLPED f_CapturarDatosCabecera()
    {

        BE_LOG_SOLPED oBESol = new BE_LOG_SOLPED();

        oBESol.IDE_SOLICITUD = Convert.ToInt32(string.IsNullOrEmpty(lblId.Text) ? "0" : lblId.Text); ;
        oBESol.COD_PEDIDO = ddlCodigo.SelectedValue.ToString(); 
        oBESol.IMPUTACION = ddlImputacion.SelectedValue;
        oBESol.SOCIEDAD = ddlSociedad.SelectedValue;
        oBESol.OBRA = ddlObra.SelectedValue;
        oBESol.FECHA = txtFecha.Text;
        oBESol.VALOR = Convert.ToDecimal(string.IsNullOrEmpty(txtValor.Text) ? "0" : txtValor.Text);
        oBESol.MONEDA = ddlMoneda.SelectedValue;
        oBESol.CENTRO = ddlCentro.SelectedValue;
        oBESol.GR_COMPRA = ddlGpoCompra.SelectedValue;
        oBESol.CENTRO_COSTE = ddlCoste.SelectedValue;
        oBESol.SOLICITANTE = Session["IDE_USUARIO"].ToString();


        return oBESol;
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlImputacion.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar Imputacion";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if  (ddlSociedad.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar Sociedad";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlMoneda.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar Moneda";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlGpoCompra.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar Grupo Compra";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlCentro.SelectedIndex == 0)
        {
            cleanMessage = "Seleccionar Centro";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            if (ddlImputacion.SelectedValue =="K")
            {
                if (ddlCoste.SelectedIndex == 0)
                {
                    cleanMessage = "Seleccionar Centro de Coste";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else
                {
                    Grabar();
                }
            }
            else
            {
                if (ddlObra.SelectedIndex == 0)
                {
                    cleanMessage = "Seleccionar Obra";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else
                {
                    Grabar();
                }
            }
        }
    }
    protected void Grabar()
    {
        string cleanMessage = string.Empty;

        if (txtValor.Text == string.Empty)
        {
            cleanMessage = "Ingresar Valor";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BE_LOG_SOLPED oBESol = new BE_LOG_SOLPED();
            oBESol = f_CapturarDatosCabecera();
            DataTable dtrpta = new DataTable();
            dtrpta = new BL_LOG_SOLPED().Mant_Insert_LOG_SOLPED(oBESol);
            if (dtrpta.Rows.Count > 0)
            {
                cleanMessage = "Registro Exitoso";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                ListarCodigos();
                ddlCodigo.SelectedValue = dtrpta.Rows[0]["COD_PEDIDO"].ToString();
                lblId.Text = dtrpta.Rows[0]["IDE_SOLICITUD"].ToString();
             
                PanelPedidos.Visible = true;
                ListarPedido();
            }
            else
            {


            }
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.AppendHeader("Refresh", "");
        ListarCodigos();
        Imputacion();
        Sociedad();
        txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        Moneda();
        GrupoCompra();

        lblId.Text = "0";
       
        txtValor.Text = string.Empty;
        PanelPedidos.Visible = false;
        txtMaterial.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        txtUnidad.Text = string.Empty;
        txtGrupo.Text = string.Empty;
        
        txtPep.Text = string.Empty;
        ListarPedido();

        lstRol.DataSource = null;
    }
    protected void ListarPedido()
    {
        BL_LOG_SOLPED oBESol = new BL_LOG_SOLPED();
        DataTable dt = new DataTable();
        dt = oBESol.SEL_LISTAR_PEDIDOS_SOLPED(ddlCodigo.SelectedValue.ToString (), Convert.ToInt32(lblId.Text));
        if (dt.Rows.Count > 0)
        {
            GridPedidos.Visible = true;
            GridPedidos.DataSource = dt;
            GridPedidos.DataBind();
        }
        else
        {

            GridPedidos.Visible = false ;
            GridPedidos.DataSource = dt;
            GridPedidos.DataBind();

        }
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {


        PanelPedidos.Visible = false;
        txtMaterial.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        txtUnidad.Text = string.Empty;
        txtGrupo.Text = string.Empty;
      
        txtPep.Text = string.Empty;

        BL_LOG_SOLPED_DETALLE oBESol = new BL_LOG_SOLPED_DETALLE();
        DataTable dt = new DataTable();
        dt = oBESol.uspDEL_LOG_SOLPED_DETALLE_POR_ID(Convert.ToInt32(lblId.Text));
        ListarPedido();

    }
    protected void EliminarSolped(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDelete = ((ImageButton)sender);
        BL_LOG_SOLPED_DETALLE oBESol = new BL_LOG_SOLPED_DETALLE();
        DataTable dt = new DataTable();
        dt = oBESol.uspDELETE_LOG_SOLPED_DETALLE_ID(Convert.ToInt32(btnDelete.CommandArgument));
        ListarPedido();
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {


        BL_LOG_SOLPED oBESol = new BL_LOG_SOLPED();
        DataTable dtResultadoeExcel = new DataTable();
        if (lblId.Text == string.Empty )
        {
            string cleanMessage = "No existe solicitud pedido";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            Session["ID"] = lblId.Text;
            Session["CODIGO"] = ddlCodigo.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('RptSolped.aspx','Title','width=775,height=480,left=10, top=10,scrollbars, resizable=no, location=0, toolbar=0')</script>", false);
            //dtResultadoeExcel = oBESol.SEL_LISTAR_PEDIDOS_SOLPED(txtCodigo.Text, Convert.ToInt32(lblId.Text));


            //gvwExportar.DataSource = dtResultadoeExcel;
            //gvwExportar.DataBind();
            //if (gvwExportar.Rows.Count > 0)
            //{
            //    GridViewExportUtil.Export("Pedido_" + txtCodigo.Text + ".xls", gvwExportar);
            //    return;
            //}
            //else
            //{

            //    string cleanMessage = "No se encontraron registros";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
        }
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        BL_LOG_SOLPED oBESol = new BL_LOG_SOLPED();
        DataTable dtResultado = new DataTable();
        if (ddlCodigo.SelectedIndex <1)
        {
            string cleanMessage = "Ingresar Codigo Pedido";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            dtResultado = oBESol.uspCONSULTAR_SOLPED_LISTA_ID(Convert.ToInt32(ddlCodigo.SelectedValue.ToString()));
            if (dtResultado.Rows.Count > 0)
            {
                try
                {
                    ddlImputacion.SelectedValue = dtResultado.Rows[0]["IMPUTACION"].ToString();
                    ddlSociedad.SelectedValue = dtResultado.Rows[0]["SOCIEDAD"].ToString();

                    txtFecha.Text = dtResultado.Rows[0]["FECHA"].ToString();
                    lblId.Text = dtResultado.Rows[0]["IDE_SOLICITUD"].ToString();
                    txtValor.Text = dtResultado.Rows[0]["VALOR"].ToString();

                    ddlMoneda.SelectedValue = dtResultado.Rows[0]["MONEDA"].ToString();
                    string centro = dtResultado.Rows[0]["CENTRO"].ToString();
                    ddlCentro.SelectedValue = dtResultado.Rows[0]["CENTRO"].ToString();
                    Centro();
                    GrupoCompra();
                 

                    if (ddlImputacion.SelectedValue == "Q")
                    {
                        string obra = dtResultado.Rows[0]["OBRA"].ToString();
                        Obra();
                        ddlObra.SelectedValue = dtResultado.Rows[0]["OBRA"].ToString();
                    }

                    if (ddlImputacion.SelectedValue == "K")
                    {
                        string coste = dtResultado.Rows[0]["CENTRO_COSTE"].ToString();
                        CentroCoste();
                        ddlCoste.SelectedValue = dtResultado.Rows[0]["CENTRO_COSTE"].ToString();
                    }

                    ddlGpoCompra.SelectedValue = dtResultado.Rows[0]["GR_COMPRA"].ToString();

                    ListarPedido();
                    PanelPedidos.Visible = true;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                string cleanMessage = "No existe Nro de Pedido";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }


    }

    protected void btnAgregarAll_Click(object sender, ImageClickEventArgs e)
    {
        int dtrpta = 0;
        foreach (var item in lstRol.Items)
        {

            TextBox Cantidad = ((TextBox)item.FindControl("txtCantidad"));
            Label Material = ((Label)item.FindControl("lblMaterial"));
            DropDownList ddlPep = ((DropDownList)item.FindControl("ddlPep"));

            if (Cantidad.Text != string.Empty)
            {
              
                BE_LOG_SOLPED_DETALLE oBESol = new BE_LOG_SOLPED_DETALLE();
                oBESol.IDE_PEDIDO = 0;
                oBESol.MATERIAL = Material.Text;
                oBESol.CANTIDAD = Convert.ToInt32(Cantidad.Text);
                oBESol.COD_PEDIDO = ddlCodigo.SelectedValue.ToString(); 
                oBESol.IDE_SOLICITUD = Convert.ToInt32(lblId.Text);
                oBESol.PEP = ddlPep.SelectedValue.ToString();

                dtrpta = new BL_LOG_SOLPED_DETALLE().INS_LOG_SOLPED_DETALLE(oBESol);
                if (dtrpta > 0)
                {
                    dtrpta++;

                }
            }

        }
        if  (dtrpta > 0)
            {
            //string cleanMessage = "Registro Exitoso";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            ListarPedido();
        }

       
    }
    protected void RegistrarPep(object sender, ImageClickEventArgs e)
    {
       
        ImageButton btnAddPep = ((ImageButton)sender);
        int item = Convert.ToInt32(btnAddPep.CommandArgument);


        ListViewItem itemx = lstRol.Items[item - 1];
        Label Material = ((Label)itemx.FindControl("lblMaterial"));
       
        ModalRegistroPep.Show();


        BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
        DataTable dtResultado = new DataTable();

        lblIdMaterial.Text = Material.Text;

        dtResultado = obj.uspSEL_PEP_CTA_CONTABLE();
        if (dtResultado.Rows.Count > 0)
        {
            ddl.DataSource = dtResultado;
            ddl.DataTextField = dtResultado.Columns["PEP"].ToString();
            ddl.DataValueField = dtResultado.Columns["PEP"].ToString();
            ddl.DataBind();
        }


    }
    protected void ddlObra_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrupoCompra();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
        DataTable dtResultado = new DataTable();
        if (ddl.SelectedValue != string.Empty)
 
        {
            dtResultado = obj.uspUPDATE_LOG_MATERIALES_PEP(lblIdMaterial.Text, ddl.SelectedValue,"", Session["IDE_USUARIO"].ToString());
            ListarMateriales();
        }
        else
        {
          
        }
    }
}