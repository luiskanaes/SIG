<%@ Page Language="C#"  MasterPageFile="~/Templates/MPWeb.master" AutoEventWireup="true" CodeFile="frmSELEC_LIDER_GRUPO.aspx.cs" Inherits="RRHH_TAREO_EMP_frmSELEC_LIDER_GRUPO" %>

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
    .auto-style1 {
        width: 827px;
    }
    </style>
</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
        <ContentTemplate>
          <%--  <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>--%>
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
                        TAREO EMPLEADOS 
                    </td>

                     

                    <td style="width: 50px; text-align: center">
                        
                    </td>
                    <td style="width: 50px; text-align: center">
                       
                    </td>
                    
                    <td style="width: 50px; text-align: center">
                 </td>
                   
                    <td style="width: 50px; text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center">
                        
                    </td>
                </tr>
            </table>
                    </asp:Panel>
                     <table style="width:100%" class="">
                    
                    <tr>
                        <td align="center">
                            <asp:Panel ID="PanelDatos" runat="server" Visible="true" Width="100%">
                                <table class="style1">
                                
                                    <caption>
                                        <h2 class="subtitulodelgado">
                                            <label>
                                            ASIGNAR LIDER DE GRUPO</label></h2>
                                        </div>
                                        </td>
                                        </tr>

                        
                            
                            
                         <tr>
                              
                             <td class="auto-style1"> &nbsp;<asp:DropDownList ID="ddlEmpleadosDisponibles" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleadosDisponibles_SelectedIndexChanged"></asp:DropDownList>
                                              </td>
                             <td> 
                                                <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR"  OnClick="btnAgregar_Click"   />
                                                 </td>

                         </tr>
                            

                                        <tr>

                                            


                                            <td colspan="6">
                                                 
                                                  <asp:GridView ID="gridPersonal" runat="server" AutoGenerateColumns="False" CssClass="mGridAzul" DataKeyNames="DNI" Width="100%">
                                                    <Columns>
                                                      <%--  <asp:BoundField DataField="Row" HeaderText="Nro." SortExpression="Row" />--%>
                                                        <asp:BoundField DataField="DNI" HeaderText="DNI" SortExpression="DNI" />
                                                        <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" SortExpression="NOMBRES" />
                                                       <%-- <asp:BoundField DataField="ASIGNADOS" HeaderText="ASIGNADOS" SortExpression="ASIGNADOS" />
                                                        <asp:BoundField DataField="SIN_ASIGNAR" HeaderText="SIN_ASIGNAR" SortExpression="SIN_ASIGNAR" />
                                                        <asp:BoundField DataField="CANT_EQUIPOS" HeaderText="CANT_EQUIPOS" SortExpression="CANT_EQUIPOS" />--%>
                                                        <asp:TemplateField HeaderText="ELIMINAR LIDER DE GRUPO">
                                                            <ItemTemplate>
                                                                <%-- <asp:ImageButton ID="btnSeleccionarRequerimiento" runat="server" 
                                                                CommandArgument='<%# Eval("DES_DNI") %>' 
                                                                ImageUrl="~/imagenes/pencil_add.ico" OnClick="Seleccionar_REQUERIMIENTO" 
                                                                ToolTip="Seleccionar" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </td>
                                        </tr>
                                        <tr>
                                          <%--  <td align="center" colspan="6">
                                                <asp:Button ID="btnRegresar" runat="server" CssClass="buttonVerde" onclick="btnRegresar_Click" onClientClick="history.go(-1);return false;" Text="Regresar" Width="30%" />
                                                <asp:Button ID="btnAgregar" runat="server" CssClass="buttonVerde" onclick="btnAgregar_Click" Text="Agregar" Width="30%" />
                                            </td>--%>
                                        </tr>
                                    </caption>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                                       <%-- <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="65%">--%>
                        <%--</asp:Panel>--%>
                                       <asp:Button ID="btnSiguiente" runat="server" OnClick="btnSiguiente_Click" Text="SIGUIENTE" />
                        </td>
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
    






