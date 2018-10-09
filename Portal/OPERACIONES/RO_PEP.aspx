<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="RO_PEP.aspx.cs" Inherits="OPERACIONES_RO_PEP" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
            height: 10%;
        }
    </style>
    <script type="text/javascript">
        document.onselectstart = function () { return false; }
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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas3.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                    INDICADORES PEP
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
        <div >
            
             <table class="style2">
                 <tr>
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
                     <label class="EtiquetaNegrita">Previsto</label>
                         <asp:DropDownList ID="ddlPrevisto" runat="server" AutoPostBack="True" 
                             CssClass="ddl" onselectedindexchanged="ddlPrevisto_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>
                     <td width="20%">
                      <label class="EtiquetaNegrita">Indicadores</label>
                         <asp:DropDownList ID="ddlIndicador" runat="server" AutoPostBack="True" 
                             CssClass="ddl" onselectedindexchanged="ddlIndicador_SelectedIndexChanged">
                         </asp:DropDownList>
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
                 </tr>
                 <tr>
                     <td align="center" colspan="4">
                         <asp:GridView ID="GridPEP" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGridAzul" PagerStyle-CssClass="pgr"
                             AlternatingRowStyle-CssClass="alt" onrowdatabound="GridPEP_RowDataBound" 
                             DataKeyNames="IDE_PEP" EmptyDataText="No se registra informacion" >
                         
                             <AlternatingRowStyle CssClass="alt" />
                         
                             <Columns>
                                 <asp:BoundField DataField="Row" HeaderText="Row" SortExpression="Row" />
                                 <asp:BoundField DataField="IDE_PEP" HeaderText="Codigo" 
                                     SortExpression="IDE_PEP" />
                                 <asp:TemplateField HeaderText="Descripcion">
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtDescripcionPep" runat="server" 
                                             Text='<%# Eval("DES_NOMBRE_PEP") %>' Width="97%"></asp:TextBox>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Indicador">
                                     <ItemTemplate>
                                         <asp:DropDownList ID="ddlGridIndicador" runat="server" CssClass="ddl">
                                         </asp:DropDownList>
                                         <asp:Label ID="lblIndicador" runat="server" Text='<%# Eval("IDE_PREVISTO") %>' 
                                             Visible="False"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Guardar">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" 
                                             CommandArgument='<%# Eval("IDE_PEP") %>' ImageUrl="~/imagenes/Save.png" 
                                             ToolTip="Guardar" Width="22px" OnClick="Actualizar_Indicador"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                             
                             <PagerStyle CssClass="pgr" />
                             
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
                    <img src="../imagenes/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>
    





