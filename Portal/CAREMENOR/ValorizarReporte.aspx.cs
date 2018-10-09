using BusinessEntity;
using BusinessLogic;
using UserCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;


public partial class CAREMENOR_ValorizarReporte : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CareMenor"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            Anios();
            Meses();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();

            ddlMes.SelectedValue = DateTime.Today.Month.ToString();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void Proveedor()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        string Centro = string.Empty;
        if (ddlcentro.SelectedIndex == 0)
        {
            Centro = string.Empty;
        }
        else
        {
            Centro = ddlcentro.SelectedValue.ToString();
        }

        dtResultado = obj.USP_SEL_TBL_VAL_PROVEEDOR_CONSOLIDADO(Centro, ddlanio.SelectedValue.ToString(), ddlMes.SelectedValue.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ddlProveedor.DataSource = dtResultado;
            ddlProveedor.DataTextField = "Proveedor";
            ddlProveedor.DataValueField = "Prov_RUC";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
        else
        {
            ddlProveedor.DataSource = dtResultado;
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }

        rpt_cuadro();
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
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();
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
    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {

        Proyectos();
        //rpt_cuadro();
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
   
        Proyectos();
        //rpt_cuadro();
    }
    protected void Proyectos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_VALORIZACION_CC_CONSOLIDADO("ALQUILER VALORIZACION", Session["IDE_USUARIO"].ToString(),  Convert.ToInt32(ddlanio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            //Panel1.Visible = true;
            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "Proy_Nombre";
            ddlcentro.DataValueField = "Proy_Codigo";
            ddlcentro.DataBind();
            if (dtResultado.Rows.Count > 1)
            {
                ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
            }
            Proveedor();
        }
        else
        {
            //Panel1.Visible = false;

        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void ddlcentro_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Proveedor();
        rpt_cuadro();
    }


    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        Proyectos();
        //rpt_cuadro();
    }

    protected void rpt_cuadro()
    {
        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/OPERACIONES/Reportes/RptMDP.rdlc");
        string NOMBRE_CORTO = string.Empty;
        string ANIO = string.Empty;
        string MES = string.Empty;
        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.Clear();



            //ReportParameter param = new ReportParameter("Path", Server.MapPath(@FolderFirmas + "44085236.jpg"), true);
            ////this.ReportViewer1.DataBind(); // Added
            //this.ReportViewer1.LocalReport.SetParameters(param);

            NOMBRE_CORTO = dsCustomers.Rows[0]["Proy_Codigo"].ToString();
            ANIO = ddlanio.SelectedValue.ToString();
            MES = ddlMes.SelectedValue.ToString();
           

            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.Reset();

        

            

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = ReportViewer1.LocalReport;
            rep.ReportPath = Server.MapPath("~/CAREMENOR/Reportes/RptValorizacion.rdlc");
          





            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

            //Warning[] warnings;
            //string[] streamIds;
            //string mimeType = string.Empty;
            //string encoding = string.Empty;
            //string extension = string.Empty;
            //DataSet dsGrpSum, dsActPlan, dsProfitDetails,
            //    dsProfitSum, dsSumHeader, dsDetailsHeader, dsBudCom = null;

            //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename=VAL_" + NOMBRE_CORTO + "_ANIO_" + ANIO + "_MES_" + MES + "." + extension);
            //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            //Response.Flush(); // send it to the client to download  
            //Response.End();


        }
        else
        {
           string cleanMessage = "No existe registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetData()
    {
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

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_RPT_TBL_VALORIZACION_CONSOLIDADO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@CENTRO", SqlDbType.VarChar, 20).Value = Centro;
        cmd.Parameters.Add("@Prov_RUC", SqlDbType.VarChar, 20).Value = ddlProveedor.SelectedValue.ToString();
        cmd.Parameters.Add("@ANIO", SqlDbType.VarChar, 20).Value = Convert.ToInt32(ddlanio.SelectedValue);
        cmd.Parameters.Add("@MES", SqlDbType.VarChar, 20).Value = Convert.ToInt32(ddlMes.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }

    protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        rpt_cuadro();
    }

   
}