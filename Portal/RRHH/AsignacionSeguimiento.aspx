<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPWeb.master" AutoEventWireup="true" CodeFile="AsignacionSeguimiento.aspx.cs" Inherits="RRHH_AsignacionSeguimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript">
 
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
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
            <br />
    <table class="style1">
        <tr>
            <td align="right" width="20%">
              <asp:Label ID="Label4" runat="server" CssClass="EtiquetaNegrita" 
                                    Text="Bienvenido :"></asp:Label></td>
            <td align="left" colspan="3" style="width: 40%">
                <asp:Label ID="lblPersona" runat="server" CssClass="EtiquetaNegrita"></asp:Label>
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
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
            <asp:TextBox ID="txtEstados" Text="Seleccionar Estados" runat="server" 
                    CssClass="txtbox" Width="322px" Font-Bold="True"></asp:TextBox>
                            <asp:Panel ID="PnlCust" runat="server" CssClass="PnlDesign">
                                <asp:CheckBoxList ID="CheckEstados" runat="server" RepeatColumns="1">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <br />
                            <cc1:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="txtEstados"
                    PopupControlID="PnlCust" Position="Bottom">
                            </cc1:PopupControlExtender></td>
            <td width="20%" align="center">
                <asp:ImageButton ID="btnBuscar" runat="server" 
                    ImageUrl="~/imagenes/boton.buscar.gif" onclick="btnBuscar_Click" />
            </td>
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
            <td align="center" width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%" align="center" colspan="5" style="width: 40%">
                <asp:Panel ID="PanelDatos" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                    Visible="False">
                    <table class="style1">
                        <tr>
                            <td align="center" width="30%">
                                &nbsp;</td>
                            <td align="center" width="30%">
                                <asp:ImageButton ID="btnGrabar" runat="server" 
                                    ImageUrl="~/imagenes/boton.guardar.gif" onclick="btnGrabar_Click" />
                            </td>
                            <td align="center" width="30%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" width="30%">
                                &nbsp;</td>
                            <td align="center" width="30%">
                                &nbsp;</td>
                            <td align="center" width="30%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:DataList ID="DataListRecursos" runat="server" DataKeyField="IDE_DETALLE" 
                                    Width="90%">
                                    <ItemTemplate>
                                        <table class="style1">
                                            <tr>
                                                <td align="center" width="5%">
                                                    <asp:ImageButton ID="btnDatos" runat="server" 
                                                        CommandArgument='<%# Eval("NRO_TICKET") %>' ImageUrl="~/imagenes/zoom_in.png" OnClick ="Seleccionar" />
                                                </td>
                                                <td align="left" width="15%">
                                                    <asp:Label ID="lblNro" runat="server" CssClass="EtiquetaNegrita" 
                                                        Text='<%# Eval("NRO_TICKET") %>'></asp:Label>
                                                </td>
                                                <td align="left" width="15%">
                                                    <asp:Label ID="lblRecurso" runat="server" CssClass="EtiquetaNegrita" 
                                                        Text='<%# Eval("DES_ASUNTO") %>'></asp:Label>
                                                </td>
                                                <td align="left" width="40%">
                                                    <asp:RadioButtonList ID="RadioEstados" runat="server" 
                                                        CssClass="EtiquetaNegrita" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" width="40%">
                                                    <asp:Label ID="Label4" runat="server" CssClass="EtiquetaNegrita" 
                                                        Font-Size="7pt" 
                                                        Text='<%# Eval("USUARIO_ATIENDE") + " - " + Eval("FECHA_ATENCION") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
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
            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="68%">
                            <table class="style1">
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="left" colspan="4">
                                &nbsp;</td>
                            <td width="10%">
                                &nbsp;</td>
                        </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" colspan="4">
                                        <asp:Label ID="Label2" runat="server" CssClass="EtiquetaUsuarioNombre" 
                                            Font-Size="11pt" Text="Datos del Requerimiento :"></asp:Label>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="left" colspan="4">
                            <hr /></td>
                            <td width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="right" width="10%">
                            <label class="EtiquetaNegrita">Empresa :</label></td>
                            <td align="left" width="20%">
                                <asp:Label ID="lblEmpresa" runat="server" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td align="right" width="10%">
                                  <label class="EtiquetaNegrita">Cargo :</label></td>
                            <td align="left" width="30%">
                              <asp:Label ID="lblCargo" runat="server" CssClass="EtiquetaSimple"></asp:Label></td>
                            <td width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="10%">
                          </td>
                            <td align="right" width="10%">
                                 <label class="EtiquetaNegrita">Proyecto :</label></td>
                            <td align="left" width="20%">
                             <asp:Label ID="lblProyecto" runat="server" CssClass="EtiquetaSimple"></asp:Label></td>
                            <td align="right" width="10%">
                                  <label class="EtiquetaNegrita">Ubicacion :</label></td>
                            <td align="left" width="30%">
                            <asp:Label ID="lblUbicacion" runat="server" CssClass="EtiquetaSimple"></asp:Label></td>
                            <td width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="10%" colspan="2" align="right">
                                <label class="EtiquetaNegrita">
                                Cento de Costo :</label></td>
                            <td align="left" width="20%">
                            <asp:Label ID="lblCentro" runat="server" CssClass="EtiquetaSimple"></asp:Label></td>
                            <td align="right" width="10%">
                             <label class="EtiquetaNegrita">Candidato :</label></td>
                            <td align="left" width="30%">
                                <asp:Label ID="lblcandidato" runat="server" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" width="10%" colspan="2" align="right">
                                <label class="EtiquetaNegrita">
                                Fecha de Ingreso :</label>
                            </td>
                            <td align="left" class="style2" width="20%">
                                <asp:Label ID="lblFechaIngreso" runat="server" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td align="right" class="style2" width="10%">
                            <label class="EtiquetaNegrita">Dni :</label>
                            </td>
                            <td align="left" class="style2" width="30%">
                                <asp:Label ID="lblDni" runat="server" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td class="style2" width="10%">
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                            <td align="right" class="style2" width="10%">
                                &nbsp;</td>
                            <td align="left" class="style2" width="20%">
                                &nbsp;</td>
                            <td align="right" class="style2" width="10%">
                                &nbsp;</td>
                            <td align="left" class="style2" width="30%">
                                &nbsp;</td>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                            <td align="center" class="style2" colspan="4">
                                <asp:TextBox ID="txtObserva" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                        </tr>
                                <tr>
                                    <td class="style2" width="10%">
                                        &nbsp;</td>
                                    <td align="center" class="style2" colspan="4">
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/imagenes/boton.cancelar.gif" />
                                    </td>
                                    <td class="style2" width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style2" width="10%">
                                        &nbsp;</td>
                                    <td align="center" class="style2" colspan="4">
                                        &nbsp;</td>
                                    <td class="style2" width="10%">
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

