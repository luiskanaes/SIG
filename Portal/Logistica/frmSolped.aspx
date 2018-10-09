<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="frmSolped.aspx.cs" Inherits="Logistica_frmSolped" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
<meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .custom-combobox {
            position: relative;
            display: inline-block;
            width: 100%;
            z-index :1;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
            width: 100%;
            /* support: IE7 */
            *height: 1.7em;
            *top: 0.1em;
             z-index :1;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 0.3em;
            width: 50%;
        }


        /*Demo fix*/
        .custom-combobox a {
            height: 34px;
            margin-top: -6px;
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

</script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="udpHojaGestion" runat="server">
<ContentTemplate>
    <script type="text/javascript" language="javascript">
         var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="../js/jsUpdateProgress.js"></script>



    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">Consultar</label>

            <asp:DropDownList ID="ddlCodigo" runat="server" CssClass="ddl "></asp:DropDownList>

        </div>
        <div class="col-md-4">
            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/imagenes/Buscar.png" Width="50px" OnClick="btnBuscar_Click" ToolTip="Buscar Pedido" />
            <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/imagenes/registro2.png" OnClick="btnNuevo_Click" Width="50px" ToolTip="Nuevo Pedido" />
            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/imagenes/Eliminar.png" OnClick="btnEliminar_Click" Width="50px" ToolTip="Eliminar Productos Seleccionado" />
            <cc1:ConfirmButtonExtender ID="cbDelete" runat="server" DisplayModalPopupID="mpeDelete" TargetControlID="btnEliminar"></cc1:ConfirmButtonExtender>
            <cc1:ModalPopupExtender ID="mpeDelete" runat="server" PopupControlID="pnlPopupDelete" TargetControlID="btnEliminar"
                OkControlID="btnSi" CancelControlID="btnNot" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopupDelete" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="header">
                    Advertencia
                </div>
                <div class="body">
                    Desea Eliminar Pedido de SOLPED, Continuar?
                </div>
                <div class="footer" align="right">
                    <asp:Button ID="btnSi" runat="server" Text="Si" CssClass="yes" />
                    <asp:Button ID="btnNot" runat="server" Text="No" CssClass="no" />
                </div>
            </asp:Panel>
            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/imagenes/Excel.png" OnClick="btnExcel_Click" ToolTip="Descarga" Width="50px" />
            <asp:ImageButton ID="btnMateriales" runat="server" ImageUrl="~/imagenes/Indicadores_4.png" OnClick="btnMateriales_Click" ToolTip="Ver Materiales" Width="50px" Visible="False" />

        </div>

    </div>
    <div class="row">
        <div class="col-md-4">
            <label class="EtiquetaNegrita">IMPUTACION</label>
            <asp:DropDownList ID="ddlImputacion" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlImputacion_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">SOCIEDAD</label>
            <asp:DropDownList ID="ddlSociedad" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlSociedad_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">OBRA</label>
            <asp:DropDownList ID="ddlObra" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlObra_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <label class="EtiquetaNegrita">FECHA</label>
            <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
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

            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha" PopupButtonID="ImgBntCalc" Format="dd/MM/yyyy" />

        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">VALOR</label>
            <asp:TextBox ID="txtValor" runat="server" onkeydown="return jsDecimals(event);" placeholder="Valor"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">MONEDA</label>
            <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="ddl">
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <label class="EtiquetaNegrita">CENTRO</label>
            <asp:DropDownList ID="ddlCentro" runat="server" CssClass="ddl" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">GRUPO COMPRA</label>
            <asp:DropDownList ID="ddlGpoCompra" runat="server" CssClass="ddl">
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label class="EtiquetaNegrita">CENTRO COSTE</label>
            <asp:DropDownList ID="ddlCoste" runat="server" CssClass="ddl" AutoPostBack="True">
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <br />
        <div class="col-md-12">
            <center>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                    </center>
        </div>

    </div>
    
        <table class="style1">


            <tr>
                <td colspan="5">
                    <asp:GridView ID="GridPedidos" runat="server" AutoGenerateColumns="False"
                        Width="100%" CssClass="mGridAzulLineas">

                        <Columns>
                            <%--<asp:BoundField DataField="Row" HeaderText="N°" SortExpression="Row" />--%>
                            <asp:BoundField DataField="IMPUTACION" HeaderText="Imp" SortExpression="IMPUTACION" />
                            <asp:BoundField DataField="MATERIAL" HeaderText="Material" SortExpression="MATERIAL" />
                            <asp:BoundField DataField="MATERIAL_TEXTO" HeaderText="Descripcion" SortExpression="MATERIAL_TEXTO">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CANTIDAD" HeaderText="Cant." SortExpression="CANTIDAD" />
                            <asp:BoundField DataField="UNIDAD" HeaderText="Und" SortExpression="UNIDAD" />
                            <asp:BoundField DataField="VALOR" HeaderText="Valor" SortExpression="VALOR" />
                            <asp:BoundField DataField="MONEDA" HeaderText="Moneda" SortExpression="MONEDA" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                            <asp:BoundField DataField="GRUPO" HeaderText="Grupo" SortExpression="GRUPO" />
                            <asp:BoundField DataField="CENTRO" HeaderText="Centro" SortExpression="CENTRO" />
                            <asp:BoundField DataField="SOLICITANTE" HeaderText="Solicitante" SortExpression="SOLICITANTE" />
                            <asp:BoundField DataField="GR_COMPRA" HeaderText="Compra" SortExpression="GR_COMPRA" />
                            <asp:BoundField DataField="CUENTA_MAYOR" HeaderText="Cuenta Mayor" SortExpression="CUENTA_MAYOR" />
                            <asp:BoundField DataField="PEP" HeaderText="PEP" SortExpression="PEP">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CENTRO_COSTE" HeaderText="Centro Coste" SortExpression="CENTRO_COSTE" />
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("IDE_PEDIDO") %>' ImageUrl="~/imagenes/Ico_delete.png" OnClick="EliminarSolped" />
                                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnDelete"></cc1:ConfirmButtonExtender>
                                    <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnDelete"
                                        OkControlID="btnYesx" CancelControlID="btnNox" BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                        <div class="header">
                                            Advertencia
                                        </div>
                                        <div class="body">
                                            Desea Eliminar Registro?
                                        </div>
                                        <div class="footer" align="right">
                                            <asp:Button ID="btnYesx" runat="server" Text="Si" CssClass="yes" />
                                            <asp:Button ID="btnNox" runat="server" Text="No" CssClass="no" />
                                        </div>
                                    </asp:Panel>
                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td style="vertical-align: middle; text-align: center">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Panel ID="PanelPedidos" runat="server" Width="100%" Visible="False">
                        <table class="style1">
                            <tr>
                                <td width="10%"></td>
                                <td width="40%" colspan="2">
                                    <label class="EtiquetaNegrita">BUSCAR MATERIALES</label>
                                </td>
                                <td width="40%"></td>
                                <td width="10%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="vertical-align: middle; text-align: center;" colspan="3" align="center">

                                    <table id="tbl_rol" class="style1">
                                        <thead>
                                            <tr align="center" style="text-align: center">
                                                <th width="7%" style="text-align: center">
                                                    <asp:TextBox ID="txtNro" runat="server" placeholder="N°" ReadOnly="True"></asp:TextBox></th>
                                                <th width="10%" style="text-align: center">
                                                    <asp:TextBox ID="txtMaterial" runat="server" placeholder="Material" AutoPostBack="True" OnTextChanged="txtMaterial_TextChanged"></asp:TextBox></th>
                                                <th width="40%" style="text-align: center">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" placeholder="Descripcion" AutoPostBack="True" OnTextChanged="txtDescripcion_TextChanged"></asp:TextBox></th>
                                                <th width="8%" style="text-align: center">
                                                    <asp:TextBox ID="txtUnidad" runat="server" placeholder="Unidad" AutoPostBack="True" OnTextChanged="txtUnidad_TextChanged"></asp:TextBox></th>
                                                <th width="10%" style="text-align: center">
                                                    <asp:TextBox ID="txtGrupo" runat="server" placeholder="Articulo" AutoPostBack="True" OnTextChanged="txtGrupo_TextChanged"></asp:TextBox></th>
                                                <th width="5%" style="text-align: center"></th>
                                                <th width="15%" style="text-align: center">
                                                    <asp:TextBox ID="txtPep" runat="server" placeholder="Pep" AutoPostBack="True" OnTextChanged="txtPep_TextChanged"></asp:TextBox></th>
                                                <th width="20%"></th>
                                                <th width="20%">
                                                    <asp:ImageButton ID="btnAgregarAll" runat="server" ImageUrl="~/imagenes/Item_Add.png" OnClick="btnAgregarAll_Click" ToolTip="Carga Masiva" />
                                                </th>
                                            </tr>
                                            <tr align="center" style="background-color: #195183; color: White; text-align: center; font-family: Arial, sans-serif; font-size: 13px">
                                                <th width="7%" style="text-align: center">N°</th>
                                                <th width="10%" style="text-align: center">Material</th>
                                                <th width="40%" style="text-align: center">Descripcion</th>
                                                <th width="8%" style="text-align: center">Unidad</th>
                                                <th width="10%" style="text-align: center">Grupo Articulo</th>
                                                <th width="5%" style="text-align: center"></th>
                                                <th width="15%" style="text-align: center">PEP</th>
                                                <th width="20%" style="text-align: center">Cantidad</th>
                                                <th width="20%" style="text-align: center"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView runat="server" ID="lstRol" DataKeyNames="IDE_MATERIAL">
                                                <ItemTemplate>
                                                    <tr style="font-family: Arial, sans-serif; font-size: 12px; border: double; border-width: 1px;">
                                                        <td>
                                                            <%#Eval("Row") %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMaterial" runat="server" Text='<%# Eval("IDE_MATERIAL") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("DES_MATERIAL") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("UNIDAD") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("GRUPO_ARTICULO") %>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnAddPep" runat="server" ImageUrl="~/imagenes/PencilAdd.png" OnClick="RegistrarPep" CommandArgument='<%# Eval("Row") %>' />
                                                        </td>
                                                        <td>
                                                            <%-- <%#Eval("PEP") %>--%>
                                                            <asp:DropDownList ID="ddlPep" runat="server" CssClass="ddl"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCantidad" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Imagenes/Item_Add.png" CommandArgument='<%# Eval("Row") %>' OnClick="Seleccionar" ToolTip="Agregar Producto" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>
                                    </table>


                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="gvwExportar" runat="server" AutoGenerateColumns="False" Width="100%" Visible="False">

                        <Columns>
                            <asp:BoundField DataField="Row" HeaderText="N°" SortExpression="Row" />
                            <asp:BoundField DataField="IMPUTACION" HeaderText="Imputacion" SortExpression="IMPUTACION" />
                            <asp:BoundField DataField="MATERIAL" HeaderText="Material" SortExpression="MATERIAL" />
                            <asp:BoundField DataField="MATERIAL_TEXTO" HeaderText="Descripcion" SortExpression="MATERIAL_TEXTO">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CANTIDAD" HeaderText="Cant." SortExpression="CANTIDAD" />
                            <asp:BoundField DataField="UNIDAD" HeaderText="Und" SortExpression="UNIDAD" />
                            <asp:BoundField DataField="VALOR" HeaderText="Valor" SortExpression="VALOR" />
                            <asp:BoundField DataField="MONEDA" HeaderText="Moneda" SortExpression="MONEDA" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                            <asp:BoundField DataField="GRUPO" HeaderText="Grupo" SortExpression="GRUPO" />
                            <asp:BoundField DataField="CENTRO" HeaderText="Centro" SortExpression="CENTRO" />
                            <asp:BoundField DataField="SOLICITANTE" HeaderText="Solicitante" SortExpression="SOLICITANTE" />
                            <asp:BoundField DataField="GR_COMPRA" HeaderText="Gpo Compra" SortExpression="GR_COMPRA" />
                            <asp:BoundField DataField="CUENTA_MAYOR" HeaderText="Cuenta Mayor" SortExpression="CUENTA_MAYOR" />
                            <asp:BoundField DataField="PEP" HeaderText="PEP" SortExpression="PEP" />
                            <asp:BoundField DataField="CENTRO_COSTE" HeaderText="Centro Coste" SortExpression="CENTRO_COSTE" />

                        </Columns>
                        <HeaderStyle BackColor="#eeeeee" />
                        <SelectedRowStyle BackColor="Yellow" />
                        <SortedAscendingHeaderStyle BackColor="Blue" ForeColor="White"
                            CssClass="AscHeader" />
                        <SortedAscendingCellStyle BackColor="LightBlue" />
                        <SortedDescendingHeaderStyle BackColor="Green" ForeColor="White"
                            CssClass="DescHeader" />
                        <SortedDescendingCellStyle BackColor="LightGreen" />
                    </asp:GridView>


                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    

                </td>
            </tr>
        </table>
        <br />


    <asp:HiddenField ID="HidRegistropep" runat="server" />
    <cc1:ModalPopupExtender ID="ModalRegistroPep"
        runat="server"

             BackgroundCssClass="modalBackground"
        PopupControlID="pnlPopupPep"
        PopupDragHandleControlID="pnlPopupPep"
        TargetControlID="HidRegistropep">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopupPep" runat="server" CssClass="modalPopup" Width="40%">
       
        <div class="row">
        <div class="col-md-12">
            <center>
              <label class="headerText">Agregar Pep</label>
                </center>
            <hr />
        </div>
        
    </div>
        <div class="row">
        <div class="col-md-4">
            
        </div>
        <div class="col-md-4">
           <center>
                <label class="EtiquetaNegrita">Material </label>
                <asp:Label ID="lblIdMaterial" runat="server" Text="Label" ></asp:Label>
           </center>
        </div>
        <div class="col-md-4">
            
        </div>
    </div>
        <div class="row">

            <div class="col-md-4">
               
            </div>
            <div class="col-md-4">
                

                <asp:DropDownList ID="ddl" runat="server" Width="100%" CssClass="ddl">
                </asp:DropDownList>
              
            </div>
            <div class="col-md-4">
            </div>

        </div>

            <br />
            <div class="row">
                <div class="col-md-12">
                    <center>
                        <asp:Button ID="Button2" runat="server" Text="Cancelar" />
                    <asp:Button ID="Button1" runat="server" Text="Guardar" OnClick="Button1_Click" />
                    </center>
                </div>
                
        </div>
            <br />
    </asp:Panel>

    
</ContentTemplate>
            <Triggers>    
                <%--<asp:AsyncPostBackTrigger  ControlID="btnIngreso"/>--%>
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


