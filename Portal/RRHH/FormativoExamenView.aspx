<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="FormativoExamenView.aspx.cs" Inherits="RRHH_FormativoExamenView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
    <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
        
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>
    <div class="row">
        <center>
        <asp:Label ID="lblCabcera" runat="server" Text="Label" Font-Bold="True"></asp:Label>
         </center>
    </div>
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <center>
            <asp:Image ID="imgFotos" runat="server" Width="120px" /><br />
           
            </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <center>
             <asp:Label ID="lblNombre" runat="server" Text="Label" Font-Bold="True" Font-Size="9pt"></asp:Label>
            </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
           <asp:Label ID="Label1" runat="server" Text="1.- COMPETENCIAS CORPORATIVAS" CssClass="headerText" ></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12" style="text-align: justify">
           <b>INSTRUCCIONES: </b> <asp:Label ID="lblintro" runat="server" Text="Label" Font-Size="9pt"></asp:Label>
        </div>
    </div>
    <br />
     <div class="row">
        <div class="col-md-12">
           <b>COMPETENCIAS CARDINALES</b> 
        </div>
    </div>
    <br />
     <div class="row">
        <div class="col-lg-12 ">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID_PARAMETRO">
                <ItemTemplate>

                    <div class="col-lg- 12">
                        <div class="panel panel-primary">
                            <div class="panel-footer">
                                <b><%# Eval("DES_ASUNTO")%></b>
                                
  
                            </div>
                            <div class="panel-body" style="text-align: justify">
                                 <%# Eval("DES_CAMPO1")%>
                                <br /><br />
                                <b>NIVELES</b>
                                <div class="radio radiobuttonlist col-sm-12">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" ></asp:RadioButtonList>
                                </div>
                               
                            </div>

                            <%--<div class="panel-footer">
                                
                            </div>--%>

                        </div>
                    </div>


                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <br />
     <div class="row">
        <div class="col-md-12">
           <b>COMPETENCIAS ESPECÍFICAS</b> 
        </div>
    </div>
    <br />
         <div class="row">
        <div class="col-lg-12 ">
            <asp:ListView ID="ListView2" runat="server" DataKeyNames="ID_PARAMETRO">
                <ItemTemplate>

                    <div class="col-lg- 12">
                        <div class="panel panel-primary">
                            <div class="panel-footer">
                                <b><%# Eval("DES_ASUNTO")%></b>
                                
  
                            </div>
                            <div class="panel-body" style="text-align: justify">
                                 <%# Eval("DES_CAMPO1")%>
                                <br /><br />
                                <b>NIVELES</b>
                                <div class="radio radiobuttonlist col-sm-12">
                                <asp:RadioButtonList ID="RadioButtonList2" runat="server"></asp:RadioButtonList>
                                    
                                </div>
                               
   
                                 
                            </div>

                            <%--<div class="panel-footer">
                                
                            </div>--%>

                        </div>
                    </div>


                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
           <asp:Label ID="Label2" runat="server" Text="2.- INTEGRACIÓN DE RESULTADOS" CssClass="headerText" ></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12"  style="text-align: justify">
           <asp:Label ID="Label3" runat="server" Text="Indicar tres competencias o cualidades en las cuales destaca (Fortalezas) y tres aspectos en las cuales muestra potencial para desarrollar o mejorar (Áreas de mejora)"></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-6">
            <label>Fortalezas</label>
            <asp:TextBox ID="txtFortalezas" runat="server" Height="150px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label>Áreas de mejora</label>
            <asp:TextBox ID="txtoportunidades" runat="server" Height="150px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
           <asp:Label ID="Label4" runat="server" Text="3.- COMPROMISO DE MEJORA" CssClass="headerText" ></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12"  style="text-align: justify">
           <asp:Label ID="Label5" runat="server" Text="Enumerar algunas acciones a realizar para la mejora de su desempeño profesional durante el siguiente periodo, y algunas acciones que tendrá en cuenta para contribuir a que el Trainee pueda lograr un mejor desempeño."></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <label>Compromisos</label>
            <asp:TextBox ID="txtCompromiso" runat="server" Height="150px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
        </div>

    </div>
<br />
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="lblCodigo" runat="server" Text="0" Visible="False"></asp:Label>
        </div>
        <div class="col-md-4">
            <center>
            <button onclick="goBack()">Regresar</button>

            <script>
                function goBack() {
                    window.history.back();
                }
            </script>
        </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <br /><br />
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="lblResultado" runat="server" CssClass="EtiquetaUsuarioNombre" Font-Size="12pt"></asp:Label>
        </div>
        <div class="col-md-4">
            
        </div>
        <div class="col-md-4">
            <center>
           <asp:Image runat="server" ID="imgLeyenda" ImageUrl="~/imagenes/LeyendaDesempenio.png" CssClass="img-responsive"  ></asp:Image>
        </center>
        </div>
    </div>
     </ContentTemplate>
            <Triggers>    
              <%--  <asp:PostBackTrigger ControlID="btnGuardar"  />--%>
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


