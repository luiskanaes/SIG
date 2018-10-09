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

public partial class OPERACIONES_TipoCambio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
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
            //Anio();
            int anio;
            anio = DateTime.Today.Year;
            txtAnio.Text = anio.ToString();
            Meses();
            ListarTC();

        }
    }
    protected void ListarTC()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarTipodeCambio( );
        if (dtResultado.Rows.Count > 0)
        {
            GridTc.DataSource = dtResultado;
            GridTc.DataBind();
        }
    
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
    private DataTable GetAnio()
    {

        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
        conn.Open();
        string query = "SELECT distinct INT_ANIO FROM CJI3_TC ORDER BY INT_ANIO DESC ";
        SqlCommand cmd = new SqlCommand(query, conn);

        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader());
        return dt;
    }
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtTc.Text == string.Empty || txtAnio.Text == string.Empty)
        {
            string cleanMessage = "No se permiten datos vacios";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            GrabarTC();
        }
    }
    protected void GrabarTC()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        int id = Convert.ToInt32(string.IsNullOrEmpty(lblIdTc.Text) ? "0" : lblIdTc.Text);
        dtResultado = obj.Registrar_CJI3_TC(id, Convert.ToDecimal(txtTc.Text), Convert.ToInt32(txtAnio.Text ), Convert.ToInt32(ddlMes.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            ListarTC();

            txtAnio.Text = string.Empty;
            txtTc.Text = string.Empty;
            lblIdTc.Text = string.Empty;

            string cleanMessage = "Registro Satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
       
    }
    protected void Seleccionar(object sender, ImageClickEventArgs e)
    {
        BL_CJI3 obj = new BL_CJI3();
        ImageButton btnEditar = ((ImageButton)sender);
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_CJI3_TC(Convert.ToInt32(btnEditar.CommandArgument));
        if (dtResultado.Rows.Count > 0)
        {

            lblIdTc.Text = dtResultado.Rows[0]["ID_TC"].ToString();
            txtTc.Text = dtResultado.Rows[0]["DEC_TC"].ToString();
            txtAnio.Text = dtResultado.Rows[0]["INT_ANIO"].ToString();
            ddlMes.Text = dtResultado.Rows[0]["INT_MES"].ToString();
            Meses();
        }
        
    }
}