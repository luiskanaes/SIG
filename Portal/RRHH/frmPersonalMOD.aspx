<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmPersonalMOD.aspx.cs" Inherits="RRHH_frmPersonalMOD" MaintainScrollPositionOnPostback="true" %>
<%@ MasterType VirtualPath="~/Templates/MPAdmin.master" %>

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
    <style type="text/css">
        .style3
        {
            width: 11%;
        }
        .style4
        {
            width: 5%;
        }
    </style>
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
                    REGISTRO POSTULANTE MOD</td>

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
                    <asp:ImageButton ID="btnReportes" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_3.png" onclick="btnReportes_Click" 
                        ToolTip="Reporte MOD" Width="50px" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
        <div  class="sky-form">
            
            <table class="style1">
                <tr>
                    <td colspan="5" align="center" style="width: 40%" width="20%">
                    <div class="fondoCabeceraSubtitulo"><h2 class="subtitulodelgado">DATOS GENERALES</h2></div></td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td width="20%">
                        <asp:Label ID="lblCodigoPersonal" runat="server" CssClass="EtiquetaNegrita" 
                            Text="0" Visible="False"></asp:Label>
                    </td> 
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" rowspan="3" valign="middle" class="style4">
                        &nbsp;</td>
                    <td width="20%">
                    <label class="EtiquetaNegrita">Tipo Documento</label>
                        <asp:DropDownList ID="ddlDocumento" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                    
                    </td>
                    <td width="20%" align="left">
                    <label class="EtiquetaNegrita">Nro Documento</label>
                        <asp:TextBox ID="txtDNI" runat="server" Width="95%" MaxLength="12" ontextchanged="txtDNI_TextChanged" TabIndex="1"></asp:TextBox>
                        
                    </td>
                    <td width="20%">
                        
                        <asp:RequiredFieldValidator ID="reqCisterna" runat="server" 
                            controltovalidate="txtDNI" CssClass="errorMessage" errormessage="Ingresar Documento" 
                            Font-Size="8pt" validationgroup="Validar" />
                            <asp:ImageButton ID="btnBuscador" runat="server" Height="40px" 
                            ImageUrl="~/imagenes/Buscador.png" onclick="btnBuscador_Click" 
                            ToolTip="Consultar DNI" Width="40px" />
                        <asp:ImageButton ID="btnLimpiador" runat="server" Height="40px" 
                            ImageUrl="~/imagenes/Limpiador.png" onclick="btnLimpiador_Click" 
                            ToolTip="Limpiar Controles" Width="40px" />
                    </td>
                    <td width="20%" align="center" rowspan="3" valign="middle">
                        <asp:Image ID="ImgFoto" runat="server" ImageUrl="~/imagenes/Foto_Fondo.png" 
                            Width="120px" Height="120px" Visible="False" />
                    </td>
                </tr>
                <tr>
                        <td width="20%" align="left">
                    <label class="EtiquetaNegrita">Nombres</label>
                        <asp:TextBox ID="txtNombre" runat="server" Width="95%" MaxLength="30" 
                             TabIndex="2"></asp:TextBox>
                        
                    </td>
                        <td width="40%" align="left">
                    <label class="EtiquetaNegrita">Apellido Paterno</label>
                        <asp:TextBox ID="txtApePat" runat="server" Width="95%" MaxLength="30" 
                             TabIndex="3"></asp:TextBox>
                        
                    </td>
                        <td width="20%" align="left">
                    <label class="EtiquetaNegrita">Apellido Materno</label>
                        <asp:TextBox ID="txtApeMat" runat="server" Width="95%" MaxLength="30" 
                             TabIndex="4"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                     <label class="EtiquetaNegrita">Fecha Nacimiento</label>
                                        <asp:TextBox ID="txtNacimiento" runat="server" Width="95%" 
                            TabIndex="5"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                             TargetControlID="txtNacimiento"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNacimiento" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                            </td>
                    <td width="20%">
                        <label class="EtiquetaNegrita">Teléfono</label>
                        <asp:TextBox ID="txtFono" runat="server" MaxLength="30" Width="95%" TabIndex="6"></asp:TextBox>
                     </td>
                        <td width="20%">
                       <label class="EtiquetaNegrita">
                        Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" TabIndex="7" Width="95%"></asp:TextBox>
                    </td>
                    <td width="30%">
                    </td>
                </tr>
                <tr>
                    <td class="style4"> 
                        </td>
                    <td width="20%">

                     
                          <label class="EtiquetaNegrita">Cargo</label>
                        <asp:DropDownList ID="ddlCargos" runat="server" CssClass="ddl" TabIndex="8" AutoPostBack="True">
                        </asp:DropDownList>
                        
                        </td>
                    <td width="20%">
                    
                     
                          <label class="EtiquetaNegrita">Especialidad</label>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="ddl" TabIndex="9" AutoPostBack="True">
                        </asp:DropDownList>
                    
                    </td>
                    <td width="30%">
                        <label class="EtiquetaNegrita">
                        Estado Civil</label>
                        <asp:DropDownList ID="ddlCivil" runat="server" CssClass="ddl" TabIndex="8">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="center" class="style4">
                        &nbsp;</td>
                    <td width="20%" colspan="2">
                        <label class="EtiquetaNegrita">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" TabIndex="9"></asp:TextBox>
                    </td>
                    <td width="20%">
                       
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" />
                       
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" class="style4">
                        &nbsp;</td>
                    <td colspan="3" style="width: 40%">
                   
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" class="style4">
                        &nbsp;</td>
                    <td align="center" colspan="3" style="width: 40%">
                        <asp:Button ID="btnRegistrar" runat="server" CssClass="buttonVerde" 
                            Text="Registrar Postulante"  validationgroup="Validar" 
                            onclick="btnRegistrar_Click" TabIndex="10" />
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" class="style4">
                        &nbsp;</td>
                    <td align="center" colspan="3" style="width: 40%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="5" style="width: 40%" width="20%">
                     <div class="fondoCabeceraSubtitulo"><h2 class="subtitulodelgado">BÚQUEDA DE POSTULANTES </h2></div>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td colspan="3" style="width: 40%">
                        <asp:TextBox ID="txtPersonal" runat="server" ontextchanged="txtPersonal_TextChanged" AutoPostBack="True"></asp:TextBox>
                      <%--  <cc1:AutoCompleteExtender ID="txtPersonal_AutoCompleteExtender" 
                        runat="server" CompletionInterval="10" CompletionListCssClass="CompletionList" 
                        CompletionListHighlightedItemCssClass="CompletionListHighlightedItem" 
                        CompletionListItemCssClass="CompletionListItem" DelimiterCharacters="" 
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetPersonal" 
                        ServicePath="" TargetControlID="txtPersonal">
                    </cc1:AutoCompleteExtender>--%>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td align="center" colspan="3" style="width: 40%">
                        <asp:GridView ID="GridPersonal" runat="server" CssClass="mGridAzul" 
                            AutoGenerateColumns="False" Width="50%">
                            <Columns>
                                <asp:BoundField DataField="Row" HeaderText="Row" SortExpression="Row" />
                                <asp:BoundField DataField="DES_NOMBRES" HeaderText="Apellidos y Nombres" 
                                    SortExpression="DES_NOMBRE">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DES_DNI" HeaderText="DNI" SortExpression="DES_DNI" />
                                <asp:TemplateField HeaderText="Sel">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# Eval("DES_DNI") %>' 
                                            ImageUrl="~/imagenes/pencil_add.ico" ToolTip="Seleccionar" OnClick ="SeleccionarPersonal" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
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
    
