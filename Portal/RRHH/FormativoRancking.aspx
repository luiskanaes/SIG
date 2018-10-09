<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="FormativoRancking.aspx.cs" Inherits="RRHH_FormativoRancking" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div class="shadowBox">
                <div class="page-container">
                    <div class="container">
                        <div class="row">
                        <section class="col-md-3">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/mensaje_formativo-01.fw.png" />
                         <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged" Visible="False" ></asp:DropDownList>
                            
                        </section>
                            <section class="col-md-6">
                                <br />
                                <asp:Button ID="btnReporte" runat="server" Text="Descargar reporte" OnClick="btnReporte_Click" />
                                <asp:Button ID="btnmenu" runat="server" Text="Regresar" OnClick="btnmenu_Click" />
                            </section>
                            <section class="col-md-3">
                                <br />
                                
                            </section>
                             
                        </div>
                        <br /><br />
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False"  EmptyDataText="There are no data records to display." Font-Size="9pt" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="80" DataKeyNames="IDE_FICHA,IDE_FASE">
                                    <Columns>
                                      
                                        <asp:BoundField DataField="Row" HeaderText="N°" SortExpression="Row" />
                                        <asp:BoundField DataField="NOMBRES_COMPLETO" HeaderText="Personal" SortExpression="NOMBRES_COMPLETO" />
                                        <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" SortExpression="PROGRAMA" />
                                        <asp:BoundField DataField="PROYECTO" HeaderText="Proyecto" SortExpression="PROYECTO" />
                                        <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO" />
                                        <asp:BoundField DataField="UBICACION" HeaderText="Ubicación" SortExpression="UBICACION" />
                                        <asp:TemplateField HeaderText="AutoEval. (Mitad)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("PTO_SEGUIMIENTO_MITAD") %>' OnClick ="View_MitadSeguimiento" ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eval. Des. (Mitad)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("PTO_DESEMPENIO_MITAD") %>' OnClick ="View_MitadDesempenio"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AutoEval. (Final)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# Eval("PTO_SEGUIMIENTO") %>' OnClick ="View_FinalSeguimiento"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eval. Des. (Final)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# Eval("PTO_DESEMPENIO") %>' OnClick ="View_FinalDesempenio"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           
                                        <asp:TemplateField HeaderText="Ficha">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton5" runat="server" Text='Ver' OnClick ="View_FIcha"></asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                           
                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
   </div>
</asp:Content>


