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


public partial class RRHH_Desemepenio : System.Web.UI.Page
{
    int anio = DateTime.Today.Year;
    string Gerente = ConfigurationManager.AppSettings["Gerente"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            
            Opciones();
            datosPersona();
            etapas();

        }
    }
    protected void datosPersona()
    {

        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_FICHA_DNI( Session["IDE_USUARIO"].ToString(), anio.ToString());
        if (dtResultado.Rows.Count > 0)
        {

            Session["ANIO"] = dtResultado.Rows[0]["ANIO"].ToString();
            Session["CODIGO_GERENCIA"] = dtResultado.Rows[0]["CODIGO_GERENCIA"].ToString();
            Session["GERENCIA"] = dtResultado.Rows[0]["GERENCIA"].ToString();
            Session["IP_CENTRO"] = dtResultado.Rows[0]["IP_CENTRO"].ToString();
            Session["CCENTRO"] = dtResultado.Rows[0]["CCENTRO"].ToString();
            Session["CENTRO"] = dtResultado.Rows[0]["CENTRO"].ToString();
    
            Session["DNI_JEFE"] = dtResultado.Rows[0]["DNI_JEFE"].ToString();
            Session["DNI_GERENTE"] = dtResultado.Rows[0]["DNI_GERENTE"].ToString();

            Session["GERENTE"] = dtResultado.Rows[0]["GERENTE"].ToString();
            Session["JEFE"] = dtResultado.Rows[0]["JEFE"].ToString();
        }
        else
        {
           string cleanMessage = "No es participe del proceso de evaluación";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void etapas()
    {

        BL_RRHH_DESEMPENIO_ETAPAS obj = new BL_RRHH_DESEMPENIO_ETAPAS();
        DataTable dtResultado = new DataTable();

        if (Gerente == Session["IDE_USUARIO"].ToString())
        {
            Session["IP_CENTRO"] = string.Empty;
        }
        else
        {
            dtResultado = obj.uspSEL_RRHH_DESEMPENIO_ETAPA_PERSONA(anio, Session["IDE_USUARIO"].ToString(), Session["IP_CENTRO"].ToString());
        }
      

        if(dtResultado.Rows.Count > 0)
        {
            dlCustomers.DataSource = dtResultado;
            dlCustomers.DataBind();
        }
        

    
    }
    protected void Opciones()
    {
        BL_RRHH_DESEMPENIO_FICHA ObjSeguridad = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtMenu = new DataTable();
        dtMenu = ObjSeguridad.uspSEL_RRHH_DESEMPENIO_OPCIONES( Session["IDE_USUARIO"].ToString(), anio.ToString());
        if (dtMenu.Rows.Count > 0)
        {
            DataList1.DataSource = dtMenu;
            DataList1.DataBind();
        }
    }
    /*
    protected void Opciones ()
    {
  
        BL_RRHH_DESEMPENIO_FICHA ObjSeguridad = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtMenu = new DataTable();
        dtMenu = ObjSeguridad.uspSEL_RRHH_DESEMPENIO_OPCION_PERFIL(Session["IDE_USUARIO"].ToString (), anio.ToString ());
        foreach (DataRow drMenuItem in dtMenu.Rows)
        {
            //esta condicion indica q son elementos padre.
            //If drMenuItem("IdPagina").Equals(drMenuItem("IdPadre")) Then
            if (drMenuItem["IdPadre"].Equals(0))
            {
                MenuItem mnuMenuItem = new MenuItem();
                mnuMenuItem.Value = drMenuItem["IdOpcion"].ToString();
                mnuMenuItem.Text = drMenuItem["NombreOpcion"].ToString();
                mnuMenuItem.ImageUrl = drMenuItem["Icono"].ToString();
                mnuMenuItem.NavigateUrl = drMenuItem["Url"].ToString();
                //agregamos el Item al menu
                Menu1.Items.Add(mnuMenuItem);
                //hacemos un llamado al metodo recursivo encargado de generar el arbol del menu.
                AddMenuItem(mnuMenuItem, dtMenu);
                mnuMenuItem.Selected = true;
            }
        }
         
    }
    private void AddMenuItem(MenuItem mnuMenuItem, DataTable dtMenuItems)
    {

        foreach (DataRow drMenuItem in dtMenuItems.Rows)
        {
            if (drMenuItem["IdPadre"].ToString().Equals(mnuMenuItem.Value) && !drMenuItem["IdOpcion"].Equals(drMenuItem["IdPadre"]))
            {
                MenuItem mnuNewMenuItem = new MenuItem();

                mnuNewMenuItem.Value = drMenuItem["IdOpcion"].ToString();
                mnuNewMenuItem.Text = drMenuItem["NombreOpcion"].ToString();
                mnuNewMenuItem.ImageUrl = drMenuItem["Icono"].ToString();
                mnuNewMenuItem.NavigateUrl = drMenuItem["Url"].ToString();
                //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
                mnuMenuItem.ChildItems.Add(mnuNewMenuItem);
                //llamada recursiva para ver si el nuevo menu item aun tiene elementos hijos.
                AddMenuItem(mnuNewMenuItem, dtMenuItems);
                mnuNewMenuItem.Selected = true;
            }
        }
    }
    */
    protected void dlCustomers_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        BL_RRHH_DESEMPENIO_FICHA obj = new BL_RRHH_DESEMPENIO_FICHA();
        DataTable dtResultado = new DataTable();
      


        DataRowView drv = e.Item.DataItem as DataRowView;
        GridView innerDataList = e.Item.FindControl("GridView1") as GridView;

        //string pk = drv.DataKeys[row.RowIndex].Values[0].ToString();

        Label lbl = (Label)e.Item.FindControl("lblIdOpcion");

        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OPCION_PERFIL(Session["IDE_USUARIO"].ToString(), anio.ToString (), Convert.ToInt32(lbl.Text));

        foreach (DataRow rw in dtResultado.Rows)
        {
            innerDataList.DataSource = dtResultado;
            innerDataList.DataBind();
        }
    }


}