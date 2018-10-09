<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="AndamiosBandeja.aspx.cs" Inherits="OPERACIONES_AndamiosBandeja" %>

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
    function popupAndamios(CECOS_GESTOR, IDE_ANDAMIOS, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("AndamiosAtencion.aspx?CECOS_GESTOR=" + CECOS_GESTOR + "&IDE_ANDAMIOS=" + IDE_ANDAMIOS, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
        function EditarSol(CECOS_GESTOR, IDE_ANDAMIOS, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("AndamiosActualizar.aspx?CECOS_GESTOR=" + CECOS_GESTOR + "&IDE_ANDAMIOS=" + IDE_ANDAMIOS, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-9">
                         <b>ATENCIÓN DE SOLICITUD DE ANDAMIOS</b>
                        <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                    </div>
                    <div class="col-md-3">
                         <asp:Button ID="btnSolicitar" runat="server" Text="SOLICITUD" OnClick="btnSolicitar_Click" />
            <asp:Button ID="btnBandeja" runat="server" Text="ATENCION" OnClick="btnBandeja_Click" />
                    </div>
                    
                </div>
               
            </div>
        </div>
    </div>
    <div class="row">
        
        <div class="col-md-4">
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
            
        </div>
    </div>
    <div class="row">
                <div class="col-md-2">
                    <label>Ticket</label>
                    <asp:TextBox ID="txtTicket" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
        <div class="col-md-2">
            <label>Centros</label>
                    <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged"></asp:DropDownList>
        </div>
                <div class="col-md-2">
                    <label>Estados</label>
                    <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                       <asp:ListItem Value="1">PENDIENTE</asp:ListItem>
                        <asp:ListItem Value="2">SOLICITUD GENERADA</asp:ListItem>
                        <asp:ListItem Value="3">EN EJECUCIÓN</asp:ListItem>
                        <asp:ListItem Value="4">SOLICITUD TERMINADA</asp:ListItem>
                        <asp:ListItem Value="5">RECHAZADO</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Area</label>
                    <asp:DropDownList ID="ddlarea" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlarea_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                   <label>Especialidad</label>
                    <asp:DropDownList ID="ddlespecialidad" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlespecialidad_SelectedIndexChanged"></asp:DropDownList>
                </div>
               
                    
                
            </div>
    <br />
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-2">
        </div>

        <div class="col-md-2">
        </div>
        <div class="col-md-2">
        </div>
        <div class="col-md-2">
        </div>
        <div class="col-md-2">
        <asp:ImageButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" ImageUrl="~/imagenes/FileBuscar.png" ToolTip="Consultar" />
                    <asp:ImageButton ID="btnDescarga" runat="server" OnClick="btnDescarga_Click" ImageUrl="~/imagenes/FileExcel.png" ToolTip="Exportar" />
                </div>
    </div>
            <br />
     <div style="overflow: scroll; width: 100%; height: 450px;" >
               <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_ANDAMIOS,COD_TICKET,CECOS_GESTOR" EmptyDataText="There are no data records to display." Font-Size="8pt"  AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50" OnRowDataBound="GridView1_RowDataBound" AllowSorting="True" OnSorting="GridView1_Sorting" OnRowCreated="GridView1_RowCreated1" >
                                        <Columns>
                                          
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                           

                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud" SortExpression="FECHA" >
                                           

                                            <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                           

                                            <asp:BoundField DataField="TICKET" HeaderText="Ticket.atención" SortExpression="TICKET" >
                                            <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="ANDAMIOS" HeaderText="Código" SortExpression="ANDAMIOS" >
                                            <ItemStyle BackColor="#FFCC66" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOM_CREADO" HeaderText="Creado" SortExpression="NOM_CREADO" />
                                            <asp:BoundField DataField="NOM_SOLICITA" HeaderText="Solicitante" SortExpression="NOM_SOLICITA" />
                                            <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" SortExpression="Especialidad" />
                                            <asp:BoundField DataField="Area" HeaderText="Area" SortExpression="Area" />
                                            <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" SortExpression="Solicitud" />
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                            <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Fecha Requerida" SortExpression="FECHA_REQUERIDA" >
                                           
                                            <ItemStyle BackColor="#FFFF66" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios(Solicitante)" SortExpression="COMENTARIOS" >
                                            </asp:BoundField>
                                            
                                            
                                            <asp:TemplateField HeaderText="Prioridad">
                                                <ItemTemplate>
                                                    <cemter>

                                                         <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("IDE_ANDAMIOS") %>' Text='<%# Eval("PRIORIDAD") %>' 
                                                             OnClick ="Procesar_Prioridad"   Enabled='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>' Tootip="Indicar prioridad"></asp:LinkButton>
                                                         
                                                    </cemter>
                                                   
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="F_ESTADO" HeaderText="Estado" SortExpression="F_ESTADO">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                            </asp:BoundField>
                                            
                                            
                                            <asp:BoundField DataField="SUPERVIDOR" HeaderText="Nombre.Supervisor" SortExpression="SUPERVIDOR" >
                                            </asp:BoundField>
   
                                            <asp:BoundField DataField="CAPATAZ" HeaderText="Nombre.Capataz" SortExpression="CAPATAZ" />
                                            <asp:BoundField DataField="DURACION" HeaderText="Duracion (dias)" SortExpression="DURACION" />
                                            <asp:BoundField DataField="HORAS" HeaderText="Horas" SortExpression="HORAS" />
                                            <asp:BoundField DataField="FECHA_ENTREGA" HeaderText="Fecha inicio" SortExpression="FECHA_ENTREGA" />
   
                                            <asp:BoundField DataField="FECHA_TERMINO" HeaderText="Fecha termino" SortExpression="FECHA_TERMINO" >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_DESMONTAJE" HeaderText="Fecha desmontaje" SortExpression="FECHA_DESMONTAJE" />
   
                                           <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones(Atención)" SortExpression="OBSERVACIONES" >
                                            <ItemStyle Width="190px" />
                                            </asp:BoundField>
                                           <asp:TemplateField HeaderText="Estado" Visible="False">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO_RESUMEN") %>' ></asp:Label>
                                                 </ItemTemplate>
                                                 <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Controles.atención">
                                                <ItemTemplate>

                                            <asp:LinkButton ID="LinkButtonA" runat="server" Text="A" OnClick ="Procesar_A"  CssClass="boton" visible='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonR" runat="server" Text="R" OnClick ="Procesar_R"  CssClass="boton" visible='<%# (Convert.ToBoolean(Eval("FLG_EDITAR") )) %>'></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle Width="190px" />
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="File">
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/ANDAMIOS/"+Eval("FILE_ANDAMIOS") %>'  Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnUpdate" runat="server" CommandArgument='<%# Eval("IDE_ANDAMIOS") %>' OnClick ="Actualizar" ImageUrl="~/imagenes/PencilAdd.png" ToolTip="Actualizar"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
                        </div>
         </div>
    <div class="row">
        <div class="col-md-9">
        </div>
       
        <div class="col-md-3">
               <b>Leyenda botones de atención</b><br />
                    <b>A : </b> atender, <b>R : </b> rechazar
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12 ">
            <div class="table-responsive">
                <asp:GridView ID="gvExcel" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
                    DataKeyNames="IDE_ANDAMIOS,COD_TICKET,CECOS_GESTOR" 
                    EmptyDataText="There are no data records to display." 
                    Font-Size="8pt">
                    <Columns>

                        <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />


                        <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud" SortExpression="FECHA">


                            <ItemStyle Width="80px" />
                        </asp:BoundField>


                        <asp:BoundField DataField="TICKET" HeaderText="Ticket" SortExpression="TICKET">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ANDAMIOS" HeaderText="Código" SortExpression="ANDAMIOS" />
                        <asp:BoundField DataField="NOM_CREADO" HeaderText="Creado" SortExpression="NOM_CREADO" />
                        <asp:BoundField DataField="NOM_SOLICITA" HeaderText="Solicitante" SortExpression="NOM_SOLICITA" />
                        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" SortExpression="Especialidad" />
                        <asp:BoundField DataField="Area" HeaderText="Area" SortExpression="Area" />
                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" SortExpression="Solicitud" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                        <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Fecha Requerida" SortExpression="FECHA_REQUERIDA">

                            <ItemStyle BackColor="#FFFF66" />
                        </asp:BoundField>

                        <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios(Solicitante)" SortExpression="COMENTARIOS"></asp:BoundField>
                         <asp:BoundField DataField="PRIORIDAD" HeaderText="Prioridad" SortExpression="PRIORIDAD" />

                        <asp:BoundField DataField="F_ESTADO" HeaderText="Estado" SortExpression="F_ESTADO">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                        </asp:BoundField>


                        <asp:BoundField DataField="SUPERVIDOR" HeaderText="Nombre.Supervisor" SortExpression="SUPERVIDOR"></asp:BoundField>

                        <asp:BoundField DataField="CAPATAZ" HeaderText="Nombre.Capataz" SortExpression="CAPATAZ" />
                        <asp:BoundField DataField="DURACION" HeaderText="Duracion (dias)" SortExpression="DURACION" />
                        <asp:BoundField DataField="HORAS" HeaderText="Horas" SortExpression="HORAS" />
                        <asp:BoundField DataField="FECHA_ENTREGA" HeaderText="Fecha inicio" SortExpression="FECHA_ENTREGA" />

                        <asp:BoundField DataField="FECHA_TERMINO" HeaderText="Fecha termino" SortExpression="FECHA_TERMINO"></asp:BoundField>
                        <asp:BoundField DataField="FECHA_DESMONTAJE" HeaderText="Fecha desmontaje" SortExpression="FECHA_DESMONTAJE" />

                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones(Atención)" SortExpression="OBSERVACIONES">
                            <ItemStyle Width="190px" />
                        </asp:BoundField>
                        


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

               
                <asp:Label ID="lblrpta" runat="server" Visible="false"></asp:Label>
                <label><asp:Label ID="lblMsg" runat="server" ></asp:Label></label>
                <asp:TextBox runat="server" ID="txtrechazo" Height="120px" MaxLength="500" TextMode="MultiLine" Visible="False"></asp:TextBox>
                <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="ddl">
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                    <asp:ListItem Value="3"></asp:ListItem>
                    <asp:ListItem Value="4"></asp:ListItem>
                    <asp:ListItem Value="5"></asp:ListItem>
                </asp:DropDownList>
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
                    <asp:Button ID="btnPrioridad" runat="server" CssClass="btn-danger active" Text="Guardar" OnClick="btnPrioridad_Click" />
                </center>
            </div>
        </div>
       
    </asp:Panel>
    
</asp:Content>


