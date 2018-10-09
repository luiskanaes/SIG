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

public partial class RRHH_SeguimientoReporte : System.Web.UI.Page
{
    public string ControlUsuario;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());

    //String fec_ini = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
    String fec_ini = "01/01/"+DateTime.Now.ToString("yyyy");
    String fec_fin = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = "";
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDescarga);
        if (Session["IDE_USUARIO"] == null)
        {
            id = (Request.QueryString["id"]);
        }
        else
        {
            id = Session["IDE_USUARIO"].ToString();
        }
        //ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            
         
        }

        consultar();
           
    }

    protected void consultar() {

            con.Open();
            //string query = "SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOI] R WHERE  YEAR([FEC_FECHA_APROBACION]) =" + ddlAnio.SelectedValue;
            string query = "exec sp_listar_gestion_cambio";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable t1 = new DataTable();

            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            con.Close();
            if (t1.Rows.Count > 0)
        {

            GridViewResultados.DataSource = t1;
            GridViewResultados.DataBind();
            btnDescarga.Visible = true;

        }
        else
        {
            GridViewResultados.DataSource = null;
            GridViewResultados.DataBind();
            btnDescarga.Visible = false;
        }
    }
    
    protected void exportarXLS()
    {

        con.Open();
        //string query = "SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOI] R WHERE  YEAR([FEC_FECHA_APROBACION]) =" + ddlAnio.SelectedValue;
        string query = "exec sp_listar_gestion_cambio";
        SqlCommand cmd = new SqlCommand(query, con);
        DataTable t1 = new DataTable();

        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(t1);
        }
        con.Close();
        if (t1.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = t1;
            gvExcel.DataBind();

            //GOP - GC - K15002 -

            DateTime fecha = DateTime.Now;



            GridViewExportUtil.Export("GOP-GC-K15002-"+ fecha.ToString("yyyyMMdd")+"-" + fecha.ToString("hhmm") + ".xls", gvExcel);
            return;

        }
        else
        {


        }


    }
    protected void btnDescarga_Click(object sender, EventArgs e)
    {
        exportarXLS();
    }
        

    protected void gvExcel_DataBound(object sender, EventArgs e)
    {

       
    }
    
    protected void gvExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[10].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[11].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[12].BackColor = System.Drawing.Color.Yellow;

      ////  e.Row.Cells[18].BackColor = System.Drawing.Color.Yellow; 

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text != "0")
            {
                for (int i = 0; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.Beige;
                }
            }
        }
    }
   
      
} 