<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MPAdmin.master" AutoEventWireup="true" CodeFile="DesempenioIntroduccion.aspx.cs" Inherits="RRHH_DesempenioIntroduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        input[type="submit"] {
            padding: 5px 15px;
            background: #EEAA00;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <center>

                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/Eval_desempeño.png" OnClick="ImageButton1_Click" />
            </center>
        </div>
       
    </div>
       <div class="row">
        <div class="col-md-12">
            <b>INSTRUCCIONES</b><br /><br />
            Te damos la bienvenida al proceso de Gestión del Desempeño. Esta evaluación está divida en 2 partes:<br /><br />

            <b>1.    Objetivos</b>
            Para poder fijar tus objetivos junto con tu jefe directo, debes considerar la metodología SMART y asegurarte de que esté alineada a la estrategia de la organización. Recuerda, durante el año podrás añadir, eliminar o editar alguno de ellos. 
            <br /><br />
            <b>2.    Competencias </b>
            Este campo podrá ser revisado en las etapas de revisión de medio año y evaluación final. Aquí visualizarás el nivel de exigencia esperada de las competencias SSK, tanto cardinales como específicas. Recuerda que te puedes apoyar en el diccionario de competencias SSK que encuentras en la intranet.
            <br /><br />
            Si tienes dudas, puedes comunicarte con nosotros escribiendo a: <a href ="mailto:desarrollodeltalento@ssk.com.pe">desarrollodeltalento@ssk.com.pe</a>

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <center>
<asp:Button runat="server" Text="Iniciar" ID="btnIniciar" OnClick="btnIniciar_Click"></asp:Button>
            </center>
        </div>
        <div class="col-md-4">
        </div>
    </div>
</asp:Content>

