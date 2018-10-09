<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesemepenioObjetivosEquipo.aspx.cs" Inherits="RRHH_DesemepenioObjetivosEquipo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #EEAA00;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }

            input[type="submit"]:hover {
                outline: thin dotted #333;
                outline: 5px auto -webkit-focus-ring-color;
                outline-offset: -2px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div class="row">
        <div class="col-md-4">
             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Desempenio180x90.fw.png" PostBackUrl="~/RRHH/Desemepenio.aspx" />
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <br />
            <center>
                  <asp:Button ID="btnVarios" runat="server" Text="OBJETIVOS TRANSVERSALES" OnClick="btnVarios_Click" />
            </center>
           
        </div>
       
    </div>
    
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>LISTA DE COLABORADORES</b>
                </div>
            </div>
        </div>
    </div>

 
    <div class="row">
        <div class="col-lg-12 ">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_DESEMPENIO" EmptyDataText="There are no data records to display." Font-Size="9pt">
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="N°" ReadOnly="True" SortExpression="Row" />
                        <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="Personal" SortExpression="NOMBRE_COMPLETO" />
                        <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO" />
                        <asp:BoundField DataField="FAMILIA" HeaderText="Familia" SortExpression="FAMILIA">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PESOS" HeaderText="% Peso" SortExpression="PESOS">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Objetivos">
                            <ItemTemplate>
                                <center>
                                     <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_DESEMPENIO") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="Actualizar" OnClick="Objetivos" />
                                </center>


                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>


            </div>
        </div>
    </div>
</asp:Content>

