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

public partial class RRHH_FormativoExamen : System.Web.UI.Page
{
    string FolderTrainee = ConfigurationManager.AppSettings["FolderTrainee"];
    string IDE_FASE, IDE_FICHA, IDE_EXAMEN, DNI_EVALUADO, IDE_PROGRAMA, CONTROL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        if (!Page.IsPostBack)
        {
            IDE_FASE = Session["IDE_FASE"].ToString ();
            IDE_FICHA = Session["IDE_FICHA"].ToString();
            IDE_EXAMEN = Session["IDE_EXAMEN"].ToString();
            BL_RRHH_FORMATIVO_EXAMEN Obj = new BL_RRHH_FORMATIVO_EXAMEN();
            DataTable dtResultado = new DataTable();
            DatosExamen();
            dtResultado = Obj.USP_EXAMEN_FORMATIVO(Convert.ToInt32(IDE_EXAMEN), Convert.ToInt32(IDE_FASE), Convert.ToInt32(IDE_FICHA));
            if (dtResultado.Rows.Count > 0)
            {
                lblCabcera.Text = dtResultado.Rows[0]["TITULO_EXAMEN"].ToString();
                string foto = dtResultado.Rows[0]["FOTO_EVALUADO"].ToString();
                lblNombre.Text = dtResultado.Rows[0]["EVALUADO"].ToString();
                lblintro.Text = dtResultado.Rows[0]["INTRODUCCION"].ToString();
                string CLASE_EXAMEN = dtResultado.Rows[0]["CLASE_EXAMEN"].ToString();
                Session["DNI_EVALUADO"] = dtResultado.Rows[0]["DNI_EVALUADO"].ToString();
                Session["IDE_PROGRAMA"]  = dtResultado.Rows[0]["IDE_PROGRAMA"].ToString();

                CONTROL = dtResultado.Rows[0]["CONTROL"].ToString();
                if (CONTROL =="1")
                {
                    btnGuardar.Visible = true;
                    btnEnviar.Visible = true;
                }
                else
                {
                    btnGuardar.Visible = false ;
                    btnEnviar.Visible = false;
                }

                if (CLASE_EXAMEN == "DESEMPENIO")
                {
                    ListarCartillaCAR("EVAL_DESEMPENIO", "COMPETENCIAS_CARDINALES");
                    ListarCartillaESP("EVAL_DESEMPENIO", "COMPETENCIAS_ESPECIFICAS");
                }
                else
                {
                    ListarCartillaCAR("EVAL_SEGUIMIENTO", "COMPETENCIAS_CARDINALES");
                    ListarCartillaESP("EVAL_SEGUIMIENTO", "COMPETENCIAS_ESPECIFICAS");
                }


                if (foto == string.Empty)
                {
                    imgFotos.ImageUrl = "~/imagenes/Foto_Fondo.png";
                }
                else
                {
                    imgFotos.ImageUrl = FolderTrainee + foto;
                }

            }
           
        }
    }
    protected void DatosExamen()
    {
        IDE_FASE = Session["IDE_FASE"].ToString();
        IDE_FICHA = Session["IDE_FICHA"].ToString();
        IDE_EXAMEN = Session["IDE_EXAMEN"].ToString();
        BL_RRHH_FORMATIVO_EXAM_CARTILLA Obj = new BL_RRHH_FORMATIVO_EXAM_CARTILLA();
        DataTable dtResultado = new DataTable();
        dtResultado = Obj.uspSEL_RRHH_FORMATIVO_EXAMEN_POR_ID(Convert.ToInt32(IDE_FICHA), Convert.ToInt32(IDE_FASE), Convert.ToInt32(IDE_EXAMEN));
        if (dtResultado.Rows.Count > 0)
        {
            lblCodigo.Text = dtResultado.Rows[0]["IDE_EVAL_EXAMEN"].ToString();
            txtFortalezas.Text = dtResultado.Rows[0]["FORTALEZAS"].ToString();
            txtoportunidades .Text = dtResultado.Rows[0]["MEJORAS"].ToString();
            txtCompromiso.Text = dtResultado.Rows[0]["COMPROMISOS"].ToString();
        }

        BL_RRHH_FORMATIVO_EXAMEN objEx = new BL_RRHH_FORMATIVO_EXAMEN();
        DataTable dtEx = new DataTable();
        dtEx = objEx.uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN(Convert.ToInt32 (lblCodigo.Text), Convert.ToInt32(Session["IDE_FICHA"].ToString()), Convert.ToInt32(Session["IDE_EXAMEN"].ToString()), Convert.ToInt32(Session["IDE_FASE"].ToString()));
      

        if (dtEx.Rows.Count > 0)
        {
            lblResultado.Text = "Resultado : " + dtEx.Rows[0]["PUNTAJE"].ToString() + " puntos.";
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void ListarCartillaCAR(string campo1, string campo2)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dt = new DataTable();
        dt = obj.ListarParametros_orden(campo1, campo2);
        if (dt.Rows.Count > 0)
        {
            ListView1.Visible = true;
            ListView1.DataSource = dt;
            ListView1.DataBind();
            OpcionesRespuestaCar();
        }
        else
        {
            ListView1.Visible = false;
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }
    protected void ListarCartillaESP(string campo1, string campo2)
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dt = new DataTable();
        dt = obj.ListarParametros_orden(campo1, campo2);
        if (dt.Rows.Count > 0)
        {
            ListView2.Visible = true;
            ListView2.DataSource = dt;
            ListView2.DataBind();
            OpcionesRespuestaEsp();
        }
        else
        {
            ListView2.Visible = false;
            ListView2.DataSource = dt;
            ListView2.DataBind();
        }

    }
    private void OpcionesRespuestaCar()
    {
        try
        {
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dt = new DataTable();
            dt = obj.ListarParametros_orden("RRHH_FORMATIVO_EXAM_CARTILLA", "NIVEL_EXIGENCIA");
            //int Cod = 0;

            BL_RRHH_FORMATIVO_EXAM_CARTILLA objEx = new BL_RRHH_FORMATIVO_EXAM_CARTILLA();
            DataTable dtEx = new DataTable();

            foreach (ListViewItem FilaFactor in ListView1.Items)
            {
                RadioButtonList RadioButtonList1 = ((RadioButtonList)FilaFactor.FindControl("RadioButtonList1"));

                RadioButtonList1.DataSource = dt;
                RadioButtonList1.DataTextField = "RESUMEN_ORDEN";
                RadioButtonList1.DataValueField = "ID_PARAMETRO";
                RadioButtonList1.DataBind();

               
                string codePregunta = ListView1.DataKeys[FilaFactor.DisplayIndex].Value.ToString();

               
                dtEx = objEx.uspSEL_RESPUESTA_EXAM_CARTILLA(Convert.ToInt32(codePregunta), Session["IDE_USUARIO"].ToString (), Session["DNI_EVALUADO"].ToString (), Convert.ToInt32(lblCodigo.Text ), Convert.ToInt32(Session["IDE_PROGRAMA"].ToString ()));
                if (dtEx.Rows.Count > 0)
                {
                    RadioButtonList1.SelectedValue = dtEx.Rows[0]["IDE_NOTA"].ToString();
                }
            }


        }
        catch (Exception ex)
        {
            UC_MessageBox.Show(Page, Page.GetType(), "Ocurrio un error al Consultar :" + ex.Message);
            return;
        }

    }

    private void OpcionesRespuestaEsp()
    {
        try
        {
            BL_PERSONAL obj = new BL_PERSONAL();
            DataTable dt = new DataTable();
            dt = obj.ListarParametros_orden("RRHH_FORMATIVO_EXAM_CARTILLA", "NIVEL_EXIGENCIA");

            BL_RRHH_FORMATIVO_EXAM_CARTILLA objEx = new BL_RRHH_FORMATIVO_EXAM_CARTILLA();
            DataTable dtEx = new DataTable();

            foreach (ListViewItem FilaFactor in ListView2.Items)
            {
                RadioButtonList RadioButtonList2 = ((RadioButtonList)FilaFactor.FindControl("RadioButtonList2"));

                RadioButtonList2.DataSource = dt;
                RadioButtonList2.DataTextField = "RESUMEN_ORDEN";
                RadioButtonList2.DataValueField = "ID_PARAMETRO";
                RadioButtonList2.DataBind();

                string codePregunta = ListView2.DataKeys[FilaFactor.DisplayIndex].Value.ToString();


                dtEx = objEx.uspSEL_RESPUESTA_EXAM_CARTILLA(Convert.ToInt32(codePregunta), Session["IDE_USUARIO"].ToString(), Session["DNI_EVALUADO"].ToString(), Convert.ToInt32(lblCodigo.Text), Convert.ToInt32(Session["IDE_PROGRAMA"].ToString()));
                if (dtEx.Rows.Count > 0)
                {
                    RadioButtonList2.SelectedValue = dtEx.Rows[0]["IDE_NOTA"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            UC_MessageBox.Show(Page, Page.GetType(), "Ocurrio un error al Consultar :" + ex.Message);
            return;
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;

        BE_RRHH_FORMATIVO_EXAMEN obj = new BE_RRHH_FORMATIVO_EXAMEN();
        obj.IDE_EVAL_EXAMEN = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        obj.IDE_FICHA = Convert.ToInt32(Session["IDE_FICHA"].ToString());
        obj.DNI_EVALUADOR = Session["IDE_USUARIO"].ToString();
        obj.DNI_EVALUADO = Session["DNI_EVALUADO"].ToString(); 
        obj.FORTALEZAS = txtFortalezas.Text.Trim();
        obj.MEJORAS = txtoportunidades.Text.Trim();
        obj.COMPROMISOS = txtCompromiso.Text.Trim();
        obj.IDE_TIPO_EXA = Convert.ToInt32(Session["IDE_EXAMEN"].ToString());
        obj.IDE_FASE = Convert.ToInt32(Session["IDE_FASE"].ToString());
        int rpta = 0;
        rpta = new BL_RRHH_FORMATIVO_EXAMEN().uspINS_RRHH_FORMATIVO_EXAMEN(obj);
        if (rpta > 0)
        {
            lblCodigo.Text = rpta.ToString();
            registrarCompetencias(rpta);
            registrarEspecificas(rpta);


            BL_RRHH_FORMATIVO_EXAMEN objEx = new BL_RRHH_FORMATIVO_EXAMEN();
            DataTable dtEx = new DataTable();
            dtEx = objEx.uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN(rpta, Convert.ToInt32(Session["IDE_FICHA"].ToString()), Convert.ToInt32(Session["IDE_EXAMEN"].ToString()), Convert.ToInt32(Session["IDE_FASE"].ToString()));
            cleanMessage = "Registro satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);

            if (dtEx.Rows.Count > 0)
            {
                lblResultado.Text =  "Resultado : " + dtEx.Rows[0]["PUNTAJE"].ToString() + " puntos.";
            }
             

        }
    }
    protected void registrarCompetencias(int IDE_EVAL_EXAMEN)
    {
        try
        {

            int Cod;
            for (int i = 0; i < ListView1.Items.Count; i++) // aqui se recorre toda la lista
            {

                ListViewItem Fila = ListView1.Items[i];
                RadioButtonList rb = (RadioButtonList)Fila.FindControl("RadioButtonList1"); // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check

                if (rb.SelectedValue != string.Empty)
                {
                    Cod = Convert.ToInt32(ListView1.DataKeys[i].Values[0].ToString()); // extraer key

                    string cleanMessage = string.Empty;

                    BE_RRHH_FORMATIVO_EXAM_CARTILLA obj = new BE_RRHH_FORMATIVO_EXAM_CARTILLA();
                    obj.IDE_EXAMEN = 0;
                    obj.IDE_PREGUNTA = Cod;
                    obj.IDE_NOTA = Convert.ToInt32(rb.SelectedValue);
                    obj.EVALUADOR = Session["IDE_USUARIO"].ToString();
                    obj.EVALUADO = Session["DNI_EVALUADO"].ToString ();
                    obj.IDE_EVAL_EXAMEN = IDE_EVAL_EXAMEN;
                    obj.IDE_PROGRAMA = Convert.ToInt32(Session["IDE_PROGRAMA"]);
                   
                    int rpta = 0;
                  
                    rpta = new BL_RRHH_FORMATIVO_EXAM_CARTILLA().uspINS_RRHH_FORMATIVO_EXAM_CARTILLA(obj);
                    if (rpta > 0)
                    {

                        cleanMessage = "Registro satisfactorio";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                    }

                }
            }

        }
        catch (Exception ex)
        {
            UC_MessageBox.Show(Page, Page.GetType(), "Ocurrio un error al Consultar :" + ex.Message);
            return;
        }
    }
    protected void registrarEspecificas(int IDE_EVAL_EXAMEN)
    {
        try
        {

            int Cod;
            for (int i = 0; i < ListView2.Items.Count; i++) // aqui se recorre toda la lista
            {

                ListViewItem Fila = ListView2.Items[i];
                RadioButtonList rb = (RadioButtonList)Fila.FindControl("RadioButtonList2"); // aqui esta elnombre de tu control en la lista y tienes q cambiar RadioButtonList x tu check

                if (rb.SelectedValue != string.Empty)
                {
                    Cod = Convert.ToInt32(ListView2.DataKeys[i].Values[0].ToString()); // extraer key

                    string cleanMessage = string.Empty;

                    BE_RRHH_FORMATIVO_EXAM_CARTILLA obj = new BE_RRHH_FORMATIVO_EXAM_CARTILLA();
                    obj.IDE_EXAMEN = 0;
                    obj.IDE_PREGUNTA = Cod;
                    obj.IDE_NOTA = Convert.ToInt32(rb.SelectedValue);
                    obj.EVALUADOR = Session["IDE_USUARIO"].ToString();
                    obj.EVALUADO = Session["DNI_EVALUADO"].ToString();
                    obj.IDE_EVAL_EXAMEN = IDE_EVAL_EXAMEN;
                    obj.IDE_PROGRAMA = Convert.ToInt32(Session["IDE_PROGRAMA"]);

                    int rpta = 0;

                    rpta = new BL_RRHH_FORMATIVO_EXAM_CARTILLA().uspINS_RRHH_FORMATIVO_EXAM_CARTILLA(obj);
                    

                }
            }

        }
        catch (Exception ex)
        {
            UC_MessageBox.Show(Page, Page.GetType(), "Ocurrio un error al Consultar :" + ex.Message);
            return;
        }
    }
                        
    protected void btnEnviar_Click(object sender, EventArgs e)
    {

        string cleanMessage = string.Empty;

        BE_RRHH_FORMATIVO_EXAMEN obj = new BE_RRHH_FORMATIVO_EXAMEN();
        obj.IDE_EVAL_EXAMEN = Convert.ToInt32(string.IsNullOrEmpty(lblCodigo.Text) ? "0" : lblCodigo.Text);
        obj.IDE_FICHA = Convert.ToInt32(Session["IDE_FICHA"].ToString());
        obj.DNI_EVALUADOR = Session["IDE_USUARIO"].ToString();
        obj.DNI_EVALUADO = Session["DNI_EVALUADO"].ToString();
        obj.FORTALEZAS = txtFortalezas.Text.Trim();
        obj.MEJORAS = txtoportunidades.Text.Trim();
        obj.COMPROMISOS = txtCompromiso.Text.Trim();
        obj.IDE_TIPO_EXA = Convert.ToInt32(Session["IDE_EXAMEN"].ToString());
        obj.IDE_FASE = Convert.ToInt32(Session["IDE_FASE"].ToString());
        int rpta = 0;
        rpta = new BL_RRHH_FORMATIVO_EXAMEN().uspINS_RRHH_FORMATIVO_EXAMEN(obj);
        if (rpta > 0)
        {
            lblCodigo.Text = rpta.ToString();
            registrarCompetencias(rpta);
            registrarEspecificas(rpta);


            BL_RRHH_FORMATIVO_EXAMEN objEx = new BL_RRHH_FORMATIVO_EXAMEN();
            DataTable dtEx = new DataTable();
            dtEx = objEx.uspSUMA_PTOS_EXA_FORMATIVO_EXAMEN(rpta, Convert.ToInt32(Session["IDE_FICHA"].ToString()), Convert.ToInt32(Session["IDE_EXAMEN"].ToString()), Convert.ToInt32(Session["IDE_FASE"].ToString()));


            BL_RRHH_FORMATIVO_EXAMEN objX = new BL_RRHH_FORMATIVO_EXAMEN();
            DataTable dt = new DataTable();
            dt = objX.USP_CORREO_EXAMEN_EJECUTADO(lblCabcera.Text.Trim(), Session["IDE_USUARIO"].ToString());
            if (dt.Rows.Count > 0)
            {

                cleanMessage = "Envio satisfactorio";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }

        }

        
    }
}