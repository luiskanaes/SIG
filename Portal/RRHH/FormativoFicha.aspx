<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="FormativoFicha.aspx.cs" Inherits="RRHH_FormativoFicha" %>
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
     <style type="text/css">
        
        .auto-style1 {
            width: 100%;
            height: 10%;
        }
          
      .button {
  padding: 5px 15px;
            background: #195183;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
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
    <div class="row">
        <div class="col-md-4">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/mensaje_formativo-01.fw.png" />
        </div>
        <div class="col-md-4">
            <center>
            <asp:Image ID="imgFotos" runat="server" Width="120px" /><br />
            <asp:HyperLink runat="server" ID="hpRegresar" ForeColor="#0066CC" NavigateUrl="~/RRHH/FormativoMenu.aspx">Regresar</asp:HyperLink>
            </center>
        </div>

        <div class="col-md-4">
            <asp:Label ID="lblCodigoFicha" runat="server" Visible="False" ></asp:Label>
        </div>
    </div>
   
    <div class="row">
        <div class="col-md-3">
            <label>Apellidos</label>
            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Nombres</label>
            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Dni</label>
            <asp:TextBox ID="txtDni" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Fecha de Nacimiento</label>
            <asp:TextBox ID="txtFecNacimiento" runat="server"></asp:TextBox>
             <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="txtFecNacimiento"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled="True" />

                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecNacimiento" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" />
                </div>
        </div>
    <div class="row">
        <div class="col-md-3">
            <label>Edad</label>
            <asp:TextBox ID="txtedad" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Tipo de Programa</label>
            <asp:DropDownList ID="ddlPrograma" runat="server" CssClass ="ddl"></asp:DropDownList>
        </div>
        
        <div class="col-md-3">
            <label>Carrera</label>
            <asp:DropDownList ID="ddlcarrera" runat="server" CssClass ="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Universidad</label>
            <asp:DropDownList ID="ddluniversidad" runat="server" CssClass ="ddl"></asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Nivel alcanzado</label>
            <asp:DropDownList ID="ddlNivel" runat="server" CssClass ="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Núm. de colegiatura</label>
            <asp:TextBox ID="txtcolegiatura" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
             <label>Lugar de residencia</label>
            <asp:TextBox ID="txtdireccion" runat="server"></asp:TextBox>
        </div>

    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Estado civil</label>
            <asp:DropDownList ID="ddlcivil" runat="server" CssClass ="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Tutor</label>
            <asp:TextBox ID="txtTutor" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Tutor correo</label>
            <asp:TextBox ID="txtTutorCorreo" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Cargo</label>
            <asp:TextBox ID="txtCargo" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Segmentación del Talento</label>
            <asp:DropDownList ID="ddlsegmentacion" runat="server" CssClass ="ddl">
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Fecha fin contrato</label>
            <asp:TextBox ID="txtFin" runat="server"></asp:TextBox>
             <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtFin"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled="True" />

                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFin" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" />
                
        </div>
        <div class="col-md-3">
             <label>Telefono de contacto</label>
            <asp:TextBox ID="txtfono" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
             <label>Adjuntar foto</label>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label>Correo institucional</label>
            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
           
        </div>
    </div>
    <br />
    <div class="row">
        <label class="headerText">EVALUACIÓN (Jefe Inmediato)</label>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label>Fortalezas</label>
            <asp:TextBox ID="txtFortalezas" runat="server" Height="150px" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label>Oportunidades de mejora</label>
            <asp:TextBox ID="txtoportunidades" runat="server" Height="150px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
     <br />
    <div class="row">
        <label class="headerText">PLANES DE DESARROLLO</label>
    </div>
    <div class="row">
        <div class="col-md-3">
             <label>Periodo</label>
            <asp:TextBox ID="txtDuracion" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label>Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnPlanes" runat="server" Text="Agregar" OnClick="btnPlanes_Click" />
        </div>
        
    </div>
    <div class="row">
        <br />
        <center>
                <div class="table-responsive">
                <asp:GridView runat="server" ID="gridPlan" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" Width="60%" Font-Size="9pt">
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="Item" SortExpression="Row" />
                        <asp:BoundField DataField="DURACION" HeaderText="Periodo" SortExpression="DURACION" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" SortExpression="DESCRIPCION" />
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_PLANES") %>' ImageUrl="~/imagenes/Error.png" OnClick="Eliminar_Plan"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                 </div>
                </center>
    </div>
    <br />
    <div class="row">
        <label class="headerText">FASES</label>
    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Empresa</label>
            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass ="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Proyecto</label>
            <asp:DropDownList ID="ddlProyecto" runat="server" CssClass ="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Fecha inicio</label>
            <asp:TextBox ID="txtInicio" runat="server"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                TargetControlID="txtInicio"
                Mask="99/99/9999"
                MessageValidatorTip="true"
                OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError"
                MaskType="Date"
                DisplayMoney="Left"
                AcceptNegative="Left"
                ErrorTooltipEnabled="True" />

            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtInicio" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" />

        </div>
        <div class="col-md-3">
            <label>Fecha final</label>
            <asp:TextBox ID="txtTermino" runat="server"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                TargetControlID="txtTermino"
                Mask="99/99/9999"
                MessageValidatorTip="true"
                OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError"
                MaskType="Date"
                DisplayMoney="Left"
                AcceptNegative="Left"
                ErrorTooltipEnabled="True" />

            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTermino" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" />


        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Ubicación</label>
            <asp:TextBox ID="txtUbicacion" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Cargo</label>
            <asp:TextBox ID="txtCargoPracticante" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Jefe directo</label>
            <asp:TextBox ID="txtJefe" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
        <label>Correo Jefe</label>
        <asp:TextBox ID="txtCorreoJefe" runat="server"></asp:TextBox>
             <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtCorreoJefe" ErrorMessage="Formato de correo inválido" ForeColor="Red"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <label>Objetivos</label>
            <asp:TextBox ID="txtObjetivos" runat="server" Height="100px" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
        <center>
            <asp:Button ID="btnCancelarfase" runat="server" Text="Cancelar fase" OnClick="btnCancelarfase_Click"></asp:Button>
            <asp:Button ID="btnFases" runat="server" Text="Guardar fase" OnClick="btnFases_Click" />
        </center>
    </div>
    <br />
    <div class="row">
        <asp:Label ID="lblIDE_FASE" runat="server" Visible="false" ></asp:Label>
        <div class="col-lg-12 ">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDE_FICHA">
                <ItemTemplate>

                    <div class="col-lg- 12">
                        <div class="panel panel-primary">
                            <div class="panel-footer">
                               <b> <%# Eval("NRO_FASE")%> </b> 
                                <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl="~/imagenes/boton.actualizar.gif"  OnClick="ActualizarFase" />
                                <asp:ImageButton ID="btnEliminarFase" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl="~/imagenes/boton.eliminar.gif" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>' OnClick="EliminarFase" />
                                <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEliminarFase"></cc1:ConfirmButtonExtender>
                                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEliminarFase"
                                    OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                        Mensaje SSK
                                    </div>
                                    <div class="body">
                                        ¿Deseas eliminar fase?
                                    </div>
                                    <div class="footer" align="right">
                                        <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                        <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="panel-body">
                                <table class="auto-style1">
                                    <tr>
                                        <td>Proyecto</td>
                                        <td> <b><%# Eval("PROYECTO")%></b></td>
                                        <td>Fecha inicio</td>
                                        <td> <b><%# Eval("FECHA_INICIO")%></b></td>
                                        <td>Fecha fin</td>
                                        <td> <b><%# Eval("FECHA_FIN")%></b></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Ubicación</td>
                                        <td><b><%# Eval("UBICACION")%></b></td>
                                        <td>Cargo</td>
                                        <td><b><%# Eval("CARGO")%></b></td>
                                        <td>Jefe directo</td>
                                        <td><b><%# Eval("JEFE")%></b></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Autoevaluación (Mitad)</td>
                                        <td><b><%# Eval("PTO_SEGUIMIENTO_MITAD")%> </b>
                                            <asp:ImageButton ID="btnDesempenioM" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/PencilAdd.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>' OnClick ="View_MitadDesempenio"/>
                                            <asp:ImageButton ID="btnDesempenioCM" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/CORREO_ENVIAR.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>' OnClick ="EnviarExamen_MitadDesempenio"/>
                                        </td>
                                        <td>Ev. Desempeño (Mitad)</td>
                                        <td><b><%# Eval("PTO_DESEMPENIO_MITAD")%></b>
                                            <asp:ImageButton ID="btnSeguimientoM" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/PencilAdd.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>' OnClick ="View_MitadSeguimiento"/>
                                            <asp:ImageButton ID="btnSeguimientoCM" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/CORREO_ENVIAR.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN_MITAD") ) %>' OnClick ="EnviarExamen_MitadSeguimiento"/>
                                        </td>
                                        <td>Estado</td>
                                        <td><b><%# Eval("ESTADO_MITAD")%></b></td>
                                        <td>
                                            <asp:ImageButton ID="btnAbrirMitad" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl="~/imagenes/lock_open.png" OnClick ="Abrir_ExamenMitad" ToolTip ="Abrir evaluación (Mitad)"/></td>
                                        <td>
                                            <asp:ImageButton ID="btnCerrarMitad" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl="~/imagenes/lock_close.png" OnClick ="Cerrar_ExamenMitad" ToolTip ="Cerrar evaluación (Mitad)"/></td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Autoevaluación (Final)</td>
                                        <td><b><%# Eval("PTO_SEGUIMIENTO")%> </b>
                                            <asp:ImageButton ID="btnDesempenio" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/PencilAdd.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>' OnClick ="View_FinalDesempenio"/>
                                            <asp:ImageButton ID="btnDesempenioC" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/CORREO_ENVIAR.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>' OnClick ="EnviarExamen_FinalDesempenio"/>
                                        </td>
                                        <td>Ev. Desempeño (Final)</td>
                                        <td><b><%# Eval("PTO_DESEMPENIO")%></b>
                                            <asp:ImageButton ID="btnSeguimiento" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/PencilAdd.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>' OnClick ="View_FinalSeguimiento"/>
                                            <asp:ImageButton ID="btnSeguimientoC" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl ="~/imagenes/CORREO_ENVIAR.png" Visible='<%# Convert.ToBoolean(Eval("FLG_EXAMEN") ) %>' OnClick ="EnviarExamen_FinalSeguimiento"/>    
                                        </td>
                                        <td>Estado</td>
                                        <td><b><%# Eval("ESTADO")%></b></td>
                                        <td>
                                            <asp:ImageButton ID="btnAbrirFinal" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl="~/imagenes/lock_open.png" OnClick="Abrir_ExamenFinal" ToolTip ="Abrir evaluación (Final)"/></td>
                                        <td>
                                            <asp:ImageButton ID="btnCerrarFinal" runat="server" CommandArgument='<%# Eval("IDE_FASE") %>' ImageUrl="~/imagenes/lock_close.png" OnClick="Cerrar_ExamenFinal" ToolTip ="Cerrar evaluación (Final)"/></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                   
                                </table>

                            </div>

                            <div class="panel-footer"> <b>Objetivos: </b>
                                <%# Eval("OBSERVACIONES")%>
                            </div>

                        </div>
                    </div>


                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="row">
        <center>
             <asp:Button ID="btnGuardar" runat="server" Text="Guardar ficha" OnClick="btnGuardar_Click" />

           
            <button onclick="goBack()" class="button">Regresar</button>

            <script>
                function goBack() {
                    window.history.back();
                }
            </script>
       
        </center>
          
        
    </div>
     </ContentTemplate>
            <Triggers>    
                <asp:PostBackTrigger ControlID="btnGuardar"  />
            </Triggers>
</asp:UpdatePanel>
 
 <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:modalpopupextender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>


