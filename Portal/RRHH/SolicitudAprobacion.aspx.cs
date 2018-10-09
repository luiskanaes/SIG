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


public partial class RRHH_SolicitudAprobacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            Empresas();
            cargos();
            AcademicoNivel();
            ParametrosCarrera();
            hdcodigo.Value = Session["IDE_ASIGNACION"].ToString();
            if (Session["IDE_ASIGNACION"] != null)
            {
                hdcodigo.Value = Session["IDE_ASIGNACION"].ToString();
                if (hdcodigo.Value != string.Empty)
                {
                    Datos(hdcodigo.Value);
                }

            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/SiteMovil.master";
    }
    protected void AcademicoNivel()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlNivelAcademico.DataSource = obj.ListarParametros_orden("NIVEL", "RRHH_FORMATIVO_FICHA");
        ddlNivelAcademico.DataTextField = "DES_ASUNTO";
        ddlNivelAcademico.DataValueField = "ID_PARAMETRO";
        ddlNivelAcademico.DataBind();

        ddlNivelAcademico.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
    }
    protected void ParametrosCarrera()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlcarrera.DataSource = obj.ListarParametros_orden("CARRERA", "RRHH_FORMATIVO_FICHA");
        ddlcarrera.DataTextField = "DES_ASUNTO";
        ddlcarrera.DataValueField = "ID_PARAMETRO";
        ddlcarrera.DataBind();

        ddlcarrera.Items.Insert(0, new ListItem("--- Seleccionar carrera---", ""));
    }
    protected void cargos()
    {
        BL_PERSONAL obj = new BL_PERSONAL();
        DataTable dtResultado = new DataTable();

        ddlCargos.DataSource = obj.ListarParametros("IDE_CARGO", "RRHH_MOI");
        ddlCargos.DataTextField = "DES_ASUNTO";
        ddlCargos.DataValueField = "ID_PARAMETRO";
        ddlCargos.DataBind();
        this.ddlCargos.Items.Insert(0, new ListItem("--- Seleccionar ---", "0"));

    }
    protected void Empresas()
    {
        BL_RO obj = new BL_RO();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.listar_empresa();

        if (dtResultado.Rows.Count > 0)
        {
            ddlEmpresas.DataSource = dtResultado;
            ddlEmpresas.DataTextField = dtResultado.Columns["DES_ABREV"].ToString();
            ddlEmpresas.DataValueField = dtResultado.Columns["ID_EMPRESA"].ToString();
            ddlEmpresas.DataBind();

            gerencias();
        }
    }
    protected void gerencias()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(1, "", 1);
        dtResultado = obj.uspLISTAR_GERENCIA(Convert.ToInt32(ddlEmpresas.SelectedValue));
        if (dtResultado.Rows.Count > 0)
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerencia.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerencia.DataBind();
            centros();

        }
        else
        {
            ddlGerencia.DataSource = dtResultado;
            ddlGerencia.DataBind();

            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        //dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(2, ddlGerencia.SelectedValue.ToString(), 1);
        dtResultado = obj.uspLISTAR_GERENCIA_X_CENTROS(ddlGerencia.SelectedValue.ToString(), 1);

        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlCentro.DataBind();

        }
        else
        {
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
        }

    }
    protected void Datos(string IDE_ASIGNACION)
    {
        BL_RRHH_SOLICITUD_ASIGNACION obj = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado = new DataTable();

        dtResultado = obj.uspSEL_RRHH_SOLICITUD_ASIGNACION_ID(IDE_ASIGNACION);
        if (dtResultado.Rows.Count > 0)
        {
            hdcodigo.Value = dtResultado.Rows[0]["IDE_ASIGNACION"].ToString();
            string IDE_POSTULANTE = dtResultado.Rows[0]["IDE_POSTULANTE"].ToString();
            hdPersonal.Value = dtResultado.Rows[0]["IDE_POSTULANTE"].ToString();


            txtDni.Text = dtResultado.Rows[0]["DNI_TMP"].ToString();
            txtNombre.Text = dtResultado.Rows[0]["NOMBRE_TMP"].ToString();
            txtPaterno.Text = dtResultado.Rows[0]["APE_PAT_TMP"].ToString();
            txtMaterno.Text = dtResultado.Rows[0]["APE_MAT_TMP"].ToString();

            string ID_EMPRESA = dtResultado.Rows[0]["ID_EMPRESA"].ToString();
            if (ID_EMPRESA != string.Empty)
            {
                ddlEmpresas.SelectedValue = ID_EMPRESA;
            }

            ddlGerencia.SelectedValue = dtResultado.Rows[0]["IDE_GERENCIA"].ToString();
            centros();
            ddlCentro.SelectedValue = dtResultado.Rows[0]["IDE_CENTRO"].ToString();
            ddlCargos.SelectedValue = dtResultado.Rows[0]["IDE_CARGO"].ToString();
            txtarea.Text = dtResultado.Rows[0]["AREA"].ToString();





            ddlNivelAcademico.SelectedValue = dtResultado.Rows[0]["IDE_NIVEL_ACADEMICO"].ToString();
            ddlcarrera.SelectedValue = dtResultado.Rows[0]["IDE_CARRERA"].ToString();

            txtColegiatura.Text = dtResultado.Rows[0]["NRO_COLEGIATURA"].ToString();


            Boolean FLG_COLEGIATURA = Convert.ToBoolean(dtResultado.Rows[0]["FLG_COLEGIATURA"].ToString());
            if (FLG_COLEGIATURA == false)
            {
                RdoColegiatura.SelectedIndex = 0;
            }
            else
            {
                RdoColegiatura.SelectedIndex = 1;
            }




            txtinicio.Text = dtResultado.Rows[0]["INICIO_CONTRATO"].ToString();



            txtComentarioGnral.Text = dtResultado.Rows[0]["COMENTARIOS_GNRAL"].ToString();

            int FLG_URL = Convert.ToInt32(dtResultado.Rows[0]["FLG_URL"].ToString());
            if (FLG_URL == 1)
            {
                HyperLink1.Visible = true;
                HyperLink1.NavigateUrl = dtResultado.Rows[0]["FILE_URL"].ToString();
                HyperLink1.Text = "Descargar solicitud";
            }
            else
            {
                HyperLink1.Visible = false;
            }

            BL_RRHH_SOLICITUD_ASIGNACION obj_ = new BL_RRHH_SOLICITUD_ASIGNACION();
            DataTable dtResultado_ = new DataTable();
            dtResultado_ = obj_.uspSEL_LISTAR_RECURSOS_SOLMOBILE(hdcodigo.Value, "RECURSOS MOVIL");
            if (dtResultado_.Rows.Count > 0)
            {
                GridView1.DataSource = dtResultado_;
                GridView1.DataBind();
            }
        }
    }
    protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerencias();
    }
    protected void ddlGerencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        centros();
    }
    protected void ProcesarReconocimiento(object sender, ImageClickEventArgs e)
    {
        string cleanMessage = string.Empty;
        ImageButton btnProcesar = ((ImageButton)sender);
        GridViewRow row = btnProcesar.NamingContainer as GridViewRow;
        string pk = GridView1.DataKeys[row.RowIndex].Values["IDE_ASIGNACION"].ToString();
        string IDE_RECURSOS = GridView1.DataKeys[row.RowIndex].Values["IDE_RECURSOS"].ToString();
        RadioButtonList rb = (RadioButtonList)row.FindControl("rdoOpcion");

        if(rb.SelectedValue != string.Empty )
        {
            BL_RRHH_SOLICITUD_ASIGNACION obj_ = new BL_RRHH_SOLICITUD_ASIGNACION();
            DataTable dtResultado_ = new DataTable();
            dtResultado_ = obj_.uspSEL_APROBAR_RECURSOS_SOLMOBILE(hdcodigo.Value, IDE_RECURSOS, Session["IDE_USUARIO"].ToString(), rb.SelectedValue.ToString());

            string estado = rb.SelectedValue;
            if(estado=="0")
            {
                estado = "7";// codigo de anulado en mobile
            }

           

            EnviarMobile(IDE_RECURSOS, estado);

            Datos(hdcodigo.Value);
            cleanMessage = "Envio satisfactorio";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
           
        }
        else
        {
            cleanMessage = "Seleccion estado de atención";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void EnviarMobile(string IDE_RECURSOS, string  IdEstadoRequerimiento)
    {
        BL_RRHH_SOLICITUD_ASIGNACION obj_ = new BL_RRHH_SOLICITUD_ASIGNACION();
        DataTable dtResultado_ = new DataTable();

        //REVISAMOS SI HAY RECURSOS PARA EL MOBILE
        dtResultado_ = obj_.uspSEL_LISTAR_RECURSOS_SOLMOBILE_ITEM(hdcodigo.Value, "RECURSOS MOVIL", IDE_RECURSOS);
        if (dtResultado_.Rows.Count > 0)
        {
            string personal = txtPaterno.Text.Trim() + ' ' + txtMaterno.Text.Trim() + ' ' + txtNombre.Text.Trim();
            string CODIGO_CARE_PADRE = dtResultado_.Rows[0]["CODIGO_CARE_PADRE"].ToString();
            string IdTrabajador = string.Empty;
            //OBTENER DNI DEL PERSONAL MOBILE

            BL_MOBILE objPer = new BL_MOBILE();
            DataTable dtPers = new DataTable();
            if (txtDni.Text.Trim() != string.Empty)
            {
                dtPers = objPer.usp_Trabajador_x_dni(txtDni.Text.Trim(), txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), txtNombre.Text.Trim());

            }
            

            if (dtPers.Rows.Count > 0)
            {
                IdTrabajador = dtPers.Rows[0]["IdTrabajador"].ToString();
            }
            //////// FIN MOBILE PERSONA //////////////////

            ///insertamos los recursos al care y mobile
            for (int i = 0; i < dtResultado_.Rows.Count; i++)
            {

                string CODIGO_EQUIPO = dtResultado_.Rows[i]["CODIGO_EXT"].ToString();//codigo del recurso en mobile
                string FILE_URL = dtResultado_.Rows[i]["FILE_URL"].ToString();
                string TICKET = dtResultado_.Rows[i]["TICKET"].ToString();
                string TIPO_EQUIPO = string.Empty;
                string CODIGO_CARE = string.Empty;
                //ENVIAR RECURSOS CARE 

                BE_RequerimientoMovil Obj = new BE_RequerimientoMovil();
                Obj.IdRequerimiento = 0;
                Obj.FechaSolicitud = txtinicio.Text;
                Obj.IdEmpresaPK = Convert.ToInt32(ddlEmpresas.SelectedValue);//analizar
                Obj.centro_costo = ddlCentro.SelectedValue;
                Obj.Requ_Numero = CODIGO_CARE_PADRE;
                DataTable dtrpta = new DataTable();

                dtrpta = new BL_RRHH_SOLICITUD_ASIGNACION().uspINS_RequerimientoMovil(Obj);
                if (dtrpta.Rows.Count > 0)
                {
                    BE_RequerimientoMovil_Detalle ObjD = new BE_RequerimientoMovil_Detalle();

                    if (CODIGO_EQUIPO == "1")
                    {
                        TIPO_EQUIPO = "CELULAR";
                    }
                    else
                    {
                        TIPO_EQUIPO = "MODEM";
                    }
                    ObjD.id_detalle = 0;
                    ObjD.NombreSolicitante = personal;
                    ObjD.FechaRequerida = txtinicio.Text;
                    ObjD.MesesRequerido = 12;
                    ObjD.LugarEntrega = "Lima";
                    ObjD.IdTipoEquipo = Convert.ToInt32(CODIGO_EQUIPO);
                    ObjD.IdRequerimiento = Convert.ToInt32(dtrpta.Rows[0]["IdRequerimiento"].ToString());
                    ObjD.Dni_Trabajador = txtDni.Text;
                    ObjD.cantidad = Convert.ToInt32(1);
                    ObjD.USER_CREACION = "RECURSOS HUMANOS";
                    ObjD.IdTrabajador = Convert.ToInt32(IdTrabajador);
                    ObjD.TipoEquipo = TIPO_EQUIPO;
                    ObjD.IdOperadorMovil = 1;
                    ObjD.Operador = "CLARO";

                    DataTable dtrpta_detalle = new DataTable();
                    dtrpta_detalle = new BL_RRHH_SOLICITUD_ASIGNACION().uspINS_RequerimientoMovil_Detalle_SIG(ObjD);
                    if (dtrpta_detalle.Rows.Count > 0)
                    {
                        CODIGO_CARE = dtrpta_detalle.Rows[0]["GUID_CODIGO"].ToString();
                    }
                }

                //MOBILE
                BL_MOBILE Mobj = new BL_MOBILE();
                DataTable MdtResultado = new DataTable();


                MdtResultado = Mobj.usp_CARE_InsertarSolicitud_Generico_Aprobador
                    (
                     DateTime.Today.ToString("dd/MM/yyyy"),
                     ddlEmpresas.SelectedValue,
                     txtinicio.Text,
                     "Lima",
                     personal,
                     BL_Session.UsuarioNombre,
                     CODIGO_CARE,
                     CODIGO_EQUIPO,
                     CODIGO_CARE_PADRE,
                     FILE_URL, TICKET, IdEstadoRequerimiento, BL_Session.UsuarioNombre
                    );

                //UNION CODIGO CARE-MOBILE
                DataTable dtrpta_Mob_care = new DataTable();
                dtrpta_Mob_care = new BL_RRHH_SOLICITUD_ASIGNACION().uspSEL_REQUERIMIENTOMOVIL_UPDATE_MOBILE(dtrpta.Rows[0]["IdRequerimiento"].ToString());
            }

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButtonList rblShippers = (RadioButtonList)e.Row.FindControl("rdoOpcion");
            Label lbl = (Label)e.Row.FindControl("lblEstado");

            if(lbl.Text=="False")
            {
                lbl.Text = "0";
            }
            else if (lbl.Text == "True")
            {
                lbl.Text = "1";
            }

            if (lbl.Text != string.Empty )
            {
                rblShippers.Items.FindByValue((e.Row.FindControl("lblEstado") as Label).Text).Selected = true;
            }
        }
    }
}
