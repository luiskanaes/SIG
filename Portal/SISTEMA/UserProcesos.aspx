<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="UserProcesos.aspx.cs" Inherits="SISTEMA_UserProcesos" %>
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
            function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
          for (i = 1; i < GridView2.rows.length; i++) {
              GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
          }
      }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>ASIGNACIÓN DE RESPONSABLE EQUIPOS DE TRABAJO </b>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <label>Empresa</label>
              <asp:DropDownList ID="ddlEmpresas" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" ></asp:DropDownList>
         </div>
        <div class="col-md-4">
                        <label>Gerencia</label>
            <asp:DropDownList ID="ddlGerencia" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerencia_SelectedIndexChanged" ></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label>Centro</label>
        <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" ></asp:DropDownList>
    
        
        </div>
         
    </div>
    <div class="row">
        <div class="col-md-9">
             <label>Proceso</label>
            <asp:DropDownList ID="ddlProceso" runat="server"  CssClass="ddl" ></asp:DropDownList>
        </div>
        <div class="col-md-3">
            <br />
            <asp:CheckBox ID="CheckTodos" runat="server" Text="Habilitar todos CC" />
        </div>
        
    </div>
    <div class="row">
        
        <div class="col-md-9">
             <label>Responsable</label>
            
            <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                <ContentTemplate>
                    
                        <asp:DropDownList runat="server" ID="ddlPersonal" ></asp:DropDownList>
                        <link rel="stylesheet" href="../Content/jquery-ui.css" />
                        <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                        <script type="text/javascript" src="../js/jquery-ui.js"></script>

                 
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
        </div>


    </div>
    <br />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>FILTROS DE BUSQUEDA </b>
            </div>
        </div>
    </div>
   <div class="row">
        
        <div class="col-md-2">
            <label>Estados</label>
            <asp:DropDownList ID="ddlEstados" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <br />
            <asp:ImageButton ID="btnImagen" runat="server" ImageUrl="~/imagenes/boton.buscar.gif" OnClick="btnImagen_Click" />
            
        </div>
       <div class="col-md-8">
           <br />
           <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/imagenes/boton.eliminar.gif" OnClick="btnEliminar_Click" />
        </div>
    </div>

    <div class="row">
         <div style="overflow: scroll; width: 100%;">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_RESPONSABLE" EmptyDataText="There are no data records to display." Font-Size="9pt" >
                                        <Columns>
                                           

                                         <%--   <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="PERSONAL" SortExpression="NOMBRE_COMPLETO" />--%>

                                           <%-- <asp:BoundField DataField="PROCESO" HeaderText="PROCESO" SortExpression="PROCESO" />--%>
                                            <asp:TemplateField HeaderText="ITEM" SortExpression="ITEM">
                                    <HeaderTemplate>
                                        <label>Item's</label><br />
                                        <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Text='<%# Eval("Row") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </asp:TemplateField>
                                           

                                            <asp:BoundField DataField="DNI_RESPONSABLE" HeaderText="DNI" SortExpression="DNI_RESPONSABLE" />
                                           

                                            <asp:TemplateField HeaderText="PERSONAL" SortExpression="PERSONAL">
                                                <HeaderTemplate>
                                                    <label>PERSONAL</label><br />
                                                    
                                                        <asp:TextBox ID="txtNOMBRE_F" runat="server" Width="300px"></asp:TextBox>
                                                      

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("NOMBRE_COMPLETO") %>'></asp:Label>
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="PROCESO" SortExpression="PROCESO">
                                                <HeaderTemplate>
                                                    <label>PROCESO</label><br />
                                                    
                                                        <asp:TextBox ID="txtPROCESO_F" runat="server" Width="300px"></asp:TextBox>
                                                      

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("PROCESO") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="IP_CENTRO" HeaderText="IP_CENTRO" SortExpression="IP_CENTRO" />
                                            <asp:BoundField DataField="GERENCIA" HeaderText="GERENCIA" SortExpression="GERENCIA" />
                                            <asp:BoundField DataField="CENTRO" HeaderText="CENTRO" SortExpression="CENTRO" />
                                            <asp:BoundField DataField="FLG_TODO" HeaderText="TOTAL" SortExpression="FLG_TODO" />
                                           

                                            <asp:BoundField DataField="FLG_ESTADO" HeaderText="ESTADO" SortExpression="FLG_ESTADO" />
                                           

                                            <asp:BoundField DataField="CORREO" HeaderText="EMAIL" SortExpression="CORREO" />
                                           

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
                            </div>
                        </div>
        </div>
</asp:Content>

