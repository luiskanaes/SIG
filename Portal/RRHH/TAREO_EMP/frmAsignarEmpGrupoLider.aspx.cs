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
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;
public partial class RRHH_TAREO_EMP_frmAsignarEmpGrupoLider : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Empresas();
            ListaLiderGrupo(); 
        }
    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresas.DataSource = dtResultado;
            ddlEmpresas.DataTextField = dtResultado.Columns["DES_ABREV"].ToString();
            ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresas.DataBind();

        }
    }
    protected void ListaLiderGrupo()
    {
        BL_TAREO_EMPLEADOS obj = new BL_TAREO_EMPLEADOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SP_LISTA_EMP_LIDER_GRUPO("");

        if (dtResultado.Rows.Count > 0)
        {
            ddlLider.DataSource = dtResultado;
            ddlLider.DataTextField = dtResultado.Columns["NOMBRES"].ToString();
            ddlLider.DataValueField = dtResultado.Columns["DNI"].ToString();
            ddlLider.DataBind();

        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

    }

    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlLider_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}