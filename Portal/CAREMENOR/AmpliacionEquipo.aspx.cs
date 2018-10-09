using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;

public partial class CAREMENOR_AmpliacionEquipo : System.Web.UI.Page
{
    string FolderAlquiler = ConfigurationManager.AppSettings["FolderAlquiler"];
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        ScriptManager.GetCurrent(this).RegisterPostBackControl(GridView1);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnAgregar);
        if (!Page.IsPostBack)
        {

            //GridView1.Attributes["onchange"] = "UploadFile(this)";

            string PDC = Request.QueryString["PDC"];
            if (PDC != string.Empty )
            {
                txtPdc.Text = PDC;
                Buscarrequerimientos();
            }
        }
    }
   
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        if (txtPdc.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar PDC";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            Buscarrequerimientos();
        }
    }
    protected void Buscarrequerimientos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
       
        if(txtPdc.Text != string.Empty )
        {
            string Estado = string.Empty;
            Estado = ddlEstado.SelectedValue.ToString();

            if (ddlEstado.SelectedIndex == 0)
            {
                Estado = string.Empty;
            }
            dtResultado = obj.USP_SEL_TBL_REQUERIMIENTO_PDC_AMPLIAR("", txtPdc.Text.Trim(), "1", Estado);//1 los equipos ampliados
            if (dtResultado.Rows.Count > 0)
            {
                btnAgregar.Visible = true;
                GridView1.DataSource = dtResultado;
                GridView1.DataBind();
            }
            else
            {
                btnAgregar.Visible = false ;
                //string cleanMessage = "No exisen requerimientos con solicitud de ampliación ";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                GridView1.DataSource = dtResultado;
                GridView1.DataBind();
            }
        }
        
    }
    

    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        string Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, cleanMessage, Reqs_ItemSecuencia, A_FASES_AMPLIACION;

        int intregistros = 0;
        if (GridView1.Rows.Count == 0)
        {

            cleanMessage = "No existe registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }



        decimal total = 0;
        decimal valor = 0;
        decimal valorMov = 0;

        string codigos = string.Empty;

        foreach (GridViewRow Fila in GridView1.Rows)
        {
            
            Label lblMontoAlq = ((Label)Fila.FindControl("lblMontoAlq"));
            Label lblMontoMov = ((Label)Fila.FindControl("lblMontoMov"));
            Label lblMontoAmp = ((Label)Fila.FindControl("lblMontoAmp"));
            total = total +

            Convert.ToDecimal(string.IsNullOrEmpty(lblMontoAlq.Text) ? "0" : lblMontoAlq.Text) +
            Convert.ToDecimal(string.IsNullOrEmpty(lblMontoMov.Text) ? "0" : lblMontoMov.Text) +
            Convert.ToDecimal(string.IsNullOrEmpty(lblMontoAmp.Text) ? "0" : lblMontoAmp.Text);
        }


        Guid g = Guid.NewGuid();

        foreach (GridViewRow row in GridView1.Rows)
        {

            Label lblMontoAmp = ((Label)row.FindControl("lblMontoAmp"));
            //Label lblMontoAlq = ((Label)row.FindControl("lblMontoAlq"));
            Label lblMontoMov = ((Label)row.FindControl("lblMontoMov"));
            Label lblFases = ((Label)row.FindControl("lblFases"));
            RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

            Label lblposicionAlq = ((Label)row.FindControl("lblposicionAlq"));

            TextBox txtNuevaPosicion = ((TextBox)row.FindControl("txtNuevaPosicion"));

            Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
            Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString();// extrae key
            Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key
            Reqs_ItemSecuencia = GridView1.DataKeys[row.RowIndex].Values[3].ToString(); // extrae key
            A_FASES_AMPLIACION = GridView1.DataKeys[row.RowIndex].Values[4].ToString(); // extrae key
            codigos += Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim() + ",";

            if (A_FASES_AMPLIACION =="1")
            {
                if (rb.SelectedValue == "P")
                {
                    //agregar monto

                    valorMov = Convert.ToDecimal(string.IsNullOrEmpty(lblMontoMov.Text) ? "0" : lblMontoMov.Text);
                    valor = Convert.ToDecimal(string.IsNullOrEmpty(lblMontoAmp.Text) ? "0" : lblMontoAmp.Text);

                    BE_TBL_RequerimientoSubDetalle oBESol = new BE_TBL_RequerimientoSubDetalle();
                    oBESol.Requ_Numero = Requ_Numero;
                    oBESol.Reqd_CodLinea = Reqd_CodLinea;
                    oBESol.Reqs_Correlativo = Reqs_Correlativo;
                    oBESol.D_PDC = txtPdc.Text.Trim();
                    //oBESol.D_PDC_FECHA = txtFechaPDC.Text;

                    oBESol.D_PDC_MONTO = Convert.ToDecimal(string.IsNullOrEmpty(valor.ToString()) ? "0" : valor.ToString());
                    oBESol.D_PDC_MONTO_TOTAL = Convert.ToDecimal(string.IsNullOrEmpty(total.ToString()) ? "0" : total.ToString());
                    oBESol.D_PDC_MONTO_MOVIL = Convert.ToDecimal(string.IsNullOrEmpty(valorMov.ToString()) ? "0" : valorMov.ToString());
                    oBESol.GUID = g.ToString();
                    oBESol.A_FASES_AMPLIACION = lblFases.Text;
                    int dtrpta;
                    dtrpta = new BL_TBL_RequerimientoSubDetalle().uspINS_TBL_RequerimientoSubDetalle_AMPLIACION(oBESol);
                    if (dtrpta > 0)
                    {
                        intregistros++;
                    }
                }

                else if (rb.SelectedValue == "T/P")
                {
                    valorMov = Convert.ToDecimal(string.IsNullOrEmpty(lblMontoMov.Text) ? "0" : lblMontoMov.Text);
                    valor = Convert.ToDecimal(string.IsNullOrEmpty(lblMontoAmp.Text) ? "0" : lblMontoAmp.Text);

                    if (txtNuevaPosicion.Text.Trim() == string.Empty)
                    {


                        cleanMessage = "Falta ingresar nueva posicion al req. " + Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else
                    {
                        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                        DataTable dtResultado = new DataTable();

                        codigos += Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim() + ",";

                        dtResultado = obj.uspINS_TBL_GENERAR_AMPLIACION(
                            Requ_Numero,
                            Reqd_CodLinea,
                            Reqs_Correlativo,
                            lblFases.Text,
                            string.IsNullOrEmpty(lblposicionAlq.Text) ? "10" : lblposicionAlq.Text,
                            txtNuevaPosicion.Text.Trim(),
                            valor.ToString(),
                            total.ToString(),
                            txtPdc.Text.Trim(),
                            Reqs_ItemSecuencia
                            );
                        if (dtResultado.Rows.Count > 0)
                        {

                            BL_TBL_RequerimientoSubDetalle objx = new BL_TBL_RequerimientoSubDetalle();

                            string req = dtResultado.Rows[0]["ID"].ToString();
                            //string req = Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim() + ",";
                            objx.USP_SEL_TBL_REQUERIMIENTO_CORREO_LIBERACION(req, "ALQUILER CARE", 21);

                            intregistros++;
                        }

                    }


                    String datos = string.Empty;
                    string ArchivoFoto = string.Empty;
                    String fileExtension = string.Empty;
                    Boolean fileOK = false;
                    string fileArchivo = string.Empty;
                    string Name = string.Empty;
                    string TipoArchivo = "AMPLIACION";
                    FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");



                    if (FileUpload1.HasFile)
                    {

                        string fileName = FileUpload1.FileName.ToString();
                        int length = FileUpload1.PostedFile.ContentLength;

                        fileExtension = Path.GetExtension(FileUpload1.FileName);

                        String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
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
                            string ruta = Server.MapPath(FolderAlquiler);

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
                            }

                            else
                            {
                                fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales(BL_Session.CENTRO_COSTO + "_" + TipoArchivo + "_" + Path.GetFileName(FileUpload1.FileName));
                                //FileUpload1.SaveAs(archivo);
                                FileUpload1.SaveAs(ruta + fileArchivo);
                            }



                        }
                        catch (Exception ex)
                        {
                            cleanMessage = "Archivo no puedo ser cargado";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        }
                    }
                }

           


            }
            else
            {
                cleanMessage = "Indicar tipo de ampliación";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }

        if (intregistros >0)
        {

            codigos = string.Empty;

            Buscarrequerimientos();

            foreach (GridViewRow row in GridView1.Rows)
            {
                Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString();// extrae key
                Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key
                codigos += Requ_Numero.Trim() + "." + Reqd_CodLinea.Trim() + "-" + Reqs_Correlativo.Trim() + ",";

            }
            BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();

            obj.USP_SEL_TBL_REQUERIMIENTO_CORREO_AMPLIACION(codigos, "ALQUILER CARE", 2);
            cleanMessage = "Actualización satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

  
    protected void VerDatos(object sender, EventArgs e)
    {
        string Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, cleanMessage;

        /////////////////////  PROCESAR LINEAS   //////////////////////////////////







        /////////////////////  CARGAR ARCHIVO   //////////////////////////////////

        String datos = string.Empty;
        string ArchivoFoto = string.Empty;
        String fileExtension = string.Empty;
        Boolean fileOK = false;
        string fileArchivo = string.Empty;
        string Name = string.Empty;
        string TipoArchivo = "AMPLIACION";
        ImageButton btnVer = ((ImageButton)sender);
        GridViewRow row = btnVer.NamingContainer as GridViewRow;

       

        Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
        Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString();// extrae key
        Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key


        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnVer);
        FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");

    

        if (FileUpload1.HasFile)
        {

            string fileName = FileUpload1.FileName.ToString();
            int length = FileUpload1.PostedFile.ContentLength;

            fileExtension = Path.GetExtension(FileUpload1.FileName);

            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
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
                string ruta = Server.MapPath(FolderAlquiler);

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
                }

                else
                {
                    fileArchivo = EliminarCaracteres.ReemplazarCaracteresEspeciales(BL_Session.CENTRO_COSTO + "_" + TipoArchivo + "_" + Path.GetFileName(FileUpload1.FileName));
                    //FileUpload1.SaveAs(archivo);
                    FileUpload1.SaveAs(ruta + fileArchivo);
                }


                BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                DataTable dt = new DataTable();
                dt = obj.USP_SEL_TBL_REQUERIMIENTO_FILE_AMPLIACION(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, fileArchivo);
                string PDC = Request.QueryString["PDC"];
                if (PDC != string.Empty)
                {
                    txtPdc.Text = PDC;
                    Buscarrequerimientos();
                }
            }
            catch (Exception ex)
            {
                cleanMessage = "Archivo no puedo ser cargado";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTipo = (Label)e.Row.FindControl("lblTipo");
            if (lblTipo.Text !=string.Empty)
            {
                RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
                rblShippers.Items.FindByValue((e.Row.FindControl("lblTipo") as Label).Text).Selected = true;
            }
         
        }
    }

    protected void btnAmpliaciones_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/AmpliacionControl");
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Buscarrequerimientos();
    }
}
  
