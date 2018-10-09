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


public partial class CAREMENOR_ResumenAtencion : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CareMenor"].ToString());
    decimal dTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
      
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDescargar);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnOR);
        if (!Page.IsPostBack)
        {

            privilegios();
            
        }
       
    }
    protected void privilegios()
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALQUILER", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count < 1)
        {
            // string cleanMessage = "No cuenta con permisos";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            HdCC.Value = BL_Session.CENTRO_COSTO.ToString();
            rpt_cuadroOR(BL_Session.CENTRO_COSTO);
            rpt_cuadro(BL_Session.CENTRO_COSTO);
           
        }
        else
        {
            BL_TBL_RequerimientoSubDetalle objx = new BL_TBL_RequerimientoSubDetalle();
            DataTable dt = new DataTable();
            HdCC.Value = string.Empty ;
            rpt_cuadroOR("");
            rpt_cuadro("");
           
        }
    }
    protected void rpt_cuadro(string CC)
    {
        
        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CAREMENOR/reportes/RptStatusSat.rdlc");

        DataTable dsCustomers = GetData(CC);
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);

        if (dsCustomers.Rows.Count > 0)
        {
            btnDescargar.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        else
        {
            btnDescargar.Visible = false;
            ReportViewer1.LocalReport.DataSources.Clear();

        }
    }
    protected void rpt_cuadroOR(string CC)
    {
        ReportViewer2.LocalReport.Refresh();
        ReportViewer2.ProcessingMode = ProcessingMode.Local;
        ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/CAREMENOR/reportes/RptStatusSatOR.rdlc");

        DataTable dsCustomers2 = GetDataOR(CC);
        ReportDataSource datasource2 = new ReportDataSource("DataSet2", dsCustomers2);

        if (dsCustomers2.Rows.Count > 0)
        {
            //btnDescargar.Visible = true;
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datasource2);
            ReportViewer2.LocalReport.Refresh();
        }
        else
        {
            //btnDescargar.Visible = false;
            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
        }
    }
    private DataTable GetData(string CC)
    {
        
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_SEL_TBL_REQUERIMIENTO_RPT_RESUMEN_TODOS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@centro", SqlDbType.VarChar, 20).Value = CC;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetDataOR(string CC)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_SEL_TBL_REQUERIMIENTO_RPT_RESUMEN_TODOS_OR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 99999;
        cmd.Parameters.Add("@centro", SqlDbType.VarChar, 20).Value = CC;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //for (int i = 2; i < Convert.ToInt32(HdColumnas.Value); i++)
        //{
          
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        dTotal += Convert.ToDecimal(e.Row.Cells[i].Text);
        //    }

          

        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[1].Text = "Total:";
        //        //e.Row.Cells[i].Text = dTotal.ToString("c");
        //        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
        //        e.Row.Font.Bold = true;
        //    }
        //}
        
    }

    protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(String.Format("~/OPERACIONES/RptStatusReq.aspx?CENTRO_COSTO=", HdCC.Value));
       
    }



    protected void btnOR_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(String.Format("~/OPERACIONES/RptStatusReqOR.aspx?CENTRO_COSTO=", HdCC.Value));
    }
}