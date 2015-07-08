<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                <h1>
                    Solicitud Reinscripción</h1>
             <%--   <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <div style="border-color: #FFCC66; background-color: #FFFFCC">
                    <h2>
                    El periodo de Reinscripciones ha FINALIZADO.
                    </h2>--%>
                </div>
            </td>
        </tr>
        </table>
    <br />
    <asp:Panel ID="Panel1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 200px">
                &nbsp;
            </td>
            <td colspan="2">
                Antes de continuar, conoce el proceso paso a paso <a href="Imagenes/PasoAPasoReInscripcion.jpg"
                    style="font-weight: 700">AQUÍ</a>
            </td>
        </tr>
        <tr>
            <td class="formLabel" style="width: 200px">
                &nbsp;
            </td>
            <td class="formLabel">
                &nbsp;
            </td>
            <td>
                Si no sabes tu CURP, consúltalo <a href="http://consultas.curp.gob.mx/CurpSP/" style="font-weight: 700"
                    target="_blank">AQUÍ</a>
            </td>
        </tr>
        <tr>
            <td class="formLabel" style="width: 200px">
                &nbsp;
            </td>
            <td class="formLabel">
                CURP:
            </td>
            <td>
                <asp:TextBox ID="txtCURP" runat="server" Width="200px" MaxLength="18" onBlur="javascript:this.value=this.value.toUpperCase();"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="txtCURP"
                    ErrorMessage="*" SetFocusOnError="True" ID="RequiredFieldValidator3"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="formLabel" style="width: 200px">
                &nbsp;
            </td>
            <td class="formLabel">
                Matricula:
            </td>
            <td>
                <asp:TextBox ID="txtMatricula" runat="server" Width="93px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="txtMatricula"
                    ErrorMessage="*" SetFocusOnError="True" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ValidationExpression="\d{10}" ForeColor="Red"
                    ControlToValidate="txtMatricula" ErrorMessage="*" SetFocusOnError="True" ID="RegularExpressionValidator2"></asp:RegularExpressionValidator>
                <asp:Button ID="btnAccesar" runat="server" Text="Accesar" Width="80px" OnClick="btnAccesar_Click" />
            </td>
        </tr>
        <tr>
            <td class="formLabel" style="width: 200px">
                &nbsp;
            </td>
            <td class="formLabel">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="formLabel" style="width: 200px">
                &nbsp;
            </td>
            <td class="formLabel">
                &nbsp;
            </td>
            <td>
                <strong>AVISO IMPORTANTE:</strong> Si tienes problemas para generar tu Ficha de
                Pago, o estás exonerado y la cuota de inscripción no te aparece en 0, favor de acudir
                al area de ingresos de tu plantel.
            </td>
        </tr>
        <tr>
            <td class="formLabel" style="width: 200px">
                &nbsp;
            </td>
            <td class="formLabel">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </asp:Panel>
    <br />
</asp:Content>
