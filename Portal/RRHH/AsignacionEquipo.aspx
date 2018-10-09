<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="AsignacionEquipo.aspx.cs" Inherits="RRHH_AsignacionEquipo" %>


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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/registro.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                Asignación de Recursos
                </td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
                <td style="width: 50px; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
   
         
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
            <td align="center" colspan="5">
                <asp:Panel ID="PanelBuscar" runat="server">
                    <table class="style1">
                        <tr>
                            <td align="center" width="20%">
                                &nbsp;</td>
                            <td align="center" width="20%">
                                <asp:Label ID="Label1" runat="server" CssClass="EtiquetaNegrita" 
                                    Text="Ingresar Numero de Ticket"></asp:Label>
                            </td>
                            <td align="center" width="20%">
                                <asp:TextBox ID="txtNroTicket" runat="server" Width="95%"></asp:TextBox>
                            </td>
                            <td align="center" width="20%">
                                <asp:ImageButton ID="btnBuscar" runat="server" 
                                    ImageUrl="~/imagenes/boton.buscar.gif" onclick="btnBuscar_Click" />
                            </td>
                            <td align="center" width="20%">
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
                <tr>
                    <td colspan="5">
                    <asp:Panel ID="PanelTicket" runat="server" Visible="False">
                    <table class="style1">
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
                            <td width="10%">
                                &nbsp;</td>
                            <td align="right" width="10%">
                                   <label class="EtiquetaNegrita">Cento de Costo :</label></td>
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
                            <td class="style2" width="10%">
                            </td>
                            <td align="right" class="style2" width="10%">
                            <label class="EtiquetaNegrita">Fecha de Ingreso</label>
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
                                <asp:Label ID="lblRequerimientoPersonal" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td align="right" class="style2" width="10%">
                                &nbsp;</td>
                            <td align="left" class="style2" width="30%">
                                <asp:Label ID="lblIdAsignacion" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                            <td align="center" class="style2" colspan="4">
                                <asp:TextBox ID="txtObserva" runat="server" Height="100px" TextMode="MultiLine" Width="100%"></asp:TextBox>
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
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="left" colspan="4">
                                <asp:Label ID="Label3" runat="server" CssClass="EtiquetaUsuarioNombre" 
                                    Font-Size="11pt" Text="Recursos Asignados :"></asp:Label>
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
                            <td colspan="6" align="center">
                                <asp:Label ID="lblMensaje" runat="server" CssClass="EtiquetaNegrita" 
                                    ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table class="style1">
                                <tr>
                    <td width="20%">
                    &nbsp;</td>
                    <td width="20%" valign="top" colspan="2" style="width: 40%">
                    <label class="EtiquetaNegrita">
                        Ubicacion</label>
                        <asp:RadioButtonList ID="RdoUbicacion" runat="server" 
                            RepeatDirection="Horizontal" CssClass="EtiquetaNegrita">
                        </asp:RadioButtonList>
                          </td>
                    <td width="20%" valign="middle" colspan="2" style="width: 40%">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                              ControlToValidate="RdoUbicacion" CssClass="EtiquetaNegrita" 
                              ErrorMessage="Elegir Ubicacion (*)" validationgroup="Validar" 
                              ForeColor="Red" /></td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td valign="top" width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="rdoEquipo" CssClass="EtiquetaNegrita" 
                            ErrorMessage="Elegir Equipo(*)" ForeColor="Red" validationgroup="Validar" />
                    </td>
                    <td valign="top" width="20%">
                        <asp:CustomValidator ID="CustomValidator1" runat="server" 
                            ClientValidationFunction="ValidateCheckBoxList" CssClass="EtiquetaNegrita" 
                            ErrorMessage="Elegir Software(*)" ForeColor="Red" validationgroup="Validar" />
                    </td>
                    <td colspan="2" style="width: 40%" valign="top" width="20%">
                        <asp:CustomValidator ID="CustomValidator2" runat="server" 
                            ClientValidationFunction="ValidateCheckBoxList2" CssClass="EtiquetaNegrita" 
                            ErrorMessage="Elegir Item(*)" ForeColor="Red" validationgroup="Validar" />
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td valign="top" width="20%">
                        <label class="EtiquetaNegrita">
                        Equipo</label>
                        <asp:RadioButtonList ID="rdoEquipo" runat="server" CssClass="EtiquetaNegrita">
                        </asp:RadioButtonList>
                    </td>
                    <td valign="top" width="20%">
                        <label class="EtiquetaNegrita">
                        Software</label>
                        <asp:CheckBoxList ID="CheckSoftware" runat="server">
                        </asp:CheckBoxList>
                    </td>
                    <td colspan="2" style="width: 40%" valign="top" width="20%">
                        <label class="EtiquetaNegrita">
                        Otros</label>
                        <asp:CheckBoxList ID="CheckOtros" runat="server">
                        </asp:CheckBoxList>
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
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%" align="center">
                                            <asp:ImageButton ID="btnGuardar" runat="server"
                                                ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnGuardar_Click"
                                                ValidationGroup="Validar" />
                                            <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
                                            <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnGuardar"
                                                OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                <div class="header">
                                                    Advertencia
                                                </div>
                                                <div class="body">
                                                    Actualizar Recursos, Desea Continuar?
                                                </div>
                                                <div class="footer" align="right">
                                                    <asp:Button ID="btnYes" runat="server" Text="Si" CssClass="yes" />
                                                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
                                                </div>
                                            </asp:Panel>
                                        </td>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="right" width="10%">
                                &nbsp;</td>
                            <td align="left" width="20%">
                                &nbsp;</td>
                            <td align="right" width="10%">
                                &nbsp;</td>
                            <td align="left" width="30%">
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
    




