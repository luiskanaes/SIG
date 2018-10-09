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
using Excel = Microsoft.Office.Interop.Excel;
using Excel_Worksheet = Microsoft.Office.Interop.Excel.Worksheet;
using Microsoft.Reporting.WebForms;
public partial class Logistica_ReporteGlobal_Consulta : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(RdOpcion);
        //this.Master.RegisterTrigger(btnStatus);
        if (!Page.IsPostBack)
        {
            ListarEmpresas();
           // ListarProyectos();
        }
    }

    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Logistica/ReporteGlobal_Carga.aspx");
    }
    protected void ListarProyectos()
    {

        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtb = new DataTable();
        dtb = obj.Listar_Proyectos_ReporteGlobal(ddlEmpresa.SelectedValue);
        if (dtb.Rows.Count > 0)
        {
           
            DataTable dtbx = new DataTable();
            dtbx = obj.Listar_Empresa_ReporteGlobal();
            lblUpdate.Text = dtbx.Rows[Convert.ToInt32(ddlEmpresa.SelectedIndex)]["FECHA_UPDATE"].ToString();
            
        

            cblProyectoList.DataSource = dtb;
            cblProyectoList.DataTextField = dtb.Columns["PROYECTO"].ToString();
            cblProyectoList.DataValueField = dtb.Columns["ID"].ToString();
            cblProyectoList.DataBind();


            //foreach (ListItem item in cblProyectoList.Items)
            //{
            //    item.Selected = true;

            //}

            //String datos = string.Empty;
            //lblSplitProyecto.Text = string.Empty;
            //foreach (ListItem li in cblProyectoList.Items)
            //{
            //    if (li.Selected)
            //    {

            //        datos += li.Value + ",";
            //    }
            //    else
            //    {

            //    }

            //}
            //lblSplitProyecto.Text = datos;

            ListarStatus();
            //ListarComprador();
            
        }
        else
        {

            cblProyectoList.DataSource = null;
            cblProyectoList.DataBind();

        }

    }
    protected void ListarStatus()
    {

        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtb = new DataTable();
        dtb = obj.Listar_Status_ReporteGlobal(ddlEmpresa.SelectedValue, lblSplitProyecto.Text );
        if (dtb.Rows.Count > 0)
        {

            cblCustomerList.DataSource = dtb;
            cblCustomerList.DataTextField = dtb.Columns["STATUSS"].ToString();
            cblCustomerList.DataValueField = dtb.Columns["ESTADO"].ToString();
            cblCustomerList.DataBind();
        }
        else
        {

            cblCustomerList.DataSource = null;
            cblCustomerList.DataBind();

        }

    }
    protected void ListarComprador()
    {

        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtb = new DataTable();
        dtb = obj.Listar_Compradores(ddlEmpresa.SelectedValue, lblSplitProyecto.Text );
        if (dtb.Rows.Count > 0)
        {

            cblCompradorList.DataSource = dtb;
            cblCompradorList.DataTextField = dtb.Columns["Numero_de_necesidad"].ToString();
            cblCompradorList.DataValueField = dtb.Columns["ID"].ToString();
            cblCompradorList.DataBind();

            //foreach (ListItem item in cblCompradorList.Items)
            //{
            //    item.Selected = true;

            //}
        }
        else
        {

            cblCompradorList.DataSource = null;
            cblCompradorList.DataBind();

        }

    }

    protected void ListarEmpresas()
    {
        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtb = new DataTable();
        dtb = obj.Listar_Empresa_ReporteGlobal();
        if (dtb.Rows.Count > 0)
        {
            ddlEmpresa.DataSource = dtb;
            ddlEmpresa.DataTextField = dtb.Columns["EMPRESA"].ToString();
            ddlEmpresa.DataValueField = dtb.Columns["EMPRESA"].ToString();
            ddlEmpresa.DataBind();


            //ListarStatus();
            ListarProyectos();

        }
    }
    /*
    protected void exportarXLS_Plantilla()
    {


        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtbReporte = new DataTable();

        dtbReporte = obj.ListarReporte_ReporteGlobal(ddlEmpresa.SelectedValue, lblSplitProyecto.Text  );

        int Col; int Fil;

        try
        {
            Excel.Application excelApp = new Excel.Application();



            string workbookPath = Server.MapPath("~/Plantilla/ReporteGlobal.xltx");

            Excel.Workbook excelWorkbook = null;

            try
            {
                excelWorkbook = excelApp.Workbooks.Open(workbookPath, 0,
                    false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true,
                    false, 0, true, false, false);
            }
            catch
            {

                excelWorkbook = excelApp.Workbooks.Add();
            }


            Excel.Sheets excelSheets = excelWorkbook.Worksheets;

            string currentSheet = "Reporte General";
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);

            // The following gets cell A1 for editing
            //Excel.Range excelCell = (Excel.Range)excelWorksheet;

            if (dtbReporte.Rows.Count > 0)
            {

                Col = 1;
                Fil = 2;

                foreach (DataRow objRow in dtbReporte.Rows)
                {


                    ((Excel.Range)excelWorksheet.Cells[Fil, Col]).Value = objRow[0];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 1]).Value = objRow[1];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 2]).Value = objRow[2];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 3]).Value = objRow[3];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 4]).Value = objRow[4];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 5]).Value = objRow[5];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 6]).Value = objRow[6];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 7]).Value = objRow[7];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 8]).Value = objRow[8];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 9]).Value = objRow[9];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 10]).Value = objRow[10];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 11]).Value = objRow[11];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 12]).Value = objRow[12];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 13]).Value = objRow[13];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 14]).Value = objRow[14];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 15]).Value = objRow[15];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 16]).Value = objRow[16];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 17]).Value = objRow[17];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 18]).Value = objRow[18];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 19]).Value = objRow[19];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 20]).Value = objRow[20];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 21]).Value = objRow[21];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 22]).Value = objRow[22];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 23]).Value = objRow[23];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 24]).Value = objRow[24];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 25]).Value = objRow[25];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 26]).Value = objRow[26];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 27]).Value = objRow[27];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 28]).Value = objRow[28];
                    ((Excel.Range)excelWorksheet.Cells[Fil, Col + 29]).Value = objRow[29];

                    //.Cells[Fil, Col + 30] = objRow.Item[30)
                    //.Cells[Fil, Col + 31] = objRow.Item[31)



                    Fil = Fil + 1;

                }
                excelApp.Visible = true;
                excelWorksheet = null;
                excelWorkbook = null;
                return;

            }
            else
            {

                UC_MessageBox.Show(Page, this.GetType(), "Error al realizar calculo en Reporte Global.");
                return;

            }

        }
        catch (Exception ex)
        {

            UC_MessageBox.Show(Page, this.GetType(), ex.Message);
            return;

        }
        finally
        {


        }

    }
     * */
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarProyectos();
        ListarStatus();
        
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        //exportarXLS_Plantilla();
        String datos = string.Empty;
       
        foreach (ListItem li in cblCompradorList.Items)
        {
            if (li.Selected)
            {

                datos += li.Value + ",";
            }
            else
            {

            }

        }
        lblSplitComprador.Text = datos;

        String datos2 = string.Empty;
   
        foreach (ListItem li in cblProyectoList.Items)
        {
            if (li.Selected)
            {

                datos2 += li.Value + ",";
            }
            else
            {

            }

        }
        lblSplitProyecto.Text = datos2;

        if (RdOpcion.SelectedIndex == 0)
        {
            if (lblSplitProyecto.Text == string.Empty )
            {
                string cleanMessage = "Falta Seleccionar Proyecto ";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                rpt_Cuadro("1");
            }
        }
        else
        {
            if (lblSplitComprador.Text == string.Empty)
            {
                string cleanMessage = "Falta Seleccionar Comprador";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                rpt_Cuadro("2");
            }
        }
        
    }

    protected void exportarXLS()
    {

        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dtResultadoeExcel = new DataTable();
        dtResultadoeExcel = obj.ListarSoped_ReporteGlobal(ddlEmpresa.SelectedValue, lblSplit.Text, lblSplitProyecto.Text );

        gvwAsignacion.DataSource = dtResultadoeExcel;
        gvwAsignacion.DataBind();
        if (gvwAsignacion.Rows.Count > 0)
        {
            GridViewExportUtil.Export("RPT_STATUS"  + ".xls", gvwAsignacion);
            return;
        }
        else
        {
            //UC_MessageBox.Show(Page, this.GetType(), "Se encontraron errores al exportar la carga de Reporte");
            //return;
        }
    }
    protected void rpt_Cuadro(string tipo)
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Logistica/reportes/Rpt_ReporteGlobal.rdlc");

        BL_LOGISTICA obj = new BL_LOGISTICA();
        DataTable dsCustomers = new DataTable();
        dsCustomers = obj.ListarReporteGlobal(ddlEmpresa.SelectedValue, lblSplitProyecto.Text, lblSplitComprador.Text, tipo);   
        //DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DsReporteGlobal", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_REPORTE_GLOBAL_LISTAR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 5).Value = ddlEmpresa.SelectedValue;
        cmd.Parameters.Add("@PROYECTO", SqlDbType.VarChar, 256).Value = lblSplitProyecto.Text  ;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
   
    protected void btnStatus_Click(object sender, EventArgs e)
    {

        String datos = string.Empty;
        lblSplit.Text = string.Empty;
        if (cblCustomerList.SelectedIndex != -1)
        {


            foreach (ListItem li in cblCustomerList.Items)
            {
                if (li.Selected)
                {

                    datos += li.Value + ",";
                }
                else
                {

                }

            }
            lblSplit.Text = datos;
            exportarXLS();
        }
        else
        {
          
            string cleanMessage = "Debe Seleccionar algun Status";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        lblSplit.Text = datos;

    }
    protected void cblProyectoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        String datos = string.Empty;
        lblSplit.Text = string.Empty;
        foreach (ListItem li in cblProyectoList.Items)
        {
            if (li.Selected)
            {

                datos += li.Value + ",";
            }
            else
            {

            }

        }
        lblSplitProyecto.Text = datos;
        ListarComprador();
        ListarStatus();
    }
    protected void RdOpcion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdOpcion.SelectedIndex == 0)
        {
            ListarProyectos();
        }
        else
        {
            ListarComprador();
        }
    }
}