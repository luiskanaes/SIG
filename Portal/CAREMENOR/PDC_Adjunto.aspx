<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="PDC_Adjunto.aspx.cs" Inherits="CAREMENOR_PDC_Adjunto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
         .box {
                  width: 40%;
                  margin: 0 auto;
                  background: #fff;
                  padding: 35px;
                  border: 2px solid #fff;
                  border-radius: 5px;
                  background-clip: padding-box;
                  text-align: center;
              }
          @media screen and (max-width: 700px) {
                  .box {
                      width: 70%;
                  }

                  .popup {
                      width: 100%;
                  }
              }
    </style>
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




            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                         <b>Datos de PDC</b>
                      
                        
                    </div>
                </div>
            </div>


            <div class="row">

                <div class="col-md-4">
                          

                     <div class="row">
                            <div class="col-md-12">
                                <label>Requerimientos asociados</label>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo" EmptyDataText="There are no data records to display." Font-Size="8pt" >
                                            <Columns>

                                               
                                                <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Requerimientos" SortExpression="Reqs_CodigoCompleto" />
                                                <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos.Alq" SortExpression="D_SOLPED_ALQUILER" />
                                                <asp:TemplateField HeaderText="Monto Alq.">
                                                <ItemTemplate>
                                                   
                                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("D_PDC_MONTO") %>' ></asp:TextBox>

                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="D_SOLPED_MOVIMIENTO" HeaderText="Pos.Mov." SortExpression="D_SOLPED_MOVIMIENTO" />
                                                
                                               <asp:TemplateField HeaderText="Monto Mov.">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtValorMov" runat="server" Text='<%# Eval("D_PDC_MONTO_MOVIL") %>' ></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ver">
                                                     <ItemTemplate>
                                                        
                                                     <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/imagenes/pencil_add.ico" class="btn btn-primary" OnClick="VerdetallePrecio" CausesValidation="false" />
                                                          </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                            </div>
                             
                        </div>

                </div>

                <%--segundo grupo--%>

                <div class="col-md-8">
                      
                    <div class="row">
                    <div class="col-md-3">
                    <asp:CheckBox ID="CheckAmpliacion" runat="server" Text="Generar ampliación" Visible="False" />
                    </div>
                    <div class="col-md-3">

                    </div>
                    <div class="col-md-3">

                    </div>
                    <div class="col-md-3">

                    </div>
                    </div>
                  

                    <div class="row">
                        <div class="col-md-3">
                            <label>Fecha PDC</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtFechaPDC" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                    TargetControlID="txtFechaPDC"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" />

                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFechaPDC" PopupButtonID="ImageButton4" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                <span class="input-group-addon">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha PDC" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>PDC</label>
                            <asp:TextBox ID="txtPdc" runat="server" MaxLength="10"></asp:TextBox>
                        </div>


                        <div class="col-md-3">
                            <label>Moneda</label>
                            <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="ddl"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>Monto PDC</label>
                            <asp:TextBox ID="txtmonto" runat="server" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <label>Cargar archivo</label>
                             <asp:FileUpload ID="FileUploadGuia" runat="server" />
                            <%--<div class="input-group">
                                <label class="input-group-btn">
                                    <span class="btn btn-primary">
                                        <asp:FileUpload ID="FileUploadGuia" runat="server" />
                                    </span>
                                </label>

                            </div>--%>
                        </div>
                        <div class="col-md-3">

                            <br />
                            <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnGuardar_Click" />
                            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
                            <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnGuardar"
                                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                <div class="header">
                                    Mensaje
                                </div>
                                <div class="body">
                                    ¿Deseas guardar registro?
                                </div>
                                <div class="footer" align="right">
                                    <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                    <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                </div>
                            </asp:Panel>


                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-md-12">


                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
                                   EmptyDataText="There are no data records to display." Font-Size="8pt" 
                                   >
                                    <Columns>

                                        <%--<asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha" SortExpression="FECHA_REGISTRO" />--%>

                                        <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                        <asp:BoundField DataField="D_PDC" HeaderText="PDC" SortExpression="D_PDC" />
                                        <asp:BoundField DataField="MONEDA" HeaderText="MONEDA" SortExpression="MONEDA" />
                                        <asp:BoundField DataField="D_PDC_MONTO_TOTAL" HeaderText="MONTO" SortExpression="D_PDC_MONTO_TOTAL" />
                                        <asp:BoundField DataField="TIPO" HeaderText="Operacion" SortExpression="TIPO" />
                                        <asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha" SortExpression="FECHA_REGISTRO" />
                                        <asp:BoundField DataField="D_PDC_FILE" HeaderText="File" SortExpression="D_PDC_FILE" />
                                        <asp:TemplateField HeaderText="File">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("D_PDC_FILE") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("VISIBLE") )) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                            </div>


                        </div>

                    </div>
                      <br />
       
                     
                </div>

            </div>






    <asp:HiddenField ID="HidRegistro" runat="server" />
    <cc1:ModalPopupExtender ID="ModalRegistro"
        runat="server"
        CancelControlID="btnNo"
        BackgroundCssClass="modalBackground"
        PopupControlID="pnlPopup"
        PopupDragHandleControlID="pnlPopup"
        TargetControlID="HidRegistro">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="box" Width="65%">

     
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>DETALLE HISTORICO </b>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                                <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
                                   EmptyDataText="There are no data records to display." Font-Size="8pt" 
                                   >
                                    <Columns>

                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" SortExpression="codigo" />
                                        <asp:BoundField DataField="D_PDC" HeaderText="Pdc" SortExpression="D_PDC" />
                                        <asp:BoundField DataField="D_PDC_MONTO" HeaderText="Monto Alq." SortExpression="D_PDC_MONTO" />
                                        <asp:BoundField DataField="D_PDC_MONTO_MOVIL" HeaderText="Monto Mov." SortExpression="D_PDC_MONTO_MOVIL" />
                                        <asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha" SortExpression="FECHA_REGISTRO" />
                                    </Columns>
                                </asp:GridView>


                            </div>
                 <asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/imagenes/botonCerrar.jpg" />
            </div>
        </div>
    </asp:Panel>


</asp:Content>


