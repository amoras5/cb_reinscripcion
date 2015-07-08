<%@ Page Title="" Language="C#" MasterPageFile="MP.master" AutoEventWireup="true"
    CodeFile="Calificaciones.aspx.cs" Inherits="Estadisticas_Calificaciones" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 style="text-align: center">
        Estadísticas de Calificaciones</h1>
    <p style="text-align: center">
        (No incluye EMSADs)</p>
    <table style="width: 100%;">
        <tr valign="top">
            <td style="width: 500px">
                <asp:CheckBox ID="chkMostrarGrafica" runat="server" AutoPostBack="True" OnCheckedChanged="chkMostrarGrafica_CheckedChanged"
                    Text="Mostrar concentrado estatal" Width="200px" />
                <br />
                <asp:Chart ID="Chart0" runat="server" Width="500px" EnableViewState="True" Visible="False">
                    <Series>
                        <asp:Series Name="Series1" Color="0, 192, 0" LegendText="0" ChartType="Pie" Label="#VAL{#,###}: #PERCENT"
                            LabelBackColor="White" CustomProperties="PieLineColor=Black, PieLabelStyle=Outside">
                            <Points>
                                <asp:DataPoint XValue="0" YValues="0" Color="0, 192, 0" />
                                <asp:DataPoint XValue="1" YValues="0" Color="192, 255, 192" LegendText="1" />
                                <asp:DataPoint XValue="2" YValues="0" Color="Yellow" LegendText="2" />
                                <asp:DataPoint XValue="3" YValues="0" Color="255, 128, 0" LegendText="3" />
                                <asp:DataPoint XValue="4" YValues="0" Color="Red" LegendText="4 o más" />
                            </Points>
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Top" Name="Legend1" Title="Número de materias reprobadas">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
                <br />
                <asp:Chart ID="Chart1" runat="server" Width="500px" EnableViewState="True">
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Key" YValueMembers="R0" Color="0, 192, 0"
                            Legend="Legend1" LegendText="0">
                        </asp:Series>
                        <asp:Series Name="Series2" XValueMember="Key" YValueMembers="R1" Color="192, 255, 192"
                            Legend="Legend1" LegendText="1">
                        </asp:Series>
                        <asp:Series Name="Series3" XValueMember="Key" YValueMembers="R2" Color="Yellow" Legend="Legend1"
                            LegendText="2">
                        </asp:Series>
                        <asp:Series Name="Series4" XValueMember="Key" YValueMembers="R3" Color="255, 128, 0"
                            Legend="Legend1" LegendText="3">
                        </asp:Series>
                        <asp:Series Name="Series5" XValueMember="Key" YValueMembers="R4" Color="Red" Legend="Legend1"
                            LegendText="4 o más">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Title="ALUMNOS" Maximum="100" Minimum="0">
                            </AxisY>
                            <AxisX Title="ZONAS">
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Top" Name="Legend1" Title="Número de materias reprobadas">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
            </td>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            Para filtrar, selecciona las opciones correspondientes.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            <strong>Ciclo: </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCiclo" runat="server" Width="120px">
                                <asp:ListItem Value="2015A"></asp:ListItem>
                                <asp:ListItem>2014B</asp:ListItem>
                                <asp:ListItem>2014A</asp:ListItem>
                                <asp:ListItem>2013B</asp:ListItem>
                                <asp:ListItem>2013A</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            <strong>Periodo: </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPeriodo" runat="server" Width="120px">
                                <asp:ListItem Value="1">Parcial 1</asp:ListItem>
                                <asp:ListItem Value="2">Parcial 2</asp:ListItem>
                                <asp:ListItem Value="3">Parcial 3</asp:ListItem>
                                <asp:ListItem Value="4">Semestral</asp:ListItem>
                                <asp:ListItem Value="5">Ordinario</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:CheckBox ID="chkPromediado" runat="server" Text="Promediado" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
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
                        <td style="text-align: right" class="style1">
                            <b>Plantel:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlanteles" runat="server" Width="300px" Enabled="False"
                                AppendDataBoundItems="True" >
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
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
                        <td style="text-align: right" class="style1">
                            <b>Semestre:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSemestres" runat="server" Width="120px">
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            <strong>Reporte:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="120px">
                                <asp:ListItem>Por Grupo</asp:ListItem>
                                <asp:ListItem>Por Alumno</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnGenerar" runat="server" Text="Generar reporte" Width="170px" OnClick="btnGenerar_Click"
                                ValidationGroup="Imprimir" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnActualizarGraficas" runat="server" OnClick="btnActualizarGraficas_Click"
                                Text="Actualizar Gráficas" Width="300px" />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            &nbsp;
                        </td>
                        <td>
                            <asp:GridView ID="gvInfoBitacora" runat="server" AutoGenerateColumns="False" Width="300px">
                                <Columns>
                                    <asp:BoundField DataField="Descripcion" />
                                    <asp:BoundField DataField="Z" HeaderText="Zona" />
                                    <asp:BoundField DataField="P" HeaderText="Plantel" />
                                    <asp:BoundField DataField="Fecha" DataFormatString="{0:g}" HeaderText="Fecha" HtmlEncode="False" />
                                </Columns>
                                <HeaderStyle BackColor="White" />
                            </asp:GridView>
                            <asp:Button ID="btninfoBitacora" runat="server" Text="Descargar listado de envíos"
                                Width="300px" OnClick="btninfoBitacora_Click" />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2">
                <asp:Chart ID="Chart2" runat="server" Width="890px" EnableViewState="True" Visible="False"
                    Height="600px">
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Key" YValueMembers="R0" Color="0, 192, 0"
                            Legend="Legend1" LegendText="0">
                        </asp:Series>
                        <asp:Series Name="Series2" XValueMember="Key" YValueMembers="R1" Color="192, 255, 192"
                            Legend="Legend1" LegendText="1">
                        </asp:Series>
                        <asp:Series Name="Series3" XValueMember="Key" YValueMembers="R2" Color="Yellow" Legend="Legend1"
                            LegendText="2">
                        </asp:Series>
                        <asp:Series Name="Series4" XValueMember="Key" YValueMembers="R3" Color="255, 128, 0"
                            Legend="Legend1" LegendText="3">
                        </asp:Series>
                        <asp:Series Name="Series5" XValueMember="Key" YValueMembers="R4" Color="Red" Legend="Legend1"
                            LegendText="4 o más">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Title="ALUMNOS" Maximum="100" Minimum="0">
                            </AxisY>
                            <AxisX Title="PLANTELES" Interval="1">
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Top" Name="Legend1" Title="Número de materias reprobadas">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
            </td>
        </tr>
    </table>
</asp:Content>
