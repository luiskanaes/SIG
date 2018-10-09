using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Data;

public partial class Controles_ControlPermisos : System.Web.UI.UserControl
{
    public string IDE_OPCIONES;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["IDE_USUARIO"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                BL_Seguridad obj = new BL_Seguridad();
                DataTable dtResultado1 = new DataTable();
                DataTable dtResultado2 = new DataTable();
                DataTable dtResultado3 = new DataTable();
                //dtResultado1 = obj.SP_OPCIONES_MODULO_PERMISOS(Session["IDE_USUARIO"].ToString (),1);
                //ListView1.DataSource = dtResultado1;
                //ListView1.DataBind();

                //dtResultado2 = obj.SP_OPCIONES_MODULO_PERMISOS(Session["IDE_USUARIO"].ToString(), 2);
                //ListView2.DataSource = dtResultado2;
                //ListView2.DataBind();


                //dtResultado3 = obj.SP_OPCIONES_MODULO_PERMISOS(Session["IDE_USUARIO"].ToString(), 3);
                //ListView3.DataSource = dtResultado3;
                //ListView3.DataBind();

                DataTable dtMenu = new DataTable();
                BL_Seguridad ObjSeguridad = new BL_Seguridad();
                IDE_OPCIONES = Session["IDE_OPCIONES"].ToString();
                dtMenu = ObjSeguridad.SP_OPCIONES_MODULO_PERMISOS(Session["IDE_USUARIO"].ToString(), 1, IDE_OPCIONES);
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
                        Menu2.Items.Add(mnuMenuItem);
                        //hacemos un llamado al metodo recursivo encargado de generar el arbol del menu.
                        AddMenuItem(mnuMenuItem, dtMenu);
                    }
                }
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
            }
        }
    }
    protected void URL(object sender, ImageClickEventArgs e)
    {

        ImageButton btnAcceso = ((ImageButton)sender);
        string URL = btnAcceso.CommandArgument.ToString();
        Response.Redirect(URL );
    }
}