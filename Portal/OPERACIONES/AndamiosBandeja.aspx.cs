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

public partial class OPERACIONES_AndamiosBandeja : System.Web.UI.Page
{
    string FolderANDAMIOS = ConfigurationManager.AppSettings["FolderANDAMIOS"];
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtSolped.Attributes.Add("onkeypress", "javascript:return lettersOnly(event);");
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDescarga);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            Anios();
            ControlProceso();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();

            Especialidad();
            area();

            Listar("", "");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void Especialidad()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlespecialidad.DataSource = obj.ListarParametros("ESPECIALIDAD", "SOL_ANDAMIOS");
        ddlespecialidad.DataTextField = "DES_ASUNTO";
        ddlespecialidad.DataValueField = "ID_PARAMETRO";
        ddlespecialidad.DataBind();
        ddlespecialidad.Items.Insert(0, new ListItem("--- TODOS ---", ""));


    }
    protected void area()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlarea.DataSource = obj.ListarParametros("AREA", "SOL_ANDAMIOS");
        ddlarea.DataTextField = "DES_ASUNTO";
        ddlarea.DataValueField = "ID_PARAMETRO";
        ddlarea.DataBind();

        ddlarea.Items.Insert(0, new ListItem("--- TODOS ---", ""));
    }
    protected void ControlProceso()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ANDAMIOS", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "CENTRO";
            ddlcentro.DataValueField = "CENTRO";
            ddlcentro.DataBind();
            ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar("", "");
    }
    protected void Listar(string campo, string direccion)
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

        string area = string.Empty;
        if (ddlarea.SelectedIndex == 0)
        {
            area  = string.Empty;
        }
        else
        {
            area = ddlarea.SelectedValue.ToString();
        }

        string especialidad = string.Empty;
        if (ddlespecialidad.SelectedIndex == 0)
        {
            especialidad = string.Empty;
        }
        else
        {
            especialidad = ddlespecialidad.SelectedValue.ToString();
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

        BL_SOL_ANDAMIOS obj = new BL_SOL_ANDAMIOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_SOL_ANDAMIOS_BANDEJA(Session["IDE_USUARIO"].ToString(), estado, ddlanio.SelectedValue.ToString(), txtTicket.Text, campo, direccion, centro,area,especialidad);
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
        Listar("", "");
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "");
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/Andamios.aspx");
    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/AndamiosBandeja.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    //static DataTable GetTableEstados()
    //{
    //    // Here we create a DataTable with four columns.
    //    DataTable table = new DataTable();
    //    table.Columns.Add("IDE", typeof(string));
    //    table.Columns.Add("DESCRIPCION", typeof(string));


    //    table.Rows.Add("P", "P");
    //    table.Rows.Add("E", "E");
    //    table.Rows.Add("T", "T");
    //    table.Rows.Add("R", "R");

    //    return table;
    //}
    protected void Procesar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string IDE_ANDAMIOS = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        string CECOS_GESTOR = GridView1.DataKeys[row.RowIndex].Values[2].ToString();
        //RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

        //string z= pk + " - " + rb.SelectedValue;
        BL_SOLPED obj = new BL_SOLPED();
        DataTable dt = new DataTable();

        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupAndamios(" + CECOS_GESTOR + "," + IDE_ANDAMIOS +  300 + "," + 300 + ");", true);

    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        limpiar();
    }
    protected void limpiar()
    {
        lblrpta.Text = string.Empty;
        lblCodigo.Text = string.Empty;
        txtrechazo.Text = string.Empty;
    }
    protected void Procesar_A(object sender, EventArgs e)
    {
        BL_SOLPED objX = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = objX.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ANDAMIOS", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            LinkButton LinkButtonA = ((LinkButton)sender);
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

            string  IDE_ANDAMIOS = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_ANDAMIOS"].ToString();
            string CECOS_GESTOR = GridView1.DataKeys[grdrow.RowIndex].Values["CECOS_GESTOR"].ToString();

            //string z= pk + " - " + rb.SelectedValue;
            BL_SOLPED obj = new BL_SOLPED();
            DataTable dt = new DataTable();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupAndamios('" + CECOS_GESTOR + "','" + IDE_ANDAMIOS + "'," + 500 + "," + 400 + ");", true);
        }
        else
        {
            string cleanMessage = "No tiene permisos de atención";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

    }

    protected void Procesar_R(object sender, EventArgs e)
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ANDAMIOS", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {

            LinkButton LinkButtonR = ((LinkButton)sender);

            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            int IDE_ANDAMIOS = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_ANDAMIOS"];
            lblCodigo.Text = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_ANDAMIOS"].ToString();
            lblrpta.Text = LinkButtonR.Text;


            ModalRegistro.Show();
            btnGrabar.Visible = true;// para rechazo
            txtrechazo.Visible = true;
            lblMsg.Text = "Comentarios de rechazo";

            // ocultar
            btnPrioridad.Visible = false;
            ddlPrioridad.Visible = false;
        }
        else
        {
            string cleanMessage = "No tiene permisos de atención";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void Procesar_Prioridad(object sender, EventArgs e)
    {

        LinkButton LinkButton1 = ((LinkButton)sender);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int IDE_ANDAMIOS = (int)GridView1.DataKeys[grdrow.RowIndex].Values["IDE_ANDAMIOS"];
        lblCodigo.Text = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_ANDAMIOS"].ToString();


        ModalRegistro.Show();
        btnGrabar.Visible = false;// para rechazo
        txtrechazo.Visible = false;
        lblMsg.Text = "Definir prioridad";



        ddlPrioridad.Visible = true;
        btnPrioridad.Visible = true;


        //BL_SOLPED obj = new BL_SOLPED();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE ANDAMIOS SUPERVISOR");
        //if (dtResultado.Rows.Count > 0)
        //{
        //    ModalRegistro.Show();
        //    btnGrabar.Visible = false;// para rechazo
        //    txtrechazo.Visible = false;
        //    lblMsg.Text = "Definir prioridad";



        //    ddlPrioridad.Visible = true;
        //    btnPrioridad.Visible = true;
        //}
        //else
        //{
        //    string cleanMessage = "No tiene permisos para definir prioridad";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        //}
       


    }

   

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        BL_SOL_ANDAMIOS obj = new BL_SOL_ANDAMIOS();
        DataTable dt = new DataTable();

        if (lblrpta.Text == "R")
        {
            if (txtrechazo.Text != string.Empty)
            {
                dt = obj.uspUPD_SOL_ANDAMIOS_PROCESAR(Convert.ToInt32(lblCodigo.Text), lblrpta.Text, txtrechazo.Text.Trim(), Session["IDE_USUARIO"].ToString());

                BL_SOL_ANDAMIOS objCorreo = new BL_SOL_ANDAMIOS();
                objCorreo.SP_ENVIARCORREO_SOL_ANDAMIOS_RPTA(Convert.ToInt32(lblCodigo.Text));

                string cleanMessage = "Registro procesado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                limpiar();
                Listar("", "");
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

        if (ViewState["DIRECCION"] == null)
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
        ",[IDE_ANDAMIOS]  ,[COD_TICKET],ANDAMIOS  ,convert(varchar(10),[FECHA],103) as [FECHA]   " +
        ",[IDE_USUARIO]  ,[FILE_ANDAMIOS] ,[FILE_RUTA]  ,[COMENTARIOS] ,[ESTADO] ," +
        " 'F_ESTADO' =    CASE " +
        "WHEN[ESTADO] = 1 THEN  'PENDIENTE ATENCION'        " +
        "WHEN[ESTADO] = 2 THEN  'SOL. GENERADA'    " +
        "WHEN[ESTADO] = 3 THEN  'EN EJECUCIÓN'          " +
        "WHEN[ESTADO] = 4 THEN  'SOL. TERMINADA'          " +
        "WHEN[ESTADO] = 5 THEN  'RECHAZADO'          " +
        "END         " +
        ",'ESTADO_RESUMEN'=          " +
        "CASE " +
        "WHEN[ESTADO] = 1 THEN  'P'     " +
        "WHEN[ESTADO] = 2 THEN  'E'    " +
        "WHEN[ESTADO] = 3 THEN  'T'     " +
        "WHEN[ESTADO] = 4 THEN  'R'    " +
        "WHEN[ESTADO] = 5 THEN  'R'    " +
        "END          " +
        ",'FLG_EDITAR' =          " +
        "CASE " +
        "WHEN[ESTADO] = 4 THEN  'False'   " +
        "WHEN[ESTADO] = 5 THEN  'False'     " +
        "ELSE 'True'          " +
        "END      " +
        ",'FLG_VISIBLE' =          " +
        "CASE " +
        "WHEN[ESTADO] = 4 THEN  'True'  " +
        "WHEN[ESTADO] = 5 THEN  'True'   " +
        "ELSE 'False'          " +
        "END        " +
        ",[FLG_ACTIVO]          " +
        ",[USUARIO_ATENCION]        " +
        ",convert(varchar(10),[FECHA_ENTREGA],103) as [FECHA_ENTREGA]   "+
        ",convert(varchar(10),[FECHA_DESMONTAJE],103) as [FECHA_DESMONTAJE]   " +
        ",convert(varchar(10),[FECHA_TERMINO],103) as [FECHA_TERMINO]   " +
        ",convert(varchar(10),[FECHA_ATENCION],103) as [FECHA_ATENCION]   " +
        ",[OBSERVACIONES]          " +
        ",[TICKET]          " +
        ",[IPCENTRO]          " +
        ",[CECOS]      " +
        ",cod_TICKET    " +
        ",SOLICITANTE  " +
        ", convert(varchar(10),[FECHA_REQUERIDA], 103) as [FECHA_REQUERIDA] " +
          ",(SELECT [DES_ASUNTO] FROM PARAMETROS P  WHERE  P.ID_PARAMETRO = S.[ESPECIALIDAD])[ESPECIALIDAD] " +
         ",(SELECT [DES_ASUNTO] FROM PARAMETROS P  WHERE  P.ID_PARAMETRO = S.[AREA])[AREA] " +
         ",(SELECT [DES_ASUNTO] FROM PARAMETROS P  WHERE  P.ID_PARAMETRO = S.[SOLICITUD])[SOLICITUD] " +
         ",(SELECT [DES_ASUNTO] FROM PARAMETROS P  WHERE  P.ID_PARAMETRO = S.[TIPO])[TIPO] " +

         ",CAPATAZ  " +
         ",SUPERVIDOR ,CECOS_GESTOR " +
         ",DURACION, HORAS,ISNULL(CONVERT(VARCHAR(10),PRIORIDAD),'--') PRIORIDAD  " +
            ",convert(varchar(10),[FECHA_ENTREGA],103) as [FECHA_ENTREGA]    " +
               ",convert(varchar(10),[FECHA_DESMONTAJE],103) as [FECHA_DESMONTAJE]  " +
        ",(SELECT SUBSTRING(P.[NOMBRES],1,1)  + '.' + P.APE_PATERNO FROM RRHH_PERSONAL_EMPRESA P WHERE P.ID_DNI = S.SOLICITANTE)NOM_SOLICITA " +
        ",(SELECT SUBSTRING(P.[NOMBRES], 1, 1) + '.' + P.APE_PATERNO FROM RRHH_PERSONAL_EMPRESA P WHERE  P.ID_DNI = S.[IDE_USUARIO])NOM_CREADO" +
        " FROM [SOL_ANDAMIOS] S  " +
        "WHERE [CECOS_GESTOR] in (SELECT [CENTRO] FROM RESPONSABLE_PROCESOS  WHERE DNI_RESPONSABLE  = " + "'" + Session["IDE_USUARIO"].ToString() + "'" + " AND [TIPO] in('RESPONSABLE ANDAMIOS','RESPONSABLE ANDAMIOS SUPERVISOR')  ) " +
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
            HeaderCell.ColumnSpan = 12;
            HeaderCell.BackColor = System.Drawing.Color.Green;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Prioridad";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.BackColor = System.Drawing.Color.Orange;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Atención";
            HeaderCell.ColumnSpan = 11;
            HeaderCell.BackColor = System.Drawing.Color.Navy;
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    protected void ddlcentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "");
    }

    protected void btnPrioridad_Click(object sender, EventArgs e)
    {
        BL_SOL_ANDAMIOS objCorreo = new BL_SOL_ANDAMIOS();
        objCorreo.uspSEL_SOL_ANDAMIOS_PRIORIDAD(Convert.ToInt32( ddlPrioridad.SelectedValue), Convert.ToInt32(lblCodigo.Text));

        string cleanMessage = "Registro satisfactorio";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        Listar("", "");
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        Listar("", "");
    }

    protected void btnDescarga_Click(object sender, ImageClickEventArgs e)
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

        string area = string.Empty;
        if (ddlarea.SelectedIndex == 0)
        {
            area = string.Empty;
        }
        else
        {
            area = ddlarea.SelectedValue.ToString();
        }

        string especialidad = string.Empty;
        if (ddlespecialidad.SelectedIndex == 0)
        {
            especialidad = string.Empty;
        }
        else
        {
            especialidad = ddlespecialidad.SelectedValue.ToString();
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

        BL_SOL_ANDAMIOS obj = new BL_SOL_ANDAMIOS();
        DataTable dtResultadoeExcel = new DataTable();
        dtResultadoeExcel = obj.uspSEL_SOL_ANDAMIOS_BANDEJA(Session["IDE_USUARIO"].ToString(), estado, ddlanio.SelectedValue.ToString(), txtTicket.Text, "", "", centro, area, especialidad);
        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();



            GridViewExportUtil.Export("ANDAMIOS_" + DateTime.Now + ".xls", gvExcel);
            return;

        }
        else
        {


        }
    }
    protected void Actualizar(object sender, EventArgs e)
    {
       
            ImageButton btnUpdate = ((ImageButton)sender);
            GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

            string IDE_ANDAMIOS = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_ANDAMIOS"].ToString();
            string CECOS_GESTOR = GridView1.DataKeys[grdrow.RowIndex].Values["CECOS_GESTOR"].ToString();

            //string z= pk + " - " + rb.SelectedValue;
            BL_SOLPED obj = new BL_SOLPED();
            DataTable dt = new DataTable();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "EditarSol('" + CECOS_GESTOR + "','" + IDE_ANDAMIOS + "'," + 500 + "," + 400 + ");", true);
      

    }

    protected void ddlespecialidad_SelectedIndexChanged(object sender, EventArgs e)
    {

        Listar("", "");
    }

    protected void ddlarea_SelectedIndexChanged(object sender, EventArgs e)
    {

        Listar("", "");
    }
}