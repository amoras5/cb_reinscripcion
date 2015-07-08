<%@ Page Title="" Language="C#" MasterPageFile="MP.master" AutoEventWireup="true"
    CodeFile="Inscripciones.aspx.cs" Inherits="Estadisticas_Inscripciones" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 style="text-align: center">
        Estadísticas del Proceso de Reinscripción</h1>
    <p style="text-align: center">
        (No incluye EMSADs)</p>
    <table style="width: 100%;">
        <tr valign="top">
            <td style="width: 500px">
                <asp:Chart ID="Chart1" runat="server" Width="500px" Height="300px">
                    <Series>
                        <asp:Series Name="Semestre 1" ToolTip="#VALY" LabelBackColor="White" ChartType="StackedColumn"
                            Color="Purple" Legend="Legend1">
                            <Points>
                                <asp:DataPoint AxisLabel="No ha llenado" XValue="0" YValues="0" />
                                <asp:DataPoint AxisLabel="Llenó web" XValue="1" YValues="0" />
                                <asp:DataPoint AxisLabel="Validado" XValue="2" YValues="0" />
                                <asp:DataPoint AxisLabel="Ficha impresa" XValue="3" YValues="0" />
                                <asp:DataPoint AxisLabel="Pagado" XValue="4" YValues="0" />
                                <asp:DataPoint AxisLabel="Conveniado" XValue="5" YValues="0" />
                                <asp:DataPoint AxisLabel="Exento" XValue="6" YValues="0" />
                                <asp:DataPoint AxisLabel="Baja" XValue="7" YValues="0" />
                            </Points>
                        </asp:Series>
                        <asp:Series Name="Semestre 2" ToolTip="#VALY" LabelBackColor="White" ChartType="StackedColumn"
                            Color="Red" Legend="Legend1">
                            <Points>
                                <asp:DataPoint AxisLabel="No ha llenado" XValue="0" YValues="0" />
                                <asp:DataPoint AxisLabel="Llenó web" XValue="1" YValues="0" />
                                <asp:DataPoint AxisLabel="Validado" XValue="2" YValues="0" />
                                <asp:DataPoint AxisLabel="Ficha impresa" XValue="3" YValues="0" />
                                <asp:DataPoint AxisLabel="Pagado" XValue="4" YValues="0" />
                            </Points>
                        </asp:Series>
                        <asp:Series Name="Semestre 3" ToolTip="#VALY" LabelBackColor="White" ChartType="StackedColumn"
                            Color="Yellow" Legend="Legend1">
                            <Points>
                                <asp:DataPoint AxisLabel="No ha llenado" XValue="0" YValues="0" />
                                <asp:DataPoint AxisLabel="Llenó web" XValue="1" YValues="0" />
                                <asp:DataPoint AxisLabel="Validado" XValue="2" YValues="0" />
                                <asp:DataPoint AxisLabel="Ficha impresa" XValue="3" YValues="0" />
                                <asp:DataPoint AxisLabel="Pagado" XValue="4" YValues="0" />
                            </Points>
                        </asp:Series>
                        <asp:Series Name="Semestre 4" ToolTip="#VALY" LabelBackColor="White" ChartType="StackedColumn"
                            Color="128, 64, 64" Legend="Legend1">
                            <Points>
                                <asp:DataPoint AxisLabel="No ha llenado" XValue="0" YValues="0" />
                                <asp:DataPoint AxisLabel="Llenó web" XValue="1" YValues="0" />
                                <asp:DataPoint AxisLabel="Validado" XValue="2" YValues="0" />
                                <asp:DataPoint AxisLabel="Ficha impresa" XValue="3" YValues="0" />
                                <asp:DataPoint AxisLabel="Pagado" XValue="4" YValues="0" />
                            </Points>
                        </asp:Series>
                        <asp:Series Name="Semestre 5" ToolTip="#VALY" LabelBackColor="White" ChartType="StackedColumn"
                            Color="0, 192, 0" Legend="Legend1">
                            <Points>
                                <asp:DataPoint AxisLabel="No ha llenado" XValue="0" YValues="0" />
                                <asp:DataPoint AxisLabel="Llenó web" XValue="1" YValues="0" />
                                <asp:DataPoint AxisLabel="Validado" XValue="2" YValues="0" />
                                <asp:DataPoint AxisLabel="Ficha impresa" XValue="3" YValues="0" />
                                <asp:DataPoint AxisLabel="Pagado" XValue="4" YValues="0" />
                            </Points>
                        </asp:Series>
                        <asp:Series Name="Semestre 6" ToolTip="#VALY" LabelBackColor="White" ChartType="StackedColumn"
                            Color="Blue" Legend="Legend1">
                            <Points>
                                <asp:DataPoint AxisLabel="No ha llenado" XValue="0" YValues="0" />
                                <asp:DataPoint AxisLabel="Llenó web" XValue="1" YValues="0" />
                                <asp:DataPoint AxisLabel="Validado" XValue="2" YValues="0" />
                                <asp:DataPoint AxisLabel="Ficha impresa" XValue="3" YValues="0" />
                                <asp:DataPoint AxisLabel="Pagado" XValue="4" YValues="0" />
                            </Points>
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Title="ALUMNOS" TitleFont="Microsoft Sans Serif, 10pt, style=Bold">
                            </AxisY>
                            <AxisX Title="ESTATUS" TitleFont="Microsoft Sans Serif, 10pt, style=Bold">
                            </AxisX>
                            <Area3DStyle Enable3D="True" />
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="Semestres">
                        </asp:Legend>
                    </Legends>
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
                            &nbsp;Para filtrar, selecciona las opciones correspondientes.
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
                        <td style="width: 60px; text-align: right">
                            <b>Estatus:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEstatus" runat="server" Width="120px">
                                <asp:ListItem Value="-1">Todos</asp:ListItem>
                                <asp:ListItem Value="0">No ha llenado</asp:ListItem>
                                <asp:ListItem Value="1">Llenó web</asp:ListItem>
                                <asp:ListItem Value="2">Validado</asp:ListItem>
                                <asp:ListItem Value="3">Ficha impresa</asp:ListItem>
                                <asp:ListItem Value="4">Pagado</asp:ListItem>
                                <asp:ListItem Value="8">Conveniado</asp:ListItem>
                                <asp:ListItem Value="9">Exento</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:Button ID="btnImprimir" runat="server" Enabled="False" Text="Generar listado"
                                Width="170px" OnClick="btnImprimir_Click" ToolTip="Para poder generar el listado, tienes que haber elegido un plantel" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Text="No hay registros encontrados."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            <b></b>
                        </td>
                        <td>
                            <asp:Button ID="btnActualizarGraficas" runat="server" OnClick="btnActualizarGraficas_Click"
                                Text="Actualizar Gráficas" Width="300px" ToolTip="Click para reflejar los filtros de Turno, Semestre y Estatus ya que no se aplican automáticamente." />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                      <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                      <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                      <tr>
                        <td style="text-align: left" colspan="2">
                            Reportes Globales:</td>
                    </tr>
                      <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnRPTSeguimientoDeInscripciones" runat="server" 
                                onclick="btnRPTSeguimientoDeInscripciones_Click" 
                                Text="Seguimiento de Inscripciones" Width="300px" />
                          </td>
                    </tr>
                      <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                      <tr>
                        <td style="width: 60px; text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
