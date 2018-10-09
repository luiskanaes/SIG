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

public partial class Logistica_Materiales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Grupo();
        }
    }
    protected void Grupo()
    {
        BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SEL_LOG_MATERIALES_POR_GRUPO();

        if (dtResultado.Rows.Count > 0)
        {
            ddlGrupo.DataSource = dtResultado;
            ddlGrupo.DataTextField = dtResultado.Columns["GRUPO"].ToString();
            ddlGrupo.DataValueField = dtResultado.Columns["GRUPO"].ToString();
            ddlGrupo.DataBind();
            Listar();
        }
        
    }

    protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    protected void Listar()
    {
        BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.SEL_LOG_MATERIALES_FILTRO_GRUPO(ddlGrupo.SelectedValue);

        if (dtResultado.Rows.Count > 0)
        {
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
}