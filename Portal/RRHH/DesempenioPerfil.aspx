<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesempenioPerfil.aspx.cs" Inherits="RRHH_DesempenioPerfil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">

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
           width :89%;
            
        }

        /*Demo fix*/
        .custom-combobox a {
            height: 34px;
            margin-top: -6px;
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
        div.DialogueBackground div.Dialogue 
        {
            width:300px; 
            height:100px; 
            position:absolute; 
            left:50%; 
            top:50%; 
            margin-left:-150px; 
            margin-top:-50px; 
            border:solid 10px #aaa; 
            background-color:#fff; 
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
        <div class="col-md-4">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Desempenio180x90.fw.png" PostBackUrl="~/RRHH/DesempenioBandeja.aspx" />
        </div>
        
        <div class="col-md-8">
            <center>

            </center>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>CREACIÓN DE PERFILES </b>
            </div>
        </div>
    </div>

    <div class="row">
        
        
        <div class="col-md-4">
            <label>Año</label>
            <asp:DropDownList ID="ddlanio" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlanio_SelectedIndexChanged"></asp:DropDownList>
     
        </div>
        <div class="col-md-4">
            <label>Perfil</label>
             <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlPerfil_SelectedIndexChanged" ></asp:DropDownList>
        </div>
         <div class="col-md-4">
            

               <label>Personal</label>
                <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                    <ContentTemplate>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlPersonal" Width="96%"  ></asp:DropDownList>
                            <link rel="stylesheet" href="../Content/jquery-ui.css" />
                            <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                            <script type="text/javascript" src="../js/jquery-ui.js"></script>
                           
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
         </div>
    
       
    </div>
    <div class="row">
        <div class="col-md-4">
             <label>Empresa</label>
            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" ></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label>Gerencia</label>
            <asp:DropDownList ID="ddlGerencia" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerencia_SelectedIndexChanged" ></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label>Centro de costo</label>
            <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" ></asp:DropDownList>
          
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
             <label>Familia</label>
            <asp:DropDownList ID="ddlFamilia" runat="server" CssClass="ddl" AutoPostBack="True" ></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:Label ID="lblCodigo" runat="server" Visible="false" ></asp:Label>
        </div>
    </div>
    <br />
      <div class="row">
        <div class="col-md-12">
            <center>
<asp:Button runat="server" Text="Cancelar" ID="btnCancel" OnClick="btnCancel_Click"></asp:Button>
                  <asp:Button ID="btnAgregar" runat="server" Text="Guardar" OnClick="btnAgregar_Click" />
            </center>
        </div>

    </div>
    <br />
    <div class="row">
        <div class="col-lg-12 ">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_DESEMPENIO" EmptyDataText="There are no data records to display." Font-Size="9pt" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="N°" ReadOnly="True" SortExpression="Row" />
                        <asp:BoundField DataField="PERSONAL" HeaderText="Personal" SortExpression="PERSONAL" />
                        <asp:BoundField DataField="PERFIL" HeaderText="Perfil" SortExpression="PERFIL" />
                        <asp:BoundField DataField="FAMILIA" HeaderText="Familia" SortExpression="FAMILIA" />
                        <asp:BoundField DataField="CODIGO_GERENCIA" HeaderText="Gerencia" SortExpression="CODIGO_GERENCIA" />
                        <asp:BoundField DataField="CCENTRO" HeaderText="CC" ReadOnly="True" SortExpression="CCENTRO" />
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <center>
                                     <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("IDE_DESEMPENIO") %>' ImageUrl="~/imagenes/PencilAdd.png" ToolTip="Actualizar" OnClick="Actualizar" />
                                </center>
                               

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>


            </div>
        </div>
    </div>

  
</asp:Content>

