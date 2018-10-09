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
public partial class RRHH_DesempenioIntroduccion : System.Web.UI.Page
{
    string IDE_DESEMPENIO = string.Empty;
    string URL_DESEMPENIO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            IDE_DESEMPENIO = Session["IDE_DESEMPENIO"].ToString();
            URL_DESEMPENIO =Session["URL_DESEMPENIO"].ToString();

        }
    }

    protected void btnIniciar_Click(object sender, EventArgs e)
    {
        string IDE_DESEMPENIO = Session["IDE_DESEMPENIO"].ToString();
        URL_DESEMPENIO = Session["URL_DESEMPENIO"].ToString();
        Response.Redirect("~/RRHH/DesempenioFijarObjetivoNuevo.aspx");
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Session["URL_DESEMPENIO"].ToString());
    }
}