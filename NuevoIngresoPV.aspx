<%@ Page Title="" Language="C#" MasterPageFile="~/MPPre.master" AutoEventWireup="true"
    CodeFile="NuevoIngresoPV.aspx.cs" Inherits="NuevoIngresoPV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <h1>
                            Solicitud de Inscripción a Plantel Virtual</h1>
                        <%--   <div style="border: thin solid #800000; background-color: #FFFFCC;">
                            <br />
                            Esta página estará disponible para que te registres a partir de el Lunes 17 de 
                            Febrero a las 8:00 AM<br /> Gracias.<br /> <br /> </div><br />--%>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            &nbsp;
                        </td>
                        <td>
                            Si no sabes tu CURP, puedes consultarlo <a href="http://consultas.curp.gob.mx/CurpSP/"
                                style="font-weight: 700" target="_blank">AQUÍ</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            &nbsp;
                        </td>
                        <td>
                            Si eres <strong>Extranjero</strong> y no cuentas aún con un CURP oficial, acude
                            a control escolar del plantel al que deseas ingresar.
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            CURP:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCURP" runat="server" AutoPostBack="True" MaxLength="18" onBlur="javascript:this.value=this.value.toUpperCase();"
                               Width="200px" ontextchanged="txtCURP_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCURP"
                                ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Accesar"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCURP"
                                Display="Dynamic" ErrorMessage="CURP inválido" ForeColor="Red" ValidationExpression="^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$"
                                ValidationGroup="Accesar"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnAccesar" runat="server" class="imBtn" OnClick="btnAccesar_Click"
                                Style="font-weight: 700" Text="Accesar" ValidationGroup="Accesar" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 224px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
