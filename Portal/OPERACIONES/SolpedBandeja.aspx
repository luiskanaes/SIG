<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="SolpedBandeja.aspx.cs" Inherits="OPERACIONES_SolpedBandeja" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">

         .boton {
            padding: 5px 15px;
            background: #195183;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }


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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <br />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-6">
                         <b>ATENCIÓN DE SOLICITUD DE OR</b>
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                        <asp:HiddenField ID="hfPerfil" runat="server" />
                    </div>
                    <div class="col-md-6" style="text-align:right">
                          <asp:Button ID="btnSolicitar" runat="server" Text="SOLICITUD" OnClick="btnSolicitar_Click" />
                    <asp:Button ID="btnBandeja" runat="server" Text="ATENCION" OnClick="btnBandeja_Click" />
                    </div>
                    
                </div>
               
            </div>
        </div>
    </div>
    <div class="row">
                
                <div class="col-md-2">
                    <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Estados</label>
                    <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                        <asp:ListItem Value="1">PENDIENTE</asp:ListItem>
                        <asp:ListItem Value="2">PARCIAL</asp:ListItem>
                        <asp:ListItem Value="3">ATENDIDO</asp:ListItem>
                        <asp:ListItem Value="4">RECHAZADO</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Empresa</label>
                    <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" ></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Centros</label>
                    <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <br />
                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
                    <asp:ImageButton ID="btnDescargar" runat="server" ImageUrl="~/imagenes/boton.Excel.jpg" OnClick="btnDescargar_Click" />
                </div>
                <div class="col-md-4">
                    <b>Leyenda</b><br />
                    <b>A : </b> atendido, <b>AP : </b> atención parcial, <b>R : </b> rechazado
                </div>
                
            </div>
            <br />
       <div style="overflow: scroll; width: 100%; height: 450px;" >
               <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_SOLPED,SOLPED" EmptyDataText="There are no data records to display." Font-Size="8pt"  AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50" OnRowDataBound="GridView1_RowDataBound" AllowSorting="True" OnSorting="GridView1_Sorting" OnRowCreated="GridView1_RowCreated1" >
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="Control">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rdoOpcion" runat="server" RepeatDirection="Horizontal"  Enabled='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>' >
                                                        <asp:ListItem>P</asp:ListItem>
                                                        <asp:ListItem>A</asp:ListItem>
                                                        <asp:ListItem>AP</asp:ListItem>
                                                        <asp:ListItem>R</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%--  <asp:TemplateField HeaderText="Procesar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnProcesar" runat="server" CommandArgument='<%# Eval("IDE_SOLPED") %>' ImageUrl="~/imagenes/SaveIcono.png" ToolTip="Procesar solicitud" OnClick ="Procesar"  Enabled='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'/>
                                                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnProcesar">
                                                        </cc1:ConfirmButtonExtender>
                                                        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnProcesar"
                                                            OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                                        </cc1:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                                Mensaje SSK
                                                            </div>
                                                            <div class="body">
                                                                ¿Deseas procesar solicitud?
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                                                <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                                            </div>
                                                        </asp:Panel>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                           

                                           
                                                <asp:TemplateField HeaderText="TICKET" SortExpression="TICKET">
                                                    <headertemplate>
                                                    <label>Ticket</label><br />
                                                        <asp:TextBox ID="txtTICKET_F" runat="server" Width="90px" OnTextChanged="txtTICKET_F_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </headertemplate>
                                                    <itemtemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("TICKET") %>'></asp:Label>
                                                </itemtemplate>

                                                </asp:TemplateField>

                                          
                                           

                                            <asp:TemplateField HeaderText="NOM_CREADO" SortExpression="NOM_CREADO">
                                                <HeaderTemplate>
                                                    <label>Creador por</label><br />
                                                    
                                                        <asp:TextBox ID="txtNOM_CREADO_F" runat="server" Width="120px"  OnTextChanged="txtNOM_CREADO_F_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                       

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("NOM_CREADO") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="NOM_SOLICITA" SortExpression="NOM_SOLICITA">
                                                <HeaderTemplate>
                                                    <label>Solicitante</label><br />
                                                        <asp:TextBox ID="txtNOM_SOLICITA_F" runat="server" Width="120px" OnTextChanged="txtNOM_SOLICITA_F_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("NOM_SOLICITA") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>

                                          <%--  <asp:BoundField DataField="NOM_SOLICITA" HeaderText="Solicitante" SortExpression="NOM_SOLICITA" />--%>
                                            <asp:BoundField DataField="COD_SI" HeaderText="SI" SortExpression="COD_SI" >

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Tipo_Solicitud" SortExpression="Tipo_Solicitud">
                                                    <headertemplate>
                                                    <label style="width:95px">Tipo Solicitud</label>
                                                        
                                                </headertemplate>
                                                    <itemtemplate>
                                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("SOLICITUD") %>'></asp:Label>
                                                </itemtemplate>

                                    </asp:TemplateField>

                                             <asp:TemplateField HeaderText="FEC_SOLICITA" SortExpression="FEC_SOLICITA">
                                                <HeaderTemplate>
                                                    <label>Fecha solicitud</label><br />
                                                    
                                                        <asp:TextBox ID="txtFecSol_F" runat="server" Width="95px" OnTextChanged="txtFecSol_F_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender26" runat="server"
                                                            TargetControlID="txtFecSol_F"
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
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("FECHA") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="COMENTARIOS" SortExpression="COMENTARIOS">
                                            <HeaderTemplate>
                                            <asp:Label ID="Label33" runat="server" Text="Comentarios" Width="200px"></asp:Label><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="Label33" runat="server" Text='<%# Bind("COMENTARIOS") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="F_ESTADO" HeaderText="Estado" SortExpression="F_ESTADO" >
                                            <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                            <asp:BoundField DataField="SOLPED" HeaderText="Codigo" SortExpression="SOLPED" >
                                            <ItemStyle BackColor="#FFFF66" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_ATENCION" HeaderText="Fecha atencion" SortExpression="FECHA_ATENCION" >
                                            <ItemStyle Width="70px" />
                                            </asp:BoundField>
                                            

                                            <asp:TemplateField HeaderText="OBSERVACIONES" SortExpression="OBSERVACIONES">
                                            <HeaderTemplate>
                                            <asp:Label ID="Label32" runat="server" Text="Observaciones" Width="200px"></asp:Label><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("OBSERVACIONES") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Estado" Visible="False">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO_RESUMEN") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                 <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="CONTROL" SortExpression="CONTROL">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text="Controles" Width="140px"></asp:Label>
                                                    <br />
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                            <asp:LinkButton ID="LinkButtonA" runat="server" Text="A" OnClick ="Procesar_A"  CssClass="boton" visible='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonAP" runat="server" Text="AP" OnClick ="Procesar_AP"  CssClass="boton" visible='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonR" runat="server" Text="R" OnClick ="Procesar_R"  CssClass="boton" visible='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle Width="170px" />
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="File">
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/SOLPED/"+Eval("FILE_SOLPED") %>'  Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
                        </div>
           </div>
    <div class="row">
        <div class="col-md-12">
              <asp:GridView ID="gvExcel" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                  AutoGenerateColumns="False" DataKeyNames="IDE_SOLPED,SOLPED" EmptyDataText="There are no data records to display." Font-Size="8pt"  >
                                        <Columns>
                                           

                                           
                                          <%--  <asp:BoundField DataField="NOM_SOLICITA" HeaderText="Solicitante" SortExpression="NOM_SOLICITA" />--%>
                                           
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                           

                                           
                                            <asp:BoundField DataField="TICKET" HeaderText="Ticket" SortExpression="TICKET" />
                                            <asp:BoundField DataField="NOM_CREADO" HeaderText="Creado por" SortExpression="NOM_CREADO" />
                                            <asp:BoundField DataField="NOM_SOLICITA" HeaderText="Solicitante" SortExpression="NOM_SOLICITA" />
                                            <asp:BoundField DataField="COD_SI" HeaderText="SI" SortExpression="COD_SI" >
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="SOLICITUD" HeaderText="Tipo Solicitud" SortExpression="SOLICITUD" />

                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Solicitud" SortExpression="FECHA" />
                                          

                                            <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios" SortExpression="COMENTARIOS" >
                                           
                                            </asp:BoundField>
                                            <asp:BoundField DataField="F_ESTADO" HeaderText="Estado" SortExpression="F_ESTADO" >
                                            
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                            <asp:BoundField DataField="SOLPED" HeaderText="Codigo" SortExpression="SOLPED" >
                                            <ItemStyle BackColor="#FFFF66" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_ATENCION" HeaderText="Fecha atencion" SortExpression="FECHA_ATENCION" >
                                            
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" SortExpression="OBSERVACIONES" >
                                           
                                            </asp:BoundField>
                                           

                                        </Columns>
                                    </asp:GridView>
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
     
            
           
                

                    
                        <%--<div class="col-md-4">
                            <center>
                            
                                <br />
                            <table id="tbl_rol"  >
                             <thead>

                                 <tr>
                                     <th width="30%"><asp:Button ID="Button1" runat="server" Text="1" OnClick="Button1_Click" /></th>
                                     <th width="30%"><asp:Button ID="Button2" runat="server" Text="2" OnClick="Button2_Click" /></th>
                                     <th width="30%"><asp:Button ID="Button3" runat="server" Text="3" OnClick="Button3_Click" /></th>
                                      
                                 </tr>
                                 <tr>
                                     <th width="30%"><asp:Button ID="Button4" runat="server" Text="4" OnClick="Button4_Click" style="height: 32px" /></th>
                                     <th width="30%"><asp:Button ID="Button5" runat="server" Text="5" OnClick="Button5_Click" /></th>
                                     <th width="30%"><asp:Button ID="Button6" runat="server" Text="6" OnClick="Button6_Click" /></th>
                                      
                                 </tr>
                                 <tr>
                                     <th width="30%"><asp:Button ID="Button7" runat="server" Text="7" OnClick="Button7_Click" /></th>
                                     <th width="30%"><asp:Button ID="Button8" runat="server" Text="8" OnClick="Button8_Click" /></th>
                                     <th width="30%"><asp:Button ID="Button9" runat="server" Text="9" OnClick="Button9_Click" /></th>
                                      
                                 </tr>
                                 <tr>
                                     <th width="30%"></th>
                                     <th width="30%"><asp:Button ID="Button0" runat="server" Text="0" OnClick="Button0_Click" /></th>
                                     <th width="30%"></th>
                                      
                                 </tr>
                             </thead>
                         </table>
                            </center>

                        </div>--%>
        <div class="row">
            <div class="col-md-12">
                <br />
                <asp:RadioButtonList runat="server" ID="rdoTipo" ForeColor="Black" RepeatDirection="Horizontal" Width="90%" Font-Size="10pt">
                    <asp:ListItem Selected="True" Value="SOLPED">SOLPED</asp:ListItem>
                    <asp:ListItem Value="Care">CARE</asp:ListItem>
                    <asp:ListItem Value="MOBILE">MOBILE</asp:ListItem>
                    <asp:ListItem Value="SAT">SAT</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            </div>
            <div class="row">
            <div class="col-md-12">
                <asp:Label runat="server" Font-Bold="True" ForeColor="Black" ID="lblMensaje">Codigo</asp:Label>
                <asp:TextBox runat="server" ID="txtSolped" MaxLength="10" onkeydown="return jsDecimals(event);" ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            controltovalidate="txtSolped" CssClass="errorMessage" 
                            errormessage="Ingresar codigo" validationgroup="Validar" />

                <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtSolped" 
                    ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{10,10}$" runat="server" 
                    ErrorMessage="falta completar los 10 digitos requeridos"  CssClass="errorMessage" validationgroup="Validar">

                </asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                
                <asp:Label ID="lblrpta" runat="server" Visible="false"></asp:Label>
                
                <asp:Label runat="server" Font-Bold="True" ForeColor="Black" ID="lblobservacion">Observaciones</asp:Label>
                <asp:TextBox runat="server" ID="txtSustento" Height="120px" MaxLength="500" TextMode="MultiLine" Visible="False"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="reqSustento" runat="server" 
                            controltovalidate="txtSustento" CssClass="errorMessage" 
                            errormessage="Ingresar comentarios" validationgroup="Validar" />

               

                <asp:TextBox runat="server" ID="txtrechazo" Height="120px" MaxLength="500" TextMode="MultiLine" Visible="False"></asp:TextBox>
               
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            controltovalidate="txtrechazo" CssClass="errorMessage" 
                            errormessage="Ingresar comentarios" validationgroup="ValidarRechazo" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">

                <br />
                <center>
                <asp:Button runat="server" Text="Cancelar" ID="btnCancel" OnClick="btnCancel_Click"></asp:Button>
                <asp:Button runat="server" Text="Grabar" ID="btnGrabar" OnClick="btnGrabar_Click" Visible="False" validationgroup="ValidarRechazo"></asp:Button>
                <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"  validationgroup="Validar" Visible="False" ></asp:Button>
                </center>
            </div>
        </div>
       
    </asp:Panel>
    
</asp:Content>

