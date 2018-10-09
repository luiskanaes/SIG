<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesempenioFijarObjetivoNuevo.aspx.cs" Inherits="RRHH_DesempenioFijarObjetivoNuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #EEAA00;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }

            input[type="submit"]:hover {
                outline: thin dotted #333;
                outline: 5px auto -webkit-focus-ring-color;
                outline-offset: -2px;
            }

        .Ancho {
            width: 40%;
        }
        .Ancho2 {
            width: 62%;
        }
        .chat_box {
            right: 20px;
            bottom: 0px;
        }

        .chat_body {
            background: white;
            height: 400px;
            padding: 5px 0px;
        }

        .chat_head, .msg_head {
            background: #f39c12;
            color: white;
            padding: 15px;
            font-weight: bold;
            cursor: pointer;
            border-radius: 5px 5px 0px 0px;
        }

        .msg_box {
            bottom: -5px;
            background: white;
            border-radius: 5px 5px 0px 0px;
        }

        .msg_head {
            background: #3498db;
        }

        .msg_body {
            background: white;
            height: 300px;
            font-size: 12px;
            padding: 15px;
            overflow: auto;
            overflow-x: hidden;
        }

        .msg_input {
            width: 100%;
            border: 1px solid white;
            border-top: 1px solid #DDDDDD;
            -webkit-box-sizing: border-box; /* Safari/Chrome, other WebKit */
            -moz-box-sizing: border-box; /* Firefox, other Gecko */
            box-sizing: border-box;
        }

        .close {
            float: right;
            cursor: pointer;
        }

        .minimize {
            float: right;
            cursor: pointer;
            padding-right: 5px;
        }

        .user {
            position: relative;
            padding: 10px 30px;
        }

            .user:hover {
                background: #f8f8f8;
                cursor: pointer;
            }

            .user:before {
                content: '';
                position: absolute;
                background: #2ecc71;
                height: 10px;
                width: 10px;
                left: 10px;
                top: 15px;
                border-radius: 6px;
            }

        .msg_a {
            position: relative;
            background: #FDE4CE;
            padding: 10px;
            min-height: 10px;
            margin-bottom: 5px;
            margin-right: 10px;
            border-radius: 5px;
        }

            .msg_a:before {
                content: "";
                position: absolute;
                width: 0px;
                height: 0px;
                border: 10px solid;
                border-color: transparent #FDE4CE transparent transparent;
                left: -20px;
                top: 7px;
            }


        .msg_b {
            background: #EEF2E7;
            padding: 10px;
            min-height: 15px;
            margin-bottom: 5px;
            position: relative;
            margin-left: 10px;
            border-radius: 5px;
        }

            .msg_b:after {
                content: "";
                position: absolute;
                width: 0px;
                height: 0px;
                border: 10px solid;
                border-color: transparent transparent transparent #EEF2E7;
                right: -20px;
                top: 7px;
            }

        @media only screen and (max-width: 500px) {
            .Ancho {
                width: 98%;
            }
            .Ancho2 {
                width: 98%;
            }
        }
    </style>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="row">
        <div class="col-md-4">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Desempenio180x90.fw.png" OnClick="ImageButton1_Click" />
        </div>
        <div class="col-md-4">
            <center>
                    <asp:Image ID="imgFotos" runat="server" Width="120px" />
                      
                        </center>
            <asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="col-md-4">
            <br />
            <br />
        <center>
                   
        <asp:Label runat="server" ID="lblnombre"></asp:Label><br />
        <asp:Label runat="server" ID="lblcargo" Font-Bold="True" Font-Size="8pt"></asp:Label>
        </center>
        </div>
    </div>
    <br />


      



    <%--  FIN SEGUNDO BLOQUE--%>
    
            <div class="row">
                
                
                <div class="col-md-12">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <b>LISTA DE OBJETIVOS  </b>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button ID="btnNuevo" runat="server" Text="Agregar Objetivo" OnClick="btnNuevo_Click" />

                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 ">

                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_OBJETIVO" EmptyDataText="No hay registros de objetivos a mostrar" Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OBJETIVO" HeaderText="OBJETIVO" SortExpression="OBJETIVO">
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="PESO" HeaderText="PESO %" SortExpression="PESO">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INICIO" HeaderText="INICIO" SortExpression="INICIO">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TERMINO" HeaderText="TERMINO" SortExpression="TERMINO">


                                            <HeaderStyle HorizontalAlign="Center" />


                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="FECHA_AMPLIACION" HeaderText="AMPLIACION" SortExpression="FECHA_AMPLIACION">


                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="APROBADO" HeaderText="V°B°" SortExpression="APROBADO">


                                            <HeaderStyle HorizontalAlign="Center" />


                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="U_CALIFICACION_PERSONA" HeaderText="AVANCE%" SortExpression="U_CALIFICACION_PERSONA">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>


                                        <asp:TemplateField HeaderText="CONTROLES">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO") %>' ImageUrl="~/imagenes/PencilAdd.png" OnClick="Update" ToolTip="Actualizar objetivo" />


                                                <asp:ImageButton ID="btnChat" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO") %>' ImageUrl="~/imagenes/Desempenio_chat.png" ToolTip="Ver observaciones" OnClick="VerChat" />


                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="ELIMINAR">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO") %>' ImageUrl="~/imagenes/Error.png" OnClick="Eliminar" ToolTip="Eliminar objetivo" Visible='<%# (Convert.ToBoolean(Eval("VISIBLE") )) %>' />
                                                <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                                                <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEliminar"
                                                    OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                                </cc1:ModalPopupExtender>
                                                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                                    <div class="header">
                                                        Mensaje
                                                    </div>
                                                    <div class="body">
                                                        ¿Deseas eliminar objetivo?
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
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <label>Enviar comentario sobre objetivos</label>
                            <asp:TextBox ID="txtComentarios" runat="server" Height="100px" MaxLength="350" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                        </div>


                    </div>
                    <br />


                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <center>
                    <asp:Button ID="btnCorreo" runat="server" Text="Enviar correo" OnClick="btnCorreo_Click" />

                    <cc1:ConfirmButtonExtender ID="cbe3" runat="server" DisplayModalPopupID="mpe3" TargetControlID="btnCorreo"></cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="btnCorreo"
                        OkControlID="btnYes3" CancelControlID="btnNo3" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Mensaje
                        </div>
                        <div class="body">
                            ¿Deseas enviar objetivo(s) asignado(s)?
                        </div>
                        <div class="footer" align="right">
                            <asp:Button ID="btnYes3" runat="server" Text="Sí" CssClass="yes" />
                            <asp:Button ID="btnNo3" runat="server" Text="No" CssClass="no" />
                        </div>
                    </asp:Panel>

                     <asp:Label ID="lblPesos" runat="server" Text="0" Visible="False"></asp:Label>
                      </center>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
                <br />

            </div>
    <%--   <asp:Label ID="Label3" runat="server" Text="COMENTARIOS" Font-Bold="True" ForeColor="#EEAA00"></asp:Label>
                <asp:TextBox ID="txtChat" runat="server" TextMode="MultiLine" Height="160px" Font-Size="9pt"></asp:TextBox>--%>
       

    <asp:HiddenField ID="HidRegistro" runat="server" />
    <cc1:ModalPopupExtender ID="ModalRegistro"
        runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="pnlPopup"
        PopupDragHandleControlID="pnlPopup"
        TargetControlID="HidRegistro">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="Ancho">
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-3">
            </div>
            <div class="col-md-3">
            </div>
            <div class="col-md-3">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <%--   <asp:Label ID="Label3" runat="server" Text="COMENTARIOS" Font-Bold="True" ForeColor="#EEAA00"></asp:Label>
                <asp:TextBox ID="txtChat" runat="server" TextMode="MultiLine" Height="160px" Font-Size="9pt"></asp:TextBox>--%>

                <div class="msg_head">
                    Comentarios
		                        
                </div>
                <div class="msg_box">
                    <div class="msg_wrap">
                        <div class="msg_body">
                            <asp:DataList ID="DataListChat" runat="server">
                                <ItemTemplate>
                                    <asp:Label ID="lblMsg" runat="server" Text='<%# Eval("NOMBRE_COMPLETO")%>' Font-Bold="true" Font-Size="8pt"></asp:Label>
                                    <asp:Label ID="lblfecha" runat="server" Text='<%# Eval("FECHA_ENVIO")%>' Font-Bold="true" Font-Size="7pt"></asp:Label>
                                    <div class='<%# Eval("CLASS")%>'>
                                        <%# Eval("COMENTARIO")%>
                                        <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/Desemepenio/"+Eval("F_FILE") %>' Visible='<%# (Convert.ToBoolean(Eval("FILE_VISIBLE") )) %>'></asp:HyperLink>
                                    </div>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>

            </div>

        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="RESPUESTA" Font-Bold="True" ForeColor="#EEAA00"></asp:Label>
                <asp:TextBox ID="txtRespuesta" runat="server" TextMode="MultiLine" Height="100px" Font-Size="9pt"></asp:TextBox>
            </div>

        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <center>
                    <asp:Button runat="server" Text="Cancelar" ID="btnCancel" OnClick="btnCancel_Click"></asp:Button>
                    <asp:Button runat="server" Text="Enviar" ID="btnEnvia" OnClick="btnEnvia_Click"></asp:Button>
                </center>
            </div>

        </div>
    </asp:Panel>

    <asp:HiddenField ID="HidObserva" runat="server" />
    <cc1:ModalPopupExtender ID="ModalObserva"
        runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="pnlPopupObserva"
        PopupDragHandleControlID="pnlPopupObserva"
        TargetControlID="HidObserva">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopupObserva" runat="server" CssClass="Ancho2">
            <div class="row">
        <div class="col-md-12">
             <br />
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <label style="color:white">Familia</label>
                    <asp:DropDownList ID="ddlFamilia" runat="server" CssClass="ddl" AutoPostBack="True" Enabled="False"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                </div>

            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <label style="color:white">Objetivo</label>
                    <asp:TextBox ID="txtObjetivos" runat="server" Height="120px" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                </div>

                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <label style="color:white">Indicador</label>
                    <asp:TextBox ID="txtIndicador" runat="server" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label style="color:white">Peso (%)</label>
                    <asp:TextBox ID="txtPeso" runat="server" onkeydown="return jsDecimals(event);" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <label style="color:white">Fecha inico</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtInicio" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                            TargetControlID="txtInicio"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInicio" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                        </span>
                    </div>
                </div>

                <div class="col-md-3">
                    <label style="color:white">Fecha Termino</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtfin" runat="server" class="form-control"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtfin"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfin" PopupButtonID="btnCalender2" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha termino" />
                        </span>
                    </div>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <asp:Panel ID="PanelAmpliacion" runat="server">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblfecha" runat="server" Text="Ampliar fecha" Font-Bold="True" ForeColor="White"></asp:Label>
                    <div class="input-group">

                        <asp:TextBox ID="txtAmpliarFecha" runat="server" class="form-control" Enabled="False"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                            TargetControlID="txtAmpliarFecha"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAmpliarFecha" PopupButtonID="btnCalender3" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="btnCalender3" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha ampliación" />
                        </span>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rdoAprobar" runat="server" ForeColor="White">
                        <asp:ListItem Value="1">APROBAR</asp:ListItem>
                        <asp:ListItem Value="0">RECHAZAR</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-3">
                </div>
                </asp:Panel>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
                    <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click"></asp:Button>
                    <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>

                    <cc1:ConfirmButtonExtender ID="cbe1x" runat="server" DisplayModalPopupID="mpe1x" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpe1x" runat="server" PopupControlID="pnlPopup1x" TargetControlID="btnGuardar"
                    OkControlID="btnYes1x" CancelControlID="btnNo1x" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup1x" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                    Mensaje
                    </div>
                    <div class="body">
                    ¿Deseas guardar objetivo?
                    </div>
                    <div class="footer" align="right">
                    <asp:Button ID="btnYes1x" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo1x" runat="server" Text="No" CssClass="no" />
                    </div>
                    </asp:Panel>
            </center>
                </div>
                <br />
            </div>

            <%--  FIN SEGUNDO BLOQUE--%>
        </div>
    </div>
      
    </asp:Panel>
</asp:Content>

