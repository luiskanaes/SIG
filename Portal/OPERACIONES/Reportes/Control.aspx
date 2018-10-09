<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Control.aspx.cs" Inherits="OPERACIONES_Reportes_Control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    document.onselectstart = function () { return false; }
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
    <script type="text/javascript" src="../../js/jsUpdateProgress.js"></script>
    <br />
			
        <table style="width:100%" >
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Tools.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                    CONTROL CIERRE DE PROCESOS CJI3
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnCargar" runat="server" 
                        ImageUrl="~/imagenes/regresar.png" Width="50px" 
                        ToolTip="Retornar" onclick="btnCargar_Click" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td >
                    <hr /></td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
            </tr>
        </table>
        <div  class="sky-form">
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
                      <label class="EtiquetaNegrita">Año</label>
                             <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl">
                             </asp:DropDownList>
                             <asp:CustomValidator ID="CustomValidator3" runat="server" 
                             ClientValidationFunction="ValidaDDL" ControlToValidate="ddlAnio" 
                             ErrorMessage="(*)" ValidateEmptyText="True" 
                                validationgroup="Consultar" CssClass="errorMessage"></asp:CustomValidator></td>
                     <td width="25%">
                     <label class="EtiquetaNegrita">Mes</label>
                     <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl">
                             </asp:DropDownList>
                             <asp:CustomValidator ID="CustomValidator1" runat="server" 
                             ClientValidationFunction="ValidaDDL" ControlToValidate="ddlMes" 
                             ErrorMessage="(*)" ValidateEmptyText="True" 
                                validationgroup="Consultar" CssClass="errorMessage"></asp:CustomValidator></td>
                     <td width="25%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td colspan="2" >
                     <label class="EtiquetaNegrita">Ingresar Sustento</label>
                         <asp:TextBox ID="txtsustento" runat="server" CssClass="textarea" Height="100px" 
                             TextMode="MultiLine" Width="100%"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td colspan="2" style="width: 50%" width="25%" align="center">
                      <asp:RequiredFieldValidator ID="reqCisterna" runat="server" 
                            controltovalidate="txtsustento" CssClass="errorMessage" 
                            errormessage="Ingresar Sustento" validationgroup="Consultar" />
                            </td>
                     <td width="25%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td align="center" colspan="2" >
                      <asp:Button runat="server" Text="Eliminar Proceso" ID="btnProcesar" 
                                    onclick="btnProcesar_Click" validationgroup="Consultar" 
                                 CssClass="buttonAzul"></asp:Button></td>
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
                    <img src="../../imagenes/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>
    





