<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmReporteMOI.aspx.cs" Inherits="RRHH_frmReporteMOI" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
    GenerenciaMOI.aspx<asp:UpdatePanel ID="udpHojaGestion" runat="server">
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
                    REPORTE MOI
                </td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
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
                    <asp:ImageButton ID="btnSeguimiento" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_4.png" 
                        onclick="btnSeguimiento_Click" ToolTip="Seguimiento MOI" Width="50px" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnReportes" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_3.png" onclick="btnReportes_Click" 
                        ToolTip="Reporte MOI" Width="50px" />
                </td>
            </tr>
            <tr>
                <td colspan="7" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
        <div  class="sky-form">
         
            <table class="style1">
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
                    <td width="20%">
                        &nbsp;</td>
<%--
                    <td width="20%">                    
                    <label class="EtiquetaNegrita"> Periodo Inicio </label>
                        <asp:DropDownList ID="ddlInicio" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                    <label class="EtiquetaNegrita"> Periodo Fin </label>
                        <asp:DropDownList ID="ddlFin" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                    </td>--%>

                     <td width="30%">
                     <label class="EtiquetaNegrita"> Fecha inicio </label>
                                        <asp:TextBox ID="txtInicio" runat="server" Width="95%"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                             TargetControlID="txtInicio"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                            </td>

                             <td width="30%">
                     <label class="EtiquetaNegrita"> Fecha fin </label>
                                        <asp:TextBox ID="txtFin" runat="server" Width="95%"></asp:TextBox>
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
                         
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFin" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                            </td>

                    <td width="20%" align="center">
                        <asp:Button ID="btnConsultar" runat="server" CssClass="buttonNaranja" 
                            Text="Consulta" Width="70%" onclick="btnConsultar_Click" />
                    </td>
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
                    <td width="20%" align="center">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table class="style1">
                            <tr>
                                <td width="50%">
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                                    </rsweb:ReportViewer>
                                </td>
                                <td width="50%">
                                    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="100%">
                                    </rsweb:ReportViewer>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
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
    






