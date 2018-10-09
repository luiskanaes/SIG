<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="SolicitudReclutamientoAll.aspx.cs" Inherits="RRHH_SolicitudReclutamientoAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
  
        $(":text").keydown(function(event) {

            if (event.keyCode == '13') {

                event.preventDefault();

            }

        });
        
        function KeyPressed(e)
        { return ((window.event) ? event.keyCode : e.keyCode) != 13; }


        function isNumberKey(evt) {
               
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    

                return false;
            }
            else {

                return true;
            }
        }

        var counter = 0;
        function AddFileUpload() {
            var div = document.createElement('DIV');
            div.innerHTML = '<input id="file' + counter + '" name = "file' + counter + '" type="file" /><input id="Button' + counter + '" type="button" value="Borrar" onclick = "RemoveFileUpload(this)" />';
            document.getElementById("FileUploadContainer").appendChild(div);
            counter++;
        }
        function RemoveFileUpload(div) {
            document.getElementById("FileUploadContainer").removeChild(div.parentNode);
        }

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
                title: 'Mensaje del Sistema',
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
     .Ancho {
                width: 30%;
                 padding: 10px;
  background-color: #ffffff;
  border: 1px solid #999999;
  border: 1px solid rgba(0, 0, 0, 0.2);
  border-radius: 6px;
  outline: none;
  -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, 0.5);
          box-shadow: 0 3px 9px rgba(0, 0, 0, 0.5);
  background-clip: padding-box;
            }
       
         @media only screen and (max-width: 500px) {
            .Ancho {
                width: 95%;
            }
        }
       </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-3">
            
            <asp:ImageButton ID="btnRegresar" runat="server" ImageUrl="~/imagenes/boton.regresar.gif" OnClick="btnRegresar_Click"  />
            <asp:ImageButton ID="btnImagen" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnImagen_Click"  />
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <div class="row">
       
            <div class="col-lg-12 ">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_ASIGNACION,CODIGO_CARE_PADRE" EmptyDataText="There are no data records to display." Font-Size="9pt" >
                        <Columns>
                            <asp:BoundField DataField="Row" HeaderText="#" SortExpression="Row" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="TICKET" SortExpression="TICKET">
                                <HeaderTemplate>
                                    <label>Ticket</label><br />
                                    <asp:TextBox ID="txtTICKET_F" runat="server" onkeypress="return event.keyCode!=13" Width="100px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TICKET") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PERSONAL" SortExpression="PERSONAL">
                                <HeaderTemplate>
                                    <label>PERSONAL</label><br />
                                    <asp:TextBox ID="txtNOMBRE_F" runat="server" onkeypress="return event.keyCode!=13"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("NOMBRE_COMPLETO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="COD_CENTRO" SortExpression="COD_CENTRO">
                                <HeaderTemplate>
                                    <label>C.Centro</label><br />
                                    <asp:TextBox ID="txtCOD_CENTRO_F" runat="server" onkeypress="return event.keyCode!=13"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("COD_CENTRO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO" />
                            <asp:BoundField DataField="AREA" HeaderText="Area" SortExpression="AREA" />

                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Adjunto">
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/RRHH/MOI_SOL/"+Eval("FILE_SOL") %>'  Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("FLG_URL") )) %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Solicitud">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_ASIGNACION") %>' ImageUrl="~/imagenes/pencil_add.ico" ToolTip="Ver solicitud" OnClick="ver_ficha" CausesValidation="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Aprobación">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEmail" runat="server" CommandArgument='<%# Eval("IDE_ASIGNACION") %>' ImageUrl="~/imagenes/email_go.ico" ToolTip="Reenviar notificación" OnClick="reenviar_aprobacion" CausesValidation="false" Visible='<%# (Convert.ToBoolean(Eval("FLG_MOBILES") )) %>'/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Recursos">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnRecursos" runat="server" CommandArgument='<%# Eval("IDE_ASIGNACION") %>' ImageUrl="~/imagenes/page_edit.ico" ToolTip="Ver atención" OnClick="Ver_recursos" CausesValidation="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Anular">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAnular" runat="server" CommandArgument='<%# Eval("IDE_ASIGNACION") %>' ImageUrl="~/imagenes/delete.png" ToolTip="Anular requerimiento" OnClick="Anular_requerimiento" CausesValidation="false" Visible='<%# (Convert.ToBoolean(Eval("BLOQUEAR") )) %>'/>
                                    <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnAnular"></cc1:ConfirmButtonExtender>
                                    <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnAnular"
                                        OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                        <div class="header">
                                            Mensaje
                                        </div>
                                        <div class="body">
                                            ¿Deseas anular requerimiento?
                                        </div>
                                        <div class="footer" align="right">
                                            <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                            <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                        </div>
                                        </asp:Panel>
                                
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
            </div>
       
    </div>

    <asp:HiddenField ID="HidRegistro" runat="server" />
    <cc1:ModalPopupExtender ID="ModalRegistro"
        runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="Panel1"
        PopupDragHandleControlID="Panel1"
        TargetControlID="HidRegistro">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Ancho">
        <div class="row">
            <div class="col-md-12">
                <h3>Recursos asignados</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no data records to display.">
                    <Columns>
                        <asp:BoundField DataField="DES_ASUNTO" HeaderText="Descripcion" SortExpression="DES_ASUNTO" />
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                        <asp:BoundField DataField="FEC_APROBADO" HeaderText="Fecha" SortExpression="FEC_APROBADO" />
                    </Columns>
                </asp:GridView>
            </div>
            
        </div>
        <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" />
                </div>
            </div>
    </asp:Panel>
</asp:Content>

