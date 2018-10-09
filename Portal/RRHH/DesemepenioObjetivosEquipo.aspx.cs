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


public partial class RRHH_DesemepenioObjetivosEquipo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //InsertarPersonalVarios();
            Listar();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void InsertarPersonalVarios()
    {
        BE_RRHH_DESEMPENIO_FICHA oBESol = new BE_RRHH_DESEMPENIO_FICHA();

        oBESol.CCENTRO = Session["CCENTRO"].ToString();
        oBESol.CODIGO_GERENCIA = Session["CODIGO_GERENCIA"].ToString();
        oBESol.IP_CENTRO = Session["IP_CENTRO"].ToString();
        oBESol.ANIO = Convert.ToInt32(Session["ANIO"].ToString());
        oBESol.DNI_JEFE = Session["DNI_JEFE"].ToString();
        oBESol.DNI_GERENTE = Session["DNI_GERENTE"].ToString();
        oBESol.USER_REGISTRA = Session["IDE_USUARIO"].ToString();

        int dtrpta = 0;
        dtrpta = new BL_RRHH_DESEMPENIO_FICHA().uspSEL_RRHH_DESEMPENIO_INSERT_VARIOS(oBESol);
        if (dtrpta > 0)
        {


        }
    }
    protected void Listar()
    {
        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_COLABORADORES(Session["IDE_USUARIO"].ToString(), Session["ANIO"].ToString(),1);
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }

    }
    protected void Objetivos(object sender, ImageClickEventArgs e)
    {
        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();

        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow row = btnEditar.NamingContainer as GridViewRow;

        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        Session["IDE_DESEMPENIO"] = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        string IDE_DESEMPENIO = Session["IDE_DESEMPENIO"].ToString();
        Session["URL_DESEMPENIO"] = "~/RRHH/DesemepenioObjetivosEquipo.aspx";
        Response.Redirect("~/RRHH/DesempenioIntroduccion.aspx");
    }

    protected void btnVarios_Click(object sender, EventArgs e)
    {
        Session["URL_DESEMPENIO"] = "~/RRHH/DesemepenioObjetivosEquipo.aspx";
        Response.Redirect("~/RRHH/DesempenioObjetivosVarios.aspx");
    }
}