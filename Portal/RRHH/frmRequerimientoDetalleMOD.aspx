<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmRequerimientoDetalleMOD.aspx.cs" Inherits="frmRequerimientoDetalleMOD" %>


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
        .style4
        {
        }
        .style6
        {
            height: 21px;
        }
    </style>
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
                        DETALLE DE REQUERIMIENTO
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
                    </asp:Panel>
                     <table style="width:100%" class="">
                    <tr>
                        <td align="center" style="width: 40%" width="20%">
                            <div class="fondoCabeceraSubtitulo">
                                <h2 class="subtitulodelgado">
                                    DATOS DEL REQUERIMIENTO 
                                </h2>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="PanelDatos" runat="server" Visible="true" Width="100%">
                                <table class="style1">
                                    <tr>
                                        <td  width="15%">
                                         &nbsp;</td>
                                        <td align="left" width="15%">
                                            <label class="EtiquetaNegrita">
                                            Empresa</label>
                                            <asp:TextBox ID="txtEmpresa" runat="server" Width="96%" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td align="left" width="15%">
                                             <label class="EtiquetaNegrita">
                                            Requerimiento</label>
                                            <asp:TextBox ID="txtRequerimiento" runat="server" Width="96%" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td align="left" width="15%">
                                             <label class="EtiquetaNegrita">
                                            Item</label>
                                            <asp:TextBox ID="txtItem" runat="server" Width="96%" Enabled="False"></asp:TextBox>
                                    </td>
                                        <td align="left" width="15%">
                                          <label class="EtiquetaNegrita">
                                            Proyecto</label>
                                            <asp:TextBox ID="txtProyecto" runat="server" Width="96%" Enabled="False"></asp:TextBox>
                                 
                                        </td>
                                         
                                        <td align="center" width="15%">
                                            <asp:ImageButton ID="btnActualizar" runat="server" Height="40px" 
                                                ImageUrl="~/imagenes/Buscador.png" onclick="btnActualizar_Click" 
                                                ToolTip="Actualizar" Width="40px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  width="15%">
                                            &nbsp;</td>
                                        <td align="left" width="15%">
                                              <label class="EtiquetaNegrita">
                                            Centro de Costo</label>
                                            <asp:TextBox ID="txtCeco" runat="server" 
                                                Width="96%" Enabled="False"></asp:TextBox></td>
                                        <td align="left" width="15%">
                                            <label class="EtiquetaNegrita">
                                            Cargo</label>
                                            <asp:TextBox ID="txtCargo" runat="server" Width="95%" Enabled="False"></asp:TextBox>
                                     
                                        </td>
                                        <td align="left" width="15%">
                                             <label class="EtiquetaNegrita">
                                            Especialidad</label>
                                            <asp:TextBox ID="txtEspecialidad" runat="server" Width="98%" Enabled="False"></asp:TextBox>
                                           </td>
                                        <td align="left" width="15%">
                                             <label class="EtiquetaNegrita">
                                            Estado de Requerimiento</label>
                                            <asp:TextBox ID="txtEstado" runat="server" Width="98%" Enabled="False"></asp:TextBox></td>
                                        <td align="left" width="15%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  width="15%">
                                            &nbsp;</td>
                                        <td align="left" width="15%">
                                            &nbsp;</td>
                                        <td align="left" width="15%">
                                            &nbsp;</td>
                                        <td align="left" width="15%">
                                            &nbsp;</td>
                                        <td align="left" width="15%">
                                            &nbsp;</td>
                                        <td align="left" width="15%">
                                            &nbsp;</td>
                                    </tr>
                                   
                                    <tr>
                                        <td align="left"  colspan="6">
                                        <div class="fondoCabeceraSubtitulo">
                                <h2 class="subtitulodelgado">
                                    DATOS DE LOS POSTULANTES ASIGNADOS A REQUERIMIENTO</h2>
                            </div></td>
                                    </tr>
                                   
                                    <tr>
                                        <td  colspan="6">
                                            <asp:GridView ID="gridPersonal" runat="server" AutoGenerateColumns="False" 
                                                CssClass="mGridAzul" DataKeyNames="DES_DNI" 
                                                Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="Row" HeaderText="Nro." SortExpression="Row" />
                                                    <asp:BoundField DataField="Nombres_Apellidos" HeaderText="Nombres Apellidos" 
                                                        SortExpression="Nombres_Apellidos" />
                                                    <asp:BoundField DataField="DES_Cargo" HeaderText="Cargo" 
                                                        SortExpression="Cargo" />
                                                    <asp:BoundField DataField="DES_Especialidad" HeaderText="Especialidad" 
                                                        SortExpression="Especialidad" />
                                                    <asp:BoundField DataField="DES_DNI" HeaderText="DNI" SortExpression="DNI" />
                                                    <asp:BoundField DataField="DES_TELEFONO" HeaderText="TELEFONO" 
                                                        SortExpression="DNI" />
                                                    <asp:TemplateField HeaderText="Editar Requerimiento">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnSeleccionarRequerimiento" runat="server" 
                                                                CommandArgument='<%# Eval("DES_DNI") %>' 
                                                                ImageUrl="~/imagenes/pencil_add.ico" OnClick="Seleccionar_REQUERIMIENTO" 
                                                                ToolTip="Seleccionar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  colspan="6" align="center">
                                        <asp:Button ID="btnRegresar" runat="server" CssClass="buttonVerde" 
                                                onclick="btnRegresar_Click" onClientClick="history.go(-1);return false;" Text="Regresar" 
                                                Width="30%" />
                                                
                                                
                                                
                                                   <asp:Button ID="btnAgregar" runat="server" CssClass="buttonVerde" 
                                                onclick="btnAgregar_Click"  Text="Agregar" 
                                                Width="30%" />
                                                </td>

                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                                       <%-- <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="65%">--%>
                        <%--</asp:Panel>--%>
                        </td>
                    </tr>
                </table>




      <table class="style1">
                <tr>
                    <td align="center" colspan="6">
                        <asp:HiddenField ID="HidRegistro" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalRegistro" 
                                 runat="server" 
                                 
                                 
                                 BackgroundCssClass="modalBackground"
                                 PopupControlID="pnlPopup" 
                                 PopupDragHandleControlID="pnlPopup"
                                 TargetControlID="HidRegistro">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="85%">
                                <table class="style1">
                              
                                    <tr>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%">
                                          <label class="EtiquetaNegrita">
                                            Nombres o Dni</label>
                                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="30" TabIndex="2" 
                                               ></asp:TextBox></td>
                                        <td width="20%">
                                         <label class="EtiquetaNegrita">
                                        Cargo</label>
                                            <asp:DropDownList ID="ddlCargosB" runat="server" CssClass="ddl" 
                                                onselectedindexchanged="ddlCargosB_SelectedIndexChanged" TabIndex="8">
                                            </asp:DropDownList></td>
                                        <td width="20%">
                                          <label class="EtiquetaNegrita">
                                        Especialidad</label>
                                            <asp:DropDownList ID="ddlEspecialidadesB" runat="server" CssClass="ddl" 
                                                onselectedindexchanged="ddlEspecialidadesB_SelectedIndexChanged" TabIndex="8">
                                            </asp:DropDownList></td>
                                        <td width="20%">
                                            &nbsp;</td>
                                        
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%">
                                        <asp:Button ID="btnBuscarModal" runat="server" CssClass="buttonNegro" 
                                                onclick="btnBuscarModal_Click" Text="Buscar" Visible="true" />
                                           </td>
                                        <td width="20%">
                                            &nbsp;</td>
                                        <td width="20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        
                                        <td align="center" colspan="5">
                                            
                                            <asp:GridView ID="gridPersonalDisponible" runat="server" AllowPaging="true" 
                                                AutoGenerateColumns="False" CssClass="mGridAzul" DataKeyNames="DES_DNI" 
                                                OnPageIndexChanging="GvCities_PageIndexChanging" PageSize="15" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="DES_DNI" HeaderText="DES_DNI" 
                                                        SortExpression="DES_DNI" />
                                                    <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" 
                                                        SortExpression="NOMBRES" />
                                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" 
                                                        SortExpression="ESTADO" />
                                                    <asp:TemplateField HeaderText="Editar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnSeleccionar" runat="server" 
                                                                CommandArgument='<%# Eval("DES_DNI") %>' ImageUrl="~/imagenes/pencil_add.ico" 
                                                                OnClick="Seleccionar_Personal" ToolTip="Seleccionar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            
                                        </td>
                                        
                                    </tr>
                                    
                                    <tr>
                                        
                                        <td align="center" colspan="5">
                                            <asp:Button ID="btnCerrar" runat="server" CssClass="buttonNegro" 
                                                onclick="btnCerrar_Click" Text="Cancelar" Visible="False" Width="30%" />
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
    






