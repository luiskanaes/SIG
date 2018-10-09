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

public partial class OPERACIONES_AprobarMovil : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CareMenor"].ToString());
    //string Proyecto;
    //string Usuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        string proyecto;
        string codigo;
        string tipo;
        if (Session["PROYECTO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            proyecto = Session["PROYECTO"].ToString();
            codigo = Session["Codigo"].ToString ();
            tipo = Session["Tipo"].ToString();

            lblNombre.Text = "Bienvenido : " + ConsultaEmpleado();
            ListarEquipos();
        }
        
    }
    public string  ConsultaEmpleado()
    {

        string  result = string.Empty ;
        string sql = "SELECT top 1 [NOMBRE] FROM [RequerimientoEncargo] WHERE [CODIGO]=" + "'" + Session["Codigo"] + "'";
        
            try
            {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            result = cmd.ExecuteScalar().ToString ();
            cmd.Dispose();
            con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
        return result;
    }
    protected void ListarEquipos()
    {
        
        DataTable dt = new DataTable();
        dt = GetDataBusquedaUCG();
        //if (Session["Tipo"].ToString() =="JP")
        //{
        //    dt = GetDataBusqueda();
        //}
        //else
        //{
        //    dt = GetDataBusquedaUCG();
        //}
       


        if (dt.Rows.Count > 0)
        {
            lstRol.DataSource = dt;
            lstRol.DataBind();
            lstRol.Visible = true;
            btnAprobar.Visible = true;
        }
        else
        {
            lstRol.Visible = false;
            btnAprobar.Visible = false;
            Show(Page, this.GetType(), "No existen equipos pendientes de aprobación");
            return;
        }

    }
    public static void Show(System.Web.UI.Page pagina, System.Type tipo, String mensaje)
    {
        ScriptManager.RegisterStartupScript(pagina, tipo, "Mensaje", "alert('" + mensaje.Replace("'", "\"") + "');", true);
    }
    private DataTable GetDataBusqueda()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_REQUERIMIENTOMOVIL_PENDIENTE_ENVIO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@centro_costo ", SqlDbType.VarChar, 150).Value = Session["PROYECTO"];
        cmd.Parameters.Add("@FLG_ENVIO", SqlDbType.Int).Value = 0;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable GetDataBusquedaUCG()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_REQUERIMIENTOMOVIL_PENDIENTE_UCG", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@centro_costo ", SqlDbType.VarChar, 150).Value = Session["PROYECTO"];


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    protected void LogAprobar(object sender, EventArgs e)
    {
        try
        {

            int intContador = 0;

            int Cod;


            for (int i = 0; i < lstRol.Items.Count; i++)
            {

                ListViewItem Fila = lstRol.Items[i];
                RadioButtonList rb = (RadioButtonList)Fila.FindControl("rdoOpcion");
                if (rb.SelectedValue != string.Empty )
                {
                    Cod = Convert.ToInt32(lstRol.DataKeys[i].Values[0].ToString()); // extrae key
                    DataTable dt = new DataTable();
                    //una sola aprobacion

                    dt = AprobarEnvioJP(Cod, rb.SelectedValue);
                    dt = AprobarEnvio(Cod, rb.SelectedValue);

                    //if (Session["Tipo"].ToString() == "JP")
                    //{
                    //    dt = AprobarEnvioJP(Cod, rb.SelectedValue);
                    //    intContador += 1;
                    //}
                    //else
                    //{
                    //    //gerencia UCG
                    //    dt = AprobarEnvio(Cod, rb.SelectedValue);
                    //    intContador += 1;
                    //}
                
                }
            }

            if(intContador > 0)
            {
                DataTable dtEnviar = new DataTable();
                dtEnviar = EnviarCorreoEncargado();

                //if (Session["Tipo"].ToString() == "JP")
                //{
                //    DataTable dtEnviar = new DataTable();
                //    dtEnviar = EnviarCorreoEncargadoUCG();
                //}
                //else
                //{
                //    //gerencia UCG
                //    DataTable dtEnviar = new DataTable();
                //    dtEnviar = EnviarCorreoEncargado();
                //}
               
            }

            Show(Page, this.GetType(), "Registros enviados para su atención");

            ListarEquipos();

        }
        catch (Exception ex)
        {

        }


        
        
        

    }
    private DataTable AprobarEnvioJP(int cod, string estado)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_REQUERIMIENTOMOVIL_INSERT_UCG", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@centro_costo", SqlDbType.VarChar, 150).Value = Session["PROYECTO"].ToString();
        cmd.Parameters.Add("@id_detalle", SqlDbType.Int).Value = cod;
        cmd.Parameters.Add("@SITUACION", SqlDbType.VarChar, 10).Value = estado;
        cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 30).Value = Session["Codigo"].ToString();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable AprobarEnvio(int cod,string estado)
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("uspSEL_REQUERIMIENTOMOVIL_INSERT_MOBILE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@centro_costo", SqlDbType.VarChar, 150).Value = Session["PROYECTO"].ToString ();
        cmd.Parameters.Add("@usuario", SqlDbType.VarChar,30).Value = Session["Codigo"].ToString();
        cmd.Parameters.Add("@id_detalle", SqlDbType.Int ).Value = cod;
        cmd.Parameters.Add("@SITUACION", SqlDbType.VarChar,10).Value = estado;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable EnviarCorreoEncargado()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("SP_EnviarCorreo_EncargadoMovil", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@proyecto", SqlDbType.VarChar, 150).Value = Session["PROYECTO"].ToString();


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
    private DataTable EnviarCorreoEncargadoUCG()
    {

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("SP_EnviarCorreo_Recurso_GerenciaUCG", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@proyecto ", SqlDbType.VarChar, 150).Value = Session["PROYECTO"].ToString();


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;

        da.Fill(dt);

        return dt;
    }
}