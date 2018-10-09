<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="CartaCobranzasBandeja.aspx.cs" Inherits="OPERACIONES_CartaCobranzasBandeja" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/gridviewScroll.min.js" type="text/javascript"></script>

    <style type="text/css">

        .GridviewScrollHeader TH, .GridviewScrollHeader TD 
{ 
  
    font-weight: bold; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #EFEFEF; 
    text-align: left; 
    vertical-align: bottom; 
} 
.GridviewScrollItem TD 
{ 
    padding: 5px; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
    height:45px;
} 
.GridviewScrollPager  
{ 
    border-top: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
} 
.GridviewScrollPager TD 
{ 
    padding-top: 3px; 
    font-size: 14px; 
    padding-left: 5px; 
    padding-right: 5px; 
} 
.GridviewScrollPager A 
{ 
    color: #666666; 
}
.GridviewScrollPager SPAN

{

    font-size: 16px;

    font-weight: bold;

}
            </style>
    <script type="text/javascript">

     
        //document.onselectstart = function () { return false; }
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
        function doAlert(mensaje) {
            var msg = new DOMAlert(
            {
                title: 'Mensaje del Sistema',
                text: '<br/>' + mensaje,
                skin: 'default',
                width: 300,
                height: 80,
                ok: { value: true, text: 'Aceptar', onclick: showValue },

            });
            msg.show();
        };

        function showValue(sender, value) {
            sender.close();

        }

         function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
          for (i = 1; i < GridView2.rows.length; i++) {
              GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
          }
         }
          $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 1250,
                height: 370,
                startHorizontal: 0,
                wheelstep: 10,
                barhovercolor: "#C0C0C0",
                barcolor: "#C0C0C0",
                IsInUpdatePanel: true,
                freezesize: 5
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-6">
                        <b>BANDEJA CARTA DE COBRANZAS  </b>
                        <asp:Label ID="lblcodigo" runat="server"></asp:Label>
                        <asp:Label ID="lbldetalle" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <center>
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
                        <asp:Button ID="btnBandeja" runat="server" Text="Mi bandeja" OnClick="btnBandeja_Click" />
                        <asp:Button ID="btnSolicitud" runat="server" Text="Mis aprobaciones" OnClick="btnValidar_Click" />
                            </center>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <label>Año</label>
            <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <label>Centro</label>
            <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <label>Estado</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
       
        <div class="col-md-3">
            <br />
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/imagenes/boton.eliminar.gif" OnClick="btnEliminar_Click" />
            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
<cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEliminar"
OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
<div class="header">
Mensaje
</div>
<div class="body">
¿Deseas eliminar registro?
</div>
<div class="footer" align="right">
<asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
<asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
</div>
</asp:Panel>

            <br />
        </div>
        <div class="col-md-3">
            <label>Leyenda</label>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/EstadoColores.jpg" />
        </div>
    </div>


    <div class="row">
        <%--<asp:Panel ID="Panel2" runat="server" Height="350px" Width="100%" ScrollBars="Vertical">--%>
            <asp:GridView ID="GridView1" runat="server" Width="100%"   
                                        AutoGenerateColumns="False" DataKeyNames="IDE_CARTA,TICKET,TICKET_ANTERIOR" 
                                        EmptyDataText="There are no data records to display." Font-Size="8pt"  
                                        CssClass="table table-striped table-bordered table-hover"
                                        OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" >
                                       
                                        <Columns>
                                            

                                            <asp:TemplateField HeaderText="ITEM" SortExpression="ITEM">
                                                <HeaderTemplate>
                                                    <label>Items</label><br />
                                                    <%--<input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Text='<%# Eval("Row") %>' Visible='<%# (Convert.ToBoolean(Eval("FLG_VISIBLE") )) %>'  />
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ticket" SortExpression="Ticket">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label42" runat="server" Text="Ticket" Width="100px"></asp:Label>
                                                     <asp:TextBox ID="txtTicket_H" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtTicket_H_TextChanged"></asp:TextBox>
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label43" runat="server" Text='<%# Bind("Ticket") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                            <asp:BoundField DataField="SOLICITANTE" HeaderText="Solicitante" SortExpression="SOLICITANTE" />
                                            <asp:BoundField DataField="CREADO" HeaderText="Creado por" SortExpression="CREADO" />
                                            <asp:BoundField DataField="C_FECHA" HeaderText="Fecha" SortExpression="C_FECHA" >
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                           
                                            <asp:TemplateField HeaderText="C_CENTRO" SortExpression="C_CENTRO">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label44" runat="server" Text="CC. Origen" Width="80px"></asp:Label>
                                                     <asp:TextBox ID="txtC_CENTRO_H" runat="server" Width="80px" AutoPostBack="true" OnTextChanged ="txtC_CENTRO_H_TextChanged"></asp:TextBox>
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label45" runat="server" Text='<%# Bind("C_CENTRO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="D_CENTRO" SortExpression="D_CENTRO">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label46" runat="server" Text="CC. Destino" Width="80px"></asp:Label>
                                                     <asp:TextBox ID="txtD_CENTRO_H" runat="server" Width="80px" AutoPostBack="true" OnTextChanged="txtD_CENTRO_H_TextChanged"></asp:TextBox>
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label47" runat="server" Text='<%# Bind("D_CENTRO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                            <asp:BoundField DataField="RESPONSABLE" HeaderText="Responsable (Destino)" SortExpression="RESPONSABLE" />
                                            <asp:BoundField DataField="ING_OPE" HeaderText="Ing.Costo" SortExpression="ING_OPE">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GERENTE_OPE" HeaderText="Gerente" SortExpression="GERENTE_OPE">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ING_DEST" HeaderText="Ing.Costo" SortExpression="ING_DEST">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GERENTE_DEST" HeaderText="Gerente" SortExpression="GERENTE_DEST">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ETAPAS" HeaderText="Estado carta" SortExpression="ETAPAS" />
                                            <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%# Eval("IDE_CARTA") %>' ImageUrl="~/imagenes/PencilAdd20.png" OnClick="seleccionar" ToolTip="Modificar carta" Visible='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>' />
                                                        <asp:ImageButton ID="btnGenerar" runat="server" CommandArgument='<%# Eval("IDE_CARTA") %>' ImageUrl="~/imagenes/registro20.fw.png" OnClick="NuevaCarta" ToolTip="Generar nueva carta" Visible='<%# (Convert.ToBoolean(Eval("NUEVO") )) %>' />
                                                    </center>
                                                    <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnGenerar" />
                                                    <cc1:ModalPopupExtender ID="mpe1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnNo1" OkControlID="btnYes1" PopupControlID="pnlPopup1" TargetControlID="btnGenerar">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                                        <div class="header">
                                                            Mensaje
                                                        </div>
                                                        <div class="body">
                                                            ¿Deseas generar carta <%# Eval("ticket") %>?
                                                        </div>
                                                        <div align="right" class="footer">
                                                            <asp:Button ID="btnYes1" runat="server" CssClass="yes" Text="Sí" />
                                                            <asp:Button ID="btnNo1" runat="server" CssClass="no" Text="No" />
                                                        </div>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descargar">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl='<%# Eval("LINK") %>' Target="_blank" Text="Descargar"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registro.Codigo.SAP">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSAP" runat="server" MaxLength="12" onkeydown="return jsDecimals(event);" Text='<%# Eval("SAP") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IMG">
                                                <ItemTemplate>

                                                      <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" NavigateUrl='<%# Eval("IMG") %>' Target="_blank" Text="Ver adjunto" Visible='<%# (Convert.ToBoolean(Eval("FLG_VISIBLE_IMG") )) %>'></asp:HyperLink>
                         
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjuntar Imagen">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Guardar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnGuardar" runat="server" CommandArgument='<%# Eval("IDE_CARTA") %>' ImageUrl="~/imagenes/SaveIcono20.png" OnClick="ProcesarSAP" Visible='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            
                                           <asp:TemplateField HeaderText="Reenviar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnReenviar" runat="server" CommandArgument='<%# Eval("IDE_CARTA") %>' ImageUrl="~/imagenes/Desempenio_comenta.png" OnClick="ReenviarCC" Visible='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle CssClass="GridviewScrollHeader" /> 
                                            <RowStyle CssClass="GridviewScrollItem" /> 
                                            <PagerStyle CssClass="GridviewScrollPager" /> 
                                        
                                    </asp:GridView>

                              
        <%--</asp:Panel>--%>
    </div>

</asp:Content>

