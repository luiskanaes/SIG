<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="RO_PROYECTOS.aspx.cs" Inherits="OPERACIONES_RO_PROYECTOS" %>


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
            <script type="text/javascript" src="../js/jsUpdateProgress.js">
</script>
            <br />
            <table style="width:100%" class="">
                <tr>
                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnNuevo" runat="server" 
                        ImageUrl="~/imagenes/registro2.png" ToolTip="Agregar Nuevo Proyecto" 
                        Width="40px" onclick="btnNuevo_Click" />
                    </td>
                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="False" 
                            ImageUrl="~/imagenes/Eliminar.png" onclick="btnEliminar_Click" 
                            ToolTip="Eliminar" Width="40px" />
                    </td>
                    <td class="headerText">
                    REGISTRO DE PROYECTOS
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
                    <td colspan="7" style="text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
            <table class="style1" width="20%">
                <tr>
                    <td align="center" width="25%">
                         &nbsp;</td>
                    <td width="25%">
                         &nbsp;</td>
                    <td width="25%">
                         &nbsp;</td>
                    <td width="25%">
                         &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" width="25%">
                         &nbsp;</td>
                    <td width="25%">
                        <label class="EtiquetaNegrita">
                        Empresa</label>
                        <asp:DropDownList ID="ddlEmpresas" runat="server" AutoPostBack="True" 
                             CssClass="ddl" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td width="25%" align="center">
                         <asp:Button ID="btnListar" runat="server" 
                             onclick="btnListar_Click" Text="Listar Proyectos" Width="85%" />
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
                         <asp:GridView ID="GridProyectos" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGridAzul" PagerStyle-CssClass="pgr"
                             AlternatingRowStyle-CssClass="alt" DataKeyNames="IDE_PROYECTO" 
                             EmptyDataText="No se registra Informacion"  >
                             <AlternatingRowStyle CssClass="alt" />
                             <Columns>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:CheckBox ID="chkEliminar" runat="server" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="ID" HeaderText="Nro" SortExpression="ID" />
                                 <asp:BoundField DataField="DES_NOMBRE" HeaderText="Proyecto" 
                                     SortExpression="DES_NOMBRE" >
                                 <ItemStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="DES_CLIENTE" HeaderText="Cliente" 
                                     SortExpression="DES_CLIENTE" />
                                 <asp:BoundField DataField="DES_TIPO_CONTRATO" HeaderText="Tipo Contrato" 
                                     SortExpression="DES_TIPO_CONTRATO" />
                                 <asp:BoundField DataField="FECHA_INICIO" HeaderText="Inicio" 
                                     SortExpression="FECHA_INICIO" />
                                 <asp:BoundField DataField="FECHA_TERMINO" HeaderText="Termino" 
                                     SortExpression="FECHA_TERMINO" />
                                 <asp:BoundField DataField="Estado" HeaderText="Estado" 
                                     SortExpression="Estado" />
                                 <asp:TemplateField HeaderText="Ver">
                                     <ItemTemplate>
                                         <asp:ImageButton ID="btnEditar" runat="server" 
                                             CommandArgument='<%# Eval("IDE_PROYECTO") %>' ImageUrl="~/imagenes/pencil.ico" 
                                             ToolTip="Ver Proyecto" OnClick="Actualizar_Proyecto" 
                                             CausesValidation="False"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                               
                             <PagerStyle CssClass="pgr" />
                               
                         </asp:GridView>
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
                    <td align="center" colspan="4">
                    <asp:HiddenField ID="HidRegistro" runat="server" />
                <cc1:ModalPopupExtender ID="ModalRegistro" 
                                 runat="server" 
                                 
                                 CancelControlID="btnNo"
                                 BackgroundCssClass="modalBackground"
                                 PopupControlID="pnlPopup" 
                                 PopupDragHandleControlID="pnlPopup"
                                 TargetControlID="HidRegistro">
         </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="65%">
                            <table class="style1">
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%">
                                &nbsp;</td>
                                    <td width="40%">
                                &nbsp;</td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="left">
                                        <label class="EtiquetaNegrita">
                                        Codigo</label>
                                        <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="40%" align="center">
                                <asp:RequiredFieldValidator ID="reqCodigo" runat="server" 
                            controltovalidate="txtCodigo" CssClass="errorMessage" 
                            errormessage="Ingresar Codigo Proyecto" validationgroup="Validar" /></td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td align="left" colspan="2" style="width: 80%" width="40%">
                                        <label class="EtiquetaNegrita">
                                        Proyecto</label>
                                        <asp:TextBox ID="txtdescripcion" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td align="left" width="40%">
                                        <label class="EtiquetaNegrita">
                                        Tipo Contrato</label>
                                        <asp:TextBox ID="txtContrato" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="40%">
                                &nbsp;</td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td align="left" width="40%" colspan="2" style="width: 80%">
                                    <label class="EtiquetaNegrita"> Cliente</label>
                                        <asp:TextBox ID="txtCliente" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td align="left" width="40%">
                                      <label class="EtiquetaNegrita"> Inicio Contractual </label>
                                        <asp:TextBox ID="txtInicio" runat="server" Width="90%"></asp:TextBox>
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
                         <cc1:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                             ControlExtender="MaskedEditExtender5"
                            ControlToValidate="txtInicio"
                            EmptyValueMessage="Fecha is requerida"
                            InvalidValueMessage="Fecha es invalida"
                            Display="Dynamic"
                            EmptyValueBlurredText="*"
                            InvalidValueBlurredMessage="*"
                            ValidationGroup="MKE" CssClass="errorMessage" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                                    </td>
                                    <td width="40%" align="left">
                                     <label class="EtiquetaNegrita"> Termino Contractual</label>
                                        <asp:TextBox ID="txtTermino" runat="server"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                             TargetControlID="txtTermino"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                             ControlExtender="MaskedEditExtender5"
                            ControlToValidate="txtTermino"
                            EmptyValueMessage="Fecha is requerida"
                            InvalidValueMessage="Fecha es invalida"
                            Display="Dynamic"
                            EmptyValueBlurredText="*"
                            InvalidValueBlurredMessage="*"
                            ValidationGroup="MKE" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTermino" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                       
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" width="40%">
                                    <label class="EtiquetaNegrita"> Termino Programado</label>
                                        <asp:TextBox ID="txtProgramado" runat="server" Width="90%"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                             TargetControlID="txtProgramado"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         <cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                             ControlExtender="MaskedEditExtender5"
                            ControlToValidate="txtProgramado"
                            EmptyValueMessage="Fecha is requerida"
                            InvalidValueMessage="Fecha es invalida"
                            Display="Dynamic"
                            EmptyValueBlurredText="*"
                            InvalidValueBlurredMessage="*"
                            ValidationGroup="MKE" />
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtProgramado" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                          
                                    </td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita"> Tipo de Cambio</label>
                                        <asp:TextBox ID="txtTipoCambio" runat="server" placeholder="0.00"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" width="40%">
                                    <label class="EtiquetaNegrita">Tipo de Moneda</label>
                                        <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="ddl" Width="93%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" width="40%">
                                    
                                        </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita"> Monto Contrato Inicial</label>
                                        <asp:TextBox ID="txtMonto" runat="server" placeholder="0.00" Width="90%"></asp:TextBox>
                                    </td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita"> Monto Contrato Actual</label>
                                        <asp:TextBox ID="txtMontoContractual" runat="server" placeholder="0.00"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita">Empresa</label>
                                        <asp:DropDownList ID="ddlEmpresaEditar" runat="server" CssClass="ddl" 
                                            Width="93%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita">Estado</label>
                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" width="40%">
                                        &nbsp;</td>
                                    <td width="40%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="center">
                                        <asp:Button ID="btnNo" runat="server" CssClass="buttonRojo" 
                                    Text="Cancelar" Width="80%" onclick="btnNo_Click" />
                                    </td>
                                    <td width="40%" align="center">
                                        <asp:Button ID="btnYes" runat="server" 
                                    CssClass="buttonRojo" Text="Grabar" Width="80%" validationgroup="Validar" 
                                            onclick="btnYes_Click" />
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%">
                                &nbsp;</td>
                                    <td width="40%">
                                &nbsp;</td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
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
    







