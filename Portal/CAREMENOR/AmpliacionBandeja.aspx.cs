using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
public partial class CAREMENOR_AmpliacionBandeja : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        ScriptManager.GetCurrent(this).RegisterPostBackControl(GridView1);
       
        if (!Page.IsPostBack)
        {

            //GridView1.Attributes["onchange"] = "UploadFile(this)";


            Buscarrequerimientos();
            
        }
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        //if (txtPdc.Text.Trim() == string.Empty)
        //{
        //    cleanMessage = "Ingresar PDC";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
        //else
        //{

        //    Buscarrequerimientos();
        //}
        Buscarrequerimientos();
    }
    protected void Buscarrequerimientos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        string Estado = string.Empty;
        Estado = ddlEstado.SelectedValue.ToString();

        if (ddlEstado.SelectedIndex ==0)
        {
            Estado = string.Empty;
        }
       

            dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_PDC_AMPLIAR(BL_Session.CENTRO_COSTO.ToString(), txtPdc.Text, "1", Estado);//1 los equipos ampliados
        if (dtResultado.Rows.Count > 0)
        {

            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {

            string cleanMessage = "No exisen requerimientos con solicitud de ampliación ";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTipo = (Label)e.Row.FindControl("lblTipo");
            if (lblTipo.Text != string.Empty)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblTipo") as Label).Text).Selected = true;
            }

        }
    }

    protected void btncrear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/AmpliacionSolicitud");
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Buscarrequerimientos();
    }
}

