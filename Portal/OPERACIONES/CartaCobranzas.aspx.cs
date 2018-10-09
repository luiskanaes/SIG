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


public partial class OPERACIONES_CartaCobranzas : System.Web.UI.Page
{
    DataTable dtPersonalOrigen= new DataTable();
    DataTable dtPersonalDestino = new DataTable();
    string FolderCarta = ConfigurationManager.AppSettings["FolderCarta"];
    string FolderCartaBackups = ConfigurationManager.AppSettings["FolderCartaBackups"];
    string URLSSK = ConfigurationManager.AppSettings["UrlSSK"];

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Form.DefaultButton = this.btnValidar.UniqueID;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
        //System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
        if (Session["IDE_USUARIO"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnUpload);
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardar);
        if (!Page.IsPostBack)
        {
            FileUpload1.Attributes["onchange"] = "UploadFile(this)";

            CONTROLES();
        
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaDestino.Text = DateTime.Now.ToString("dd/MM/yyyy");

            gerenciasDestino();
            gerenciasOrigen();


            //BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
            //dtPersonalOperaciones = obj.uspSEL_RRHH_PERSONAL_EMPRESA_CC(BL_Session.CENTRO_COSTO);

            //IngCosto_operacion();
            //Gerencia_operacion();
          
            
            if (Session["IDE_CARTA"]!=null)
            {
                string IDE_CARTA = Session["IDE_CARTA"].ToString();
                if (IDE_CARTA.Length > 0)
                {
                    CartaDatos(Session["IDE_CARTA"].ToString());
                }
               
            }
            else
            {

                DataTable dtResultado = new DataTable();
                BL_PERSONAL obj = new BL_PERSONAL();
                dtResultado = obj.uspCONSULTAR_CECOS_GERENCIA(BL_Session.CENTRO_COSTO);
                if (dtResultado.Rows.Count > 0)
                {

                    ddlGerenciaOrigen.SelectedValue = dtResultado.Rows[0]["IDE_GERENCIA"].ToString();
                }
                else
                {
                    ddlGerenciaOrigen.SelectedValue = BL_Session.CENTRO_COSTO;
                }

                centrosOrigen();
                ddlCentroOrigen.SelectedValue = BL_Session.CENTRO_COSTO;
                PersonalOrigen();
                ddlPersonalOrigen.SelectedValue = Session["IDE_USUARIO"].ToString();
                ddlPersonalOrigen.Text = BL_Session.UsuarioNombre;
            }
        }

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.Browser.IsMobileDevice)
            MasterPageFile = "~/Templates/MPAdminPopupMovil.master";
    }
    protected void Upload(object sender, EventArgs e)
    {

        string cleanMessage = string.Empty;

        if (ddlPersonal.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar responsable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
      
        else
        {

            BE_CARTA_COBRAZAS oBESol = new BE_CARTA_COBRAZAS();
            oBESol.IDE_CARTA = Convert.ToInt32(string.IsNullOrEmpty(lblcodigo.Text) ? "0" : lblcodigo.Text); ;
            oBESol.FECHA_SOLICITA = txtFecha.Text.Trim();
            oBESol.C_USUARIO = ddlPersonalOrigen.SelectedValue.ToString();
            oBESol.C_IPE_CENTRO = ddlGerenciaOrigen.SelectedValue;
            oBESol.C_CENTRO = ddlCentroOrigen.SelectedValue;
            oBESol.C_FECHA = txtFecha.Text.Trim();
            oBESol.D_IPE_CENTRO = ddlGerenciaDestino.SelectedValue.ToString();
            oBESol.D_CENTRO = ddlCentro.SelectedValue.ToString();
            oBESol.D_FECHA = txtFechaDestino.Text.Trim();
            oBESol.D_USUARIO = ddlPersonal.SelectedValue.ToString();
            oBESol.USER_CREACION = Session["IDE_USUARIO"].ToString();
            oBESol.NOTA = txtNota.Text.Trim();
            int dtrpta = 0;
            dtrpta = new BL_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS(oBESol);
            if (dtrpta > 0)
            {
                lblcodigo.Text = dtrpta.ToString();

                FileAdjunto();
            }
        }

        
        //FileUpload1.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUpload1.FileName)));
        //lblMessage.Visible = true;
    }
    protected void CONTROLES()
    {

        BL_SOLPED obj = new BL_SOLPED();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspSEL_RESPONSABLE_PROCESOS(Session["IDE_USUARIO"].ToString(), "RESPONSABLE CARTA DE COBRANZA", BL_Session.ID_EMPRESA.ToString());
        if (dtResultado.Rows.Count > 0)
        {
            ddlGerenciaOrigen.Enabled = true;
            //ddlCentroOrigen.Enabled = true;
            ddlPersonalOrigen.Enabled = true;
        }
        else
        {
            ddlGerenciaOrigen.Enabled = false ;
            //ddlCentroOrigen.Enabled = false;
            ddlPersonalOrigen.Enabled = false;
        }

    }
   
    protected void CartaDatos(string IDE_CARTA)
    {
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_ID(Convert.ToInt32(IDE_CARTA));
        if (dtResultado.Rows.Count > 0)
        {
            string C_IPE_CENTRO = string.Empty;
            lblcodigo.Text = dtResultado.Rows[0]["IDE_CARTA"].ToString();
            txtFecha.Text = dtResultado.Rows[0]["C_FECHA"].ToString();
            txtFechaDestino.Text = dtResultado.Rows[0]["D_FECHA"].ToString();

            

            C_IPE_CENTRO = dtResultado.Rows[0]["C_CENTRO"].ToString();


            string C_CENTRO = dtResultado.Rows[0]["C_CENTRO"].ToString();
            //gerencias();
            DataTable dt = new DataTable();
            BL_PERSONAL xobj = new BL_PERSONAL();
            dt = xobj.uspCONSULTAR_CECOS_GERENCIA(C_IPE_CENTRO);
            if (dt.Rows.Count > 0)
            {
                C_IPE_CENTRO = dt.Rows[0]["IDE_GERENCIA"].ToString();

                ddlGerenciaOrigen.SelectedValue = C_IPE_CENTRO;
            }
            else
            {
                ddlGerenciaOrigen.SelectedValue = C_IPE_CENTRO;
            }

            //ddlGerenciaOrigen.SelectedValue = C_IPE_CENTRO;
            centrosOrigen();
            ddlCentroOrigen.SelectedValue = C_CENTRO;
            PersonalOrigen();

            ddlPersonalOrigen.SelectedValue = dtResultado.Rows[0]["C_USUARIO"].ToString();
            string nombre = dtResultado.Rows[0]["SOLICITA"].ToString();
            ddlPersonalOrigen.Text = nombre;


            string D_IPE_CENTRO = dtResultado.Rows[0]["D_IPE_CENTRO"].ToString();
            string D_CENTRO = dtResultado.Rows[0]["D_CENTRO"].ToString();
            //gerencias();
            ddlGerenciaDestino.SelectedValue = D_IPE_CENTRO;
            
            centros();
            ddlCentro.SelectedValue = D_CENTRO;
            PEP();
            Personal();
            ddlPersonal.SelectedValue = dtResultado.Rows[0]["D_USUARIO"].ToString();
            ddlPersonal.Text  = dtResultado.Rows[0]["RESPONSABLE"].ToString();
            int dtrpta = Convert.ToInt32(lblcodigo.Text);
            ListarDetalle(dtrpta);
            txtNota.Text = dtResultado.Rows[0]["NOTA"].ToString();


            ddlOCosto.SelectedValue = dtResultado.Rows[0]["DNI_ING_OPE"].ToString();
            ddlOCosto.Text = dtResultado.Rows[0]["RESP_ING_OPE"].ToString();
            lbl1.Text = dtResultado.Rows[0]["ING_OPE"].ToString();

            ddlOGerencia.SelectedValue = dtResultado.Rows[0]["DNI_GERENTE_OPE"].ToString();
            ddlOGerencia.Text = dtResultado.Rows[0]["RESP_GERENTE_OPE"].ToString();
            lbl2.Text = dtResultado.Rows[0]["GERENTE_OPE"].ToString();

            ddlCostoDestino.SelectedValue = dtResultado.Rows[0]["DNI_ING_DEST"].ToString();
            ddlCostoDestino.Text = dtResultado.Rows[0]["RESP_ING_DEST"].ToString();
            lbl3.Text = dtResultado.Rows[0]["ING_DEST"].ToString();

            ddlGerenciaDestino2.SelectedValue = dtResultado.Rows[0]["DNI_GERENTE_DEST"].ToString();
            ddlGerenciaDestino2.Text = dtResultado.Rows[0]["RESP_GERENTE_DEST"].ToString();
            lbl4.Text = dtResultado.Rows[0]["GERENTE_DEST"].ToString();

            txtTicket.Text  = dtResultado.Rows[0]["Ticket"].ToString();

            int APROBACION = Convert.ToInt32( dtResultado.Rows[0]["APROBACION"].ToString());
            int RECHAZO = Convert.ToInt32(dtResultado.Rows[0]["RECHAZO"].ToString());

      

            if (RECHAZO==0 && APROBACION==0)
            {
                btnGuardar.Visible = true;
                btnaprobacion.Visible = true;
                btnAgregarAll.Visible = true;
              
                //btnUpload.Visible = true;
                if (dtResultado.Rows[0]["CANT_REGISTRO"].ToString() == "4")
                {
                    btnaprobacion.Visible = true;
                }
                else
                {
                    btnaprobacion.Visible = false;
                }

                foreach (ListViewItem item in lstRol.Items)
                {
                    item.FindControl("btnEditar").Visible = true;
                    item.FindControl("btnEliminar").Visible = true;
                }

                foreach (GridViewRow row in GridView1.Rows)
                {
                    ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                    btnEliminar.Visible = true ;

                }
            }
            else
            {
                btnGuardar.Visible = false ;
                btnaprobacion.Visible = false ;
                btnAgregarAll.Visible = false;
              
                //btnUpload.Visible = false;
                //lstRol.FindControl("btnEditar").Visible = false;
                //lstRol.FindControl("btnEliminar").Visible = false;

                foreach (ListViewItem item in lstRol.Items)
                {
                    item.FindControl("btnEliminar").Visible = false;
                    item.FindControl("btnEditar").Visible = false;
                }

                foreach (GridViewRow row in GridView1.Rows)
                {
                    ImageButton btnEliminar = (ImageButton)row.FindControl("btnEliminar");
                    btnEliminar.Visible = false ;

                }
            }


        }
    }
    protected void Upnl_LoadCosto(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlOCosto"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }

    protected void Upnl_LoadPEP2(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPEP_Origen"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }

    protected void Upnl_LoadGerencia(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlOGerencia"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
        }
    }
    protected void Upnl_LoadOrigen(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPersonalOrigen"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;


            }
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
    protected void Upnl_LoadCostoDestino(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlCostoDestino"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;
            }
        }
    }
    protected void Upnl_LoadGerenciaDestino(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlGerenciaDestino2"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;
            }
        }
    }
    protected void Upnl_LoadPEP(object sender, EventArgs e)
    {
        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];

        if (string.IsNullOrEmpty(eventTarget)) return;
        if (eventTarget.Equals("udpHojaGestion"))
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (arg == null) return;
            if (arg.ToString().Equals("ddlPEP"))
            {
                //string strSelectedValue = ddlPersonal.SelectedValue;
            }
        }
    }
    protected void PEP()
    {

        try
        {
            BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
            DataTable dtResultado = new DataTable();


            dtResultado = obj.uspSEL_PEP_CTA_CONTABLE_POR_OBRA(ddlCentro.SelectedValue);

            if (dtResultado.Rows.Count > 0)
            {
                ddlPEP.Items.Clear();
                ddlPEP.DataSource = dtResultado;

                ddlPEP.DataTextField = "PEP";
                ddlPEP.DataValueField = "PEP";
                ddlPEP.DataBind();
                ddlPEP.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
                ddlPEP.Enabled = true;
            }
            else
            {
                ddlPEP.Items.Clear();
                BL_RRHH_COMPETENCIAS_EVAL Xobj = new BL_RRHH_COMPETENCIAS_EVAL();
                //ddlPEP.DataSource = GetTablePEP(ddlCentro.SelectedValue.ToString());
                ddlPEP.DataSource = Xobj.uspLISTAR_GERENCIA_CENTROS(6, ddlGerenciaDestino.SelectedValue.ToString(), Convert.ToInt32(BL_Session.ID_EMPRESA));
                ddlPEP.DataTextField = "PEP_DESC";
                ddlPEP.DataValueField = "PEP";
                ddlPEP.DataBind();
                ddlPEP.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

                //ddlPEP.SelectedValue = ddlCentro.SelectedValue.ToString();
                //ddlPEP.Enabled = false ;
            }
        }
        catch (Exception)
        {

        }

       
    }
    static DataTable GetTablePEP(string cc)
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("PEP", typeof(string));
        table.Columns.Add("PEP1", typeof(string));

        
         table.Rows.Add(cc, cc);



        return table;
    }
    protected void Personal()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado.Clear();
        //dtResultado = obj.uspSEL_RRHH_PERSONAL_EMPRESA_CC_TIPO(ddlCentro.SelectedValue.ToString(), "01");
        string gerencia = string.Empty;
        if (ddlGerenciaDestino.SelectedValue.ToString() == "Z01")
        {
            gerencia = ddlCentro.SelectedValue.ToString(); ;
        }
        else
        {
            gerencia = ddlGerenciaDestino.SelectedValue.ToString();
        }

        dtResultado = obj.uspSEL_RRHH_PERSONAL_GERENCIA_CARTA(gerencia, "01");
        dtPersonalDestino.Clear();
        dtPersonalDestino = dtResultado;

        if (dtResultado.Rows.Count > 0)
        {
            PersonalCosto();
            PersonalGerencia();
            ddlPersonal.Items.Clear();
            ddlPersonal.DataSource = dtResultado;
            ddlPersonal.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonal.DataValueField = "ID_DNI";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlPersonal.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }
    protected void PersonalOrigen()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();

        //dtResultado = obj.uspSEL_RRHH_PERSONAL_CARTA_COBRANZA(ddlCentroOrigen.SelectedValue.ToString(),"01");

        string gerencia = string.Empty;
        if (ddlGerenciaOrigen.SelectedValue.ToString() == "Z01")
        {
            gerencia = ddlCentroOrigen.SelectedValue.ToString(); ;
        }
        else
        {
            gerencia = ddlGerenciaOrigen.SelectedValue.ToString();
        }

        dtResultado = obj.uspSEL_RRHH_PERSONAL_GERENCIA_CARTA(gerencia, "01");


        dtPersonalOrigen.Clear();
        dtPersonalOrigen = dtResultado;

        if (dtResultado.Rows.Count > 0)
        {
            Gerencia_operacion();
            IngCosto_operacion();
            ddlPersonalOrigen.Items.Clear();
            ddlPersonalOrigen.DataSource = dtResultado;
            ddlPersonalOrigen.DataTextField = "NOMBRE_COMPLETO";
            ddlPersonalOrigen.DataValueField = "ID_DNI";
            ddlPersonalOrigen.DataBind();
            ddlPersonalOrigen.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlPersonalOrigen.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }
    protected void PersonalCosto()
    {
        
        if (dtPersonalDestino.Rows.Count > 0)
        {
            ddlCostoDestino.DataSource = dtPersonalDestino;
            ddlCostoDestino.DataTextField = "NOMBRE_COMPLETO";
            ddlCostoDestino.DataValueField = "ID_DNI";
            ddlCostoDestino.DataBind();
            ddlCostoDestino.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlCostoDestino.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }
    protected void PersonalGerencia()
    {

        if (dtPersonalDestino.Rows.Count > 0)
        {
            ddlGerenciaDestino2.DataSource = dtPersonalDestino;
            ddlGerenciaDestino2.DataTextField = "NOMBRE_COMPLETO";
            ddlGerenciaDestino2.DataValueField = "ID_DNI";
            ddlGerenciaDestino2.DataBind();
            ddlGerenciaDestino2.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlGerenciaDestino2.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }
    protected void IngCosto_operacion()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        
        if (dtPersonalOrigen.Rows.Count > 0)
        {
            ddlOCosto.DataSource = dtPersonalOrigen;
            ddlOCosto.DataTextField = "NOMBRE_COMPLETO";
            ddlOCosto.DataValueField = "ID_DNI";
            ddlOCosto.DataBind();
            ddlOCosto.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlOCosto.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }
    protected void Gerencia_operacion()
    {
        
        if (dtPersonalOrigen.Rows.Count > 0)
        {
            ddlOGerencia.DataSource = dtPersonalOrigen;
            ddlOGerencia.DataTextField = "NOMBRE_COMPLETO";
            ddlOGerencia.DataValueField = "ID_DNI";
            ddlOGerencia.DataBind();
            ddlOGerencia.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
        else
        {
            ddlOGerencia.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
        }
    }
    protected void misdatos()
    {
        //lblMigerencia.Text = BL_Session.IP_CENTRO;
        //lblMicentro.Text = BL_Session.CENTRO_COSTO;
        //lblsolicitante.Text = BL_Session.UsuarioNombre;
    }
    protected void gerenciasDestino()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(4, "", Convert.ToInt32(BL_Session.ID_EMPRESA));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerenciaDestino.DataSource = dtResultado;
            ddlGerenciaDestino.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerenciaDestino.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerenciaDestino.DataBind();

            ddlGerenciaDestino.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

            if(ddlGerenciaDestino.SelectedIndex > 0)
            {
                centros();
             
            }
           

        }
        else
        {
            ddlGerenciaDestino.DataSource = dtResultado;
            ddlGerenciaDestino.DataBind();

            //ddlCentro.DataSource = dtResultado;
            //ddlCentro.DataBind();
        }

    }
    protected void gerenciasOrigen()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(4, "", Convert.ToInt32(BL_Session.ID_EMPRESA));

        if (dtResultado.Rows.Count > 0)
        {
            ddlGerenciaOrigen.DataSource = dtResultado;
            ddlGerenciaOrigen.DataTextField = dtResultado.Columns["DES_GERENCIA"].ToString();
            ddlGerenciaOrigen.DataValueField = dtResultado.Columns["IDE_GERENCIA"].ToString();
            ddlGerenciaOrigen.DataBind();

            centrosOrigen();
            PersonalOrigen();
        }
        else
        {
            ddlGerenciaOrigen.DataSource = dtResultado;
            ddlGerenciaOrigen.DataBind();

            //ddlCentro.DataSource = dtResultado;
            //ddlCentro.DataBind();
        }

    }
    protected void ddlGerenciaDestino_SelectedIndexChanged(object sender, EventArgs e)
    {

        centros();

        Personal();


    }
    protected void centros()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(5, ddlGerenciaDestino.SelectedValue.ToString(), Convert.ToInt32(BL_Session.ID_EMPRESA));

        
        if (dtResultado.Rows.Count > 0)
        {
            ddlCentro.Visible = true ;
            lblCC1.Visible = true;
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentro.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlCentro.DataBind();

            ddlCentro.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

            if (ddlCentro.SelectedIndex > 0)
            {
                Personal();
                //PEP();
            }
            PEP();
            //Personal();
            //PEP();
        }
        else
        {
            lblCC1.Visible = false;
            ddlCentro.Visible = false ;
            ddlCentro.DataSource = dtResultado;
            ddlCentro.DataBind();
            PEP();
        }

    }
    protected void centrosOrigen()
    {
        BL_RRHH_COMPETENCIAS_EVAL obj = new BL_RRHH_COMPETENCIAS_EVAL();
        DataTable dtResultado = new DataTable();
        dtResultado = obj.uspLISTAR_GERENCIA_CENTROS(7, ddlGerenciaOrigen.SelectedValue.ToString(), Convert.ToInt32(BL_Session.ID_EMPRESA));

        if (dtResultado.Rows.Count > 0)
        {
            ddlCentroOrigen.DataSource = dtResultado;
            ddlCentroOrigen.DataTextField = dtResultado.Columns["NOMBRE"].ToString();
            ddlCentroOrigen.DataValueField = dtResultado.Columns["CENTRO_COSTO"].ToString();
            ddlCentroOrigen.DataBind();
            PersonalOrigen();
        }
        else
        {
            ddlCentroOrigen.DataSource = dtResultado;
            ddlCentroOrigen.DataBind();
        }

        PEP_ORIGEN();
        if (ddlGerenciaOrigen.SelectedValue.ToString()=="Z01")
        {
            ddlCentroOrigen.Visible = true;
            lblCC.Visible = true;
        }
        else
        {
            lblCC.Visible = false ;
            ddlCentroOrigen.Visible = false ;
        }
    }
    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        PEP();
        Personal();
    }

    protected void btnAgregarAll_Click(object sender, ImageClickEventArgs e)
    {
        Carta(1);
    }
    protected void Carta(int tipo)
    {
        string cleanMessage = string.Empty;

        if (ddlPersonal.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar responsable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtdocumento.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar descripción";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlPEP_Origen.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar cuenta de costo (Origen)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlPEP.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar cuenta de costo (Destino)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtCantidad.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar cantidad";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (txtPrecio.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar precio unitario";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {

            BE_CARTA_COBRAZAS oBESol = new BE_CARTA_COBRAZAS();
            oBESol.IDE_CARTA = Convert.ToInt32(string.IsNullOrEmpty(lblcodigo.Text) ? "0" : lblcodigo.Text); ;
            oBESol.FECHA_SOLICITA = txtFecha.Text.Trim();
            oBESol.C_USUARIO = ddlPersonalOrigen.SelectedValue.ToString();
            oBESol.C_IPE_CENTRO = ddlGerenciaOrigen.SelectedValue;
            oBESol.C_CENTRO = ddlCentroOrigen.SelectedValue;
            oBESol.C_FECHA = txtFecha.Text.Trim();
            oBESol.D_IPE_CENTRO = ddlGerenciaDestino.SelectedValue.ToString();
            oBESol.D_CENTRO = ddlCentro.SelectedValue.ToString();
            oBESol.D_FECHA = txtFechaDestino.Text.Trim();
            oBESol.D_USUARIO = ddlPersonal.SelectedValue.ToString();
            oBESol.USER_CREACION = Session["IDE_USUARIO"].ToString();
            oBESol.NOTA = txtNota.Text.Trim();
            int dtrpta = 0;
            dtrpta = new BL_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS(oBESol);
            if (dtrpta > 0)
            {
                lblcodigo.Text = dtrpta.ToString();

                BE_CARTA_COBRAZAS_DETALLE oBESolDetalle = new BE_CARTA_COBRAZAS_DETALLE();
                oBESolDetalle.IDE_DETALLE = Convert.ToInt32(string.IsNullOrEmpty(lbldetalle.Text) ? "0" : lbldetalle.Text); ;
                oBESolDetalle.IDE_CARTA = dtrpta;
                oBESolDetalle.DOCUMENTO = txtdocumento.Text.Trim ();
                oBESolDetalle.DETALLE = txtdetalle.Text.Trim();
                oBESolDetalle.CUENTA_COSTO = ddlPEP.SelectedValue;
                oBESolDetalle.CANTIDAD = Convert.ToInt32( txtCantidad.Text.Trim());
                oBESolDetalle.PRECIO = Convert.ToDecimal (txtPrecio.Text.Trim());
                oBESolDetalle.CUENTA_COSTO_ORIGEN = ddlPEP_Origen.SelectedValue;
                int dtdetalle = 0;
                dtdetalle = new BL_CARTA_COBRAZAS_DETALLE().uspUPD_CARTA_COBRAZAS_DETALLE(oBESolDetalle);
                if (dtdetalle > 0)
                {
                   
                    txtdocumento.Text = string.Empty;
                    txtdetalle.Text = string.Empty;
                    ddlPEP.Text = string.Empty;
                    txtCantidad.Text = string.Empty;
                    txtPrecio.Text = string.Empty;
                    ListarDetalle(dtrpta);
                    //lblcodigo.Text = string.Empty;
                    lbldetalle.Text = string.Empty;
                    //lblcodigo.Text = dtdetalle.ToString();
                }
            }
        }
    }
    protected void ListarDetalle(int IDE_CARTA)
    {
        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_DETALLE(IDE_CARTA );
        if (dtResultado.Rows.Count == 0)
        {
            lstRol.Visible = false;
            lstRol.DataSource = dtResultado;
            lstRol.DataBind();
            lblMonto.Text = string.Empty;
            lblTotal.Text = string.Empty;
        }
        else
        {
            lstRol.Visible = true;
            lstRol.DataSource = dtResultado;
            lstRol.DataBind();
            string monto = dtResultado.Rows[0]["monto"].ToString();
            lblTotal.Text = String.Format("{0:n}", monto.ToString());
            lblMonto.Text = "SON : " + dtResultado.Rows[0]["LETRA"].ToString();
        }
        file(IDE_CARTA);
    }

    protected void file(int IDE_CARTA)
    {
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
        DataTable dtResultado = new DataTable();

        //int IDE_CARTA = Convert.ToInt32(Request.QueryString["IDE_CARTA"].ToString());
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_FILE_IDE_CARTA(IDE_CARTA);
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
    protected void EliminarFile(object sender, EventArgs e)
    {
        String path = Server.MapPath(FolderCarta);
       
        //int IDE_CARTA = Convert.ToInt32(Request.QueryString["IDE_CARTA"].ToString());

        ImageButton btnEliminar = ((ImageButton)sender);
        GridViewRow grdrow = (GridViewRow)((ImageButton)sender).NamingContainer;
        string IDE_FILE = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_FILE"].ToString();
        string Archivo = GridView1.DataKeys[grdrow.RowIndex].Values["ARCHIVO"].ToString();
        string IDE_CARTA = GridView1.DataKeys[grdrow.RowIndex].Values["IDE_CARTA"].ToString();
        
        BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
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

        dtResultado = obj.uspDEL_CARTA_COBRAZAS_FILE_ID(Convert.ToInt32(IDE_FILE));
        file(Convert.ToInt32(IDE_CARTA));
    }
    protected void Editar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEditar.CommandArgument);

        //ListViewDataItem itemx = (ListViewDataItem)e.Itemx;

        ListViewItem itemx = lstRol.Items[item - 1];
       
        string IDE_DETALLE = lstRol.DataKeys[item - 1].Values[0].ToString();
        string IDE_CARTA = lstRol.DataKeys[item - 1].Values[0].ToString();

        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
        dtResultado = obj.uspSEL_CARTA_COBRAZAS_DETALLE_ID(Convert.ToInt32( IDE_DETALLE));
        if (dtResultado.Rows.Count > 0)
        {

            lbldetalle.Text= dtResultado.Rows[0]["IDE_DETALLE"].ToString();
            lblcodigo.Text = dtResultado.Rows[0]["IDE_CARTA"].ToString();

            txtdocumento.Text = dtResultado.Rows[0]["DOCUMENTO"].ToString();
            txtdetalle.Text = dtResultado.Rows[0]["DETALLE"].ToString();
            ddlPEP.SelectedValue = dtResultado.Rows[0]["CUENTA_COSTO"].ToString();
            ddlPEP_Origen.SelectedValue = dtResultado.Rows[0]["CUENTA_COSTO_ORIGEN"].ToString();
            txtCantidad.Text = dtResultado.Rows[0]["CANTIDAD"].ToString();
            txtPrecio.Text = dtResultado.Rows[0]["PRECIO"].ToString();

        }

    }
    protected void Eliminar(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditar = ((ImageButton)sender);
        int item = Convert.ToInt32(btnEditar.CommandArgument);

        //ListViewDataItem itemx = (ListViewDataItem)e.Itemx;

        ListViewItem itemx = lstRol.Items[item - 1];

        string IDE_DETALLE = lstRol.DataKeys[item - 1].Values[0].ToString();
        string IDE_CARTA = lstRol.DataKeys[item - 1].Values[0].ToString();

        DataTable dtResultado = new DataTable();
        BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
        dtResultado = obj.uspDEL_CARTA_COBRAZAS_DETALLE_POR_ID(Convert.ToInt32(IDE_DETALLE));
        ListarDetalle(Convert.ToInt32(IDE_CARTA));

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        GuardarCarta("Registro exitoso");
    }
    protected void GuardarCarta(string msg)
    {
        string cleanMessage = string.Empty;

        if (ddlPersonal.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar responsable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlOCosto.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Ingeniero de Costo (DE)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlOGerencia.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Gerente de Operaciones (DE)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlCostoDestino.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Ingeniero de Costo (PARA)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlGerenciaDestino2.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Gerente de Operaciones (PARA)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
          

            int cantidad = 0;
            BE_CARTA_COBRAZAS oBESol = new BE_CARTA_COBRAZAS();
            oBESol.IDE_CARTA = Convert.ToInt32(string.IsNullOrEmpty(lblcodigo.Text) ? "0" : lblcodigo.Text); ;
            oBESol.FECHA_SOLICITA = txtFecha.Text.Trim();
            oBESol.C_USUARIO = ddlPersonalOrigen.SelectedValue.ToString();
            oBESol.C_IPE_CENTRO = ddlGerenciaOrigen.SelectedValue;
            oBESol.C_CENTRO = ddlCentroOrigen.SelectedValue;
            oBESol.C_FECHA = txtFecha.Text.Trim();
            oBESol.D_IPE_CENTRO = ddlGerenciaDestino.SelectedValue.ToString();
            oBESol.D_CENTRO = ddlCentro.SelectedValue.ToString();
            oBESol.D_FECHA = txtFechaDestino.Text.Trim();
            oBESol.D_USUARIO = ddlPersonal.SelectedValue.ToString();
            oBESol.USER_CREACION = Session["IDE_USUARIO"].ToString();
            oBESol.NOTA = txtNota.Text.Trim();

           

            int dtrpta = 0;
            dtrpta = new BL_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS(oBESol);
            if (dtrpta > 0)
            {
                lblcodigo.Text = dtrpta.ToString();
                //FileAdjunto();
                string CARGO = string.Empty;
                string PERSONAL = string.Empty;
                string D_IPE_CENTRO = string.Empty;
                string D_CENTRO = string.Empty;

                for (int i = 1; i <= 4; i++)
                {
                    if (i == 1)
                    {
                        CARGO = "INGENIERO";
                        PERSONAL = ddlOCosto.SelectedValue.ToString();
                        D_IPE_CENTRO = BL_Session.IP_CENTRO;
                        D_CENTRO = BL_Session.CENTRO_COSTO;
                    }
                    else if (i == 2)
                    {
                        CARGO = "GERENTE";
                        PERSONAL = ddlOGerencia.SelectedValue.ToString();
                        D_IPE_CENTRO = BL_Session.IP_CENTRO;
                        D_CENTRO = BL_Session.CENTRO_COSTO;
                    }
                    else if (i == 3)
                    {
                        CARGO = "INGENIERO";
                        PERSONAL = ddlCostoDestino.SelectedValue.ToString();
                        D_IPE_CENTRO = ddlGerenciaDestino.SelectedValue.ToString();
                        D_CENTRO = ddlCentro.SelectedValue.ToString();
                    }
                    else if (i == 4)
                    {
                        CARGO = "GERENTE";
                        PERSONAL = ddlGerenciaDestino2.SelectedValue.ToString();
                        D_IPE_CENTRO = ddlGerenciaDestino.SelectedValue.ToString();
                        D_CENTRO = ddlCentro.SelectedValue.ToString();
                    }



                    BE_CARTA_COBRAZAS_APROBACIONES oBESolAprobacion = new BE_CARTA_COBRAZAS_APROBACIONES();
                    oBESolAprobacion.IDE_APROBACION = 0;
                    oBESolAprobacion.IDE_CARTA = Convert.ToInt32(lblcodigo.Text);
                    oBESolAprobacion.D_IPE_CENTRO = D_IPE_CENTRO;
                    oBESolAprobacion.D_CENTRO = D_CENTRO;
                    oBESolAprobacion.DNI_APRUEBA = PERSONAL;
                    oBESolAprobacion.TIPO_CARGO = i;
                    oBESolAprobacion.CARGO = CARGO;
                    int dtAprobacion = 0;
                    dtAprobacion = new BL_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS_APROBACIONES(oBESolAprobacion);
                    if (dtAprobacion > 0)
                    {
                        cantidad++;
                    }
                }
            }
            if (cantidad == 4)
            {
                //gerenciasDestino();
                //gerenciasOrigen();
              
                cleanMessage = msg;

                DataTable dtResultado = new DataTable();
                BL_CARTA_COBRAZAS obj = new BL_CARTA_COBRAZAS();
                dtResultado = obj.uspSEL_CARTA_COBRAZAS_ID(dtrpta);
                if (dtResultado.Rows.Count > 0)
                {
                    if (dtResultado.Rows[0]["DNI_ING_OPE"].ToString() == string.Empty )
                    {
                        ddlOCosto.SelectedValue = string.Empty;
                        ddlOCosto.Text = string.Empty;
                    }

                    if (dtResultado.Rows[0]["DNI_GERENTE_OPE"].ToString() == string.Empty)
                    {
                        ddlOGerencia.SelectedValue = string.Empty;
                        ddlOGerencia.Text = string.Empty;
                    }
                    if (dtResultado.Rows[0]["DNI_ING_DEST"].ToString() == string.Empty)
                    {
                        ddlCostoDestino.SelectedValue = string.Empty;
                        ddlCostoDestino.Text = string.Empty;
                    }

                    if (dtResultado.Rows[0]["DNI_GERENTE_DEST"].ToString() == string.Empty)
                    {
                        ddlGerenciaDestino2.SelectedValue = string.Empty;
                        ddlGerenciaDestino2.Text = string.Empty;
                    }


                    txtTicket.Text = dtResultado.Rows[0]["Ticket"].ToString();

                    //if (dtResultado.Rows[0]["CANT_REGISTRO"].ToString()=="4")
                    //{
                    //    btnaprobacion.Visible = true;
                    //}
                    //else
                    //{
                    //    btnaprobacion.Visible = false ;
                    //}
                }
               
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
            else
            {
                CartaDatos(dtrpta.ToString());
                cleanMessage = "Existe personal asigando a más de un cargo";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                
            }
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Page.Response.Redirect(Page.Request.Url.ToString(), true);

    }

    protected void btnBandeja_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Response.Redirect("~/OPERACIONES/CartaCobranzasBandeja.aspx");
    }

    protected void btnaprobacion_Click(object sender, EventArgs e)
    {
        string cleanMessage = string.Empty;
        if (ddlPersonal.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar responsable";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlOCosto.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Ingeniero de Costo (DE)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlOGerencia.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Gerente de Operaciones (DE)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlCostoDestino.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Ingeniero de Costo (PARA)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (ddlGerenciaDestino2.Text.Trim() == string.Empty)
        {
            cleanMessage = "Ingresar Gerente de Operaciones (PARA)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (lblcodigo.Text.Trim() == string.Empty)
        {
            cleanMessage = "Faltan guardar datos (detalle carta cobranza)";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else if (lstRol.Items.Count == 0)
        {
            cleanMessage = "Falta registrar el detalle de la carta cobranza";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
        else
        {
            GuardarCarta("Envio satisfactorio");

            DataTable dtResultado = new DataTable();

            BL_CARTA_COBRAZAS_DETALLE obj = new BL_CARTA_COBRAZAS_DETALLE();
            dtResultado = obj.SP_CORREO_APROBACIONES_CARTACOBRANZA(Convert.ToInt32(lblcodigo.Text), 1, URLSSK, Session["IDE_USUARIO"].ToString(), Session["IDE_USUARIO"].ToString(), 0);
            if (dtResultado.Rows.Count > 0)
            {
               
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
            }
        }
    }

    protected void ddlGerenciaOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlPersonalOrigen.Text = string.Empty;
        //ddlOCosto.Text = string.Empty;
        //ddlOGerencia.Text = string.Empty;
        centrosOrigen();
        //PersonalOrigen();
    }

    protected void ddlCentroOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        PersonalOrigen();
    }

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
       
    //}
    protected void FileAdjunto()
    {
        String path = Server.MapPath(FolderCarta);
        String pathBackups = FolderCartaBackups;
        //String path = FolderCarta;
        string Archivo = string.Empty;
        HttpFileCollection uploadedFiles = Request.Files;
        if (lblcodigo.Text != string.Empty)
        {
            Archivo = "FILE_" + lblcodigo.Text + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(FileUpload1.FileName);
            if (File.Exists(path + Archivo))
            {
                File.Delete(path + Archivo);
                FileUpload1.SaveAs(path + Archivo);
                FileUpload1.SaveAs(pathBackups + Archivo);
            }
            else
            {
                FileUpload1.SaveAs(path + Archivo);
                FileUpload1.SaveAs(pathBackups + Archivo);

            }

            Boolean fileOK = false;
            int nro = 0;
            //for (int i = 0; i < Request.Files.Count; i++)
            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                HttpPostedFile PostedFile = uploadedFiles[i];
                if (PostedFile.ContentLength > 0)
                {
                    //string FileName = System.IO.Path.GetFileName(PostedFile.FileName);


                    String fileExtension = Path.GetExtension(PostedFile.FileName);

                    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" ,".msg"};
                    for (int j = 0; j < allowedExtensions.Length; j++)
                    {
                        if (fileExtension.ToUpper() == allowedExtensions[j].ToUpper())
                        {
                            fileOK = true;
                        }
                    }

                    if (fileOK)
                    {
                        try
                        {
                            nro = i + 1;
                            Archivo = "FILE_" + lblcodigo.Text + "_" + DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(PostedFile.FileName);
                            if (File.Exists(path + Archivo))
                            {
                                File.Delete(path + Archivo);
                                PostedFile.SaveAs(path + Archivo);
                            }
                            else
                            {
                                PostedFile.SaveAs(path + Archivo);

                            }
                            BE_CARTA_COBRAZAS_FILE oBESol = new BE_CARTA_COBRAZAS_FILE();
                            oBESol.IDE_FILE = 0;
                            oBESol.ARCHIVO = Archivo;
                            oBESol.RUTA = FolderCarta;
                            oBESol.IDE_CARTA = Convert.ToInt32(lblcodigo.Text);
                            oBESol.NOMBRE_ORIGINAL = Path.GetFileName(PostedFile.FileName);

                            int rpta;
                            rpta = new BL_CARTA_COBRAZAS().uspINS_CARTA_COBRAZAS_FILE(oBESol);
                            if (rpta > 0)
                            {
                                rpta++;
                            }
                            if (rpta > 0)
                            {
                                file(Convert.ToInt32(lblcodigo.Text));
                                string cleanMessage = "Archivo(s) cargado(s) correctamente";
                                //FileUpload1.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUpload1.FileName)));
                                //lblMessage.Visible = true;
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            string cleanMessage = "Archivo no puede ser cargado";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
                        }
                    }

                }
            }

        }
        else
        {
            string cleanMessage = "Falta registrar carta cobranza";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }

    protected void btnAdjunto_Click(object sender, EventArgs e)
    {
        if (lblcodigo.Text != string.Empty)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "myScript", "popup2(" + lblcodigo.Text + "," + 500 + "," + 320 + ");", true);
        }
        else
        {
            string cleanMessage = "Falta registrar carta cobranza";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "doAlert('" + cleanMessage + "');", true);
        }
    }






    protected void btnValidar_Click(object sender, EventArgs e)
    {
        Session["IDE_CARTA"] = null;
        Session["IDE_CARTA"] = string.Empty;
        Session.Remove("IDE_CARTA");
        Response.Redirect("~/OPERACIONES/CartaCobranzasBandejaAprobaciones.aspx");
    }

    protected void PEP_ORIGEN()
    {

        try
        {
            BL_LOG_SOLPED obj = new BL_LOG_SOLPED();
            DataTable dtResultado = new DataTable();


            dtResultado = obj.uspSEL_PEP_CTA_CONTABLE_POR_OBRA(ddlCentroOrigen.SelectedValue);

            if (dtResultado.Rows.Count > 0)
            {
                ddlPEP_Origen.Items.Clear();
                ddlPEP_Origen.DataSource = dtResultado;

                ddlPEP_Origen.DataTextField = "PEP";
                ddlPEP_Origen.DataValueField = "PEP";
                ddlPEP_Origen.DataBind();
                ddlPEP_Origen.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));
                ddlPEP_Origen.Enabled = true;
            }
            else
            {
                ddlPEP_Origen.Items.Clear();
                BL_RRHH_COMPETENCIAS_EVAL Xobj = new BL_RRHH_COMPETENCIAS_EVAL();
                //ddlPEP.DataSource = GetTablePEP(ddlCentro.SelectedValue.ToString());
                ddlPEP_Origen.DataSource = Xobj.uspLISTAR_GERENCIA_CENTROS(6, ddlGerenciaOrigen.SelectedValue.ToString(), Convert.ToInt32(BL_Session.ID_EMPRESA));
                ddlPEP_Origen.DataTextField = "PEP_DESC";
                ddlPEP_Origen.DataValueField = "PEP";
                ddlPEP_Origen.DataBind();
                ddlPEP_Origen.Items.Insert(0, new ListItem("--- Seleccionar ---", ""));

                //ddlPEP.SelectedValue = ddlCentro.SelectedValue.ToString();
                //ddlPEP.Enabled = false ;
            }
        }
        catch (Exception)
        {

        }


    }
}