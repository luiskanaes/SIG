<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Reporte_MOD_PEP.aspx.cs" Inherits="Reporte_MOD_PEP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


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
    function doAlert( mensaje)
		{
		    var msg = new DOMAlert(
			{
			    title: 'Mensaje del Sistemas :',
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
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas.png" 
                        Width="48px" />
                    </td>
                    <td class="headerText">
                        REPORTE MANO DE OBRA DIRECTA POR ELEMENTO PEP&nbsp;</td>
                    <td style="width: 50px; text-align: center">
                    </td>
                    <td style="width: 50px; text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center" align="center" colspan="3">
                        <hr />
                        <asp:Label ID="lblSplit" runat="server" Visible="False"></asp:Label>
                        <br />
                    </td>
                </tr>
            </table>
  
                <br />
                <table class="style1">
                    <tr>
                        <td width="25%">
                            <label class="label">Año</label>
                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                            <%--<asp:CustomValidator ID="CustomValidator3" runat="server" 
                             ClientValidationFunction="ValidaDDL" ControlToValidate="ddlAnio" 
                             ErrorMessage="Selecione un Año" ValidateEmptyText="True" 
                                validationgroup="Consultar" CssClass="errorMessage"></asp:CustomValidator>--%>
                        </td>
                        <td width="25%">
                           <label class="label">Mes</label>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" 
                             ClientValidationFunction="ValidaDDL" ControlToValidate="ddlAnio" 
                             ErrorMessage="Selecione un Mes" ValidateEmptyText="True" 
                                validationgroup="Consultar" CssClass="errorMessage"></asp:CustomValidator>--%>
                        </td>    
                         <td width="25%">
                         <label class="EtiquetaNegrita">Empresa</label>
                            <asp:DropDownList ID="ddlEmpresas" runat="server" 
                                AutoPostBack="True" CssClass="ddl"  >
                            </asp:DropDownList>
                         </td>   
                        <td width="25%" align="left">
                         <label class="EtiquetaNegrita">Proyectos</label>
                             <asp:TextBox ID="txtCustomer" Text="Seleccionar" runat="server" 
                    CssClass="txtbox" Width="322px" Font-Bold="True"></asp:TextBox>
                            <asp:Panel ID="PnlCust" runat="server" CssClass="PnlDesign">
                                <asp:CheckBoxList ID="CheckProyectos" runat="server" RepeatColumns="1">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <br />
                            <cc1:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="txtCustomer"
                    PopupControlID="PnlCust" Position="Bottom">
                            </cc1:PopupControlExtender>
                        </td>
                         
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                          <asp:Button runat="server" Text="Consultar" ID="btnListar" Width="30%" 
                                    onclick="btnListar_Click" validationgroup="Consultar" 
                                 CssClass="buttonAzul"></asp:Button>
                    </tr>
                    <tr>
                        <td width="25%">
                             &nbsp;</td>
                        <td width="25%" align="left">
                        </td>
                        <td width="25%" align="center">
                            </td>
                        <td width="25%">
                             &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                             &nbsp;</td>
                        <td align="left" width="25%">
                             &nbsp;</td>
                        <td align="center" width="25%">
                             &nbsp;</td>
                        <td width="25%">
                             &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                             &nbsp;</td>
                        <td align="center" width="25%">
                            <asp:Button ID="btnConsultar" runat="server"  
                                 onclick="btnConsultar_Click" Text="Costos / Ventas" Width="90%" />
                        </td>
                        <td align="center" width="25%">
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
                        <td align="center" colspan="4">
                            <asp:GridView ID="gvwAsignacion" runat="server" CssClass="EtiquetaSimple">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                           
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




