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

public partial class CAREMENOR_EquipoMayoresAtencion : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    string FolderAlquilerBackups = ConfigurationManager.AppSettings["FolderAlquilerBackups"];
    string Requ_Numero;
    string Reqd_CodLinea;
    string Reqs_Correlativo;
    string FLG_MENOR;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnFile);
        if (!Page.IsPostBack)
        {
            txtFechaLegajo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            FileUploadLegajo.Attributes["onchange"] = "UploadFile(this)";

            FileUploadAdjudicacion.Attributes["onchange"] = "UploadFile(this)";
            FileUploadNotaAlquiler.Attributes["onchange"] = "UploadFile(this)";
            FileUploadContrato.Attributes["onchange"] = "UploadFile(this)";
            FileUploadOtros.Attributes["onchange"] = "UploadFile(this)";

            Documento();
            Proveedor();
            Movilizacion();
            Estados();
            Atencion();
            requerimientos();
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            FLG_MENOR = Request.QueryString["FLG_MENOR"].ToString();
            datos();
            file();
            GrupoFile();
        }
    }
    protected void Upnl_Load(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersonal"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Upnl_LoadRequerimiento(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlRequerimiento"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Proveedor()
    {
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.SP_CONSULTAR_TBL_Proveedor();
        if (dtResultado.Rows.Count > 0)
        {
            ddlProveedor.DataSource = dtResultado;
            ddlProveedor.DataTextField = "Prov_RazonSocial";
            ddlProveedor.DataValueField = "Prov_RUC";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlProveedor.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));



        }
    }
    protected void requerimientos()
    {

        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        FLG_MENOR = Request.QueryString["FLG_MENOR"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspLISTAR_REQUERIMIENTOS_LIBRE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, FLG_MENOR);
        if (dtResultado.Rows.Count > 0)
        {
            ddlRequerimiento .DataSource = dtResultado;
            ddlRequerimiento.DataTextField = "Reqs_CodigoCompleto";
            ddlRequerimiento.DataValueField = "Reqs_CodigoCompleto";
            ddlRequerimiento.DataBind();
            ddlRequerimiento.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlRequerimiento.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));



        }
    }
    protected void Estados()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableEstados();
        if (dtResultado.Rows.Count > 0)
        {
            //ddlEstado.DataSource = dtResultado;
            //ddlEstado.DataTextField = "descripcion";
            //ddlEstado.DataValueField = "codigo";
            //ddlEstado.DataBind();

        }
    }
    static DataTable GetTableEstados()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "EN PROCESO");
        table.Rows.Add(2, "ATENDIDO");
        table.Rows.Add(3, "ANULADO");
        table.Rows.Add(4, "STOCK SSK");

        return table;
    }
    protected void Documento()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableDocumento();
        if (dtResultado.Rows.Count > 0)
        {
            ddldocumento.DataSource = dtResultado;
            ddldocumento.DataTextField = "descripcion";
            ddldocumento.DataValueField = "codigo";
            ddldocumento.DataBind();

        }
    }
    static DataTable GetTableDocumento()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "LEGAJO");
        table.Rows.Add(2, "ADJUDICACIÓN");


        return table;
    }
    protected void Movilizacion()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableMovilizacion();
        if (dtResultado.Rows.Count > 0)
        {
            ddlMovilizacion.DataSource = dtResultado;
            ddlMovilizacion.DataTextField = "descripcion";
            ddlMovilizacion.DataValueField = "codigo";
            ddlMovilizacion.DataBind();

        }
    }
    static DataTable GetTableMovilizacion()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "SI");
        table.Rows.Add(0, "NO");


        return table;
    }
    protected void Atencion()
    {

        DataTable dtResultado = new DataTable();
        dtResultado = GetTableAtencion();
        if (dtResultado.Rows.Count > 0)
        {
            ddlAtencion.DataSource = dtResultado;
            ddlAtencion.DataTextField = "descripcion";
            ddlAtencion.DataValueField = "codigo";
            ddlAtencion.DataBind();

        }
    }
    static DataTable GetTableAtencion()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("codigo", typeof(int));
        table.Columns.Add("descripcion", typeof(string));

        table.Rows.Add(1, "ESTÁNDAR");
        table.Rows.Add(2, "REGULARIZACIÓN");


        return table;
    }
    protected void btnCargar_Click(object sender, EventArgs e)
    {
        SOLPED();

       
    }
    protected void SOLPED()
    {
        string cleanMessage = string.Empty;

        if (txtFechaLegajo.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de legajo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlProveedor.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar nombre de proveedor";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {


           
            ////////////////////////////////
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();


            BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
            oBESol.Requ_Numero = Requ_Numero;
            oBESol.Reqd_CodLinea = Reqd_CodLinea;
            oBESol.Reqs_Correlativo = Reqs_Correlativo;
            oBESol.D_DOCUMENTO_TIPO = Convert.ToInt32(ddldocumento.SelectedValue.ToString());
            oBESol.D_DOCUMENTO_RUTA = FolderAlquiler;
            oBESol.D_DOCUMENTO_FILE = "";
            oBESol.D_DOC_MOVILIZACION = Convert.ToInt32(ddlMovilizacion.SelectedValue.ToString());
            oBESol.D_DOCUMENTO_FECHA = txtFechaLegajo.Text.Trim();
            oBESol.D_Prov_RUC = ddlProveedor.SelectedValue.ToString();
            oBESol.D_ATENCION_TIPO = Convert.ToInt32(ddlAtencion.SelectedValue.ToString());
            oBESol.TIPO_OPERACION = 2;
            oBESol.D_DOCUMENTO_FILENAME = "";
            oBESol.TIPO_FILE  = "";
            oBESol.D_ATENCION_COMENTARIOS = txtComentarios.Text.Trim();
            //string OPERARIO = "0";
            //if(CheckOperario.Checked)
            //{
            //    OPERARIO = "1";
            //}
            //else
            //{
            //    OPERARIO = "0";
            //}
            oBESol.D_FLG_OPERARIO = ddlOperario.SelectedValue.ToString();
            oBESol.COD_GUID = string.Empty;
            oBESol.D_FECHA_SALE_OBRA = txtFinSalida.Text.Trim();
            DataTable  dtrpta = new DataTable();
            dtrpta = new BL_TBL_RequerimientoSubDetalle().uspUPD_TBL_RequerimientoSubDetalle_Alquiler(oBESol);
           
            if (dtrpta.Rows.Count > 0)
            {



                if (GridView2.Rows.Count > 0)
                {
                    GuardarGrupo();
                }

                cleanMessage = "Registro existo!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                datos();
            }

        }
    }


    protected void GuardarGrupo()
    {

        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


      

        foreach (GridViewRow row in GridView2.Rows)
        {
            string  Reqs_CodigoCompleto = GridView2.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
            dtResultado = obj.uspREGISTRAR_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1, Reqs_CodigoCompleto, txtFinSalida.Text);
        }
    }

    public void CleanControl(ControlCollection controles)
    {
     
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
    protected void datos()
    {
        CleanControl(this.Controls);
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle Obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = Obj.USP_SEL_TBL_REQUERIMIENTO_EQUIPO_MAYOR_ID(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo);
        if (dtResultado.Rows.Count > 0)
        {
            try
            {


                txtFechaLegajo.Text = dtResultado.Rows[0]["D_DOCUMENTO_FECHA"].ToString();
                ddldocumento.SelectedValue = dtResultado.Rows[0]["D_DOCUMENTO_TIPO"].ToString();
                ddlMovilizacion.SelectedValue = dtResultado.Rows[0]["D_DOC_MOVILIZACION"].ToString();
              

               
                ddlAtencion.SelectedValue = dtResultado.Rows[0]["D_ATENCION_TIPO"].ToString();
                txtComentarios.Text = dtResultado.Rows[0]["D_ATENCION_COMENTARIOS"].ToString();
                txtFinSalida.Text = dtResultado.Rows[0]["D_FECHA_SALE_OBRA"].ToString();
                //CheckOperario.Checked = Convert.ToBoolean( dtResultado.Rows[0]["FLG_OPERARIO"].ToString());

                ddlOperario.SelectedValue = dtResultado.Rows[0]["FLG_OPERARIO"].ToString();


                ddlProveedor.SelectedValue = dtResultado.Rows[0]["D_Prov_RUC"].ToString();
                ddlProveedor.Text = dtResultado.Rows[0]["Proveedor"].ToString();

                if (dtResultado.Rows[0]["PROCESO_BOTON"].ToString() == "1")
                {
                    btnCargar.Visible = true;
                    btnFile.Visible = true;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                        btnEliminar.Visible = true;
                    }
                }
                else
                {
                    btnCargar.Visible = false;
                    btnFile.Visible = false;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                        btnEliminar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
            
            }

        }
    }

    protected void btnFile_Click(object sender, EventArgs e)
    {
        Guid g;
        // Create and display the value of two GUIDs.
        g = Guid.NewGuid();

        if (FileUploadLegajo.HasFile)
        {
            cargar(FileUploadLegajo, "LEGAJO", g.ToString());
        }

        if (FileUploadAdjudicacion.HasFile)
        {
            cargar(FileUploadAdjudicacion, "ADJUDICACIÓN", g.ToString());
        }

        if (FileUploadNotaAlquiler.HasFile)
        {
            cargar(FileUploadNotaAlquiler, "NOTA ALQUILER", g.ToString());
        }

        if (FileUploadContrato.HasFile)
        {
            cargar(FileUploadContrato, "CONTRATOS", g.ToString());
        }

        if (FileUploadOtros.HasFile)
        {
            cargar(FileUploadOtros, "OTROS", g.ToString());
        }

        
       

    }

    protected void cargar(FileUpload FileUpload1, string TipoArchivo, string g)
    {
        string cleanMessage = string.Empty;

        if (txtFechaLegajo.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar fecha de legajo";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }

        else
        {


            // Si el directorio no existe, crearlo
            //if (!Directory.Exists(Server.MapPath(FolderAlquiler)))
            //    Directory.CreateDirectory(FolderAlquiler);

            String fileExtension = string.Empty;
            Boolean fileOK = false;
            string fileArchivo = string.Empty;
            string Name = string.Empty;
            if (FileUpload1.HasFile)
            {

                string fileName = FileUpload1.FileName.ToString();
                int length = FileUpload1.PostedFile.ContentLength;

                fileExtension = Path.GetExtension(FileUpload1.FileName).ToUpper();

                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension.ToUpper() == allowedExtensions[i].ToUpper())
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {

                    // Se carga la ruta física de la carpeta temp del sitio
                    string ruta = Server.MapPath(FolderAlquiler);
                    string rutaBackups = FolderAlquilerBackups;
                    // Si el directorio no existe, crearlo
                    //if (!Directory.Exists(ruta))
                    //    Directory.CreateDirectory(ruta);

                    string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.PostedFile.FileName);
                    Name = EliminarCaracteres.ReemplazarCaracteresEspeciales(FileUpload1.PostedFile.FileName);
                    // Verificar que el archivo no exista
                    if (File.Exists(archivo))
                    {
                        fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales(BL_Session.CENTRO_COSTO + "_" + TipoArchivo + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName));
                        FileUpload1.SaveAs(ruta + fileArchivo);
                        FileUpload1.SaveAs(rutaBackups + fileArchivo);
                    }

                    else
                    {
                        fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales( BL_Session.CENTRO_COSTO + "_" + TipoArchivo + "_" + Path.GetFileName(FileUpload1.FileName));
                        //FileUpload1.SaveAs(archivo);
                        FileUpload1.SaveAs(ruta + fileArchivo);
                        FileUpload1.SaveAs(rutaBackups + fileArchivo);
                    }

                }
                catch (Exception ex)
                {
                    cleanMessage = "Archivo no puedo ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            ////////////////////////////////
            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();


            BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
            oBESol.Requ_Numero = Requ_Numero;
            oBESol.Reqd_CodLinea = Reqd_CodLinea;
            oBESol.Reqs_Correlativo = Reqs_Correlativo;
            oBESol.D_DOCUMENTO_TIPO = 0;
            oBESol.D_DOCUMENTO_RUTA = FolderAlquiler;
            oBESol.D_DOCUMENTO_FILE = fileArchivo;
            oBESol.D_DOC_MOVILIZACION = 0;
            oBESol.D_DOCUMENTO_FECHA = txtFechaLegajo.Text.Trim();
            oBESol.D_Prov_RUC = "";
            oBESol.D_ATENCION_TIPO = 0;
            oBESol.TIPO_OPERACION = 1;
            oBESol.D_DOCUMENTO_FILENAME = Name;
            oBESol.TIPO_FILE = TipoArchivo;
            oBESol.D_FLG_OPERARIO = "";
            oBESol.COD_GUID = g.ToString();
            oBESol.D_FECHA_SALE_OBRA = "";
            DataTable dtrpta = new DataTable();
            dtrpta = new BL_TBL_RequerimientoSubDetalle().uspUPD_TBL_RequerimientoSubDetalle_Alquiler(oBESol);

            if (dtrpta.Rows.Count > 0)
            {
                
                file();
                //cleanMessage = "Archivo cargado";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }
        }
    }
    protected void file()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.uspSEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
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
        //revisar
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();

        String path = Server.MapPath(FolderAlquiler);


        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_FILE = GridView1.DataKeys[grdrow.RowIndex].Values["ide_LegajoFile"].ToString();
        string Archivo = GridView1.DataKeys[grdrow.RowIndex].Values["FILE_ARCHIVO"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        try
        {
            if (File.Exists(path + Archivo))
            {
                File.Delete(path + Archivo);

            }
        }
        catch (Exception ex)
        {

        }

        dtResultado = obj.uspDEL_TBL_REQUERIMIENTOSUBDETALLE_LEGAJOFILE(Convert.ToInt32(IDE_FILE));
        if ( 0 == Convert.ToInt32( dtResultado.Rows[0]["ID"].ToString()))
        {
           string cleanMessage = "No se puede eliminar archivo, grupo de documentos asociados a más requerimientos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            file();
        }


       
    }

    protected void btnAsociar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlRequerimiento.SelectedValue == string.Empty)
        {
            cleanMessage = "Ingresar Nro de requerimiento";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtFinSalida.Text.Trim()== string.Empty)
        {
            cleanMessage = "Ingresar fecha fin de contrato";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (GridView1.Rows.Count==0)
        {
            cleanMessage = "No existen documentos registrados";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
            Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
            Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();


            dtResultado = obj.uspREGISTRAR_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1, ddlRequerimiento.SelectedValue.ToString(), txtFinSalida.Text.Trim());
            GrupoFile();
        }

    }
    protected void GrupoFile()
    {
        Requ_Numero = Request.QueryString["Requ_Numero"].ToString();
        Reqd_CodLinea = Request.QueryString["Reqd_CodLinea"].ToString();
        Reqs_Correlativo = Request.QueryString["Reqs_Correlativo"].ToString();
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();


        dtResultado = obj.GRUPO_LEGAJOFILE(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, 1);
        if (dtResultado.Rows.Count > 0)
        {
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();
        }
    }

    protected void Retirar(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderAlquiler);


        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string Reqs_CodigoCompleto = GridView2.DataKeys[grdrow.RowIndex].Values["Reqs_CodigoCompleto"].ToString();

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspRETIRAR_GRUPOLEGAJO(Reqs_CodigoCompleto);
        file();
        GrupoFile();


    }
}