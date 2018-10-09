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

public partial class RRHH_MisNominaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Nominaciones();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Nominaciones()
    {
        BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_ESTRELLA_NOMINACIONES( Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
          
        }
        else
        {
        
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
        }
    }
    protected void EliminarNominacion(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnEliminarFase = ((ImageButton)sender);

        BL_RRHH_ESTRELLA_NOMINACION obj = new BL_RRHH_ESTRELLA_NOMINACION();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_ESTRELLA_NOMINACION_POR_ID(Convert.ToInt32(btnEliminarFase.CommandArgument));
        if (Convert.ToInt32 ( dtResultado.Rows[0]["ID"].ToString())  > 0)
        {
            try
            {


                Nominaciones();

            }
            catch (Exception ex)
            {
                cleanMessage = "Existen problemas con la eliminación de nominaciones";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        else
        {
            cleanMessage = "No se puede eliminar nominación, en proceso de atención";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RRHH/EstrellaReconocer.aspx");
    }
}