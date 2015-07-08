<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center; padding: 50px 0 0 100px;">
        <table border="0" cellpadding="0" style="width: 305px">
            <tr>
                <td align="center" colspan="2" style="font-size: x-large">
                    <strong>Acceso </strong>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                        style="font-weight: 700">Usuario:</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="UserName" runat="server" Width="130px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px">
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                        style="font-weight: 700">Contraseña:</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="130px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="color: red; text-align: center;">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px">
                </td>
                <td style="text-align: left">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" OnClick="LoginButton_Click"
                        Text="Accesar" ValidationGroup="Login1" Width="130px" />
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
