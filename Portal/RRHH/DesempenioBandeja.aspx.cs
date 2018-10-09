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

public partial class RRHH_DesempenioBandeja : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Opciones();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }

    
    protected void Opciones()
    {
        GridView1.DataSource = GetTableEstado();
        GridView1.DataBind();
    }
    static DataTable GetTableEstado()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("IDE", typeof(string));
        table.Columns.Add("IMAGEN", typeof(string));
        table.Columns.Add("DESCRIPCION", typeof(string));
        table.Columns.Add("URL", typeof(string));

        table.Rows.Add("0", "~/imagenes/desempeño-34.png", "CONTROL DE ETAPAS", "~/RRHH/DesempenioEtapas.aspx");
        table.Rows.Add("1", "~/imagenes/desempeño-35.png", "CREAR PERFILES", "~/RRHH/DesempenioPerfil.aspx");
        table.Rows.Add("11", "~/imagenes/desempeño-36.png", "PERSONAL NUEVO/LIBRE", "~/RRHH/DesempenioNuevo.aspx");
        table.Rows.Add("2", "~/imagenes/desempeño-3.png", "RESETEAR CLAVES", "");
        table.Rows.Add("3", "~/imagenes/desempeño-4.png", "INICIAR FORMULARIOS", "");
        table.Rows.Add("4", "~/imagenes/desempeño-5.png", "SESIONES DE CALIBRACION", "");
        table.Rows.Add("5", "~/imagenes/desempeño-6.png", "SUSTITUIR COMO OTRO USUARIO", "");
        table.Rows.Add("6", "~/imagenes/desempeño-7.png", "EXPORTAR REPORTES", "");
        table.Rows.Add("7", "~/imagenes/desempeño-8.png", "MOVER A LOS COLABORADORES DE UN PASO A OTRO", "");
        table.Rows.Add("8", "~/imagenes/desempeño-9.png", "REASIGNAR UN FORMULARIO A OTRO JEFE", "");
        return table;
    }

}