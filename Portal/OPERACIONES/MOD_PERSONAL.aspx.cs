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

public partial class OPERACIONES_MOD_PERSONAL : System.Web.UI.Page
{
    string IDE_MOD, IDE_REQUERIMIENTO;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDescarga);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Etapas();
            Mano();
            Nacionalidad();
            Condicion();
            fotocheck();
            Cuidad();
            IDE_REQUERIMIENTO = Session["IDE_REQUERIMIENTO"].ToString();
            Listar();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void Etapas()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEtapas.DataSource = obj.ListarParametros_orden("IDE_ETAPAS", "MOD_REQUERIMIENTO_PERSONAL");
        ddlEtapas.DataTextField = "DES_ASUNTO";
        ddlEtapas.DataValueField = "ID_PARAMETRO";
        ddlEtapas.DataBind();

        ddlEtapas.Items.Insert(0, new ListItem("--- SELECCIONAR ---", ""));
    }
    protected void Condicion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCondicion.DataSource = obj.ListarParametros_orden("IDE_CONDICION", "MOD_REQUERIMIENTO_PERSONAL");
        ddlCondicion.DataTextField = "DES_ASUNTO";
        ddlCondicion.DataValueField = "ID_PARAMETRO";
        ddlCondicion.DataBind();

        ddlCondicion.Items.Insert(0, new ListItem("--- SELECCIONAR ---", ""));
    }
    protected void fotocheck()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlFotocheck.DataSource = obj.ListarParametros_orden("IDE_FOTOCHECK", "MOD_REQUERIMIENTO_PERSONAL");
        ddlFotocheck.DataTextField = "DES_ASUNTO";
        ddlFotocheck.DataValueField = "ID_PARAMETRO";
        ddlFotocheck.DataBind();

        ddlFotocheck.Items.Insert(0, new ListItem("--- SELECCIONAR ---", ""));
    }
    protected void Mano()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlMano.DataSource = obj.ListarParametros_orden("IDE_MANO_OBRA", "MOD_REQUERIMIENTO_PERSONAL");
        ddlMano.DataTextField = "DES_ASUNTO";
        ddlMano.DataValueField = "ID_PARAMETRO";
        ddlMano.DataBind();

        ddlMano.Items.Insert(0, new ListItem("--- SELECCIONAR ---", ""));
    }
    protected void Nacionalidad()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlNacionalidad.DataSource = obj.ListarParametros_orden("IDE_TIPO_NACIONALIDAD", "MOD");
        ddlNacionalidad.DataTextField = "DES_ASUNTO";
        ddlNacionalidad.DataValueField = "ID_PARAMETRO";
        ddlNacionalidad.DataBind();

        //ddlNacionalidad.Items.Insert(0, new ListItem("--- SELECCIONAR ---", ""));
    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEliminar = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEliminar.CommandArgument);
        GridViewRow row = btnEliminar.NamingContainer as GridViewRow;
        string REQ_PERSONAL = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
        //string IDE_MOD = GridView1.DataKeys[row.RowIndex].Values[1].ToString();

        //BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        //DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspDEL_MOD_REQUERIMIENTO_IDE(Convert.ToInt32(IDE_REQUERIMIENTO), Convert.ToInt32(IDE_MOD));
        //Listar();
    }
    protected void Ver(object sender, ImageClickEventArgs e)
    {

        ImageButton btnVer = ((ImageButton)sender);
        int item = Convert.ToInt32(btnVer.CommandArgument);
        GridViewRow row = btnVer.NamingContainer as GridViewRow;
        string REQ_PERSONAL = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

        DataTable dtResultado = new DataTable();
        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        dtResultado = obj.uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_IDE(Convert.ToInt32(REQ_PERSONAL));
        if (dtResultado.Rows.Count > 0)
        {
            txtDni.Focus();
            lblCodigo.Text = dtResultado.Rows[0]["REQ_PERSONAL"].ToString();
            txtDni.Text = dtResultado.Rows[0]["DNI"].ToString();
            txtPaterno.Text = dtResultado.Rows[0]["APE_PATERNO"].ToString();
            txtMaterno.Text = dtResultado.Rows[0]["APE_MATERNO"].ToString();

            txtNombres.Text = dtResultado.Rows[0]["NOMBRES"].ToString();
            txtFechaNac.Text = dtResultado.Rows[0]["FEC_NACIMIENTO"].ToString();
            txtTelefonos.Text = dtResultado.Rows[0]["TELEFONOS"].ToString();
            txtMedico.Text = dtResultado.Rows[0]["FEC_EXA_MEDICO"].ToString();

            txtTr.Text = dtResultado.Rows[0]["FEC_CHARLA_TR"].ToString();
            txtSSK.Text = dtResultado.Rows[0]["FEC_CHARLA_SSK"].ToString();
            txtAltura.Text = dtResultado.Rows[0]["FEC_CHARLA_ALTURA"].ToString();
            txtEspacio.Text = dtResultado.Rows[0]["FEC_CHARLA_ESP_CONFINADO"].ToString();
            txtCaliente.Text = dtResultado.Rows[0]["FEC_CHARL_CALIENTE"].ToString();
            txtFile.Text = dtResultado.Rows[0]["FEC_ENTREGA_FILE_TR"].ToString();
            txtPlanta.Text = dtResultado.Rows[0]["FEC_ACCESO_PLANTA"].ToString();
            txtObservaciones.Text = dtResultado.Rows[0]["OBSERVACIONES"].ToString();

            string IDE_ETAPAS = dtResultado.Rows[0]["IDE_ETAPAS"].ToString();
            if (IDE_ETAPAS ==string.Empty)
            {

            }
            else
            {
                ddlEtapas.SelectedValue = IDE_ETAPAS;
            }

            string IDE_TIPO_NACIONALIDAD = dtResultado.Rows[0]["IDE_TIPO_NACIONALIDAD"].ToString();
            if (IDE_TIPO_NACIONALIDAD == string.Empty)
            {
                ddlNacionalidad.SelectedIndex = 0;
            }
            else
            {
                ddlNacionalidad.SelectedValue = IDE_TIPO_NACIONALIDAD;
            }

            Cuidad();
            string DES_COD_DPTO = dtResultado.Rows[0]["DES_COD_DPTO"].ToString();
            if (DES_COD_DPTO == string.Empty)
            {
               
            }
            else
            {
                ddlCiudad.SelectedValue = DES_COD_DPTO;
            }

            Provincia();
            string DES_CODPROV = dtResultado.Rows[0]["DES_CODPROV"].ToString();
            if (DES_CODPROV == string.Empty)
            {

            }
            else
            {
                ddlProvincia.SelectedValue = DES_CODPROV;
            }
            Distrito();
            string UBIGEO = dtResultado.Rows[0]["UBIGEO"].ToString();
            if (UBIGEO == string.Empty)
            {

            }
            else
            {
                ddlDistrito.SelectedValue = UBIGEO;
            }

            string IDE_MANO_OBRA = dtResultado.Rows[0]["IDE_MANO_OBRA"].ToString();
            if (IDE_MANO_OBRA == string.Empty)
            {

            }
            else
            {
                ddlMano.SelectedValue = IDE_MANO_OBRA;
            }

            string IDE_CONDICION = dtResultado.Rows[0]["IDE_CONDICION"].ToString();
            if (IDE_CONDICION == string.Empty)
            {

            }
            else
            {
                ddlCondicion.SelectedValue = IDE_CONDICION;
            }

            string IDE_FOTOCHECK = dtResultado.Rows[0]["IDE_FOTOCHECK"].ToString();
            if (IDE_FOTOCHECK == string.Empty)
            {

            }
            else
            {
                ddlFotocheck.SelectedValue = IDE_FOTOCHECK;
            }
        }

        //string IDE_MOD = GridView1.DataKeys[row.RowIndex].Values[1].ToString();

    }
    protected void Listar()
    {
        IDE_REQUERIMIENTO = Session["IDE_REQUERIMIENTO"].ToString();
        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_REQ(Convert.ToInt32(IDE_REQUERIMIENTO));
        if (dtResultado.Rows.Count > 0)
        {
            //GridView1.Visible = true;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            //GridView1.Visible = false;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //int value = Convert.ToInt32(DataBinder.Eval(GridView1.DataItem, "Your Columnname in the datasource")),


        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "DATOS PERSONAL";
            HeaderCell.ColumnSpan = 12;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 0; i <= 11; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }


            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#239B56");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);


            for (int i = 12; i <= 16; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#239B56");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }




            HeaderCell = new TableCell();
            HeaderCell.Text = "CHARLAS";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC300");
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 17; i <= 19; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC300");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.Black;
            }


            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#5DADE2");
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 20; i <= 22; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#5DADE2");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.Black;
            }


            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#707B7C");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 23; i <= 23; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#707B7C");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }



            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }


    }

    protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Provincia();
        Distrito();
    }

    protected void Cuidad()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarOrigen(ddlNacionalidad.SelectedValue.ToString(), 1);
        ddlCiudad.DataSource = dtResultado;
        ddlCiudad.DataTextField = dtResultado.Columns["DESCRIPCION"].ToString();
        ddlCiudad.DataValueField = dtResultado.Columns["CODIGO"].ToString();
        ddlCiudad.DataBind();

        ddlCiudad.Items.Insert(0, new ListItem("---- SELECCIONAR  ----", ""));
        //Provincia();
    }
    protected void Provincia()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarOrigen(ddlCiudad.SelectedValue, 2);
        ddlProvincia.DataSource = dtResultado;
        ddlProvincia.DataTextField = dtResultado.Columns["DESCRIPCION"].ToString();
        ddlProvincia.DataValueField = dtResultado.Columns["CODIGO"].ToString();
        ddlProvincia.DataBind();
        ddlProvincia.Items.Insert(0, new ListItem("---- SELECCIONAR  ----", ""));

    }

    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        Distrito();
    }
    protected void Distrito()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.ListarOrigen(ddlProvincia.SelectedValue, 3);
        ddlDistrito.DataSource = dtResultado;
        ddlDistrito.DataTextField = dtResultado.Columns["DESCRIPCION"].ToString();
        ddlDistrito.DataValueField = dtResultado.Columns["CODIGO"].ToString();
        ddlDistrito.DataBind();

        ddlDistrito.Items.Insert(0, new ListItem("---- SELECCIONAR  ----", ""));
    }

    protected void ddlNacionalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cuidad();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        CleanControl(this.Controls);
    }
    public void CleanControl(ControlCollection controles)
    {
        
        lblCodigo.Text = string.Empty;
        foreach (Control control in controles)
        {
            if (control is TextBox)
                ((TextBox)control).Text = string.Empty;
            //else if (control is DropDownList)
            //    ((DropDownList)control).ClearSelection();
            else if (control is RadioButtonList)
                ((RadioButtonList)control).ClearSelection();
            else if (control is CheckBoxList)
                ((CheckBoxList)control).ClearSelection();
            else if (control is RadioButton)
                ((RadioButton)control).Checked = false;
            else if (control is CheckBox)
                ((CheckBox)control).Checked = false;
            else if (control.HasControls())
                //Esta linea detécta un Control que contenga otros Controles
                //Así ningún control se quedará sin ser limpiado.
                CleanControl(control.Controls);
        }
    }
    protected void CartaDatos(string IDE_CARTA)
    {
     
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (lblCodigo.Text.Trim() == string.Empty)
        {
            cleanMessage = "Falta seleccionar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

        }
        else
        {
            if (ddlEtapas.SelectedIndex == 0)
            {
                cleanMessage = "Seleccionar etapa";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            //else if (txtDni.Text.Trim() == string.Empty)
            //{
            //    cleanMessage = "Ingresar N° documento";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            else if (txtPaterno.Text.Trim() == string.Empty)
            {
                cleanMessage = "Ingresar apellido paterno";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtMaterno.Text.Trim() == string.Empty)
            {
                cleanMessage = "Ingresar apellido materno";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else if (txtNombres.Text.Trim() == string.Empty)
            {
                cleanMessage = "Ingresar nombres";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            //else if (ddlCiudad .SelectedIndex == 0)
            //{
            //    cleanMessage = "Seleccionar ciudad";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            //else if (ddlProvincia.SelectedIndex == 0)
            //{
            //    cleanMessage = "Seleccionar provincia";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            //else if (ddlDistrito.SelectedIndex == 0)
            //{
            //    cleanMessage = "Seleccionar distrito";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            //else if (ddlMano .SelectedIndex == 0)
            //{
            //    cleanMessage = "Seleccionar mano de obra";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            //else if (ddlCondicion.SelectedIndex == 0)
            //{
            //    cleanMessage = "Seleccionar condición";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            //else if (ddlFotocheck.SelectedIndex == 0)
            //{
            //    cleanMessage = "Seleccionar fotocheck";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            //}
            else
            {
                BE_MOD_REQUERIMIENTO_PERSONAL oBESol = new BE_MOD_REQUERIMIENTO_PERSONAL();
                oBESol.REQ_PERSONAL = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
                oBESol.IDE_RESPONSABLE = Session["IDE_USUARIO"].ToString();
                oBESol.IDE_ETAPAS = Convert.ToInt32(ddlEtapas.SelectedValue);
                oBESol.DNI = txtDni.Text.Trim();
                oBESol.APE_PATERNO = txtPaterno.Text.Trim();
                oBESol.APE_MATERNO = txtMaterno.Text.Trim();
                oBESol.NOMBRES = txtNombres.Text.Trim();
                oBESol.FEC_NACIMIENTO = txtFechaNac.Text.Trim();

                string UBIGEO;
                if (ddlDistrito.SelectedIndex == 0)
                {
                    UBIGEO = string.Empty;
                }
                else
                {
                    UBIGEO = ddlDistrito.SelectedValue.ToString();
                }
                oBESol.UBIGEO = UBIGEO;


                oBESol.TELEFONOS = txtTelefonos.Text.Trim();
                string MANO;
                if (ddlMano.SelectedIndex == 0)
                {
                    MANO = "0";
                }
                else
                {
                    MANO = ddlMano.SelectedValue.ToString();
                }
                oBESol.IDE_MANO_OBRA = Convert.ToInt32(MANO);

                string CONDICION;
                if (ddlCondicion.SelectedIndex == 0)
                {
                    CONDICION = "0";
                }
                else
                {
                    CONDICION = ddlCondicion.SelectedValue.ToString();
                }
                oBESol.IDE_CONDICION = Convert.ToInt32(CONDICION);

                oBESol.FEC_EXA_MEDICO = txtMedico.Text.Trim();
                oBESol.FEC_CHARLA_TR = txtTr.Text.Trim();
                oBESol.FEC_CHARLA_SSK = txtSSK.Text.Trim();
                oBESol.FEC_CHARLA_ALTURA = txtAltura.Text.Trim();
                oBESol.FEC_CHARLA_ESP_CONFINADO = txtEspacio.Text.Trim();
                oBESol.FEC_CHARL_CALIENTE = txtCaliente.Text.Trim();
                oBESol.FEC_ENTREGA_FILE_TR = txtFile.Text.Trim();
                oBESol.FEC_ACCESO_PLANTA = txtPlanta.Text.Trim();

                string FOTOCHECK;
                if (ddlFotocheck.SelectedIndex == 0)
                {
                    FOTOCHECK = "0";
                }
                else
                {
                    FOTOCHECK = ddlFotocheck.SelectedValue.ToString();
                }
                oBESol.IDE_FOTOCHECK = Convert.ToInt32(FOTOCHECK);
                oBESol.OBSERVACIONES = txtObservaciones.Text.Trim();

                int dtrpta = 0;
                dtrpta = new BL_MOD_REQUERIMIENTO().uspUPD_MOD_REQUERIMIENTO_PERSONAL(oBESol);
                if (dtrpta > 0)
                {


                    cleanMessage = "Registro exitoso.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    CleanControl(this.Controls);
                    Listar();
                    btnMasivo.Focus();

                }
            }
        }
    }

    protected void btnDescarga_Click(object sender, EventArgs e)
    {

        IDE_REQUERIMIENTO = Session["IDE_REQUERIMIENTO"].ToString();
        BL_MOD_REQUERIMIENTO obj = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultadoeExcel = new DataTable();
        dtResultadoeExcel = obj.uspSEL_MOD_REQUERIMIENTO_PERSONAL_POR_REQ(Convert.ToInt32(IDE_REQUERIMIENTO));
        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();



            GridViewExportUtil.Export("ESTATUS_MOD_" + DateTime.Now + ".xls", gvExcel);
            return;

        }
        else
        {


        }
    }

    protected void btnRegresa_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OPERACIONES/MOD_BANDEJA.aspx");
    }

    protected void btnMasivo_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        BL_MOD_REQUERIMIENTO OBJ = new BL_MOD_REQUERIMIENTO();
        DataTable dtResultado = new DataTable();


        int intContador = 0;

        if (GridView1.Rows.Count == 0)
        {
            
            cleanMessage = "No existe registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        foreach (GridViewRow Fila in GridView1.Rows)
        {
            CheckBox ChkBoxCell = ((CheckBox)Fila.FindControl("chkSelect"));
            if (ChkBoxCell.Checked == true)
            {
                intContador += 1;
            }
        }

        if (intContador == 0)
        {
            cleanMessage = "Debe seleccionar al menos un registro.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
           
        }

        string  REQ_PERSONAL;
        string IDE_REQUERIMIENTO;
        int registroUpdate = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            //TextBox txt;
            //txt = (TextBox)GridView1.HeaderRow.FindControl("TextBox1");
            CheckBox ChkBoxCell = ((CheckBox)row.FindControl("chkSelect"));
            if (ChkBoxCell.Checked)
            {
                REQ_PERSONAL = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                IDE_REQUERIMIENTO = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key


                DropDownList ddlEtapas_F = (DropDownList)GridView1.HeaderRow.FindControl("ddlEtapas_F");

                if (ddlEtapas_F.SelectedIndex > 0)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), ddlEtapas_F.SelectedItem.ToString(), 1);
                    registroUpdate++;
                }

                TextBox txtExaMedico_F = (TextBox)GridView1.HeaderRow.FindControl("txtExaMedico_F");
                Boolean ExaMedico_F = EsFecha(txtExaMedico_F.Text);
                if (ExaMedico_F==true )
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtExaMedico_F.Text, 2);
                    registroUpdate++;
                }

                TextBox txtTr_F = (TextBox)GridView1.HeaderRow.FindControl("txtTr_F");
                Boolean Tr_F = EsFecha(txtTr_F.Text);
                if (Tr_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtTr_F.Text, 3);
                    registroUpdate++;
                }

                TextBox txtSSK_F = (TextBox)GridView1.HeaderRow.FindControl("txtSSK_F");
                Boolean SSK_F = EsFecha(txtSSK_F.Text);
                if (SSK_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtSSK_F.Text, 4);
                    registroUpdate++;
                }

                TextBox txtALTURA_F = (TextBox)GridView1.HeaderRow.FindControl("txtALTURA_F");
                Boolean ALTURA_F = EsFecha(txtALTURA_F.Text);
                if (ALTURA_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtALTURA_F.Text, 5);
                    registroUpdate++;
                }

                TextBox txtESPACIO_F = (TextBox)GridView1.HeaderRow.FindControl("txtESPACIO_F");
                Boolean ESPACIO_F = EsFecha(txtESPACIO_F.Text);
                if (ESPACIO_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtESPACIO_F.Text, 6);
                    registroUpdate++;
                }

                TextBox txtCaliente_F = (TextBox)GridView1.HeaderRow.FindControl("txtCaliente_F");
                Boolean Caliente_F = EsFecha(txtCaliente_F.Text);
                if (Caliente_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtCaliente_F.Text, 7);
                    registroUpdate++;
                }

                TextBox txtFileTr_F = (TextBox)GridView1.HeaderRow.FindControl("txtFileTr_F");
                Boolean FileTr_F = EsFecha(txtFileTr_F.Text);
                if (FileTr_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtFileTr_F.Text, 8);
                    registroUpdate++;
                }

                TextBox txtPlanta_F = (TextBox)GridView1.HeaderRow.FindControl("txtPlanta_F");
                Boolean Planta_F = EsFecha(txtPlanta_F.Text);
                if (Planta_F == true)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), txtPlanta_F.Text, 9);
                    registroUpdate++;
                }

                DropDownList ddlFOTOCHECK_F = (DropDownList)GridView1.HeaderRow.FindControl("ddlFOTOCHECK_F");

                if (ddlFOTOCHECK_F.SelectedIndex > 0)
                {
                    OBJ.uspUPD_MOD_REQUERIMIENTO_PERSONAL_MASIVO(Convert.ToInt32(REQ_PERSONAL), ddlFOTOCHECK_F.SelectedItem.ToString(), 10);
                    registroUpdate++;
                }

            }
            ChkBoxCell = null;
        }

        if(registroUpdate > 0)
        {
            cleanMessage = "Se actualización satisfactoria";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Listar();
        }
        
        
    }
    public static Boolean EsFecha(String fecha)
    {
        try
        {
            DateTime.Parse(fecha);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
