<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="AmpliacionEquipo.aspx.cs" Inherits="CAREMENOR_AmpliacionEquipo" %>


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
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>AMPLIACION DE EQUIPOS  </b>
            </div>
        </div>
    </div>
    <div class="row">
        
        <div class="col-md-2">
            <label>Ingresar PDC</label>
            <asp:TextBox ID="txtPdc" runat="server"></asp:TextBox>
        </div>
          <div class="col-md-2">
             <label>Estados</label>
             <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                 <asp:ListItem Value="0">TODOS</asp:ListItem>
                 <asp:ListItem Value="1">PENDIENTE</asp:ListItem>
                 <asp:ListItem Value="2">REVISADO</asp:ListItem>
                 <asp:ListItem Value="3">RECHAZADO</asp:ListItem>
             </asp:DropDownList>
             </div>
        <div class="col-md-4">
            <br />
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnAmpliaciones" runat="server" ImageUrl="~/imagenes/boton.regresar.gif" OnClick="btnAmpliaciones_Click" />
            </div>
        <div class="col-md-4">
            <br />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/EstadoColores2.jpg" />
        </div>
    </div>
    <br />
    <div class="row">
        
        <div class="col-md-12">

            <div class="table-responsive">
              
                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,Reqs_ItemSecuencia,A_FASES_AMPLIACION" EmptyDataText="There are no data records to display." Font-Size="8pt" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>


                            
                            <%--<asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos. Alq" SortExpression="D_SOLPED_ALQUILER" >--%>


                            
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblFases" runat="server" Text='<%# Eval(" A_FASES_AMPLIACION") %>' Visible="false"></asp:Label>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval(" FASES_AMPLIACION_IMG") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="#Requerimientos#" SortExpression="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,Reqs_CodigoCompleto" />
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
                                <asp:TemplateField HeaderText="Pos. Alq">
                                    <itemtemplate>
                                    <asp:Label ID="lblposicionAlq" runat="server" Text='<%# Eval("D_SOLPED_ALQUILER") %>'></asp:Label>
                                </itemtemplate>

                                </asp:TemplateField>

                            

                            

                            <asp:BoundField DataField="D_PDC_MONTO_ALQ_INI" HeaderText="Monto Alq." SortExpression="D_PDC_MONTO_ALQ_INI" />

   


                            <asp:TemplateField HeaderText="Monto Mov." >
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoMov" runat="server" Text='<%# Eval("D_PDC_MONTO_MOVIL") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Visible="false" Text='<%# Eval("A_TIPO_AMPLIACION") %>'></asp:Label>
                                    <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal" >
                                        <asp:ListItem>T/P</asp:ListItem>
                                        <asp:ListItem>P</asp:ListItem>
                                       
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monto Amp." >
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoAmp" runat="server" Text='<%# Eval("MONTO_AMPLIACION_SOL") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BackColor="#FFFF66"/>
                            </asp:TemplateField>

                            

                            

                            <asp:TemplateField HeaderText="Monto Final Alq.">
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoAlq" runat="server" Text='<%# Eval("D_PDC_MONTO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BackColor="#FFCC66" />
                            </asp:TemplateField>

   


                            <asp:BoundField DataField="A_FECHA_INI_AMPLIACION" HeaderText="F.Inicio Amp." SortExpression="A_FECHA_INI_AMPLIACION">
                            <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="A_FECHA_FIN_AMPLIACION" HeaderText="F.Termino Amp." SortExpression="A_FECHA_FIN_AMPLIACION">
                            <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Adjuntar archivo">
                                           <ItemTemplate>
                                               <asp:FileUpload ID="FileUpload1" runat="server" />
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                  
                            <asp:TemplateField HeaderText="File">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("A_FILE_AMPLIACION") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("ADJUNTO_AMP") )) %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nueva posicion">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNuevaPosicion" runat="server" Width="50px" Text='<%# Eval("D_SOLPED_ALQUILER_ORIGEN") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                                  
                                       <asp:TemplateField HeaderText="Guardar" Visible="False">
                                           <ItemTemplate>
                                            <center>
                                            <asp:ImageButton ID="btnVer" runat="server" CommandArgument='<%# Eval("Reqs_CodigoCompleto") %>' ImageUrl="~/imagenes/SaveIcono20.png" ToolTip="Ver datos" onclick="VerDatos" />
                                            </center>
                                            </ItemTemplate>
                                       </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                
            </div>
        </div>
       

    </div>
    <div class="row">
        
        <div class="col-md-12">
            
            <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnAgregar_Click" Visible="False" />
        </div>
     
</div>

</asp:Content>

