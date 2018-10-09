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

public partial class OPERACIONES_CartaCobranzasFile : System.Web.UI.Page
{
    string FolderCarta = ConfigurationManager.AppSettings["FolderCarta"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int IDE_CARTA = Convert.ToInt32(Request.QueryString["IDE_CARTA"].ToString());
            file();

            CartaDatos(IDE_CARTA.ToString());
        }
            
    }
    protected void CartaDatos(string IDE_CARTA)
    {
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_ID(Convert.ToInt32(IDE_CARTA));
        if (dtResultado.Rows.Count > 0)
        {
            int APROBACION = Convert.ToInt32(dtResultado.Rows[0]["APROBACION"].ToString());
            int RECHAZO = Convert.ToInt32(dtResultado.Rows[0]["RECHAZO"].ToString());

            if (RECHAZO == 0 && APROBACION == 0)
            {

            }
            else
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                    btnEliminar.Visible = false;
                }
            }
        }
    }
    protected void file()
    {
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        DataTable dtResultado = new DataTable();

        int IDE_CARTA = Convert.ToInt32(Request.QueryString["IDE_CARTA"].ToString());
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_FILE_IDE_CARTA(IDE_CARTA);
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
    protected void Eliminar(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderCarta);
        int IDE_CARTA = Convert.ToInt32(Request.QueryString["IDE_CARTA"].ToString());

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_FILE = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FILE"].ToString();
        string Archivo = GridView1.DataKeys[grdrow.RowIndex].Values["ARCHIVO"].ToString();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        DataTable dtResultado = new DataTable();
        try
        {
            if (File.Exists(path + Archivo))
            {
                File.Delete(path + Archivo);
               
            }
        }
        catch(Exception ex)
        {

        }

            dtResultado = obj.uspDEL_CARTA_COBRAZAS_FILE_ID(Convert.ToInt32(IDE_FILE));
        file();
    }
}