<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="MisNominaciones.aspx.cs" Inherits="RRHH_MisNominaciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-4">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/SSK_Estrella140X85.png" />
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <center>
                  <asp:Button runat="server" Text="REGRESAR" ID="btnRegresar" CausesValidation="False" OnClick="btnRegresar_Click"></asp:Button>
            </center>
           
        </div>
    </div>
    <br />
    <div class="row">

        <div class="col-lg-12 ">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDE_NOMINACION">
                <ItemTemplate>


                    <ul class="timeline">
                        <li class="timeline-inverted">
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">
                                        <%# Eval("EVALUADO_COMPLETO")%>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_NOMINACION") %>' ImageUrl="~/imagenes/Error.png" OnClick="EliminarNominacion" />
                                        <p>
                                            <small class="text-muted"><i class="fa fa-time"></i><%# Eval("FECHA")%></small>
                                        </p>
                                    </h4>
                                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                                    <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEliminar"
                                        OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                        <div class="header">
                                            Mensaje SSK
                                        </div>
                                        <div class="body">
                                            ¿Deseas eliminar nominación?
                                        </div>
                                        <div class="footer" align="right">
                                            <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                            <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="timeline-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <center>
                                                <b> <%# Eval("DES_FACTOR")%></b>
                                            </center>
                                            <br />
                                        </div>
                                        <div class="col-md-6" style="text-align: justify;">
                                           
                                            <%# Eval("SUSTENTO")%>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>

