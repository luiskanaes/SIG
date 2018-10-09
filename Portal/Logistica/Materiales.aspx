<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPopup.master" AutoEventWireup="true" CodeFile="Materiales.aspx.cs" Inherits="Logistica_Materiales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
        <tr>
             <th width="20%"></td>
             <th width="20%"></td>
             <th width="20%"></td>
             <th width="20%"></td>
             <th width="20%"></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td> <label class="EtiquetaNegrita">Grupo Materiales</label>
                <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView  ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" >
                    <Columns>
                        <asp:BoundField DataField="Row" HeaderText="N°" SortExpression="Row" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IDE_MATERIAL" HeaderText="Material" SortExpression="IDE_MATERIAL" />
                        <asp:BoundField DataField="DES_MATERIAL" HeaderText="Descripcion" SortExpression="DES_MATERIAL" />
                        <asp:BoundField DataField="UNIDAD" HeaderText="Unidad" SortExpression="UNIDAD" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GRUPO_ARTICULO" HeaderText="Grupo Articulo" SortExpression="GRUPO_ARTICULO" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CLASE_COSTE" HeaderText="Clase Coste" SortExpression="CLASE_COSTE" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PEP" HeaderText="PEP" SortExpression="PEP" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                <HeaderStyle BackColor="#eeeeee" />
                <SelectedRowStyle  BackColor="Yellow"  />
                <SortedAscendingHeaderStyle BackColor="Blue" ForeColor="White"   
                CssClass="AscHeader"/>
                <SortedAscendingCellStyle BackColor="LightBlue" />
                <SortedDescendingHeaderStyle BackColor="Green" ForeColor="White"  
                CssClass="DescHeader" />
                <SortedDescendingCellStyle BackColor="LightGreen"/>
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
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

