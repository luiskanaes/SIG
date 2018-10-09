<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="TipoCambio.aspx.cs" Inherits="OPERACIONES_TipoCambio" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
    document.onselectstart = function () { return false; }
    function SoloNum(e) {
        var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
        return ((key_press > 47 && key_press < 58) || key_press == 46);
        // el  "|| key_press == 46" es para incluir el punto ".", si borramos solo ingresara enteros 
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
    function ValidaDDL(source, arguments) {
        if (arguments.Value < 1) {
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    } 
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
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas4.png" 
                        Width="48px" />
                </td>
                <td class="headerText">
                Tipo de Cambio
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnRegresar" runat="server" 
                        ImageUrl="~/imagenes/boton.regresar.gif" 
                        PostBackUrl="~/OPERACIONES/Carga_CJI3.aspx" />
                </td>
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
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        <label class="label">Tipo de Cambio</label>
                        <asp:TextBox ID="txtTc" runat="server" MaxLength="10" 
                            onkeydown="return jsDecimals(event);" placeholder="Tipo de Cambio" ></asp:TextBox>
                    </td>
                    <td width="20%">
                    <label class="label">Año</label>
                        <asp:TextBox ID="txtAnio" runat="server" MaxLength="5"  onkeydown="return jsDecimals(event);" ></asp:TextBox>
                     </td>
                    <td width="20%" align="left">
                     <label class="label">Mes</label>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl">
                            </asp:DropDownList></td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%" align="center">
                        <asp:ImageButton ID="btnGrabar" runat="server" 
                            ImageUrl="~/imagenes/boton.guardar.gif" onclick="btnGrabar_Click" />
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
                        <asp:Label ID="lblIdTc" runat="server" CssClass="EtiquetaNegrita" 
                            Visible="False"></asp:Label>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="5">
                        <asp:GridView ID="GridTc" runat="server" AutoGenerateColumns="False" 
                            CssClass="mGridAzul" Width="60%">
                            <Columns>
                                <asp:BoundField DataField="Row" HeaderText="N°" SortExpression="Row" />
                                <asp:BoundField DataField="DEC_TC" HeaderText="Tipo de Cambio" 
                                    SortExpression="DEC_TC" />
                                <asp:BoundField DataField="INT_ANIO" HeaderText="Año" 
                                    SortExpression="INT_ANIO" />
                                <asp:BoundField DataField="Mes" HeaderText="Mes" SortExpression="Mes" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" 
                                            CommandArgument='<%# Eval("ID_TC") %>' ImageUrl="~/imagenes/pencil.ico" OnClick="Seleccionar"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
    




