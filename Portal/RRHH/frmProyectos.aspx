<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmProyectos.aspx.cs" Inherits="RRHH_frmProyectos" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Usuarios.png" 
                        Width="50px" />
                </td>
                <td class="headerText">
                   REGISTRO DE PROYECTOS
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnObra" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/maquinaria1.png" onclick="btnObra_Click" 
                        ToolTip="Registro Proyectos" Width="50px" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnPersonal" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_5.png" ToolTip="Registro Postulante" 
                        Width="52px" Height="50px" onclick="btnPersonal_Click" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnControl" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_2.png" ToolTip="Control MOI" 
                        Width="55px" onclick="btnControl_Click" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnReportes" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_3.png" onclick="btnReportes_Click" 
                        ToolTip="Reporte MOI" Width="50px" />
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
    






