<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="EquipoMayoresAtencion.aspx.cs" Inherits="CAREMENOR_EquipoMayoresAtencion" %>

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
        

      


        /*Demo fix*/
       

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
                      __doPostBack('<%=Upnl.ClientID%>', this.element.attr("id"));
                    },

                    _destroy: function () {
                        this.wrapper.remove();
                        this.element.show();
                    }
                });
            })(jQuery);

            $(document).ready(function () {
               
                $("#<%= ddlProveedor.ClientID %>").combobox();
                  $("#<%= ddlRequerimiento.ClientID %>").combobox();
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
                <b>Datos solicitud</b>
            </div>
        </div>
    </div>


        <div class="row">
            <div class="col-md-6">
                <asp:Panel ID="Panel1" runat="server" CssClass="panelCuadro">
                    <div class="row">


                        <div class="col-md-6">

                            <label>Documento</label>
                            <asp:DropDownList ID="ddldocumento" runat="server" CssClass="ddl"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label>Movilización</label>
                            <asp:DropDownList ID="ddlMovilizacion" runat="server" CssClass="ddl"></asp:DropDownList>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label>Fecha Legajo</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtFechaLegajo" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                    TargetControlID="txtFechaLegajo"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" />

                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaLegajo" PopupButtonID="btnCalender1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                <span class="input-group-addon">
                                    <asp:ImageButton ID="btnCalender1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha incio" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Operario</label>
                            <asp:DropDownList ID="ddlOperario" runat="server" CssClass="ddl">
                                <asp:ListItem Value="0">NO</asp:ListItem>
                                <asp:ListItem Value="1">SI</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                             <label>F. Fin alquiler</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFinSalida" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                        TargetControlID="txtFinSalida"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />

                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFinSalida" PopupButtonID="ImageButton1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha fin alquilar" />
                                    </span>
                                </div>
                        </div>
                        <div class="col-md-6">
                              <label>Atencion</label>
                            <asp:DropDownList ID="ddlAtencion" runat="server" CssClass="ddl"></asp:DropDownList>
                        </div>
                        
                    </div>
                   <%-- <div class="row">
                        <div class="col-md-12">
                            <label>Cargar archivo</label>
                            <div class="input-group">
                                <label class="input-group-btn">
                                    <span class="btn btn-primary">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </span>
                                </label>

                            </div>
                        </div>

                    </div>--%>
                    <div class="row">
                        <div class="col-md-12" style="height: 42px">
                            <label>Proveedor</label>
                            <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="ddl"></asp:DropDownList>
                                    <link rel="stylesheet" href="../Content/jquery-ui.css" />
                                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    
                    <div class="row">
                        <div class="col-md-12">
                           <label>Comentarios</label>
                            <asp:TextBox ID="txtComentarios" runat="server" Height="80px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                          
                            
                        </div>
                    </div>

                </asp:Panel>
                <%-- fin primer--%>
            </div>


                <%--segundo grupo--%>
                
            <div class="col-md-6">
                <asp:Panel ID="Panel2" runat="server" CssClass="panelCuadro">
                    <div class="row">
                        
                        <div class="col-md-12">
                            <label>1. Legajo</label>
                            <asp:FileUpload ID="FileUploadLegajo" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-12">
                             <label>2. Adjudicación directa</label>
                            <asp:FileUpload ID="FileUploadAdjudicacion" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-12">
                             <label>3. Nota de alquiler</label>
                             <asp:FileUpload ID="FileUploadNotaAlquiler" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-12">
                             <label>4. Contrato</label>
                            <asp:FileUpload ID="FileUploadContrato" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-12">
                              <label>5. Otros</label>
                             <asp:FileUpload ID="FileUploadOtros" runat="server" />
                        </div>
                    </div>
                     <br /><br />
                    <div class="row">
                        <div class="col-md-12">
                            <center>

                                <asp:Button ID="btnCargar" runat="server" Text="Guardar" OnClick="btnCargar_Click" Visible="False" />
                                <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnCargar"></cc1:ConfirmButtonExtender>
                                <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnCargar"
                                    OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                        Mensaje
                                    </div>
                                    <div class="body">
                                        ¿Deseas guardar registros?
                                    </div>
                                    <div class="footer" align="right">
                                        <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                        <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                    </div>
                                </asp:Panel>

                                <asp:Button ID="btnFile" runat="server" Text="Cargar" OnClick="btnFile_Click" Style="display: none"/>
                                <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnFile.ClientID %>").click();
            }
        }
    </script>
                            </center>
                        </div>
                    </div>
                   
                </asp:Panel>
            </div>
                
            </div>
                                <br />
       
                        <div class="row">
                            <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="ide_LegajoFile,FILE_ARCHIVO,CODIGO_GRUPO" EmptyDataText="There are no data records to display." Font-Size="8pt" >
                                            <Columns>

                                                <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                                <asp:BoundField DataField="FILE_NOMBRE" HeaderText="File" SortExpression="FILE_NOMBRE" />
                                                <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" />
                                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha" SortExpression="fecha_registro" />
                                                <asp:BoundField DataField="CODIGO_GRUPO" HeaderText="Grupo Legajo" SortExpression="CODIGO_GRUPO">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="File">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagenes/adjuntar32x32.png" NavigateUrl='<%# "~/File/CareMenor/Alquiler/"+Eval("FILE_ARCHIVO") %>' Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("ide_LegajoFile") %>' ImageUrl="~/imagenes/delete.png" OnClick="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                            </div>
                        </div>



        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Asociar Legajos/requerimiento</b>
                </div>
            </div>
        </div>
         <div class="row">
         
            <div class="col-md-3">
                   <label>Requerimiento</label>
                <asp:UpdatePanel runat="server" ID="Upnl1" OnLoad="Upnl_LoadRequerimiento">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="ddlRequerimiento" Width="110%"></asp:DropDownList>
                    <link rel="stylesheet" href="../Content/jquery-ui.css" />
                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
           
            <div class="col-md-3">
                <%-- <label>F. Fin alquiler</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSalida" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                        TargetControlID="txtSalida"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />

                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSalida" PopupButtonID="ImageButton6" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha fin alquilar" />
                                    </span>
                                </div>--%>
            </div>
              <div class="col-md-3">
                <br />
                <asp:ImageButton ID="btnAsociar" runat="server" ImageUrl="~/imagenes/boton.agregar.gif" OnClick="btnAsociar_Click" />
            </div>
            <div class="col-md-3">
            </div>
        </div>

        
                    <br />
       
                        <div class="row">
                            <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Reqs_CodigoCompleto,CODIGO_GRUPO" EmptyDataText="There are no data records to display." Font-Size="8pt" >
                                            <Columns>

                                                <asp:BoundField DataField="Row" HeaderText="#" ReadOnly="True" SortExpression="Row" />
                                                <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Requerimiento" SortExpression="Reqs_CodigoCompleto" />
                                                <asp:BoundField DataField="CODIGO_GRUPO" HeaderText="Grupo Legajo" SortExpression="CODIGO_GRUPO">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fin_contrato" HeaderText="Fin contrato" SortExpression="fin_contrato">
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                               <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("Reqs_CodigoCompleto") %>' ImageUrl="~/imagenes/delete.png" OnClick="Retirar" />
                                                        <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
                                                        <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnEliminar"
                                                            OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                                                        </cc1:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                                Mensaje
                                                            </div>
                                                            <div class="body">
                                                                ¿Deseas retirar requerimiento?
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                                                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                                                            </div>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                            </div>
                        </div>
              <br />
              <br /><br />
              <br />
    </ContentTemplate>
      <Triggers>
   <asp:PostBackTrigger ControlID="btnFile"  />
           <asp:PostBackTrigger ControlID="btnFile"  />
         
      </Triggers>
 </asp:UpdatePanel>
</asp:Content>


