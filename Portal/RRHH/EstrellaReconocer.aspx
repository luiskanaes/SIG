<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="EstrellaReconocer.aspx.cs" Inherits="RRHH_EstrellaReconocer" %>

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
            *height: 1.7em;
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




        
.button {
  
 padding:7px 15px; 
    background:#195183; 
    border:0 none;
    cursor:pointer;
    -webkit-border-radius: 5px;
    border-radius: 5px; 
    color:#ffffff;
            vertical-align: middle;
}


  .box {
  width: 40%;
  margin: 0 auto;
  background: rgba(255,255,255,0.2);
  padding: 35px;
  border: 2px solid #fff;
  border-radius: 5px;
  background-clip: padding-box;
  text-align: center;
}



.overlay {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.7);
  transition: opacity 500ms;
  visibility: hidden;
  opacity: 0;
}
.overlay:target {
  visibility: visible;
  opacity: 1;
  z-index :999;
}

.popup {
  margin: 70px auto;
  padding: 10px;
  background: #fff;
  border-radius: 2px;
  width: 28%;
  position: relative;

}

.popup h2 {
  margin-top: 0;
  color: #333;
  font-family: Tahoma, Arial, sans-serif;
}
.popup .close {
  position: absolute;
  top: 20px;
  right: 30px;
  font-size: 30px;
  color: #DA070E;
  opacity: 1;
}
.popup .close:hover {
  color: #DA070E;
}
.popup .content {

  overflow: auto;
}
       
@media screen and (max-width: 700px){
  .box{
    width: 70%;
  }
  .popup{
    width: 100%;
  }
}
    </style>
     <link rel="stylesheet" href="../Content/chosen.css" />
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
    function JSFunction() {
        
        <%--__doPostBack('<%=udpHojaGestion.ClientID%>', '');--%>
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
                                __doPostBack('<%=Upnl.ClientID%>', this.element.attr("id"));
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
                        __doPostBack('<%=Upnl.ClientID%>', this.element.attr("id"));
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

        <section class="col-md-4">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/SSK_Estrella140X85.png" />
        </section>
        <section class="col-md-4">
            <br />
            <center>
                   <asp:Button runat="server" Text="MIS NOMINACIONES" ID="btnNominaciones" CausesValidation="False" OnClick="btnNominaciones_Click"></asp:Button>
                </center>
        </section>
        <section class="col-md-4">
            <br />
            <center>
              
               
                <a class="button" href="#popup1">POLÍTICAS DE NOMINACIÓN</a>
              

                <div id="popup1" class="overlay">
                <div class="popup">
               
                <a class="close" href="#">&times;</a>
                <div class="content">
              <asp:Image ID="Image2" runat="server"  ImageUrl="~/imagenes/Politicas_nominacion.jpg" CssClass="img-responsive" />
                </div>
                </div>
                </div>
             
             </center>

        </section>
        
    </div>
    <br />
      <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
             <asp:Label ID="Label1" runat="server" Text="CENTRO DE COSTO" Font-Bold="True" ></asp:Label>
            <asp:DropDownList ID="ddlCentro" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged" CssClass ="ddl" ></asp:DropDownList>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <br />
       <div class="row">

            <section class="col-md-4">
            </section>
            <section class="col-md-4">
                
                <asp:Label ID="Label3" runat="server" Text="NOMBRE DEL NOMINADO" Font-Bold="True" ></asp:Label>
                
                <br />
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
            </section>
            <section class="col-md-4">
            </section>
        </div>

    <div class="row">
        <br />
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <asp:Label ID="Label4" runat="server" Text="COMPETENCIAS" Font-Bold="True"></asp:Label>
         
                <asp:CheckBoxList ID="CheckCompetencia" runat="server" RepeatColumns="1" >
                </asp:CheckBoxList>
           
        </div>
        <div class="col-md-4">
        </div>
    </div>
     <div class="row">
         <br />
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <center>
                  <asp:Button ID="btnSustentar" runat="server" Text="SUSTENTAR" OnClick="btnSustentar_Click" CausesValidation="False" />
            </center>
          
        </div>
        <div class="col-md-4">
            <asp:Label ID="lblCodigos" runat="server" Visible ="false" ></asp:Label>
        </div>
    </div>
    <div class="row">
        <br />
        <div class="col-md-4">
        </div>
        
        <div class="col-md-4">
            <asp:ListView ID="ListView1" runat="server"  DataKeyField="IDE_NOMINACION,DNI_EVALUADO,IDE_FACTOR">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server"  Font-Bold="True" Text='<%# Eval("DES_FACTOR") %>' ></asp:Label>
                    
                    
                    <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandArgument='<%# Eval("IDE_NOMINACION") %>' ImageUrl="~/imagenes/Error.png" OnClick="EliminarNominacion" />
                    
                    
                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                    <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEliminar"
                        OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Mensaje 
                        </div>
                        <div class="body">
                            ¿Deseas eliminar nominación?
                        </div>
                        <div class="footer" align="right">
                            <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" CausesValidation="false"/>
                            <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" CausesValidation="false"/>
                        </div>
                    </asp:Panel>

                    <asp:Label ID="lblIDE_NOMINACION" runat="server" Text='<%# Eval("IDE_NOMINACION") %>'  Visible ="false"></asp:Label>
                    <asp:Label ID="lblDNI_EVALUADO" runat="server"  Text='<%# Eval("DNI_EVALUADO") %>'  Visible ="false"></asp:Label>
                    <asp:Label ID="lblIDE_FACTOR" runat="server"  Text='<%# Eval("IDE_FACTOR") %>'  Visible ="false"></asp:Label>
                <br />
                <asp:Label ID="Label6" runat="server" Text="Mencione la razón/acción vinculada a la competencia" Font-Italic="True" Font-Overline="False"></asp:Label>
                 
                    <asp:TextBox ID="txtSustento" runat="server" Text='<%# Eval("SUSTENTO") %>' MaxLength ="850" TextMode="MultiLine" Height ="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtSustento"
                        ErrorMessage="Falta ingresar sustento"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                <br />
                </ItemTemplate>
              
            </asp:ListView>
        </div>
        <div class="col-md-4">
        </div>
        
    </div>
    <div class="row">
        <br />
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <center>
                  <asp:Button ID="btnEnviar" runat="server" Text="ENVIAR NOMINACIÓN" OnClick="btnEnviar_Click" Visible="False" />
            </center>
            <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEnviar"></cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEnviar"
                OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                    Mensaje
                </div>
                <div class="body">
                    ¿Deseas enviar el nominación?
                </div>
                <div class="footer" align="right">
                    <asp:Button ID="btnYes" runat="server" Text="Sí" CssClass="yes" />
                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
                </div>
            </asp:Panel>

        </div>
        <div class="col-md-4">
        </div>
    </div>
    <asp:HiddenField ID="HidRegistro" runat="server" />
    <cc1:ModalPopupExtender ID="ModalRegistro"
        runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="pnlPopup2"
        PopupDragHandleControlID="pnlPopup"
        TargetControlID="HidRegistro">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup2" runat="server">
        <table class="style1">

            <tr>
                <td style="vertical-align: middle; text-align: center">
                    <asp:Label ID="Label2" runat="server" Text="Registro exitoso. Muchas gracias." ForeColor="White"></asp:Label>
                    
                    
                </td>

            </tr>
            <tr>
                    
                <td style="vertical-align: middle; text-align: center">
                    <br />
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/close_window.png" OnClick="ImageButton3_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

