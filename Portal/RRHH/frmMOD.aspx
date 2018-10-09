<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmMOD.aspx.cs" Inherits="RRHH_frmMOD" %>


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
        function SoloNum(e) {
            var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
            return ((key_press > 47 && key_press < 58) || key_press == 46);
            // el  "|| key_press == 46" es para incluir el punto ".", si borramos solo ingresara enteros 
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
            <asp:Panel ID="PanelBuscar" runat="server">
            <table style="width:100%" class="">
                <tr>
                    <td style="width: 50px; text-align: center">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Lista.png" 
                        Width="50px" />
                    </td>
                    <td class="headerText">
                        REGISTRO MOVIMIENTOS MOD
                    </td>

                      <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/requerimiento.png" onclick="btnRequerimiento_Click" 
                        ToolTip="Registro Requerimiento" Width="52px" Height="50px" />
                    </td>

                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnPersonal" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_5.png" onclick="btnPersonal_Click" 
                        ToolTip="Registro Postulante" Width="52px" Height="50px" />
                    </td>
                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnControl" runat="server" CausesValidation="False" 
                            Height="50px" ImageUrl="~/imagenes/Indicadores_2.png" 
                            onclick="btnControl_Click" ToolTip="Control MOD" Width="55px" />
                    </td>
                    
                    <td style="width: 50px; text-align: center">
                <asp:ImageButton ID="btnSeguimiento" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_4.png" 
                        onclick="btnSeguimiento_Click" ToolTip="Seguimiento MOD" Width="50px" /></td>
                   
                    <td style="width: 50px; text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
            <div  class="sky-form">
                <table class="style1">
                    <tr>
                        <td colspan="5" align="center" style="width: 40%" width="20%">
                            <div class="fondoCabeceraSubtitulo">
                                <h2 class="subtitulodelgado">
                                    BÚSQUEDA DE PERSONAL 
                                </h2>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                        &nbsp;</td>
                        <td width="20%">
                            <label class="EtiquetaNegrita">DNI</label>
                            <asp:TextBox ID="txtDniBusqueda" runat="server" Width="95%" MaxLength="12" 
                              ></asp:TextBox>
                        </td>
                        <td colspan="2" style="width: 40%" width="20%">
                            <label class="EtiquetaNegrita">Apellidos y Nombres</label>
                            <asp:TextBox ID="txtPersonal" runat="server" AutoPostBack="True" 
                            ontextchanged="txtPersonal_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txtPersonal_AutoCompleteExtender" 
                        runat="server" CompletionInterval="10" CompletionListCssClass="CompletionList" 
                        CompletionListHighlightedItemCssClass="CompletionListHighlightedItem" 
                        CompletionListItemCssClass="CompletionListItem" DelimiterCharacters="" 
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetPersonal" 
                        ServicePath="" TargetControlID="txtPersonal">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td width="20%" align="center">
                            <asp:ImageButton ID="btnBuscador" runat="server" Height="40px" 
                            ImageUrl="~/imagenes/Buscador.png" ToolTip="Consultar Personal" 
                            Width="40px" onclick="btnBuscador_Click" />
                        </td>
                    </tr>
                    </table>
                    </asp:Panel>
                     <table style="width:100%" class="">
                    <tr>
                        <td colspan="5" align="center" style="width: 40%" width="20%">
                            <div class="fondoCabeceraSubtitulo">
                                <h2 class="subtitulodelgado">
                                    DATOS DEL PERSONAL 
                                </h2>
                            </div>
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
                       
                       
                       </td>
                        <td width="20%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <asp:Panel ID="PanelDatos" runat="server" Visible="False" Width="100%">
                                <table class="style1">
                                    <tr>
                                        <td width="20%">
                                        &nbsp;</td>
                                        <td align="left" width="20%">
                                       
                                            <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="True" 
                                                Enabled="False" oncheckedchanged="chkEstado_CheckedChanged" 
                                                Text="Pendiente Aprobación"   />
                                       
                                        </td>
                                        <td align="left" colspan="2" style="width: 40%" width="20%">
                                            <asp:Label ID="lblIde_MOD" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblIdPersonal" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblProcesoActual" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td width="20%">
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        &nbsp;</td>
                                        <td width="20%" align="left">
                                        <label class="EtiquetaNegrita"> Estado</label>
                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl">
                                            </asp:DropDownList></td>
                                        <td width="20%"  align="left">
                                        <label class="EtiquetaNegrita"> Requerimiento</label>
                                            <asp:TextBox ID="txtRequerimiento" runat="server" Width="96%"></asp:TextBox>
                                            </td>
                                        <td width="20%" align="left">
                                          <label class="EtiquetaNegrita"> Item</label>
                                            <asp:TextBox ID="txtItem" runat="server" Width="96%"></asp:TextBox></td>
                                        <td width="20%">
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td align="left" width="20%">
                                            <label class="EtiquetaNegrita">
                                            Dni</label>
                                            <asp:TextBox ID="txtDNI" runat="server" Width="95%"></asp:TextBox>
                                        </td>
                                        <td align="left" colspan="2" style="width: 40%" width="20%">
                                            <label class="EtiquetaNegrita">
                                            Colaborador</label>
                                            <asp:TextBox ID="txtNombre" runat="server" Width="98%"></asp:TextBox>
                                        </td>
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        &nbsp;</td>
                                        <td width="20%" align="left">
                                            <label class="EtiquetaNegrita">
                                            Empresa</label>
                                            <asp:DropDownList ID="ddlEmpresas" runat="server" CssClass="ddl" 
                                            AutoPostBack="True" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" align="left">
                                            <label class="EtiquetaNegrita">
                                            Proyecto</label>
                                            <asp:DropDownList ID="ddlObra" runat="server" CssClass="ddl" 
                                            AutoPostBack="True" onselectedindexchanged="ddlObra_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" align="left">
                                            <label class="EtiquetaNegrita">
                                            Centro de Costo</label>
                                            <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" 
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        &nbsp;</td>
                                        <td width="20%" align="left">
                                            <label class="EtiquetaNegrita">
                                            Fuente</label>
                                            <asp:DropDownList ID="ddlFuente" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" align="left">
                                            <label class="EtiquetaNegrita">
                                            Cargo</label>
                                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" align="left">
                                            <label class="EtiquetaNegrita">
                                            Especialidad</label>
                                            <asp:DropDownList ID="ddlCargo" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        &nbsp;</td>
                                        <td align="left" width="20%">
                                                    <label class="EtiquetaNegrita">
                                            Fecha Aprobación</label>
                                            <asp:TextBox ID="txtFechaAprobacion" runat="server" Width="96%"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                             TargetControlID="txtFechaAprobacion"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaAprobacion" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                                        
                                        </td>
                                        <td align="left" width="20%">
                                            <label class="EtiquetaNegrita"> Fecha Viaje</label>
                                            <asp:TextBox ID="txtViaje" runat="server" Width="96%"></asp:TextBox>
                                             <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                             TargetControlID="txtViaje"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtViaje" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                                       
                                        </td>
                                        <td align="left" width="20%">
                                             <label class="EtiquetaNegrita">
                                            CMOD</label>
                                            <asp:DropDownList ID="ddlCMOD" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        &nbsp;</td>
                                        <td width="20%" align="left">
                                         <label class="EtiquetaNegrita"> Exámen Médico</label>
                                        <asp:TextBox ID="txtMedico" runat="server" Width="96%"></asp:TextBox>
                                             <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                             TargetControlID="txtMedico"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMedico" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                                  
                                        </td>
                                        <td width="20%" align="left">
                                        <asp:CheckBox ID="chkAtendido" runat="server" AutoPostBack="True" 
                                                Enabled="true" oncheckedchanged="chkAtendido_CheckedChanged" 
                                                Text="Reclutamiento Atendido" />
                                    </td>
                                        <td width="20%" align="left">
                                          
                                        </td>
                                        <td width="20%" style="visibility: hidden">
                                            <asp:TextBox ID="txtSueldo" runat="server" Width="96%"></asp:TextBox>
                                        </td>
                                    </tr>
                     
                              
                                    <tr>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td align="left" colspan="3" style="width: 40%">
                                         <label class="EtiquetaNegrita"> Comentarios</label>
                                            <asp:TextBox ID="txtComentarios" runat="server" CssClass="textarea" 
                                                Height="80px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td align="left" colspan="3" style="width: 40%">
                                            &nbsp;</td>
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td align="center" colspan="3" style="width: 40%">
                                            <asp:Button ID="btnRegistrar" runat="server" CssClass="buttonVerde" 
                                                Text="Registrar" Width="30%" onclick="btnRegistrar_Click" />
                                                 &nbsp;
                                            <asp:Button ID="btnRegresar" runat="server" CssClass="buttonVerde" 
                                                Text="Regresar" Width="30%" onclientclick="window.close();" />
                                    
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
                                        <td width="20%">
                                        &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                        
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
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                      <tr style="visibility: hidden">
                                        <td width="20%">
                                            &nbsp;</td>
                                       
                                        <td align="left" width="20%">
                                         
                                          <label class="EtiquetaNegrita" > Fecha Salida</label>
                                            <asp:TextBox ID="txtTermino" runat="server" Width="96%"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                             TargetControlID="txtTermino"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTermino" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                      
                                        </td>
                                        <td align="left" width="20%">
                                         
                                            <label class="EtiquetaNegrita"> Motivo de Salida</label>
                                            <asp:DropDownList ID="ddlSalida" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                        <asp:HiddenField ID="HidRegistro" runat="server" />
                <cc1:ModalPopupExtender ID="ModalRegistro" 
                                 runat="server" 
                                 
                                 
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
                                    <td align="center" width="40%" colspan="2" style="width: 80%">
                                        <asp:Label ID="lblPersonal" runat="server" CssClass="EtiquetaNegrita"></asp:Label>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="center" colspan="2" style="width: 80%" width="40%">
                                        <asp:GridView ID="gridPersonal" runat="server" AutoGenerateColumns="False" 
                                            CssClass="mGridAzul" DataKeyNames="DES_DNI,ID_MOD">
                                            <Columns>
                                                <asp:BoundField DataField="Row" HeaderText="Row" SortExpression="Row" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" 
                                                    SortExpression="ESTADO" />
                                                <asp:BoundField DataField="PROYECTO" HeaderText="Proyecto" 
                                                    SortExpression="PROYECTO" />
                                                <asp:BoundField DataField="DES_REQUERIMIENTO" HeaderText="Requerimiento" 
                                                    SortExpression="DES_REQUERIMIENTO" />
                                                               <asp:BoundField DataField="DES_ITEM" HeaderText="Item" 
                                                    SortExpression="DES_ITEM" />
                                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" 
                                                    SortExpression="EMPRESA" />
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnSeleccionar" runat="server" 
                                                            CommandArgument='<%# Eval("ID_MOD") %>' ImageUrl="~/imagenes/pencil_add.ico" 
                                                            ToolTip="Seleccionar" OnClick="Seleccionar_MOD"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Anular">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnAnular" runat="server" 
                                                            CommandArgument='<%# Eval("ID_MOD") %>' ImageUrl="~/imagenes/delete.png" 
                                                            ToolTip="Anular" OnClick="Anular_MOD"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="center" colspan="2" style="width: 80%" width="40%">
                                        <asp:Button ID="btnCerrar" runat="server" CssClass="buttonNegro" 
                                            Text="Cancelar" Visible="False" Width="30%" onclick="btnCerrar_Click" />
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="right">
                                        <asp:Button ID="btnNo" runat="server" CssClass="buttonRojo" Text="Cancelar" 
                                            Width="30%" onclick="btnNo_Click" />
                                    </td>
                                    <td width="40%" align="left">
                                        <asp:Button ID="btnAsignar" runat="server" CssClass="buttonRojo" 
                                            onclick="btnAsignar_Click" Text="Nuevo Proceso" Width="40%" />
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
    






