using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OPERACIONES_PedidoMovil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            Session["PROYECTO"] = Request.QueryString["PROYECTO"];
            Session["Codigo"] = Request.QueryString["Codigo"];
            Session["Tipo"] = Request.QueryString["Tipo"];




            Response.Redirect("~/Operaciones/AprobarMovil.aspx");
        }
    }
}