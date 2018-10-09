<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="AlmacenImportar.aspx.cs" Inherits="Logistica_AlmacenImportar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    
                    <asp:Image ID="Image1" runat="server" ImageUrl ="~/imagenes/Loading3.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <br />
			
        <table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/registro.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                CARGA ALMACEN
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/imagenes/regresar.png" PostBackUrl="~/Logistica/Almacen_Carga.aspx" 
                        ToolTip="Regresar" Width="50px" Visible="False" />
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
                     <td align="right" width="20%">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Elegir Empresa"
                        ControlToValidate="RdoEmpresa" validationgroup="Validar" CssClass="errorMessage"/></td>
                     <td colspan="2" width="20%">
                     <asp:RadioButtonList ID="RdoEmpresa" runat="server" CssClass="EtiquetaNegrita" 
                    Font-Size="12pt" RepeatDirection="Horizontal" Width="250px">
                                <asp:ListItem Value="SSK" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="SKEX"></asp:ListItem>
                            </asp:RadioButtonList>
                        
                     </td>
                     <td align="center" width="20%">
                      
                     </td>
                     <td align="center" width="15%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td align="right" width="20%">
                       
                         <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" 
                             ControlToValidate="FileUpload1" CssClass="errorMessage" 
                             ErrorMessage="Adjuntar archivo." validationgroup="Validar" /><asp:Image ID="img1" runat="server" ImageUrl="~/imagenes/check.png" 
                             Visible="False" /></td>
                     <td colspan="2" width="20%">
                         <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ddl" Width="100%" />
                     </td>
                     <td width="20%" align="center">
                         <asp:Button ID="btnProcesar" runat="server" CssClass="buttonRojo" 
                             onclick="btnProcesar_Click" Text="Procesar" validationgroup="Validar" 
                             Width="70%" />
                     </td>
                     <td align="center" width="15%">
                         &nbsp;</td>
                 </tr>
                
                 <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" align="left">
            <label class="EtiquetaNegrita">Fecha inicio</label>
                                        <asp:TextBox ID="txtInicio" runat="server" Width="95%" ></asp:TextBox>
                                        <cc1:maskededitextender ID="MaskedEditExtender5" runat="server"
                             TargetControlID="txtInicio"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:calendarextender ID="CalendarExtender1" runat="server" 
                    TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"  CssClass="cal_Theme1" />
            </td>
            <td width="20%" align="left">
              <label class="EtiquetaNegrita">Fecha fin</label>
                                        <asp:TextBox ID="txtFin" runat="server" Width="95%" ></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                             TargetControlID="txtFin"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFin" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" CssClass="cal_Theme1"  />
            </td>
            <td width="20%" align="center">
                <asp:Button ID="btnConsultar" runat="server" CssClass="buttonRojo" 
                            Text="Consulta" Width="70%" onclick="btnConsultar_Click" />
            </td>
           
                     <td align="center" width="15%">
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
                     <td width="15%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td colspan="5">
                     <div style="overflow: scroll; width: 100%;  text-align: center">
                         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                         </rsweb:ReportViewer>
</div>
                     </td>
                 </tr>
             </table>
             <br />
     
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





