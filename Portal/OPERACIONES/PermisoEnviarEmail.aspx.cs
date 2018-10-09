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
using System.Text.RegularExpressions;

public partial class OPERACIONES_PermisoEnviarEmail : System.Web.UI.Page
{
    string IDE_PERMISO;
    DataTable dtPersonal = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
      

        if (!Page.IsPostBack)
        {
            IDE_PERMISO = Request.QueryString["Requ_Numero"];
            BL_RESPONSABLE_PROCESOS obj = new BL_RESPONSABLE_PROCESOS();

           
            dtPersonal.Columns.AddRange(new DataColumn[2] { new DataColumn("Row", typeof(string )),
                            new DataColumn("CORREO", typeof(string)) });


            DataTable dtResultado = new DataTable();
            dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_POR_ID("", "", "ENVIAR CORREO MDP/RRHH");
            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i < dtResultado.Rows.Count; i++)
                {
                    int nro = Convert.ToInt32( dtResultado.Rows[i]["Row"].ToString());
                    string correo = dtResultado.Rows[i]["CORREO"].ToString();
                    dtPersonal.Rows.Add(nro, correo);
                }
                    
            }
            Session["datos"] = dtPersonal;
            Listar();
        }
    }
    protected void Listar()
    {

        

        int cantidad = GridView1.Rows.Count + 1;
        GridView1.Visible = true;
        int numeroFilas = GridView1.Rows.Count;

        DataTable Dt;

        if (Session["datos"] == null)
        {
            Dt = new DataTable();
            Dt.Columns.Add("Row");
            Dt.Columns.Add("CORREO");
        }
        else
        {
            Dt = Session["datos"] as DataTable;
        }

        int i = 0;
        if (GridView1.Rows.Count > 1)
        {
            i = numeroFilas + 1;
        }
        while (i > cantidad)//cantidad
        {
            DataRow Fila = Dt.NewRow();
            Fila["Row"] = "";
            Fila["CORREO"] = "";
            i++;
            cantidad--;
            Dt.Rows.Add(Fila);
        }

        GridView1.DataSource = Dt;
        GridView1.DataBind();

        Session["datos"] = Dt;



    }

    protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
    {
        string correos = string.Empty;
        int cantidad = GridView1.Rows.Count;
        if (cantidad > 0)
        {

            foreach (GridViewRow Fila in GridView1.Rows)
            {
                Label lblCorreo = ((Label)Fila.FindControl("lblCorreo"));
                correos += lblCorreo.Text  + ",";
            }
        }

        BL_TBSOLICITUD_PERMISOS oB = new BL_TBSOLICITUD_PERMISOS();
        IDE_PERMISO = Request.QueryString["Requ_Numero"];

        DataTable dtResultado = new DataTable();
        dtResultado = oB.SP_EnviarCorreo_PermisoRHH(Convert.ToInt32(IDE_PERMISO), correos);
        if (dtResultado.Rows.Count > 0)
        {
           
            string cleanMessage = dtResultado.Rows[0]["MSG"].ToString();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

       
    }



    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
 
        dtPersonal.Columns.AddRange(new DataColumn[2] { new DataColumn("Row", typeof(string )),
                            new DataColumn("CORREO", typeof(string)) });

        if (txtCorreo.Text == string.Empty)
        {
            string cleanMessage = "Ingresar correo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {
            Boolean Email = email_bien_escrito(txtCorreo.Text.Trim());

            if (Email == true)
            {
                dtPersonal = Session["datos"] as DataTable;
                DataRow reg = dtPersonal.NewRow();
                for (int i = 0; i < 1; i++)
                {
                    reg["Row"] = GridView1.Rows.Count + 1;
                    reg["CORREO"] = txtCorreo.Text.Trim();
                    dtPersonal.Rows.Add(reg);
                }
            }
            else
            {
                string cleanMessage = "Formato de correo invalido";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            

        }
        //Session["datos"] = dtPersonal;
        txtCorreo.Text = string.Empty;
        Listar();
    }
    private Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}