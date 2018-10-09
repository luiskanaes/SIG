<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="EquiposMayoresAlquilerMenorView.aspx.cs" Inherits="CAREMENOR_EquiposMayoresAlquilerMenorView" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
<script src="../js/jquery-ui.min.js" type="text/javascript"></script>
<script src="../js/gridviewScroll.min.js" type="text/javascript"></script>

     <style type="text/css">

         
        .GridviewScrollHeader TH, .GridviewScrollHeader TD 
{ 
  
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

    function PopupAmpliacion( ancho, alto) {
            gridviewScroll();
        var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (ancho / 2);
        posicion_y = (screen.height / 2) - (alto / 2);

        var win = window.open("AmpliacionBandeja.aspx", "Page Title", "width=" + ancho + ",height=" + alto + ",menubar=0,toolbar=0,directories=0,scrollbars=no,resizable=no,left=" + posicion_x + ",top=" + posicion_y + "");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
        
    <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-6">
                                 <b>ALQUILER DE EQUIPO MENORES A TERCEROS </b>
                            </div>
                            <div class="col-md-6">
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/imagenes/boton.Excel.jpg" OnClick="btnImprimir_Click" />
                                <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/imagenes/boton.actualizarRefresh.jpg" OnClick="btnActualizar_Click" />
                            <asp:ImageButton ID="btnAmpliacion" runat="server" OnClick="btnAmpliacion_Click" ImageUrl="~/imagenes/boton.ampliacion.jpg" />
                            </div>
                            
                        </div>
                       
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <label>Año</label>
                    <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Estados</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"></asp:DropDownList>
                </div>
                
                <div class="col-md-2">
                    <label>Centro de costo</label>
                    <asp:DropDownList ID="ddlcentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlcentro_SelectedIndexChanged1"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                 
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/LeyendaArriendo.png" />
                </div>
            </div>
    
         <%--<div style="overflow: scroll; width: 105%; height: 450px;" >--%>

   
        <div class="row">

            <div class="col-lg-12 ">



                <asp:Label ID="lblCantidad" runat="server"></asp:Label>


                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-condensed"
                        AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,D_SOLPED,Reqs_CtdReservada,D_FLG_APRUEBA_CGO,CANTIDAD_LEGAJO"
                        EmptyDataText="There are no data records to display." Font-Size="8pt"
                        OnRowCreated="GridView1_RowCreated"
                        Width="100%">

                        <Columns>


                            <asp:TemplateField HeaderText="Requerimientos" SortExpression="Requerimientos">
                                <HeaderTemplate>
                                    <asp:Label ID="Label42" runat="server" Text="Requerimientos" Width="100px"></asp:Label>
                                    <asp:TextBox ID="txtRequerimientosa_H" runat="server" Width="100px"  ></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label43" runat="server" Text='<%# Bind("Reqs_CodigoCompleto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="SUBFAMILIA" SortExpression="SUBFAMILIA">
                                <HeaderTemplate>
                                    <asp:Label ID="Label3" runat="server" Text="SubFamilia" Width="130px"></asp:Label>
                                    <asp:TextBox ID="txtSUBFAMILIA_H" runat="server" Width="130px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Subf_Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Marca" SortExpression="Marca">
                                <HeaderTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="Marca" Width="80px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Marc_Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Modelo" SortExpression="Modelo">
                                <HeaderTemplate>
                                    <asp:Label ID="Label7" runat="server" Text="Modelo" Width="80px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Mode_Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Capacidad" SortExpression="Capacidad">
                                <HeaderTemplate>
                                    <asp:Label ID="Label21" runat="server" Text="Capacidad" Width="80px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Text='<%# Bind("Equi_Capacidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="FAMILIA" SortExpression="FAMILIA">
                                <HeaderTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="Familia" Width="200px"></asp:Label>
                                    <asp:TextBox ID="txtFamilia_H" runat="server" Width="200px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Fami_Descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Observaciones" SortExpression="Observaciones">
                                <HeaderTemplate>
                                    <asp:Label ID="Label9" runat="server" Text="Observaciones" Width="200px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Reqd_Observaciones") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="descripcion_alquiler" SortExpression="descripcion_alquiler">
                                <HeaderTemplate>
                                    <asp:Label ID="Label23" runat="server" Text="Descripción Req. adicionales" Width="250px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label24" runat="server" Text='<%# Bind("descripcion_alquiler") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:BoundField DataField="Reqs_CtdReservada" HeaderText="Cantidad requerida" SortExpression="Reqs_CtdReservada">
                                <HeaderStyle ForeColor="White" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_DOCUMENTO_SOLICITA" HeaderText="Fecha Generada" SortExpression="D_DOCUMENTO_SOLICITA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Reqs_FechaDisponibilidad" HeaderText="Req.Creada" SortExpression="Reqs_FechaDisponibilidad">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Requ_FechaRequeridoPara" HeaderText="Req.Para" SortExpression="Requ_FechaRequeridoPara">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Requ_FechaRequeridoHasta" HeaderText="Req.Hasta" SortExpression="Requ_FechaRequeridoHasta">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Adjunto">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnadjunto" runat="server" CommandArgument='<%# Eval("RowNumber") %>' ImageUrl="~/imagenes/2bajar.gif" OnClick="Adjunto" Visible='<%# (Convert.ToBoolean(Eval("ADJUNTO") )) %>' CausesValidation="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OR" SortExpression="OR">
                                <HeaderTemplate>
                                    <asp:Label ID="Label72" runat="server" Text="Codigo OR" Width="100px"></asp:Label>
                                    <asp:TextBox ID="txtOR_H" runat="server" Width="100px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label73" runat="server" Text='<%# Bind("SOLPED_SIG") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="NOMBRE_SOLICITANTE" HeaderText="Solicitante OR" SortExpression="NOMBRE_SOLICITANTE">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FECHA_SOLPED" HeaderText="Fecha OR" SortExpression="FECHA_SOLPED">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TIPO_SOLICITUD" HeaderText="Tipo OR" SortExpression="TIPO_SOLICITUD">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Adjunto OR">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink21" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%#Eval("URL") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("GUIA_SOLPED") )) %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                            <asp:BoundField DataField="ATENCION_TIPO" HeaderText="Atención" SortExpression="ATENCION_TIPO">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DOCUMENTO_TIPO" HeaderText="Documento" SortExpression="DOCUMENTO_TIPO">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Cantidad adjuntos">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("CANTIDAD_LEGAJO") %>' OnClick="VerLegajos" CausesValidation="False"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="GPO_LEGAJO" SortExpression="GPO_LEGAJO">
                                <HeaderTemplate>
                                    <asp:Label ID="Label61" runat="server" Text="Grupo Legajo" Width="80px"></asp:Label>
                                    <asp:TextBox ID="txtGPO_H" runat="server" Width="80px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label62" runat="server" Text='<%# Bind("GPO_LEGAJO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Proveedor" SortExpression="Proveedor">
                                <HeaderTemplate>
                                    <asp:Label ID="Label11" runat="server" Text="Proveedor" Width="220px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("Proveedor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="DOC_MOVILIZACION" HeaderText="Movilización" SortExpression="DOC_MOVILIZACION">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_DOCUMENTO_FECHA" HeaderText="F.Pre-Legajo" SortExpression="D_DOCUMENTO_FECHA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>


                            <asp:BoundField DataField="D_FECHA_ENVIO_CGO" HeaderText="Fecha enviada" SortExpression="D_FECHA_ENVIO_CGO">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="CGO_ESTADO" SortExpression="CGO_ESTADO">
                                <HeaderTemplate>
                                    <asp:Label ID="Label51" runat="server" Text="Situación" Width="120px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgCGO" runat="server" ImageUrl='<%# (Eval("CGO_IMG") ) %>' />
                                    <asp:Label ID="Label52" runat="server" Text='<%# Bind("CGO_ESTADO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="USUSARIO_APRUEBA_CGO" SortExpression="USUSARIO_APRUEBA_CGO">
                                <HeaderTemplate>
                                    <asp:Label ID="Label53" runat="server" Text="Revisado por" Width="220px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label54" runat="server" Text='<%# Bind("D_USUSARIO_APRUEBA_CGO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="D_FECHA_APRUEBA_CGO" HeaderText="Fecha revisión" SortExpression="D_FECHA_APRUEBA_CGO">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>





                            <asp:TemplateField HeaderText="SOLPED" SortExpression="SOLPED">
                                <HeaderTemplate>
                                    <asp:Label ID="Label45" runat="server" Text="Solped" Width="90px"></asp:Label>
                                    <asp:TextBox ID="txtSOLPED_H" runat="server" Width="90px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label44" runat="server" Text='<%# Bind("D_SOLPED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos.Alq." SortExpression="D_SOLPED_ALQUILER">
                                <HeaderStyle ForeColor="White" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_SOLPED_MOVIMIENTO" HeaderText="Pos.Mov." SortExpression="D_SOLPED_MOVIMIENTO">
                                <HeaderStyle ForeColor="White" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_SOLPED_FECHA" HeaderText="F.Solped" SortExpression="D_SOLPED_FECHA">
                                <HeaderStyle ForeColor="White" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_SUBFAMILIA" HeaderText="Sub. Familia" SortExpression="DES_SUBFAMILIA" />
                            <asp:BoundField DataField="DES_MARCA" HeaderText="Marca" SortExpression="DES_MARCA" />
                            <asp:BoundField DataField="DES_MODELO" HeaderText="Modelo" SortExpression="DES_MODELO" />
                            <asp:BoundField DataField="N_Reqs_Capacidad" HeaderText="Capacidad" SortExpression="N_Reqs_Capacidad" />

                            <asp:BoundField DataField="D_FECHA_ENVIO_LOG" HeaderText="Fec.Sol. PDC" SortExpression="D_FECHA_ENVIO_LOG">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>




                            <asp:TemplateField HeaderText="PDC" SortExpression="PDC">
                                <HeaderTemplate>
                                    <asp:Label ID="Label46" runat="server" Text="PDC" Width="90px"></asp:Label>
                                    <asp:TextBox ID="txtPDC_H" runat="server" Width="90px"></asp:TextBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label47" runat="server" Text='<%# Bind("D_PDC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="D_PDC_FECHA" HeaderText="Fecha.PDC" SortExpression="D_PDC_FECHA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PDC_MONEDA" HeaderText="Moneda" SortExpression="PDC_MONEDA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_PDC_MONTO" HeaderText="Valor Alq." SortExpression="D_PDC_MONTO">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_PDC_MONTO_MOVIL" HeaderText="Valor Mov." SortExpression="D_PDC_MONTO_MOVIL">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_PDC_MONTO_TOTAL" HeaderText="Total" SortExpression="D_PDC_MONTO_TOTAL">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Adj PDC">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink44" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("D_PDC_FILE") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("DOCUMENTO_PDC") )) %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>


                           <%-- <asp:BoundField DataField="D_COSTO_HORA" HeaderText="CostoxHora" SortExpression="D_COSTO_HORA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_HRAS_MIN" HeaderText="Horas mínima" SortExpression="D_HRAS_MIN">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="A_GUIA_INGRESO" HeaderText="Guia Ingreso" SortExpression="A_GUIA_INGRESO" />
                            <asp:BoundField DataField="D_FECHA_ENVIO_OBRA" HeaderText="F.Ingreso Proyecto" SortExpression="D_FECHA_ENVIO_OBRA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_FECHA_SALE_OBRA" HeaderText="F.Término contrato" SortExpression="D_FECHA_SALE_OBRA">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_CODIGO_CARE" HeaderText="Código.Care" SortExpression="D_CODIGO_CARE">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_COMENTARIOS" HeaderText="Comentarios/Observaciones" SortExpression="D_COMENTARIOS">
                                <HeaderStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="A_SERIE" HeaderText="Serie" SortExpression="A_SERIE" />
<asp:BoundField DataField="A_PLACA" HeaderText="Placa" SortExpression="A_PLACA" />


<asp:BoundField DataField="A_GUIA_SALIDA" HeaderText="Guia Salida" SortExpression="A_GUIA_SALIDA" />
                            <asp:TemplateField HeaderText="Guia">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("D_GUIA_FILE") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("OBSERVACION_FILE") )) %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="File Observ.">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink4" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("D_OBSERVACION_FILE") %>' Target="_blank" Visible='<%# (Convert.ToBoolean(Eval("GUIA_FILE") )) %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="PROCESO" SortExpression="PROCESO">
                                <HeaderTemplate>
                                    <asp:Label ID="Label13" runat="server" Text="Estado" Width="120px"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("PROCESO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="F.SalidaProyecto">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSalida" runat="server" Text='<%# Eval("D_FECHA_SALIDA") %>' Width="100px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                        TargetControlID="txtSalida"
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

                        </Columns>
                        <HeaderStyle CssClass="GridviewScrollHeader" /> 
                                            <RowStyle CssClass="GridviewScrollItem" /> 
                                            <PagerStyle CssClass="GridviewScrollPager" /> 
                    </asp:GridView>

            </div>


    </div>
    
    <asp:GridView ID="gvExcel" runat="server" CssClass="mGridAzul" AutoGenerateColumns="False" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo,Reqs_CtdReservada" EmptyDataText="There are no data records to display." Font-Size="8pt"
                Width="100%" Visible="False">


                            <Columns>


                                <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Requerimientos" SortExpression="Reqs_CodigoCompleto">
                                </asp:BoundField>

                               <asp:BoundField DataField="Subf_Descripcion" HeaderText="SubFamilia" SortExpression="Subf_Descripcion">
                                </asp:BoundField>


                                <asp:BoundField DataField="Marc_Descripcion" HeaderText="Marca" SortExpression="Marc_Descripcion">
                                </asp:BoundField>

                                <asp:BoundField DataField="Mode_Descripcion" HeaderText="Modelo" SortExpression="Mode_Descripcion">
                                </asp:BoundField>

                                <asp:BoundField DataField="Equi_Capacidad" HeaderText="Capacidad" SortExpression="Equi_Capacidad">
                                </asp:BoundField>

                                <asp:BoundField DataField="descripcion_alquiler" HeaderText="descripcion alquiler" SortExpression="descripcion_alquiler">
                                </asp:BoundField>

                                <asp:BoundField DataField="Reqd_Observaciones" HeaderText="Observaciones" SortExpression="Reqd_Observaciones">
                                </asp:BoundField>

                                <asp:BoundField DataField="Fami_Descripcion" HeaderText="Familia" SortExpression="Fami_Descripcion">
                                </asp:BoundField>


     
                                <asp:BoundField DataField="Reqs_CtdReservada" HeaderText="Cantidad requerida" SortExpression="Reqs_CtdReservada">
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_DOCUMENTO_SOLICITA" HeaderText="Fecha Generada" SortExpression="D_DOCUMENTO_SOLICITA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Reqs_FechaDisponibilidad" HeaderText="Req.Creada" SortExpression="Reqs_FechaDisponibilidad">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Requ_FechaRequeridoPara" HeaderText="Req.Para" SortExpression="Requ_FechaRequeridoPara">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Requ_FechaRequeridoHasta" HeaderText="Req.Hasta" SortExpression="Requ_FechaRequeridoHasta">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NOMBRE_SOLICITANTE" HeaderText="Solicitante OR" SortExpression="NOMBRE_SOLICITANTE">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FECHA_SOLPED" HeaderText="Fecha OR" SortExpression="FECHA_SOLPED">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TIPO_SOLICITUD" HeaderText="Tipo OR" SortExpression="TIPO_SOLICITUD">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>


                                <asp:BoundField DataField="ATENCION_TIPO" HeaderText="Atención" SortExpression="ATENCION_TIPO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DOCUMENTO_TIPO" HeaderText="Documento" SortExpression="DOCUMENTO_TIPO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>


                                <asp:BoundField DataField="CANTIDAD_LEGAJO" HeaderText="Cantidad adjuntos" SortExpression="CANTIDAD_LEGAJO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                

                                <asp:BoundField DataField="GPO_LEGAJO" HeaderText="Grupo Legajo" SortExpression="GPO_LEGAJO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>


                                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                

                                <asp:BoundField DataField="DOC_MOVILIZACION" HeaderText="Movilización" SortExpression="DOC_MOVILIZACION">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_DOCUMENTO_FECHA" HeaderText="F.Pre-Legajo" SortExpression="D_DOCUMENTO_FECHA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>


                                <asp:BoundField DataField="D_FECHA_ENVIO_CGO" HeaderText="Fecha enviada" SortExpression="D_FECHA_ENVIO_CGO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CGO_ESTADO" HeaderText="Situación" SortExpression="CGO_ESTADO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                              <asp:BoundField DataField="D_USUSARIO_APRUEBA_CGO" HeaderText="Revisado por" SortExpression="D_USUSARIO_APRUEBA_CGO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                
                                <asp:BoundField DataField="D_FECHA_APRUEBA_CGO" HeaderText="Fecha revisión" SortExpression="D_FECHA_APRUEBA_CGO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                               
                                <asp:BoundField DataField="D_SOLPED" HeaderText="SOLPED" SortExpression="D_SOLPED">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>


                               


                                <asp:BoundField DataField="D_SOLPED_ALQUILER" HeaderText="Pos.Alq." SortExpression="D_SOLPED_ALQUILER">
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_SOLPED_MOVIMIENTO" HeaderText="Pos.Mov." SortExpression="D_SOLPED_MOVIMIENTO">
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_SOLPED_FECHA" HeaderText="F.Solped" SortExpression="D_SOLPED_FECHA">
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="D_FECHA_ENVIO_LOG" HeaderText="Fec.Sol. PDC" SortExpression="D_FECHA_ENVIO_LOG">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>


                                <asp:BoundField DataField="D_PDC" HeaderText="PDC" SortExpression="D_PDC">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                


                                <asp:BoundField DataField="D_PDC_FECHA" HeaderText="Fecha.PDC" SortExpression="D_PDC_FECHA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PDC_MONEDA" HeaderText="Moneda" SortExpression="PDC_MONEDA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_PDC_MONTO" HeaderText="Valor Alq." SortExpression="D_PDC_MONTO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_PDC_MONTO_MOVIL" HeaderText="Valor Mov." SortExpression="D_PDC_MONTO_MOVIL">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_PDC_MONTO_TOTAL" HeaderText="Total" SortExpression="D_PDC_MONTO_TOTAL">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <%--<asp:BoundField DataField="D_COSTO_HORA" HeaderText="CostoxHora" SortExpression="D_COSTO_HORA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_HRAS_MIN" HeaderText="Horas mínima" SortExpression="D_HRAS_MIN">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="A_GUIA_INGRESO" HeaderText="Guia Ingreso" SortExpression="A_GUIA_INGRESO" />
                                <asp:BoundField DataField="D_FECHA_ENVIO_OBRA" HeaderText="F.Ingreso Proyecto" SortExpression="D_FECHA_ENVIO_OBRA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_FECHA_SALE_OBRA" HeaderText="F.Término contrato" SortExpression="D_FECHA_SALE_OBRA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_CODIGO_CARE" HeaderText="Código.Care" SortExpression="D_CODIGO_CARE">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D_COMENTARIOS" HeaderText="Comentarios/Observaciones" SortExpression="D_COMENTARIOS">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                        <asp:BoundField DataField="A_SERIE" HeaderText="Serie" SortExpression="A_SERIE" />
                        <asp:BoundField DataField="A_PLACA" HeaderText="Placa" SortExpression="A_PLACA" />


                        <asp:BoundField DataField="A_GUIA_SALIDA" HeaderText="Guia Salida" SortExpression="A_GUIA_SALIDA" />

                                <asp:BoundField DataField="PROCESO" HeaderText="Estado" SortExpression="PROCESO">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                <asp:BoundField DataField="D_FECHA_SALIDA" HeaderText="F.SalidaProyecto" SortExpression="D_FECHA_SALIDA">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>

                                
                            </Columns>
                             
                        </asp:GridView>
        </asp:Content>