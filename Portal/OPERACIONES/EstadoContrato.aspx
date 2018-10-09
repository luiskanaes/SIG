<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="EstadoContrato.aspx.cs" Inherits="OPERACIONES_EstadoContrato" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
     
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
    <asp:HiddenField ID="hdIdTc" runat="server" />
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                
                    <img alt="" src="../imagenes/loading3.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-md-4">

            <div class="row" style="padding:5px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <b>DATOS DE CARGA</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Año</label>
                                <asp:DropDownList ID="ddlAnio" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Mes</label>
                                <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Archivo de carga</label>
                                <asp:FileUpload ID="FileUpload1" runat="server"  />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <br />
                                <asp:Button ID="btnProcesar" runat="server"  Text="Procesar carga" validationgroup="Validar" OnClick="btnProcesar_Click" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar carga" OnClick="btnEliminar_Click" />
                                <cc1:confirmbuttonextender id="cbe1" runat="server" displaymodalpopupid="mpe1" targetcontrolid="btnEliminar"></cc1:confirmbuttonextender>
                                <cc1:modalpopupextender id="mpe1" runat="server" popupcontrolid="pnlPopup1" targetcontrolid="btnEliminar"
                                    okcontrolid="btnYes1" cancelcontrolid="btnNo1" backgroundcssclass="modalBackground">
                                </cc1:modalpopupextender>
                                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                        Mensaje
                                    </div>
                                    <div class="body">
                                        ¿Deseas eliminar registros cargados?
                                    </div>
                                    <div class="footer" align="right">
                                        <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                        <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="row" style="padding:5px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <b>TIPO DE CAMBIO</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Tipo de Cambio</label>
                                <asp:TextBox ID="txtTc" runat="server" MaxLength="10" 
                            onkeydown="return jsDecimals(event);" placeholder="Tipo de Cambio" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Año</label>
                                <asp:DropDownList ID="ddlAnio2" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Mes</label>
                                <asp:DropDownList ID="ddlMes2" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <br />
                                <asp:ImageButton ID="btnGrabar" runat="server" 
                            ImageUrl="~/imagenes/boton.guardar.gif" onclick="btnGrabar_Click" />

                                <asp:ImageButton ID="btnNuevo" runat="server" 
                            ImageUrl="~/imagenes/boton.Nuevo.jpg" OnClick="btnNuevo_Click"  />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                
                               
                                <asp:GridView ID="GridTc" runat="server" AutoGenerateColumns="False"
                                    CssClass="mGridAzul" Width="100%" >
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
                                                    CommandArgument='<%# Eval("ID_TC") %>' ImageUrl="~/imagenes/pencil.ico" OnClick="Seleccionar" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <%--SEGUNDO CUERPO--%>
        <div class="col-md-8" >

            <div class="row" style="padding:5px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <b>REPORTES CJ3</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblSplit" runat="server" Visible="False"></asp:Label>
                               <%-- <asp:CustomValidator ID="CustomValidator3" runat="server"
                                    ClientValidationFunction="ValidaDDL" ControlToValidate="ddlAnio"
                                    ErrorMessage="Selecione un Año" ValidateEmptyText="True"
                                    ValidationGroup="Consultar" CssClass="errorMessage"></asp:CustomValidator>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>Año</label>
                                <asp:DropDownList ID="ddlAnio3" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Mes</label>
                                <asp:DropDownList ID="ddlMes3" runat="server" CssClass="ddl">
                            </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Estados</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:ImageButton ID="btnListar" runat="server" 
                                                ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnListar_Click"  />
                                </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <br />
                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtbox" Font-Bold="True"
                                    Text="Seleccionar Proyectos" Width="100%"></asp:TextBox>
                                <asp:Panel ID="PnlCust" runat="server" CssClass="PnlDesign">
                                    <asp:CheckBoxList ID="CheckProyectos" runat="server" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </asp:Panel>

                                <cc1:PopupControlExtender ID="PceSelectCustomer" runat="server"
                                    PopupControlID="PnlCust" Position="Bottom" TargetControlID="txtCustomer">
                                </cc1:PopupControlExtender>
                            </div>
                            <div class="col-md-6">
                                <br />
                                <asp:ImageButton ID="btnConsultar" runat="server" 
                                                ImageUrl="~/imagenes/boton.CostoVenta.jpg" OnClick="btnConsultar_Click"  />
                                 <asp:ImageButton ID="btnResumen" runat="server" 
                                                ImageUrl="~/imagenes/boton.Resumen.jpg" OnClick="btnResumen_Click"  />
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">

                                <rsweb:ReportViewer ID="ReportViewer1" runat="server"  BorderColor="Black"  BorderStyle="Solid" BorderWidth="1px" Visible="False" Width="100%">
                                                     </rsweb:ReportViewer>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
       
    </div>
     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProcesar" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        window.onsubmit = function () {
            if (Page_IsValid) {
                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, updateProgress.get_displayAfter());
            }
        }
    </script>
</asp:Content>

