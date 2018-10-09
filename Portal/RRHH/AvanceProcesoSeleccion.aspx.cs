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

public partial class RRHH_AvanceProcesoSeleccion : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {


            Session["PROCESO"] = "0";

            string query = "SELECT CONVERT(CHAR(10),MIN(R.[FECHA_CREACION]),103) AS FECHA FROM TMP_REQUERIMIENTO_PERSONAL R INNER JOIN TMP_DETALLE_REQUERIMIENTO_PERSONAL D "                           +
                    "ON R.ID_REQUERIMIENTO_PERSONAL = D.ID_REQUERIMIENTO_PERSONAL WHERE FLG_ESTADO_PROCESO IN (1,2)";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            con.Close();

            string fecha = t1.Rows[0]["FECHA"].ToString();
            txtInicio.Text = fecha;
            txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            rpt_Cuadro();
        }
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Session["PROCESO"] = "1";
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
                DataTable dtResultadoMOD = new DataTable();
                DataTable dtResultadoMOI = new DataTable();
                dtResultadoMOD = GetDataMOD();
                dtResultadoMOI = GetDataMOI();
                if (dtResultadoMOD.Rows.Count > 0 || dtResultadoMOI.Rows.Count > 0)
                {
                    //btnDescarga.Visible = true;

                    //GridViewMOD.DataSource = dtResultadoMOD;
                    //GridViewMOD.DataBind();

                    //GridViewMOI.DataSource = dtResultadoMOI;
                    //GridViewMOI.DataBind();

                    rpt_Cuadro();

                }
                else
                {

                    //btnDescarga.Visible = false;
                }
            }
        }
    }
    protected void rpt_Cuadro()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RRHH/Reporte/RptProcesos_Seleccion.rdlc");

        DataTable dsMOI = GetDataMOI();
        ReportDataSource datasourceMOI = new ReportDataSource("DataSet1", dsMOI);

        DataTable dsMOD = GetDataMOD();
        ReportDataSource datasourceMOD = new ReportDataSource("DataSet2", dsMOD);

        ReportViewer1.LocalReport.DataSources.Clear();
   
        ReportViewer1.LocalReport.DataSources.Add(datasourceMOI);
        ReportViewer1.LocalReport.DataSources.Add(datasourceMOD);
   
    }
    private DataTable GetDataMOI()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_CONTROL_SELECCION_REPORTE_AVANCES", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 10).Value = "MOI";
        cmd.Parameters.Add("@PROCESO", SqlDbType.Int).Value = Session["PROCESO"].ToString();
        cmd.Parameters.Add("@FECHA_INI", SqlDbType.VarChar, 10).Value = txtInicio.Text;
        cmd.Parameters.Add("@FECHA_TER", SqlDbType.VarChar, 10).Value = txtFin.Text;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetDataMOD()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("USP_CONTROL_SELECCION_REPORTE_AVANCES", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 10).Value = "MOD";
        cmd.Parameters.Add("@PROCESO", SqlDbType.Int).Value = Session["PROCESO"].ToString();
        cmd.Parameters.Add("@FECHA_INI", SqlDbType.VarChar, 10).Value = txtInicio.Text;
        cmd.Parameters.Add("@FECHA_TER", SqlDbType.VarChar, 10).Value = txtFin.Text;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}