<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="ReporteGlobal_Consulta.aspx.cs" Inherits="Logistica_ReporteGlobal_Consulta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Templates/MPAdmin.master" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Styles/sky-forms.css" rel="stylesheet" type="text/css" />
<style type="text/css">

        
    </style>
<script type="text/javascript">
    document.onselectstart = function () { return false; }
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
        <table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Lista.png" 
                        Width="50px" />
                </td>
                <td class="headerText">
                    CONSULTA REPORTE GLOBAL
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnCargar" runat="server" 
                        ImageUrl="~/imagenes/cargar.png" Width="50px" 
                        ToolTip="Cargar archivos"  
                        CausesValidation="False" onclick="btnCargar_Click"  />
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
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ClientValidationFunction="ValidaDDL" ControlToValidate="ddlEmpresa" 
                    CssClass="errorMessage" ErrorMessage="Selecionar Empresa" 
                    ValidateEmptyText="True" validationgroup="Consultar"></asp:CustomValidator>
            </td>
            <td width="20%">
                <asp:Label ID="lblSplitProyecto" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20%">
                <asp:Label ID="lblSplitComprador" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" align="right">
                &nbsp;</td>
            <td width="20%" align="right">
                <asp:Label ID="Label1" runat="server" CssClass="EtiquetaNegrita" 
                    Text="Empresa :"></asp:Label>
            </td>
            <td width="20%">
            
                <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" 
                    CssClass="ddl" onselectedindexchanged="ddlEmpresa_SelectedIndexChanged">
                </asp:DropDownList>
            
            </td>
            <td width="20%" align="center" colspan="2" style="width: 40%">
                <asp:Button ID="btnConsultar" runat="server" 
                    Text="Consultar Reporte" Width="60%" validationgroup="Consultar" 
                    onclick="btnConsultar_Click"/>
            </td>
        </tr>
        <tr>
            <td align="right" width="20%">
                &nbsp;</td>
            <td align="right" width="20%">
                <asp:Label ID="Label13" runat="server" CssClass="EtiquetaNegrita" 
                    Text="Tipo Busqueda :"></asp:Label>
            </td>
            <td width="20%">
                <asp:RadioButtonList ID="RdOpcion" runat="server" CssClass="EtiquetaNegrita" 
                    onselectedindexchanged="RdOpcion_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">Proyecto</asp:ListItem>
                    <asp:ListItem Value="2">Comprador</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="center" colspan="2" style="width: 40%" width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" width="20%">
                &nbsp;</td>
            <td align="right" width="20%">
                <asp:Label ID="Label2" runat="server" CssClass="EtiquetaNegrita" 
                    Text="Proyectos :"></asp:Label>
            </td>
            <td width="20%">
           <asp:TextBox ID="txtProyecto" Text="Seleccionar Proyecto" runat="server" 
                    CssClass="txtbox" Width="322px"></asp:TextBox>
                <asp:Panel ID="PnlProyecto" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="cblProyectoList" runat="server" 
                        onselectedindexchanged="cblProyectoList_SelectedIndexChanged">
                       
                    </asp:CheckBoxList>
                </asp:Panel>
                 
                <br />
                <cc1:PopupControlExtender ID="PceSelectProyecto" runat="server" TargetControlID="txtProyecto"
                    PopupControlID="PnlProyecto" Position="Bottom">
                </cc1:PopupControlExtender>
                
                </td>
            <td align="center" colspan="2" style="width: 40%" width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%" align="right">
                &nbsp;</td>
            <td width="20%" align="right">
                <asp:Label ID="label12" runat="server" CssClass="EtiquetaNegrita">Compradores :</asp:Label>
            </td>
            <td width="20%">
            <asp:TextBox ID="txtComprador" Text="Seleccionar Comprador" runat="server" 
                    CssClass="txtbox" Width="322px"></asp:TextBox>
                <asp:Panel ID="PnlComprador" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="cblCompradorList" runat="server">
                       
                    </asp:CheckBoxList>
                </asp:Panel>
                 
                <br />
                <cc1:PopupControlExtender ID="PceSelectComprador" runat="server" TargetControlID="txtComprador"
                    PopupControlID="PnlComprador" Position="Bottom">
                </cc1:PopupControlExtender>
           </td>
            <td width="20%" align="center" colspan="2" style="width: 40%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" align="right">
                <asp:Label ID="label11" runat="server" CssClass="EtiquetaNegrita">Status :</asp:Label>
            </td>
            <td width="20%">
            
                <asp:TextBox ID="txtCustomer" Text="Seleccionar Status" runat="server" 
                    CssClass="txtbox" Width="322px"></asp:TextBox>
                <asp:Panel ID="PnlCust" runat="server" CssClass="PnlDesign">
                    <asp:CheckBoxList ID="cblCustomerList" runat="server">
                       
                    </asp:CheckBoxList>
                </asp:Panel>
                 
                <br />
                <cc1:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="txtCustomer"
                    PopupControlID="PnlCust" Position="Bottom">
                </cc1:PopupControlExtender>
                
            
        </td>
            <td width="20%" align="center" colspan="2" style="width: 40%">
                <asp:Button ID="btnStatus" runat="server" CausesValidation="False" 
                    onclick="btnStatus_Click" Text="Descarga Status" 
                    Width="60%" />
            </td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td align="right" width="20%">
                <asp:Label ID="label10" runat="server" CssClass="EtiquetaNegrita">Ultima 
                actualizacion :</asp:Label>
            </td>
            <td width="20%">
                <asp:Label ID="lblUpdate" runat="server" CssClass="EtiquetaNegrita" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td align="center" colspan="2" style="width: 40%" width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                <asp:Label ID="lblSplit" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                </rsweb:ReportViewer>
                
            </td>
        </tr>
        <tr>
            <td align="center" colspan="5">
                <asp:GridView ID="gvwAsignacion" runat="server" CssClass="EtiquetaSimple" 
                    Visible="False">
                </asp:GridView>
            </td>
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


