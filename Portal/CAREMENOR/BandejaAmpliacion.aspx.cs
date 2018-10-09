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
using System.Data.SqlClient;
using System.Drawing;


public partial class CAREMENOR_BandejaAmpliacion : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CareMenor"].ToString());
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = btnBuscar.UniqueID;
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnImprimir);
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(GridView1);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            Permisos();
            Proyectos();
            Estados();

            Anios();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();


            ddlEstado.SelectedIndex = 1;
            Listar("", "", "", "", "", "");
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }

        //ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void Permisos()
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALQUILER", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count < 1)
        {
            Response.Redirect("~/CAREMENOR/EquiposMayoresAlquilerView.aspx");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void OnLoadRefresh(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void Proyectos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_ARRIENDO_CC("RESPONSABLE ALQUILER", Session["IDE_USUARIO"].ToString(), BL_Session.CENTRO_COSTO);
        if (dtResultado.Rows.Count > 0)
        {


            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "Proy_Nombre";
            ddlcentro.DataValueField = "Proy_Codigo";
            ddlcentro.DataBind();

            if (dtResultado.Rows.Count > 1)
            {
                ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
            }

            //Proveedor();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {


            string cleanMessage = "No cuenta con permisos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void Anios()
    {


        DataTable dtResultado = new DataTable();
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
    protected void Estados()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableEstados();
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstado.DataSource = dtResultado;
            ddlEstado.DataTextField = "descripcion";
            ddlEstado.DataValueField = "codigo";
            ddlEstado.DataBind();

        }
    }
    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));
        table.Rows.Add(0, "--- TODOS ---");
        table.Rows.Add(1, "EN PROCESO");
        table.Rows.Add(2, "ATENDIDO TERCEROS");
        table.Rows.Add(3, "ANULADO");
        table.Rows.Add(4, "ATENDIDO SSK");

        return table;
    }
    //protected void Listar()

    protected void OnLoad(object sender, EventArgs e)
    {
        filtros();
    }
    protected void Listar(string txtRequerimientosa_H,
    string txtFamilia_H,
    string txtSUBFAMILIA_H,
    string txtSOLPED_H,
    string txtPDC_H,
    string txtGPO_H)
    {
        string estado = string.Empty;
        if (ddlEstado.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstado.SelectedValue.ToString();
        }

        string Centro = string.Empty;
        int contarCC = Convert.ToInt32(ddlcentro.Items.Count.ToString());
        if (contarCC <= 1)
        {
            Centro = ddlcentro.SelectedValue.ToString();
        }
        else
        {
            if (ddlcentro.SelectedIndex == 0)
            {
                Centro = string.Empty;
            }
            else
            {
                Centro = ddlcentro.SelectedValue.ToString();
            }
        }

        estado = ddlEstado.SelectedValue.ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR("", estado, ddlanio.SelectedValue.ToString(), Centro,
        txtRequerimientosa_H,
        txtFamilia_H,
        txtSUBFAMILIA_H,
        txtSOLPED_H,
        txtPDC_H,
        txtGPO_H,
        "1","");
        if (dtResultado.Rows.Count > 0)
        {
            lblCantidad.Text = "Cantidad : " + dtResultado.Rows.Count.ToString();
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {

            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }





    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //int value = Convert.ToInt32(DataBinder.Eval(GridView1.DataItem, "Your Columnname in the datasource")),


        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell = new TableCell();
            //HeaderCell.Text = "Datos de alquiler";
            //HeaderCell.ColumnSpan = 14;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 0; i <= 18; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Datos solicitud de SOLPED";
            //HeaderCell.ColumnSpan = 9;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#626567");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 19; i <= 28; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#626567");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Regularizaciones";
            //HeaderCell.ColumnSpan = 4;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#5fbf6f");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 29; i <= 32; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E8449");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Información SOLPED";
            //HeaderCell.ColumnSpan = 7;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#922B21");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 33; i <= 41; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#922B21");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }





            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Datos PDC";
            //HeaderCell.ColumnSpan = 8;
            //HeaderCell.BackColor =  System.Drawing.ColorTranslator.FromHtml("#17202A");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 42; i <= 51; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Datos de arriendo";
            //HeaderCell.ColumnSpan = 12;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#512E5F");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);
            for (int i = 52; i <= 66; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#512E5F");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }
            //GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "FLG_AMPLIACION")) == true)//Una forma de comparar el valor. En este caso un bit en SQL
            {


                //e.Row.BackColor = Color.FromName("#c6efce");
                e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(162, 37, 37);//Fondo
                e.Row.ControlStyle.ForeColor = System.Drawing.Color.Red;//Fuente
                e.Row.ControlStyle.BorderColor = System.Drawing.Color.Green;//Borde (quizá este te sea de ayuda)
                //e.Row.CssClass = "EstiloProcesado";//Estilo (quizá este también te sea de ayuda)

            }
        }
    }
    protected void Seleccionar(object sender, EventArgs e)
    {

        ImageButton btnSelect = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();

        string FLG_MENOR = "0";

        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "','" + FLG_MENOR + "'," + 1000 + "," + 400 + ");", true);


    }
    protected void RegistroLegajo(object sender, EventArgs e)
    {

        ImageButton btnLegajo = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();


        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupLegajo('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "'," + 1000 + "," + 400 + ");", true);


    }
    protected void SeleccionarPDC(object sender, EventArgs e)
    {

        ImageButton btnSelect = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupPDC('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "'," + 600 + "," + 520 + ");", true);

    }
    protected void SeleccionarObra(object sender, EventArgs e)
    {

        ImageButton btnobra = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup2('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "'," + 1000 + "," + 520 + ");", true);

    }
    protected void SOLPED(object sender, EventArgs e)
    {

        ImageButton btnSAP = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;


        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "PopupPosicion('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "'," + 500 + "," + 320 + ");", true);


        //lblRequ_Numero.Text = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        //lblReqd_CodLinea.Text = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        //lblReqs_Correlativo.Text = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();


        //BL_SOLPED obj = new BL_SOLPED();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALQUILER SOLPED");
        //if (dtResultado.Rows.Count > 0)
        //{

        //    BL_TBL_RequerimientoSubDetalle Xobj = new BL_TBL_RequerimientoSubDetalle();
        //    DataTable dt = new DataTable();
        //    dt = Xobj.LISTAR_GRUPO_LEGAJOFILE(lblRequ_Numero.Text, lblReqd_CodLinea.Text, lblReqs_Correlativo.Text, 1);
        //    dt.Rows.Count.ToString();

        //    if (dt.Rows.Count > 0)
        //    {

        //        GridReq.DataSource = dt;
        //        GridReq.DataBind();

        //        ModalRegistro.Show();
        //    }
        //    else
        //    {
        //        string cleanMessage = "Falta adjuntar documentación de legajos";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //    }

        //}
        //else
        //{
        //    string cleanMessage = "No tiene permisos para realizar esta operación";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
    }
    protected void Adjunto(object sender, EventArgs e)
    {
        ImageButton btnadjunto = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle Obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = Obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_ID(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        if (dtResultado.Rows.Count > 0)
        {
            if (dtResultado.Rows[0]["ADJUNTO"].ToString() == "1")
            {

                //Response.BinaryWrite(imageBuffer);
                //Response.End();
                Response.Redirect("~/CAREMENOR/DescargaAlquiler.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo);
                //ImgFoto.ImageUrl = "~/HandlerAlquilerSolicitud.ashx?ID=" + Nombre; // dtResultado.Rows[0]["icodpersonal"]; 


            }
        }
    }
    protected void EnviarSAP(object sender, EventArgs e)
    {

        ImageButton btnSOLPED = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        string CANTIDAD_LEGAJO = GridView1.DataKeys[grdrow.RowIndex].Values["CANTIDAD_LEGAJO"].ToString();
        string D_FLG_APRUEBA_CGO = GridView1.DataKeys[grdrow.RowIndex].Values["D_FLG_APRUEBA_CGO"].ToString();


        string url = URLSSK;

        string ADJUNTO = string.IsNullOrEmpty(CANTIDAD_LEGAJO) ? "0" : CANTIDAD_LEGAJO;

        if (Convert.ToInt32(ADJUNTO) == 0)
        {
            string cleanMessage = "Falta agregar documentos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {


            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();

            if (D_FLG_APRUEBA_CGO == "1")
            {
                //solicitar regularizacion

                string mensaje = "El Sistema de Equipos Menores SSK informa, se solicita la aprobación de esta lista de equipos por concepto de REGULARIZACIÓN para el proyecto ";
                obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER REGULARIZACION", url);


                string cleanMessage = "Envio satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                string mensaje = "El Sistema de Equipos Menores SSK informa, solicita la emisión SOLPED en SAP según la lista de equipos para el proyecto ";
                obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_CORREO(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, mensaje, "ALQUILER CARE SOLPED", url);

                string cleanMessage = "Envio satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }




            filtros();
        }


    }
    protected void EnviarSOL_PDC(object sender, EventArgs e)
    {

        ImageButton btnLegajo = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        string SOLPED = GridView1.DataKeys[grdrow.RowIndex].Values["D_SOLPED"].ToString();

        string url = URLSSK + "OPERACIONES/UrlPDC_EM?Usuario=" + Session["IDE_USUARIO"].ToString() + "&Requ_Numero=" + Requ_Numero.Trim() + "&Reqd_CodLinea=" + Reqd_CodLinea.Trim() + "&Reqs_Correlativo=" + Reqs_Correlativo;

        string cleanMessage = string.Empty;
        if (SOLPED.Length < 1)
        {
            cleanMessage = "Falta generar SOLPED";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }

        else
        {
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();
            string mensaje = "El Sistema de Equipos Menores SSK informa, se requiere la emisión de Pedido (PDC), según la SOLPED  N° " + SOLPED + ", del proyecto ";
            obj.USP_SEL_TBL_REQUERIMIENTO_CORREO_SOLPED_VARIOS(SOLPED, mensaje, "ALQUILER EQ MAYORES (PDC)", url);

            cleanMessage = "Envio satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            filtros();
        }

    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {

        filtros();


    }

    protected void filtros()
    {
        if (GridView1.Rows.Count > 0)
        {
            TextBox txtRequerimientosa_H = (TextBox)GridView1.HeaderRow.FindControl("txtRequerimientosa_H");
            TextBox txtFamilia_H = (TextBox)GridView1.HeaderRow.FindControl("txtFamilia_H");
            TextBox txtSUBFAMILIA_H = (TextBox)GridView1.HeaderRow.FindControl("txtSUBFAMILIA_H");
            TextBox txtSOLPED_H = (TextBox)GridView1.HeaderRow.FindControl("txtSOLPED_H");
            TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");
            TextBox txtGPO_H = (TextBox)GridView1.HeaderRow.FindControl("txtGPO_H");

            Listar(txtRequerimientosa_H.Text.Trim(), txtFamilia_H.Text.Trim(), txtSUBFAMILIA_H.Text.Trim(),
                    txtSOLPED_H.Text.Trim(),
                    txtPDC_H.Text.Trim(),
                    txtGPO_H.Text.Trim());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            Listar("", "", "", "", "", "");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }

    }




    protected void GuardarSalida(object sender, EventArgs e)
    {

        ImageButton btnSave = ((ImageButton)sender);

        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();

        TextBox txtSalida = (TextBox)grdrow.FindControl("txtSalida");

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        if (txtSalida.Text == string.Empty)
        {

            string cleanMessage = "Ingresar fecha de salida";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            if (IsDate(txtSalida.Text))
            {
                dtResultado = obj.uspUPD_TBL_RequerimientoSubDetalle_FECHA_SALIDA(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, txtSalida.Text.Trim());
                string cleanMessage = "Registro satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                filtros();
            }
            else
            {
                string cleanMessage = "Error! verificar fecha de salida";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

        }
    }
    public bool IsDate(object inValue)
    {
        bool bValid;
        try
        {
            DateTime myDT = DateTime.Parse(inValue.ToString());
            bValid = true;
        }
        catch (Exception e)
        {
            bValid = false;
        }

        return bValid;
    }


    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {

        filtros();
    }

    protected void ddlcentro_SelectedIndexChanged1(object sender, EventArgs e)
    {

        filtros();
    }


    protected void VerLegajos(object sender, EventArgs e)
    {

        LinkButton btnSelect = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();


        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupVerLegajos('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "'," + 900 + "," + 300 + ");", true);


    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        filtros();
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        string estado = string.Empty;
        if (ddlEstado.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstado.SelectedValue.ToString();
        }

        string Centro = string.Empty;
        if (ddlcentro.SelectedIndex == 0)
        {
            Centro = string.Empty;
        }
        else
        {
            Centro = ddlcentro.SelectedValue.ToString();
        }

        estado = ddlEstado.SelectedValue.ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultadoeExcel = new DataTable();



        TextBox txtRequerimientosa_H = (TextBox)GridView1.HeaderRow.FindControl("txtRequerimientosa_H");
        TextBox txtFamilia_H = (TextBox)GridView1.HeaderRow.FindControl("txtFamilia_H");
        TextBox txtSUBFAMILIA_H = (TextBox)GridView1.HeaderRow.FindControl("txtSUBFAMILIA_H");
        TextBox txtSOLPED_H = (TextBox)GridView1.HeaderRow.FindControl("txtSOLPED_H");
        TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtGPO_H");
        TextBox txtGPO_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");



        dtResultadoeExcel = obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR("", estado, ddlanio.SelectedValue.ToString(), Centro,
            txtRequerimientosa_H.Text.Trim(), txtFamilia_H.Text.Trim(), txtSUBFAMILIA_H.Text.Trim(),
                txtSOLPED_H.Text.Trim(),
                txtPDC_H.Text.Trim(), txtGPO_H.Text.Trim(),"1","");

        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();



            GridViewExportUtil.Export("ARRIENDOS_" + DateTime.Now + ".xls", gvExcel);
            return;

        }
        else
        {


        }
    }

    protected void btnActualizar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/EquiposMayoresAlquiler");
        //    Listar("", "", "", "", "", "");
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void txtRequerimientosa_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }

    protected void txtSUBFAMILIA_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }
    protected void txtGPO_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }

    protected void btnAmpliacion_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "PopupAmpliacion(" + 950 + "," + 500 + ");", true);

    }
}