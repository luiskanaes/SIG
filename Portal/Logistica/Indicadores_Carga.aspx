<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Indicadores_Carga.aspx.cs" Inherits="Logistica_Indicadores_Carga" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../Styles/sky-forms.css" rel="stylesheet" type="text/css" />

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
                if (!jsIsUserFriendlyChar(key, "NoDecimals")) {
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <img alt="" src="../imagenes/loading3.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div  class="sky-form">
     <table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                    INDICADORES - CARGA ARCHIVOS
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnConsultas" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Consultar.png" onclick="btnConsultas_Click" 
                        ToolTip="Consultar Indicadores" Width="50px" />
                </td>
            </tr>
            <tr>
                    <td style="text-align: center" align="center" colspan="3">
                        <hr />
                        <asp:Label ID="lblMensaje" runat="server" CssClass="EtiquetaNegrita"></asp:Label>
                        <br />
                    </td>
                </tr>
        </table>

    <table class="style1">
        <tr>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%" align="center">
                <asp:Button ID="btnProcesar" runat="server"  
                    onclick="btnProcesar_Click" Text="Procesar" validationgroup="Validar" 
                    Width="65%" />
            </td>
        </tr>
        <tr>
                        <td width="25%" align="right">
                            <asp:Label ID="Label1" runat="server" CssClass="EtiquetaNegrita" Text="Asignacion :" 
                    Font-Size="13pt"></asp:Label>
                        </td>
                        <td width="25%" colspan="2">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" 
                    Width="100%" />
                        </td>
                        <td width="25%">
                            <asp:Image ID="img1" runat="server" ImageUrl="~/imagenes/check.png" 
                    Visible="False" />
                            <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ErrorMessage="Adjuntar archivo."
                        ControlToValidate="FileUpload1" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    </tr>
        <tr>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
        </tr>
    </table>
 </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProcesar" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        window.onsubmit = function () {
            if (Page_IsValid) {
                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, 100);
            }
        }
    </script>
</asp:Content>




