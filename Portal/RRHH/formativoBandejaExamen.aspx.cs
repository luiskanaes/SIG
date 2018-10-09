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

public partial class RRHH_formativoBandejaExamen : System.Web.UI.Page
{
    string FolderTrainee = ConfigurationManager.AppSettings["FolderTrainee"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {


            datosFicha(Session["IDE_USUARIO"].ToString());
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void datosFicha(string  id)
    {
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_FORMATIVO_FICHA_DNI(id);
        if (dtResultado.Rows.Count > 0)
        {
            lblCodigoFicha.Text = dtResultado.Rows[0]["IDE_FICHA"].ToString();
            lblnombre.Text = dtResultado.Rows[0]["NOMBRES_COMPLETO"].ToString();
            
            string foto = dtResultado.Rows[0]["FOTO"].ToString();

            if (foto == string.Empty)
            {
                imgFotos.ImageUrl = "~/imagenes/Foto_Fondo.png";
            }
            else
            {
                imgFotos.ImageUrl = FolderTrainee + foto;
            }
            ListarFases();

        }
        else
        {
            string cleanMessage = "No existe información";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void ListarFases()
    {
        BL_RRHH_FORMATIVO_FICHA obj = new BL_RRHH_FORMATIVO_FICHA();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_RRHH_FORMATIVO_FASE_POR_FICHA(Convert.ToInt32(lblCodigoFicha.Text));
        if (dt.Rows.Count > 0)
        {
            ListView1.Visible = true;
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
        else
        {
            ListView1.Visible = false;

        }
    }
    protected void View_MitadDesempenio(object sender, EventArgs e)
    {

        ImageButton btnDesempenioM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();


        
        Session["IDE_FASE"] = IDE_FASE;
        Session["IDE_FICHA"] = IDE_FICHA;
        Session["IDE_EXAMEN"] = 1833;
        Response.Redirect("~/RRHH/FormativoExamen.aspx");
        
    }
    protected void View_FinalDesempenio(object sender, EventArgs e)
    {

        ImageButton btnDesempenioM = ((ImageButton)sender);
        int IDE_FASE = Convert.ToInt32(btnDesempenioM.CommandArgument);

        ListViewItem CommentItem = btnDesempenioM.NamingContainer as ListViewItem;
        int IDE_FICHA = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_FICHA"];

        BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dt = new DataTable();

        //Ev.Seguimiento(Final) 4
        
    Session["IDE_FASE"] = IDE_FASE;
    Session["IDE_FICHA"] = IDE_FICHA;
    Session["IDE_EXAMEN"] = 1835;
    Response.Redirect("~/RRHH/FormativoExamen.aspx");
        
    }
}