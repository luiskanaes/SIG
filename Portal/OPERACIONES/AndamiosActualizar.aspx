<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="AndamiosActualizar.aspx.cs" Inherits="OPERACIONES_AndamiosActualizar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style >
        .panelCuadro {
  margin-bottom: 20px;
  padding :10px;
  background-color: #F7DC6F;
  border: 1px solid transparent;
  border-radius: 4px;
  -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
          box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
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
            *height: 2.0em;
            *top: 0.1em;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 0.5em;
           width :90%;
            
        }

        /*Demo fix*/
        .custom-combobox a {
            height: 35px;
            margin-top: -6px;
            visibility: hidden;
        }
            div.DialogueBackground 
        { 
            position:absolute; 
            width:95%; 
            height:100%; 
            top:0; 
            left:0; 
            background-color:#777; 
            opacity:0.5;
            filter: alpha(opacity=50); 
            text-align:center; 
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
    //document.onselectstart = function () { return false; }
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>SOLICITUD DE ANDAMIOS : </b>
                <asp:Label ID="lblCentro" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
               
                <asp:Label ID="lblTicket" runat="server" Font-Bold="True"></asp:Label>
               
            </div>
        </div>
    </div>
   
    <div class="row">


        <asp:Panel ID="Panel1" runat="server" CssClass="panelCuadro">
            <div class="row">
                <div class="col-md-2">
                     </div>
                <div class="col-md-2">
                    <label>Fecha requerida</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtrequerida" runat="server" CssClass="form-control" ></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtrequerida"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtrequerida" PopupButtonID="ImageButton1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        <span class="input-group-addon">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio"  />
                        </span>
                </div>
                     </div>
                <div class="col-md-4">
                    <label>Solicitante</label>
                    <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlPersonal"></asp:DropDownList>
                            <link rel="stylesheet" href="../Content/jquery-ui.css" />
                            <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                            <script type="text/javascript" src="../js/jquery-ui.js"></script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
                <div class="col-md-2">
                    <label>Area</label>
                    <asp:DropDownList ID="ddlarea" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
                </div>
                 <div class="col-md-2">
                     </div>
           
            </div>

            <div class="row">
                 <div class="col-md-2">
                    
                </div>
                
                <div class="col-md-2">
                     <label>Solicitud</label>
                    <asp:DropDownList ID="ddlSolicitud" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Tipo</label>
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                     <label>Especialidad</label>
                    <asp:DropDownList ID="ddlespecialidad" runat="server" CssClass="ddl" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    
                </div>
            </div>

            

            <div class="row">
                <div class="col-md-2"> </div>
              
                <div class="col-md-8">
                   <label>Texto (comentarios)</label>
                    <asp:TextBox ID="txtcomentarios" runat="server" MaxLength="850" Height="70px" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    
                </div>


               
            </div>
            <div class="row">
                <div class="col-md-2">

                </div>

                <div class="col-md-6">
                    <label>Cargar archivo</label>
                    <div class="input-group">
                        <label class="input-group-btn">
                            <span class="btn btn-primary">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </span>
                        </label>

                    </div>
                </div>
                <div class="col-md-2">
                    <label>Código Ref.</label>
                    <asp:TextBox ID="txtCodAndamio" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:Button ID="btnviar" runat="server" Text="GUARDAR" OnClick="btnviar_Click"></asp:Button>
                   

                    <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnviar"></cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnviar"
                        OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Mensaje
                        </div>
                        <div class="body">
                            ¿Deseas enviar solicitud?
                        </div>
                        <div class="footer" align="right">
                            <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                            <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                        </div>
                    </asp:Panel>
                </div>
               
            </div>
            <br />
        </asp:Panel>

    </div>
 </ContentTemplate>
      <Triggers>
          <asp:PostBackTrigger ControlID="btnviar"  />
      </Triggers>
 </asp:UpdatePanel>
</asp:Content>
