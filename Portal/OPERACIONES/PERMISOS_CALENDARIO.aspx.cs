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
using System.Drawing;
using System.Data.SqlClient;

public partial class OPERACIONES_PERMISOS_CALENDARIO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    DataTable dtMes = new DataTable();
    DataTable dtUsuarios = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        int year = 0;
        int month = 0;
        //ScriptManager.GetCurrent(this).RegisterPostBackControl(Calendar1);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            lblCentro.Text  = BL_Session.CENTRO_COSTO;
            VerificarCC_asignado();
            ParametrosMotivo();
            DateTime miFecha = DateTime.Now;
             year = miFecha.Year;
             month = miFecha.Month;
            lblfecha.Text = "01/" + Convert.ToString(month).Trim().PadLeft(2, '0') + "/" + Convert.ToString(year);
        }
       
        dtMes = GetFechas(year, month);

        FechaLeyenda(year, month);

       
    }

    protected void VerificarCC_asignado()
    {
        BL_EQUIPO_TRABAJO obj = new BL_EQUIPO_TRABAJO();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_EQUIPO_TRABAJO_DNI(Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            lblCentro.Text = dtResultado.Rows[0]["CC_SUPERVISOR"].ToString();
            
        }
        else
        {
            lblCentro.Text = BL_Session.CENTRO_COSTO;
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void ParametrosMotivo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        GridLeyenda.DataSource = obj.ListarParametros("IDE_MOTIVO", "TBSOLICITUD_PERMISOS");
        GridLeyenda.DataBind();

        ddlLeyenda.DataSource = obj.ListarParametros("IDE_MOTIVO", "TBSOLICITUD_PERMISOS");
        ddlLeyenda.DataTextField = "DES_ASUNTO";
        ddlLeyenda.DataValueField = "ID_PARAMETRO";
        ddlLeyenda.DataBind();

        ddlLeyenda.Items.Insert(0, new ListItem("--- TODOS ---", ""));

    }
    
    private DataTable GetFechas(int year, int month)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("dbo.SP_TBSOLICITUD_PERMISOS_SELECT_DIAS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@anio", SqlDbType.Int).Value = year;
        cmd.Parameters.Add("@mes", SqlDbType.Int).Value = month;
        cmd.Parameters.Add("@ccentro", SqlDbType.VarChar, 80).Value = lblCentro.Text;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);
        return dt;
    }
    protected void CalendarDRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            // If the month is CurrentMonth
            //if (!e.Day.IsOtherMonth)
                if (e.Day.Date.ToShortDateString()!= null)
                {
                foreach (DataRow dr in dtMes.Rows)
                {
                    if ((dr["FECHA"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTime dtEvent = (DateTime)dr["FECHA"];
                        if (dtEvent.Equals(e.Day.Date))
                        {
                            //e.Cell.BackColor = GetColor();


                            System.Web.UI.WebControls.Image image;
                            image = new System.Web.UI.WebControls.Image();
                            image.ImageUrl = "~/imagenes/pinAzul.png";

                            BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
                            DataTable dtResultado = new DataTable();
                            dtResultado = obj.SP_TBSOLICITUD_PERMISOS_MES_TOOLTIP(dr["DIA"].ToString(), lblCentro.Text);

                            image.ToolTip =  dtResultado.Rows[0]["MENSAJE"].ToString();


                            e.Cell.Controls.Add(image);
                            e.Cell.ToolTip = dtResultado.Rows[0]["MENSAJE"].ToString();
                            //e.Cell.BackColor = GetColor();
                            //e.Cell.ForeColor = Color.White;

                            //DateTime miFecha = e.Day.Date;
                            //int day = miFecha.Day;
                            //dtUsuarios = GetUser(day);
                            //foreach (DataRow drf in dtUsuarios.Rows)
                            //{
                            //    string nom = "<br />" + drf["NOMBRE_COMPLETO"].ToString();
                            //    e.Cell.Controls.Add(new LiteralControl(nom));
                            //    //e.Cell.ToolTip = "dia festivoOOOO";
                            //    //e.Cell.Controls.Add(new LiteralControl("<br />Holiday"));
                            //    //e.Cell.Controls[0].Visible = false;
                            //    //e.Cell.Enabled = false;
                            //    //e.Cell.CssClass = "disabledDate";
                            //}

                        }
                    }
                }
            }
            //If the month is not CurrentMonth then hide the Dates
            else
            {
                e.Cell.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    private Color GetColor()
    {
        return Color.FromArgb(62, 166, 62);
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DateTime miFecha = Convert.ToDateTime(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
        int year = miFecha.Year;
        int month = miFecha.Month;
        int day = miFecha.Day;

        lblfecha.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        dtMes = GetFechas(year, month);
        FechaLeyenda(year, month);
    }
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {

        int anio = Convert.ToInt32(Calendar1.VisibleDate.Year.ToString());
        int mes = Convert.ToInt32(Calendar1.VisibleDate.Month.ToString());



        lblfecha.Text = "01/" + Convert.ToString(mes).Trim().PadLeft(2, '0') + "/" + Convert.ToString(anio);
        dtMes = GetFechas(anio, mes);
        FechaLeyenda(anio, mes);
    }
    private void FechaLeyenda(int anio, int mes)
    {
        DataTable dtResultado = new DataTable();
        DataTable dtNombres = new DataTable();
        BL_TBSOLICITUD_PERMISOS obj = new BL_TBSOLICITUD_PERMISOS();
  
        //dtResultado = GetFechas(anio, mes);
        //if (dtResultado.Rows.Count == 0)
        //{
        //    dlCustomers.Visible = false;
        //    dlCustomers.DataSource = dtResultado;
        //    dlCustomers.DataBind();
        //}
        //else
        //{
        //    dlCustomers.Visible = true;
        //    dlCustomers.DataSource = dtResultado;
        //    dlCustomers.DataBind();

        //}

        dtResultado = obj.SP_TBSOLICITUD_VER_PERMISOS_MES(anio, mes,"",ddlLeyenda.SelectedValue.ToString (), lblCentro.Text);
        if (dtResultado.Rows.Count == 0)
        {
            GridPermisos.Visible = false;
            GridPermisos.DataSource = dtResultado;
            GridPermisos.DataBind();
        }
        else
        {
            GridPermisos.Visible = true;
            GridPermisos.DataSource = dtResultado;
            GridPermisos.DataBind();

        }


    }

    protected void dlCustomers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string estado = string.Empty;
        if (ddlLeyenda.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlLeyenda.SelectedValue.ToString();
        }

        DataRowView drv = e.Item.DataItem as DataRowView;
        GridView innerDataList = e.Item.FindControl("GridView1") as GridView;
        Label lbl = (Label)e.Item.FindControl("lbldia");
        DataTable _DataTable2 = new DataTable();
        _DataTable2 = GetFechasDetalle(lbl.Text, estado );
        foreach (DataRow rw in _DataTable2.Rows)
        {
            innerDataList.DataSource = _DataTable2;
            innerDataList.DataBind();
        }
    }
    private DataTable GetFechasDetalle(string dia, string estado)
    {
        

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("dbo.SP_TBSOLICITUD_PERMISOS_MES_DETALLE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FECHA", SqlDbType.VarChar,10).Value = dia;
        cmd.Parameters.Add("@ccentro", SqlDbType.VarChar, 10).Value = lblCentro.Text;
        cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar, 10).Value = estado;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);
        return dt;
    }

    protected void ddlLeyenda_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime miFecha = Convert.ToDateTime (lblfecha.Text );
        int anio = miFecha.Year;
        int mes = miFecha.Month;

        //int anio = Convert.ToInt32(Calendar1.
        //int mes = Convert.ToInt32(Calendar1.VisibleDate.Month.ToString());


        dtMes = GetFechas(anio, mes);
        FechaLeyenda(anio, mes);
    }

    protected void GridLeyenda_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRowView drv = e.Item.DataItem as DataRowView;
        GridView GridLeyendaParametros = e.Item.FindControl("GridLeyendaParametros") as GridView;
        Label lbl = (Label)e.Item.FindControl("lblidParemetro");
        DataTable _DataTable2 = new DataTable();

        BL_PERSONAL obj = new BL_PERSONAL();

        _DataTable2 = USP_PARAMETRO_LISTAR_ID(lbl.Text);
        foreach (DataRow rw in _DataTable2.Rows)
        {
            GridLeyendaParametros.DataSource = _DataTable2;
            GridLeyendaParametros.DataBind();
        }
    }
    private DataTable USP_PARAMETRO_LISTAR_ID(string ID_PARAMETRO)
    {


        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("dbo.USP_PARAMETRO_LISTAR_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID_PARAMETRO ", SqlDbType.Int).Value = Convert.ToInt32( ID_PARAMETRO);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);
        return dt;
    }
}