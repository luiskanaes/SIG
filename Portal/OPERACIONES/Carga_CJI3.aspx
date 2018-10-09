<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Carga_CJI3.aspx.cs" Inherits="OPERACIONES_Carga_CJI3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



<script type="text/javascript">
   
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
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 10%;
        }



    </style>
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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/registro.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                    CARGA CJI3
                </td>
                <td style="width: 60px; text-align: center">
                    <asp:ImageButton ID="btnTC" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/CambioTC.png"  
                        ToolTip="Tipo de Cambio" Visible="False" Width="50px" 
                        onclick="btnTC_Click" />
                </td>
                <td style="width: 60px; text-align: center">
                    <asp:ImageButton ID="btnAdmin" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Eliminar.png" onclick="btnAdmin_Click" 
                        ToolTip="Revertir Proceso" Visible="False" Width="50px" />
                </td>
                <td style="width: 60px; text-align: center">
                    <asp:ImageButton ID="btnConsultar" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/GraficoVentas4.png" onclick="btnConsultar_Click" 
                        ToolTip="Consultar CJI3" Width="60px" />
                </td>
            </tr>
            <tr>
                    <td style="text-align: center" align="center" colspan="5">
                        <hr />
                        <asp:Label ID="lblMensaje" runat="server" CssClass="EtiquetaNegrita"></asp:Label>
                        <br />
                    </td>
                </tr>
        </table>

            <table class="style1" width="25%">
                <tr>
                    <td width="25%">
                        <asp:Label ID="Label2" runat="server" CssClass="EtiquetaNegrita" 
                            Text="Adjuntar archivo"></asp:Label>
                    </td>
                    <td width="25%" align="left">
                        <asp:Label ID="Label1" runat="server" CssClass="EtiquetaNegrita" 
                            Text="Año"></asp:Label>
                    </td>
                    <td width="25%">
                     <asp:Label ID="Label3" runat="server" CssClass="EtiquetaNegrita" 
                            Text="Mes"></asp:Label></td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="25%">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" 
                            Width="80%" />
                    </td>
                    <td align="left" width="25%">
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td width="25%" align="left">

                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl" Width="90%">
                        </asp:DropDownList>
                                   
                    </td>
                    <td width="25%">
                        <asp:Button ID="btnProcesar" runat="server" 
                            onclick="btnProcesar_Click" Text="Procesar archivo" validationgroup="Validar" 
                            Width="80%" />
                    </td>
                </tr>
                <tr>
                    <td width="25%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Required" ControlToValidate="FileUpload1"
    runat="server" Display="Dynamic" ForeColor="Red" />
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.XLS)$"
    ControlToValidate="FileUpload1" runat="server" ForeColor="Red" ErrorMessage="Seleccione archivo del formato Libro 97-2003 (*.xls)"
    Display="Dynamic" validationgroup="Validar" />
                     <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ErrorMessage="Please choose a file."
                        ControlToValidate="FileUpload1" validationgroup="Validar" CssClass="errorMessage"/>
                        </td>
                    <td width="25%" align="center">
                        
                    </td>
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
                }, updateProgress.get_displayAfter());
            }
        }
    </script>
</asp:Content>

