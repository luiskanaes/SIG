using BusinessEntity;
using BusinessLogic;
using UserCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.Reporting.WebForms;
public partial class OPERACIONES_Reportes_Control : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ListarAnio();
            Meses();


        }
    }
    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Carga_CJI3.aspx");
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        BL_CJI3 obj = new BL_CJI3();
        BL_Seguridad objSeg = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.eliminar_periodo(Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue));
        string estado = dtResultado.Rows[0]["ESTADO"].ToString();
        if (estado == "1")
        {
            objSeg.auditoria_procesos("CJI3", Session["IDE_USUARIO"].ToString(), txtsustento.Text);
            txtsustento.Text = string.Empty;
            UC_MessageBox.Show(Page, this.GetType(), "Periodo Eliminado Satisfactoriamente");
        }
        else
        {
            txtsustento.Text = string.Empty;
            UC_MessageBox.Show(Page, this.GetType(), "No existen registros ha eliminar");
            return;
        }
    }
    protected void ListarAnio()
    {
        BL_CJI3 obj = new BL_CJI3();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_anio();

        if (dtResultado.Rows.Count > 0)
        {
            ddlAnio.DataSource = dtResultado;
            ddlAnio.DataTextField = dtResultado.Columns["ANIO"].ToString();
            ddlAnio.DataValueField = dtResultado.Columns["INT_EJERCICIO"].ToString();
            ddlAnio.DataBind();
        }
    }
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();
    }
    private DataTable GetMeses()
    {
        DataTable dtMes = new DataTable();

        //Add Columns to Table
        dtMes.Columns.Add(new DataColumn("DisplayMember"));
        dtMes.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dtMes.Rows.Add(1, "ENERO");
        dtMes.Rows.Add(2, "FEBRERO");
        dtMes.Rows.Add(3, "MARZO");
        dtMes.Rows.Add(4, "ABRIL");
        dtMes.Rows.Add(5, "MAYO");
        dtMes.Rows.Add(6, "JUNIO");
        dtMes.Rows.Add(7, "JULIO");
        dtMes.Rows.Add(8, "AGOSTO");
        dtMes.Rows.Add(9, "SETIEMBRE");
        dtMes.Rows.Add(10, "OCTUBRE");
        dtMes.Rows.Add(11, "NOVIEMBRE");
        dtMes.Rows.Add(12, "DICIEMBRE");

        return dtMes;

    }
}