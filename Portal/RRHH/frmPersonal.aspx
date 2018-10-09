<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmPersonal.aspx.cs" Inherits="RRHH_frmPersonal" MaintainScrollPositionOnPostback="true" %>
<%@ MasterType VirtualPath="~/Templates/MPAdmin.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    //document.onselectstart = function () { return false; }
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
        function SoloNum(e) {
            var key_press = document.all ? key_press = e.keyCode : key_press = e.which;
            return ((key_press > 47 && key_press < 58) || key_press == 46);
            // el  "|| key_press == 46" es para incluir el punto ".", si borramos solo ingresara enteros 
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
   </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
     <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
    <br />
			
        <table style="width:100%" class="">
            <tr>
                <td style="width: 50px; text-align: center">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Usuarios.png" 
                        Width="50px" />
                </td>
                <td class="headerText">
                    REGISTRO POSTULANTE MOI</td>

                           <td style="width: 50px; text-align: center">
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/requerimiento.png" onclick="btnRequerimiento_Click" 
                        ToolTip="Registro Requerimiento" Width="52px" Height="50px" />
                    </td>

                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnPersonal" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_5.png" onclick="btnPersonal_Click" 
                        ToolTip="Registro Postulante" Width="52px" Height="50px" />
                </td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnControl" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_2.png" 
                        onclick="btnControl_Click" ToolTip="Control MOD" Width="55px" />
                </td>
                <td style="width: 50px; text-align: center">
                <asp:ImageButton ID="btnSeguimiento" runat="server" CausesValidation="False" 
                        Height="50px" ImageUrl="~/imagenes/Indicadores_4.png" 
                        onclick="btnSeguimiento_Click" ToolTip="Seguimiento MOD" Width="50px" /></td>
                <td style="width: 50px; text-align: center">
                    <asp:ImageButton ID="btnReportes" runat="server" CausesValidation="False" 
                        ImageUrl="~/imagenes/Indicadores_3.png" onclick="btnReportes_Click" 
                        ToolTip="Reporte MOD" Width="50px" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center">
                <hr /></td>
            </tr>
        </table>
        <div  class="sky-form">
            

            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>DATOS GENERALES </b><asp:Label ID="lblCodigoPersonal" runat="server" CssClass="EtiquetaNegrita" 
                            Text="0" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <center>
                         <asp:Image ID="ImgFoto" runat="server" Height="120px" 
                            ImageUrl="~/imagenes/Foto_Fondo.png" Width="120px" />
                    </center>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="EtiquetaNegrita">Tipo Documento</label>
                    <asp:DropDownList ID="ddlDocumento" runat="server" CssClass="ddl">
                    </asp:DropDownList>

                </div>
                <div class="col-md-3">
                    <label class="EtiquetaNegrita">Nro Documento</label>
                        <asp:TextBox ID="txtDNI" runat="server"  MaxLength="12" 
                            AutoPostBack="True" ontextchanged="txtDNI_TextChanged" TabIndex="1"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="reqCisterna" runat="server" 
                            controltovalidate="txtDNI" CssClass="errorMessage" errormessage="Ingresar Documento" 
                            Font-Size="8pt" validationgroup="Validar" />
                </div>
                <div class="col-md-6">
                    <br />
                    <asp:ImageButton ID="btnBuscador" runat="server" 
                            ImageUrl="~/imagenes/boton.buscar.gif" onclick="btnBuscador_Click" 
                            ToolTip="Consultar DNI" />
                        <asp:ImageButton ID="btnLimpiador" runat="server" 
                            ImageUrl="~/imagenes/boton.cancelar.gif" onclick="btnLimpiador_Click" 
                            ToolTip="Limpiar Controles"/>
                </div>
               
            </div>

            <div class="row">
                <div class="col-md-3">
                       <label class="EtiquetaNegrita">Nombres</label>
                        <asp:TextBox ID="txtNombre" runat="server"  MaxLength="30" 
                             TabIndex="2"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="EtiquetaNegrita">Apellido Paterno</label>
                        <asp:TextBox ID="txtApePat" runat="server"  MaxLength="30" 
                             TabIndex="3"></asp:TextBox>
                        
                </div>
                
                <div class="col-md-6">
                    <label class="EtiquetaNegrita">Apellido Materno</label>
                        <asp:TextBox ID="txtApeMat" runat="server"  MaxLength="30" 
                             TabIndex="4"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                                         <label class="EtiquetaNegrita">Fecha Nacimiento</label>
                                        <asp:TextBox ID="txtNacimiento" runat="server"  
                            TabIndex="5"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                             TargetControlID="txtNacimiento"
                             Mask="99/99/9999"
                             MessageValidatorTip="true"
                             OnFocusCssClass="MaskedEditFocus"
                             OnInvalidCssClass="MaskedEditError"
                             MaskType="Date"
                             DisplayMoney="Left"
                             AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                         
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNacimiento" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy"   />
                </div>
                <div class="col-md-3">
                     <label class="EtiquetaNegrita">Teléfono</label>
                        <asp:TextBox ID="txtFono" runat="server" MaxLength="30"  TabIndex="6"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="EtiquetaNegrita">Cargo</label>
                        <asp:DropDownList ID="ddlCargos" runat="server" CssClass="ddl" >
                        </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label class="EtiquetaNegrita">Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server"  ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="EtiquetaNegrita">Estado Civil</label>
                        <asp:DropDownList ID="ddlCivil" runat="server" CssClass="ddl" >
                        </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label>Foto</label>
                       <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
                <div class="col-md-6">
                     <label class="EtiquetaNegrita">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" ></asp:TextBox>
                </div>
                
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
                         <asp:Button ID="btnRegistrar" runat="server" 
                            Text="Registrar"  validationgroup="Validar" 
                            onclick="btnRegistrar_Click" />
                    </center>
                </div>

            </div>
            <br />
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>BÚQUEDA DE POSTULANTES</b>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                     <asp:TextBox ID="txtPersonal" runat="server" 
                            AutoPostBack="True" ontextchanged="txtPersonal_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                       <asp:GridView ID="GridPersonal" runat="server"
                            AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover">
                            <Columns>
                                <asp:BoundField DataField="Row" HeaderText="Row" SortExpression="Row" />
                                <asp:BoundField DataField="DES_NOMBRES" HeaderText="Apellidos y Nombres" 
                                    SortExpression="DES_NOMBRE">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DES_DNI" HeaderText="DNI" SortExpression="DES_DNI" />
                                <asp:TemplateField HeaderText="Sel">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# Eval("DES_DNI") %>' 
                                            ImageUrl="~/imagenes/pencil_add.ico" ToolTip="Seleccionar" OnClick ="SeleccionarPersonal" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </div>
                <div class="col-md-3">
                </div>
            </div>

            
        </div>
 </ContentTemplate>
            <Triggers>    
            </Triggers>
</asp:UpdatePanel>
 <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="../imagenes/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>
    
