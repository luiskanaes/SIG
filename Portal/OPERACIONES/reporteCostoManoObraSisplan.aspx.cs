using System;
using System.Data;
using System.Configuration; 
using System.Web.UI; 
using BusinessLogic; 
using System.Data.SqlClient; 
using DataAccess.Conexion;

public partial class OPERACIONES_reporteCostoManoObraSisplan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    public string WspUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnBuscador);
        if (!Page.IsPostBack)
        {
            Uri MyUrl = Request.Url;
            //Response.Write("URL Port: " + MyUrl+ "<br>");
            //Response.Write("URL Protocol: " + Server.HtmlEncode(MyUrl.Scheme) + "<br>");
            WspUrl = MyUrl.ToString();
            Empresas();
            CentroCostos();
            Anios();
            Meses();
            nroSemanas();
        }
        

    }

    protected void btnBuscador_Click(object sender, ImageClickEventArgs e)
    {
        exportarXLS();
    }

    protected void exportarXLS()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultadoeExcel = new DataTable();

        String danalistas = string.Empty;
        String destados = string.Empty;
        String dcargos = string.Empty;
        String dceco = string.Empty;
        String dmano = string.Empty;
        //lblSplit.Text = string.Empty; 

        //dtResultadoeExcel = obj.ConsultarControlReporte_MOI(txtInicio.Text, txtFin.Text, danalistas, dcargos, dceco, destados, dmano, dfecha);
        Util oUtilitarios = new Util();

        dtResultadoeExcel = oUtilitarios.EjecutaDatatable("dbo.SP_PLA_RPT_CMO_DETALLE", 1,ddlAnio.SelectedValue.ToString(), ddlMeses.SelectedValue.ToString(), ddlSemana.SelectedValue.ToString(), ddlCentro.SelectedValue.ToString());

        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();
             

            GridViewExportUtil.Export("CMO_" + ddlCentro.SelectedItem + ".xls", gvExcel);
            return;

        }
        else
        {

            string cleanMessage = "No existen data registrada";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }


    }
    protected void Empresas()
    {
        //BL_RO obj = new BL_RO();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.listar_empresa();

        //if (dtResultado.Rows.Count > 0)
        //{
        //    ddlEmpresas.DataSource = dtResultado;
        //    ddlEmpresas.DataTextField = dtResultado.Columns["DES_NOMBRE"].ToString();
        //    ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
        //    ddlEmpresas.DataBind();

        //}
    }

    protected void CentroCostos()
    {
        Util oUtilitarios = new Util();

        DataTable dt = new DataTable();
         
        dt  = oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_CENTROS_COSTOS");
         
        if (dt.Rows.Count > 0)
        {
            ddlCentro.DataSource = dt;
            ddlCentro.DataTextField = dt.Columns[2].ToString();
            ddlCentro.DataValueField = dt.Columns[1].ToString();
            ddlCentro.DataBind();

        }

    }
    protected void Anios()
    {
        Util oUtilitarios = new Util();

        DataTable dt = new DataTable();

        dt = oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_ANIOS");

        if (dt.Rows.Count > 0)
        {
            ddlAnio.DataSource = dt;
            ddlAnio.DataTextField = dt.Columns[2].ToString();
            ddlAnio.DataValueField = dt.Columns[1].ToString();
            ddlAnio.DataBind();

        }




    }
    protected void Meses()
    {

        Util oUtilitarios = new Util();

        DataTable dt = new DataTable();

        dt = oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_MESES");

        if (dt.Rows.Count > 0)
        {
            ddlMeses.DataSource = dt;
            ddlMeses.DataTextField = dt.Columns[2].ToString();
            ddlMeses.DataValueField = dt.Columns[1].ToString();
            ddlMeses.DataBind();

        }

    }
    protected void nroSemanas()
    {
        Util oUtilitarios = new Util();

        DataTable dt = new DataTable();

        dt = oUtilitarios.EjecutaDatatable("dbo.USP_LISTAR_SEMANAS");

        if (dt.Rows.Count > 0)
        {
            ddlSemana.DataSource = dt;
            ddlSemana.DataTextField = dt.Columns[2].ToString();
            ddlSemana.DataValueField = dt.Columns[1].ToString();
            ddlSemana.DataBind();

        }

    }
}