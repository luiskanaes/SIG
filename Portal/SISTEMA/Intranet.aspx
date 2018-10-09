<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="Intranet.aspx.cs" Inherits="SISTEMA_Intranet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        
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
           width :90%;
            
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
                            function pageLoad() {
            //Auto populate dropdown list.
            (function ($) {
                $.widget("custom.combobox", {
                    _create: function () {
                        this.wrapper = $("<span>")
                          .addClass("custom-combobox")
                          .insertAfter(this.element);

                        this.element.hide();
                        select = this.element.hide();
                        this._createAutocomplete(select);
                        this._createShowAllButton(select);
                        console.log(this.element.attr("id") + " : " + select.is(':focus'));
                    },

                    _createAutocomplete: function (select) {
                        var selected = this.element.children(":selected"),
                            select = this.element.hide(),
                          value = selected.val() ? selected.text() : "";
                        var disabled = select.is(':disabled');

                        this.input = $("<input>")
                          .appendTo(this.wrapper)
                          .val(value)
                          .attr("title", "")
                          .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                          .attr('disabled', disabled)
                          .autocomplete({
                              delay: 0,
                              minLength: 0,
                              source: $.proxy(this, "_source")
                          })
                           .focus(function () {

                           })
                          .tooltip({
                              tooltipClass: "ui-state-highlight"
                          });

                        this._on(this.input, {
                            autocompleteselect: function (event, ui) {
                                ui.item.option.selected = true;
                                this._trigger("select", event, {
                                    item: ui.item.option
                                });
                              <%--  __doPostBack('<%=Upnl.ClientID%>', this.element.attr("id"));--%>
                            },
                            autocompletechange: "_removeIfInvalid"
                        });
                    },

                    _createShowAllButton: function (select) {
                        var input = this.input,
                          wasOpen = false;
                        var disabled = select.is(':disabled');
                        $("<a>")
                          .attr("tabIndex", -1)
                          .attr('disabled', disabled)
                          .appendTo(this.wrapper)
                          .button({
                              icons: {
                                  primary: "ui-icon-triangle-1-s"
                              },
                              text: false
                          })
                          .removeClass("ui-corner-all")
                          .addClass("custom-combobox-toggle ui-corner-right")
                          .mousedown(function () {
                              wasOpen = input.autocomplete("widget").is(":visible");
                          })
                          .click(function () {
                              if ($(this).attr('disabled')) {
                                  return false;
                              }
                              input.focus();

                              // Close if already visible
                              if (wasOpen) {
                                  return;
                              }

                              // Pass empty string as value to search for, displaying all results
                              input.autocomplete("search", "");
                          });
                    },

                    _source: function (request, response) {
                        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                        response(this.element.children("option").map(function () {
                            var text = $(this).text();
                            if (this.value && (!request.term || matcher.test(text)))
                                return {
                                    label: text,
                                    value: text,
                                    option: this
                                };
                        }));
                    },

                    _removeIfInvalid: function (event, ui) {

                        // Selected an item, nothing to do
                        if (ui.item) {
                            return;
                        }

                        // Search for a match (case-insensitive)
                        var value = this.input.val(),
                          valueLowerCase = value.toLowerCase(),
                          valid = false;
                        this.element.children("option").each(function () {
                            if ($(this).text().toLowerCase() === valueLowerCase) {
                                this.selected = valid = true;
                                return false;
                            }
                        });

                        // Found a match, nothing to do
                        if (valid) {
                            return;
                        }

                        // Remove invalid value
                        if (value != '') {
                            this.input
                              .val("")
                              .attr("title", value + " didn't match any item")
                              .tooltip("open");
                            this.element.val("");
                            this._delay(function () {
                                this.input.tooltip("close").attr("title", "");
                            }, 2500);
                            this.input.data("ui-autocomplete").term = "";
                        } else {
                            this.input.val("");
                            this.element.val("");
                            this.input.data("ui-autocomplete").term = "";
                        }
                     <%--   __doPostBack('<%=Upnl.ClientID%>', this.element.attr("id"));--%>
                    },

                    _destroy: function () {
                        this.wrapper.remove();
                        this.element.show();
                    }
                });
            })(jQuery);

            $(document).ready(function () {
               
                 $("#<%= ddlPersonal.ClientID %>").combobox();
            });
        }
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                <b>ADMINISTRACION INTRANET</b>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-9">
                    <label>Tipo contenido</label>
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>

                </div>
                <div class="col-md-3">
                    <label>Orden</label>
                    <asp:DropDownList ID="ddlOrden" runat="server" CssClass="ddl" AutoPostBack="True">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>

                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>

                    </asp:DropDownList>
                </div>
               
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Estado</label>
                    <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True">
                        <asp:ListItem Value="1">HABILITAR</asp:ListItem>
                        <asp:ListItem Value="0">BLOQUEAR</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>
                        <asp:Label ID="lblImg" runat="server"></asp:Label></label>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Imagen Zoom</label>
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                   <label> <asp:Label ID="lblUrl" runat="server" ></asp:Label></label>
                    <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Descripcion</label>
                    <asp:TextBox ID="txtdescripcion" runat="server" Height="80px" TextMode="MultiLine"></asp:TextBox>
                </div>

            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
                         <asp:Button ID="Button1" runat="server" Text="Cancelar" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Guardar" OnClick="Button2_Click" />
                    </center>
                   
                </div>
            </div>
        </div>
        <%--fin primer bloque--%>
        <div class="col-md-8">
            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                        AutoGenerateColumns="False" DataKeyNames="IDE_BANNER,IMG_URL,IMG_ZOOM" EmptyDataText="There are no data records to display." 
                                        Font-Size="8pt"  >
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                           

                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" SortExpression="DESCRIPCION" />
                                            <asp:BoundField DataField="DESCRIPCION_ADICIONAL" HeaderText="CATEGORIA" SortExpression="DESCRIPCION_ADICIONAL" />
                                            <asp:BoundField DataField="ORDEN" HeaderText="ORDEN" SortExpression="ORDEN" />
                                           
                                            <asp:BoundField DataField="FLG_ESTADO" HeaderText="ESTADO" SortExpression="FLG_ESTADO" />
                                           
                                            <asp:TemplateField HeaderText="EDITAR">
                                           <ItemTemplate>
                                            <center>
                                            <asp:ImageButton ID="btnVer" runat="server" CommandArgument='<%# Eval("IDE_BANNER") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="Actualizar" onclick="VerDatos" />
                                            </center>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ELIMINAR">
                                                <ItemTemplate>
                                                    <center>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_BANNER") %>' ImageUrl="~/imagenes/Error.png" ToolTip="Procesar solicitud" OnClick ="EliminarFile" />
                                                     </center>
                                                         <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEliminar">
                                                        </cc1:ConfirmButtonExtender>
                                                        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEliminar"
                                                            OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                                        </cc1:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                                Mensaje SSK
                                                            </div>
                                                            <div class="body">
                                                                ¿Deseas eliminar imagen?
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                                                <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                                            </div>
                                                        </asp:Panel>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="IMG">
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# Eval("FilePath") %>'  Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>NUEVO INGRESOS Y PROMOCIONES </b>
            </div>
        </div>
    </div>
    <div class="row">
        
        <div class="col-md-4">
            <div class="row">

                <div class="col-md-12">
                    <label>Personal</label>

                    <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                        <ContentTemplate>

                            <asp:DropDownList runat="server" ID="ddlPersonal"></asp:DropDownList>
                            <link rel="stylesheet" href="../Content/jquery-ui.css" />
                            <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                            <script type="text/javascript" src="../js/jquery-ui.js"></script>


                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="row">
                
                <div class="col-md-6">
                    <label>Anuncios</label>
                    <asp:DropDownList ID="ddlNuevo" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlNuevo_SelectedIndexChanged">
                        <asp:ListItem Value="1">NUEVOS INGRESOS</asp:ListItem>
                        <asp:ListItem Value="2">PROMOCIONES</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                     <label>Estado</label>
                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="ddl" >
                        <asp:ListItem Value="1">HABILITAR</asp:ListItem>
                        <asp:ListItem Value="0">ELIMINAR</asp:ListItem>
                     </asp:DropDownList>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Descripción</label>
                    <asp:TextBox ID="txtnuevo" runat="server" Height="80px" TextMode="MultiLine"></asp:TextBox>
                </div>

            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
<asp:Button runat="server" Text="Registrar" ID="btnagregar" OnClick="btnagregar_Click"></asp:Button>
                    </center>
                </div>

            </div>
        </div>
     <%--   fin cuerpo 1--%>
         <div class="col-md-8">
              <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
                                        AutoGenerateColumns="False" DataKeyNames="ID_DNI" EmptyDataText="There are no data records to display." 
                                        Font-Size="8pt"  >
                                        <Columns>
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                           

                                            <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="NOMBRE" SortExpression="NOMBRE_COMPLETO" />
                                            <asp:BoundField DataField="DESCRIPCION_NUEVO" HeaderText="DESCRIPCION" SortExpression="DESCRIPCION_NUEVO" />
                                            <asp:BoundField DataField="DATO" HeaderText="DATO" SortExpression="DATO" />
                                           
                                            <asp:TemplateField HeaderText="EDITAR">
                                           <ItemTemplate>
                                            <center>
                                            <asp:ImageButton ID="btnDatos" runat="server" CommandArgument='<%# Eval("ID_DNI") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="Actualizar" onclick="datosPersonal" />
                                            </center>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                           

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
         </div>
        </div>

</asp:Content>

