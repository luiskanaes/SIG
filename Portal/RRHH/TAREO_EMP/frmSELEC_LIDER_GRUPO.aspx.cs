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

public partial class RRHH_TAREO_EMP_frmSELEC_LIDER_GRUPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Emp_Disponibles();
            Emp_Lista_Lider_Grupo();
        }
    }
    protected void Emp_Disponibles()
    {
        BL_TAREO_EMPLEADOS obj = new BL_TAREO_EMPLEADOS();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.SP_BUSCAR_EMP_DISPONIBLES("");

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpleadosDisponibles.DataSource = dtResultado;
            ddlEmpleadosDisponibles.DataTextField = dtResultado.Columns["NOMBRES"].ToString();
            ddlEmpleadosDisponibles.DataValueField = dtResultado.Columns["DNI"].ToString();
            ddlEmpleadosDisponibles.DataBind();

        }
    }

    protected void Emp_Lista_Lider_Grupo()
    {
        BL_TAREO_EMPLEADOS obj = new BL_TAREO_EMPLEADOS();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.SP_LISTA_EMP_LIDER_GRUPO("");

        if (dtResultado .Rows.Count > 0)
        {

            gridPersonal.DataSource = dtResultado ;
            gridPersonal.DataBind();

        }
    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        BL_TAREO_EMPLEADOS obj = new BL_TAREO_EMPLEADOS();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.SP_INSERTAR_EMP_LIDER_GRUPO("",ddlEmpleadosDisponibles.SelectedValue.ToString()); 
         
        DataTable dtResultado2 = new DataTable();

        dtResultado2 = obj.SP_LISTA_EMP_LIDER_GRUPO("");

        if (dtResultado2.Rows.Count > 0)
        {

            gridPersonal.DataSource = dtResultado2;
            gridPersonal.DataBind();

        }
    }

    protected void ddlEmpleadosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl.Replace(Request.RawUrl, "frmAsignarEmpGrupoLider.aspx"));
    }
}