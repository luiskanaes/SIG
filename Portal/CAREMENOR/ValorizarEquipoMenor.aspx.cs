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
public partial class CAREMENOR_ValorizarEquipoMenor : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CareMenor"].ToString());
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];
   
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
            Anios();
            Meses();
            ddlanio.SelectedValue = DateTime.Today.Year.ToString();

            ddlMes.SelectedValue = DateTime.Today.Month.ToString();
            ConsultarPermisos();
            //Proyectos();
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
            //rangoFecha();

        }
    }
    protected void ConsultarPermisos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_VALORIZACION_CC("ALQUILER VALORIZACION", Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            btnBuscar.Visible = true;
      
            btnGuardar.Visible = true;
            btnCerrar.Visible = true;
            btnValorizar.Visible = true;
            btnTarifas.Visible = true;
            btnImprimir.Visible = true;

           
        }
        else
        {
            btnImprimir.Visible = false;
            btnValorizar.Visible = false;
            btnTarifas.Visible = false;
            btnBuscar.Visible = false;
           
            btnGuardar.Visible = false;
            btnCerrar.Visible = false;

            string cleanMessage = "No cuenta con permisos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);


        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
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
    protected void Proyectos()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.USP_SEL_TBL_VALORIZACION_CC("ALQUILER VALORIZACION", Session["IDE_USUARIO"].ToString());
        if (dtResultado.Rows.Count > 0)
        {
            btnBuscar.Visible = true;
            GridView1.Visible = true;
            btnGuardar.Visible = true;
            btnCerrar.Visible = true;
            btnValorizar.Visible = true;
            btnTarifas.Visible = true;
            btnImprimir.Visible = true;

            ddlcentro.DataSource = dtResultado;
            ddlcentro.DataTextField = "Proy_Nombre";
            ddlcentro.DataValueField = "Proy_Codigo";
            ddlcentro.DataBind();

            if (dtResultado.Rows.Count > 1)
            {
                ddlcentro.Items.Insert(0, new ListItem("--- TODOS ---", ""));
            }
           

            Proveedor();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            btnImprimir.Visible = false;
            btnValorizar.Visible = false;
            btnTarifas.Visible = false;
            btnBuscar.Visible = false;
            GridView1.Visible = false;
            btnGuardar.Visible = false;
            btnCerrar.Visible = false;

            string cleanMessage = "No cuenta con permisos";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
           

        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void Proveedor()
    {

        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        string Centro = string.Empty;

        int contarCC = Convert.ToInt32(ddlcentro.Items.Count.ToString());

        if(contarCC <=1)
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
        

         dtResultado = obj.USP_SEL_TBL_VALORIZACION_PROVEEDOR(Centro, ddlanio.SelectedValue.ToString(), ddlMes.SelectedValue.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ddlProveedor.DataSource = dtResultado;
            ddlProveedor.DataTextField = "Proveedor";
            ddlProveedor.DataValueField = "D_Prov_RUC";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
        else
        {
            ddlProveedor.DataSource = dtResultado;
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("--- TODOS ---", ""));
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        Listar("","","");
    }
    protected void Listar(string requerimiento, string descripcion, string PDC)
    {
        

      
        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        string proveedor = string.Empty;
        if (ddlProveedor.SelectedIndex == 0)
        {
            proveedor = string.Empty;
        }
        else
        {
            proveedor = ddlProveedor.SelectedValue.ToString();
        }

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

        dtResultado = obj.USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_V2(Centro, proveedor, ddlanio.SelectedValue.ToString(), ddlMes.SelectedValue.ToString(),PDC, requerimiento);
        if (dtResultado.Rows.Count > 0)
        {
            lblCantidad.Text = "Cantidad : " + dtResultado.Rows.Count.ToString();
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
            btnExportar.Visible = true;
        }
        else
        {
            btnExportar.Visible = false ;
            GridView1.DataSource = dtResultado;
            GridView1.DataBind();
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


            //for (int i = 0; i <= 13; i++)
            //{
            //    GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#195183");
            //    GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            //}

            for (int i = 14; i <= 17; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC300");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.Black;
            }

            for (int i = 18; i <= 20; i++)
            {
                GridView1.Columns[i].HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#c70039");
                GridView1.Columns[i].HeaderStyle.ForeColor = System.Drawing.Color.White;
            }
            //DropDownList ddlTarifa = (e.Row.FindControl("ddlTarifa") as DropDownList);
            //ddlTarifa.DataSource = GetDataTarifa();
            //ddlTarifa.DataTextField = "DESCRIPCION";
            //ddlTarifa.DataValueField = "TIPO_TARIFA";
            //ddlTarifa.DataBind();

            ////Add Default Item in the DropDownList
            //ddlTarifa.Items.Insert(0, new ListItem("---"));

            ////Select the Country of Customer in DropDownList
            //string Tarifa = (e.Row.FindControl("lblTarifa") as Label).Text;
            //if (Tarifa != string.Empty)
            //{
            //    ddlTarifa.Items.FindByValue(Tarifa).Selected = true;
            //}

        }


    }

 

    protected void ddlcentro_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Proveedor();
    }

    protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar("","","");
    }
    protected void Meses()
    {
        ddlMes.DataSource = GetMeses();
        ddlMes.DataTextField = "ValueMember";
        ddlMes.DataValueField = "DisplayMember";
        ddlMes.DataBind();
    }
    private DataTable GetMeses()
    {
        DataTable dtMes = new DataTable();

        //Add Columns to Table
        dtMes.Columns.Add(new DataColumn("DisplayMember"));
        dtMes.Columns.Add(new DataColumn("ValueMember"));

        //Now Add Values

        dtMes.Rows.Add(1, "ENERO");
        dtMes.Rows.Add(2, "FEBRERO");
        dtMes.Rows.Add(3, "MARZO");
        dtMes.Rows.Add(4, "ABRIL");
        dtMes.Rows.Add(5, "MAYO");
        dtMes.Rows.Add(6, "JUNIO");
        dtMes.Rows.Add(7, "JULIO");
        dtMes.Rows.Add(8, "AGOSTO");
        dtMes.Rows.Add(9, "SETIEMBRE");
        dtMes.Rows.Add(10, "OCTUBRE");
        dtMes.Rows.Add(11, "NOVIEMBRE");
        dtMes.Rows.Add(12, "DICIEMBRE");

        return dtMes;

    }

    protected void btnTarifas_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/ValorizarFijaFecha");
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
            string ide_valor, Proy_Codigo, D_Prov_RUC, FLG_FASES;
            foreach (GridViewRow row in GridView1.Rows)
            {

                Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key
                ide_valor = GridView1.DataKeys[row.RowIndex].Values[3].ToString(); // extrae key
                Proy_Codigo = GridView1.DataKeys[row.RowIndex].Values[4].ToString(); // extrae key

                D_Prov_RUC = GridView1.DataKeys[row.RowIndex].Values[5].ToString(); // extrae key
                FLG_FASES = GridView1.DataKeys[row.RowIndex].Values[6].ToString(); // extrae key

                if (Convert.ToInt32(FLG_FASES) < 3)// MIGRADO 
                {
                    if (D_Prov_RUC == string.Empty)
                    {
                        cleanMessage = "Falta definir proveedor para el Req. " + Requ_Numero;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }
                    else
                    {
                        //TextBox txtInicio = row.FindControl("txtInicio") as TextBox;  
                        //TextBox txtTarifaDia = row.FindControl("txtTarifaDia") as TextBox;
                        TextBox txtObservacion = row.FindControl("txtObservacion") as TextBox;
                        TextBox txtDescuentoDia = row.FindControl("txtDescuentoDia") as TextBox;
                        TextBox txtDescuentoFinal = row.FindControl("txtDescuentoFinal") as TextBox;
                        TextBox txtHES = row.FindControl("txtHES") as TextBox;
                        Label lblDIAS_TRABAJO = row.FindControl("lblDIAS_TRABAJO") as Label;

                        Label lblTarifa_unidad = row.FindControl("lblTarifa_unidad") as Label;
                        Label lblTARIFA_DIA = row.FindControl("lblTARIFA_DIA") as Label;

                        Label lblV_FECHA_INICIO_VAL = row.FindControl("lblV_FECHA_INICIO_VAL") as Label;
                        Label lblV_FECHA_FIN_VAL = row.FindControl("lblV_FECHA_FIN_VAL") as Label;
                        Label lbl_ideMoneda = row.FindControl("lbl_ideMoneda") as Label;

                        TextBox txtInicioVal = row.FindControl("txtInicioVal") as TextBox;
                        TextBox txtFinVal = row.FindControl("txtFinVal") as TextBox;

                        if (lblTARIFA_DIA.Text == string.Empty)
                        {
                            cleanMessage = "Falta ingresar tarifa para el Req. " + Requ_Numero + "." + Reqd_CodLinea + "-" + Reqs_Correlativo;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        }
                        else
                        {
                            //lblDIAS_TRABAJO.Text = ObtenerDias(Convert.ToDateTime(txtInicioVal.Text), Convert.ToDateTime(txtFinVal.Text));
                            BE_valorizar_equipoMenor oBESol = new BE_valorizar_equipoMenor();
                            oBESol.IDE_VAL = 0;//Convert.ToInt32(string.IsNullOrEmpty(lblcodigo.Text) ? "0" : lblcodigo.Text); 
                            oBESol.MES = Convert.ToInt32(ddlMes.SelectedValue);
                            oBESol.ANIO = Convert.ToInt32(ddlanio.SelectedValue);
                            oBESol.Requ_Numero = Requ_Numero;
                            oBESol.Reqd_CodLinea = Reqd_CodLinea;
                            oBESol.Reqs_Correlativo = Reqs_Correlativo;
                            oBESol.Proy_Codigo = Proy_Codigo;
                            oBESol.Prov_RUC = D_Prov_RUC;
                            oBESol.FECHA_INICIO = lblV_FECHA_INICIO_VAL.Text;
                            oBESol.FECHA_FIN = lblV_FECHA_FIN_VAL.Text;
                            oBESol.IDE_MONEDA = Convert.ToInt32(lbl_ideMoneda.Text);
                            oBESol.PRECIO = Convert.ToDecimal(lblTARIFA_DIA.Text);
                            oBESol.DIA_TRABAJO = Convert.ToInt32(string.IsNullOrEmpty(lblDIAS_TRABAJO.Text) ? "0" : lblDIAS_TRABAJO.Text);
                            oBESol.DIA_DSCTO = Convert.ToInt32(txtDescuentoDia.Text);
                            oBESol.TOTAL = 0;
                            oBESol.DESCUENTO = Convert.ToInt32(txtDescuentoFinal.Text);
                            oBESol.FLG_FASES = Convert.ToInt32(FLG_FASES);
                            oBESol.USER_VALORIZA = Session["IDE_USUARIO"].ToString();
                            oBESol.ide_valor = ide_valor;
                            oBESol.OBSERVA = txtObservacion.Text;
                            oBESol.HES = txtHES.Text;
                            oBESol.PERIODO_INICIO = txtInicioVal.Text;
                            oBESol.PERIODO_FIN = txtFinVal.Text;
                            oBESol.UNIDAD_TARIFA = lblTarifa_unidad.Text;
                            int rpta;
                            rpta = new BL_TBL_RequerimientoSubDetalle().uspINS_valorizar_equipoMenor(oBESol);
                            if (rpta > 0)
                            {
                                registroUpdate++;
                            }
                        }

                    }
                }
                ////fin
              
            }


            if (registroUpdate > 0)
            {
                cleanMessage = "Se actualización satisfactoria";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                Listar("","","");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
            }
        }




        
    }

    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        rangoFecha();
        Proyectos();
        Listar("","","");
    }
    protected void rangoFecha()
    {
        int MES = Convert.ToInt32(ddlMes.SelectedValue.ToString()) - 1;
        int MES_SGTE = Convert.ToInt32(ddlMes.SelectedValue.ToString());
        int ANIO = Convert.ToInt32(ddlanio.SelectedValue.ToString());
        int ANIO_SGTE = Convert.ToInt32(ddlanio.SelectedValue.ToString());

        if (MES ==0)//enero
        {
            MES = 12;
            ANIO = ANIO - 1;
        }


        MES.ToString().Trim().PadLeft(2, '0');
        MES_SGTE.ToString().Trim().PadLeft(2, '0');

        //txtInicio.Text= "21/" + MES.ToString().Trim().PadLeft(2, '0') + "/" + ANIO.ToString();
        //txtfin.Text = "20/" + MES_SGTE.ToString().Trim().PadLeft(2, '0') + "/" + ANIO_SGTE.ToString();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void ddlanio_SelectedIndexChanged(object sender, EventArgs e)
    {
        rangoFecha();
        Proyectos();
        Listar("","","");
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        //Proyectos();
        filtros();
     
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

    private static string ObtenerDias(DateTime Fecha1, DateTime Fecha2)
    {
        

        int cuantosDiasPrestado;
        TimeSpan diferencia;
        diferencia = Fecha2 - Fecha1;
        //diferencia = MonthDevolucion.Value.Date - MonthPrestamo.Value.Date;
        cuantosDiasPrestado = diferencia.Days + 1;
        

        return cuantosDiasPrestado.ToString();
    }
    private static string  GetDateFormat(DateTime startDate, DateTime endDate)
    {
        string mensaje;
        //bool status = false;
        /*Valida fecha*/
        if (startDate.Date <= endDate.Date)
        {
            //status = true;
            TimeSpan difference = endDate.Subtract(startDate.Date);

            StringBuilder sb = new StringBuilder();

            if (difference.Ticks == 0)
            {
                sb.Append("0");
            }
            else if (difference.Ticks > 0)
            {
                // This is to convert the timespan to datetime object
                DateTime totalDate = DateTime.MinValue + difference;

                //int differenceInYears = totalDate.Year - 1;
                //int differenceInMonths = totalDate.Month - 1;
                int differenceInDays = totalDate.Day ;

                //if (differenceInYears > 0)
                //    sb.AppendFormat("{0} año(s)", differenceInYears);

                //if (differenceInMonths > 0)
                //    if (differenceInMonths == 1)
                //        sb.AppendFormat(" {0} mes", differenceInMonths);
                //    else
                //        sb.AppendFormat(" {0} meses", differenceInMonths);

                if (differenceInDays > 0)
                    if (differenceInDays == 1)
                        sb.AppendFormat(" {0} ", differenceInDays);
                    else
                        sb.AppendFormat(" {0} ", differenceInDays);
            }

            mensaje = sb.ToString();
        }
        else
        {
            mensaje = "0";
        }
        return mensaje;

    }


    protected void txtRequerimientosa_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }
    protected void txtSubf_Descripcion_H_TextChanged(object sender, EventArgs e)
    {
        filtros();
    }
    protected void filtros()
    {
        if (GridView1.Rows.Count > 0)
        {
            TextBox txtRequerimientosa_H = (TextBox)GridView1.HeaderRow.FindControl("txtRequerimientosa_H");
            TextBox txtSubf_Descripcion_H = (TextBox)GridView1.HeaderRow.FindControl("txtSubf_Descripcion_H");
            TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");

            Listar(txtRequerimientosa_H.Text.Trim(), txtSubf_Descripcion_H.Text.Trim(), txtPDC_H.Text.Trim());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            Listar("", "","");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupVerValorizarReporte(" + 1100 + "," + 500 + ");", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);

        //Response.Redirect("~/CAREMENOR/ValorizarReporte");
    }

    protected void btnCerrar_Click(object sender, ImageClickEventArgs e)
    {
        string cleanMessage;
        int cantidad = 0;
        int cantPendienteCal = 0;
        int cantProcesado = 0;
        int cantPendienteHES = 0;
        if (GridView1.Rows.Count > 0)
        {
            string Reqs_Correlativo;
            string Requ_Numero;
            string Reqd_CodLinea;
            string ide_valor, Proy_Codigo, FLG_FASES, D_Prov_RUC;
            foreach (GridViewRow row in GridView1.Rows)
            {

                Requ_Numero = GridView1.DataKeys[row.RowIndex].Values[0].ToString(); // extrae key
                Reqd_CodLinea = GridView1.DataKeys[row.RowIndex].Values[1].ToString(); // extrae key
                Reqs_Correlativo = GridView1.DataKeys[row.RowIndex].Values[2].ToString(); // extrae key
                ide_valor = GridView1.DataKeys[row.RowIndex].Values[3].ToString(); // extrae key
                Proy_Codigo = GridView1.DataKeys[row.RowIndex].Values[4].ToString(); // extrae key
                D_Prov_RUC = GridView1.DataKeys[row.RowIndex].Values[5].ToString(); // extrae key
                FLG_FASES = GridView1.DataKeys[row.RowIndex].Values[6].ToString(); // extrae key

                //0 pendiente de calculado
                //1 pendiente HES
                //2 PENDIENTE CIERRE
                //3 CERRADO

                

                if (FLG_FASES == "0")
                {
                    //0 pendiente de calculado
                    cantPendienteCal++;
                }

                else if (FLG_FASES == "1")
                {
                    //1 pendiente HES
                    cantPendienteHES++;
                }
                else if (FLG_FASES == "2")
                {
                    BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
                    DataTable dtResultado = new DataTable();

                    dtResultado = obj.Ins_valorizar_equipoMenor_cierre(
                        Convert.ToInt32(ddlanio.SelectedValue.ToString()),
                        Convert.ToInt32(ddlMes.SelectedValue.ToString()),
                        Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, Convert.ToInt32(ide_valor)

                        );
                    if (dtResultado.Rows.Count > 0)
                    {
                        cantidad++;
                    }
                }
                else if (FLG_FASES == "3")
                {
                    //1 pendiente HES
                    cantProcesado++;
                }
                

            }

            if(cantidad>0)
            {
                cleanMessage = "Se migraron " + cantidad.ToString() + " registro(s)";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                Listar("", "","");
            }
            else
            {
                if (cantPendienteCal > 0)
                {
                    cleanMessage = "Pendiente cálculo " + cantPendienteCal.ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else if (cantPendienteHES > 0)
                {
                    cleanMessage = "Pendiente registro HES " + cantPendienteCal.ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }
                else
                {
                    cleanMessage = "No existen registros a procesar";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                }


            }
        }
        else
        {
            cleanMessage = "No existen registros";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }

    protected void btnValorizar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CAREMENOR/ValorizarEquipoMenor");
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        filtrosExportar();
    }

    protected void filtrosExportar()
    {
        if (GridView1.Rows.Count > 0)
        {
            TextBox txtRequerimientosa_H = (TextBox)GridView1.HeaderRow.FindControl("txtRequerimientosa_H");
            TextBox txtSubf_Descripcion_H = (TextBox)GridView1.HeaderRow.FindControl("txtSubf_Descripcion_H");
            TextBox txtPDC_H = (TextBox)GridView1.HeaderRow.FindControl("txtPDC_H");

            ListarExportar(txtRequerimientosa_H.Text.Trim(), txtSubf_Descripcion_H.Text.Trim(), txtPDC_H.Text.Trim());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
        else
        {
            ListarExportar("", "","");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
        }
    }

    protected void ListarExportar(string requerimiento, string descripcion, string pdc)
    {



        BL_TBL_RequerimientoSubDetalle obj = new BL_TBL_RequerimientoSubDetalle();
        DataTable dtResultado = new DataTable();

        string proveedor = string.Empty;
        if (ddlProveedor.SelectedIndex == 0)
        {
            proveedor = string.Empty;
        }
        else
        {
            proveedor = ddlProveedor.SelectedValue.ToString();
        }

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

        dtResultado = obj.USP_SEL_TBL_VALORIZACION_EQUIPO_MENOR_V2(Centro, proveedor, ddlanio.SelectedValue.ToString(), ddlMes.SelectedValue.ToString(),pdc, requerimiento);
        if (dtResultado.Rows.Count > 0)
        {
          
            GridView2.DataSource = dtResultado;
            GridView2.DataBind();

            GridViewExportUtil.Export("VALORIZACION_" + DateTime.Now + ".xls", GridView2);
            return;
        }
        

      
    }

    protected void btnCuadro_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupVerResumen(" + 1000 + "," + 500 + ");", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
    protected void seleccionarMarca(object sender, EventArgs e)
    {

        LinkButton LinkButton1 = ((LinkButton)sender);
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        GridViewRow row = LinkButton1.NamingContainer as GridViewRow;
        //string pk = GridPEP.DataKeys[row.RowIndex].Values[0].ToString();


        string Reqs_ItemSecuencia = GridView1.DataKeys[grdrow.RowIndex].Values["Reqs_ItemSecuencia"].ToString();

     

        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popupMarca('" + Reqs_ItemSecuencia  + "'," + 900 + "," + 400 + ");", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "gridviewScroll();", true);
    }
}