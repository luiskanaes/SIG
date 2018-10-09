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
public partial class CAREMENOR_AlquilerHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
           
        }
        if (!Page.IsPostBack)
        {
            CONTROL_PROCESOS();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }

    protected void CONTROL_PROCESOS()
    {

        
    }

    protected void btnEquipo1_Click(object sender, EventArgs e)
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALQUILER", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            Response.Redirect("~/CAREMENOR/EquiposMayoresAlquiler.aspx");

        }
        else
        {
            Response.Redirect("~/CAREMENOR/EquiposMayoresAlquilerView.aspx");
         
        }


      
    }



    protected void btnEquipo2_Click(object sender, EventArgs e)
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALQUILER", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            Response.Redirect("~/CAREMENOR/EquiposMayoresAlquilerMenor.aspx");

        }
        else
        {
            Response.Redirect("~/CAREMENOR/EquiposMayoresAlquilerMenorView.aspx");

            //string cleanMessage = "No cuenta con permisos";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void btnValorizarMenor_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CAREMENOR/ValorizarEquipoMenor.aspx");
    }
}