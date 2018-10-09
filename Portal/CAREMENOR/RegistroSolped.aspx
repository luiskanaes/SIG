<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="RegistroSolped.aspx.cs" Inherits="CAREMENOR_RegistroSolped" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    

    <style type="text/css">
        .Marco {
            overflow: auto;
        }

            .Marco .Grid {
                width: 105%;
            }


        .Ancho {
            width: 35%;
            padding: 10px;
            background-color: #ffffff;
            border: 1px solid #999999;
            border: 1px solid rgba(0, 0, 0, 0.2);
            border-radius: 6px;
            outline: none;
            -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, 0.5);
            box-shadow: 0 3px 9px rgba(0, 0, 0, 0.5);
            background-clip: padding-box;
        }

        @media only screen and (max-width: 500px) {
            .Ancho {
                width: 95%;
            }
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
            title: 'Mensaje del Sistema ',
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
  
       
    </script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            
            <ContentTemplate>

  
    <asp:Panel ID="Panel1" runat="server" >
        <div class="row">
            <div class="col-md-12">
               
                <asp:Label runat="server" Font-Bold="True" ForeColor="Black" ID="lblMensaje">Codigo Solped</asp:Label>
                <asp:TextBox runat="server" ID="txtSolped" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtSolped" CssClass="errorMessage"
                    ErrorMessage="Ingresar codigo" ValidationGroup="Validar" />

                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSolped"
                    ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{10,10}$" runat="server"
                    ErrorMessage="falta completar los 10 digitos requeridos" CssClass="errorMessage" ValidationGroup="Validar">

                </asp:RegularExpressionValidator><br />
                    <asp:Label runat="server" ID="lblMsg" CssClass="errorMessage"></asp:Label>
               
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                <asp:GridView ID="GridReq" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="Requ_Numero,Reqd_CodLinea,Reqs_Correlativo" OnRowDataBound="GridReq_RowDataBound" OnRowCreated="GridReq_RowCreated">

                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="#" SortExpression="Row" />
                        <asp:BoundField DataField="Reqs_CodigoCompleto" HeaderText="Cod.Requerimiento" SortExpression="Reqs_CodigoCompleto" />
                        <asp:BoundField DataField="D_SOLPED" HeaderText="Solped" SortExpression="D_SOLPED" />
                        <asp:TemplateField HeaderText="Pos.Alq.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPosAlquiler" runat="server" Text='<%# Eval("D_SOLPED_ALQUILER") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pos.Mov.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPosMov" runat="server" Text='<%# Eval("D_SOLPED_MOVIMIENTO") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubFamilia">
                            <ItemTemplate>
                                 <asp:Label ID="lblFamilia" runat="server" Text='<%# Eval("N_Reqs_Familia") %>' Visible="false"  onkeypress="return event.keyCode!=13"/>
                                 <asp:Label ID="lblSubFamilia" runat="server" Text='<%# Eval("N_Reqs_SubFamilia") %>' Visible="false"  onkeypress="return event.keyCode!=13"/>
                            <asp:DropDownList ID="ddlSubFamilia" runat="server" CssClass="ddlAjustado"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubFamilia_SelectedIndexChanged"> </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                 <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("N_Reqs_Marca") %>' Visible="false"  onkeypress="return event.keyCode!=13"/>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="ddlAjustado" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" > </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modelo">
                            <ItemTemplate>
                                 <asp:Label ID="lblModelo" runat="server" Text='<%# Eval("N_Reqs_Modelo") %>' Visible="false"  onkeypress="return event.keyCode!=13"/>
                            <asp:DropDownList ID="ddlModelo" runat="server" CssClass="ddlAjustado" > </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Capacidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCapacidad" runat="server" Text='<%# Eval("N_Reqs_Capacidad") %>' Width="200"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
               </div>
            </div>
        </div>

        <div class="row">
            

            <div class="col-md-12">
                <asp:Label ID="lblRequ_Numero" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblReqd_CodLinea" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblReqs_Correlativo" runat="server" Visible="false"></asp:Label>
                <br />
                
                <asp:Button runat="server" Text="Cancelar" ID="btnCancel" OnClick="btnCancel_Click" CausesValidation="False"  ></asp:Button>
                <asp:Button runat="server" Text="Guardar" ID="btnGuardar" validationgroup="Validar" OnClick="btnGuardar_Click" ></asp:Button>
                
            </div>
        </div>

    </asp:Panel>
                   </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

