<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="RO_REPORTE.aspx.cs" Inherits="OPERACIONES_RO_REPORTE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" language ="javascript">
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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas4.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                    RESULTADOS OPERATIVOS
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnPEP" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_4.png" ToolTip="Indicadores PEP" 
                        Width="50px" Height="50px" onclick="btnPEP_Click" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnProyecto" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_2.png" ToolTip="Proyectos" 
                        Width="50px" onclick="btnProyecto_Click" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnMantenimiento" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_5.png" 
                        ToolTip="Ingreso de Costos / Ventas" Width="50px" 
                        onclick="btnMantenimiento_Click" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnReporte" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_3.png" 
                        ToolTip="Resultados Operativos" Width="50px" onclick="btnReporte_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
  
             <table class="style1">
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td width="25%">
                         <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl" AutoPostBack="True" 
                             onselectedindexchanged="ddlMes_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>
                     <td width="25%">
                         <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl">
                         </asp:DropDownList>
                     </td>
                     <td width="25%" align="center">
                         <asp:Button ID="btnConsultar" runat="server" Text="Consultar" 
                             Width="70%" onclick="btnConsultar_Click"  />
                     </td>
                 </tr>
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td width="25%">
                      <label class="EtiquetaNegrita"> Empresa</label>
                         <asp:DropDownList ID="ddlEmpresas" runat="server" AutoPostBack="True" 
                             CssClass="ddl" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>
                     <td width="25%">
                         &nbsp;</td>
                     <td align="center" width="25%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td colspan="2" style="width: 50%" width="25%">
                         <asp:DropDownList ID="ddlProyecto" runat="server" CssClass="ddl" 
                             onselectedindexchanged="ddlProyecto_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>
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
    




