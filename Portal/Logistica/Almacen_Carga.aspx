<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Almacen_Carga.aspx.cs" Inherits="Logistica_Almacen_Carga" %>
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
<asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
     <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
    <br />
			
        <table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/registro.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                CONSULTA ALMACEN
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/imagenes/cargar.png" PostBackUrl="~/Logistica/AlmacenImportar.aspx" 
                        ToolTip="Carga Masiva" Width="50px" />
                </td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
             <table class="style1">
                 <tr>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         <asp:Label ID="lblCodigoMaterial" runat="server"></asp:Label>
                     </td>
                     <td width="20%">
                         <asp:Label ID="lblCompra" runat="server"></asp:Label>
                     </td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         <asp:TextBox ID="TextBox1" runat="server" Width="97%"></asp:TextBox>
                     </td>
                     <td width="20%">
                         <asp:TextBox ID="TextBox2" runat="server" Width="97%"></asp:TextBox>
                     </td>
                     <td width="20%">
                         <asp:TextBox ID="TextBox3" runat="server" Width="97%"></asp:TextBox>
                     </td>
                     <td width="20%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         <asp:TextBox ID="TextBox4" runat="server" Width="97%"></asp:TextBox>
                     </td>
                     <td width="20%">
                         <asp:TextBox ID="TextBox5" runat="server" Width="97%"></asp:TextBox>
                     </td>
                     <td width="20%">
                         <asp:TextBox ID="TextBox6" runat="server" Width="97%"></asp:TextBox>
                     </td>
                     <td width="20%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td align="center" width="20%">
                         <asp:Button ID="btnGrabar" runat="server" 
                             Text="Actualizar" Width="80%" />
                     </td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td align="center" colspan="5">
                         <asp:GridView ID="GridAlmacen" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGridAzul">
                         </asp:GridView>
                     </td>
                 </tr>
                 <tr>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                     <td width="20%">
                         &nbsp;</td>
                 </tr>
             </table>
             <br />
     
 </ContentTemplate>
            <Triggers>    
            </Triggers>
</asp:UpdatePanel>
 <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="../imagenes/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>
    





