<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPWeb.master" AutoEventWireup="true" CodeFile="AsignacionControl.aspx.cs" Inherits="RRHH_AsignacionControl" %>

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
        .style2
        {
            height: 20px;
        }
        .auto-style1 {
            width: 20%;
        }
        .auto-style2 {
            height: 20px;
            width: 20%;
        }
        .auto-style3 {
            width: 11%;
        }
        .auto-style4 {
            height: 20px;
            width: 11%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td align="right" width="20%">
              <asp:Label ID="Label4" runat="server" CssClass="EtiquetaNegrita" 
                                    Text="Bienvenido :"></asp:Label></td>
            <td align="left" colspan="3" style="width: 40%">
                <asp:Label ID="lblPersona" runat="server" CssClass="EtiquetaNegrita"></asp:Label>
            </td>
            <td align="center" width="20%">
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
            <td align="center" colspan="5" width="20%">
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
                            <td align="right" class="auto-style3">
                            <label class="EtiquetaNegrita">Empresa :</label></td>
                            <td align="left" class="auto-style1">
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
                            <td align="right" class="auto-style3">
                                 <label class="EtiquetaNegrita">Proyecto :</label></td>
                            <td align="left" class="auto-style1">
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
                            <td align="right" class="auto-style3">
                                   <label class="EtiquetaNegrita">Cento de Costo :</label></td>
                            <td align="left" class="auto-style1">
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
                            <td align="right" class="auto-style4">
                            <label class="EtiquetaNegrita">Fecha de Ingreso :</label>
                            </td>
                            <td align="left" class="auto-style2">
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
                            <td align="right" class="auto-style4">
                                &nbsp;</td>
                            <td align="left" class="auto-style2">
                                &nbsp;</td>
                            <td align="right" class="style2" width="10%">
                                &nbsp;</td>
                            <td align="left" class="style2" width="30%">
                                &nbsp;</td>
                            <td class="style2" width="10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="center"  colspan="4">
                                <asp:TextBox ID="txtObserva" runat="server" Height="100px" TextMode="MultiLine" CssClass="textarea" Width="100%" ></asp:TextBox>
                            </td>
                            <td width="10%">
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
                            <td colspan="6">
                                <asp:Label ID="lblMensaje" runat="server" CssClass="EtiquetaNegrita" 
                                    ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <asp:DataList ID="DataListRecursos" runat="server" Width="70%" 
                                    DataKeyField="IDE_DETALLE">
                                    <ItemTemplate>
                                        <table class="style1">
                                            <tr>
                                                <td align="center" width="10%">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/pto.png" />
                                                </td>
                                                <td align="left" width="35%">
                                                    <asp:Label ID="lblRecurso" runat="server" CssClass="EtiquetaNegrita" 
                                                        Text='<%# Eval("DES_ASUNTO") %>'></asp:Label>
                                                </td>
                                                <td align="left" width="55%">
                                                    <asp:RadioButtonList ID="RadioEstados" runat="server" 
                                                        CssClass="EtiquetaNegrita" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <asp:ImageButton ID="btnGuardar" runat="server" 
                                    ImageUrl="~/imagenes/boton.guardar.gif" onclick="btnGuardar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="right" class="auto-style3">
                                &nbsp;</td>
                            <td align="left" class="auto-style1">
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
</asp:Content>

