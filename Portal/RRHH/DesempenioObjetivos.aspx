<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesempenioObjetivos.aspx.cs" Inherits="RRHH_DesempenioObjetivos" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
                width: 50%;
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
            border:groove;
  
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
        }
    </style>
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
        <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>MIS OBJETIVOS</b>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col-md-4">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Desempenio180x90.fw.png" PostBackUrl="~/RRHH/Desemepenio.aspx" />
            <br />
            <label>Listar objetivos</label>
            <asp:DropDownList ID="ddlObejtivos" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlObejtivos_SelectedIndexChanged"></asp:DropDownList>

        </div>
        <div class="col-md-4">
             <asp:Chart ID="Chart2" runat="server" CssClass="img-responsive" Width="350px" EnableViewState="true">
                <Series>
                    <asp:Series Name="Series1" XValueMember="obj2" YValueMembers="TOTAL_TRANSCURRIDOS"></asp:Series>
                    <asp:Series Name="Series2" XValueMember="obj2" YValueMembers="TOTAL_DIAS" ></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
                <Titles>
                    <asp:Title Name="Title1" Text="Días Transcurridos">
                    </asp:Title>
                </Titles>
                <Legends>
                    <asp:Legend Name="Legend1" TitleAlignment="Near" Title="Leyenda"  ></asp:Legend>
                </Legends>
            </asp:Chart>
        </div>

        <div class="col-md-4">
            <asp:Chart ID="Chart1" runat="server" CssClass="img-responsive" Width="350px"  EnableViewState="true">
                <Series>
                     <asp:Series Name="Series1" XValueMember="obj" YValueMembers="AVANCE" ></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
                <Titles>
                    <asp:Title Name="Title1" Text="Avance de objetivos">
                    </asp:Title>
                </Titles>
            </asp:Chart>
           
        </div>
    </div>

    <div class="row">

        <div class="col-lg-12 ">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDE_OBJETIVO" OnItemDataBound="ListView1_ItemDataBound">
                <ItemTemplate>
                    <div class="row">
                        <%--       INICIO CUERPO 1--%>
                        <div class="col-md-8">

                            <ul class="timeline">
                                <li class="timeline-inverted">
                                    <div class="timeline-panel" style="background-color: white;">
                                        <div class="timeline-heading">
                                            <h4 class="timeline-title">
                                                <p>
                                                    <%--<asp:Image ID="IMG" runat="server" Visible='<%# (Convert.ToBoolean(Eval("FLG_APROBADO") )) %>' ImageUrl='<%# (Eval("IMG_OK") ) %>' />--%>
                                                   <%# Eval("ROW")%>.- 
                                                    <%# Eval("OBJETIVO")%>
                                                    <asp:Label ID="lblIDE_OBJETIVO" runat="server" Text='<%# Eval("IDE_OBJETIVO")%>' Visible="false" ></asp:Label>
                                                    <%-- <small class="text-muted"><i class="fa fa-time"></i><%# Eval("FECHA_REGISTRO")%></small>--%>
                                                </p>

                                            </h4>

                                        </div>
                                        <br />
                                        <div class="timeline-body">
                                            <div class="row">
                                                <div class="col-md-4" style="text-align: justify;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Desempenio_calender.png" />
                                                    <b>Fecha Inicio</b><br />
                                                    <%# Eval("INICIO")%>
                                                </div>
                                                <div class="col-md-4" style="text-align: justify;">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Desempenio_calender.png" />
                                                    <b>Fecha Termino</b><br />
                                                    <%# Eval("TERMINO")%>
                                                </div>
                                            
                                                <div class="col-md-4" style="text-align: justify;">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/Desempenio_calender.png" />
                                                    <b>Fecha ampliación</b>
                                                    <asp:ImageButton ID="btnFecha" runat="server" Visible='<%# (Convert.ToBoolean(Eval("FLG_AMPLIACION") )) %>'  CommandArgument='<%# Eval("IDE_OBJETIVO")%>' ImageUrl="~/imagenes/pencil_add.ico" ToolTip="Solicitar fecha de ampliación" OnClick="RealizarAmpliacion"/>
                                                    <br />
                                                    <%# Eval("FECHA_AMPLIACION")%>
                                                </div>
                                             </div>
                                            
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/imagenes/Desempenio_peso.png" />
                                                    <b>Peso (%)</b><br />
                                                    <%# Eval("PESO")%>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/Desempenio_Indicador.png" />
                                                    <b>Indicador</b><br />
                                                    <%# Eval("INDICADOR")%>
                                                </div>
                                                
                                            
                                                <div class="col-md-4">
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/imagenes/Desempenio_calender.png" />
                                                    <label>Fecha de aprobación</label><br />
                                                      <%# Eval("FECHA_APROBADO")%>
                                                </div>
                                                
                                                
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label>Avance (%)</label><br />
                                                    <asp:DropDownList ID="ddlAvance" runat="server" ></asp:DropDownList >
                                                    <%-- <asp:ImageButton ID="btnSaveAvance" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO")%>' ImageUrl="~/imagenes/D_SaveAvance.png" CssClass="img-responsive" OnClick="GuardarAvance" />--%>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("IDE_OBJETIVO")%>' ForeColor="Red">(Guardar avance)</asp:LinkButton>
                                                  
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                            </div>
                                            <br />
                                            

                                                   

                                                 <%--   <asp:ImageButton ID="btnCorreo" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO")%>' ImageUrl="~/imagenes/Desempenio_comenta.png" ToolTip="Enviar correo de observación" OnClick="VerBotonEnviarMSg" />--%>
<%--                                                    <asp:ImageButton ID="btnChat" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO")%>' ImageUrl="~/imagenes/Desempenio_chat.png" ToolTip="Listar observaciones" OnClick="verchat" />--%>
                                                    <%--      <asp:Label ID="lblcaht" runat="server" Text='<%#"Respuesta nuevas: " + Eval("RESPUESTA")%>'></asp:Label>--%>
                                               
                                        
                                            

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <%-- <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="~/imagenes/Desempenio_comenta.png" ToolTip="Enviar correo de observación" />--%>
                                                    <label>Enviar comentarios</label>
                                                    <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Height="150" Font-Size="9pt"></asp:TextBox>
                                                </div>
                                            </div>
                                           
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>Adjuntar archivo</label>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </div>
                                                <div class="col-md-6">
                                                </div>
                                                
                                            </div>
                                            <br />
                                            <div class="row">
                                                

                                                <div class="col-md-12">
                                                     <asp:ImageButton ID="btnLike" CommandArgument='<%# Eval("IDE_OBJETIVO")%>' runat="server" ImageUrl="~/imagenes/Desempenio_btn1.png" ToolTip="Aprobar objetivo" OnClick="likeObjetivo" Visible='<%# (Convert.ToBoolean(Eval("VISIBLE") )) %>'  />
                                                    <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnLike"></cc1:ConfirmButtonExtender>
                                                    <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnLike"
                                                        OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                                        <div class="header">
                                                            Mensaje
                                                        </div>
                                                        <div class="body">
                                                            ¿Deseas aprobar objetivo?
                                                        </div>
                                                        <div class="footer" align="right">
                                                            <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                                            <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                                        </div>
                                                    </asp:Panel>


                                                    <asp:ImageButton ID="btn" runat="server" CommandArgument='<%# Eval("IDE_OBJETIVO")%>' OnClick="enviarComentario" ImageUrl="~/imagenes/Desempenio_btn2.png" />
                                                    <cc1:ConfirmButtonExtender ID="cbeX" runat="server" DisplayModalPopupID="mpeX" TargetControlID="btn"></cc1:ConfirmButtonExtender>
                                                    <cc1:ModalPopupExtender ID="mpeX" runat="server" PopupControlID="pnlPopupX" TargetControlID="btn"
                                                        OkControlID="btnYesX" CancelControlID="btnNoX" BackgroundCssClass="modalBackground">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlPopupX" runat="server" CssClass="modalPopup" Style="display: none">
                                                        <div class="header">
                                                            Mensaje
                                                        </div>
                                                        <div class="body">
                                                            ¿Deseas enviar comentarios?
                                                        </div>
                                                        <div class="footer" align="right">
                                                            <asp:Button ID="btnYesX" runat="server" Text="Sí" CssClass="yes" />
                                                            <asp:Button ID="btnNoX" runat="server" Text="No" CssClass="no" />
                                                        </div>
                                                    </asp:Panel>

                                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <%--  FIN CUERPO 1--%>
                        <div class="col-md-4">
                            <br />
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
                                                <div class='<%# Eval("CLASS")%>'><%# Eval("COMENTARIO")%>	
                                                    <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/Desemepenio/"+Eval("F_FILE") %>' Visible ='<%# (Convert.ToBoolean(Eval("FILE_VISIBLE") )) %>'  ></asp:HyperLink>
                                                </div>
                                                
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </div>
                            <%--    INICIO CUERPO 2--%>
                        </div>
                        <%-- FIN CUERPO 2--%>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    
     <asp:HiddenField ID="HidObserva" runat="server" />
    <cc1:ModalPopupExtender ID="ModalObserva"
        runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="pnlPopupObserva"
        PopupDragHandleControlID="pnlPopupObserva"
        TargetControlID="HidObserva">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopupObserva" runat="server" CssClass="Ancho">
        <div class="row">
           
            <div class="col-md-3">
                <asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="col-md-6">
                  <label style="color:white">Fecha de ampliación</label>
                <div class="input-group">

                        <asp:TextBox ID="txtAmpliarFecha" runat="server" class="form-control" ></asp:TextBox>
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
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server"
                  ControlToValidate="txtAmpliarFecha"
                  ErrorMessage="Ingresar fecha de ampliación"
                  ForeColor="Orange" ValidationGroup="fecha"/>
            </div>
            <div class="col-md-3">
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-6">
                <label style="color:white">Comentarios</label>
                <asp:TextBox ID="txtComentar" runat="server" TextMode="MultiLine" Height="100" ValidationGroup="fecha" ></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                  ControlToValidate="txtComentar"
                  ErrorMessage="Ingresar comentarios"
                  ForeColor="Orange" ValidationGroup="fecha"/>
            </div>
            <div class="col-md-3">
            </div>
           
        </div>
        <br />
      <div class="row">
            <div class="col-md-12">
                <center>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Enviar" ValidationGroup="fecha" OnClick="btnSave_Click" />
                   

                </center>
                
            </div>
        <%--   <cc1:ConfirmButtonExtender ID="cbe3" runat="server" DisplayModalPopupID="mpe3" TargetControlID="btnSave"></cc1:ConfirmButtonExtender>
                   <cc1:ModalPopupExtender ID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="btnSave"
                    OkControlID="btnYes3" CancelControlID="btnNo3" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="header">
                    Mensaje
                    </div>
                    <div class="body">
                    ¿Deseas solicitar de ampliación de fecha?
                    </div>
                    <div class="footer" align="right">
                    <asp:Button ID="btnYes3" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo3" runat="server" Text="No" CssClass="no" />
                    </div>
                    </asp:Panel>--%>
            
        </div>
    </asp:Panel>
</asp:Content>

