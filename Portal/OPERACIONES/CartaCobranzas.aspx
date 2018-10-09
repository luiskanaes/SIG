<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdminPopup.master" AutoEventWireup="true" CodeFile="CartaCobranzas.aspx.cs" Inherits="OPERACIONES_CartaCobranzas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <style type="text/css">
              .panelCuadro {
                  margin-bottom: 20px;
                  padding: 10px;
                  background-color: #EBEBEB;
                  border: 1px solid transparent;
                  border-radius: 4px;
                  -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
                  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
                  border: solid 1px;
                  font-size: 13px;
              }
              

              .btn-file {
                  position: relative;
                  overflow: hidden;
              }

                  .btn-file input[type=file] {
                      position: absolute;
                      top: 0;
                      right: 0;
                      min-width: 100%;
                      min-height: 100%;
                      text-align: right;
                      filter: alpha(opacity=0);
                      opacity: 0;
                      outline: none;
                      background: white;
                      cursor: inherit;
                      display: block;
                     
                  }
    </style>
        <script type="text/javascript">
            $(":text").keydown(function(event) {

                if (event.keyCode == '13') {

                    event.preventDefault();

                }

            });
            document.onkeypress = KeyPressed;
            function KeyPressed(e)
            { return ((window.event) ? event.keyCode : e.keyCode) != 13; }


            function isNumberKey(evt) {
               
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    

                    return false;
                }
                else {

                    return true;
                }
            }

            var counter = 0;
            function AddFileUpload() {
                var div = document.createElement('DIV');
                div.innerHTML = '<input id="file' + counter + '" name = "file' + counter + '" type="file" /><input id="Button' + counter + '" type="button" value="Borrar" onclick = "RemoveFileUpload(this)" />';
                document.getElementById("FileUploadContainer").appendChild(div);
                counter++;
            }
            function RemoveFileUpload(div) {
                document.getElementById("FileUploadContainer").removeChild(div.parentNode);
            }

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
                $("#<%= ddlOCosto.ClientID %>").combobox();
                $("#<%= ddlOGerencia.ClientID %>").combobox();
                $("#<%= ddlCostoDestino.ClientID %>").combobox();
                $("#<%= ddlGerenciaDestino2.ClientID %>").combobox();
                $("#<%= ddlPersonalOrigen.ClientID %>").combobox();
                $("#<%= ddlPEP.ClientID %>").combobox();
                $("#<%= ddlPEP_Origen.ClientID %>").combobox();
                
            });
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-2"  style="text-align: right">
              <label>Ticket</label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtTicket" runat="server"></asp:TextBox>
                    <%#Eval("Row") %>
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-3">
        </div>
    </div>
    <br />
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-6">
                        <b>CARTA DE COBRANZAS  </b>
                <asp:Label ID="lblcodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbldetalle" runat="server" Visible="False"></asp:Label>
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
           <%--CUERPO #1--%>
        <div class="col-md-6">
            

            <div class="row" style="text-align: right">
                <div class="col-md-4">
                      <label>Fecha</label>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Enabled="False" ></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                    TargetControlID="txtFecha"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />

                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                <span class="input-group-addon">
                    <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                </span>
            </div>
                </div>
                <div class="col-md-2">
                  
                </div>
            </div>

            <div class="row">
                <div class="col-md-4" style="text-align: right">
                      <label>De</label>
                </div>
                <div class="col-md-6">
                      <asp:DropDownList ID="ddlGerenciaOrigen" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerenciaOrigen_SelectedIndexChanged" ></asp:DropDownList>
                </div>
                <div class="col-md-2">
                   
                </div>
            </div>
            <div class="row">
                <div class="col-md-4" style="text-align: right">
                    <label>Personal</label>
                </div>
                <div class="col-md-6">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" OnLoad="Upnl_LoadOrigen">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlPersonalOrigen"></asp:DropDownList>
                            <link rel="stylesheet" href="../Content/jquery-ui.css" />
                            <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                            <script type="text/javascript" src="../js/jquery-ui.js"></script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-2">
                   
                </div>
            </div>
            <div class="row">
                <div class="col-md-4" style="text-align: right">
                    <asp:Label ID="lblCC" runat="server" Text="Centro de costo" Font-Bold="True"></asp:Label> 
                </div>
                <div class="col-md-6">
                 <asp:DropDownList ID="ddlCentroOrigen" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCentroOrigen_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                   
                </div>
            </div>
            
        </div>


        <%--CUERPO #2--%>
        <div class="col-md-6">
            
            <div class="row">
                <div class="col-md-3">
                     <label>Fecha</label>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtFechaDestino" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtFechaDestino"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFechaDestino" PopupButtonID="ImageButton1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                        </span>
                    </div>
                </div>
                <div class="col-md-3">
                    <label></label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label>Para</label>
                </div>
                <div class="col-md-6">
                     <asp:DropDownList ID="ddlGerenciaDestino" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerenciaDestino_SelectedIndexChanged" ></asp:DropDownList>
                </div>
                <div class="col-md-3">
                  
                </div>
            </div>
                        <div class="row">
                <div class="col-md-3">
                     <label>Responsable</label>
                </div>
                <div class="col-md-6">
                    <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlPersonal"></asp:DropDownList>

                            <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                            <script type="text/javascript" src="../js/jquery-ui.js"></script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-3">
                    
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCC1" runat="server" Text="Centro de costo" Font-Bold="True" Visible="False"></asp:Label>
                    
                </div>
                <div class="col-md-6">
                     <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged" Visible="False" ></asp:DropDownList>
                </div>
                <div class="col-md-3">
                   
                </div>
            </div>

        </div>
    </div>




    <div class="row">

        <div class="col-md-1" style="text-align: right">
            
        </div>
        <div class="col-md-4">
          
            
        </div>
        <div class="col-md-2" style="text-align: right">
            
        </div>
        <div class="col-md-4">
           
            
        </div>
        <div class="col-md-1" style="text-align: right">
        </div>

    </div>


    <br />
    <div class="row">
        <div class="col-md-12">
            <table id="tbl_rol" class="style1">
                <thead>
                    <tr align="center" style="text-align: center">
                        <th width="25%" colspan ="2">
                            <asp:TextBox ID="txtdocumento" runat="server" placeholder="Descripcion" onkeypress="return event.keyCode!=13"></asp:TextBox></th>
                        <th width="30%" style="text-align: center">
                            <asp:TextBox ID="txtdetalle" runat="server" placeholder="Detalle" onkeypress="return event.keyCode!=13"></asp:TextBox></th>
                        <th width="10%" style="text-align: center;" onkeypress="return event.keyCode!=13">

                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" OnLoad="Upnl_LoadPEP2">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="ddlPEP_Origen"></asp:DropDownList>

                                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </th>
                        
                        <th width="10%" style="text-align: center;" onkeypress="return event.keyCode!=13">

                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnLoad="Upnl_LoadPEP">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="ddlPEP"></asp:DropDownList>

                                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </th>

                        <th width="5%" style="text-align: center" onkeypress="return event.keyCode!=13">
                            <asp:TextBox ID="txtCantidad" runat="server" placeholder="Cant." onkeydown="return isNumberKey(event)"></asp:TextBox></th>
                        <th width="10%" style="text-align: center" onkeypress="return event.keyCode!=13">
                            <asp:TextBox ID="txtPrecio" runat="server" placeholder="P.U.(PEN)" onkeydown="return jsDecimals(event);"></asp:TextBox></th>
                        <th width="10%"></th>
                        <th width="20%">
                            <asp:ImageButton ID="btnAgregarAll" runat="server" ImageUrl="~/imagenes/boton.agregar.gif"  ToolTip="Guardar" OnClick="btnAgregarAll_Click" />
                        </th>
                    </tr>
                    <tr align="center" style="background-color: #195183; color: White; text-align: center; font-family: Arial, sans-serif; font-size: 13px">
                        <th width="5%" style="text-align: center">N°</th>
                        <th width="20%" style="text-align: center">Descripción</th>
                        <th width="30%" style="text-align: center">Detalle</th>
                        <th width="10%" style="text-align: center">Origen: Cta de costo</th>
                        <th width="10%" style="text-align: center">Destino: Cta de costo</th>
                        <th width="5%" style="text-align: center">Cantidad</th>
                        <th width="10%" style="text-align: center">P.U.(PEN)</th>
                        <th width="10%" style="text-align: center">Total</th>
                        <th width="20%" style="text-align: center"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:ListView runat="server" ID="lstRol" DataKeyNames="IDE_DETALLE,IDE_CARTA">
                        <ItemTemplate>
                            <tr style="font-family: Arial, sans-serif; font-size: 14px; border: double; border-width: 1px;">
                                <td style="text-align: center">
                                    <%#Eval("Row") %>
                                </td>
                                <td style="text-align: left">
                                    <%#Eval("DOCUMENTO") %>
                                </td>
                                <td style="text-align: left">
                                    <%#Eval("DETALLE") %>
                                </td>
                                <td style="text-align: center">
                                    <%#Eval("CUENTA_COSTO_ORIGEN") %>
                                </td>
                                <td style="text-align: center">
                                    <%#Eval("CUENTA_COSTO") %>
                                </td>
                                <td style="text-align: center">
                                    <%#Eval("CANTIDAD") %>
                                </td>
                                <td style="text-align: center">
                                    <%# String.Format("{0:n}", Eval("PRECIO")) %>
                                   <%-- <%#Eval("PRECIO") %>--%>
                                </td>
                                <td style="text-align: center">
                                  <%--  <%#Eval("TOTAL") %>--%>
                                    <%# String.Format("{0:n}", Eval("TOTAL")) %>
                                </td>
                                <td style="text-align: center">
                                   <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/imagenes/PencilAdd.png" OnClick="Editar" CommandArgument='<%# Eval("Row") %>' />
                                   <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/imagenes/Ico_delete.png" OnClick="Eliminar" CommandArgument='<%# Eval("Row") %>' />
                                </td>
                               
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
                <tr align="center" style=" text-align: center; font-family: Arial, sans-serif; font-size: 13px">
                        <th width="5%" style="text-align: center"></th>
                        <th width="20%" style="text-align: center"></th>
                        <th width="30%" style="text-align: center"></th>
                        <th width="10%" style="text-align: center"></th>
                        <th width="10%" style="text-align: center"></th>
                        <th width="5%" style="text-align: center"></th>
                        <th width="10%" style="text-align: center"></th>
                        <th width="10%" style="text-align: center; background-color:yellow"><b><asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></b></th>
                        <th width="20%" style="text-align: center"></th>
                    </tr>
            </table>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12" >
            <asp:Label ID="lblMonto" runat="server" Font-Bold="True" Font-Size="11pt" ></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">


        
        <div class="col-md-4">
            <label>Cargar archivos</label>
            <div class="input-group">
                                <label class="input-group-btn">
                                    <span class="btn btn-primary">
                                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True"/>
                                    </span>
                                </label>

                            </div>

            
    <br />
    <asp:Label ID="lblMessage" runat="server" Text="File uploaded successfully." ForeColor="Green"
        Visible="false" />
    <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
    </script>

        </div>
        <div class="col-md-8">
            <label>Archivos cargados</label>
          <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IDE_CARTA,IDE_FILE,ARCHIVO" EmptyDataText="No hay registros para mostrar" Font-Size="8pt"  PageSize="50"  >
                                        <Columns>
                                         
                                            <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                           

                                           
                                           

                                            <asp:BoundField DataField="NOMBRE_ORIGINAL" HeaderText="File" SortExpression="NOMBRE_ORIGINAL" />
                                            <asp:BoundField DataField="FECHA_CARGA" HeaderText="Fecha" SortExpression="FECHA_CARGA" />
                                           

                                           
                                           

                                            <asp:TemplateField HeaderText="File">
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CartaCobranzas/"+Eval("ARCHIVO") %>'  Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IDE_FILE") %>' ImageUrl="~/imagenes/delete.png" OnClick="EliminarFile" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           

                                        </Columns>
                                    </asp:GridView>
                                    
                                   
                                </div>
            <asp:Button ID="btnAdjunto" runat="server" Text="Ver archivos" OnClick="btnAdjunto_Click" Visible="False" />
        </div>
        
    </div>
    <div class="row">
        <div class="col-md-12">
            <label>Notas:</label>
            <asp:TextBox ID="txtNota" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    <label>Aprobaciones</label>
    <asp:Panel ID="Panel1" runat="server"  CssClass="panelCuadro">
    <div class="row">
        <div class="col-md-3">
           <center> <b>Responsable emisión</b></center>
        </div>
        <div class="col-md-3">
            <center> <b>Gerente</b></center>
        </div>
        <div class="col-md-3">
             <center><b>Responsable revisión</b></center>
        </div>
        <div class="col-md-3">
             <center> <b>Gerente</b></center>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3" onkeypress="return event.keyCode!=13">
            <asp:UpdatePanel runat="server" ID="UpdOCosto" OnLoad="Upnl_LoadCosto">
                <ContentTemplate>
                    <div >
                        <asp:DropDownList ID="ddlOCosto" runat="server" Width="100%">
                        </asp:DropDownList>
                        
                        <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                        <script type="text/javascript" src="../js/jquery-ui.js"></script>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-3" onkeypress="return event.keyCode!=13">
            <asp:UpdatePanel runat="server" ID="UpdOgerencia" OnLoad="Upnl_LoadGerencia">
                <ContentTemplate>
                    <div >
                        <asp:DropDownList ID="ddlOGerencia" runat="server" Width="100%">
                        </asp:DropDownList>
                      
                        <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                        <script type="text/javascript" src="../js/jquery-ui.js"></script>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-3" onkeypress="return event.keyCode!=13">
            <asp:UpdatePanel runat="server" ID="UpdCosto_destino" OnLoad="Upnl_LoadCostoDestino">
                <ContentTemplate>
                    <div >
                        <asp:DropDownList ID="ddlCostoDestino" runat="server" Width="100%">
                        </asp:DropDownList>
                     
                        <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                        <script type="text/javascript" src="../js/jquery-ui.js"></script>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-3" onkeypress="return event.keyCode!=13">
            <asp:UpdatePanel runat="server" ID="UpdGerencia_destino" OnLoad="Upnl_LoadGerenciaDestino">
                <ContentTemplate>
                    <div >
                        <asp:DropDownList ID="ddlGerenciaDestino2" runat="server" Width="100%">
                        </asp:DropDownList>
                    
                        <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                        <script type="text/javascript" src="../js/jquery-ui.js"></script>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <center><asp:Label runat="server" ID="lbl1"></asp:Label></center>
        </div>
        <div class="col-md-3">
            <center><asp:Label runat="server" ID="lbl2"></asp:Label></center>
        </div>
        <div class="col-md-3">
           <center><asp:Label runat="server" ID="lbl3"></asp:Label></center>
        </div>
        <div class="col-md-3">
            <center><asp:Label runat="server" ID="lbl4"></asp:Label></center>
        </div>
    </div>
              </asp:Panel>
    <div class="row">
        <div class="col-md-12">
            <center>
<asp:Button runat="server" Text="Guardar carta" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>
                <asp:Button ID="btnaprobacion" runat="server" OnClick="btnaprobacion_Click" Text="Enviar aprobación" />
            </center>
            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnaprobacion"></cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnaprobacion"
                OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                    Mensaje
                </div>
                <div class="body">
                    ¿Deseas enviar carta cobraza para su aprobación?
                </div>
                <div class="footer" align="right">
                    <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>

