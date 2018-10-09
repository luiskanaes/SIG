<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="SolicitudReclutamiento.aspx.cs" Inherits="RRHH_SolicitudReclutamiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

        <script type="text/javascript">
   
            

            function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=CheckSoftware.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
     function ValidateCheckBoxList2(sender, args) {
        var checkBoxList = document.getElementById("<%=CheckOtros.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
     }

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
                $("#<%= ddlJefe.ClientID %>").combobox();
                 $("#<%= ddlAprobador.ClientID %>").combobox();
            });
        }
</script>
      <style type="text/css">
          .adjunto
        {
            padding: 5px 15px;
            background: #ec9600;
            border: 0 none;
            cursor: pointer;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            color: #ffffff;
        }
         </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="udpHojaGestion" runat="server" OnLoad="Upnl_Load">
<ContentTemplate>
 <asp:HiddenField ID="hdcodigo" runat="server" />
    <asp:HiddenField ID="hdEstado" runat="server" />
    <asp:HiddenField ID="hdPersonal" runat="server" />
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4" style="text-align:right">
            
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
            <asp:Button ID="btnBandeeja" runat="server" Text="Bandeja" OnClick="btnBandeeja_Click" />
           
        </div>
    </div>
     <br />
    <div class="row">

        <div class="panel panel-default">
            <div class="panel-heading">REQUERIMIENTO DE PERSONAL MOI</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Ticket</label>
                        <asp:TextBox ID="txtTicket" runat="server" BackColor="#F0F0F0" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:CheckBox ID="Checkpersonal" runat="server" AutoPostBack="True" OnCheckedChanged="Checkpersonal_CheckedChanged" Text="Personal (Registrado en Sisplan)" />
                       
                        <asp:UpdatePanel runat="server" ID="Upnl" OnLoad="Upnl_Load">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList runat="server" ID="ddlPersonal"></asp:DropDownList>
                                    <link rel="stylesheet" href="../Content/jquery-ui.css" />
                                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-md-3">
                       
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Dni (Personal nuevo)</label>
                        <asp:TextBox ID="txtDni" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Apellido paterno</label>
                        <asp:TextBox ID="txtPaterno" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Apellido materno</label>
                        <asp:TextBox ID="txtMaterno" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Nombres</label>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                         
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Cargo</label>
                        <asp:DropDownList ID="ddlCargos" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
            </div>
        </div>


    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">1.- DATOS DEL PUESTO</div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-3">
                       <label>Empresa</label>
                        <asp:DropDownList ID="ddlEmpresas" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-3">

                        <label>Gerencia</label>
                        <asp:DropDownList ID="ddlGerencia" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGerencia_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">

                        <label>Centro</label>

                        <asp:DropDownList ID="ddlCentro" runat="server"  CssClass="ddl" ></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>Area</label>
                        <asp:TextBox ID="txtarea" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Jefe inmediato</label>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" OnLoad="Upnl_Load">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList runat="server" ID="ddlJefe"></asp:DropDownList>
                                    <link rel="stylesheet" href="../Content/jquery-ui.css" />
                                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>



                <div class="row">
                    <div class="col-md-3">
                        <label>Tipo de proceso</label>
                        <asp:RadioButtonList ID="RdoTipoProceso" runat="server" 
                            RepeatDirection="Horizontal" >
                        </asp:RadioButtonList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                              ControlToValidate="RdoTipoProceso" CssClass="EtiquetaNegrita" 
                              ErrorMessage="(*)" validationgroup="Validar" 
                              ForeColor="Red" />

                    </div>
                    <div class="col-md-3">
                         <label>Origen posición</label>
                        <asp:RadioButtonList ID="rdoOrigen" runat="server" 
                            RepeatDirection="Horizontal" >
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                              ControlToValidate="rdoOrigen" CssClass="EtiquetaNegrita" 
                              ErrorMessage="(*)" validationgroup="Validar" 
                              ForeColor="Red" />
                    </div>
                    <div class="col-md-3">
                        <label>Reclutamiento</label>
                        <asp:RadioButtonList ID="rdoRecObra" runat="server" 
                            RepeatDirection="Horizontal" >
                        </asp:RadioButtonList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                              ControlToValidate="rdoRecObra" CssClass="EtiquetaNegrita" 
                              ErrorMessage="(*)" validationgroup="Validar" 
                              ForeColor="Red" />
                    </div>
                    <div class="col-md-3">
                        <label>Reclutamiento Lugar</label>
                        <asp:RadioButtonList ID="rdoRecLima" runat="server" 
                            RepeatDirection="Horizontal" >
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                              ControlToValidate="rdoRecLima" CssClass="EtiquetaNegrita" 
                              ErrorMessage="(*)" validationgroup="Validar" 
                              ForeColor="Red" />
                    </div>
                </div>


            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">2.- PERFIL DEL PUESTO</div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-3">
                        <label>Formación academica</label>
                        <asp:DropDownList ID="ddlNivelAcademico" runat="server" CssClass="ddl"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>Especialización</label>
                        <asp:DropDownList ID="ddlcarrera" runat="server" CssClass="ddl"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label>N° Colegiatura</label>
                        <asp:TextBox ID="txtColegiatura" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Habilitado</label>
                        <asp:RadioButtonList ID="RdoColegiatura" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Ingles</label>
                        <asp:RadioButtonList ID="rdoIngles" runat="server" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                              ControlToValidate="rdoIngles" CssClass="EtiquetaNegrita" 
                              ErrorMessage="(*)" validationgroup="Validar" 
                              ForeColor="Red" />
                    </div>
                    <div class="col-md-3">
                         <label>Maestría</label>
                        <asp:RadioButtonList ID="rdoMaestria" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                         <label>Software</label>
                        <asp:RadioButtonList ID="rdoSoftware" runat="server" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                              ControlToValidate="rdoSoftware" CssClass="EtiquetaNegrita" 
                              ErrorMessage="(*)" validationgroup="Validar" 
                              ForeColor="Red" />
                    </div>
                    <div class="col-md-3">
                        <label>Sexo</label>
                        <asp:RadioButtonList ID="rdoSexo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Femenino</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Masculino</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>Estado civil</label>
                        <asp:DropDownList ID="ddlcivil" runat="server" CssClass="ddl"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">4.- FUNCIONES DEL PUESTO</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox ID="txtFuncionesPuesto" runat="server" TextMode="MultiLine" Height="80px" MaxLength="850"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">5.- CONDICIONES DE CONTRATACION</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Remuneración mensual bruta</label>
                        <asp:TextBox ID="txtRemuneracion" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                         <label>Comisiones</label>
                        <asp:TextBox ID="txtComisiones" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>Gratificación por objetivos logrados</label>
                        <asp:RadioButtonList ID="rdoGratificaciones" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                        <label>Premio de obra</label>
                        <asp:RadioButtonList ID="rdoPremioObra" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-3">
                        <label>Inicio Contrado</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtinicio" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                        TargetControlID="txtinicio"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />

                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtinicio" PopupButtonID="ImageButton1" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha ingreso" />
                                    </span>
                                </div>
                    </div>
                    <div class="col-md-3">
                        <label>Fin contrato</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtfin" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                        TargetControlID="txtfin"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />

                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfin" PopupButtonID="ImageButton2" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <span class="input-group-addon">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/imagenes/calendar.png" ToolTip="Fecha ingreso" />
                                    </span>
                                </div>
                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">6.- BENEFICIOS</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Vales de alimentación</label>
                        <asp:RadioButtonList ID="rdoValesAlimento" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                        <label>Seguro de vida</label>
                        <asp:RadioButtonList ID="rdoSeguroVida" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                        <label>Asignación movilidad</label>
                        <asp:RadioButtonList ID="rdoAsignacionMovil" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">7.- CONDICIONES DE TRABAJO</div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-3">
                        <label>Régimen de trabajo</label>
                        <asp:TextBox ID="txtregimen" runat="server"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <label>Horario de trabajo</label>
                        <asp:TextBox ID="txtHorarioTrabajo" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                         <label>Bono de destaque</label>
                        <asp:RadioButtonList ID="rdoBonoDestaque" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                         <label>Pasaje</label>
                        <asp:RadioButtonList ID="rdoPasaje" runat="server" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">8.- COMENTARIOS GENERALES</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox ID="txtComentarioGnral" runat="server" TextMode="MultiLine" Height="80px" MaxLength="850"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-heading">9.- ASIGNACIÓN DE RECURSOS</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Equipo</label>
                        <asp:RadioButtonList ID="rdoEquipo" runat="server" CssClass="EtiquetaNegrita">
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                              ControlToValidate="rdoEquipo" 
                              ErrorMessage="Seleccionar equipo(*)" validationgroup="Validar" 
                              ForeColor="Red" />
                    </div>
                    <div class="col-md-4">
                        <label>Software</label>
                         <asp:CheckBoxList ID="CheckSoftware" runat="server">
                        </asp:CheckBoxList>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" 
                            ClientValidationFunction="ValidateCheckBoxList"
                            ErrorMessage="Elegir Software(*)" ForeColor="Red" validationgroup="Validar" />
                    </div>
                    <div class="col-md-4">
                        <label>Otros recursos</label>
                         <asp:CheckBoxList ID="CheckOtros" runat="server">
                        </asp:CheckBoxList>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" 
                            ClientValidationFunction="ValidateCheckBoxList2" 
                            ErrorMessage="Elegir Item(*)" ForeColor="Red" validationgroup="Validar" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Ficha de solicitud</label>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="adjunto" />
                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" ForeColor="#FF3300" Visible="False">[HyperLink1]</asp:HyperLink>
                    </div>

                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>Aprobador (Proceso equipos mobiles)</label>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnLoad="Upnl_Load">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList runat="server" ID="ddlAprobador"></asp:DropDownList>
                                    <link rel="stylesheet" href="../Content/jquery-ui.css" />
                                    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
                                    <script type="text/javascript" src="../js/jquery-ui.js"></script>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                


                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:ImageButton ID="btnGuardar" runat="server"
                            ImageUrl="~/imagenes/boton.guardar.gif" OnClick="btnGuardar_Click"
                            ValidationGroup="Validar" />
                        <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnGuardar"></cc1:ConfirmButtonExtender>
                        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnGuardar"
                            OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                                Mensaje
                            </div>
                            <div class="body">
                                Guardar requerimiento, Desea Continuar?
                            </div>
                            <div class="footer" align="right">
                                <asp:Button ID="btnYes" runat="server" Text="Si" CssClass="yes" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
                            </div>
                        </asp:Panel>
                        <asp:ImageButton ID="btnEnviar" runat="server" ImageUrl="~/imagenes/boton.enviar.gif" OnClick="btnEnviar_Click" Visible="False" />
                        <cc1:ConfirmButtonExtender ID="cbeP1" runat="server" DisplayModalPopupID="mpeP1" TargetControlID="btnEnviar"></cc1:ConfirmButtonExtender>
                        <cc1:ModalPopupExtender ID="mpeP1" runat="server" PopupControlID="pnlPopupP1" TargetControlID="btnEnviar"
                            OkControlID="btnYesP1" CancelControlID="btnNoP1" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopupP1" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                                Mensaje
                            </div>
                            <div class="body">
                                Una vez enviado el requerimiento ya no podra realizar cambios ¿Deseas continuar? 
                            </div>
                            <div class="footer" align="right">
                                <asp:Button ID="btnYesP1" runat="server" Text="Sí" CssClass="yes" />
                                <asp:Button ID="btnNoP1" runat="server" Text="No" CssClass="no" />
                            </div>
                        </asp:Panel>
			
                         <asp:ImageButton ID="btnNotificar" runat="server" ImageUrl="~/imagenes/boton.Notidficar.png" OnClick="btnNotificar_Click" Visible="False" />
                        <cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnNotificar"></cc1:ConfirmButtonExtender>
                        <cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnNotificar"
                            OkControlID="btnYes1" CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                                Mensaje
                            </div>
                            <div class="body">
                                ¿Deseas notificar sobre los cambios realizados?
                            </div>
                            <div class="footer" align="right">
                                <asp:Button ID="btnYes1" runat="server" Text="Sí" CssClass="yes" />
                                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="no" />
                            </div>
                        </asp:Panel>
                         </div>
                </div>
            </div>
        </div>
    </div>
    
    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

