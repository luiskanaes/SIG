<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="PERMISOS_BANDEJA.aspx.cs" Inherits="OPERACIONES_PERMISOS_BANDEJA" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/ControlPermisos.ascx" TagPrefix="uc1" TagName="ControlPermisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

        <script type="text/javascript">
   
 
    document.onselectstart = function () { return false; }
    function SoloNum(e) {
        var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
        return ((key_press > 47 && key_press < 58) || key_press == 46);
        // el  "|| key_press == 46" es para incluir el punto ".", si borramos solo ingresara enteros 
    }
    function jsDecimals(e) {

        var evt = (e) ? e : window.event;
        var key = (evt.keyCode) ? evt.keyCode : evt.which;
        if (key != null) {
            key = parseInt(key, 10);
            if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
                if (!jsIsUserFriendlyChar(key, "Decimals")) {
                    return false;
                }
            }
            else {
                if (evt.shiftKey) {
                    return false;
                }
            }
        }
        return true;
    }

    // Función para las teclas especiales
    //------------------------------------------
    function jsIsUserFriendlyChar(val, step) {
        // Backspace, Tab, Enter, Insert, y Delete
        if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
            return true;
        }
        // Ctrl, Alt, CapsLock, Home, End, y flechas
        if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
            return true;
        }
        if (step == "Decimals") {
            if (val == 190 || val == 110) {  //Check dot key code should be allowed
                return true;
            }
        }
        // The rest
        return false;
    }
    function val(e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8 || tecla == 32) return true;
        patron = /[A-Za-z]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    }
    function ValidaDDL(source, arguments) {
        if (arguments.Value < 1) {
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    } 
    function doAlert( mensaje)
    {
        var msg = new DOMAlert(
        {
            title: 'Mensaje del Sistema :',
            text: '<br/>' + mensaje,
            skin: 'default',
            width: 300,
            height: 80,
            ok: { value: true, text: 'Aceptar', onclick: showValue },
			   
        });
        msg.show();
    };
		
    function showValue(sender, value)
    {
        sender.close();
			
    }
function popup(Requ_Numero, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("PermisoEnviarEmail.aspx?Requ_Numero=" + Requ_Numero , "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
       var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <%-- <asp:ImageButton ID="btnMdp" runat="server" ImageUrl="~/imagenes/pdf_download.png" OnClick ="Formato" Visible='<%# (Convert.ToBoolean(Eval("MDP") )) %>'/>--%>
        
        <div class="col-md-12">
             <center>
              <%--  <uc1:controlpermisos runat="server" ID="ControlPermisos" />--%>
            </center>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-md-3">
             <label>Estados</label>
            <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-3">
           

        </div>
        <div class="col-md-3">
            <br />
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/imagenes/boton.Excel.jpg" OnClick="btnExportar_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12 ">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_PERMISO" EmptyDataText="There are no data records to display." Font-Size="8pt" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="80" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <center>
                                         <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("COLOR") %>' />
                                    </center>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TICKET" HeaderText="Ticket" SortExpression="TICKET" />
                        <asp:BoundField DataField="PERSONAL" HeaderText="Personal" SortExpression="PERSONAL" />
                        <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                        <asp:BoundField DataField="INICIO" HeaderText="Del" SortExpression="INICIO" />
                        <asp:BoundField DataField="FIN" HeaderText="Al" SortExpression="FIN" />
                        <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios" SortExpression="COMENTARIOS" />
                        <asp:TemplateField HeaderText="Estado" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("SITUACION_RESUMEN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Control">
                            <ItemTemplate>
                                <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal" Enabled='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'>
                                    <asp:ListItem>P</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>R</asp:ListItem>
                                </asp:RadioButtonList>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Procesar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnProcesar" runat="server" CommandArgument='<%# Eval("IDE_PERMISO") %>' ImageUrl="~/imagenes/SaveIcono.png" ToolTip="Procesar solicitud" OnClick="Procesar" Enabled='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>' />
                                <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnProcesar"></cc1:ConfirmButtonExtender>
                                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnProcesar"
                                    OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                        Mensaje SSK
                                    </div>
                                    <div class="body">
                                        ¿Deseas procesar solicitud?
                                    </div>
                                    <div class="footer" align="right">
                                        <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                        <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                    </div>
                                </asp:Panel>

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Formato">
                            <ItemTemplate>
                                <%-- <asp:ImageButton ID="btnMdp" runat="server" ImageUrl="~/imagenes/pdf_download.png" OnClick ="Formato" Visible='<%# (Convert.ToBoolean(Eval("MDP") )) %>'/>--%>
                                <center>
                                                      <asp:HyperLink ID="HyperLink1" runat="server" Visible='<%# Convert.ToBoolean(Eval("FORMATO") ) %>' Font-Bold="True" NavigateUrl='<%# Eval("LINK") %>' Target="_blank" Enabled='<%# Convert.ToBoolean(Eval("BLOQUEAR") ) %>' Text='<%# Eval("Descargar") %>'></asp:HyperLink>
                                                   </center>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="File">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/MDP/"+Eval("FILE_DOC") %>' Visible='<%# (Convert.ToBoolean(Eval("FILE_VISIBLE") )) %>' Target="_blank"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Enviar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEnviar" runat="server" CommandArgument='<%# Eval("IDE_PERMISO") %>' ImageUrl='<%# Eval("IMG") %>' ToolTip='<%# Eval("IMG_TOOL") %>' OnClick="EnviarEmail" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>


            </div>
        </div>
    </div>

    <asp:HiddenField ID="HidRegistro" runat="server" />
    <cc1:ModalPopupExtender ID="ModalRegistro"
        runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="Panel1"
        PopupDragHandleControlID="Panel1"
        TargetControlID="HidRegistro">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server">
        <center>
<asp:Label runat="server" ID="lblmsg" Font-Bold="True" ForeColor="#fff600"></asp:Label>
<asp:Label runat="server" ID="lblCodigo" Visible="False"></asp:Label>
            <asp:Label runat="server" ID="lblrpta" Visible="False"></asp:Label>
           
        <asp:TextBox runat="server" ID="txtSustento" Height="120px" MaxLength="500" TextMode="MultiLine" Width="500px"></asp:TextBox>
            <br />
     <asp:Button runat="server" Text="Cancelar" ID="btnCancel" OnClick="btnCancel_Click"></asp:Button>
        
      <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>
        </center>
    </asp:Panel>


      <div class="row">
        <div class="col-lg-12 ">
            <div class="table-responsive">
                <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_PERMISO" EmptyDataText="There are no data records to display." Font-Size="8pt" >
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                       
                        <asp:BoundField DataField="TICKET" HeaderText="Ticket" SortExpression="TICKET" />
                        <asp:BoundField DataField="PERSONAL" HeaderText="Personal" SortExpression="PERSONAL" />
                        <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                        <asp:BoundField DataField="INICIO" HeaderText="Del" SortExpression="INICIO" />
                        <asp:BoundField DataField="FIN" HeaderText="Al" SortExpression="FIN" />
                         <asp:BoundField DataField="ESTADO" HeaderText="Situacion" SortExpression="ESTADO" />
                        <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios" SortExpression="COMENTARIOS" />
                    </Columns>
                </asp:GridView>


            </div>
        </div>
    </div>
</asp:Content>

