<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="BuenasIdeasBandeja.aspx.cs" Inherits="OPERACIONES_BuenasIdeasBandeja" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #d79b01;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }

        input[type="submit"]:hover {
            outline: thin dotted #333;
            outline: 5px auto -webkit-focus-ring-color;
            outline-offset: -2px;
        }
    </style>
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

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-4">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Buenasideas.180x90.fw.png" />
        </div>
        <div class="col-md-4">
            <label>Estados</label>
            <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <br />
            <center>
                  <asp:Button ID="btnReporte" runat="server" Text="DESCARGAR REPORTE" OnClick="btnReporte_Click" CausesValidation="False" />
            </center>
        </div>
    </div>
    <br />
                            <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_IDEAS" EmptyDataText="There are no data records to display." Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="80" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                             <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                                            <asp:BoundField DataField="PERSONAL" HeaderText="Personal" SortExpression="PERSONAL" />
                                            <asp:BoundField DataField="TITULO" HeaderText="Título de la propuesta" SortExpression="TITULO" />
                                             <asp:TemplateField HeaderText="Puntos">
                                                 <ItemTemplate>
                                                     <asp:TextBox ID="txtPunto" Width ="70" onKeyPress="return SoloNum(event)" runat="server" Enabled='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>' Text='<%# Eval("PUNTOS") %>'></asp:TextBox>
                                                     <asp:RangeValidator ID="rangeValidator1" runat="server" ControlToValidate="txtPunto" MaximumValue="501" MinimumValue="0" ForeColor="Red" ErrorMessage="Fuera de rango (0-500)" />
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Detalles">
                                                 <ItemTemplate>
                                                     <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_IDEAS") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="ver información" OnClick ="ver_Sustento"  CausesValidation="false"  />
                                                       
                                                 </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Estado">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("SITUACION_RESUMEN") %>'></asp:Label>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Control">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal"  Enabled='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>' >
                                                        <asp:ListItem>P</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>R</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Procesar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnProcesar" runat="server" CommandArgument='<%# Eval("IDE_IDEAS") %>' ImageUrl="~/imagenes/SaveIcono.png" ToolTip="Procesar Reconocimiento" OnClick ="ProcesarReconocimiento"  Enabled='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>'/>
                                                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnProcesar">
                                                        </cc1:ConfirmButtonExtender>
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
        <asp:Panel ID="Panel1" runat="server" >
        <center>
<asp:Label runat="server" Text="Comentarios de rechazo" Font-Bold="True" ForeColor="White"></asp:Label>
<asp:Label runat="server" ID="lblCodigo" Visible="False"></asp:Label>
           
        <asp:TextBox runat="server" ID="txtSustento" Height="120px" MaxLength="500" TextMode="MultiLine" Width="500px"></asp:TextBox>
            <br />
     <asp:Button runat="server" Text="Cancelar" ID="btnCancel"></asp:Button>
        
      <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>
        </center>
        </asp:Panel>
</asp:Content>

