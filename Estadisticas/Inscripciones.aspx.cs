using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Web.UI.DataVisualization.Charting;

public partial class Estadisticas_Inscripciones : System.Web.UI.Page
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
                      where P.Zona == Zona && (P.EsEmsad == false || P.PlantelID == 124) && P.Activo == true
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
        int Zona = int.Parse(this.ddlZonas.SelectedValue);
        int PlantelID = int.Parse(this.ddlPlanteles.SelectedValue);
        byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
        byte Semestre = byte.Parse(this.ddlSemestres.SelectedValue);
        int Estatus = int.Parse(ddlEstatus.SelectedValue);

        db = new DBEscolarDataContext();
        var res = (from A in db.Alumnos
                   join P in db.Plantels on A.PlantelID equals P.PlantelID
                   where P.Zona == (Zona == 0 ? P.Zona : Zona) && P.PlantelID == (PlantelID == 0 ? P.PlantelID : PlantelID) && A.Turno == (Turno == 0 ? A.Turno : Turno)
                   && A.Semestre == (Semestre == 0 ? A.Semestre : Semestre) && A.Estatus == (Estatus == -1 ? A.Estatus : Estatus) && (P.EsEmsad == false || P.PlantelID == 124)
                   group A by new { A.Estatus, A.Semestre } into g
                   orderby g.Key.Estatus, g.Key.Semestre
                   select new { Estatus = g.Key.Estatus, Semestre = g.Key.Semestre, Alumnos = g.Count() }).ToList();

        for (int i = 1; i <= 6; i++)//Semestres
        {

            for (int j = 0; j < 5; j++)//Estatus
            {
                var R = res.Find(x => x.Estatus == j && x.Semestre == i);

                if (R == null)
                    this.Chart1.Series[i - 1].Points[j].SetValueY(0);
                else
                {
                    this.Chart1.Series[i - 1].Points[j].SetValueY(R.Alumnos);
                    var TotalXEstatus = res.FindAll(x => x.Estatus == j).Sum(x => x.Alumnos);
                    this.Chart1.Series[i - 1].Points[j].ToolTip = string.Format("#VALY / {0} = {1:P}", TotalXEstatus, (decimal)R.Alumnos / TotalXEstatus);
                }
            }
        }

        //Columna de Conveniados
        DataPoint GPoint = this.Chart1.Series[0].Points[5];
        GPoint.Color = System.Drawing.Color.LightGreen;
        var Conveniados = res.Where(x => x.Estatus == 8).Sum(y => y.Alumnos);
        GPoint.SetValueY(Conveniados);

        //Columna de Excentos
        GPoint = this.Chart1.Series[0].Points[6];
        GPoint.Color = System.Drawing.Color.LawnGreen;
        var Exentos = res.Where(x => x.Estatus == 9).Sum(y => y.Alumnos);
        GPoint.SetValueY(Exentos);

        //Columna de bajas
        GPoint = this.Chart1.Series[0].Points[7];
        GPoint.Color = System.Drawing.Color.LightGray;
        var Bajas = res.Where(x => x.Estatus == 5).Sum(y => y.Alumnos);
        var BajasAcad = res.Where(x => x.Estatus == 6).Sum(y => y.Alumnos);
        int BajasTotales = Bajas + BajasAcad;
        GPoint.SetValueY(Bajas + BajasAcad);
        if (BajasTotales != 0)
            GPoint.ToolTip = string.Format("Bajas Totales: #VALY\nBajas: {0} = {1:P}\nBajas Académicas: {2} = {3:P}", Bajas, (decimal)Bajas / BajasTotales, BajasAcad, (decimal)BajasAcad / BajasTotales);

        this.lblMensaje.Visible = res.Count() == 0;

        //Habilitar / Deshabilitar reporte
        this.btnImprimir.Enabled = this.ddlPlanteles.SelectedIndex > 0 && res.Count() > 0;
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {

        int Zona = int.Parse(this.ddlZonas.SelectedValue);
        int PlantelID = int.Parse(this.ddlPlanteles.SelectedValue);
        byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
        byte Semestre = byte.Parse(this.ddlSemestres.SelectedValue);
        int Estatus = int.Parse(ddlEstatus.SelectedValue);

        db = new DBEscolarDataContext();
        var Alumnos = (from A in db.vwRPTEstadisticaAlumnos01s
                       where A.Zona == Zona && A.PlantelID == PlantelID && A.Turno == (Turno == 0 ? A.Turno : Turno)
                       && A.Semestre == (Semestre == 0 ? A.Semestre : Semestre) && A.Estatus == (Estatus == -1 ? A.Estatus : Estatus)
                       orderby A.Zona, A.Plantel, A.Turno, A.Semestre, A.Grupo
                       select A).ToList();

        this.lblMensaje.Visible = Alumnos.Count() == 0;
        if (Alumnos.Count() == 0)
            return;

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Reportes/rptEstAlu01.rpt"));
        rptDoc.SetDataSource(Alumnos);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        rptDoc.Close();
        rptDoc.Dispose();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "inline; filename=rptEstAlu01.xls");
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        stream.Close();
        stream.Dispose();
    }

    protected void btnActualizarGraficas_Click(object sender, EventArgs e)
    {
        this.MostrarDatos();
    }

    protected void btnRPTSeguimientoDeInscripciones_Click(object sender, EventArgs e)
    {
        db = new DBEscolarDataContext();
        var Alumnos = (from A in db.vwRPTEstSegInsc01s
                       orderby A.Zona, A.Plantel
                       select A).ToList();

        if (Alumnos.Count() == 0)
            return;

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Reportes/rptEstSegInsc01.rpt"));
        rptDoc.SetDataSource(Alumnos);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        rptDoc.Close();
        rptDoc.Dispose();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "inline; filename=rptEstSegInsc01.xls");
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        stream.Close();
        stream.Dispose();
    }

}