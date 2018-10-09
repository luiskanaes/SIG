<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="MOD_PERSONAL.aspx.cs" Inherits="OPERACIONES_MOD_PERSONAL" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-9">
                        <b>ESTATUS REQUERIMIENTO DE PERSONAL</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnRegresa" runat="server" Text="Regresar" OnClick="btnRegresa_Click" CssClass="btn-danger active" />
                    </div>
                    
                </div>
                
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Etapas</label>
            <asp:DropDownList ID="ddlEtapas" runat="server" CssClass="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Nacionalidad</label>
            <asp:DropDownList ID="ddlNacionalidad" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlNacionalidad_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-3">
             <label>N° Documento</label>
            <asp:TextBox ID="txtDni" runat="server" MaxLength="12"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblCodigo" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
             <label>Apellido paterno</label>
            <asp:TextBox ID="txtPaterno" runat="server" MaxLength="100"></asp:TextBox>
        </div>
        <div class="col-md-3">
             <label>Apellido materno</label>
            <asp:TextBox ID="txtMaterno" runat="server" MaxLength="100"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Nombres</label>
            <asp:TextBox ID="txtNombres" runat="server" MaxLength="100"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Fecha nacimiento</label>
            <div class="input-group">
                <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                    TargetControlID="txtFechaNac"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaNac" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                </span>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <label>Ciudad</label>
        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCiudad_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-3">
              <label>Provincia</label>
        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-3">
             <label>Distrito</label>
        <asp:DropDownList ID="ddlDistrito" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Telefonos</label>
            <asp:TextBox ID="txtTelefonos" runat="server" MaxLength="100"></asp:TextBox>
        </div>
    </div>
     <div class="row">
        <div class="col-md-3">
             <label>Mano de Obra</label>
            <asp:DropDownList ID="ddlMano" runat="server" CssClass="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
             <label>Condicion</label>
            <asp:DropDownList ID="ddlCondicion" runat="server" CssClass="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Examen medico</label>
            <div class="input-group">
                <asp:TextBox ID="txtMedico" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    TargetControlID="txtMedico"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMedico" PopupButtonID="ImageButton1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha examen medico" />
                </span>
            </div>
        </div>
        <div class="col-md-3">
            <label>Charla TR</label>
            <div class="input-group">
                <asp:TextBox ID="txtTr" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="txtTr"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTr" PopupButtonID="ImageButton2" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Charla TR" />
                </span>
            </div>
        </div>
    </div>
     <div class="row">
        <div class="col-md-3">
             <label>Charla SSK</label>
            <div class="input-group">
                <asp:TextBox ID="txtSSK" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtSSK"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSSK" PopupButtonID="ImageButton3" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Charla SSK" />
                </span>
            </div>
        </div>
        <div class="col-md-3">
            <label>Charla altura</label>
            <div class="input-group">
                <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                    TargetControlID="txtAltura"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAltura" PopupButtonID="ImageButton4" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Charla altura" />
                </span>
            </div>
        </div>
        <div class="col-md-3">
             <label>Charla espacios confinados</label>
            <div class="input-group">
                <asp:TextBox ID="txtEspacio" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                    TargetControlID="txtEspacio"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtEspacio" PopupButtonID="ImageButton5" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="espacios confinados" />
                </span>
            </div>
        </div>
        <div class="col-md-3">
               <label>Charla trabajos en caliente</label>
            <div class="input-group">
                <asp:TextBox ID="txtCaliente" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                    TargetControlID="txtCaliente"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtCaliente" PopupButtonID="ImageButton6" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="trabajos en caliente" />
                </span>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
              <label>Entrega File TR</label>
            <div class="input-group">
                <asp:TextBox ID="txtFile" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                    TargetControlID="txtFile"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtFile" PopupButtonID="ImageButton7" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="File TR" />
                </span>
            </div>
        </div>
        <div class="col-md-3">
              <label>Acceso a planta</label>
            <div class="input-group">
                <asp:TextBox ID="txtPlanta" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server"
                    TargetControlID="txtPlanta"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtPlanta" PopupButtonID="ImageButton8" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Acceso a planta" />
                </span>
            </div>
        </div>
        <div class="col-md-3">
              <label>Fotocheck</label>
            <asp:DropDownList ID="ddlFotocheck" runat="server" CssClass="ddl"></asp:DropDownList>
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
             <label>Observaciones</label>
            <asp:TextBox ID="txtObservaciones" runat="server" Height="80px" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
        </div>

    </div>
    <div class="row">
        <div class="col-md-12">
             <br />
            <center>
<asp:Button runat="server" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click"></asp:Button>
                <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn-danger active"></asp:Button>
                <asp:Button runat="server" Text="Descargar" ID="btnDescarga" OnClick="btnDescarga_Click" ></asp:Button>
            </center>
        </div>

    </div>
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
    <br />
     <div style="overflow: scroll; width: 100%; height: 450px;" >
      <div class="row">
           <div class="col-md-12">                
                                
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                        AutoGenerateColumns="False" 
                                        DataKeyNames="REQ_PERSONAL,IDE_REQUERIMIENTO" EmptyDataText="There are no data records to display."  OnRowCreated="GridView1_RowCreated">
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="ITEM" SortExpression="ITEM">
                                                <HeaderTemplate>
                                                    <label>ITEM</label><br />
                                                    <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Text='<%# Eval("Row") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ETAPAS" SortExpression="ETAPAS">
                                                <HeaderTemplate>
                                                    <label>ETAPAS</label><br />
                                                    <asp:DropDownList ID="ddlEtapas_F" runat="server" CssClass="ddl" Width="100px" >
                                                        <asp:ListItem Value="0">-------</asp:ListItem>
                                                        <asp:ListItem Value="1">CESADO</asp:ListItem>
                                                        <asp:ListItem Value="2">CONVOCATORIA</asp:ListItem>
                                                        <asp:ListItem Value="3">PROCESO</asp:ListItem>
                                                        <asp:ListItem Value="4">STAND BY</asp:ListItem>
                                                        <asp:ListItem Value="5">TERMINADO</asp:ListItem>
                                                        <asp:ListItem Value="6">ANULADO</asp:ListItem>
                                                  
                                                    </asp:DropDownList>
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("ETAPAS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           

                                            <asp:BoundField DataField="CATEGORIA" HeaderText="CATEGORIA" SortExpression="CATEGORIA" />
                                            <asp:BoundField DataField="ESPECIALIDAD" HeaderText="ESPECIALIDAD" SortExpression="ESPECIALIDAD" />
                                           

                                            <asp:BoundField DataField="DNI" HeaderText="DNI" SortExpression="DNI" >
                                           

                                            <ItemStyle HorizontalAlign="Center" />
                                           

                                            </asp:BoundField>
                                           

                                            <asp:BoundField DataField="APE_PATERNO" HeaderText="APELLIDO.PATERNO" SortExpression="APE_PATERNO" >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="APE_MATERNO" HeaderText="APELLIDO.MATERNO" SortExpression="APE_MATERNO" />
                                            <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" SortExpression="NOMBRES" />
                                            <asp:BoundField DataField="FEC_NACIMIENTO" HeaderText="FEC.NACIMIENTO" SortExpression="FEC_NACIMIENTO">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EDAD" HeaderText="EDAD" SortExpression="EDAD" >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DES_UBIGEO" HeaderText="PROCEDENCIA(UBIGEO)" SortExpression="DES_UBIGEO" />
                                            <asp:BoundField DataField="TELEFONOS" HeaderText="TELEFONOS" SortExpression="TELEFONOS" />
                                            <asp:BoundField DataField="MANO_OBRA" HeaderText="MANO.OBRA" SortExpression="MANO_OBRA">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CONDICION" HeaderText="CONDICION" SortExpression="CONDICION">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="MEDICO" SortExpression="MEDICO">
                                                <HeaderTemplate>
                                                    <label>EXAMEN MEDICO</label><br />
                                                    
                                                        <asp:TextBox ID="txtExaMedico_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server"
                                                            TargetControlID="txtExaMedico_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FEC_EXA_MEDICO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TR" SortExpression="TR">
                                                <HeaderTemplate>
                                                    <label>CHARLA TR.</label><br />
                                                    
                                                        <asp:TextBox ID="txtTr_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender20" runat="server"
                                                            TargetControlID="txtTr_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FEC_CHARLA_TR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="SSK" SortExpression="SSK">
                                                <HeaderTemplate>
                                                    <label>CHARLA SSK</label><br />
                                                    
                                                        <asp:TextBox ID="txtSSK_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender21" runat="server"
                                                            TargetControlID="txtSSK_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FEC_CHARLA_SSK") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ALTURA" SortExpression="ALTURA">
                                                <HeaderTemplate>
                                                    <label>CHARLA ALTURA</label><br />
                                                    
                                                        <asp:TextBox ID="txtALTURA_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender22" runat="server"
                                                            TargetControlID="txtALTURA_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FEC_CHARLA_ALTURA") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ESPACIOS" SortExpression="ESPACIOS">
                                                <HeaderTemplate>
                                                    <label>ESPACIOS CONF.</label><br />
                                                    
                                                        <asp:TextBox ID="txtESPACIO_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender23" runat="server"
                                                            TargetControlID="txtESPACIO_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FEC_CHARLA_ESP_CONFINADO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CALIENTE" SortExpression="CALIENTE">
                                                <HeaderTemplate>
                                                    <label>TRAB. CALIENTE</label><br />
                                                    
                                                        <asp:TextBox ID="txtCaliente_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender24" runat="server"
                                                            TargetControlID="txtCaliente_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("FEC_CHARL_CALIENTE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FILE" SortExpression="FILE">
                                                <HeaderTemplate>
                                                    <label>ENTREGA FILE.TR</label><br />
                                                    
                                                        <asp:TextBox ID="txtFileTr_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender25" runat="server"
                                                            TargetControlID="txtFileTr_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("FEC_ENTREGA_FILE_TR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PLANTA" SortExpression="PLANTA">
                                                <HeaderTemplate>
                                                    <label>ACCESO PLANTA</label><br />
                                                    
                                                        <asp:TextBox ID="txtPlanta_F" runat="server" Width="100px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender26" runat="server"
                                                            TargetControlID="txtPlanta_F"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("FEC_ACCESO_PLANTA") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FOTOCHECK" SortExpression="FOTOCHECK">
                                                <HeaderTemplate>
                                                    <label>FOTOCHECK</label><br />
                                                    <asp:DropDownList ID="ddlFOTOCHECK_F" runat="server" CssClass="ddl" Width="100px" >
                                                        <asp:ListItem Value="0">-------</asp:ListItem>
                                                        <asp:ListItem Value="1">NO</asp:ListItem>
                                                        <asp:ListItem Value="2">SI</asp:ListItem>
                                                        <asp:ListItem Value="3">CARTA HABILITADA</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("FOTOCHECK") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="OBSERVACIONES/COMENTARIOS" SortExpression="OBSERVACIONES">
                                            </asp:BoundField>
                                           

                                            <asp:TemplateField HeaderText="EDITAR">
                                                <ItemTemplate>
                                                    <cemter>

                                                         <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/imagenes/PencilAdd.png" OnClick="Ver" CommandArgument='<%# Eval("REQ_PERSONAL") %>' />
                                                    </cemter>
                                                   
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ELIMINAR">
                                                <ItemTemplate>
                                                    <cemter>
                                                         <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/imagenes/Ico_delete.png" OnClick="Eliminar" CommandArgument='<%# Eval("REQ_PERSONAL") %>' />
                                                        
                                                    </cemter>
                                                    <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                                                    <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEliminar"
                                                        OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                                        <div class="header">
                                                            Mensaje
                                                        </div>
                                                        <div class="body">
                                                            ¿Deseas eliminar registro?
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
            <asp:Button ID="btnMasivo" runat="server" Text="Guardar varios" OnClick="btnMasivo_Click" CssClass="btn-danger active" />
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
                                
                                    <asp:GridView ID="gvExcel" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                        AutoGenerateColumns="False" 
                                        DataKeyNames="REQ_PERSONAL,IDE_REQUERIMIENTO" EmptyDataText="There are no data records to display."  Visible="False">
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="ITEM" ReadOnly="True" SortExpression="Row" >
                                           

                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                           

                                            <asp:BoundField DataField="RESPONSABLE" HeaderText="RESPONSABLE" SortExpression="RESPONSABLE" />
                                           

                                            <asp:BoundField DataField="ETAPAS" HeaderText="ETAPAS" SortExpression="ETAPAS" />
                                            <asp:BoundField DataField="CATEGORIA" HeaderText="CATEGORIA" SortExpression="CATEGORIA" />
                                            <asp:BoundField DataField="ESPECIALIDAD" HeaderText="ESPECIALIDAD" SortExpression="ESPECIALIDAD" />
                                            <asp:BoundField DataField="DNI" HeaderText="DNI" SortExpression="DNI">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="APE_PATERNO" HeaderText="APELLIDO.PATERNO" SortExpression="APE_PATERNO" />
                                            <asp:BoundField DataField="APE_MATERNO" HeaderText="APELLIDO.MATERNO" SortExpression="APE_MATERNO" />
                                            <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" SortExpression="NOMBRES" />
                                            <asp:BoundField DataField="FEC_NACIMIENTO" HeaderText="FEC.NACIMIENTO" SortExpression="FEC_NACIMIENTO">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EDAD" HeaderText="EDAD" SortExpression="EDAD" />
                                            <asp:BoundField DataField="DES_UBIGEO" HeaderText="PROCEDENCIA(UBIGEO)" SortExpression="DES_UBIGEO" />
                                            <asp:BoundField DataField="TELEFONOS" HeaderText="TELEFONOS" SortExpression="TELEFONOS" />
                                            <asp:BoundField DataField="MANO_OBRA" HeaderText="MANO.OBRA" SortExpression="MANO_OBRA">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CONDICION" HeaderText="CONDICION" SortExpression="CONDICION">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_EXA_MEDICO" HeaderText="EXAMEN MEDICO" SortExpression="FEC_EXA_MEDICO">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_CHARLA_TR" HeaderText="TR" SortExpression="FEC_CHARLA_TR">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_CHARLA_SSK" HeaderText="SSK" SortExpression="FEC_CHARLA_SSK">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_CHARLA_ALTURA" HeaderText="ALTURA" SortExpression="FEC_CHARLA_ALTURA">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_CHARLA_ESP_CONFINADO" HeaderText="ESPACIO CONFINADO" SortExpression="FEC_CHARLA_ESP_CONFINADO">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_CHARL_CALIENTE" HeaderText="TRABAJOS CALIENTES" SortExpression="FEC_CHARL_CALIENTE">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_ENTREGA_FILE_TR" HeaderText="ENTREGA FILE TR" SortExpression="FEC_ENTREGA_FILE_TR">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FEC_ACCESO_PLANTA" HeaderText="ACCESO PLANTA" SortExpression="FEC_ACCESO_PLANTA">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FOTOCHECK" HeaderText="FOTOCHECK" SortExpression="FOTOCHECK">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                           

                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="OBSERVACIONES/COMENTARIOS" SortExpression="OBSERVACIONES" />
                                           

                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                   
                              
                        </div>
   </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardar" />
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

