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


public partial class CAREMENOR_AsignarRequerimiento : System.Web.UI.Page
{
    string Req = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Req = Request.QueryString["Req"].ToString();
            Personal();
            requerimientos();
        }
    }
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersonal"))
            {
                string strSelectedValue = ddlPersonal.SelectedValue;
                //Your code here ... 
            }
        }
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
         
        }
        
    }
    protected void requerimientos()
    {
        Req = Request.QueryString["Req"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_ItemSecuencia(Req);
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        int intContador = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            //TextBox txt;
           string Reqs_ItemSecuencia = GridView1.DataKeys[row.RowIndex].Values["Reqs_ItemSecuencia"].ToString(); // extrae key
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_ASIGNA_RESPONSABLE(Reqs_ItemSecuencia, ddlPersonal.SelectedValue.ToString());
            intContador += 1;
        }

        if (intContador>0)
        {
            requerimientos();
        }
    }
}