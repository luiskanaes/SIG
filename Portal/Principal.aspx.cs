using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using UserCode;
using System.Data;
using System.Configuration;

public partial class Principal : System.Web.UI.Page
{

    public int intPerfil;
    public string ControlUsuario;
    string FolderFirmas = ConfigurationManager.AppSettings["FolderFirmas"];
    string FolderFotos = ConfigurationManager.AppSettings["FolderFotos"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnCerrar);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(imgPopup);
        this.Form.DefaultButton = this.btnBuscar.UniqueID;
        //Session["IDE_USUARIO"] = BL_Session.Usuario.ToString();
        if (!Page.IsPostBack)
        {




            BL_Seguridad obj = new BL_Seguridad();
            DataTable dt = new DataTable();
            dt = obj.uspSEL_TBOPCIONES_MENU();
            if (dt.Rows.Count > 0)
            {
                ListView1.DataSource = dt;
                ListView1.DataBind();

            }
            else
            {
                ListView1.DataSource = dt;
                ListView1.DataBind();
            }

            /////////////////////////////
            DataTable dtPersonal = new DataTable();
            dtPersonal = obj.SP_LISTAR_FELIZ_CUMPLE();
            if (dtPersonal.Rows.Count > 0)
            {
                GridCumple.DataSource = dtPersonal;
                GridCumple.DataBind();

            }
            else
            {
                GridCumple.DataSource = dtPersonal;
                GridCumple.DataBind();
            }
            SelecionarPerfil();
            AnuncioHSEC("Tool Box HSEC");

            if (BL_Session.FLG_COMUNICADO.ToString() == "1")
            {
                DataTable dtResultado = new DataTable();
                BL_HSEC_ANUNCIOS objPopup = new BL_HSEC_ANUNCIOS();
                dtResultado = objPopup.uspCONSULTAR_POPUP_ANUNCIOS();
                if (dtResultado.Rows.Count > 0)
                {

                    imgPopup.ImageUrl = dtResultado.Rows[0]["URL"].ToString();
                    string URL_WEB = dtResultado.Rows[0]["URL_WEB"].ToString();
                    hdUrl.Value = dtResultado.Rows[0]["URL_WEB"].ToString();
                    //if (URL_WEB !=string.Empty )
                    //{

                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "OpenPopup_url('" + URL_WEB + "');", true);
                    //}



                }

                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "OpenPopup();", true);
            }
        }

    }
    protected void AnuncioHSEC(string TIPO)
    {
        BL_HSEC_ANUNCIOS obj = new BL_HSEC_ANUNCIOS();
        DataTable dt = new DataTable();
        dt = obj.uspSEL_HSEC_ANUNCIOS_BUSCAR(TIPO, "1");
        if (dt.Rows.Count > 0)
        {
            DataList1.DataSource = dt;
            DataList1.DataBind();

        }
        else
        {
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void VerEnlace(object sender, ImageClickEventArgs e)
    {



        ImageButton btnEnlace = ((ImageButton)sender);
        string url = btnEnlace.CommandArgument.ToString();

        ListViewItem CommentItem = btnEnlace.NamingContainer as ListViewItem;
        string IDE_OPCIONES = ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OPCIONES"].ToString();
        Session["IDE_OPCIONES"] = ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OPCIONES"].ToString();

        Response.Redirect(url);
    }

    public void SelecionarPerfil()
    {
        BL_RRHH_PERSONAL_EMPRESA obj = new BL_RRHH_PERSONAL_EMPRESA();
        DataTable dt = new DataTable();
        string dni = BL_Session.Usuario;
        dt = obj.uspSEL_RRHH_PERSONAL_EMPRESA_POR_ID(dni);
        if (dt.Rows.Count > 0)
        {
            //lblNombre.Text = dt.Rows[0]["NOMBRE_CORTO"].ToString();

            lblCcentro.Text = dt.Rows[0]["CENTRO_COSTO"].ToString();
            string url = Server.MapPath(FolderFirmas + dt.Rows[0]["FIRMA"].ToString());





            string foto = dt.Rows[0]["URL_FOTO"].ToString();
            if (foto == string.Empty)
            {
                imgFoto.ImageUrl = "~/imagenes/Foto_Fondo.png";

            }
            else
            {
                imgFoto.ImageUrl = dt.Rows[0]["URL_FOTO"].ToString();

            }



        }

    }
  

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        if (tb_Buscar.Text == string.Empty)
        {
            cleanMessage = "Ingresar datos de busqueda";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            BL_INTRANET obj = new BL_INTRANET();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.SP_Listar_Directorio_Corporativo(tb_Buscar.Text.Trim());
            if (dtResultado.Rows.Count > 0)
            {
                PanelDirectorio.Visible = true;
                GridBuscar.DataSource = dtResultado;
                GridBuscar.DataBind();
            }
            else
            {
                PanelDirectorio.Visible = false;
                cleanMessage = "No existe información";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }

    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        AnuncioHSEC("Tool Box HSEC");
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        AnuncioHSEC("Comunicados HSEC");
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
        AnuncioHSEC("Comite SST");
    }

    protected void btn4_Click(object sender, EventArgs e)
    {
        AnuncioHSEC("Brigadas de Emergencia");
    }

    protected void btnCerrar_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtResultado = new DataTable();
        BL_HSEC_ANUNCIOS obj = new BL_HSEC_ANUNCIOS();
        dtResultado = obj.uspUPDATE_POPUP_LECTURA(Session["IDE_USUARIO"].ToString());
        BL_Session.FLG_COMUNICADO = 0;

        //if (dtResultado.Rows.Count > 0)
        //{

        //    hdcodigo.Value = dtResultado.Rows[0]["IDE_ANUNCIO"].ToString();
        //}
    }

    protected void VerUrl(object sender, ImageClickEventArgs e)
    {

        ImageButton imgPopup = ((ImageButton)sender);
        if(hdUrl.Value !=string.Empty )
        {
            string URL_WEB = hdUrl.Value;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "OpenPopup_url('" + URL_WEB + "');", true);
        }

        DataTable dtResultado = new DataTable();
        BL_HSEC_ANUNCIOS obj = new BL_HSEC_ANUNCIOS();
        dtResultado = obj.uspUPDATE_POPUP_LECTURA(Session["IDE_USUARIO"].ToString());
        BL_Session.FLG_COMUNICADO = 0;
    }
}