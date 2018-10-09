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
using System.Text;
using System.Drawing;

public partial class CAREMENOR_ValorizarFijaFecha : System.Web.UI.Page
{
    public string ListaCodigo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExportar);
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Proyectos();
            Listar();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void Listar()
    {
       
        if (GridView1.Rows.Count > 0)
        {
            TextBox txtRequerimientosa_H = (TextBox)GridView1.HeaderRow.FindControl("txtRequerimientosa_H");
            TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");

            filtros(txtRequerimientosa_H.Text.Trim(),txtPDC_H.Text.Trim());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            filtros("", "");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
    }
    protected void filtros(string txtRequerimientosa_H,string txtPDC_H)
    {


        string Centro = string.Empty;
        int contarCC = Convert.ToInt32(ddlcentro.Items.Count.ToString());
        if (contarCC <= 1)
        {
            Centro = ddlcentro.SelectedValue.ToString();
        }
        else
        {
            if (ddlcentro.SelectedIndex == 0)
            {
                Centro = string.Empty;
            }
            else
            {
                Centro = ddlcentro.SelectedValue.ToString();
            }
        }


        string ESTADO = "0";
        if (ddlEstado.SelectedIndex ==0)
        {
            ESTADO = string.Empty;
        }
        else
        {
            ESTADO = ddlEstado.SelectedValue.ToString();
        }

           BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
            DataTable dtResultado = new DataTable();
            dtResultado = obj.USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_TODO(Centro, "", ESTADO,txtPDC_H , txtRequerimientosa_H);
        if (dtResultado.Rows.Count > 0)
        {
            lblCantidad.Text = "Cantidad : " + dtResultado.Rows.Count.ToString();
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        else
        {
            lblCantidad.Text = "Cantidad : " + dtResultado.Rows.Count.ToString();
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }
        
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            

            //Literal tot = (Literal)e.Row.FindControl("ltltotal");
            //Double total = Double.Parse(tot.Text);

            //for (int i = 0; i <= 13; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}


            //for (int i = 14; i <= 20; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#626567");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}


            //for (int i = 21; i <= 24; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#5fbf6f");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}



            //for (int i = 25; i <= 29; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#922B21");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}





            //for (int i = 30; i <= 36; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}


            //for (int i = 37; i <= 40; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#512E5F");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}

        }

       
         
    }


  
    
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        int registroUpdate = 0;
        string cleanMessage = string.Empty;
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        if (GridView1.Rows.Count == 0)
        {

            cleanMessage = "No existe registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            string Reqs_Correlativo;
            string Requ_Numero;
            string Reqd_CodLinea;
            string ide_valor;
            string Proyecto;

            foreach (GridViewRow row in GridView1.Rows)
            {

                Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key
                ide_valor = GridView1.DataKeys[row.RowIndex].Values[3].ToString(); // extrae key
                Proyecto = GridView1.DataKeys[row.RowIndex].Values[4].ToString(); // extrae key

                TextBox txtInicio = row.FindControl("txtInicio") as TextBox;  //(TextBox)GridView1.HeaderRow.FindControl("txtInicio");
                TextBox txtTarifaDia = row.FindControl("txtTarifaDia") as TextBox;//  (TextBox)GridView1.HeaderRow.FindControl("txtTarifaDia");
                TextBox txtFin = row.FindControl("txtFin") as TextBox;//
                TextBox txtUltimaVal = row.FindControl("txtUltimaVal") as TextBox;//


                DropDownList ddlTarifa = row.FindControl("ddlTarifa") as DropDownList;//
               


                TextBox txtDia_inicio = row.FindControl("txtDia_inicio") as TextBox;//
                TextBox txtDia_fin = row.FindControl("txtDia_fin") as TextBox;//
             
                string inicio = string.IsNullOrEmpty(txtDia_inicio.Text) ? "0" : txtDia_inicio.Text;
                txtDia_fin.Text = (Convert.ToInt32(inicio) - 1).ToString();


                //DateTime fechaActual = DateTime.Now;
                //int anyo = fechaActual.Year;
                //int mes = fechaActual.Month;
                //int MES = Convert.ToInt32(mes) - 1;
                //int MES_SGTE = Convert.ToInt32(mes);
                //int ANIO = Convert.ToInt32(anyo);
                //int ANIO_SGTE = Convert.ToInt32(anyo);

                //if (MES == 0)//enero
                //{
                //    MES = 12;
                //    ANIO = ANIO - 1;
                //}


                //MES.ToString().Trim().PadLeft(2, '0');
                //MES_SGTE.ToString().Trim().PadLeft(2, '0');


                //txtInicio.Text= "21/" + MES.ToString().Trim().PadLeft(2, '0') + "/" + ANIO.ToString();
                //txtfin.Text = "20/" + MES_SGTE.ToString().Trim().PadLeft(2, '0') + "/" + ANIO_SGTE.ToString();

                Label lblMoneda = row.FindControl("lblMoneda") as Label;//

                string IDE_MONEDA, TIPO_TARIFA;
                IDE_MONEDA = lblMoneda.Text;
                if (ddlTarifa.SelectedIndex  == 0)
                {
                    TIPO_TARIFA = string.Empty;
                }
                else
                {
                    TIPO_TARIFA = ddlTarifa.SelectedValue.ToString();
                }

                

                if (txtInicio.Text != string.Empty && txtFin.Text != string.Empty)
                {
                    if (EsFecha(txtInicio.Text) == false)
                    {
                        cleanMessage = "Error de fecha de inicio del req." + Requ_Numero +"." + Reqd_CodLinea+ "-"+ Reqs_Correlativo;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if(EsFecha(txtFin.Text) == false)
                    {
                        cleanMessage = "Error de fecha de termino del req." + Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if (txtDia_inicio.Text == string.Empty)
                    {
                        cleanMessage = "Ingresar día de incio del periodo del " + Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else if (txtDia_fin.Text == string.Empty)
                    {
                        cleanMessage = "Ingresar día de termino del periodo del " + Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }

                    else
                    {
                        if (EsFecha(txtInicio.Text) == true && EsFecha(txtFin.Text) == true)
                        {

                            if(Convert.ToDateTime(txtInicio.Text)> Convert.ToDateTime(txtFin.Text))
                            {
                                cleanMessage = "La fecha de inicio del Req. " + Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo + ", no puede ser mayor a su fecha de finalización";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                            }
                            else
                            {
                                string PRECIO = string.IsNullOrEmpty(txtTarifaDia.Text) ? "0.00" : txtTarifaDia.Text;
                                if(txtUltimaVal.Text== string.Empty)
                                {
                                    // REGISTRAR INFO
                                    dtResultado = obj.USP_UPDATE_TBL_VALORIZACION_FECHA(ide_valor, Requ_Numero, Reqd_CodLinea, Reqs_Correlativo,Convert.ToDateTime(txtInicio.Text).ToString("dd/MM/yyyy"),Convert.ToDateTime( txtFin.Text).ToString("dd/MM/yyyy"), PRECIO, IDE_MONEDA, TIPO_TARIFA, txtDia_inicio.Text, txtDia_fin.Text);
                                    if (dtResultado.Rows.Count > 0)
                                    {
                                        if (Convert.ToInt32(dtResultado.Rows[0]["ID"].ToString()) > 0)
                                        {
                                            registroUpdate++;
                                        }

                                    }
                                }
                                else
                                {
                                    if (Convert.ToDateTime(txtUltimaVal.Text) > Convert.ToDateTime(txtFin.Text))
                                    {
                                        cleanMessage = "La fecha de fin del Req. " + Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo + ", no puede ser menor a su fecha de su ultima valorización";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                                    }
                                    else
                                    {
                                        // REGISTRAR INFO
                                        dtResultado = obj.USP_UPDATE_TBL_VALORIZACION_FECHA(ide_valor, Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, Convert.ToDateTime(txtInicio.Text).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtFin.Text).ToString("dd/MM/yyyy"), PRECIO, IDE_MONEDA, TIPO_TARIFA, txtDia_inicio.Text, txtDia_fin.Text);
                                        if (dtResultado.Rows.Count > 0)
                                        {
                                            if (Convert.ToInt32(dtResultado.Rows[0]["ID"].ToString()) > 0)
                                            {
                                                registroUpdate++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (txtInicio.Text != string.Empty && txtFin.Text == string.Empty)
                {
                    if (EsFecha(txtInicio.Text) == true)
                    {
                        string PRECIO = string.IsNullOrEmpty(txtTarifaDia.Text) ? "0.00" : txtTarifaDia.Text;
                        dtResultado = obj.USP_UPDATE_TBL_VALORIZACION_FECHA(ide_valor,Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, Convert.ToDateTime(txtInicio.Text).ToString("dd/MM/yyyy"), "", PRECIO, IDE_MONEDA, TIPO_TARIFA, txtDia_inicio.Text, txtDia_fin.Text);
                        if (dtResultado.Rows.Count > 0)
                        {
                            registroUpdate++;
                        }
                    }
                    else
                    {
                        cleanMessage = "Error de fecha de inicio del req." + Requ_Numero;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }

                }
            }
        }


        

        if (registroUpdate > 0)
        {
            cleanMessage = "Actualización correcta";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            Listar();
        }
        else
        {
            cleanMessage = "Completar datos para la valorización";
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

    protected void seleccionar(object sender, EventArgs e)
    {
        
        ImageButton btnEditar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;

        GridViewRow row = btnEditar.NamingContainer as GridViewRow;
        //string pk = GridPEP.DataKeys[row.RowIndex].Values[0].ToString();

      

        string Requ_Numero = GridView1.DataKeys[grdrow.RowIndex].Values["Requ_Numero"].ToString();
        string Reqd_CodLinea = GridView1.DataKeys[grdrow.RowIndex].Values["Reqd_CodLinea"].ToString();
        string Reqs_Correlativo = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_Correlativo"].ToString();
        HdidValor.Value = GridView1.DataKeys[grdrow.RowIndex].Values["ide_valor"].ToString();
        HDProyecto.Value = GridView1.DataKeys[grdrow.RowIndex].Values["Proy_Codigo"].ToString();
        

        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup('" + Requ_Numero + "'," + Reqd_CodLinea + ",'" + Reqs_Correlativo + "','" + HdidValor.Value + "','" + HDProyecto.Value + "'," +600 + "," + 400 + ");", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }


   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Boolean KeyID = Convert.ToBoolean(GridView1.DataKeys[e.Row.RowIndex].Values["FLG_VISIBLE"].ToString());

            if (KeyID == false)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#c6efce");
            }



            DropDownList ddlTarifa = (e.Row.FindControl("ddlTarifa") as DropDownList);
            ddlTarifa.DataSource = GetDataTarifa();
            ddlTarifa.DataTextField = "DESCRIPCION";
            ddlTarifa.DataValueField = "TIPO_TARIFA";
            ddlTarifa.DataBind();

            //Add Default Item in the DropDownList
            ddlTarifa.Items.Insert(0, new ListItem("---"));

            //Select the Country of Customer in DropDownList
            string Tarifa = (e.Row.FindControl("lblTarifa") as Label).Text;
           
            if (Tarifa != string.Empty)
            {
                ddlTarifa.Items.FindByValue(Tarifa).Selected = true;

            }

            // color 

            TextBox txtInicio = (e.Row.FindControl("txtInicio") as TextBox);
            TextBox txtTarifaDia = (e.Row.FindControl("txtTarifaDia") as TextBox);


            foreach (TableCell cell in e.Row.Cells)
            {
                if (txtInicio.Text == string.Empty && txtTarifaDia.Text == string.Empty && Tarifa == "")
                {
                    cell.BackColor = Color.Orange;
                }
                else if (txtInicio.Text != string.Empty && txtTarifaDia.Text != string.Empty && Tarifa == "")
                {
                    cell.BackColor = Color.Orange;
                }
                else if (txtInicio.Text != string.Empty && txtTarifaDia.Text == string.Empty && Tarifa == "")
                {
                    cell.BackColor = Color.Orange;
                }
                else if (txtInicio.Text != string.Empty && txtTarifaDia.Text == string.Empty && Tarifa != "")
                {
                    cell.BackColor = Color.Orange;
                }
                else if (txtInicio.Text == string.Empty && txtTarifaDia.Text != string.Empty && Tarifa == "")
                {
                    cell.BackColor = Color.Orange;
                }
            }

            


            //if (txtInicio.Text == string.Empty && txtTarifaDia.Text == string.Empty && Tarifa == "")
            //{


            //    foreach (TableCell cell in e.Row.Cells)
            //    {
            //        cell.BackColor = Color.Orange;
            //    }
            //}
            //else if (txtInicio.Text != string.Empty && txtTarifaDia.Text != string.Empty && Tarifa == "")
            //{
            //    //e.Row.BackColor = Color.Orange;
            //    foreach (TableCell cell in e.Row.Cells)
            //    {
            //        cell.BackColor = Color.Orange;
            //    }
            //}

        }
    }
    private DataTable GetData()
    {
        
        DataTable dt= new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("IDE_MONEDA"));
        dt.Columns.Add(new DataColumn("DESCRIPCION"));

        //Now Add Values

        dt.Rows.Add(1, "SOLES");
        dt.Rows.Add(2, "DOLARES");
      

        return dt;
    }

    private DataTable GetDataTarifa()
    {

        DataTable dt = new DataTable();

        //Add Columns to Table
        dt.Columns.Add(new DataColumn("TIPO_TARIFA"));
        dt.Columns.Add(new DataColumn("DESCRIPCION"));

        //Now Add Values

        dt.Rows.Add(1, "DIARIO");
        dt.Rows.Add(2, "MENSUAL");


        return dt;
    }

    protected void rangoFecha()
    {
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }


    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        //Proyectos();
        Listar();
    }

    protected void btnValorizar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/ValorizarEquipoMenor");
    }

    protected void btnTarifas_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/ValorizarFijaFecha");
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupVerValorizarReporte(" + 1100 + "," + 500 + ");", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        //Response.Redirect("~/CAREMENOR/ValorizarReporte");
    }


    protected void Proyectos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_VALORIZACION_CC("ALQUILER VALORIZACION", Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            

            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "Proy_Nombre";
            ddlcentro.DataValueField = "Proy_Codigo";
            ddlcentro.DataBind();

            if (dtResultado.Rows.Count > 1)
            {
                ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
            }
           
            //Proveedor();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            

            string cleanMessage = "No cuenta con permisos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        string Centro = string.Empty;
        int contarCC = Convert.ToInt32(ddlcentro.Items.Count.ToString());
        if (contarCC <= 1)
        {
            Centro = ddlcentro.SelectedValue.ToString();
        }
        else
        {
            if (ddlcentro.SelectedIndex == 0)
            {
                Centro = string.Empty;
            }
            else
            {
                Centro = ddlcentro.SelectedValue.ToString();
            }
        }

        string ESTADO = "0";
        if (ddlEstado.SelectedIndex == 0)
        {
            ESTADO = string.Empty;
        }
        else
        {
            ESTADO = ddlEstado.SelectedValue.ToString();
        }
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_TODO(Centro, "", ESTADO, "","");
        if (dtResultado.Rows.Count > 0)
        {
          
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();

            GridViewExportUtil.Export("TARIFAS_" + DateTime.Now + ".xls", GridView2);
            return;
        }
       
    }

    protected void ddlcentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void btnCuadro_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupVerResumen(" + 1000 + "," + 500 + ");", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
    
}