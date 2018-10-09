using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OPERACIONES_RO_Opciones : System.Web.UI.Page
{
    public string ControlUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            ControlBotones();
        }

    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = true;
            btnProyecto.Visible = true;
            btnReporte.Visible = true;
        }
        else
        {
            btnMantenimiento.Visible = true;
            btnPEP.Visible = false;
            btnProyecto.Visible = false;
            btnReporte.Visible = true;
        }
    }
    protected void btnPEP_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PEP.aspx");
    }
    protected void btnProyecto_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_PROYECTOS.aspx");
    }
    protected void btnMantenimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_COSTOS_VENTAS.aspx");
    }
    protected void btnReporte_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/RO_REPORTE.aspx");
    }
}