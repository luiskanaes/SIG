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

public partial class RRHH_DesempecioObjetivoEmpresa : System.Web.UI.Page
{
    string FolderDesemepenio = ConfigurationManager.AppSettings["FolderDesemepenio"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(ListView1);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(Chart1);

        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Anios();
            Personal();
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
                string strSelectedValue = ddlPersonal.SelectedValue;
                //Your code here ... 
            }
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
        anio = DateTime.Today.Year;
        anioActual = DateTime.Today.Year;
        for (int i = 0; i < 6; i++)
        {
            anio = anioActual - i;
            table.Rows.Add(anio, anio);


        }

        return table;
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarPersonal_mando(Session["IDE_USUARIO"].ToString ());
        dtResultado = obj.uspSEL_RRHH_PERSONAL_TIPO_TODO("01");
        if (dtResultado.Rows.Count > 0)
        {
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));


            string cleanMessage = "No existe personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }
    protected void cboObjetivos(string dni)
    {


        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA(dni, Convert.ToInt32(Session["ANIO"]), "");
        if (dtResultado.Rows.Count > 0)
        {
            ddlObejtivos.DataSource = dtResultado;
            ddlObejtivos.DataTextField = "OBJETIVO";
            ddlObejtivos.DataValueField = "IDE_OBJETIVO";
            ddlObejtivos.DataBind();

            ddlObejtivos.Items.Insert(0, new ListItem("--- Todos ---", ""));
        }
        else
        {

            ddlObejtivos.Items.Insert(0, new ListItem("--- N.R. ---", ""));
        }

    }

    protected void ListarGrafico()
    {
        string IDE_OBJETIVO = string.Empty;
        if (ddlObejtivos.SelectedIndex == 0)
        {
            IDE_OBJETIVO = string.Empty;
        }
        else
        {
            IDE_OBJETIVO = ddlObejtivos.SelectedValue.ToString();
        }


        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado.Clear();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_GRAFICO(lblPersonal.Text, Convert.ToInt32(Session["ANIO"]), IDE_OBJETIVO);


        DataTable dt = dtResultado;

        /*
        string[] x = new string[dt.Rows.Count];
        int[] y = new int[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i]["OBJ"].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i]["AVANCE"]);
        }
        Chart1.Series["Series1"].Points.DataBindXY(x, y);
   
        Chart1.Series[0].ChartType = SeriesChartType.Column;
        Chart1.Series["Series1"].Label = "#PERCENT";
        Chart1.Series[0].LegendText = "#VALX";
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        Chart1.Legends["Series1"].Enabled = true;

        
        string[] x2 = new string[dt.Rows.Count];
        int[] y2 = new int[dt.Rows.Count];
        string[] l2 = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x2[i] = dt.Rows[i]["obj2"].ToString();
            y2[i] = Convert.ToInt32(dt.Rows[i]["TOTAL_TRANSCURRIDOS"]);
            l2[i] = dt.Rows[i]["LEYENDA_TRANSCURRIDOS"].ToString();

        }
        Chart2.Series[0].Points.DataBindXY(x2, y2);
        Chart2.Series[0].ToolTip = l2.ToString();
        Chart2.Series[0].IsValueShownAsLabel = true;
        Chart2.Series[0].ChartType = SeriesChartType.Column;
        */
        //Chart1.ResetAutoValues();

        Chart1.Series[0].Points.Clear();
        Chart1.Series["Series1"].Points.Clear();
        Chart1.ResetAutoValues();
        Chart1.DataSource = dtResultado;
        Chart1.DataBind();
        Chart1.Series[0].IsValueShownAsLabel = true;
        //Chart1.Series["Series1"].Label = "#PERCENT";


        Chart2.DataSource = dtResultado;
        Chart2.DataBind();
        Chart2.Series[0].IsValueShownAsLabel = true;
        Chart2.Series[1].IsValueShownAsLabel = true;
        Chart2.Series[0].LegendText = "Días transcurridos";
        Chart2.Series[1].LegendText = "Total de días";

    }
    protected void ListarObjetivos()
    {
        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();

        string IDE_OBJETIVO = string.Empty;
        if (ddlObejtivos.SelectedIndex == 0)
        {
            IDE_OBJETIVO = string.Empty;
        }
        else
        {
            IDE_OBJETIVO = ddlObejtivos.SelectedValue.ToString();
        }

        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_PERSONA(lblPersonal.Text, Convert.ToInt32(Session["ANIO"]), IDE_OBJETIVO);
        if (dtResultado.Rows.Count > 0)
        {
            ListView1.DataSource = dtResultado;
            ListView1.DataBind();
        }
        else
        {

            ListView1.DataSource = dtResultado;
            ListView1.DataBind();

        }
        //ListarGrafico();
    }
    protected void likeObjetivo(object sender, ImageClickEventArgs e)
    {

        ImageButton btnLike = ((ImageButton)sender);

        int item = Convert.ToInt32(btnLike.CommandArgument);
        ListViewItem CommentItem = btnLike.NamingContainer as ListViewItem;
        int IDE_OBJETIVO = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"];
        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_LIKE(Convert.ToInt32(IDE_OBJETIVO), lblPersonal.Text, Convert.ToInt32(Session["ANIO"]), 3);
        ListarObjetivos();

        TextBox txtPunto = (TextBox)CommentItem.FindControl("txtPunto");
    }
    protected void VerBotonEnviarMSg(object sender, ImageClickEventArgs e)
    {

        String cleanMessage = String.Empty;
        ImageButton btnLike = ((ImageButton)sender);

        int item = Convert.ToInt32(btnLike.CommandArgument);
        ListViewItem CommentItem = btnLike.NamingContainer as ListViewItem;
        int IDE_OBJETIVO = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"];


        ImageButton btn = (ImageButton)CommentItem.FindControl("btn");
        TextBox txtComentarios = (TextBox)CommentItem.FindControl("txtComentarios");
        btn.Visible = true;
        txtComentarios.Text = String.Empty;
        txtComentarios.Visible = true;
    }
    protected void enviarComentario(object sender, ImageClickEventArgs e)
    {

        String cleanMessage = String.Empty;
        ImageButton btn = ((ImageButton)sender);

        int item = Convert.ToInt32(btn.CommandArgument);
        ListViewItem CommentItem = btn.NamingContainer as ListViewItem;
        int IDE_OBJETIVO = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"];



        TextBox txtComentarios = (TextBox)CommentItem.FindControl("txtComentarios");
        FileUpload FileUpload1 = (FileUpload)CommentItem.FindControl("FileUpload1");






        // Si el directorio no existe, crearlo
        if (!Directory.Exists(Server.MapPath(FolderDesemepenio)))
            Directory.CreateDirectory(FolderDesemepenio);

        String fileExtension = string.Empty;
        Boolean fileOK = false;
        string fileArchivo = string.Empty;
        if (FileUpload1.HasFile)
        {

            string fileName = FileUpload1.FileName;
            int length = FileUpload1.PostedFile.ContentLength;

            fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
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
                string ruta = Server.MapPath(FolderDesemepenio);

                // Si el directorio no existe, crearlo
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string archivo = String.Format("{0}\\{1}", ruta, FileUpload1.FileName);

                // Verificar que el archivo no exista
                if (File.Exists(archivo))
                {
                    fileArchivo = DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(ruta + fileArchivo);
                }

                else
                {
                    fileArchivo = FileUpload1.PostedFile.FileName;
                    FileUpload1.SaveAs(archivo);
                }
            }
            catch (Exception ex)
            {
                cleanMessage = "Archivo no puedo ser cargado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }


        if (txtComentarios.Text.Trim() == String.Empty)
        {
            cleanMessage = "Ingresar comentarios";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {


            BE_RRHH_DESEMPENIO_OBJETIVOS_MSG oBESol = new BE_RRHH_DESEMPENIO_OBJETIVOS_MSG();
            oBESol.IDE_MSG = 0;// Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
            oBESol.DNI_USER = lblPersonal.Text;
            oBESol.IDE_OBJETIVO = IDE_OBJETIVO;
            oBESol.TIPO = "OBSERVACION";
            oBESol.ANIO = Convert.ToInt32(Session["ANIO"]);
            oBESol.ASUNTO = String.Empty;
            oBESol.COMENTARIO = txtComentarios.Text.Trim();
            oBESol.FILE = fileArchivo;
            oBESol.URL = FolderDesemepenio;

            int dtrpta = 0;
            dtrpta = new BL_RRHH_DESEMPENIO_OBJETIVOS_MSG().uspINS_RRHH_DESEMPENIO_OBJETIVOS_MSG(oBESol);
            if (dtrpta > 0)
            {
                txtComentarios.Text = String.Empty;
                cleanMessage = "Envio satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                ListarObjetivos();

            }
        }

    }
    protected void RealizarAmpliacion(object sender, ImageClickEventArgs e)
    {
        ImageButton btnFecha = ((ImageButton)sender);

        int item = Convert.ToInt32(btnFecha.CommandArgument);
        ListViewItem CommentItem = btnFecha.NamingContainer as ListViewItem;
        int IDE_OBJETIVO = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"];
        lblCodigo.Text = ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"].ToString();
        ModalObserva.Show();
    }





    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG

        DataRowView drv = e.Item.DataItem as DataRowView;
        DataList innerDataList = e.Item.FindControl("DataListChat") as DataList;
        Label lbl = (Label)e.Item.FindControl("lblIDE_OBJETIVO");
        DropDownList ddlAvance = (DropDownList)e.Item.FindControl("ddlAvance");


        BL_RRHH_DESEMPENIO_OBJETIVOS_MSG obj = new BL_RRHH_DESEMPENIO_OBJETIVOS_MSG();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RRHH_DESEMPENIO_OBJETIVOS_MSG(Convert.ToInt32(lbl.Text));


        foreach (DataRow rw in dtResultado.Rows)
        {
            innerDataList.DataSource = dtResultado;
            innerDataList.DataBind();
        }

        DataTable dt = new DataTable();
        dt = GetTableAvance();

        ddlAvance.DataSource = GetTableAvance(); ;
        ddlAvance.DataTextField = "AVANCE";
        ddlAvance.DataValueField = "AVANCE";
        ddlAvance.DataBind();

        ddlAvance.SelectedValue = drv["AVANCE"].ToString();

    }
    static DataTable GetTableAvance()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("PESO", typeof(int));
        table.Columns.Add("AVANCE", typeof(string));



        table.Rows.Add(0, 0);
        table.Rows.Add(10, 10);
        table.Rows.Add(20, 20);
        table.Rows.Add(30, 30);
        table.Rows.Add(40, 40);
        table.Rows.Add(50, 50);
        table.Rows.Add(60, 60);
        table.Rows.Add(70, 70);
        table.Rows.Add(80, 80);
        table.Rows.Add(90, 90);
        table.Rows.Add(100, 100);

        return table;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblCodigo.Text = string.Empty;
        txtAmpliarFecha.Text = string.Empty;
        txtComentar.Text = string.Empty;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BL_RRHH_DESEMPENIO_OBJETIVOS_MSG obj = new BL_RRHH_DESEMPENIO_OBJETIVOS_MSG();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspUPD_RRHH_DESEMPENIO_AMPLIACION(Convert.ToInt32(lblCodigo.Text), txtComentar.Text.Trim(), lblPersonal.Text, txtAmpliarFecha.Text);

        string cleanMessage = "Envio satisfactorio";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        ListarObjetivos();
        ListarGrafico();
    }

    protected void GuardarAvance(object sender, ImageClickEventArgs e)
    {
        //ImageButton btnFecha = ((ImageButton)sender);

        //int item = Convert.ToInt32(btnFecha.CommandArgument);
        //ListViewItem CommentItem = btnFecha.NamingContainer as ListViewItem;
        //int IDE_OBJETIVO = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"];
        //lblCodigo.Text = ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"].ToString();
        ModalObserva.Show();
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton LinkButton1 = ((LinkButton)sender);
        ListViewItem CommentItem = LinkButton1.NamingContainer as ListViewItem;
        int IDE_OBJETIVO = (int)ListView1.DataKeys[CommentItem.DisplayIndex].Values["IDE_OBJETIVO"];

        DropDownList ddlAvance = (DropDownList)CommentItem.FindControl("ddlAvance");


        BL_RRHH_DESEMPENIO_OBJETIVOS obj = new BL_RRHH_DESEMPENIO_OBJETIVOS();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspUPD_RRHH_DESEMPENIO_AVANCE(IDE_OBJETIVO, Convert.ToInt32(ddlAvance.SelectedValue));
        string cleanMessage = "actualización satisfactoria";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        ListarGrafico();
        //ModalObserva.Show();
    }

    protected void ddlObejtivos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarObjetivos();
        ListarGrafico();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (ddlPersonal.SelectedValue==string.Empty )
        {
            string cleanMessage = "Falta consultar personal";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            PanelPersonal.Visible = false;
        }
        else
        {
            PanelPersonal.Visible = true ;
            lblPersonal.Text = ddlPersonal.SelectedValue.ToString();
            cboObjetivos(lblPersonal.Text);
            ListarObjetivos();
            ListarGrafico();
        }
       
    }
}