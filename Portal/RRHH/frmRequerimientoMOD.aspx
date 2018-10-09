<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmRequerimientoMOD.aspx.cs" Inherits="RRHH_frmMOD" %>
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
            <table style="width:100%" class="">
                <tr>
                    <td style="width: 50px; text-align: center">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Lista.png" 
                        Width="50px" />
                    </td>
                    <td class="headerText">
                        MANTENIMIENTO DE REQUERIMIENTO MOD</td>
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
                            onclick="btnControl_Click" ToolTip="Control MOI" Width="55px" />
                    </td>
                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnSeguimiento" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_4.png" 
                        onclick="btnSeguimiento_Click" ToolTip="Seguimiento MOI" Width="50px" />
                    </td>
                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnReportes" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_3.png" onclick="btnReportes_Click" 
                        ToolTip="Reporte MOI" Width="50px" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
            <cc1:Accordion ID="Accordion1" runat="server" TransitionDuration="1000"
                ContentCssClass="accordionContent" HeaderCssClass="accordionHeader"
                FadeTransitions="true" Height="700px" Width="100%">
                <Panes>


                    <cc1:AccordionPane ID="pane2" runat="server">
                        <Header>BUSCAR</Header>
                        <Content>
                            <table class="style1">
                                <tr>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center" style="width: 40%" width="20%">
                                        <div class="fondoCabeceraSubtitulo">
                                            <h2 class="subtitulodelgado">BÚSQUEDA DE REQUERIMIENTO</h2>
                                        </div>
                                    </td>
                                </tr>
                                </td>
                <tr>
                    <td width="15%">
                        <%--    <label class="EtiquetaNegrita">Empresa</label>
                            <asp:DropDownList ID="ddlEmpresasB" runat="server" 
                                AutoPostBack="True" CssClass="ddl" 
                                onselectedindexchanged="ddlEmpresasB_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                        <asp:CheckBox ID="chkEmpresas" runat="server" AutoPostBack="True"
                            OnCheckedChanged="chkEmpresas_CheckedChanged" Text="(Seleccionar todo)"
                            Font-Size="Smaller" />
                        <asp:TextBox ID="txtEmpresas" Text="EMPRESAS" runat="server"
                            Width="95%"></asp:TextBox>
                        <asp:Panel ID="pnlEmpresas" runat="server" CssClass="PnlDesign">
                            <asp:CheckBoxList ID="ddlEmpresasB" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtEmpresas"
                            PopupControlID="pnlEmpresas" Position="Bottom">
                        </cc1:PopupControlExtender>
                    </td>
                    <td width="15%">
                        <%--<label class="EtiquetaNegrita">Proyecto</label>
                            <asp:DropDownList ID="ddlObraB" runat="server" 
                                AutoPostBack="True" CssClass="ddl" 
                                onselectedindexchanged="ddlObraB_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                        <asp:CheckBox ID="chkProyectos" runat="server" AutoPostBack="True"
                            OnCheckedChanged="chkProyectos_CheckedChanged" Text="(Seleccionar todo)"
                            Font-Size="Smaller" />
                        <asp:TextBox ID="txtObraB" Text="PROYECTOS" runat="server"
                            Width="95%"></asp:TextBox>
                        <asp:Panel ID="pnlObra" runat="server" CssClass="PnlDesign">
                            <asp:CheckBoxList ID="ddlObraB" runat="server" oncheckedchanged="ddlObraB_CheckedChanged">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txtObraB"
                            PopupControlID="pnlObra" Position="Bottom">
                        </cc1:PopupControlExtender>
                    </td>
                    <td width="15%">
                        <%--   <label class="EtiquetaNegrita">Centro de Costo</label>
                            <asp:DropDownList ID="ddlCentroB" runat="server" AutoPostBack="True" 
                                CssClass="ddl" onselectedindexchanged="ddlCentroB_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                        <asp:CheckBox ID="chkCentros" runat="server" AutoPostBack="True"
                            OnCheckedChanged="chkCentros_CheckedChanged" Text="(Seleccionar todo)"
                            Font-Size="Smaller" />
                        <asp:TextBox ID="txtCentros" Text="CENTROS DE COSTO" runat="server"
                            Width="95%"></asp:TextBox>
                        <asp:Panel ID="pnlCentros" runat="server" CssClass="PnlDesign">
                            <asp:CheckBoxList ID="ddlCentroB" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txtCentros"
                            PopupControlID="pnlCentros" Position="Bottom">
                        </cc1:PopupControlExtender>
                    </td>
                    <td width="15%">
                        <%--<label class="EtiquetaNegrita">Estado</label>--%>
                        <asp:CheckBox ID="chkEstados" runat="server" AutoPostBack="True"
                            OnCheckedChanged="chkEstados_CheckedChanged" Text="(Seleccionar todo)"
                            Font-Size="Smaller" />
                        <asp:TextBox ID="txtEstados" Text="ESTADOS" runat="server"
                            CssClass="txtbox"></asp:TextBox>
                        <asp:Panel ID="PnlEstados" runat="server" CssClass="PnlDesign">
                            <asp:CheckBoxList ID="ddlEstadoB" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <br />
                        <cc1:PopupControlExtender ID="PceSelectProyecto" runat="server" TargetControlID="txtEstados"
                            PopupControlID="PnlEstados" Position="Bottom">
                        </cc1:PopupControlExtender>
                    </td>
                </tr>
                                <tr>
                                    <td width="15%" align="left">
                                        <label class="EtiquetaNegrita">Requerimiento</label>
                                        <asp:TextBox ID="txtRequerimientoB" runat="server" Width="96%"></asp:TextBox>
                                    </td>

                                    <td width="15%" align="left">
                                        <label class="EtiquetaNegrita">Item</label>
                                        <asp:TextBox ID="txtItemB" runat="server" Width="96%"></asp:TextBox>
           
                                    </td>

                                    <td width="15%" align="left">

                                        <asp:CheckBox ID="chkCargos" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkCargos_CheckedChanged" Text="(Seleccionar todo)"
                                            Font-Size="Smaller" />
                                        <asp:TextBox ID="txtCargos" Text="CARGOS" runat="server"
                                            CssClass="txtbox"></asp:TextBox>
                                        <asp:Panel ID="pnlCargos" runat="server" CssClass="PnlDesign">
                                            <asp:CheckBoxList ID="ddlCargos" runat="server">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <br />
                                        <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="txtCargos"
                                            PopupControlID="pnlCargos" Position="Bottom">
                                        </cc1:PopupControlExtender>



                                    </td>
                                    <td width="15%">


                                        <asp:CheckBox ID="chkEspecialidad" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkEspecialidad_CheckedChanged" Text="(Seleccionar todo)"
                                            Font-Size="Smaller" />
                                        <asp:TextBox ID="txtEspecialidad" Text="ESPECIALIDAD" runat="server"
                                            CssClass="txtbox"></asp:TextBox>
                                        <asp:Panel ID="pnlEspecialidad" runat="server" CssClass="PnlDesign">
                                            <asp:CheckBoxList ID="ddlEspecialidad" runat="server">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <br />
                                        <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txtEspecialidad"
                                            PopupControlID="pnlEspecialidad" Position="Bottom">
                                        </cc1:PopupControlExtender>

                                    </td>
                                    <td width="15%">
                                        <asp:CheckBox ID="chkReclutadores" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkReclutadores_CheckedChanged" Text="(Seleccionar todo)"
                                            Font-Size="Smaller" />
                                        <%--
                            <label class="EtiquetaNegrita">Reclutadores</label>--%>
                                        <asp:TextBox ID="txtAnalista" Text="RECLUTADORES" runat="server"
                                            CssClass="txtbox"></asp:TextBox>
                                        <asp:Panel ID="PnlAnalistas" runat="server" CssClass="PnlDesign">
                                            <asp:CheckBoxList ID="ddlAnalistas" runat="server">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <br />
                                        <cc1:PopupControlExtender ID="PceSelectAnalista" runat="server" TargetControlID="txtAnalista"
                                            PopupControlID="PnlAnalistas" Position="Bottom">
                                        </cc1:PopupControlExtender>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="buttonVerde"
                                            OnClick="btnBuscar_Click" Text="Buscar" Width="30%" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gridPersonal" runat="server" AutoGenerateColumns="False"
                                            CssClass="mGridAzul" DataKeyNames="ID_DETALLE_REQUERIMIENTO_PERSONAL"
                                            Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="Row" HeaderText="Row" SortExpression="Row" />
                                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa"
                                                    SortExpression="EMPRESA" />
                                                <asp:BoundField DataField="PROYECTO" HeaderText="Proyecto"
                                                    SortExpression="PROYECTO" />
                                                <asp:BoundField DataField="CENTROCOSTO" HeaderText="Centro de Costo"
                                                    SortExpression="CENTROCOSTO" />
                                                <asp:BoundField DataField="NUMERO_REQUISICION" HeaderText="Requerimiento"
                                                    SortExpression="NUMERO_REQUISICION" />
                                                <asp:BoundField DataField="SECUENCIA" HeaderText="Item"
                                                    SortExpression="SECUENCIA" />
                                                <asp:BoundField DataField="CARGO" HeaderText="Cargo"
                                                    SortExpression="CARGO" />
                                                <asp:BoundField DataField="ESPECIALIDAD" HeaderText="Especialidad"
                                                    SortExpression="ESPECIALIDAD" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado"
                                                    SortExpression="ESTADO" />
                                                <asp:BoundField DataField="FECHA_APROBACION" HeaderText="Fecha Aprobación"
                                                    SortExpression="FECHA_APROBACION" />
                                                <asp:BoundField DataField="CANDIDATO_FINALISTA" HeaderText="Candidato Finalista"
                                                    SortExpression="CANDIDATO_FINALISTA" />
                                                <asp:TemplateField HeaderText="Editar Requerimiento">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnSeleccionarRequerimiento" runat="server"
                                                            CommandArgument='<%# Eval("ID_DETALLE_REQUERIMIENTO_PERSONAL") %>'
                                                            ImageUrl="~/imagenes/pencil_add.ico" OnClick="Seleccionar_REQUERIMIENTO"
                                                            ToolTip="Seleccionar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Anular Requerimiento">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnAnularRequerimiento" runat="server"
                                                            CommandArgument='<%# Eval("ID_DETALLE_REQUERIMIENTO_PERSONAL") %>'
                                                            ImageUrl='<%# Eval("IMG_ESTADO") %>' OnClick="Anular_REQUERIMIENTO" OnClientClick="return confirm('Desea anular requerimiento?');"
                                                            ToolTip="Seleccionar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar Requerimiento">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEliminarRequerimiento" runat="server"
                                                            CommandArgument='<%# Eval("ID_DETALLE_REQUERIMIENTO_PERSONAL") %>'
                                                            ImageUrl="~/imagenes/delete.png" OnClick="Eliminar_REQUERIMIENTO" OnClientClick="return confirm('Desea eliminar requerimiento?');"
                                                            ToolTip="Seleccionar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>

                                </tr>
                            </table>
                        </Content>
                    </cc1:AccordionPane>

                    <cc1:AccordionPane ID="pane1" runat="server">
                        <Header>REGISTRAR</Header>
                        <Content>
                            <table class="style1">
                                <tr>
                                    <td colspan="5" align="center" style="width: 40%" width="20%">
                                        <div class="fondoCabeceraSubtitulo">
                                            <h2 class="subtitulodelgado">REGISTRO DEL REQUERIMIENTO</h2>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">&nbsp;</td>
                                    <td width="20%">
                                        <label class="EtiquetaNegrita">
                                            Empresa</label>
                                        <asp:DropDownList ID="ddlEmpresas" runat="server"
                                            AutoPostBack="True" CssClass="ddl"
                                            OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        <label class="EtiquetaNegrita">
                                            Proyecto</label>
                                        <asp:DropDownList ID="ddlObra" runat="server"
                                            AutoPostBack="True" CssClass="ddl"
                                            OnSelectedIndexChanged="ddlObra_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        <label class="EtiquetaNegrita">
                                            Centro de Costo</label>
                                        <asp:DropDownList ID="ddlCentro" runat="server" AutoPostBack="True"
                                            CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="15%">&nbsp;</td>
                                    <td width="20%" align="left">
                                        <label class="EtiquetaNegrita">
                                            Requerimiento</label>
                                        <asp:TextBox ID="txtRequerimiento" runat="server" Width="96%"></asp:TextBox>
                                    </td>
                                    <td width="20%" align="left">
                                        <label class="EtiquetaNegrita">Item Inicio</label>
                                        <asp:TextBox ID="txtItem" runat="server" Width="96%"></asp:TextBox>
                                    </td>
                                    <td width="20%" align="left">
                                        <label class="EtiquetaNegrita">Item Fin</label>
                                        <asp:TextBox ID="txtItemFin" runat="server" Width="96%"></asp:TextBox>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="15%">&nbsp;</td>
                                    <td width="20%" align="left">
                                        <label class="EtiquetaNegrita">Cargos</label>
                                        <asp:DropDownList ID="ddlCargoB" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="20%" align="left">
                                        <label class="EtiquetaNegrita">Especialidad</label>
                                        <asp:DropDownList ID="ddlEspecialidadB" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="20%" align="left">
                                        <label class="EtiquetaNegrita">
                                            Fecha Aprobación</label>
                                        <asp:TextBox ID="txtFechaAprobacion"
                                            runat="server" Width="96%"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="txtFechaAprobacion_MaskedEditExtender"
                                            runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtFechaAprobacion" />
                                        <cc1:CalendarExtender ID="txtFechaAprobacion_CalendarExtender" runat="server"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc"
                                            TargetControlID="txtFechaAprobacion" />
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="15%">&nbsp;</td>
                                    <td align="left" width="20%">&nbsp;</td>
                                    <td align="left" width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="width: 40%" width="20%" align="center">
                                        <asp:Button ID="btnRegistrar" runat="server" CssClass="buttonVerde"
                                            Text="Registrar" Width="30%" OnClick="btnRegistrar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </cc1:AccordionPane>

                </Panes>
            </cc1:Accordion> 
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
                                                          <asp:BoundField DataField="CARGO" HeaderText="CARGO" 
                                                        SortExpression="CARGO" />
                                                          <asp:BoundField DataField="ESPECIALIDAD" HeaderText="ESPECIALIDAD" 
                                                        SortExpression="ESPECIALIDAD" />
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
    


