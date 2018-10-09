<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="RO_COSTOS_VENTAS.aspx.cs" Inherits="OPERACIONES_RO_COSTOS_VENTAS" %>


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
        function jsDecimals(e) {

        var evt = (e) ? e : window.event;
        var key = (evt.keyCode) ? evt.keyCode : evt.which;
        if (key != null) {
            key = parseInt(key, 10);
            if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
                if (!jsIsUserFriendlyChar(key, "Decimals")) {
                    return false;
                }
            }
            else {
                if (evt.shiftKey) {
                    return false;
                }
            }
        }
        return true;
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
   function NumCheck(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    if (key == 8) return true
    if (key > 47 && key < 58) {
      if (field.value == "") return true
      regexp = /.[0-9]{5}$/
      return !(regexp.test(field.value))
    }
    if (key == 46) {
      if (field.value == "") return false
      regexp = /^[0-9]+$/
      return regexp.test(field.value)
    }
    return false
  }
  function SoloNumerosDecimales3(e, valInicial, nEntero, nDecimal) {
    var obj = e.srcElement || e.target;
    var tecla_codigo = (document.all) ? e.keyCode : e.which;
    var tecla_valor = String.fromCharCode(tecla_codigo);
    var patron2 = /[\d.]/;
    var control = (tecla_codigo === 46 && (/[.]/).test(obj.value)) ? false : true;
    var existePto = (/[.]/).test(obj.value);

    //el tab
    if (tecla_codigo === 8)
        return true;

    if (valInicial !== obj.value) {
        var TControl = obj.value.length;
        if (existePto === false && tecla_codigo !== 46) {
            if (TControl === nEntero) {
                obj.value = obj.value + ".";
            }
        }

        if (existePto === true) {
            var subVal = obj.value.substring(obj.value.indexOf(".") + 1, obj.value.length);

            if (subVal.length > 1) {
                return false;
            }
        }

        return patron2.test(tecla_valor) && control;
    }
    else {
        if (valInicial === obj.value) {
            obj.value = '';
        }
        return patron2.test(tecla_valor) && control;
    }
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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas2.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                    RESULTADOS OPERATIVOS
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
                <td colspan="6" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
        <div  class="sky-form">
             <table class="style1">
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
                     <td width="25%">
                         &nbsp;</td>
                     <td width="25%">
                     <label class="EtiquetaNegrita"> Empresa</label>
                         <asp:DropDownList ID="ddlEmpresas" runat="server" AutoPostBack="True" 
                             CssClass="ddl" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged">
                         </asp:DropDownList>
                     </td>
                     <td align="center" width="25%">
                         <asp:Button ID="btnAgregar" runat="server" CssClass="buttonRojo" 
                             Text="Agregar Resultado" onclick="btnAgregar_Click" Width="85%"  />
                     </td>
                     <td width="25%">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td width="25%">
                         &nbsp;</td>
                     <td colspan="2" style="width: 50%" width="25%">
                     <label class="EtiquetaNegrita">Proyecto</label>
                         <asp:DropDownList ID="ddlProyecto" runat="server" CssClass="ddl" 
                             AutoPostBack="True">
                         </asp:DropDownList>
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
                     <td align="center" colspan="4"><asp:HiddenField ID="HidRegistro" runat="server" />
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
                                    <td width="40%" align="center" colspan="2" style="width: 80%">
                                        <asp:CheckBox ID="CheckPopup" runat="server" Font-Size="8pt" 
                                            Text="No Cerrar Popup" />
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" colspan="2" style="width: 80%" width="40%" valign="middle">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/maquinaria1.png" 
                                            Width="40px" />
                                        <asp:Label ID="lblProyecto" runat="server" CssClass="EtiquetaNegrita" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" colspan="2" style="width: 80%" width="40%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita">Previsto</label>
                                        <asp:DropDownList ID="ddlPrevisto" runat="server" AutoPostBack="True" 
                                            CssClass="ddl" onselectedindexchanged="ddlPrevisto_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="40%" align="left">
                                    <label class="EtiquetaNegrita">Indicadores</label>
                                        <asp:DropDownList ID="ddlIndicador" runat="server" AutoPostBack="True" 
                                            CssClass="ddl" onselectedindexchanged="ddlIndicador_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%">
                                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" colspan="2" style="width: 80%" width="40%">
                                        <asp:CheckBox ID="CheckDetalle" runat="server" AutoPostBack="True" 
                                            Font-Size="8pt" oncheckedchanged="CheckDetalle_CheckedChanged" 
                                            Text="Indicar Detalle PEP" />
                                        <asp:Label ID="lblEstado" runat="server" CssClass="EtiquetaNegrita" Text="0" 
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" colspan="2" style="width: 80%" width="40%">
                                        <asp:DropDownList ID="ddlDetalle" runat="server" CssClass="ddl" Visible="False" 
                                            onselectedindexchanged="ddlDetalle_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" width="40%">
                                    <label class="EtiquetaNegrita">Año</label>
                                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" width="40%">
                                    <label class="EtiquetaNegrita">Mes</label>
                                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl" AutoPostBack="True" 
                                            onselectedindexchanged="ddlMes_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                 <tr>
                                     <td width="10%">
                                         &nbsp;</td>
                                     <td align="left" colspan="2" style="width: 80%" width="40%">
                                         <table class="style1">
                                             <tr>
                                                 <td width="30%">
                                                 <label class="EtiquetaNegrita"> Resultado</label>
                                                     <asp:TextBox ID="txtValor" runat="server" 
                                                         onkeypress="return SoloNumerosDecimales3(event, '0.0', 10, 3);" 
                                                         placeholder="0.00" Width="97%"></asp:TextBox>
                                                 </td>
                                                 <td width="30%">
                                                 <label class="EtiquetaNegrita"> Previsto</label>
                                                     <asp:TextBox ID="txtProyeccion" runat="server" 
                                                         onkeypress="return SoloNumerosDecimales3(event, '0.0', 10, 3);" 
                                                         placeholder="0.00" Width="97%"></asp:TextBox>
                                                 </td>
                                                 <td width="30%">
                                                    <label class="EtiquetaNegrita"> Original</label>
                                                     <asp:TextBox ID="txtMontoInicio" runat="server" onkeypress="return SoloNumerosDecimales3(event, '0.0', 10, 3);" 
                                                         placeholder="0.00" Width="97%"></asp:TextBox>
                                                 </td>
                                             </tr>
                                         </table>
                                     </td>
                                     <td width="10%">
                                         &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="center" width="40%" colspan="2" style="width: 80%">
                                        <asp:Label ID="lblMsg" runat="server" CssClass="errorMessage" Font-Bold="True" 
                                            Font-Size="8pt" Visible="False"></asp:Label>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td align="left" width="40%">
                                        
                                        </td>
                                    <td align="center" width="40%">
                                        &nbsp;</td>
                                    <td width="10%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                &nbsp;</td>
                                    <td width="40%" align="center">
                                        <asp:Button ID="btnNo" runat="server" 
                                    Text="Cancelar" Width="80%" onclick="btnNo_Click" />
                                    </td>
                                    <td width="40%" align="center">
                                        <asp:Button ID="btnYes" runat="server" 
                                    Text="Grabar" Width="80%" validationgroup="Validar" 
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
             <br />
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
    






