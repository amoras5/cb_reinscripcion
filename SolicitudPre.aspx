<%@ Page Title="" Language="C#" MasterPageFile="~/MPPre.master" AutoEventWireup="true"
    CodeFile="SolicitudPre.aspx.cs" Inherits="SolicitudPre" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="False">
        <div style="text-align: center;">
            <br />
            <br />
            <asp:Label ID="lblMen" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnReimprimir" runat="server" OnClick="btnReimprimir_Click" 
                Text="Reimprimir solicitud" CausesValidation="False" />
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
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="950px"
                    Height="500px">
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Apellido Paterno:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApPaterno" runat="server" MaxLength="50" Width="200px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Apellido Materno:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApMaterno" runat="server" MaxLength="50" Width="200px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Nombre(s):
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="200px" Enabled="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                                                        ControlToValidate="txtNombre" 
                                                        ErrorMessage="Datos personales: Escribe tu nombre o nombres." ForeColor="Red" 
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Género:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSexo" runat="server" Enabled="False">
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
                                                    <asp:TextBox ID="txtFecNac" runat="server" MaxLength="10" Enabled="False"></asp:TextBox><asp:RequiredFieldValidator
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
                                                    Teléfono:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="20"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel">
                                                    Celular:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCelular" runat="server" MaxLength="20" Width="128px"></asp:TextBox><asp:RequiredFieldValidator
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
                                                    &nbsp;
                                                </td>
                                                <td width="350px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Estado Civil:
                                                </td>
                                                <td width="350px">
                                                    <asp:DropDownList ID="ddlEstadoCivil" runat="server" Width="200px">
                                                        <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                        <asp:ListItem Value="0">Soltero</asp:ListItem>
                                                        <asp:ListItem Value="1">Casado</asp:ListItem>
                                                        <asp:ListItem Value="2">Viudo</asp:ListItem>
                                                        <asp:ListItem Value="3">Unión Libre</asp:ListItem>
                                                        <asp:ListItem Value="4">Divorciado</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlEstadoCivil"
                                                        ErrorMessage="Datos personales: Selecciona el Estado Civil." ForeColor="Red"
                                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Ocupación:
                                                </td>
                                                <td width="350px">
                                                    <asp:DropDownList ID="ddlOcupacion" runat="server" Width="200px">
                                                        <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                        <asp:ListItem Value="0">Estudiante</asp:ListItem>
                                                        <asp:ListItem Value="1">Empleado</asp:ListItem>
                                                        <asp:ListItem Value="2">Negocio Propio</asp:ListItem>
                                                        <asp:ListItem Value="3">Jornalero</asp:ListItem>
                                                        <asp:ListItem Value="4">Otros</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlOcupacion"
                                                        ErrorMessage="Datos personales: Selecciona la Ocupación." ForeColor="Red" Operator="GreaterThanEqual"
                                                        SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Tipo de Beca:
                                                </td>
                                                <td width="350px">
                                                    <asp:DropDownList ID="ddlTipoBeca" runat="server" Width="200px">
                                                        <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                        <asp:ListItem Value="0">Ninguno</asp:ListItem>
                                                        <asp:ListItem Value="1">Prospera</asp:ListItem>
                                                        <asp:ListItem Value="2">SEPYC</asp:ListItem>
                                                        <asp:ListItem Value="3">SEDESOL</asp:ListItem>
                                                        <asp:ListItem Value="4">OTROS</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlTipoBeca"
                                                        ErrorMessage="Datos personales: Selecciona el Tipo de Beca." ForeColor="Red"
                                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Tipo de Sangre:
                                                </td>
                                                <td width="350px">
                                                    <asp:DropDownList ID="ddlTipoSangre" runat="server" Width="200px">
                                                        <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                        <asp:ListItem Value="10">O+</asp:ListItem>
                                                        <asp:ListItem Value="1">O-</asp:ListItem>
                                                        <asp:ListItem Value="2">A-</asp:ListItem>
                                                        <asp:ListItem Value="3">A+</asp:ListItem>
                                                        <asp:ListItem Value="4">B-</asp:ListItem>
                                                        <asp:ListItem Value="5">B+</asp:ListItem>
                                                        <asp:ListItem Value="6">AB-</asp:ListItem>
                                                        <asp:ListItem Value="7">AB+</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddlTipoSangre"
                                                        ErrorMessage="Datos personales: Selecciona el Tipo de Sangre." ForeColor="Red"
                                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Alergias:
                                                </td>
                                                <td width="350px">
                                                    <asp:CheckBox ID="chkAlergias" runat="server" AutoPostBack="True" OnCheckedChanged="chkAlergias_CheckedChanged" /><asp:TextBox
                                                        ID="txtAlergias" runat="server" Width="180px" Visible="False" MaxLength="50"></asp:TextBox><asp:CompareValidator
                                                            ID="CompareValidator1" runat="server" ControlToValidate="txtAlergias" ErrorMessage="Datos personales: Alergias, Explique."
                                                            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="EXPLIQUE:">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Enf. Crónico Degenerativas:
                                                </td>
                                                <td width="350px">
                                                    <asp:CheckBox ID="chkEnfCronicas" runat="server" AutoPostBack="True" OnCheckedChanged="chkEnfCronicas_CheckedChanged" />
                                                    <asp:TextBox ID="txtEnfCronicas" runat="server" MaxLength="50" Visible="False" Width="180px"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtEnfCronicas"
                                                        ErrorMessage="Datos personales: Enfermedades crónico degenerativas, Explique."
                                                        ForeColor="Red" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="EXPLIQUE:">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Algún otro padecimiento o discapacidad:
                                                </td>
                                                <td width="350px">
                                                    <asp:CheckBox ID="chkCapDiferentes" runat="server" AutoPostBack="True" OnCheckedChanged="chkCapDiferentes_CheckedChanged" /><asp:TextBox
                                                        ID="txtCapDiferentes" runat="server" Width="180px" Visible="False" MaxLength="50"></asp:TextBox><asp:CompareValidator
                                                            ID="CompareValidator18" runat="server" ControlToValidate="txtCapDiferentes" ErrorMessage="Datos personales: Capacidades diferentes, Explique."
                                                            ForeColor="Red" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="EXPLIQUE:">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formLabel" width="150">
                                                    Derechohabiente:
                                                </td>
                                                <td width="350px">
                                                    <asp:CheckBox ID="chkIMSS" runat="server" Text="IMSS"></asp:CheckBox>
                                                    <br />
                                                    <asp:CheckBox ID="chkSeguro" runat="server" Text="Seguro Popular"></asp:CheckBox>
                                                    <br />
                                                    <asp:CheckBox ID="chkISSSTE" runat="server" Text="ISSSTE"></asp:CheckBox>
                                                    <br />
                                                    <asp:CheckBox ID="chkPrivado" runat="server" Text="Privado"></asp:CheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabIntereses" runat="server" HeaderText="Intereses">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 101px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 200px">
                                        <asp:DropDownList ID="ddlInt1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInt1_SelectedIndexChanged"
                                            Width="200px">
                                            <asp:ListItem Selected="True" Value="0">&lt;--Seleccionar--&gt;</asp:ListItem>
                                            <asp:ListItem Value="1">Deportes</asp:ListItem>
                                            <asp:ListItem Value="2">Artisticas</asp:ListItem>
                                            <asp:ListItem Value="3">Medios Comunicación</asp:ListItem>
                                            <asp:ListItem Value="4">Tecnológicas</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="ddlIntDet1" runat="server" Width="200px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="-1">&lt;&lt; Selecciona categoría</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlIntDet1"
                                            ErrorMessage="Intereses: Selecciona un Interes." ForeColor="Red" Operator="GreaterThanEqual"
                                            SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        Si es otro, especifique:<asp:TextBox ID="txtIntOtro1" runat="server" MaxLength="50"
                                            Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 101px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 200px">
                                        <asp:DropDownList ID="ddlInt2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInt2_SelectedIndexChanged"
                                            Width="200px">
                                            <asp:ListItem Selected="True" Value="0">&lt;--Seleccionar--&gt;</asp:ListItem>
                                            <asp:ListItem Value="1">Deportes</asp:ListItem>
                                            <asp:ListItem Value="2">Artisticas</asp:ListItem>
                                            <asp:ListItem Value="3">Medios Comunicación</asp:ListItem>
                                            <asp:ListItem Value="4">Tecnológicas</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="ddlIntDet2" runat="server" Width="200px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="-1">&lt;&lt; Selecciona categoría</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlIntDet2"
                                            ErrorMessage="Intereses: Selecciona un Interes." ForeColor="Red" Operator="GreaterThanEqual"
                                            SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        Si es otro, especifique:<asp:TextBox ID="txtIntOtro2" runat="server" MaxLength="50"
                                            Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 101px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 200px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 219px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabEstudio" runat="server" HeaderText="Estudio Socioeconómico">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="formLabel">
                                        Vives con:
                                    </td>
                                    <td style="width: 300px">
                                        <asp:RadioButtonList ID="rblViveCon" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rblViveCon_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="2">Padre</asp:ListItem>
                                            <asp:ListItem Value="1">Madre</asp:ListItem>
                                            <asp:ListItem Value="3">Ambos</asp:ListItem>
                                            <asp:ListItem Value="4">Tutor</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rblViveCon"
                                            ErrorMessage="Estudio socioeconómico: Selecciona con quien vives." ForeColor="Red"
                                            SetFocusOnError="True">Selecciona una opción</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="450">
                                        <asp:Panel ID="pnlViveConTutor" runat="server">
                                            Si es con Tutor, especifique:
                                            <asp:DropDownList ID="ddlViveConTutor" runat="server" Width="130px">
                                                <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                                <asp:ListItem Value="1">Abuelo</asp:ListItem>
                                                <asp:ListItem Value="2">Abuela</asp:ListItem>
                                                <asp:ListItem Value="3">Tio</asp:ListItem>
                                                <asp:ListItem Value="4">Tia</asp:ListItem>
                                                <asp:ListItem Value="5">Hermano</asp:ListItem>
                                                <asp:ListItem Value="6">Hermana</asp:ListItem>
                                                <asp:ListItem Value="7">Padrino</asp:ListItem>
                                                <asp:ListItem Value="8">Madrina</asp:ListItem>
                                                <asp:ListItem Value="9">Primo</asp:ListItem>
                                                <asp:ListItem Value="10">Prima</asp:ListItem>
                                                <asp:ListItem Value="11">Amigo</asp:ListItem>
                                                <asp:ListItem Value="12">Amiga</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlViveConTutor"
                                                ErrorMessage="Estudio socioeconómico: Selecciona tu parentesco con el tutor."
                                                ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer"
                                                ValueToCompare="0">*</asp:CompareValidator></asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Tu Casa es:
                                    </td>
                                    <td style="width: 300px">
                                        <asp:RadioButtonList ID="rblTipoDeCasa" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="2">Rentada</asp:ListItem>
                                            <asp:ListItem Value="1">Propia</asp:ListItem>
                                            <asp:ListItem Value="3">Prestada</asp:ListItem>
                                            <asp:ListItem Value="4">Otra</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rblTipoDeCasa"
                                            ErrorMessage="Estudio socioeconómico: Selecciona el tipo de casa." ForeColor="Red"
                                            SetFocusOnError="True">Selecciona una opción</asp:RequiredFieldValidator>
                                    </td>
                                    <td width="450">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Ingreso Mensual:
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rblIngresoMensual" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Menor a $2500</asp:ListItem>
                                            <asp:ListItem Value="2">Entre $2500 y $5000</asp:ListItem>
                                            <asp:ListItem Value="3">Mayor a $5000</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="rblIngresoMensual"
                                            ErrorMessage="Estudio socioeconómico: Selecciona el ingreso mensual." ForeColor="Red"
                                            SetFocusOnError="True">Selecciona una opción</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Numero de Integrantes:
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlIntegrantes" runat="server">
                                            <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem Value="11">&gt;10</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddlIntegrantes"
                                            ErrorMessage="Estudio socioeconómico: Selecciona el numero de integrantes." ForeColor="Red"
                                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkTienePC" runat="server" Text="Tienes computadora" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkTieneInternet" runat="server" Text="Tienes internet" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabSecundaria" runat="server" HeaderText="Escuela de Procedencia">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="formLabel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        Secundaria en la que estudiaste
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Municipio:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSecMunicipio" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSecMunicipio_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Localidad:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSecLocalidad" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSecLocalidad_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Secundaria:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSecSecundaria" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSecSecundaria_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSecundaria" runat="server" MaxLength="200" Width="300px" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabPadre" runat="server" HeaderText="Padre">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="formLabel">
                                        Apellido Paterno:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPadreApPaterno" runat="server" MaxLength="50" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                                runat="server" ControlToValidate="txtPadreApPaterno" ErrorMessage="Padre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Apellido Materno:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPadreApMaterno" runat="server" MaxLength="50" Style="color: #003300;
                                            margin-left: 0px;" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                                                runat="server" ControlToValidate="txtPadreApMaterno" ErrorMessage="Padre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Nombre(s):
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPadreNombre" runat="server" MaxLength="50" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator18"
                                                runat="server" ControlToValidate="txtPadreNombre" ErrorMessage="Padre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Teléfono:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPadreTelefono" runat="server" MaxLength="20" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator19"
                                                runat="server" ControlToValidate="txtPadreTelefono" ErrorMessage="Padre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Ocupación:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPadreOcupacion" runat="server" Width="200px">
                                            <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                            <asp:ListItem Value="0">Empresario</asp:ListItem>
                                            <asp:ListItem Value="1">Ganadero</asp:ListItem>
                                            <asp:ListItem Value="2">Obrero</asp:ListItem>
                                            <asp:ListItem Value="3">Jornalero</asp:ListItem>
                                            <asp:ListItem Value="4">Comerciante</asp:ListItem>
                                            <asp:ListItem Value="5">Ejidatario</asp:ListItem>
                                            <asp:ListItem Value="6">Industrial</asp:ListItem>
                                            <asp:ListItem Value="7">Ama de casaAma de casa</asp:ListItem>
                                            <asp:ListItem Value="8">Empleado</asp:ListItem>
                                            <asp:ListItem Value="9">Otro</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator15" runat="server" ControlToValidate="ddlPadreOcupacion"
                                            ErrorMessage="Padre o Tutor." ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True"
                                            Type="Integer" ValueToCompare="0">*</asp:CompareValidator>&nbsp; Empresa:
                                        <asp:TextBox ID="txtPadreEmpresa" runat="server" MaxLength="50" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator20"
                                                runat="server" ControlToValidate="txtPadreEmpresa" ErrorMessage="Padre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabMadre" runat="server" HeaderText="Madre">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="formLabel">
                                        Apellido Paterno:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMadreApPaterno" runat="server" MaxLength="50" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator21"
                                                runat="server" ControlToValidate="txtMadreApPaterno" ErrorMessage="Madre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Apellido Materno:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMadreApMaterno" runat="server" MaxLength="50" Style="color: #003300;
                                            margin-left: 0px;" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator22"
                                                runat="server" ControlToValidate="txtMadreApMaterno" ErrorMessage="Madre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Nombre(s):
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMadreNombre" runat="server" MaxLength="50" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator23"
                                                runat="server" ControlToValidate="txtMadreNombre" ErrorMessage="Madre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Teléfono:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMadreTelefono" runat="server" MaxLength="20" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator24"
                                                runat="server" ControlToValidate="txtMadreTelefono" ErrorMessage="Madre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        Ocupación:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMadreOcupacion" runat="server" Width="200px">
                                            <asp:ListItem Value="-1">Seleccionar:</asp:ListItem>
                                            <asp:ListItem Value="0">Empresario</asp:ListItem>
                                            <asp:ListItem Value="1">Ganadero</asp:ListItem>
                                            <asp:ListItem Value="2">Obrero</asp:ListItem>
                                            <asp:ListItem Value="3">Jornalero</asp:ListItem>
                                            <asp:ListItem Value="4">Comerciante</asp:ListItem>
                                            <asp:ListItem Value="5">Ejidatario</asp:ListItem>
                                            <asp:ListItem Value="6">Industrial</asp:ListItem>
                                            <asp:ListItem Value="7">Ama de casaAma de casa</asp:ListItem>
                                            <asp:ListItem Value="8">Empleado</asp:ListItem>
                                            <asp:ListItem Value="9">Otro</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator16" runat="server" ControlToValidate="ddlMadreOcupacion"
                                            ErrorMessage="Madre o Tutor." ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True"
                                            Type="Integer" ValueToCompare="0">*</asp:CompareValidator>&nbsp; Empresa:
                                        <asp:TextBox ID="txtMadreEmpresa" runat="server" MaxLength="50" Style="color: #003300"
                                            Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator25"
                                                runat="server" ControlToValidate="txtMadreEmpresa" ErrorMessage="Madre o Tutor."
                                                ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabCambioDePlantel" runat="server" HeaderText="Cambio de Plantel">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="formLabel" style="width: 224px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        Selecciona nuevamente el plantel <strong>solo si quieres cambiarte</strong>.
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel" style="width: 224px">
                                        Municipio:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlantelMunicipio" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddlPlantelMunicipio_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel" style="width: 224px">
                                        Plantel:&nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlantelPlantel" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" Width="500px" OnSelectedIndexChanged="ddlPlantelPlantel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator21" runat="server" ControlToValidate="ddlPlantelPlantel"
                                            ErrorMessage="Cambio de Plantel: Selecciona el Plantel." ForeColor="Red" Operator="GreaterThanEqual"
                                            SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel" style="width: 224px">
                                        Turno de preferencia:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlantelTurno" runat="server" Width="200px">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator22" runat="server" ControlToValidate="ddlPlantelTurno"
                                            ErrorMessage="Cambio de Plantel: Selecciona el Turno." ForeColor="Red" Operator="GreaterThanEqual"
                                            SetFocusOnError="True" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formLabel" style="width: 224px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        El Turno seleccionado es para información estadística, no garantiza tu inscripción
                                        definitiva en el mismo.
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
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div style="color: #FFFFFF; background-color: #99CC00; font-size: 14pt; text-align: right;
            vertical-align: middle;">
            Despues de haber llenado todas las pestañas da CLICK AQUÍ:
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar e Imprimir Solicitud" OnClick="btnGuardar_Click" />
            <br />
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
