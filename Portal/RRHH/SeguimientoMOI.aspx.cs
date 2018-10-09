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
public partial class RRHH_SeguimientoMOI : System.Web.UI.Page
{
    public string ControlUsuario;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString());

    //String fec_ini = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
    String fec_ini = "01/01/" + DateTime.Now.ToString("yyyy");
    String fec_fin = DateTime.Now.ToString("dd/MM/yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = "";
        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDescarga);
        if (Session["IDE_USUARIO"] == null)
        {
            id = (Request.QueryString["id"]);
        }
        else
        {
            id = Session["IDE_USUARIO"].ToString();
        }
        //ControlUsuario = Session["ControlUsuario"].ToString();
        if (!Page.IsPostBack)
        {

            ControlBotones();
            CentroCostosB(id);
            //Anio();
            Analistas();
            Estado();
            cargo();
            tipoMano(id);
            fechaAprobacion();
            txtInicio.Text = fec_ini;
            txtFin.Text = fec_fin;
            chkEstados.Checked = true;
            chkTodosCECO.Checked = true;
            chkManoObra.Checked = true;
            chkReclutador.Checked = true;
            chkCargo.Checked = true;
            chkAprobacion.Checked = true;
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

    protected void btnPersonal_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmPersonal.aspx");
    }
    protected void btnControl_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmMOI.aspx");
    }
    protected void btnReportes_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmReporteMOI.aspx");
    }
    protected void btnSeguimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/SeguimientoReporteMOI.aspx");
    }

    protected void btnRequerimiento_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/RRHH/frmRequerimiento.aspx");
    }

    protected void Estado()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlEstados.DataSource = obj.ListarParametros("ID_ESTADO_PROCESO", "TMP_DET_REQ_PER");
        ddlEstados.DataTextField = "DES_ASUNTO";
        ddlEstados.DataValueField = "IN_ORDEN";
        ddlEstados.DataBind();
        //this.ddlEstados.Items.Insert(0, new ListItem("--------TODOS--------", "0")); 
        foreach (ListItem item in ddlEstados.Items)
        {
            item.Selected = true;

        }

    }
    protected void btnConsulta_Click(object sender, EventArgs e)
    {
        Consultar();
    }
    protected void Consultar()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        String danalistas = string.Empty;
        String destados = string.Empty;
        String dcargos = string.Empty;
        String dceco = string.Empty;
        String dmano = string.Empty;
        String dfecha = string.Empty;
        //lblSplit.Text = string.Empty;

        if (ddlAnalista.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlAnalista.Items)
            {
                if (li.Selected)
                {

                    danalistas += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = danalistas;

        }

        if (ddlAprobacion.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlAprobacion.Items)
            {
                if (li.Selected)
                {

                    dfecha += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = danalistas;

        }

        if (ddlCargo.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlCargo.Items)
            {
                if (li.Selected)
                {

                    dcargos += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = dcargos;

        }

        if (ddlEstados.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlEstados.Items)
            {
                if (li.Selected)
                {

                    destados += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = destados;

        }

        if (ddlCecoB.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlCecoB.Items)
            {
                if (li.Selected)
                {

                    dceco += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = dceco;

        }


        if (ddlMano.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlMano.Items)
            {
                if (li.Selected)
                {

                    dmano += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = dceco;

        }

        //else
        //{
        //    UC_MessageBox.Show(Page, this.GetType(), "Debe Seleccionar algun Status");
        //    return;
        //}

        //lblSplit.Text = danalistas;

        dtResultado = obj.ConsultarControlReporte_MOI(txtInicio.Text, txtFin.Text, danalistas, dcargos, dceco, destados, dmano, dfecha);

        if (dtResultado.Rows.Count > 0)
        {

            GridViewResultados.DataSource = dtResultado;
            GridViewResultados.DataBind();
            btnDescarga.Visible = true;

        }
        else
        {
            GridViewResultados.DataSource = null;
            GridViewResultados.DataBind();
            btnDescarga.Visible = false;
        }
    }
    protected void exportarXLS()
    {

        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultadoeExcel = new DataTable();

        String danalistas = string.Empty;
        String destados = string.Empty;
        String dcargos = string.Empty;
        String dceco = string.Empty;
        String dmano = string.Empty;
        //lblSplit.Text = string.Empty;


        if (ddlAnalista.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlAnalista.Items)
            {
                if (li.Selected)
                {

                    danalistas += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = danalistas;

        }

        if (ddlCargo.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlCargo.Items)
            {
                if (li.Selected)
                {

                    dcargos += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = dcargos;

        }

        if (ddlEstados.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlEstados.Items)
            {
                if (li.Selected)
                {

                    destados += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = destados;

        }

        if (ddlCecoB.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlCecoB.Items)
            {
                if (li.Selected)
                {

                    dceco += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = dceco;

        }


        if (ddlMano.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlMano.Items)
            {
                if (li.Selected)
                {

                    dmano += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = dceco;

        }

        //else
        //{
        //    UC_MessageBox.Show(Page, this.GetType(), "Debe Seleccionar algun Status");
        //    return;
        //}

        //lblSplit.Text = danalistas;

        string dfecha = "";
        if (ddlAprobacion.SelectedIndex != -1)
        {
            foreach (ListItem li in ddlAprobacion.Items)
            {
                if (li.Selected)
                {

                    dfecha += li.Value + ",";
                }
                else
                {

                }

            }
            //lblSplit.Text = danalistas;

        }
        dtResultadoeExcel = obj.ConsultarControlReporte_MOI(txtInicio.Text, txtFin.Text, danalistas, dcargos, dceco, destados, dmano, dfecha);

        if (dtResultadoeExcel.Rows.Count > 0)
        {

            //ExcelHelper.ToExcel(dtResultadoeExcel, "CJI3_" + ddlEstados.SelectedItem + ".xls", Page.Response);

            gvExcel.DataSource = dtResultadoeExcel;
            gvExcel.DataBind();



            GridViewExportUtil.Export("Seguimiento_" + ddlEstados.SelectedItem + ".xls", gvExcel);
            return;

        }
        else
        {


        }


    }
    protected void btnDescarga_Click(object sender, EventArgs e)
    {
        exportarXLS();
    }
    protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
    {
        Consultar();
    }

    protected void CentroCostosB(String id)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.Listar_centro_Costos_Correo(id);
        if (dtResultado.Rows.Count > 0)
        {
            ddlCecoB.DataSource = dtResultado;
            ddlCecoB.DataTextField = "DES_CCOSTO";
            ddlCecoB.DataValueField = "ID_CENTROCOSTO";
            ddlCecoB.DataBind();
            //this.ddlCecoB.Items.Insert(0, new ListItem("(Seleccionar Todo)", "0"));
        }

        foreach (ListItem item in ddlCecoB.Items)
        {
            item.Selected = true;

        }

    }


    protected void GridViewResultado1_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cargo()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCargo.DataSource = obj.ListarParametros("IDE_CARGO", "RRHH_MOI");
        ddlCargo.DataTextField = "DES_ASUNTO";
        ddlCargo.DataValueField = "ID_PARAMETRO";
        ddlCargo.DataBind();

        foreach (ListItem item in ddlCargo.Items)
        {
            item.Selected = true;

        }

    }

    protected void Analistas()
    {
        try
        {
            con.Open();
            //string query = "SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOI] R WHERE  YEAR([FEC_FECHA_APROBACION]) =" + ddlAnio.SelectedValue;
            string query = "SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOI] R WHERE DES_RESPONSABLE IS NOT NULL UNION ALL SELECT DISTINCT [DES_RESPONSABLE] , (SELECT UPPER(U.NOMBRE_USUARIO) FROM dbo.TBUSUARIO U WHERE U.IDE_USUARIO = R.[DES_RESPONSABLE]) RESPONSABLE FROM [RRHH_MOD] R WHERE DES_RESPONSABLE IS NOT NULL";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            con.Close();
            if (t1.Rows.Count > 0)
            {
                ddlAnalista.DataSource = t1;
                ddlAnalista.DataTextField = "RESPONSABLE";
                ddlAnalista.DataValueField = "DES_RESPONSABLE";
                ddlAnalista.DataBind();
                foreach (ListItem item in ddlAnalista.Items)
                {
                    item.Selected = true;

                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void tipoMano(string id)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlMano.DataSource = obj.ListarManoObraID(id);
        ddlMano.DataTextField = "DES_MANO_OBRA";
        ddlMano.DataValueField = "DES_MANO_OBRA";
        ddlMano.DataBind();

        foreach (ListItem item in ddlMano.Items)
        {
            item.Selected = true;

        }

    }

    protected void fechaAprobacion()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlAprobacion.DataSource = obj.ListarParametros("IDE_FECHA", "RRHH_MOI");
        ddlAprobacion.DataTextField = "DES_ASUNTO";
        ddlAprobacion.DataValueField = "ID_PARAMETRO";
        ddlAprobacion.DataBind();

        foreach (ListItem item in ddlAprobacion.Items)
        {
            item.Selected = true;

        }

    }


    protected void gvExcel_DataBound(object sender, EventArgs e)
    {


    }

    protected void gvExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
        e.Row.Cells[18].BackColor = System.Drawing.Color.Yellow;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text != "0")
            {
                for (int i = 0; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.Beige;
                }
            }
        }
    }

    protected void chkTodosCECO_CheckedChanged(object sender, EventArgs e)
    {

        if (chkTodosCECO.Checked == true)
        {
            foreach (ListItem item in ddlCecoB.Items)
            {
                item.Selected = true;

            }

        }

        if (chkTodosCECO.Checked == false)
        {
            foreach (ListItem item in ddlCecoB.Items)
            {
                item.Selected = false;

            }
        }

    }

    protected void chkEstados_CheckedChanged(object sender, EventArgs e)
    {

        if (chkEstados.Checked == true)
        {
            foreach (ListItem item in ddlEstados.Items)
            {
                item.Selected = true;

            }

        }

        if (chkEstados.Checked == false)
        {
            foreach (ListItem item in ddlEstados.Items)
            {
                item.Selected = false;

            }
        }

    }


    protected void chkReclutador_CheckedChanged(object sender, EventArgs e)
    {
        if (chkReclutador.Checked == true)
        {
            foreach (ListItem item in ddlAnalista.Items)
            {
                item.Selected = true;

            }

        }

        if (chkReclutador.Checked == false)
        {
            foreach (ListItem item in ddlAnalista.Items)
            {
                item.Selected = false;

            }
        }
    }
    protected void chkManoObra_CheckedChanged(object sender, EventArgs e)
    {

        if (chkManoObra.Checked == true)
        {
            foreach (ListItem item in ddlMano.Items)
            {
                item.Selected = true;

            }

        }

        if (chkManoObra.Checked == false)
        {
            foreach (ListItem item in ddlMano.Items)
            {
                item.Selected = false;

            }
        }

    }
    protected void chkCargo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCargo.Checked == true)
        {
            foreach (ListItem item in ddlCargo.Items)
            {
                item.Selected = true;

            }

        }

        if (chkCargo.Checked == false)
        {
            foreach (ListItem item in ddlCargo.Items)
            {
                item.Selected = false;

            }
        }

    }

    protected void chkAprobacion_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAprobacion.Checked == true)
        {
            foreach (ListItem item in ddlAprobacion.Items)
            {
                item.Selected = true;

            }

        }

        if (chkAprobacion.Checked == false)
        {
            foreach (ListItem item in ddlAprobacion.Items)
            {
                item.Selected = false;

            }
        }

    }
}