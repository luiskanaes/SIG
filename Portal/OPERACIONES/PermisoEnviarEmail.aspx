<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="PermisoEnviarEmail.aspx.cs" Inherits="OPERACIONES_PermisoEnviarEmail" %>
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
                title: 'Mensaje del Sistema :',
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
        function lettersOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
          ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
          (charCode < 97 || charCode > 122)) {

                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>ENVIAR MDP A RRHH  </b> 
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <label>Enviar copia</label>
            <asp:TextBox ID="txtCorreo" runat="server"  ValidationGroup="Enviar" placeholder="Ingresar correo"></asp:TextBox>
            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" 
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                 ControlToValidate="txtCorreo" ErrorMessage="formato correo invalido" 
                 ForeColor="Red" Font-Bold="True" Font-Size="8pt" ValidationGroup="Enviar" ></asp:RegularExpressionValidator>
            <br />
            <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/imagenes/boton.agregar.gif" OnClick="btnAgregar_Click" />
           
        </div>
    </div>
    <br />
    <div class="row">

        <div class="col-md-12">
            
            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False"  EmptyDataText="There are no data records to display." Font-Size="9pt" >
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                            <asp:TemplateField HeaderText="Destinatario">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCorreo" runat="server" Text='<%# Eval("correo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
           
            <asp:ImageButton ID="btnEnviar" runat="server" ImageUrl="~/imagenes/boton.enviar.gif" OnClick="btnEnviar_Click" ValidationGroup="Enviar" />
            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEnviar"></cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEnviar"
            OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
            Mensaje
            </div>
            <div class="body">
            ¿Deseas enviar correos?
            </div>
            <div class="footer" align="right">
            <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
            <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
            </div>
            </asp:Panel>
        </div>
       
    </div>
    <div class="row">
        <div class="col-md-12">
           
        </div>
       
    </div>
    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>ENVIAR APROBACION </b>
            </div>
        </div>
    </div>
</asp:Content>

