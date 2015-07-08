<%@ Page Title="" Language="C#" MasterPageFile="~/MPPre.master" AutoEventWireup="true"
    CodeFile="SolicitudPV.aspx.cs" Inherits="SolicitudPV" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="False">
        <div style="text-align: center;">
            <br />
            <br />
            Tu información ya fue validada en el plantel, ya no es posible modificar los datos.<br />
            <asp:Button ID="btnReimprimir" runat="server" OnClick="btnReimprimir_Click" Text="Reimprimir solicitud" />
            <br />
            <br />
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
            <br />
            <br />
        </div>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlSolicitud" runat="server">
        <div style="color: #FFFFFF; background-color: #99CC00; font-size: 14pt; text-align: left;
            vertical-align: middle;">
            &nbsp; Favor de llenar la información de todas las pestañas y al finalizar da click
            en el botón Guardar al final de la página.
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                    Width="950px">
                    <asp:TabPanel runat="server" HeaderText="Datos Personales" ID="tabDatosPersonales">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr valign="top">
                                    <td>
                                        <table style="width: 400px">
                                            <tr>
                                                <td class="formLabel">
                                                    CURP:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCURP" runat="server" MaxLength="18" Width="200px" Enabled="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCURP" ErrorMessage="Datos personales: Escribe la Fecha de Nacimiento (DD/MM/AAAA)." ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Apellido Paterno:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApPaterno" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Apellido Materno:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApMaterno" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Nombre(s):
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNombre" ErrorMessage="Datos personales: Escribe la Fecha de Nacimiento (DD/MM/AAAA)." ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Género:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSexo" runat="server">
                                                        <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="ddlSexo"
                                                        ErrorMessage="Datos personales: Selecciona el Sexo." ForeColor="Red" Operator="NotEqual"
                                                        SetFocusOnError="True" ValueToCompare="-1">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Fecha de Nacimiento:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFecNac" runat="server" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFecNac" ErrorMessage="Datos personales: Escribe la Fecha de Nacimiento (DD/MM/AAAA)."
                                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtFecNac"
                                                        Text="Fecha inválida. Se espera formato DD/MM/AAAA" Font-Size="X-Small" ForeColor="Red"
                                                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" ValueToCompare="0"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Teléfono Local:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTelefono" ErrorMessage="Datos personales: Escribe el Numero de Teléfono."
                                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Celular:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCelular" runat="server" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCelular" ErrorMessage="Datos personales: Escribe el Numero de Celular."
                                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Correo Electrónico:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCorreo" runat="server" MaxLength="100" onBlur="javascript:this.value=this.value.toLowerCase();"
                                                        Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                                            runat="server" ControlToValidate="txtCorreo" ErrorMessage="Datos personales: Escribe el Correo electrónico."
                                                            ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCorreo"
                                                                ErrorMessage="Datos personales: Correo electrónico inválido" ForeColor="Red"
                                                                SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                            HeaderText="Favor de corregir los siguientes errores:" />
                                    </td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="formLabel">
                                                    Calle:
                                                </td>
                                                <td width="350px">
                                                    <asp:TextBox ID="txtDirCalle" runat="server" MaxLength="100" Width="250px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDirCalle" ErrorMessage="Datos personales: Escribe el Nombre de la Calle."
                                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Número:
                                                </td>
                                                <td width="350px">
                                                    <asp:TextBox ID="txtDirNumero" runat="server" MaxLength="50" Width="250px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDirNumero"
                                                        ErrorMessage="Datos personales: Escribe el Numero de la Casa." ForeColor="Red"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="350px">
                                                    Si no sabes tu código postal búscalo <a href="http://www.sepomex.gob.mx/ServiciosLinea/Paginas/ccpostales.aspx"
                                                        style="font-weight: 700" target="_blank">AQUÍ</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Código Postal:
                                                </td>
                                                <td width="350px">
                                                    <asp:Panel ID="pnlColonias" runat="server" DefaultButton="btnMostrarColonias">
                                                        <asp:TextBox ID="txtDirCP" runat="server" MaxLength="5" Width="100px"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDirCP" ErrorMessage="*"
                                                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="BuscarColonias"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDirCP"
                                                                ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\d{5}"
                                                                ValidationGroup="BuscarColonias"></asp:RegularExpressionValidator><asp:Button ID="btnMostrarColonias"
                                                                    runat="server" OnClick="btnMostrarColonias_Click" Text="Mostrar Colonias" ValidationGroup="BuscarColonias"
                                                                    Width="120px" /></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Colonia/Localidad:
                                                </td>
                                                <td width="350px">
                                                    <asp:DropDownList ID="ddlColonia" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlColonia_SelectedIndexChanged" Width="300px">
                                                        <asp:ListItem Value="-1">Escribe el código postal y da click en Mostrar</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlColonia"
                                                        ErrorMessage="Datos personales: Selecciona la colonia/localidad." ForeColor="Red"
                                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                    <br />
                                                    <asp:TextBox ID="txtDirColonia" runat="server" Visible="False" Width="295px" MaxLength="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDirColonia"
                                                        ErrorMessage="Datos personales: Escribe el nombre de la colonia." ForeColor="Red"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="txtDirColonia"
                                                        ErrorMessage="Datos personales: Escribe el nombre de la colonia." ForeColor="Red"
                                                        Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Escribe aquí el nombre de la colonia">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Municipio:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMunicipio" runat="server" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator18" runat="server" 
                                                        ControlToValidate="ddlMunicipio" 
                                                        ErrorMessage="Datos personales: Selecciona el Municipio." ForeColor="Red" 
                                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" 
                                                        ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Estado:</td>
                                                <td width="350px">
                                                    <asp:TextBox ID="txtEstado" runat="server" MaxLength="200" Width="295px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtEstado" ErrorMessage="Datos personales: Escribe el nombre de la colonia." ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    &nbsp;</td>
                                                <td width="350px">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    &nbsp;</td>
                                                <td width="350px" style="text-align: right">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    &nbsp;</td>
                                                <td width="350px">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    &nbsp;</td>
                                                <td width="350px" style="text-align: right">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="color: #FFFFFF; background-color: #99CC00; font-size: 14pt; text-align: right;
            vertical-align: middle;">
            Despues de haber llenado todas las pestañas da CLICK AQUÍ:
            <asp:Button ID="btnGuardar0" runat="server" OnClick="btnGuardar_Click" 
                Text="Guardar e Imprimir Solicitud" />
            &nbsp;<br />
            <br />
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
</asp:Content>
