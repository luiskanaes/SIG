<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="CartaCobranzasAprobaciones.aspx.cs" Inherits="OPERACIONES_CartaCobranzasAprobaciones" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <style type="text/css">
               .panelCuadro {
  margin-bottom: 20px;
  padding :10px;
  background-color: #EBEBEB;
  border: 1px solid transparent;
  border-radius: 4px;
  -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
          box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
      border:solid 1px;
      font-size:13px;
}
                  a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 100%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
        .custom-combobox {
            position: relative;
            display: inline-block;
            width :100%;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
            width :100%;
            /* support: IE7 */
            *height: 1.9em;
            *top: 0.1em;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 0.3em;
           width :100%;
            
        }

        /*Demo fix*/
        .custom-combobox a {
            height: 34px;
            margin-top: -6px;
            visibility: hidden;
        }
            div.DialogueBackground 
        { 
            position:absolute; 
            width:100%; 
            height:100%; 
            top:0; 
            left:0; 
            background-color:#777; 
            opacity:0.5;
            filter: alpha(opacity=50); 
            text-align:center; 
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
    function popup2(IDE_CARTA, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("CartaCobranzasFile.aspx?IDE_CARTA=" + IDE_CARTA, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");

    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-9">
                        <b>CARTA DE COBRANZAS  </b>
                <asp:Label ID="lblcodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbldetalle" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblticket" runat="server" Visible="False"></asp:Label>
                    </div>
                    <div class="col-md-3">
                         <asp:Button ID="btnSolicitud" runat="server" Text="Regresar" OnClick="btnValidar_Click"  CssClass="btn-danger active" />
                    </div>
                    
                </div>
                
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                    <label>Fecha</label>
                </div>
                <div class="col-md-6">
                     <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-md-2">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                    <label>De</label>
                </div>
                <div class="col-md-6">
                     <asp:TextBox ID="lblMigerencia" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                     <label>Centro costo</label>
                </div>
                <div class="col-md-6">
                   
                    <asp:TextBox ID="lblMicentro" runat="server"></asp:TextBox>
                   
                </div>
                <div class="col-md-2">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                     <label>Responsable</label>
                </div>
                <div class="col-md-6">
                     <asp:TextBox ID="lblsolicitante" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                </div>
            </div>
        </div>

    <%--   CUERPO #2--%>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                     <label>Fecha</label>
                </div>
                <div class="col-md-6">
                     <asp:TextBox ID="txtFechaDestino" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                     <label>Para</label>
                </div>
                <div class="col-md-8">
                      <asp:DropDownList ID="ddlGerenciaDestino" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerenciaDestino_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                    <label>Centro costo</label>
                </div>
                <div class="col-md-8">
                     <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3"  style="text-align: right">
                     <label>Responsable</label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="lblpersonal" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
       
    </div>


 
    <br />
    <div class="row">
        <div class="col-md-12">

             <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." Font-Size="12pt" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" >
                                        <Columns>
                                           <%-- <asp:BoundField DataField="PRECIO" HeaderText="P.U.(PEN)" SortExpression="PRECIO">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" SortExpression="TOTAL">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Row" HeaderText="N°" SortExpression="Row">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DOCUMENTO" HeaderText="DESCRIPCION" SortExpression="DOCUMENTO" />
                                            <asp:BoundField DataField="DETALLE" HeaderText="DETALLE" SortExpression="DETALLE" />
                                            <asp:BoundField DataField="CUENTA_COSTO_ORIGEN" HeaderText="CTA COSTO (Origen)" SortExpression="CUENTA_COSTO_ORIGEN" />
                                            <asp:BoundField DataField="CUENTA_COSTO" HeaderText="CTA COSTO (Destino)" SortExpression="CUENTA_COSTO">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CANTIDAD" HeaderText="CANT" SortExpression="CANTIDAD">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="P.U.(PEN)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrecio" runat="server" Text='<%# String.Format("{0:n}", Eval("PRECIO")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTOTAL" runat="server" Text='<%# String.Format("{0:n}", Eval("TOTAL")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>



        </div>
    </div>
    
    <div class="row">
        <div class="col-md-9" >
            <asp:Label ID="lblMonto" runat="server" Font-Bold="True" Font-Size="11pt" ></asp:Label>
        </div>
        <div class="col-md-3" style="text-align: right" >
              <asp:Button ID="btnAdjunto" runat="server" Text="Ver archivos" OnClick="btnAdjunto_Click" />
            </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <label>Notas:</label>
            <asp:TextBox ID="txtNota" runat="server" Height="100px" TextMode="MultiLine" Enabled="False" BackColor="#EBEBEB"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label>Comentarios:</label>
            <asp:TextBox ID="txtComentarios" runat="server" Height="100px" TextMode="MultiLine" ></asp:TextBox>
        </div>
    </div>
    <br />
    <label>Aprobaciones</label>
    <asp:Panel ID="Panel1" runat="server"  CssClass="panelCuadro">

        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-6">
                        <center>
                         <b>Ingeniero de Costos</b><br />
                         <asp:Label ID="lblOCosto" runat="server" ></asp:Label><br />
                            <asp:Label runat="server" ID="lbl1" ForeColor="#FF3300"></asp:Label>
                            <br />
                        </center>
                    </div>
                    <div class="col-md-6">
                        <center>
                         <b>Gerente</b><br />
                             <asp:Label ID="lblOGerencia" runat="server" ></asp:Label><br />
                             <asp:Label runat="server" ID="lbl2" ForeColor="#FF3300"></asp:Label>
                             <br />
                             </center>
                    </div>

                </div>
            </div>



            <%--   CUERPO #2--%>
            <div class="col-md-6">

                <div class="row">
                    <div class="col-md-6">
                        <center>
                        <b>Ingeniero de Costos</b><br />
                                 <asp:Label ID="lblCostoDestino" runat="server" ></asp:Label><br />
                                <asp:Label runat="server" ID="lbl3" ForeColor="#FF3300"></asp:Label>
                                <br />
                            </center>

                    </div>
                    <div class="col-md-6">
                        <center>
                                  <b>Gerente</b><br />
                         <asp:Label ID="lblGerenciaDestino2" runat="server" ></asp:Label><br />
                                 <asp:Label runat="server" ID="lbl4" ForeColor="#FF3300"></asp:Label>
                                 <br />
                            </center>
                    </div>
                </div>

            </div>

        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-md-12">
            <center>
<asp:Button runat="server" Text="Rechazar" ID="btnAnular" CssClass="btn-danger active" OnClick="btnAnular_Click" CausesValidation="False"></asp:Button>
<asp:Button runat="server" Text="Aprobar" ID="btnGuardar" OnClick="btnGuardar_Click" CausesValidation="False"></asp:Button>
            </center>
        </div>
        <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnGuardar"
                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                    Mensaje
                </div>
                <div class="body">
                    ¿Deseas aprobar carta cobraza?
                </div>
                <div class="footer" align="right">
                    <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
            </asp:Panel>
    </div>
    
</asp:Content>

