 <%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true"
    CodeFile="ImprimirFichaDePago.aspx.cs" Inherits="ImprimirFichaDePago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <table style="width: 100%">
        <tr>
            <td style="width: 133px">
                &nbsp;
            </td>
            <td class="formLabel" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 133px">
                &nbsp;</td>
            <td class="formLabel">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px">
                &nbsp;
            </td>
            <td class="formLabel">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
