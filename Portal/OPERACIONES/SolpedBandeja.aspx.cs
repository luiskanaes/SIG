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

using System.Data.SqlClient;


public partial class OPERACIONES_SolpedBandeja : System.Web.UI.Page
{
    string FolderSOLPED = ConfigurationManager.AppSettings["FolderSOLPED"];
  
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtSolped.Attributes.Add("onkeypress", "javascript:return lettersOnly(event);");
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDescargar);
        //ScriptManager.GetCurrent(this).RegisterPostBackControl(GridView1);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            Anios();
            ControlProcesoEmpresa();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();

            ddlEstados.SelectedIndex = 1;
            Listar("", "", "", "");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void ControlProcesoEmpresa()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS_EMPRESA(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN");
        if (dtResultado.Rows.Count > 0)
        {

            ddlEmpresa.DataSource = dtResultado;
            ddlEmpresa.DataTextField = "DES_ABREV";
            ddlEmpresa.DataValueField = "ID_EMPRESA";
            ddlEmpresa.DataBind();
            //ddlEmpresa.Items.Insert(0, new ListItem("--- TODOS ---", ""));

            ControlProceso();
        }
 
    }
    protected void ControlProceso()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN",ddlEmpresa.SelectedValue.ToString());
        if (dtResultado.Rows.Count > 0)
        {

            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "CENTRO";
            ddlcentro.DataValueField = "CENTRO";
            ddlcentro.DataBind();
            ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));

            hfPerfil.Value = dtResultado.Rows[0]["tipo"].ToString();
        }
        else
        {
            dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ALMACEN (SERVICIOS Y TRANSPORTE)", ddlEmpresa.SelectedValue.ToString());
            if (dtResultado.Rows.Count > 0)
            {
                ddlcentro.DataSource = dtResultado;
                ddlcentro.DataTextField = "CENTRO";
                ddlcentro.DataValueField = "CENTRO";
                ddlcentro.DataBind();
                ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
                hfPerfil.Value = dtResultado.Rows[0]["tipo"].ToString();
            }

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar("", "", "", "");
    }
    protected void Listar(string txtFecSol_F, string txtNOM_CREADO_F, string txtNOM_SOLICITA_F, string txtTICKET_F)
    {
        string estado = string.Empty;
        if (ddlEstados.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString();
        }

        string centro  = string.Empty;
        if (ddlcentro.SelectedIndex == 0)
        {
            centro = string.Empty;
        }
        else
        {
            centro = ddlcentro.SelectedValue.ToString();
        }

        string anio = string.Empty;
        if (ddlanio.SelectedIndex == 0)
        {
            anio = string.Empty;
        }
        else
        {
            anio = ddlanio.SelectedValue.ToString();
        }



        string IDE_EMPRESA = string.Empty;
        //if (ddlEmpresa.SelectedIndex == 0)
        //{
        //    IDE_EMPRESA = string.Empty;
        //}
        //else
        //{
        //    IDE_EMPRESA = ddlEmpresa.SelectedValue.ToString();
        //}
        IDE_EMPRESA = ddlEmpresa.SelectedValue.ToString();

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_SOLPED_BANDEJA(Session["IDE_USUARIO"].ToString(), estado, anio, txtFecSol_F, txtNOM_CREADO_F, txtNOM_SOLICITA_F, txtTICKET_F, centro, hfPerfil.Value, IDE_EMPRESA);
        if (dtResultado.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
    protected void Anios()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableAnio();
        if (dtResultado.Rows.Count > 0)
        {
            ddlanio.DataSource = dtResultado;
            ddlanio.DataTextField = "ANIO1";
            ddlanio.DataValueField = "ANIO";
            ddlanio.DataBind();
            ddlanio.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
    }
    static DataTable GetTableAnio()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ANIO", typeof(int));
        table.Columns.Add("ANIO1", typeof(string));

        int anio = 0;
        int anioActual = 0;
        anio = DateTime.Today.Year + 1;
        anioActual = DateTime.Today.Year + 1;
        for (int i = 0; i < 5; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
    }
    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "", "", "");
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "", "", "");
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Solped.aspx");
    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/SolpedBandeja.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        //BL_PERSONAL ObjEstado = new BL_PERSONAL();
        //DataTable dtResultado = new DataTable();
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    dtResultado = GetTableEstados();
        //    if (dtResultado.Rows.Count > 0)
        //    {
        //        RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
        //        rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
        //    }


        //}
    }
    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("IDE", typeof(string));
        table.Columns.Add("DESCRIPCION", typeof(string));


        table.Rows.Add("P", "P");
        table.Rows.Add("AP", "AP");
        table.Rows.Add("A", "A");
        table.Rows.Add("R", "R");

        return table;
    }
    protected void Procesar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        //RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

        //string z= pk + " - " + rb.SelectedValue;
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dt = new DataTable();

        //lblrpta.Text = rb.SelectedValue.ToString();

        //if (rb.SelectedValue == "R")
        //{
        //    ModalRegistro.Show();
        //    lblCodigo.Text = pk.ToString();
        //    lblMensaje.Visible = false;
        //    txtSolped.Visible = false;
        //}
        //else if (rb.SelectedValue == "A")
        //{
        //    lblMensaje.Visible = true ;
        //    txtSolped.Visible = true;
        //    ModalRegistro.Show();
        //    lblCodigo.Text = pk.ToString();
        //}
        //else if (rb.SelectedValue == "AP")
        //{
        //    lblMensaje.Visible = true;
        //    txtSolped.Visible = true;
        //    ModalRegistro.Show();
        //    lblCodigo.Text = pk.ToString();
        //}
        //else
        //{
        //    dt = obj.uspUPD_SOLPED_PROCESAR(Convert.ToInt32(pk), rb.SelectedValue, "", Session["IDE_USUARIO"].ToString(),txtSustento.Text.Trim());
        //    string cleanMessage = "Registro procesado";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        //    Listar();
        //}

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dt = new DataTable();

        if (lblrpta.Text == "R")
        {
            if (txtSustento.Text != string.Empty)
            {
                dt = obj.uspUPD_SOLPED_PROCESAR(Convert.ToInt32(lblCodigo.Text), lblrpta.Text, txtSustento.Text.Trim(), Session["IDE_USUARIO"].ToString(), "",rdoTipo.SelectedValue.ToString());

                BL_SOLPED objCorreo = new BL_SOLPED();
                objCorreo.SP_ENVIARCORREO_SOLPED_RPTA(Convert.ToInt32(lblCodigo.Text));

                string cleanMessage = "Registro procesado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                limpiar();
                Listar("", "", "", "");
            }
            else
            {
                string cleanMessage = "Ingresar sustento";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
               
            }
        }
        else
        {
            if ( txtSolped.Text != string.Empty)
            {
               if(txtSustento.Text != string.Empty)
                {
                    if (txtSolped.Text.Length == 10)
                    {
                        dt = obj.uspUPD_SOLPED_PROCESAR(Convert.ToInt32(lblCodigo.Text), lblrpta.Text, txtSustento.Text.Trim(), Session["IDE_USUARIO"].ToString(), txtSolped.Text, rdoTipo.SelectedValue.ToString());

                        BL_SOLPED objCorreo = new BL_SOLPED();
                        objCorreo.SP_ENVIARCORREO_SOLPED_RPTA(Convert.ToInt32(lblCodigo.Text));
                        string cleanMessage = "Solicitud procesada";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        limpiar();
                        Listar("", "", "", "");
                    }
                    else
                    {

                        string cleanMessage = "Codigo ingresado, falta completar digitos (10) ";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                       
                    }
                }
               else
                {
                    string cleanMessage = "Falta ingresar sustento";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                   
                }
                
            }
            else
            {
                string cleanMessage = "Falta ingresar codigo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
              
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        limpiar();
    }
    protected void limpiar()
    {
        lblrpta.Text = string.Empty;
        lblCodigo.Text = string.Empty;
        txtSolped.Text = string.Empty;
        txtSustento.Text = string.Empty;
        txtrechazo.Text = string.Empty;
    }
    protected void Procesar_A(object sender, EventArgs e)
    {

        LinkButton LinkButtonA = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        //int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_SOLPED = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_SOLPED"];
        lblrpta.Text = LinkButtonA.Text;
        lblCodigo.Text = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_SOLPED"].ToString();
        string SOLPED = GridView1.DataKeys[grdrow.RowIndex].Values["SOLPED"].ToString();
        ModalRegistro.Show();

        rdoTipo.Visible = true;
        //Button0.Enabled = true;
        //Button1.Enabled = true;
        //Button2.Enabled = true;
        //Button3.Enabled = true;
        //Button4.Enabled = true;
        //Button5.Enabled = true;
        //Button6.Enabled = true;
        //Button7.Enabled = true;
        //Button8.Enabled = true;
        //Button9.Enabled = true;
        lblMensaje.Visible = true;
        txtSolped.Visible = true;
        lblobservacion.Text = "Comentarios de aprobación";

        btnGrabar.Visible = false;// para rechazo
        btnGuardar.Visible = true;

        txtSustento.Visible = true;
        txtrechazo.Visible = false;
        txtSolped.Text = SOLPED;
    }
    protected void Procesar_AA(object sender, EventArgs e)
    {
        ModalRegistro.Show();
    }
    protected void Procesar_AP(object sender, EventArgs e)
    {

        LinkButton LinkButtonAP = ((LinkButton)sender);
       
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        //int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_SOLPED = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_SOLPED"];
        string SOLPED = GridView1.DataKeys[grdrow.RowIndex].Values["SOLPED"].ToString();
        lblCodigo.Text = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_SOLPED"].ToString();
        lblrpta.Text = LinkButtonAP.Text;
        ModalRegistro.Show();


        rdoTipo.Visible = true;
        //Button0.Enabled = true;
        //Button1.Enabled = true;
        //Button2.Enabled = true;
        //Button3.Enabled = true;
        //Button4.Enabled = true;
        //Button5.Enabled = true;
        //Button6.Enabled = true;
        //Button7.Enabled = true;
        //Button8.Enabled = true;
        //Button9.Enabled = true;
        lblMensaje.Visible = true;
        txtSolped.Visible = true;

        lblobservacion.Text = "Comentarios de atención parcial";
        btnGrabar.Visible = false;// para rechazo
        btnGuardar.Visible = true ;

        txtSustento.Visible = true ;
        txtrechazo.Visible = false;
        txtSolped.Text = SOLPED;
    }
    protected void Procesar_R(object sender, EventArgs e)
    {

        LinkButton LinkButtonR = ((LinkButton)sender);
       
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        //int IDE_FICHA = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FICHA"];
        int IDE_SOLPED = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_SOLPED"];
        lblCodigo.Text = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_SOLPED"].ToString();
        lblrpta.Text = LinkButtonR.Text;

        ModalRegistro.Show();

        rdoTipo.Visible  = false;
        //Button0.Enabled = false;
        //Button1.Enabled = false;
        //Button2.Enabled = false;
        //Button3.Enabled = false;
        //Button4.Enabled = false;
        //Button5.Enabled = false;
        //Button6.Enabled = false;
        //Button7.Enabled = false;
        //Button8.Enabled = false;
        //Button9.Enabled = false;

        lblMensaje.Visible = false;
        txtSolped.Visible = false;
        btnGrabar.Visible = true;// para rechazo
        btnGuardar.Visible = false ;
        txtSustento.Visible = false;
        txtrechazo.Visible = true ;
        lblobservacion.Text = "Comentarios de rechazo";
    }




    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button1.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button2.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button3.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button4.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button5.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button6_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button6.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button7_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button7.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button8_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button8.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button9_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button9.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }
    //}

    //protected void Button0_Click(object sender, EventArgs e)
    //{
    //    if (txtSolped.Text.Length < 10)
    //    {
    //        txtSolped.Text = txtSolped.Text + Button0.Text;
    //        ModalRegistro.Show();
    //    }
    //    else
    //    {
    //        ModalRegistro.Show();
    //    }

    //}

    

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dt = new DataTable();

        if (lblrpta.Text == "R")
        {
            if (txtrechazo.Text != string.Empty)
            {
                dt = obj.uspUPD_SOLPED_PROCESAR(Convert.ToInt32(lblCodigo.Text), lblrpta.Text, txtrechazo.Text.Trim(), Session["IDE_USUARIO"].ToString(), "", rdoTipo.SelectedValue.ToString());

                BL_SOLPED objCorreo = new BL_SOLPED();
                objCorreo.SP_ENVIARCORREO_SOLPED_RPTA(Convert.ToInt32(lblCodigo.Text));

                string cleanMessage = "Registro procesado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                limpiar();
                Listar("","","","");
            }
            else
            {
                string cleanMessage = "Ingresar sustento";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
        }
       
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        // Use the RowType property to determine whether the 
        // row being created is the header row. 
        if (e.Row.RowType == DataControlRowType.Header)
        {
            // Call the GetSortColumnIndex helper method to determine
            // the index of the column being sorted.
            int sortColumnIndex = GetSortColumnIndex();

            if (sortColumnIndex != -1)
            {
                // Call the AddSortImage helper method to add
                // a sort direction image to the appropriate
                // column header. 
                AddSortImage(sortColumnIndex, e.Row);
            }
        }
    }
    protected int GetSortColumnIndex()
    {

        // Iterate through the Columns collection to determine the index
        // of the column being sorted.
        foreach (DataControlField field in GridView1.Columns)
        {
            if (field.SortExpression == GridView1.SortExpression)
            {
                return GridView1.Columns.IndexOf(field);
            }
        }

        return -1;
    }
    protected void AddSortImage(int columnIndex, GridViewRow headerRow)
    {

        // Create the sorting image based on the sort direction.
        Image sortImage = new Image();
        if (GridView1.SortDirection == SortDirection.Ascending)
        {
            sortImage.ImageUrl = "~/Images/Ascending.jpg";
            sortImage.AlternateText = "Ascending Order";
        }
        else
        {
            sortImage.ImageUrl = "~/Images/Descending.jpg";
            sortImage.AlternateText = "Descending Order";
        }

        // Add the image to the appropriate header cell.
        headerRow.Cells[columnIndex].Controls.Add(sortImage);

    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string columnas = e.SortExpression;
        SortDirection direccion;

        if(ViewState["DIRECCION"]==null)
        {
            direccion = SortDirection.Ascending;
            ViewState["DIRECCION"] = SortDirection.Descending;
        }
        else
        {
            direccion = (SortDirection)ViewState["DIRECCION"];
            if (direccion == SortDirection.Ascending)
            {
                ViewState["DIRECCION"] = SortDirection.Descending;
            }
            else
            {
                ViewState["DIRECCION"] = SortDirection.Ascending;
            }

        }
        LlenarGridView(columnas, direccion);
    }
    protected void LlenarGridView(string campo, SortDirection direccion)
    {
        string orden = "";
        if (direccion == SortDirection.Ascending)
        {
            orden = "ASC";
        }
        else
        {
            orden = "DESC";
        }

        DataTable t1 = new DataTable();

        string estado = string.Empty;


        string sql = "SELECT  ROW_NUMBER() OVER(ORDER BY  COD_TICKET)  ROW " +
        ",[IDE_SOLPED]  ,[SOLPED]  ,convert(varchar(10),[FECHA],103) as [FECHA]   " +
        ",[IDE_USUARIO]  ,[FILE_SOLPED] ,[FILE_RUTA]  ,[COMENTARIOS] ,[ESTADO] ," +
        " 'F_ESTADO' =    CASE " +
        "WHEN[ESTADO] = 1 THEN  'PENDIENTE ATENCION'        " +
        "WHEN[ESTADO] = 2 THEN  'ATENCION PARCIAL'    " +
        "WHEN[ESTADO] = 3 THEN  'ATENDIDO'          " +
        "WHEN[ESTADO] = 4 THEN  'RECHAZADO'          " +
        "END         " +
        ",'ESTADO_RESUMEN'=          " +
        "CASE " +
        "WHEN[ESTADO] = 1 THEN  'P'     " +
        "WHEN[ESTADO] = 2 THEN  'AP'    " +
        "WHEN[ESTADO] = 3 THEN  'A'     " +
        "WHEN[ESTADO] = 4 THEN  'R'    " +
        "END          " +
        ",'FLG_EDITAR' =          " +
        "CASE " +
        "WHEN[ESTADO] = 3 THEN  'False'   " +
        "WHEN[ESTADO] = 4 THEN  'False'     " +
        "ELSE 'True'          " +
        "END      " +
        ",'FLG_VISIBLE' =          " +
        "CASE " +
        "WHEN[ESTADO] = 3 THEN  'True'  " +
        "WHEN[ESTADO] = 4 THEN  'True'   " +
        "ELSE 'False'          " +
        "END        " +
        ",[FLG_ACTIVO]          " +
        ",[FECHA_CARGA]          " +
        ",[USUARIO_ATENCION]        " +
         ",(SELECT [DES_CAMPO1] FROM [PARAMETROS] WHERE  [ID_PARAMETRO] =TIPO_SOLICITUD)SOLICITUD        " +
        ",convert(varchar(10),[FECHA_ATENCION],103) as [FECHA_ATENCION]   " +
        ",[OBSERVACIONES]          " +
        ",[TICKET]          " +
        ",[IPCENTRO]          " +
        ",[CECOS]      " +
        ",TIPO  " +
        ",cod_TICKET    " +
        ",COD_SI  " +
        ",SOLICITANTE  " +
        ",(SELECT SUBSTRING(P.[NOMBRES],1,1)  + '.' + P.APE_PATERNO FROM RRHH_PERSONAL_EMPRESA P WHERE P.ID_DNI = S.SOLICITANTE)NOM_SOLICITA " +
        ",(SELECT SUBSTRING(P.[NOMBRES], 1, 1) + '.' + P.APE_PATERNO FROM RRHH_PERSONAL_EMPRESA P WHERE  P.ID_DNI = S.[IDE_USUARIO])NOM_CREADO" +
        " FROM[SOLPED] S  " +
        "WHERE [CECOS_GESTOR] in (SELECT [CENTRO] FROM RESPONSABLE_PROCESOS  WHERE DNI_RESPONSABLE  = " + "'" + Session["IDE_USUARIO"].ToString() + "'" + " AND [TIPO] ='RESPONSABLE ALMACEN'  ) " +
        " and YEAR ([FECHA]) = " + "'" + ddlanio.SelectedValue.ToString() + "'";

        if (ddlEstados.SelectedIndex == 0)
        {
            sql = sql + " order by " + campo + " " + orden;
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString();
            sql = sql + " and ESTADO = " + "'" + estado + "'" + 
                " order by " + campo + " " + orden;
        }

  

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);


                using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                {
                    a.Fill(t1);
                }
                cmd.CommandTimeout = 99999;
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        GridView1.Visible = true;
        GridView1.DataSource = t1;
        GridView1.DataBind();
        //Listar(campo,orden );
    }

    protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Solicitud";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Atención";
            HeaderCell.ColumnSpan = 7;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    protected void ddlcentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "", "", "");
    }



    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        ConsultarLista();
    }

    protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
    {

        TextBox txtFecSol_F = (TextBox)GridView1.HeaderRow.FindControl("txtFecSol_F");
        TextBox txtNOM_CREADO_F = (TextBox)GridView1.HeaderRow.FindControl("txtNOM_CREADO_F");
        TextBox txtNOM_SOLICITA_F = (TextBox)GridView1.HeaderRow.FindControl("txtNOM_SOLICITA_F");
        TextBox txtTICKET_F = (TextBox)GridView1.HeaderRow.FindControl("txtTICKET_F");

        string estado = string.Empty;
        if (ddlEstados.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstados.SelectedValue.ToString();
        }

        string centro = string.Empty;
        if (ddlcentro.SelectedIndex == 0)
        {
            centro = string.Empty;
        }
        else
        {
            centro = ddlcentro.SelectedValue.ToString();
        }

        string IDE_EMPRESA = string.Empty;
        //if (ddlEmpresa.SelectedIndex == 0)
        //{
        //    IDE_EMPRESA = string.Empty;
        //}
        //else
        //{
        //    IDE_EMPRESA = ddlEmpresa.SelectedValue.ToString();
        //}
        IDE_EMPRESA = ddlEmpresa.SelectedValue.ToString();
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_SOLPED_BANDEJA(Session["IDE_USUARIO"].ToString(), estado, ddlanio.SelectedValue.ToString(), txtFecSol_F.Text.Trim(), txtNOM_CREADO_F.Text.Trim(), txtNOM_SOLICITA_F.Text.Trim(), txtTICKET_F.Text.Trim(), centro, hfPerfil.Value , IDE_EMPRESA);
        if (dtResultado.Rows.Count > 0)
        {
            gvExcel.Visible = true;
            gvExcel.DataSource = dtResultado;
            gvExcel.DataBind();

            GridViewExportUtil.Export("SOLPED_" + DateTime.Now + ".xls", gvExcel);
            return;
        }
        


       
    }

    protected void ConsultarLista()
    {
        if(GridView1.Rows.Count> 0)
        {
            TextBox txtFecSol_F = (TextBox)GridView1.HeaderRow.FindControl("txtFecSol_F");
            TextBox txtNOM_CREADO_F = (TextBox)GridView1.HeaderRow.FindControl("txtNOM_CREADO_F");
            TextBox txtNOM_SOLICITA_F = (TextBox)GridView1.HeaderRow.FindControl("txtNOM_SOLICITA_F");
            TextBox txtTICKET_F = (TextBox)GridView1.HeaderRow.FindControl("txtTICKET_F");
            Listar(txtFecSol_F.Text.Trim(),
            txtNOM_CREADO_F.Text.Trim(),
            txtNOM_SOLICITA_F.Text.Trim(),
            txtTICKET_F.Text.Trim());
        }
        else
        {
            Listar("","","","");
        }
        
    }
    protected void txtTICKET_F_TextChanged(object sender, EventArgs e)
    {
        ConsultarLista();
    }
    protected void txtNOM_CREADO_F_TextChanged(object sender, EventArgs e)
    {
        ConsultarLista();
    }
    protected void txtNOM_SOLICITA_F_TextChanged(object sender, EventArgs e)
    {
        ConsultarLista();
    }
    protected void txtFecSol_F_TextChanged(object sender, EventArgs e)
    {
        ConsultarLista();
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarLista();
    }
}