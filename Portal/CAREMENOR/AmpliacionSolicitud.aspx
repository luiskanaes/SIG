<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="AmpliacionSolicitud.aspx.cs" Inherits="CAREMENOR_AmpliacionSolicitud" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
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
        function doAlert(mensaje) {
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

        function showValue(sender, value) {
            sender.close();

        }
        function lettersOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
          ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
          (charCode < 97 || charCode > 122)) {

                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-8">
                         <b>SOLICITUD DE AMPLIACION DE EQUIPOS  </b>
                    </div>
                    <div class="col-md-4">
                       
                    </div>
                   
                </div>

               
            </div>
        </div>
    </div>
    <div class="row">
        

        <div class="col-md-6">
            <label>Centro de costo</label>
            <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Ingresar PDC</label>
            <asp:TextBox ID="txtPdc" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <br />
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
             <asp:ImageButton ID="btncrear" runat="server" ImageUrl="~/imagenes/boton.regresar.gif" OnClick="btncrear_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">

            <div class="table-responsive">
              
                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo" EmptyDataText="There are no data records to display." Font-Size="8pt">
                        <Columns>
                            <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Requerimientos" SortExpression="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,Reqs_CodigoCompleto,Reqs_ItemSecuencia" />
                            <asp:BoundField DataField="D_SOLPED" HeaderText="Solped" SortExpression="D_SOLPED" />
                            <asp:BoundField DataField="D_PDC" HeaderText="Pdc" SortExpression="D_PDC" />
                            <asp:TemplateField HeaderText="Descripcion del equipo">
                                <ItemTemplate>
                                    <%# Eval("DES_SUBFAMILIA") %> - 
                                    <%# Eval("DES_MARCA") %> -
                                    <%# Eval("DES_MODELO") %> -
                                    <%# Eval("N_Reqs_Capacidad") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos.Alq" SortExpression="D_SOLPED_ALQUILER" />
                            <asp:BoundField DataField="PDC_MONEDA" HeaderText="Moneda" SortExpression="PDC_MONEDA" />
                            <asp:BoundField DataField="D_PDC_MONTO" HeaderText="Monto Alq." SortExpression="D_PDC_MONTO" />

                         
                            <asp:TemplateField HeaderText="Monto">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server" onkeydown="return jsDecimals(event);" Text='<%# Eval("MONTO_AMPLIACION_SOL") %>'  Enabled='<%# (Convert.ToBoolean(Eval("BLOQUEAR_SOL_AMP") )) %>' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="D_FECHA_SALE_OBRA" HeaderText="Fin de contrato" SortExpression="D_FECHA_SALE_OBRA" />
                            <asp:TemplateField HeaderText="F.Inicio">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInicio" runat="server" Text='<%# Eval("A_FECHA_INI_AMPLIACION") %>' Width="140px" TextMode="Date" Enabled='<%# (Convert.ToBoolean(Eval("BLOQUEAR_SOL_AMP") )) %>'></asp:TextBox>
                                       <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                            TargetControlID="txtInicio"
                                            Mask="99/99/9999"
                                            MessageValidatorTip="true"
                                            OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="Date"
                                            DisplayMoney="Left"
                                            AcceptNegative="Left"
                                            ErrorTooltipEnabled="True" />--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="F.Termino">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFin" runat="server" Text='<%# Eval("A_FECHA_FIN_AMPLIACION") %>' Width="140px" TextMode="Date" Enabled='<%# (Convert.ToBoolean(Eval("BLOQUEAR_SOL_AMP") )) %>'></asp:TextBox>
   <%--                                     <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                                            TargetControlID="txtFin"
                                            Mask="99/99/9999"
                                            MessageValidatorTip="true"
                                            OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="Date"
                                            DisplayMoney="Left"
                                            AcceptNegative="Left"
                                            ErrorTooltipEnabled="True" />--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ampliar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Enabled='<%# (Convert.ToBoolean(Eval("BLOQUEAR_SOL_AMP") )) %>'/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                
            </div>
        </div>

    </div>
    <div class="row">
        <div class="col-md-6">
            <label>Adjuntar documento</label>
            <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
            <asp:Label ID="Label1" runat="server" Text="Archivo...."></asp:Label>
        </div>
        <div class="col-md-6">
            <br />
            <asp:Button ID="btnGuardar" runat="server" Text="Enviar" OnClick="btnGuardar_Click" Visible="False" />
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                    Mensaje
                </div>
                <div class="body">
                    ¿Deseas solicitar ampliación? <br />
                    advertencia: una vez solicitado no podra realizar modificaciones
                </div>
                <div class="footer" align="right">
                    <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
            </asp:Panel>

        </div>
</div>
     </ContentTemplate>
      <Triggers>
          <asp:PostBackTrigger ControlID="btnGuardar"  />
      </Triggers>
 </asp:UpdatePanel>
</asp:Content>
