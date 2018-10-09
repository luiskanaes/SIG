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

public partial class RRHH_EstrellaSSK : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            estado();
        }
    }

    protected void estado()
    {
        {
            BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
            DataTable dtResultado = new DataTable();

            dtResultado = obj.USP_ESTRELLA_CC_EVALUADOR(Session["IDE_USUARIO"].ToString () , BL_Session.CENTRO_COSTO);
            if (dtResultado.Rows.Count > 0)
            {

                btnReconocer.Visible = true;

            }
            else
            {

                btnReconocer.Visible = false;

                string cleanMessage = "No tiene acceso al proceso de nominación : Estrella SSK";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void btnReconocer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/EstrellaReconocer.aspx");
    }
}