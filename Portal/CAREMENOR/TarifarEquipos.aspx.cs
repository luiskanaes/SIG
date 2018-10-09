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

public partial class CAREMENOR_TarifarEquipos : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    string idValor;
    string Proyecto;
    
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

        if (!Page.IsPostBack)
        {
            txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");

            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            idValor = Request.QueryString["idValor"].ToString();
            Proyecto = Request.QueryString["Proyecto"].ToString();



            Listar();


        }
    }
    protected void Listar()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_VALORIZAR_VALORPERIODO_POR_ID(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        if (dtResultado.Rows.Count > 0)
        {

            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
        else
        {

            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        idValor = Request.QueryString["idValor"].ToString();
        Proyecto = Request.QueryString["Proyecto"].ToString();

        string cleanMessage = string.Empty;
       

        string CODIGO = string.IsNullOrEmpty(idValor) ? "0" : idValor;
        string PRECIO = string.IsNullOrEmpty(txtPrecio.Text) ? "0" : txtPrecio.Text;

        if (txtPrecio.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar tarifa";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            //verificamos fecha de registro
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dt = new DataTable();
            dt = obj.USP_TBL_VALORIZACION_FECHA_TARIFA(Requ_Numero,
                            Reqd_CodLinea,
                            Reqs_Correlativo,
                            txtInicio.Text.Trim());
            if (dt.Rows[0]["ESTADO"].ToString()=="0")
            {
                cleanMessage = dt.Rows[0]["MSG"].ToString();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                
                DataTable dtResultado = new DataTable();

                dtResultado = obj.uspINS_valorizar_ValorPeriodo(
                            Convert.ToInt32(CODIGO),
                            Requ_Numero,
                            Reqd_CodLinea,
                            Reqs_Correlativo,
                            txtInicio.Text.Trim(),
                            PRECIO,
                            Session["IDE_USUARIO"].ToString(),
                           Proyecto

                        );
                if (dtResultado.Rows.Count > 0)
                {
                    txtInicio.Text = string.Empty;
                    Listar();
                    cleanMessage = "Registro satisfactorio";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

                }
            }
        }

    }
}