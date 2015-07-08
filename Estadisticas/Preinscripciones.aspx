<%@ Page Title="" Language="C#" MasterPageFile="MP.master" AutoEventWireup="true"
    CodeFile="Preinscripciones.aspx.cs" Inherits="Estadisticas_Preinscripciones"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 style="text-align: center">
        Estadísticas del proceso de PreInscripción</h1>
    <br />
    <table style="width: 100%">
        <tr valign="top">
            <td style="width: 500px">
                <asp:Chart ID="Chart1" runat="server" Width="500px">
                    <Series>
                        <asp:Series Label="#VALY" LabelBackColor="White" Name="Series1" XValueMember="Estatus"
                            YValueMembers="Alumnos" Legend="Legend1" XValueType="String">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Title="ALUMNOS" TitleFont="Microsoft Sans Serif, 10pt, style=Bold">
                            </AxisY>
                            <AxisX Title="ESTATUS" TitleFont="Microsoft Sans Serif, 10pt, style=Bold">
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Para filtrar, selecciona las opciones correspondientes.
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 60px; text-align: right">
                            <strong>Ciclo: </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCiclo" runat="server" Width="120px">
                                <asp:ListItem Selected="True">2015B</asp:ListItem>
                                <asp:ListItem>2014B</asp:ListItem>
                                <asp:ListItem>2013B</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            <b>Zona:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlZonas" runat="server" AutoPostBack="True" Width="300px"
                                OnSelectedIndexChanged="ddlZonas_SelectedIndexChanged">
                                <asp:ListItem Value="0">Todas</asp:ListItem>
                                <asp:ListItem Value="1">Zona 1</asp:ListItem>
                                <asp:ListItem Value="2">Zona 2</asp:ListItem>
                                <asp:ListItem Value="3">Zona 3</asp:ListItem>
                                <asp:ListItem Value="4">Zona 4</asp:ListItem>
                                <asp:ListItem Value="5">Zona 5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            <b>Plantel:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlanteles" runat="server" AutoPostBack="True" Width="300px"
                                Enabled="False" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlPlanteles_SelectedIndexChanged">
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            <b>Turno:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTurnos" runat="server" Width="120px">
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Matutino</asp:ListItem>
                                <asp:ListItem Value="2">Vespertino</asp:ListItem>
                                <asp:ListItem Value="3">Nocturno</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 60px; text-align: right">
                            <b>Estatus:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEstatus" runat="server" Width="120px">
                                <asp:ListItem Value="-1">Todos</asp:ListItem>
                                <asp:ListItem Value="0">Llenó web</asp:ListItem>
                                <asp:ListItem Value="1">Validado</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:Button ID="btnActualizar" runat="server" 
                                onclick="btnActualizar_Click" Text="Actualizar Gráficas" Width="175px" />
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnImprimir" runat="server" Enabled="False" Text="Imprimir listado de Alumnos"
                                Width="300px" OnClick="btnImprimir_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            <b></b>
                        </td>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Text="No hay registros encontrados."></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
