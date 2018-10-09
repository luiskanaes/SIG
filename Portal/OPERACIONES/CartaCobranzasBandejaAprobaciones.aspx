<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="CartaCobranzasBandejaAprobaciones.aspx.cs" Inherits="OPERACIONES_CartaCobranzasBandejaAprobaciones" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
          <style type="text/css">
   
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-6">
                        <b>BANDEJA APROBACIONES (CARTA DE COBRANZAS)  </b>
                <asp:Label ID="lblcodigo" runat="server"></asp:Label>
                <asp:Label ID="lbldetalle" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <center>
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
                        <asp:Button ID="btnBandeja" runat="server" Text="Mi bandeja" OnClick="btnBandeja_Click"  />
                        <asp:Button ID="btnSolicitud" runat="server" Text="Mis aprobaciones" OnClick="btnValidar_Click" />
                        </center>
                        
                    </div>
                    
                </div>
                
            </div>
        </div>
    </div>
     <div class="row">
        <div class="col-md-2">
            <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl"  AutoPostBack="True" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-2">
           <label>Atención</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label>Leyenda</label>
           <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/EstadoColores.jpg" />
        </div>
        <div class="col-md-3">
            
        </div>
    </div>
         <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_CARTA,TICKET,IDE_APROBACION" EmptyDataText="There are no data records to display." Font-Size="8pt" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ticket" SortExpression="Ticket">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label42" runat="server" Text="Ticket" Width="100px"></asp:Label>
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label43" runat="server" Text='<%# Bind("Ticket") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SOLICITANTE" HeaderText="Solicitante" SortExpression="SOLICITANTE" />
                                            <asp:BoundField DataField="CREADO" HeaderText="Creado por" SortExpression="CREADO" />
                                            <asp:BoundField DataField="C_FECHA" HeaderText="Fecha" SortExpression="C_FECHA" >
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                          <%--  <asp:BoundField DataField="D_CENTRO" HeaderText="Centro" SortExpression="D_CENTRO" />--%>
                                            <asp:BoundField DataField="RESPONSABLE" HeaderText="Responsable" SortExpression="RESPONSABLE" />
                                            <asp:BoundField DataField="ING_OPE" HeaderText="Ing.Costo" SortExpression="ING_OPE" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GERENTE_OPE" HeaderText="Gerente" SortExpression="GERENTE_OPE" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ING_DEST" HeaderText="Ing.Costo" SortExpression="ING_DEST" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GERENTE_DEST" HeaderText="Gerente" SortExpression="GERENTE_DEST" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="ESTADO_FIRMA" HeaderText="Atención" SortExpression="ESTADO_FIRMA" />
                                            <asp:BoundField DataField="ETAPAS" HeaderText="Estado carta" SortExpression="ETAPAS" >

                                            <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="SAP" HeaderText="Codigo SAP" SortExpression="SAP" />

                                            <asp:TemplateField HeaderText="Atender">
                                                <ItemTemplate>
                                                    <center>

                                                    
                                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%# Eval("IDE_CARTA") %>' ImageUrl="~/imagenes/PencilAdd20.png" OnClick ="seleccionar"  Visible='<%# (Convert.ToBoolean(Eval("CONTROL") )) %>'/>
                                                  </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
                        </div>
</asp:Content>

