<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="RO_Opciones.aspx.cs" Inherits="OPERACIONES_RO_Opciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                MANTENIMIENTO RO
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
        <div  class="sky-form">
             <br />
        </div>
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
    




