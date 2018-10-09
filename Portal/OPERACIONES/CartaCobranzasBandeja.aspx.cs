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
using System.Drawing;
using System.Drawing.Imaging;
public partial class OPERACIONES_CartaCobranzasBandeja : System.Web.UI.Page
{
    string FolderCarta = ConfigurationManager.AppSettings["FolderCarta"];
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(this).RegisterPostBackControl(GridView1);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Anios();
            Estado();
            //ddlEstado.SelectedIndex = 1;
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();
            centros();
            Listar("","","");
            CONTROLES();

            
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void CONTROLES()
    {

        

    }
    protected void btnValidar_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Response.Redirect("~/OPERACIONES/CartaCobranzasBandejaAprobaciones.aspx");
    }
    protected void centros()
    {
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_CECOS(Convert.ToInt32(ddlanio.SelectedValue), Session["IDE_USUARIO"].ToString());

        if (dtResultado.Rows.Count == 1)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["C_CENTRO"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["C_IPE_CENTRO"].ToString();
            ddlCentro.DataBind();
            //ddlCentro.Items.Insert(0, new ListItem("--- Todos ---", ""));
        }
        else if (dtResultado.Rows.Count > 1)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["C_CENTRO"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["C_IPE_CENTRO"].ToString();
            ddlCentro.DataBind();
            ddlCentro.Items.Insert(0, new ListItem("--- Todos ---", ""));
        }
        else
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Response.Redirect("~/OPERACIONES/CartaCobranzas.aspx");
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
    protected void Estado()
    {

        BL_Seguridad obj = new BL_Seguridad();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.ListarParametros("REGISTRO", "IDE_ESTADO", string.Empty);
        dtResultado = GetTableEstado();
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstado.DataSource = dtResultado;
            ddlEstado.DataTextField = "DESCRIPCION";
            ddlEstado.DataValueField = "ID";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("--- Todos ---", ""));
        }
    }
    static DataTable GetTableEstado()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("DESCRIPCION", typeof(string));


        table.Rows.Add("1", "Pendiente");
        table.Rows.Add("2", "Aprobados");
        table.Rows.Add("4", "Registrado");
        table.Rows.Add("3", "Anulado");



        return table;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Listar("","","");
    }
    protected void Listar(string  txtTicket_H, string txtC_CENTRO_H, string txtD_CENTRO_H)
    {
        string estado = string.Empty;
        if (ddlEstado.SelectedIndex == 0)
        {
            estado = string.Empty;
        }
        else
        {
            estado = ddlEstado.SelectedValue.ToString();
        }


        string IPCENTRO = string.Empty;
        //if (ddlCentro.SelectedIndex == 0)
        //{
        //    IPCENTRO = string.Empty;

        //    if (txtC_CENTRO_H != string.Empty)
        //    {
        //        IPCENTRO = txtC_CENTRO_H;
        //    }
        //}
        //else
        //{
        //    IPCENTRO = ddlCentro.SelectedValue.ToString();
        //    if (txtC_CENTRO_H != string.Empty)
        //    {
        //        IPCENTRO = txtC_CENTRO_H;
        //    }
        //}


      
        int contarCC = Convert.ToInt32(ddlCentro.Items.Count.ToString());
        if (contarCC <= 1)
        {
            IPCENTRO = ddlCentro.SelectedValue.ToString();
        }
        else
        {
            if (ddlCentro.SelectedIndex == 0)
            {
                IPCENTRO = string.Empty;
            }
            else
            {
                if (txtC_CENTRO_H != string.Empty)
                {
                    IPCENTRO = txtC_CENTRO_H;
                }
                else
                {
                    IPCENTRO = ddlCentro.SelectedValue.ToString();
                    if (txtC_CENTRO_H != string.Empty)
                    {
                        IPCENTRO = txtC_CENTRO_H;
                    }
                }
            }
        }



        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS(Convert.ToInt32(ddlanio.SelectedValue), Session["IDE_USUARIO"].ToString(),IPCENTRO , estado , txtTicket_H, txtD_CENTRO_H);
        if (dtResultado.Rows.Count > 0)
        {


            //lstRol.DataSource = dtResultado;
            //lstRol.DataBind();

            GridView1.DataSource = dtResultado;
            GridView1.DataBind();




        }
        else
        {
            //lstRol.DataSource = dtResultado;
            //lstRol.DataBind();
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }

        BL_SOLPED objx = new BL_SOLPED();
        DataTable dtResultadox = new DataTable();
        dtResultadox = objx.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE CARTA DE COBRANZA (SAP)", BL_Session.ID_EMPRESA.ToString());
        if (dtResultadox.Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                ImageButton btnGuardar = (ImageButton)row.FindControl("btnGuardar");
                btnGuardar.Visible = true;
            }
        }
        else
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                ImageButton btnGuardar = (ImageButton)row.FindControl("btnGuardar");
                btnGuardar.Visible = false;
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("","","");
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //TableCell HeaderCell = new TableCell();
            //HeaderCell.Text = "Carta cobranzas";
            //HeaderCell.ColumnSpan = 8;
            //HeaderCell.BackColor = System.Drawing.Color.Green;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            for (int i = 0; i <= 7; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E8449");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }

            for (int i = 8; i <= 11; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }

            for (int i = 12; i <= 14; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }

            for (int i = 15; i <= 18; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF5733");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "De:";
            //HeaderCell.ColumnSpan = 2;
            //HeaderCell.BackColor = System.Drawing.Color.Black;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Para:";
            //HeaderCell.ColumnSpan = 2;
            //HeaderCell.BackColor = System.Drawing.Color.Gray;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Control de aprobaciones";
            //HeaderCell.ColumnSpan = 3;
            //HeaderCell.BackColor = System.Drawing.Color.Navy;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Control de atención";
            //HeaderCell.ColumnSpan = 4;
            //HeaderCell.BackColor = System.Drawing.Color.Red ;
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);
            //GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void seleccionar(object sender, EventArgs e)
    {

        ImageButton btnSelect = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        Session["IDE_CARTA"] = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();
        Response.Redirect("~/OPERACIONES/CartaCobranzas.aspx");
    }
    protected void NuevaCarta(object sender, EventArgs e)
    {

        ImageButton btnGenerar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_CARTA = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();
        string TICKET_ANTERIOR = GridView1.DataKeys[grdrow.RowIndex].Values["TICKET_ANTERIOR"].ToString();
        

        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspINS_CARTA_COBRAZAS_GENERAR(Convert.ToInt32(IDE_CARTA));
        if (dtResultado.Rows.Count > 0)
        {
            ddlEstado.SelectedIndex = 0;
            
            Listar("","","");
            //txtTicket.Text = string.Empty;
        }
    }
    protected void pdf(object sender, EventArgs e)
    {

        ImageButton btnPdf = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        Session["IDE_CARTA"] = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();
        Session["TICKET"] = GridView1.DataKeys[grdrow.RowIndex].Values["TICKET"].ToString();

    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "", "");
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("", "", "");
    }
    protected void ProcesarSAP(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderCarta);
        string Archivo = string.Empty;
        ImageFormat format;
        String fileExtension = string.Empty;

        ImageButton btnGuardar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_CARTA = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();

        TextBox txtSAP = (TextBox)grdrow.FindControl("txtSAP");

        if (txtSAP.Text == string.Empty)
        {
            string cleanMessage = "Ingresar codigo SAP";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {


            ////////////////////IMAGEN CARGA//////////////////////


            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnGuardar);
            FileUpload FileUpload1 = (FileUpload)grdrow.FindControl("FileUpload1");

            Boolean fileOK = false;
            String pathFoto = Server.MapPath(FolderCarta);



            // Si el directorio no existe, crearlo
            if (!Directory.Exists(pathFoto))
                Directory.CreateDirectory(pathFoto);

            if (FileUpload1.HasFile)
            {

                string fileName = FileUpload1.FileName;
                int length = FileUpload1.PostedFile.ContentLength;

                fileExtension = Path.GetExtension(FileUpload1.FileName).ToUpper();

                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int j = 0; j < allowedExtensions.Length; j++)
                {
                    if (fileExtension == allowedExtensions[j].ToUpper())
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    
                    Archivo = "SAP_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.FileName);
                    if (File.Exists(path + Archivo))
                    {
                        File.Delete(path + Archivo);
                       
                    }

                    System.Drawing.Image img = RedimensionarImagen(FileUpload1.PostedFile.InputStream);
                    switch (fileExtension)
                    {
                        case "gif":
                            format = ImageFormat.Gif;
                            break;
                        case "bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case "png":
                            format = ImageFormat.Png;
                            break;
                        default:
                            format = ImageFormat.Jpeg;
                            break;
                    }

                    img.Save(pathFoto + Archivo, format);
                    //FileUpload1.SaveAs(path + Archivo);

                    
       
                    DataTable dtResultado = new DataTable();
                    BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
                    BL_CARTA_COBRAZAS_DETALLE xobj = new BL_CARTA_COBRAZAS_DETALLE();
                    DataTable dt = new DataTable();
                    dtResultado = obj.uspUPD_CARTA_COBRAZAS_SAP(Convert.ToInt32(IDE_CARTA), txtSAP.Text, Session["IDE_USUARIO"].ToString(), FolderCarta, Archivo);
                    if (dtResultado.Rows.Count > 0)
                    {
                        string cleanMessage = "Registro satisfactoio";
                        dt = xobj.SP_CORREO_APROBACIONES_CARTACOBRANZA_SAP(Convert.ToInt32(IDE_CARTA));
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        Listar("", "", "");
                    }
                    else
                    {
                        txtSAP.Text = "";
                        string cleanMessage = "No se puede realizar ninguna actualización";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                }
                catch (Exception ex)
                {
                    string cleanMessage = "Archivo no puede ser cargado";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
            }
            else
            {
                string cleanMessage = "No se cargo imagen, vuelva a intentarlo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            }

            

            
           
        }
    }

  
    private static System.Drawing.Image RedimensionarImagen(Stream stream)
    {
        // Se crea un objeto Image, que contiene las propiedades de la imagen
        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

        // Tamaño máximo de la imagen (altura o anchura)
        const int max = 1500;

        int h = img.Height;
        int w = img.Width;
        int newH, newW;

        if (h > w && h > max)
        {
            // Si la imagen es vertical y la altura es mayor que max,
            // se redefinen las dimensiones.
            newH = max;
            newW = (w * max) / h;
        }
        else if (w > h && w > max)
        {
            // Si la imagen es horizontal y la anchura es mayor que max,
            // se redefinen las dimensiones.
            newW = max;
            newH = (h * max) / w;
        }
        else
        {
            newH = h;
            newW = w;
        }
        if (h != newH && w != newW)
        {
            // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
              System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
        }
        else
            return img;
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ESTADO_ING_OPE = DataBinder.Eval(e.Row.DataItem, "ESTADO_ING_OPE").ToString();
            string ESTADO_GERENTE_OPE = DataBinder.Eval(e.Row.DataItem, "ESTADO_GERENTE_OPE").ToString();
            string ESTADO_ING_DEST = DataBinder.Eval(e.Row.DataItem, "ESTADO_ING_DEST").ToString();
            string ESTADO_GERENTE_DEST = DataBinder.Eval(e.Row.DataItem, "ESTADO_GERENTE_DEST").ToString();

            //if (ESTADO_ING_OPE !=string.Empty )
            //{
            //    e.Row.Cells[8].BackColor 333333= Color.FromName(ESTADO_ING_OPE);
            //    e.Row.Cells[9].BackColor = Color.FromName(ESTADO_GERENTE_OPE);
            //    e.Row.Cells[10].BackColor = Color.FromName(ESTADO_ING_DEST);
            //    e.Row.Cells[11].BackColor = Color.FromName(ESTADO_GERENTE_DEST);
            //}
          
           
        }
    }

    protected void txtD_CENTRO_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }
    protected void txtTicket_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }
    protected void txtC_CENTRO_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }
    protected void filtros()
    {
        TextBox txtTicket_H = (TextBox)GridView1.HeaderRow.FindControl("txtTicket_H");
        TextBox txtC_CENTRO_H = (TextBox)GridView1.HeaderRow.FindControl("txtC_CENTRO_H");
        TextBox txtD_CENTRO_H = (TextBox)GridView1.HeaderRow.FindControl("txtD_CENTRO_H");
        //TextBox txtSOLPED_H = (TextBox)GridView1.HeaderRow.FindControl("txtSOLPED_H");
        //TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");
        //TextBox txtGPO_H = (TextBox)GridView1.HeaderRow.FindControl("txtGPO_H");

        Listar(txtTicket_H.Text.Trim(), txtC_CENTRO_H.Text.Trim(), txtD_CENTRO_H.Text.Trim());
        // txtFamilia_H.Text.Trim(), txtSUBFAMILIA_H.Text.Trim(),txtSOLPED_H.Text.Trim());
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);

        if (GridView1.Rows.Count>0)
        {
            Listar(txtTicket_H.Text.Trim(), txtC_CENTRO_H.Text.Trim(), txtD_CENTRO_H.Text.Trim());
        }
        else
        {
            Listar("", ddlCentro.SelectedValue, ddlCentro.SelectedValue);
        }


    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        Listar("",ddlCentro.SelectedValue, ddlCentro.SelectedValue);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage;
        int intContador = 0;

        if (GridView1.Rows.Count == 0)
        {
             cleanMessage = "No existe Registros";
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
            return;
        }

        string IDE_CARTA;
        foreach (GridViewRow row in GridView1.Rows)
        {

            CheckBox ChkBoxCell = ((CheckBox)row.FindControl("chkSelect"));

            if (ChkBoxCell.Checked)
            {
                IDE_CARTA = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                DataTable dtResultado = new DataTable();
                BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
             
                DataTable dt = new DataTable();
                dtResultado = obj.uspDEL_CARTA_COBRAZAS(Convert.ToInt32(IDE_CARTA));
                intContador++;
            }
            ChkBoxCell = null;
        }

        if (intContador> 0)
        {
            cleanMessage = "Registros eliminados correctamente";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        Listar("", "", "");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);

    }

    protected void ReenviarCC(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderCarta);
        string Archivo = string.Empty;


        ImageButton btnReenviar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_CARTA = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();


        dtResultado = obj.SP_CORREO_APROBADOR_CARTACOBRANZA_PENDIENTE(Convert.ToInt32(IDE_CARTA));
       string  cleanMessage = "Se notifico nuevamente a los aprobadores (pendientes)";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
    }
}