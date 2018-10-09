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

public partial class CAREMENOR_VerLegajos : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
          
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            lblreq.Text = Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo;
            file();
         
        }
    }
    protected void file()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.uspSEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
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
}