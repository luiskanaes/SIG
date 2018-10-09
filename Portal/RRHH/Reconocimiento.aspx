<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master"  AutoEventWireup="true" CodeFile="Reconocimiento.aspx.cs" Inherits="RRHH_Reconocimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #57227a;
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
  padding: 5px 15px;
            background: #57227a;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
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
        
        __doPostBack('<%=udpHojaGestion.ClientID%>', '');
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
    <asp:UpdatePanel ID="udpHojaGestion" runat="server" OnLoad="Upnl_Load">
<ContentTemplate>
    <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
        
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
    
  
     <div class="row">

            <section class="col-md-4">
               
                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/imagenes/Bravo170.png" NavigateUrl="~/RRHH/ReconocerTalento.aspx"></asp:HyperLink>
            </section>
            <section class="col-md-4">
            </section>
         <section class="col-md-4">
             <br />
             <center>
              
               
                <a class="button" href="#popup1">¿CÓMO RECONOCER?</a>
              

                <div id="popup1" class="overlay">
                <div class="popup">
               
                <a class="close" href="#">&times;</a>
                <div class="content">
              <asp:Image ID="Image2" runat="server"  ImageUrl="~/imagenes/EjemploBravo.png" CssClass="img-responsive" />
                </div>
                </div>
                </div>
             </center>
             
            </section>
         </div>
   
           <div class="row">

            <section class="col-md-4">
            </section>
            <section class="col-md-4">
                
                <asp:Label ID="Label3" runat="server" Text="NOMBRE DEL RECONOCIDO" Font-Bold="True" ForeColor="#990099"></asp:Label>
                <%-- <asp:DropDownList ID="ddlPersonal" runat="server" CssClass="ddl"></asp:DropDownList>   --%>
                <br />
                <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                    <ContentTemplate>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlPersonal" Width="96%"  ></asp:DropDownList>
                            <link rel="stylesheet" href="../Content/jquery-ui.css" />
                            <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                            <script type="text/javascript" src="../js/jquery-ui.js"></script>
                            <%--<script src="../js/jquery.min.js" type="text/javascript"></script>
                <script src="../js/chosen.jquery.js" type="text/javascript"></script>
                <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </section>
            <section class="col-md-4">
            </section>
        </div>
 
    <div class="row">
        <br />
            <section class="col-md-4">
                
            </section>
                <section class="col-md-4">
                <asp:Label ID="Label4" runat="server" Text="COMPETENCIA" Font-Bold="True" ForeColor="#990099"></asp:Label>
                    <asp:DropDownList ID="ddlCompetencia" runat="server" CssClass="ddl" Width="98%"></asp:DropDownList>
            </section>
            <section class="col-md-4">
                
            </section>
        </div>
        <div class="row">
            <br />
        <section class="col-md-4">
                
        </section>
            <section class="col-md-4">
            <asp:Label ID="Label5" runat="server" Text="¿POR QUÉ LO RECONOCES?" Font-Bold="True" ForeColor="#990099"></asp:Label>
                
                <br />
                <asp:Label ID="Label6" runat="server" Text="Mencione la razón/acción vinculada a la competencia" Font-Italic="True" Font-Overline="False"></asp:Label>
                 <asp:TextBox ID="txtSustento" runat="server" placeholder="Sustento" CssClass="textarea.input-group-lg" Height="150px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                
                
                <asp:Label ID="Label2" runat="server" Text="máximo 850 caracteres" Font-Size="11pt"></asp:Label>
            </section>
        <section class="col-md-4">
                
        </section>
        </div>


    <div class="row">
        <br />
        <center>
        <asp:Button ID="btnIngresar" runat="server" Text="ENVIAR EL RECONOCIMIENTO" OnClick="btnIngresar_Click" />

        <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnIngresar"></cc1:ConfirmButtonExtender>
        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnIngresar"
            OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Mensaje
            </div>
            <div class="body">
                ¿Deseas enviar el reconocimiento?
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
            </div>
        </asp:Panel>
            
                    
               
        </center>
    </div>
   
        
 

     </ContentTemplate>
            <Triggers>    
              <%--  <asp:AsyncPostBackTrigger ControlID="btnIngresar"  />--%>
            </Triggers>
</asp:UpdatePanel>
 
 <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="../images/loading.gif" style="vertical-align: middle" alt="Procesando" />
                    
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
     <cc1:modalpopupextender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>

