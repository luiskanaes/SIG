<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="ReconocerPremios.aspx.cs" Inherits="RRHH_ReconocerPremios" %>
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
        .panel-primary {
  border-color: #57227a;
}
/** COLOR DE LAS LISTA*/
.panel-primary > .panel-heading {
  color: #ffffff;
  background-color: #57227a;
  border-color: #195183;
}

.panel-primary > .panel-heading + .panel-collapse .panel-body {
  border-top-color: #428bca;
}

.panel-primary > .panel-footer + .panel-collapse .panel-body {
  border-bottom-color: #428bca;
}
.panel-footer  {
  height :100px;
}
.panel-success {
  border-color: #d6e9c6;
}

.panel-success > .panel-heading {
  color: #468847;
  background-color: #dff0d8;
  border-color: #d6e9c6;
}

.panel-success > .panel-heading + .panel-collapse .panel-body {
  border-top-color: #d6e9c6;
}

.panel-success > .panel-footer + .panel-collapse .panel-body {
  border-bottom-color: #d6e9c6;
}

.panel-warning {
  border-color: #fbeed5;
}

.panel-warning > .panel-heading {
  color: #c09853;
  background-color: #fcf8e3;
  border-color: #fbeed5;
}

.panel-warning > .panel-heading + .panel-collapse .panel-body {
  border-top-color: #fbeed5;
}

.panel-warning > .panel-footer + .panel-collapse .panel-body {
  border-bottom-color: #fbeed5;
}

.panel-danger {
  border-color: #eed3d7;
}

.panel-danger > .panel-heading {
  color: #b94a48;
  background-color: #f2dede;
  border-color: #eed3d7;
}

.panel-danger > .panel-heading + .panel-collapse .panel-body {
  border-top-color: #eed3d7;
}

.panel-danger > .panel-footer + .panel-collapse .panel-body {
  border-bottom-color: #eed3d7;
}

.panel-info {
  border-color: #bce8f1;
}

.panel-info > .panel-heading {
  color: #3a87ad;
  background-color: #d9edf7;
  border-color: #bce8f1;
}

.panel-info > .panel-heading + .panel-collapse .panel-body {
  border-top-color: #bce8f1;
}

.panel-info > .panel-footer + .panel-collapse .panel-body {
  border-bottom-color: #bce8f1;
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <div class="shadowBox">
              
                    <div class="container">

                        <div class="row">
                            <section class="col-md-4">
                                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/imagenes/Bravo170.png" NavigateUrl="~/RRHH/ReconocerTalento.aspx"></asp:HyperLink>
                            </section>
                            <section class="col-md-4">
                            </section>
                            <section class="col-md-4">
                            </section>
                        </div>

                        <div class="row">
                        <section class="col-md-4">
                            <label class="EtiquetaNegrita">Mis Puntos Bravo</label>
                            <asp:TextBox ID="txtPunto" runat="server" Enabled="False"></asp:TextBox>
                        </section>
                        <section class="col-md-4">
                            <label class="EtiquetaNegrita">Puntos</label>
                            <asp:DropDownList ID="ddlProductos" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged" ></asp:DropDownList>
                          </section>
                        <section class="col-md-4">
                        </section>
                        </div>
                        <br /><br />
                        <div class="row">
                            <div class="col-lg-12 ">
                            <asp:ListView ID="ListView1" runat="server" DataKeyNames="idProducto,Puntos">
                                <ItemTemplate>

                                    <div class="col-lg-4">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <%# Eval("nombre")%>
                                            </div>
                                            <div class="panel-body">
                                                <center>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Img_url") %>' class="img-responsive"  Width="235px" Height="170px" />
                                                
                                                <asp:Button ID="btnPremio" runat="server" Text="Seleccionar" OnClick ="SeleccionarPremio" CommandArgument='<%# Eval("idProducto") %>'/>
                                                <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnPremio">
                                                        </cc1:ConfirmButtonExtender>
                                                        <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnPremio"
                                                            OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                                        </cc1:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                                Mensaje SSK
                                                            </div>
                                                            <div class="body">
                                                                ¿Deseas seleccionar el premio?
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnYesx" runat="server" Text="Sí" CssClass="yes" />
                                                                <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                                            </div>
                                                             </asp:Panel>
                                           </center>
                                            </div>

                                            <%--<div class="panel-footer">
                                                <%# Eval("descripcion")%>
                                            </div>--%>

                                        </div>
                                    </div>


                                </ItemTemplate>
                            </asp:ListView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <center>
                                     <label class="EtiquetaNegrita">Nota : "Las imágenes son referenciales y los productos están sujetos a disponibilidad de stock"</label>
                                </center>
                            </div>
                        </div>
                    </div>
               
   </div>
    
</asp:Content>




