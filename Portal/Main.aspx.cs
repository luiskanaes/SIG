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
using System.Text;

public partial class Main : System.Web.UI.Page
{
    public static DataTable dtProductos;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
          
            Banner();
            ListarFotos();
            Enlaces();
            Archivos();
            PROMOCION_PERSONAL(1);
            ANIVERSARIOS(2);
          

        }


    }
    protected void Banner()
    {
        BL_INTRANET obj = new BL_INTRANET();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SP_LISTAR_BANNER(1, "1");
       
        this.Rgallary.DataSource = dtResultado;
        this.Rgallary.DataBind();
    }
    protected void ListarFotos()
    {
       

        BL_INTRANET obj = new BL_INTRANET();
  
        DataTable dt = new DataTable();

        dt = obj.SP_LISTAR_BANNER(2, "1");
        if (dt.Rows.Count > 0)
        {
          
            images.DataSource = dt;
            images.DataBind();

        }
        else
        {

            images.DataSource = dt;
            images.DataBind();
        }
    }
    protected void Enlaces()
    {
        BL_INTRANET obj = new BL_INTRANET();

        DataTable dt = new DataTable();

        dt = obj.SP_LISTAR_BANNER(3,"1");
        if (dt.Rows.Count > 0)
        {

            ListView2.DataSource = dt;
            ListView2.DataBind();

        }
        else
        {

            ListView2.DataSource = dt;
            ListView2.DataBind();
        }
    }

    protected void Archivos()
    {
        BL_INTRANET obj = new BL_INTRANET();

        DataTable dt = new DataTable();

        dt = obj.SP_LISTAR_BANNER_FILE(5, "1");
        if (dt.Rows.Count > 0)
        {

            dlCustomers.DataSource = dt;
            dlCustomers.DataBind();

        }
        else
        {

            dlCustomers.DataSource = dt;
            dlCustomers.DataBind();
        }
    }

    protected void dlCustomers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        BL_INTRANET obj = new BL_INTRANET();
        DataRowView drv = e.Item.DataItem as DataRowView;
        GridView innerDataList = e.Item.FindControl("GridView1") as GridView;
        Label lbl = (Label)e.Item.FindControl("lblDescripcion");
        DataTable _DataTable2 = new DataTable();
        _DataTable2 = obj.SP_LISTAR_BANNER_DESCRIPCION(5,"1",lbl.Text);
        foreach (DataRow rw in _DataTable2.Rows)
        {
            innerDataList.DataSource = _DataTable2;
            innerDataList.DataBind();
        }
    }
    protected void PROMOCION_PERSONAL(int tipo)
    {

        //TIPO  1 = FLG_INGRESO_NUEVO
        //TIPO 2 = FLG_ASCENSO_NUEVO

        BL_INTRANET Xobj = new BL_INTRANET();
        DataTable dt = new DataTable();
        dt = Xobj.SP_LISTAR_ANUNCIOS_PERSONAL(tipo);
        this.Repeater1.DataSource = dt;
        this.Repeater1.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        PROMOCION_PERSONAL(1);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion1", "carruselBanner();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "carrusel();", true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        PROMOCION_PERSONAL(2);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion1", "carruselBanner();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "carrusel();", true);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Archivos();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion1", "carruselBanner();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "carrusel();", true);
    }
    protected void ANIVERSARIOS(int tipo)
    {

    //          @TIPO = 1 CUMPLEAÑOS
    //          @TIPO = 2 ANIVERSARIOS

       BL_INTRANET Xobj = new BL_INTRANET();
        DataTable dt = new DataTable();
        dt = Xobj.SP_LISTAR_ANIVERSARIO(tipo);
        this.Repeater2.DataSource = dt;
        this.Repeater2.DataBind();

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ANIVERSARIOS(2);
      
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion1", "carruselBanner();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "carrusel();", true);

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        ANIVERSARIOS(1);
   
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion1", "carruselBanner();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "carrusel();", true);

    }
   
}