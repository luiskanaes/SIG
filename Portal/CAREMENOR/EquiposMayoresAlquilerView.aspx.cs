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
using System.Text;

public partial class CAREMENOR_EquiposMayoresAlquilerView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CareMenor"].ToString());
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnImprimir);
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(GridView1);

        Session.Remove("FileUpload1");
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            

            Proyectos();
            Estados();

            Anios();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();


            ddlEstado.SelectedIndex = 1;
            Listar("", "", "", "", "", "","");
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
            ddlanio.Items.Insert(0, new ListItem("--- TODOS ---", ""));
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
    string txtGPO_H,
    string txtOR_H)
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
        //if (ddlcentro.SelectedIndex == 0)
        //{
        //    Centro = string.Empty;
        //}
        //else
        //{
        //    Centro = ddlcentro.SelectedValue.ToString();
        //}
        Centro = BL_Session.CENTRO_COSTO.ToString();
        estado = ddlEstado.SelectedValue.ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR("", estado, ddlanio.SelectedValue.ToString(), Centro,
        txtRequerimientosa_H,
        txtFamilia_H,
        txtSUBFAMILIA_H,
        txtSOLPED_H,
        txtPDC_H,
        txtGPO_H, "", txtOR_H);
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

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //int value = Convert.ToInt32(DataBinder.Eval(GridView1.DataItem, "Your Columnname in the datasource")),


        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell = new TableCell();
            ////HeaderCell.Text = "Datos de alquiler";
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
            ////HeaderCell.Text = "Datos solicitud de SOLPED";
            //HeaderCell.ColumnSpan = 7;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#626567");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 19; i <= 25; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#626567");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            ////HeaderCell.Text = "Regularizaciones";
            //HeaderCell.ColumnSpan = 4;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#5fbf6f");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 26; i <= 29; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#5fbf6f");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            ////HeaderCell.Text = "Información SOLPED";
            //HeaderCell.ColumnSpan = 5;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#922B21");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 30; i <= 37; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#922B21");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }





            //HeaderCell = new TableCell();
            ////HeaderCell.Text = "Datos PDC";
            //HeaderCell.ColumnSpan = 7;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 38; i <= 45; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            //HeaderCell = new TableCell();
            ////HeaderCell.Text = "Datos de arriendo";
            //HeaderCell.ColumnSpan = 10;
            //HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#512E5F");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);
            for (int i = 46; i <= 57; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#512E5F");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }
            //GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
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
            TextBox txtOR_H = (TextBox)GridView1.HeaderRow.FindControl("txtOR_H");
            Listar(txtRequerimientosa_H.Text.Trim(), txtFamilia_H.Text.Trim(), txtSUBFAMILIA_H.Text.Trim(),
                    txtSOLPED_H.Text.Trim(),
                    txtPDC_H.Text.Trim(),
                    txtGPO_H.Text.Trim(), txtOR_H.Text.Trim());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            Listar("", "", "", "", "", "","");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

        filtros();
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

    protected void btnDescarga_Click(object sender, EventArgs e)
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
        TextBox txtOR_H = (TextBox)GridView1.HeaderRow.FindControl("txtOR_H");


        dtResultadoeExcel = obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR("", estado, ddlanio.SelectedValue.ToString(), Centro,
            txtRequerimientosa_H.Text.Trim(), txtFamilia_H.Text.Trim(), txtSUBFAMILIA_H.Text.Trim(),
                txtSOLPED_H.Text.Trim(),
                txtPDC_H.Text.Trim(), txtGPO_H.Text.Trim(),"", txtOR_H.Text.Trim());

        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();



            GridViewExportUtil.Export("ALQ_" + DateTime.Now + ".xls", gvExcel);
            return;

        }
        else
        {


        }
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

        string anio = string.Empty;
        if (ddlanio.SelectedIndex == 0)
        {
            anio = string.Empty;
        }
        else
        {
            anio = ddlanio.SelectedValue.ToString();


        }

       
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultadoeExcel = new DataTable();



        TextBox txtRequerimientosa_H = (TextBox)GridView1.HeaderRow.FindControl("txtRequerimientosa_H");
        TextBox txtFamilia_H = (TextBox)GridView1.HeaderRow.FindControl("txtFamilia_H");
        TextBox txtSUBFAMILIA_H = (TextBox)GridView1.HeaderRow.FindControl("txtSUBFAMILIA_H");
        TextBox txtSOLPED_H = (TextBox)GridView1.HeaderRow.FindControl("txtSOLPED_H");
        TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtGPO_H");
        TextBox txtGPO_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");
        TextBox txtOR_H = (TextBox)GridView1.HeaderRow.FindControl("txtOR_H");


        dtResultadoeExcel = obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR("", estado, anio, Centro,
            txtRequerimientosa_H.Text.Trim(), txtFamilia_H.Text.Trim(), txtSUBFAMILIA_H.Text.Trim(),
                txtSOLPED_H.Text.Trim(),
                txtPDC_H.Text.Trim(), txtGPO_H.Text.Trim(),"", txtOR_H.Text.Trim());

        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();



            GridViewExportUtil.Export("ALQ_" + DateTime.Now + ".xls", gvExcel);
            return;

        }
        else
        {


        }
    }

    protected void btnActualizar_Click(object sender, ImageClickEventArgs e)
    {
        Listar("", "", "", "", "", "","");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void btnAmpliacion_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "PopupAmpliacion(" + 1100 + "," + 500 + ");", true);
    }

    protected void ddlanio_SelectedIndexChanged1(object sender, EventArgs e)
    {
        filtros();
    }
}