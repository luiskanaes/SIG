<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="RegularizacionEquipo.aspx.cs" Inherits="CAREMENOR_RegularizacionEquipo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

     
        //document.onselectstart = function () { return false; }
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
                title: 'Mensaje del Sistema',
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

        
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
          for (i = 1; i < GridView2.rows.length; i++) {
              GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
          }
      }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row" style ="padding :30px">
        
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/ssk80x30.fw.png" />
                    LISTA DE EQUIPOS A REVISAR  </b>
                </div>
            </div>
        </div>

        <div class="row">
            <div style="overflow: scroll; width: 100%;">
                
                <div class="col-md-12">
                    <div class="table-responsive">


                        <div class="row">
                            
                            <div class="col-md-4">
                                <asp:Button ID="btn" runat="server" Text="Procesar" OnClick="btn_Click" Visible="False" />
                                <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btn"></cc1:ConfirmButtonExtender>
                                <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btn"
                                    OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                        Mensaje
                                    </div>
                                    <div class="body">
                                        ¿Deseas procesar registros?
                                    </div>
                                    <div class="footer" align="right">
                                        <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                        <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                    </div>
                                </asp:Panel>

                                <asp:ImageButton ID="btnRespnder" runat="server" ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnRespnder_Click" Visible="False" />
                                <asp:Button ID="btnFile" runat="server" Text="Cargar" OnClick="btnFile_Click"  Style="display: none" />
                                <script type="text/javascript">
                                    function UploadFile(fileUpload) {
                                        if (fileUpload.value != '') {
                                            document.getElementById("<%=btnFile.ClientID %>").click();
            }
        }
                                </script>
                            </div>
                            <div class="col-md-6">
                                <p>Leyenda</p> <b>A: </b> atendido // <b>R:</b> Rechazado //<b>  P:</b> Pendiente // <b>  I+:</b> Solicitar más información
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                        <br />

                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-bordered table-hover" 
                            AutoGenerateColumns="False" EmptyDataText="No existen registros pendiente de atención" Font-Size="12pt" 
                            DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,RESUMEN_ESTADO_CGO" OnRowCreated="GridView1_RowCreated"
                             OnRowDataBound="GridView1_RowDataBound" >
                            <Columns>
                                <asp:TemplateField HeaderText="Item" SortExpression="ITEM">
                                    <HeaderTemplate>
                                        <label>Items</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label  ID="lblSelect" runat="server" Text='<%# Eval("Row") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="D_FECHA_APRUEBA_CGO" HeaderText="Fecha Revisión" SortExpression="D_FECHA_APRUEBA_CGO">
                                <ItemStyle VerticalAlign="Middle" />
                                </asp:BoundField>

                              <%--  <asp:BoundField DataField="CGO_ESTADO" HeaderText="Estados" SortExpression="CGO_ESTADO" />--%>

                                <asp:TemplateField HeaderText="Estado" >
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("RESUMEN_ESTADO_CGO") %>'></asp:Label>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Controles">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal" >
                                            <asp:ListItem>P</asp:ListItem>
                                            <asp:ListItem>A</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                            <asp:ListItem>I+</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                

                              

                                <asp:TemplateField HeaderText="Comentarios" SortExpression="Comentarios">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label31" runat="server" Text="Observaciones" Width="250px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtObservaciones" runat="server" Height="60px" TextMode="MultiLine" Text='<%# Eval("C_COMENTARIOS_CGO") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Respuestas" SortExpression="Respuestas">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label33" runat="server" Text="Respuestas" Width="250px" ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtRespuesta" runat="server" Height="60px" TextMode="MultiLine" Text='<%# Eval("C_RESPUESTAS_CGO") %>'></asp:TextBox>
                                        <b>Cargar archivos</b>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requerimientos" SortExpression="Requerimientos">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label42" runat="server" Text="Requerimientos" Width="100px"></asp:Label>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label43" runat="server" Text='<%# Bind("Reqs_CodigoCompleto") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:BoundField DataField="Reqs_CtdReservada" HeaderText="Cantidad requerida" SortExpression="Reqs_CtdReservada">

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>


                                <asp:TemplateField HeaderText="Familia" SortExpression="FAMILIA">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label2" runat="server" Text="Familia" Width="180px"></asp:Label>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Fami_Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SubFamilia" SortExpression="SUBFAMILIA">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label3" runat="server" Text="SubFamilia" Width="140px"></asp:Label>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Subf_Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Marca" SortExpression="Marca">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label5" runat="server" Text="Marca" Width="150px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Marc_Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Modelo" SortExpression="Modelo">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label7" runat="server" Text="Modelo" Width="150px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Mode_Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Capacidad" SortExpression="Capacidad">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label21" runat="server" Text="Capacidad" Width="120px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label22" runat="server" Text='<%# Bind("Equi_Capacidad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Observaciones">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label9" runat="server" Text="Observaciones" Width="250px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("Reqd_Observaciones") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="descripcion_alquiler" SortExpression="descripcion_alquiler">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label23" runat="server" Text="Descripción Req. adicionales" Width="250px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label24" runat="server" Text='<%# Bind("descripcion_alquiler") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>DOCUMENTACIÓN  </b>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-bordered table-hover"  AutoGenerateColumns="False" DataKeyNames="ide_LegajoFile,FILE_ARCHIVO,CODIGO_GRUPO,FLG_EXTRA" EmptyDataText="There are no data records to display." Font-Size="8pt" OnRowDataBound="GridView2_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                            <asp:BoundField DataField="FILE_NOMBRE" HeaderText="File" SortExpression="FILE_NOMBRE" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" />
                            <asp:BoundField DataField="fecha_registro" HeaderText="Fecha" SortExpression="fecha_registro" />
                            <asp:BoundField DataField="CODIGO_GRUPO" HeaderText="Grupo Legajo" SortExpression="CODIGO_GRUPO">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Carga" HeaderText="Origen carga" SortExpression="Carga" />
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("FILE_ARCHIVO") %>' Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                         
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

