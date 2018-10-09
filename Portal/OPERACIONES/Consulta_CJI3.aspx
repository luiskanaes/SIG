<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Consulta_CJI3.aspx.cs" Inherits="OPERACIONES_Consulta_CJI3" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


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
                if (!jsIsUserFriendlyChar(key, "NoDecimals")) {
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
			    title: 'Mensaje del Sistemas :',
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
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 10%;
        }
        .style2
        {
            width: 100%;
            height: 10%;
        }
        #contenedor_htnl {
        width: 100%;
        }

        #Opciones_html {
        float: left;
        width: 35%;
        }
        #contenido_html {
        float: left;
        width: 65%;
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
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/GraficoVentas.png" 
                        Width="48px" />
                    </td>
                    <td class="headerText">
                CONSULTA CJI3
                    </td>
                    <td style="width: 50px; text-align: center">
                    </td>
                    <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="btnCargar" runat="server" 
                        ImageUrl="~/imagenes/cargar.png" Width="50px" onclick="btnCargar_Click" 
                        ToolTip="Cargar archivos" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" align="center" colspan="3">
                        <hr />
                        <asp:Label ID="lblSplit" runat="server" Visible="False"></asp:Label>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" 
                             ClientValidationFunction="ValidaDDL" ControlToValidate="ddlAnio" 
                             ErrorMessage="Selecione un Año" ValidateEmptyText="True" 
                                validationgroup="Consultar" CssClass="errorMessage"></asp:CustomValidator>
                        <br />
                    </td>
                </tr>
            </table>
  
                <br />
                <table class="style1">
                    <tr>
                        <td width="20%" align="center">
                        <div id="contenedor_htnl">
                        
 
                            <div id="Opciones_html">
                                <table class="style2">
                                    <tr>
                                        <td width="10%">
                                            &nbsp;</td>
                                        <td width="80%" align="center">
                                            <asp:ImageButton ID="btnListar" runat="server" 
                                                ImageUrl="~/imagenes/boton.buscar.gif" onclick="btnListar_Click" />
                                        </td>
                                        <td width="10%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            &nbsp;</td>
                                        <td align="left" width="80%">
                                         <label class="label">Año</label>
                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            &nbsp;</td>
                                        <td align="left" width="80%">
                                        <label class="label">Mes</label>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                                          </td>
                                        <td width="10%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            &nbsp;</td>
                                        <td align="left" width="80%">
                                        <label class="label">Estados</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            &nbsp;</td>
                                        <td align="left" width="80%">
                                         </td>
                                        <td width="10%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </div>
 
                            <div id="contenido_html">
                                <table class="Subtitulo">
                                    <tr>
                                        <td width="25%" align="left">
                                         <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtbox" Font-Bold="True" 
                                Text="Seleccionar Proyectos" Width="322px"></asp:TextBox>
                            <asp:Panel ID="PnlCust" runat="server" CssClass="PnlDesign">
                                <asp:CheckBoxList ID="CheckProyectos" runat="server" RepeatColumns="2">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            
                            <cc1:PopupControlExtender ID="PceSelectCustomer" runat="server" 
                                PopupControlID="PnlCust" Position="Bottom" TargetControlID="txtCustomer">
                            </cc1:PopupControlExtender>

                                        </td>
                                        <td width="25%" align="center">
                                            <asp:ImageButton ID="btnConsultar" runat="server" 
                                                ImageUrl="~/imagenes/boton.CostoVenta.jpg" onclick="btnConsultar_Click" />
                                        </td>
                                        <td width="25%">
                                            <asp:ImageButton ID="btnResumen" runat="server" 
                                                ImageUrl="~/imagenes/boton.Resumen.jpg" onclick="btnResumen_Click" />
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
                                        <td colspan="4">
                                                     
      
                                                     <rsweb:ReportViewer ID="ReportViewer1" runat="server"  BorderColor="Black"  BorderStyle="Solid" BorderWidth="1px" Visible="False" Width="100%">
                                                     </rsweb:ReportViewer>
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
                            </div>
 

                        </div>

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvwAsignacion" runat="server" CssClass="EtiquetaSimple">
                                <HeaderStyle BackColor="#999999" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            
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

