<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="AsignarRecurso.aspx.cs" Inherits="RRHH_AsignarRecurso" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
  
    		function doAlert( mensaje)
		{
		    var msg = new DOMAlert(
			{
			    title: 'Mensaje del Sistema ',
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
        function goBack()
      {
      window.history.back()
      }
      function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=CheckSoftware.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
     function ValidateCheckBoxList2(sender, args) {
        var checkBoxList = document.getElementById("<%=CheckOtros.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
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
         

            <table class="style1">
                <tr>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="left" width="20%">
                        <asp:Label ID="lblIdAsignacion" runat="server" CssClass="EtiquetaNegrita" 
                            Visible="False"></asp:Label>
                    </td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" bgcolor="#999999" class="headerText">
                     <asp:Label ID="Label2" runat="server" Text=" Requerimiento de Personal " 
                            ForeColor="White" ></asp:Label>
                       
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
                        <label class="EtiquetaNegrita">
                        Requerimiento</label>
                        <asp:TextBox ID="txtRequerimiento" runat="server" Width="93%"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <label class="EtiquetaNegrita">
                        Fecha</label>
                        <asp:TextBox ID="txtFecha" runat="server" Width="93%"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <label class="EtiquetaNegrita">
                        Centro de Costo</label>
                        <asp:TextBox ID="txtCentro" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                    &nbsp;</td>
                    <td colspan="3" style="width: 40%">
                        <label class="EtiquetaNegrita">
                        Proyecto</label>
                        <asp:TextBox ID="txtProyecto" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td width="20%">
                    &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td colspan="3" style="width: 40%">
                    <label class="EtiquetaNegrita">Personal</label>
                        <asp:TextBox ID="txtPersonal" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td colspan="3" style="width: 40%">
                         <label class="EtiquetaNegrita">Ocupacion</label>
                        <asp:TextBox ID="txtOcupacion" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td width="20%">
                    &nbsp;</td>
                    <td width="20%">
                   <label class="EtiquetaNegrita">Documento</label>
                        <asp:TextBox ID="txtDNI" runat="server" ReadOnly="True" Width="95%"></asp:TextBox></td>
                    <td width="20%">
                      <label class="EtiquetaNegrita">Fecha Ingreso</label>
                        <asp:TextBox ID="txtIngreso" runat="server"></asp:TextBox></td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                    &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td colspan="3" style="width: 40%">
                       
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                     <td colspan="5" bgcolor="#999999" class="headerText">
                     <asp:Label ID="Label3" runat="server" Text="Asignación de Recursos " 
                            ForeColor="White" ></asp:Label>

                    </td>
                </tr>
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
                    <td valign="top" width="20%" align="center" colspan="3" style="width: 40%">
                        <asp:Label ID="lblMensaje" runat="server" CssClass="EtiquetaNegrita" 
                            ForeColor="Red"></asp:Label>
                   </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td align="center" colspan="3" style="width: 40%" valign="top" width="20%">
                   <label class="EtiquetaNegrita">Observaciones</label></td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%" align="center" colspan="5" style="width: 40%">
                    
                        <asp:TextBox 
                            ID="txtObervacion" runat="server" 
                            Height="100px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="20%" align="center">
                        &nbsp;</td>
                    <td width="20%" align="center">
                        <asp:ImageButton ID="btnCancelar" runat="server" 
                            ImageUrl="~/imagenes/boton.cancelar.gif"  
                            onclick="btnCancelar_Click" Visible="False" />
                            <cc1:ConfirmButtonExtender ID="cbDelete" runat="server" DisplayModalPopupID="mpeDelete" TargetControlID="btnCancelar">
                            </cc1:ConfirmButtonExtender>
                            <cc1:ModalPopupExtender ID="mpeDelete" runat="server" PopupControlID="pnlPopupDelete" TargetControlID="btnCancelar"
                            OkControlID="btnSi" CancelControlID="btnNot" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlPopupDelete" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                            Advertencia
                            </div>
                            <div class="body">
                            Desea Anular Asignación de Recursos, Continuar?
                            </div>
                            <div class="footer" align="right">
                            <asp:Button ID="btnSi" runat="server" Text="Si" CssClass="yes" />
                            <asp:Button ID="btnNot" runat="server" Text="No" CssClass="no" />
                            </div>
                            </asp:Panel>
                    </td>
                    <td width="20%" align="center">
                        <asp:ImageButton ID="btnRegistrar" runat="server" 
                            ImageUrl="~/imagenes/boton.guardar.gif" onclick="btnRegistrar_Click" 
                            validationgroup="Validar" Visible="False" />
                    </td>
                    <td width="20%" align="center">
                        <asp:ImageButton ID="btnEnviar" runat="server" 
                            ImageUrl="~/imagenes/boton.enviar.gif" onclick="btnEnviar_Click" 
                            Visible="False" />
                        <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEnviar"></cc1:ConfirmButtonExtender>
                        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEnviar"
                            OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                                Advertencia
                            </div>
                            <div class="body">
                                Verificar la Asignación de Recursos, Desea Continuar?
                            </div>
                            <div class="footer" align="right">
                                <asp:Button ID="btnYes" runat="server" Text="Si" CssClass="yes" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
                            </div>
                        </asp:Panel>
                    </td>
                    <td width="20%" align="center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="5">
                        <asp:Panel ID="PanelEstados" runat="server" Visible="False">
                            <table class="style1">
                                <tr>
                                   <td bgcolor="#999999" class="headerText">
                                     <asp:Label ID="Label1" runat="server" Text="Control de Recursos " 
                                    ForeColor="White" ></asp:Label>

                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblTicket" runat="server" CssClass="EtiquetaNegrita" 
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DataList ID="DataListRecursos" runat="server" DataKeyField="IDE_DETALLE" 
                                            Width="100%">
                                            <ItemTemplate>
                                                <table class="style1">
                                                    <tr>
                                                        <td align="center" width="5%">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/pto.png" />
                                                        </td>
                                                        <td align="left" width="25%">
                                                            <asp:Label ID="lblRecurso" runat="server" CssClass="EtiquetaNegrita" 
                                                                Text='<%# Eval("DES_ASUNTO") %>'></asp:Label>
                                                        </td>
                                                        <td align="left" width="50%">
                                                            <asp:RadioButtonList ID="RadioEstados" runat="server" 
                                                                CssClass="EtiquetaNegrita" Enabled="False" RepeatDirection="Horizontal">
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" width="30%">
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
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                    <td align="center" width="20%">
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviar" />
            <asp:PostBackTrigger ControlID="btnCancelar" />
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
    




