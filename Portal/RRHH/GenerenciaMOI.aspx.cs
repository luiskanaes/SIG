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
using Microsoft.Reporting.WebForms;

public partial class RRHH_GenerenciaMOI : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDescarga);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        { 
            TipoManoObra();
        }

       
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
       
        string cleanMessage;
        if (txtInicio.Text == string.Empty || txtFin.Text == string.Empty)
        {
            cleanMessage = "Ingresar Periodo de Busqueda";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
           
        }
        else
        {
            DateTime inicio = Convert.ToDateTime(txtInicio.Text);
            DateTime fin = Convert.ToDateTime(txtFin.Text);
            if (inicio > fin)
            {
                cleanMessage = "El Periodo Fin no puede ser menor al Periodo Inicio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                DataTable dtResultado = new DataTable();
                dtResultado = GetData();
                if (dtResultado.Rows.Count > 0)
                {
                    btnDescarga.Visible = true;
                    GridView1.DataSource = dtResultado;
                    GridView1.DataBind();
                    rpt_Cuadro();

                }
                else
                {

                    btnDescarga.Visible = false ;
                }
            }
        }
    }
    protected void rpt_Cuadro()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RRHH/Reporte/Rpt_GenerenciaMOI.rdlc");

        DataTable dsCustomers = GetData();
        ReportDataSource datasource = new ReportDataSource("DataSet1", dsCustomers);


        ReportViewer1.LocalReport.DataSources.Clear();
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
       
        SqlCommand cmd  ;
        if (ddlTipoMano.SelectedValue == "370") {

            cmd = new SqlCommand("USP_CONTROL_MOI_REPORTE", con);
    
        }
        else {
            cmd = new SqlCommand("USP_CONTROL_MOD_REPORTE", con);
           
        }
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@FECHA_INI", SqlDbType.VarChar, 10).Value = txtInicio.Text;
        cmd.Parameters.Add("@FECHA_TER", SqlDbType.VarChar, 10).Value = txtFin.Text;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }

    protected void btnDescarga_Click(object sender, EventArgs e)
    {
        String tipo_mo = "";
        if (ddlTipoMano.SelectedValue == "370")
        { 
            tipo_mo = "MOI";
        }
        else
        { 
            tipo_mo = "MOD";
        }
        GridViewExportUtil.Export("Resumen_"+tipo_mo+"_"+ DateTime.Now + ".xls", GridView1);
        return;

    }

    protected void TipoManoObra()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlTipoMano.DataSource = obj.ListarParametros("TIPO_MANO", "RRHH_EMPLEADOS");
        ddlTipoMano.DataTextField = "DES_ASUNTO";
        ddlTipoMano.DataValueField = "ID_PARAMETRO";
        ddlTipoMano.DataBind();
    }
}