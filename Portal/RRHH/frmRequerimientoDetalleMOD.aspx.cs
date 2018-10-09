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
using System.Web.Services;
using System.Web.Script.Services;

public partial class frmRequerimientoDetalleMOD : System.Web.UI.Page
{
    public string ControlUsuario;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        string requerimiento = Convert.ToString(Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"]);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {
            //cargarDatosMOD();
            ControlBotones();
            cargarDatosMOD();
            especialidad();
            cargos();

        }
    }
    protected void ControlBotones()
    {
        if (ControlUsuario == "ADMIN")
        {

        }
        else
        {

        }
    }
    protected void btnSeguimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/SeguimientoMOD.aspx");
    }
    protected void btnPersonal_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmPersonalMOD.aspx");
    }
    protected void btnControl_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmMOD.aspx");
    }
   
    protected void btnRequerimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmRequerimientoMOD.aspx");
    }
    [WebMethod]
    [ScriptMethod]
    public static List<string> GetPersonal(string prefixText)
    {
        List<string> lista = new List<string>();
        try
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager
                    .ConnectionStrings["Conexion"].ConnectionString;
            con.Open();

            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_PERSONAL_BUSQUEDA_NOMBRE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("DES_NOMBRE", SqlDbType.VarChar, 100).Value = prefixText;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
   
            IDataReader lector = cmd.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(lector.GetString(0) + " - " + lector.GetString(1));
            }

            lector.Close();
            return lista;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            return null;
        }
    }

    protected void btnActualizar_Click(object sender, ImageClickEventArgs e)
    {
        cargarDatosMOD();
    }
            

    protected void cargarDatosMOD()
    {
        string cleanMessage = string.Empty;        
        
        string requerimiento = Convert.ToString(Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"]);

        DataTable dtResultado = new DataTable();
        dtResultado = new BL_PERSONAL().Mant_Bus_Det_Req_Mod(requerimiento);

        if (dtResultado.Rows.Count > 0)
        {
            txtEmpresa.Text = dtResultado.Rows[0]["EMPRESA"].ToString();
            txtProyecto.Text = dtResultado.Rows[0]["PROYECTO"].ToString();
            txtRequerimiento.Text = dtResultado.Rows[0]["NUMERO_REQUISICION"].ToString();
            txtItem.Text = dtResultado.Rows[0]["SECUENCIA"].ToString();
            txtCeco.Text = dtResultado.Rows[0]["CENTROCOSTO"].ToString();
            txtCargo.Text = dtResultado.Rows[0]["CARGO"].ToString();
            txtEspecialidad.Text = dtResultado.Rows[0]["ESPECIALIDAD"].ToString();
            txtEstado.Text = dtResultado.Rows[0]["ESTADO"].ToString();
            //int idPersona = Convert.ToInt32(dtResultado.Rows[0]["IDE_EMPLEADO"].ToString());
        }


        DataTable dtResultadoPersonal = new DataTable();
        dtResultadoPersonal = new BL_PERSONAL().Mant_Bus_Det_Req_Per_Mod(requerimiento);

        if (dtResultadoPersonal.Rows.Count > 0)
        {
            gridPersonal.DataSource = dtResultadoPersonal;
            gridPersonal.DataBind(); 
        }



        

    } 
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/RRHH/frmRequerimientoMOD.aspx");
    }

    protected void Seleccionar_REQUERIMIENTO(object sender, ImageClickEventArgs e)
    {

        //PanelDatos.Visible = true;
        ImageButton btnSeleccionar = ((ImageButton)sender);
        ////lblIde_MOD.Text = btnSeleccionar.CommandArgument;

        Session["DES_DNI"] = btnSeleccionar.CommandArgument;
        string script = "window.open('frmMOD.aspx', '')";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

        ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('frmMOD.aspx','Title','height=1600,width=1800,left=10, top=10,scrollbars, resizable=no, location=center, toolbar=0')</script>", false);

        //Response.Redirect("frmMOD.aspx");//?id=123
        //PanelDatos.Visible = true;
        //ImageButton btnSeleccionar = ((ImageButton)sender);
        ////lblIde_MOD.Text = btnSeleccionar.CommandArgument;
        //Datos(Convert.ToInt32 (btnSeleccionar.CommandArgument ));
        //Estado();
        //string cleanMessage = string.Empty;
        //ImageButton btnSeleccionarRequerimiento = ((ImageButton)sender);
        //int ID_REQUERIMIENTO_PERSONAL = Convert.ToInt32(btnSeleccionarRequerimiento.CommandArgument);
        //Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"] = btnSeleccionarRequerimiento.CommandArgument;
        //BL_PERSONAL obj = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //DataTable dtIResultado = new DataTable();

        //dtIResultado = obj.buscar_CantidadRequerimientos_MOD(ID_REQUERIMIENTO_PERSONAL);

        //if (dtIResultado.Rows[0]["TOTAL"].ToString().Equals("0"))
        //{

        //    dtResultado = obj.buscar_PersonalDisponible_MOD(ID_REQUERIMIENTO_PERSONAL);
             

        //    if (dtResultado.Rows.Count > 0)
        //    {
        //        ModalRegistro.Show();
        //        //btnCerrar.Visible = true;

        //    }
        //    else
        //    {
        //        cleanMessage = "No existen postulantes matriculados";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        //    }

        //    //gridPersonalDisponible.DataSource = dtResultado;
        //    //gridPersonalDisponible.DataBind();
        //}
        //else
        //{
        //    Response.Redirect("~/RRHH/frmRequerimientoDetalleMOD.aspx");
        //}
    }



    protected void Seleccionar_Personal(object sender, ImageClickEventArgs e)
    {
        //PanelDatos.Visible = true;
        ImageButton btnSeleccionar = ((ImageButton)sender);
        ////lblIde_MOD.Text = btnSeleccionar.CommandArgument;

        Session["DES_DNI"] = btnSeleccionar.CommandArgument;
        string script = "window.open('frmMOD.aspx', '')";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

        ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('frmMOD.aspx','Title','height=1600,width=1800,left=10, top=10,scrollbars, resizable=no, location=center, toolbar=0')</script>", false);

        //Response.Redirect("frmMOD.aspx");//?id=123
    }

    protected void ddlCargosB_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BL_PERSONAL obj = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.buscar_PersonalDisponible_Cargo(Convert.ToInt32(ddlCargo.SelectedValue));

        //if (dtResultado.Rows.Count > 0)
        //{
        //    //ModalRegistro.Dispose();
        //    ModalRegistro.Show();

        //    //btnAsignar.Visible = false;
        //    btnNo.Visible = false;
        //    btnCerrar.Visible = true;

        //}
        //else
        //{

        //}

        //gridPersonalDisponible.DataSource = dtResultado;
        //gridPersonalDisponible.DataBind();

    }

    protected void ddlEspecialidadesB_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BL_PERSONAL obj = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.buscar_PersonalDisponible_Cargo(Convert.ToInt32(ddlCargo.SelectedValue));

        //if (dtResultado.Rows.Count > 0)
        //{
        //    //ModalRegistro.Dispose();
        //    ModalRegistro.Show();

        //    //btnAsignar.Visible = false;
        //    btnNo.Visible = false;
        //    btnCerrar.Visible = true;

        //}
        //else
        //{

        //}

        //gridPersonalDisponible.DataSource = dtResultado;
        //gridPersonalDisponible.DataBind();

    }


    protected void btnBuscarModal_Click(object sender, EventArgs e)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.buscar_PersonalDisponible_Cargo_MOD(Convert.ToInt32(ddlCargosB.SelectedValue), txtNombre.Text, ddlEspecialidadesB.SelectedValue);
        string cleanMessage = string.Empty;

        if (dtResultado.Rows.Count == 0)
        {
            //ModalRegistro.Dispose();
            //

            ModalRegistro.Show();
            gridPersonalDisponible.DataSource = null;
            gridPersonalDisponible.DataBind();
            btnCerrar.Visible = true;

            //btnAsignar.Visible = false;
            //btnNo.Visible = false;
            //btnCerrar.Visible = true;

        }
        else
        {
            ModalRegistro.Show();
            gridPersonalDisponible.DataSource = dtResultado;
            gridPersonalDisponible.DataBind();
            btnCerrar.Visible = true;
        }


    }


    protected void GvCities_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = (GridView)sender;
        gv.PageIndex = e.NewPageIndex;
        //bindCitiesGrid();
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.buscar_PersonalDisponible_Cargo_MOD(Convert.ToInt32(ddlCargosB.SelectedValue), txtNombre.Text, ddlEspecialidadesB.SelectedValue);

        if (dtResultado.Rows.Count > 0)
        {
            //ModalRegistro.Dispose();
            ModalRegistro.Show();

            //btnAsignar.Visible = false;
            //btnNo.Visible = false;
            btnCerrar.Visible = true;

        }
        else
        {

        }

        gridPersonalDisponible.DataSource = dtResultado;
        gridPersonalDisponible.DataBind();
    }



    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        //txtPersonal.Text = string.Empty;
    }


    
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        DataTable dtResultado = new DataTable();
        int requerimiento = Convert.ToInt32(Session["ID_DETALLE_REQUERIMIENTO_PERSONAL"]);
          BL_PERSONAL obj = new BL_PERSONAL();


          dtResultado = obj.buscar_PersonalDisponible_MOD(requerimiento);
         

            if (dtResultado.Rows.Count > 0)
            {
                ModalRegistro.Show();
                btnCerrar.Visible = true;
                gridPersonalDisponible.DataSource = dtResultado;
                gridPersonalDisponible.DataBind();
                cargarDatosMOD();     

            }
        //ModalRegistro.Show();
        //btnCerrar.Visible = true;
        ////PanelDatos.Visible = true;
        //ImageButton btnSeleccionar = ((ImageButton)sender);
        //////lblIde_MOD.Text = btnSeleccionar.CommandArgument;

        //Session["DES_DNI"] = btnSeleccionar.CommandArgument;
        //string script = "window.open('frmMOD.aspx', '')";
        ////ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

        //ScriptManager.RegisterStartupScript(this, typeof(Page), "PopupWindow", "<script language='javascript'>window.open('frmMOD.aspx','Title','height=1600,width=1800,left=10, top=10,scrollbars, resizable=no, location=center, toolbar=0')</script>", false);

        ////Response.Redirect("frmMOD.aspx");//?id=123
    }

    protected void especialidad()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable(); 

        ddlEspecialidadesB.DataSource = obj.ListarParametros("IDE_ESPECIALIDAD", "RRHH_MOD");
        ddlEspecialidadesB.DataTextField = "DES_ASUNTO";
        ddlEspecialidadesB.DataValueField = "ID_PARAMETRO";
        ddlEspecialidadesB.DataBind();
        this.ddlEspecialidadesB.Items.Insert(0, new ListItem("--------TODOS--------", "0"));
          
    }


    protected void cargos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable(); 

        ddlCargosB.DataSource = obj.ListarParametros("IDE_CATEGORIA", "RRHH_MOD");
        ddlCargosB.DataTextField = "DES_ASUNTO";
        ddlCargosB.DataValueField = "ID_PARAMETRO";
        ddlCargosB.DataBind();
        this.ddlCargosB.Items.Insert(0, new ListItem("--------TODOS--------", "0"));
         

    }

}