<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="ValorizarEquipoMenor.aspx.cs" Inherits="CAREMENOR_ValorizarEquipoMenor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
<script src="../js/gridviewScroll.min.js" type="text/javascript"></script>

     <style type="text/css">

          .panelCuadro {
  margin-bottom: 20px;
  padding :10px;
  background-color: #f5f5f5;
  border: 1px solid;
  border-radius: 4px;
  -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
          box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
}
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
             width: 35%;
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
        width: 1250,
        height: 470,
        startHorizontal: 0,
        wheelstep: 10,
        barhovercolor: "#C0C0C0",
        barcolor: "#C0C0C0",
        IsInUpdatePanel: true,
        freezesize: 5
        });
        }
        $(":text").keydown(function (event) {

            if (event.keyCode == '13') {

                event.preventDefault();

            }

        });
        document.onkeypress = KeyPressed;
        function KeyPressed(e)
        { return ((window.event) ? event.keyCode : e.keyCode) != 13; }

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
    function popup(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, FLG_MENOR, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("EquipoMayoresAtencion.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo + "&FLG_MENOR=" + FLG_MENOR, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");


        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
    function popup2(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("EquipoMayoresAtencion2.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
    function popupLegajo(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("Legajo_Adjunto.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
    function popupPDC(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, ancho, alto) {
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("PDC_Adjunto.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 100);
    }
        function popupVerLegajos(Requ_Numero, Reqd_CodLinea, Reqs_Correlativo, ancho, alto) {
            gridviewScroll();
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("VerLegajos.aspx?Requ_Numero=" + Requ_Numero + "&Reqd_CodLinea=" + Reqd_CodLinea + "&Reqs_Correlativo=" + Reqs_Correlativo, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
        var win_timer = setInterval(function () {
            if (win.closed) {
                var boton = document.getElementById('<%=btnBuscar.ClientID%>');
                boton.click();
               
                clearInterval(win_timer);
            }
     }, 50);
    }

   
       
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
          for (i = 1; i < GridView2.rows.length; i++) {
              GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
          }
        }
         function popupVerResumen( ancho, alto) {
           
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("ResumenValorizacion.aspx" , "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
       
         }
         function popupVerValorizarReporte(ancho, alto) {

             var posicion_x;
             var posicion_y;
             posicion_x = (screen.width / 2) - (ancho / 2);
             posicion_y = (screen.height / 2) - (alto / 2);

             var win = window.open("ValorizarReporte.aspx", "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");

         }

         function popupMarca(Reqs_ItemSecuencia, ancho, alto) {
             var posicion_x;
             var posicion_y;
             posicion_x = (screen.width / 2) - (ancho / 2);
             posicion_y = (screen.height / 2) - (alto / 2);

             var win = window.open("UpdateEquipo.aspx?Reqs_ItemSecuencia=" + Reqs_ItemSecuencia, "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
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
                    <div class="col-md-6">
                        <b>VALORIZACIÓN EQUIPOS MENORES</b>
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
        <div class="col-md-2">
             <label>Año</label>
            <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged">
                            </asp:DropDownList>
        </div>
        <div class="col-md-2">
               <label>Mes</label>
            <asp:DropDownList ID="ddlMes" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                            </asp:DropDownList>
        </div>

        <div class="col-md-2">
            <label>CC.</label>
            <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged1"></asp:DropDownList>

        </div>
        <div class="col-md-2">
            <label>Proveedores</label>
            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>

        </div>


        
        <div class="col-md-4">
       <br />
     <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
             <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnGuardar_Click" />
            <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/imagenes/botonCerrar.jpg" OnClick="btnCerrar_Click" />
            

            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnCerrar"></cc1:ConfirmButtonExtender>
<cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnCerrar"
OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
<div class="header">
Mensaje
</div>
<div class="body">
¿Deseas procesar el cierre de valorización?
</div>
<div class="footer" align="right">
<asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
<asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
</div>
</asp:Panel>
        </div>
       
    </div>
    <div class="row">
        <div class="col-md-8">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/EstadoValorizacion.png" />
        </div>
        <div class="col-md-4">
                   
            </div>
    </div>




    <div class="row">

        <div class="col-lg-12 ">
            <asp:ImageButton ID="btnExportar" runat="server" ImageUrl="~/imagenes/boton.Excel.jpg" OnClick="btnExportar_Click" Visible="False" />
            
            <asp:Label ID="lblCantidad" runat="server"></asp:Label>

            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-condensed"
                AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,ide_valor,Proy_Codigo,D_Prov_RUC,FLG_FASES,Reqs_ItemSecuencia"
                EmptyDataText="There are no data records to display." Font-Size="8pt"
                OnRowCreated="GridView1_RowCreated"
                Width="100%" >
                <Columns>
              
                    <asp:TemplateField HeaderText="ITEM" SortExpression="ITEM">
                        <HeaderTemplate>
                           <asp:Label ID="Label01" runat="server" Text="Item" Width="40px"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("IMG_FASES") %>' />
                            <asp:Label ID="lblNro" runat="server" Text='<%# Eval("RowNumber") %>' />
                        </ItemTemplate>
                       
                    </asp:TemplateField>

                   
                   <%-- <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Requerimiento" SortExpression="Reqs_CodigoCompleto" />--%>
                    
                    <asp:TemplateField HeaderText="Requerimientos" SortExpression="Requerimientos">
                        <HeaderTemplate>
                            <asp:Label ID="Label42" runat="server" Text="Requerimientos" Width="100px"></asp:Label>
                            <asp:TextBox ID="txtRequerimientosa_H" runat="server" Width="100px"   onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label43" runat="server" Text='<%# Bind("Reqs_CodigoCompleto") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>
                    <asp:BoundField DataField="D_CODIGO_CARE" HeaderText="Codigo Care" SortExpression="D_CODIGO_CARE" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>
                    
                  
                    
                    <asp:TemplateField HeaderText="PDC" SortExpression="PDC">
                        <HeaderTemplate>
                            <asp:Label ID="Label46" runat="server" Text="PDC" Width="90px"></asp:Label>
                            <asp:TextBox ID="txtPDC_H" runat="server" Width="90px" onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label47" runat="server" Text='<%# Bind("D_PDC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos. Alq." SortExpression="D_SOLPED_ALQUILER">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    <%--<asp:TemplateField HeaderText="Adjunto OR">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink21" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%#Eval("URL") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("GUIA_SOLPED") )) %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Descripción" SortExpression="Descripción">
                        <HeaderTemplate>
                            <asp:Label ID="Label44" runat="server" Text="Descripción" Width="250px"></asp:Label>
                            <asp:TextBox ID="txtSubf_Descripcion_H" runat="server" Width="250px" Visible="false" OnTextChanged="txtSubf_Descripcion_H_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label45" runat="server" Text='<%# Bind("DES_SUBFAMILIA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <%-- <asp:BoundField DataField="Subf_Descripcion" HeaderText="Descripción" SortExpression="Subf_Descripcion" />--%>
                    
                    <asp:TemplateField HeaderText="Marca">
                        <ItemTemplate>
                          
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("DES_MARCA") %>' Font-Bold="True" Font-Underline="True" OnClick="seleccionarMarca"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                   
                    <asp:BoundField DataField="DES_MODELO" HeaderText="Modelo" SortExpression="DES_MODELO" />
                    <asp:BoundField DataField="N_Reqs_Capacidad" HeaderText="Capacidad" SortExpression="N_Reqs_Capacidad" />
                    <asp:BoundField DataField="D_FECHA_ENVIO_OBRA" HeaderText="Ingreso a Obra" SortExpression="D_FECHA_ENVIO_OBRA" >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="D_FECHA_SALIDA" HeaderText="F.Salida proyecto" SortExpression="D_FECHA_SALIDA" >

                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="F.INICIO VAL.">
                        <ItemTemplate>
                            <asp:Label ID="lblV_FECHA_INICIO_VAL" runat="server" Text='<%# Eval("V_FECHA_INICIO_VAL") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="F.FIN VAL.">
                        <ItemTemplate>
                            <asp:Label ID="lblV_FECHA_FIN_VAL" runat="server" Text='<%# Eval("V_FECHA_FIN_VAL") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="F.INICIO PERIODO">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInicioVal" runat="server" Text='<%# Eval("FECHA_INICIO_VAL") %>' Width="100px" placeholder="dd/mm/yyyy" onkeypress="return event.keyCode!=13"></asp:TextBox>

                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtInicioVal"
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
                    <asp:TemplateField HeaderText="F.FIN PERIODO">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFinVal" runat="server" Text='<%# Eval("FECHA_FIN_VAL") %>' Width="100px"  placeholder="dd/mm/yyyy" onkeypress="return event.keyCode!=13"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtFinVal"
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

                      <asp:TemplateField HeaderText="UND TARIFA">
                        <ItemTemplate>

                            
                            <asp:Label ID="lbl_ideMoneda" runat="server" Text='<%# Eval("D_PDC_MONEDA") %>' Visible="false"  />
                             <asp:Label ID="lblTarifa_unidad" runat="server" Text='<%# Eval("TIPO_TARIFA") %>' Visible="false"  />
                            <asp:Label ID="lblTarifa" runat="server" Text='<%# Eval("DES_TIPO_TARIFA") %>'  />
                        
                        </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    
                 

                    <asp:TemplateField HeaderText="TARIFA (T)">
                        <ItemTemplate>
                            <asp:Label ID="lblTARIFA_DIA" runat="server" Text='<%# Eval("V_TARIFA_DIA") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" BackColor="#FFFFA8"  />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="DIAS TRABAJO">
                        <ItemTemplate>
                             <asp:Label ID="lblDIAS_TRABAJO" runat="server" Text='<%# Eval("DIA_TRABAJO") %>' ></asp:Label>
                          
                         
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DIAS DSTO.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescuentoDia" runat="server" Width="80px" Text='<%# Eval("DIA_DSCTO") %>' onkeydown="return jsDecimals(event);" onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TOTAL" HeaderText="MONTO PARC." SortExpression="TOTAL" >

                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="MONTO DSCTO.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescuentoFinal" runat="server" Width="80px" Text='<%# Eval("TOTAL_DESCUENTO") %>' onkeydown="return jsDecimals(event);" onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TOTAL_VAL" HeaderText="TOTAL VALORIZACION" SortExpression="TOTAL_VAL">
                         <ItemStyle HorizontalAlign="Center" BackColor="#e5e5e5"  />
                   
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="HES">
                        <ItemTemplate>
                            <asp:TextBox ID="txtHES" runat="server" Width="150px" Text='<%# Eval("HES") %>' MaxLength="10" onkeypress="return event.keyCode!=13"></asp:TextBox>
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtHES"
                    ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{10,10}$" runat="server"
                    ErrorMessage="falta completar los 10 digitos requeridos" CssClass="errorMessage" >

                </asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="OBSERVACIONES">
                        <ItemTemplate>
                            <asp:TextBox ID="txtObservacion" runat="server" Width="250px" Text='<%# Eval("V_OBSERVACION") %>' MaxLength="450" onkeypress="return event.keyCode!=13"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                   

                </Columns>
                <HeaderStyle CssClass="GridviewScrollHeader" />
                <RowStyle CssClass="GridviewScrollItem" />
                <PagerStyle CssClass="GridviewScrollPager" />
            </asp:GridView>




            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There are no data records to display." Font-Size="8pt" >
                <Columns>

                    <asp:BoundField DataField="RowNumber" HeaderText="ITEM" SortExpression="RowNumber" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>
              
                    <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Requerimientos" SortExpression="Reqs_CodigoCompleto" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>
                    <asp:BoundField DataField="D_CODIGO_CARE" HeaderText="Codigo Care" SortExpression="D_CODIGO_CARE" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="D_PDC" HeaderText="PDC" SortExpression="D_PDC" >
                    <ItemStyle BackColor="#FAFEA7" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos. Alq." SortExpression="D_SOLPED_ALQUILER">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     
          
                    <asp:BoundField DataField="DES_SUBFAMILIA" HeaderText="Descripción" SortExpression="DES_SUBFAMILIA"></asp:BoundField>
                   
                    
                    <asp:BoundField DataField="DES_MARCA" HeaderText="Marca" SortExpression="DES_MARCA" />
                    <asp:BoundField DataField="DES_MODELO" HeaderText="Modelo" SortExpression="DES_MODELO" />
                    <asp:BoundField DataField="N_Reqs_Capacidad" HeaderText="Capacidad" SortExpression="N_Reqs_Capacidad" />
                    <asp:BoundField DataField="D_FECHA_ENVIO_OBRA" HeaderText="Ingreso a Obra" SortExpression="D_FECHA_ENVIO_OBRA" >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="D_FECHA_SALIDA" HeaderText="F.Salida proyecto" SortExpression="D_FECHA_SALIDA" >

                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>


                    <asp:BoundField DataField="V_FECHA_INICIO_VAL" HeaderText="F.INICIO VAL." SortExpression="V_FECHA_INICIO_VAL" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="V_FECHA_FIN_VAL" HeaderText="F.FIN VAL." SortExpression="V_FECHA_FIN_VAL" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FECHA_INICIO_VAL" HeaderText="F.INICIO PERIODO." SortExpression="FECHA_INICIO_VAL" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FECHA_FIN_VAL" HeaderText="F.FIN PERIODO." SortExpression="FECHA_FIN_VAL" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="DES_TIPO_TARIFA" HeaderText="UND TARIFA." SortExpression="DES_TIPO_TARIFA" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    

                    <asp:TemplateField HeaderText="TARIFA (T)">
                        <ItemTemplate>
                            <asp:Label ID="lblTARIFA_DIA" runat="server" Text='<%# Eval("V_TARIFA_DIA") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" BackColor="#FFFFA8"  />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="DIAS TRABAJO">
                        <ItemTemplate>
                             <asp:Label ID="lblDIAS_TRABAJO" runat="server" Text='<%# Eval("DIA_TRABAJO") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:BoundField DataField="DIA_DSCTO" HeaderText="DIAS DSTO." SortExpression="DIA_DSCTO" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="TOTAL" HeaderText="MONTO PARC." SortExpression="TOTAL" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>


                    <asp:BoundField DataField="TOTAL_DESCUENTO" HeaderText="MONTO DSCTO." SortExpression="TOTAL_DESCUENTO" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>



                    <asp:BoundField DataField="TOTAL_VAL" HeaderText="TOTAL VALORIZACION" SortExpression="TOTAL_VAL">
                         <ItemStyle HorizontalAlign="Center" BackColor="#e5e5e5"  />
                   
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>


                    <asp:BoundField DataField="HES" HeaderText="HES" SortExpression="HES" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    
                     <asp:BoundField DataField="V_OBSERVACION" HeaderText="OBSERVACIONES" SortExpression="V_OBSERVACION" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>


                   

                </Columns>
   
            </asp:GridView>

        </div>
    </div>


</asp:Content>

