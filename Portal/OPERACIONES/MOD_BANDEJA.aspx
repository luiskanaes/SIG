<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="MOD_BANDEJA.aspx.cs" Inherits="OPERACIONES_MOD_BANDEJA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>BANDEJA DE REQUERIMIENTO PERSONAL EN OBRA </b>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <label>Ticket</label>
            <asp:TextBox ID="txtTicket" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <label>Año</label>
            <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <label>Centros</label>
            <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-6">
            <br />
        
<asp:Button runat="server" Text="Buscar" ID="btnBuscar" CssClass="btn-danger active" OnClick="btnBuscar_Click"></asp:Button>
                 <asp:Button ID="Button1" runat="server" Text="Nuevo" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Requerimientos" OnClick="Button2_Click" />
             
        </div>
        
        

    </div>

    <div class="row">
        <asp:DataList ID="dlCustomers" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="row"
             OnItemDataBound="dlCustomers_ItemDataBound">
            <ItemTemplate>

                <div class="col-sm-12">
                    <!--THUMBNAIL#2-->
                    <div class="panel-body">
                        <div class="row" style="color:#ffffff;
                            padding:3px 0 3px 10px;
                            margin-bottom:2px;
                            font-weight:bold;
                            text-align:justify;
                            background:#999;
                            border-radius:5px;
                            padding:10px;">
                            
                            
                            <div class="col-md-9" >
                                <asp:Label ID="lblIDE_MOD" runat="server" Text='<%# Eval("IDE_MOD")%>' Visible="false"></asp:Label>
                                TICKET : <b style="background-color:yellow; color:black"><%# Eval("TICKET")%></b> -
                                SOLICITADO POR :   <b><%# Eval("NOM_SOLICITANTE")%></b>
                            </div>
                            <div class="col-md-3">
                                <asp:ImageButton ID="btn1" runat="server"  ImageUrl="~/imagenes/boton.actualizar.gif"  CommandArgument='<%# Eval("IDE_MOD") %>' OnClick="Datos_Req"/>
                                <asp:ImageButton ID="btn2" runat="server"  ImageUrl="~/imagenes/boton.eliminar.gif"  CommandArgument='<%# Eval("IDE_MOD") %>' OnClick="Eliminar_Req"/>
                                <cc1:ConfirmButtonExtender ID="cbe2" runat="server" DisplayModalPopupID="mpe2" TargetControlID="btn2"></cc1:ConfirmButtonExtender>
                                <cc1:ModalPopupExtender ID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="btn2"
                                OkControlID="btnYes2" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none">
                                <div class="header">
                                Mensaje
                                </div>
                                <div class="body" style="color:#000000">
                                ¿Deseas eliminar requerimiento?
                                </div>
                                <div class="footer" align="right">
                                <asp:Button ID="btnYes2" runat="server" Text="Sí" CssClass="yes" />
                                <asp:Button ID="btnNo2" runat="server" Text="No" CssClass="no" />
                                </div>
                                </asp:Panel>
                            
                            </div>
                        </div>
                        <b>
                            

                          

                            <hr />
                        </b>


                        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="IDE_MOD,IDE_REQUERIMIENTO" CssClass="EtiquetaSimple"
                             GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" ShowHeader="false" />
                                <asp:BoundField DataField="CATEGORIA" HeaderText="CATEGORIA" SortExpression="CATEGORIA" ShowHeader="false"/>
                                <asp:BoundField DataField="ESPECIALIDAD" HeaderText="ESPECIALIDAD" SortExpression="ESPECIALIDAD" ShowHeader="false"/>
                                <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" SortExpression="CANTIDAD" ShowHeader="false"/>
                                 <asp:BoundField DataField="PENDIENTE" HeaderText="PENDIENTE" SortExpression="PENDIENTE" ShowHeader="false"/>
                                    <asp:TemplateField HeaderText="Ver Personal">
                                        <itemtemplate>
                                        <asp:ImageButton ID="btnEquipo" runat="server" CommandArgument='<%# Eval("IDE_REQUERIMIENTO") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="ver información" OnClick="Ver" CausesValidation="false" />
                                    </itemtemplate>
                                    </asp:TemplateField>


                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                       
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_REQUERIMIENTO") %>' ImageUrl="~/imagenes/Ico_delete.png" OnClick="Eliminar" />
                              
                                       
                                        <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                                        <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEliminar"
                                            OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                        </cc1:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                            <div class="header">
                                                Mensaje
                                            </div>
                                            <div class="body" >
                                                ¿Deseas eliminar registro?
                                            </div>
                                            <div class="footer" align="right">
                                                <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                            </div>
                                        </asp:Panel>


                                    </ItemTemplate>
                                    
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>


                    </div>
                </div>

            </ItemTemplate>
        </asp:DataList>
 </div>
</asp:Content>

