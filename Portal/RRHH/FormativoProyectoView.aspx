<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="FormativoProyectoView.aspx.cs" Inherits="RRHH_FormativoProyectoView" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<style type="text/css">
      
    </style>--%>
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
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                var ModalProgress = '<%= ModalProgress.ClientID %>';
            </script>
            <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/mensaje_formativo-01.fw.png" />
            <div class="row">
                <label class="headerText">A.- Información general</label>
            </div>
            <div class="row">
                 <div class="col-md-3">
                      <label>Cargo</label>
                     <asp:TextBox ID="txtCargo" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Nombre Proyecto</label>
                    <asp:TextBox ID="txtProyecto" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                      <label>Duración</label>
                     <asp:TextBox ID="txtDuracion" runat="server"></asp:TextBox>
                </div>
               <div class="col-md-3">
                    <label>Cantidad</label>
                     <asp:TextBox ID="txtCantidad" onkeydown="return jsDecimals(event);" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label>Centro Costo</label>
                    <asp:TextBox ID="txtCentro" runat="server" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Detalle Ceco</label>
                    <asp:TextBox ID="txtNombreCeco" runat="server" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Ubicación</label>
                    <asp:TextBox ID="txtUbicacion" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Supervisor</label>
                    <asp:TextBox ID="txtSupervisor" runat="server"></asp:TextBox>
                </div>
                </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblIdCargo" runat="server" Visible="False"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <br />
            <div class="row">
                <label class="headerText">B.- Descripción del Proyecto</label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Objetivos</label>
                    <asp:TextBox ID="txtObjetivo" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label>Entregables</label>
                    <asp:TextBox ID="txtEntregable" runat="server" Height="150px" TextMode="MultiLine"  ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Indicadores</label>
                    <asp:TextBox ID="txtIndicadores" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
                <div class="col-md-6">
                   
                </div>
            </div>
            <br />
            <div class="row">
                <label class="headerText">C.- Alcance y extensión del proyecto</label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>¿Qué incluye?</label>
                    <asp:TextBox ID="txtIncluye" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label>¿Qué no incluye?</label>
                    <asp:TextBox ID="txtNoincluye" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Riesgos</label>
                    <asp:TextBox ID="txtRiesgo" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label>Restricciones</label>
                    <asp:TextBox ID="txtRestricciones" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>

            </div>
            <br />
            <div class="row">
                <label class="headerText">D.- Planeamiento inicial del proyecto</label>
            </div>
           <div class="row">
                <div class="col-md-6">
                    <label>Requisitos</label>
                    <asp:TextBox ID="txtRequisitos" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label>Costos estimados</label>
                    <asp:TextBox ID="txtCosto" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Beneficios estimados</label>
                    <asp:TextBox ID="txtBeneficios" runat="server"  Height="150px" TextMode="MultiLine" ></asp:TextBox>
                </div>
                <div class="col-md-6">
                </div>
            </div>
            <br />
            <div class="row">
                <label class="headerText">Principales Etapas</label>
            </div>
            <br />
            <div class="row">
                <br />
                <center>
                <div class="table-responsive">
                <asp:GridView runat="server" ID="gridHito" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" Width="60%" Font-Size="9pt">
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="Item" SortExpression="Row" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion" SortExpression="DESCRIPCION" />
                        <asp:BoundField DataField="FECHA_HITO" HeaderText="Fecha" SortExpression="FECHA_HITO" />

                    </Columns>
                    </asp:GridView>
                 </div>
                </center>
            </div>
            <br />
            <div class="row">
                <label class="headerText">Relaciones Claves</label>
            </div>

            <div class="row">
                 <br />
                <center>
                    <div class="table-responsive">
                <asp:GridView runat="server" ID="GridStakeholder" AutoGenerateColumns="False" Width="60%" CssClass="table table-striped table-bordered table-hover" Font-Size="9pt">
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="Item" SortExpression="Row" />
                        <asp:BoundField DataField="HOLDER" HeaderText="Stakeholders" SortExpression="HOLDER" />
                        <asp:BoundField DataField="ROL" HeaderText="Rol" SortExpression="ROL" />
                        <asp:BoundField DataField="INTERACCION" HeaderText="Interración" SortExpression="INTERACCION" />

                    </Columns>
                    </asp:GridView>
                        </div>
                </center>
            </div>

            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblComentarioGeneral" runat="server"></asp:Label>
                    <asp:TextBox ID="txtComentarioGeneral" runat="server" Height="100px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <br />
                    <asp:Label ID="lblComentarioArea" runat="server"></asp:Label>
                    <asp:TextBox ID="txtComentarioArea" runat="server" Height="100px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <br />
                    <asp:Label ID="lblComentarioRRHH" runat="server"></asp:Label>
                    <asp:TextBox ID="txtComentarioRRHH" runat="server" Height="100px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-6">
                    <br />
                    <asp:Label ID="lblRechazo" runat="server"></asp:Label>
                    <asp:TextBox ID="txtRechazo" runat="server" Height="100px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row">
                <br />
                <center>
            <button onclick="goBack()" class="btn-danger" style="height: 30px">Regresar</button>

            <script>
                function goBack() {
                    window.history.back();
                }
            </script>
                    <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
        </center>
            </div>
        </ContentTemplate>
        <Triggers>
        
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
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>







