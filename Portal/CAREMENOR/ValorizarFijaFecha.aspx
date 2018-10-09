<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="ValorizarFijaFecha.aspx.cs" Inherits="CAREMENOR_ValorizarFijaFecha" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>--%>
    <script src="../js/gridviewScroll.min.js" type="text/javascript"></script>

     <style type="text/css">
             	.GridviewScrollHeader TH, .GridviewScrollHeader TD 
{ 
    padding: 5px; 
    font-weight: bold; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #EFEFEF; 
    text-align: left; 
    vertical-align: bottom; 
} 
.GridviewScrollItem TD 
{ 
    padding: 5px; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
    height:45px;
} 
.GridviewScrollPager  
{ 
    border-top: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
} 
.GridviewScrollPager TD 
{ 
    padding-top: 3px; 
    font-size: 14px; 
    padding-left: 5px; 
    padding-right: 5px; 
} 
.GridviewScrollPager A 
{ 
    color: #666666; 
}
.GridviewScrollPager SPAN

{

    font-size: 16px;

    font-weight: bold;

}


         .Marco {
             overflow: auto;
         }

             .Marco .Grid {
                 width: 105%;
             }


         .Ancho {
             width: 40%;
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
       
         $(document).ready(function () {
        gridviewScroll();
        });

        function gridviewScroll() {
        $('#<%=GridView1.ClientID%>').gridviewScroll({
        width: 1240,
        height: 470,
        startHorizontal: 0,
        wheelstep: 10,
        barhovercolor: "#C0C0C0",
        barcolor: "#C0C0C0",
        IsInUpdatePanel: true,
        freezesize: 5
        });
        }
       
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
            title: 'Mensaje del Sistema ',
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
  

    function popup(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, idValor, Proyecto, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("TarifarEquipos.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo + "&idValor=" + idValor + "&Proyecto=" + Proyecto, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");


        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
        function popupVerResumen(ancho, alto) {

            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (ancho / 2);
            posicion_y = (screen.height / 2) - (alto / 2);

            var win = window.open("ResumenValorizacion.aspx", "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");

        }
        function popupVerValorizarReporte(ancho, alto) {

            var posicion_x;
            var posicion_y;
            posicion_x = (screen.width / 2) - (ancho / 2);
            posicion_y = (screen.height / 2) - (alto / 2);

            var win = window.open("ValorizarReporte.aspx", "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");

        }
        $(":text").keydown(function (event) {

            if (event.keyCode == '13') {

                event.preventDefault();

            }

        });
        document.onkeypress = KeyPressed;
        function KeyPressed(e)
        { return ((window.event) ? event.keyCode : e.keyCode) != 13; }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="HDRequ_Numero" runat="server" />
            <asp:HiddenField ID="HDReqd_CodLinea" runat="server" />
            <asp:HiddenField ID="HDReqs_Correlativo" runat="server" />
            <asp:HiddenField ID="HDCodigoPrecio" runat="server" />
             <asp:HiddenField ID="HDProyecto" runat="server" />
            <asp:HiddenField ID="HDUnidadTarifa" runat="server" />
            <asp:HiddenField ID="HdidValor" runat="server" />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                
                <div class="row">
                    <div class="col-md-6">
                        <b>REGISTRO DE TARIFA </b>
                    </div>
                    <div class="col-md-6" style="text-align :right">
                         <asp:ImageButton ID="btnValorizar" runat="server" ImageUrl="~/imagenes/botonValorizar.jpg" OnClick="btnValorizar_Click"  />
                        <asp:ImageButton ID="btnTarifas" runat="server" ImageUrl="~/imagenes/boton.Tarifas.jpg" OnClick="btnTarifas_Click" />
                        <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/imagenes/boton.imprimir.gif" OnClick="btnImprimir_Click" />
                        <asp:ImageButton ID="btnCuadro" runat="server" ImageUrl="~/imagenes/boton.Resumen.jpg" OnClick="btnCuadro_Click" />
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
      <div class="row">
           <div class="col-md-4">
               <label>CC.</label>
            <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged" ></asp:DropDownList>
               </div>
        <div class="col-md-4">
             <label>Situación</label>
            <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" CssClass="ddl" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                <asp:ListItem Value="3">TODOS</asp:ListItem>
                <asp:ListItem Value="0">PENDIENTE TARIFAR</asp:ListItem>
                <asp:ListItem Value="1">TARIFADOS</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        
        <div class="col-md-4">
         
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/imagenes/boton.Excel.jpg" OnClick="btnExportar_Click" />
            <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnGuardar_Click" />

            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnGuardar"
                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                    Mensaje
                </div>
                <div class="body">
                    ¿Deseas guardar registros?
                </div>
                <div class="footer" align="right">
                    <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
            </asp:Panel>

              
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
    </div>

    <div class="row">
        <div class="col-lg-12 ">
            <asp:Label ID="lblCantidad" runat="server"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-condensed"
                AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,ide_valor,FLG_VISIBLE,Proy_Codigo"
                EmptyDataText="There are no data records to display." Font-Size="8pt"
                OnRowCreated="GridView1_RowCreated"
                Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="#" SortExpression="RowNumber" />
                    <%--<asp:BoundField DataField="Requ_Numero" HeaderText="Requerimiento" SortExpression="Requ_Numero">
                        <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="Requerimientos" SortExpression="Requerimientos">
                        <HeaderTemplate>
                            <asp:Label ID="Label42" runat="server" Text="Requerimientos" Width="100px"></asp:Label>
                            <asp:TextBox ID="txtRequerimientosa_H" runat="server" Width="100px"  onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label43" runat="server" Text='<%# Bind("Reqs_CodigoCompleto") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                      <asp:BoundField DataField="DES_SUBFAMILIA" HeaderText="SubFamilia" SortExpression="DES_SUBFAMILIA" />
                    <asp:BoundField DataField="DES_MARCA" HeaderText="Marca" SortExpression="DES_MARCA" />
                    <asp:BoundField DataField="DES_MODELO" HeaderText="Modelo" SortExpression="DES_MODELO" />
                    
                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" />
                   <%-- <asp:BoundField DataField="D_PDC" HeaderText="PDC" SortExpression="D_PDC" />--%>
                    <asp:TemplateField HeaderText="PDC" SortExpression="PDC">
                        <HeaderTemplate>
                            <asp:Label ID="Label46" runat="server" Text="PDC" Width="90px"></asp:Label>
                            <asp:TextBox ID="txtPDC_H" runat="server" Width="90px" onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label47" runat="server" Text='<%# Bind("D_PDC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Adj PDC">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink44" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("D_PDC_FILE") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("DOCUMENTO_PDC") )) %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />

                    </asp:TemplateField>

                    <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos. Alq." SortExpression="D_SOLPED_ALQUILER">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="D_CODIGO_CARE" HeaderText="Codigo CARE" SortExpression="D_CODIGO_CARE" />
                    
                    <asp:BoundField DataField="D_FECHA_ENVIO_OBRA" HeaderText="Ingreso a Obra" SortExpression="D_FECHA_ENVIO_OBRA" />

                    <asp:TemplateField HeaderText="FEC.INICIO VAL.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInicio" runat="server"  Text='<%# Bind("V_FECHA_INICIO_VALOR", "{0:yyyy-MM-dd}") %>'  Width="140px" TextMode="Date"  ></asp:TextBox>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="FEC.FIN VAL.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFin" runat="server"  Text='<%# Bind("V_FECHA_FIN_VALOR", "{0:yyyy-MM-dd}") %>' Width="140px" TextMode="Date"  onkeypress="return event.keyCode!=13"></asp:TextBox>

                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtFin"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left"
                                ErrorTooltipEnabled="True" />--%>
                           

                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FEC.ULTIMA VAL.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUltimaVal" runat="server" Text='<%# Eval("ULTIMA_VALORIZACION") %>' Width="100px"  placeholder="dd/mm/yyyy"  onkeypress="return event.keyCode!=13" BackColor="#E4E4E4" Enabled="false"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                TargetControlID="txtUltimaVal"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left"
                                ErrorTooltipEnabled="True" />
                           
                        </ItemTemplate>

                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="PERIODO.(EVAL.)">
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-6" style="text-align:center">
                                     <asp:TextBox ID="txtDia_inicio" runat="server" Width="35px" Text='<%# Eval("DIA_INICIO_PERIODO") %>' onkeypress="return event.keyCode!=13" onkeydown="return jsDecimals(event);" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-md-6" style="text-align:center">
                                  <asp:TextBox ID="txtDia_fin" runat="server" Width="35px" Text='<%# Eval("DIA_FIN_PERIODO") %>' onkeypress="return event.keyCode!=13" onkeydown="return jsDecimals(event);" MaxLength="2" Enabled="False" ></asp:TextBox>
                                </div>
                                
                            </div>
                           

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="UND TARIFA">
                        <ItemTemplate>
                            <asp:Label ID="lblTarifa" runat="server" Text='<%# Eval("TIPO_TARIFA") %>' Visible="false"  onkeypress="return event.keyCode!=13"/>
                            <asp:DropDownList ID="ddlTarifa" runat="server" CssClass="ddlAjustado">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="MONEDA">
                        <ItemTemplate>
                            <asp:Label ID="lblMoneda" runat="server" Text='<%# Eval("IDE_MONEDA") %>' Visible="false" />
                           <asp:Label ID="lblMoneda_des" runat="server" Text='<%# Eval("DES_MONEDA") %>' Visible="true" />
                            
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TARIFA">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTarifaDia" runat="server" Width="90px" Text='<%# Eval("V_TARIFA_DIA") %>' onkeypress="return event.keyCode!=13" onkeydown="return jsDecimals(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT. TARIF." Visible="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/imagenes/pencil_add.ico" OnClick="seleccionar"   Visible='<%# (Convert.ToBoolean(Eval("FLG_VISIBLE") )) %>' />
                           
                            

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                  
                </Columns>
                <HeaderStyle CssClass="GridviewScrollHeader" />
                <RowStyle CssClass="GridviewScrollItem" />
                <PagerStyle CssClass="GridviewScrollPager" />
            </asp:GridView>
        </div>
    </div>

   
    <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered table-condensed"
                AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,ide_valor,FLG_VISIBLE,Proy_Codigo"
                EmptyDataText="There are no data records to display." Font-Size="8pt" >
                <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="#" SortExpression="RowNumber" />
                <asp:BoundField DataField="Requ_Numero" HeaderText="Requerimiento" SortExpression="Requ_Numero">
                <ItemStyle BackColor="#FAFEA7" />
                </asp:BoundField>
                <asp:BoundField DataField="Reqs_Correlativo" HeaderText="Correlativo" SortExpression="Reqs_Correlativo">
                <ItemStyle BackColor="#FAFEA7" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Reqd_CodLinea" HeaderText="Linea" SortExpression="Reqd_CodLinea">
                <ItemStyle BackColor="#FAFEA7" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DES_SUBFAMILIA" HeaderText="Descripción" SortExpression="DES_SUBFAMILIA">
                <ItemStyle BackColor="#FAFEA7" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" />
                <asp:BoundField DataField="D_PDC" HeaderText="PDC" SortExpression="D_PDC" />
                <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos. Alq." SortExpression="D_SOLPED_ALQUILER">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="D_CODIGO_CARE" HeaderText="Codigo CARE" SortExpression="D_CODIGO_CARE" />
                <asp:BoundField DataField="Marc_Descripcion" HeaderText="Marca" SortExpression="Marc_Descripcion" />
                <asp:BoundField DataField="Mode_Descripcion" HeaderText="Modelo" SortExpression="Mode_Descripcion" />
                <asp:BoundField DataField="D_FECHA_ENVIO_OBRA" HeaderText="Ingreso a Obra" SortExpression="D_FECHA_ENVIO_OBRA" />
                <asp:BoundField DataField="V_FECHA_INICIO_VAL" HeaderText="FEC.INICIO VAL." SortExpression="V_FECHA_INICIO_VAL" />
                <asp:BoundField DataField="V_FECHA_FIN_VAL" HeaderText="FEC.FIN VAL." SortExpression="V_FECHA_FIN_VAL" />
                <asp:BoundField DataField="ULTIMA_VALORIZACION" HeaderText="FEC.ULTIMA VAL." SortExpression="ULTIMA_VALORIZACION" />
      
                <asp:BoundField DataField="DIA_INICIO_PERIODO" HeaderText="PERIODO.(EVAL.INI)" SortExpression="DIA_INICIO_PERIODO" />
                <asp:BoundField DataField="DIA_FIN_PERIODO" HeaderText="PERIODO.(EVAL.FIN)" SortExpression="DIA_FIN_PERIODO" />
                    
                    
                     <asp:TemplateField HeaderText="UND TARIFA">
                        <ItemTemplate>
                            <asp:Label ID="lblTarifa" runat="server" Text='<%# Eval("DES_TIPO_TARIFA") %>' />
                            
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:BoundField DataField="DES_MONEDA" HeaderText="MONEDA" SortExpression="DES_MONEDA" />
                  <asp:BoundField DataField="V_TARIFA_DIA" HeaderText="TARIFA" SortExpression="V_TARIFA_DIA" />
                     
                </Columns>
                
            </asp:GridView>
</asp:Content>

