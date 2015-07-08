using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Collections;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

public partial class Estadisticas_Preinscripciones : System.Web.UI.Page
{
    DBEscolarDataContext db;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            this.MostrarDatos();
    }

    protected void ddlZonas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Zona = int.Parse(this.ddlZonas.SelectedValue);

        this.ddlPlanteles.Enabled = Zona > 0;
        this.ddlPlanteles.Items.Clear();
        this.ddlPlanteles.Items.Add(new ListItem("Todos", "0"));

        if (Zona > 0)
        {
            db = new DBEscolarDataContext();
            var res = from P in db.Plantels
                      where P.Zona == Zona && P.Activo == true
                      orderby P.plantel
                      select new { PlantelID = P.PlantelID, Nombre = string.Format("{0} - {1}", P.plantel, P.Descripcion) };

            this.ddlPlanteles.DataSource = res;
            this.ddlPlanteles.DataTextField = "Nombre";
            this.ddlPlanteles.DataValueField = "PlantelID";
            this.ddlPlanteles.DataBind();
        }

        this.MostrarDatos();
    }

    protected void ddlPlanteles_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.MostrarDatos();
    }

    private void MostrarDatos()
    {
        string Ciclo = this.ddlCiclo.SelectedValue;
        int Zona = int.Parse(this.ddlZonas.SelectedValue);
        int PlantelID = int.Parse(this.ddlPlanteles.SelectedValue);
        byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
        int Estatus = int.Parse(ddlEstatus.SelectedValue);

        db = new DBEscolarDataContext();
        var res = (from A in db.PreInscritos
                   join P in db.Plantels on A.PlantelID equals P.PlantelID
                   where A.Ciclo == Ciclo && P.Zona == (Zona == 0 ? P.Zona : Zona) && P.PlantelID == (PlantelID == 0 ? P.PlantelID : PlantelID)
                    && A.Turno == (Turno == 0 ? A.Turno : Turno) && A.Estatus == (Estatus == -1 ? A.Estatus : Estatus)
                   group A by A.Estatus into g
                   select new { Estatus = g.Key.ToString().Replace("0", "Llenó web").Replace("1", "Validado").Replace("2", "Validado (Insc)"), Alumnos = g.Count() }).ToList();

        this.lblMensaje.Visible = res.Count() == 0;

        this.Chart1.DataSource = res;
        this.Chart1.DataBind();

        //Habilitar / Deshabilitar reporte
        this.btnImprimir.Enabled = this.ddlPlanteles.SelectedIndex > 0 && res.Count() > 0;
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        this.MostrarDatos();
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        string Ciclo = this.ddlCiclo.SelectedValue;
        int Zona = int.Parse(this.ddlZonas.SelectedValue);
        int PlantelID = int.Parse(this.ddlPlanteles.SelectedValue);
        byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
        int Estatus = int.Parse(ddlEstatus.SelectedValue);

        string NomPlantel = this.ddlPlanteles.SelectedItem.Text;

        db = new DBEscolarDataContext();
        var res = (from A in db.vwRPTEstadisticaPreInscritos011s
                   where A.Ciclo == Ciclo && A.Zona == Zona && A.PlantelID == PlantelID && A.Turno == (Turno == 0 ? A.Turno : Turno)
                    && A.Estatus == (Estatus == -1 ? A.Estatus : Estatus)
                   orderby A.Zona, A.Plantel, A.Turno, A.Nombre
                   select A).ToList();

        this.lblMensaje.Visible = res.Count() == 0;
        if (res.Count() == 0)
            return;

        ReportDocument rptDoc = new ReportDocument();

        rptDoc.Load(Server.MapPath("../Reportes/rptEstPre01.rpt"));
        rptDoc.SetDataSource(res);
        rptDoc.SetParameterValue("NomPlantel", NomPlantel);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        rptDoc.Close();
        rptDoc.Dispose();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "inline; filename=Alumnos.xls");
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        stream.Close();
        stream.Dispose();
    }

}