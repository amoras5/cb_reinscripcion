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
using System.Data.Common;

public partial class Estadisticas_Calificaciones : System.Web.UI.Page
{

    public string Ciclo
    {
        get
        {
            return ViewState["Ciclo"] == null ? "" : ViewState["Ciclo"].ToString();
        }
        set
        {
            ViewState["Ciclo"] = Ciclo;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Graficar();

            this.MostrarBitacora();
        }
    }

    protected void ddlZonas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Zona = int.Parse(this.ddlZonas.SelectedValue);

        this.Chart2.Visible = false;

        this.ddlPlanteles.Enabled = Zona > 0;
        this.ddlPlanteles.Items.Clear();
        this.ddlPlanteles.Items.Add(new ListItem("Todos", "0"));

        if (Zona > 0)
        {
            DBEscolarDataContext db = new DBEscolarDataContext();
            var res = from P in db.Plantels
                      where P.Zona == Zona && P.EsEmsad == false && P.Activo == true
                      orderby P.plantel
                      select new { Plantel = P.plantel, Nombre = string.Format("{0} - {1}", P.plantel, P.Descripcion) };

            this.ddlPlanteles.DataSource = res;
            this.ddlPlanteles.DataTextField = "Nombre";
            this.ddlPlanteles.DataValueField = "Plantel";
            this.ddlPlanteles.DataBind();
        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        string Ciclo = this.ddlCiclo.SelectedValue;
        int Periodo = int.Parse(this.ddlPeriodo.SelectedValue);
        int Zona = int.Parse(this.ddlZonas.SelectedValue);
        int Plantel = int.Parse(this.ddlPlanteles.SelectedValue);
        byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
        byte Semestre = byte.Parse(this.ddlSemestres.SelectedValue);

        switch (this.DropDownList1.SelectedIndex)
        {
            case 0:
                this.GenerarReporteXGrupo(Zona, Plantel, Ciclo, Periodo, Turno, Semestre);
                break;

            case 1:
                this.GenerarReporteXAlumno(Zona, Plantel, Ciclo, Periodo, Turno, Semestre);
                break;

            default:
                break;
        }
    }

    private void GenerarReporteXGrupo(int Zona, int Plantel, string Ciclo, int Periodo, byte Turno, byte Semestre)
    {
        EscolarModel.EstaEntities db = new EscolarModel.EstaEntities();

       
        var Alumnos = (from A in db.vwRPTParcial1XGPO
                       join P in db.vwPlanteles on new { z = A.or_zona, pl = A.or_plant } equals new { z = (byte)P.idZona, pl = (int)P.idPlantel }
                       where A.Ciclo == Ciclo && A.Periodo == Periodo &&
                            A.or_zona == (Zona == 0 ? A.or_zona : Zona) && A.or_plant == (Plantel == 0 ? A.or_plant : Plantel)
                            && A.or_turno == (Turno == 0 ? A.or_turno : Turno) && A.or_semest == (Semestre == 0 ? A.or_semest : Semestre)
                            && P.EsEmsad == 0
                       orderby A.or_zona, A.or_plant, A.or_turno, A.or_semest, A.or_grupo, A.or_asigna
                       select A).ToList();

        if (Alumnos.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');", "No hay registros encontrados"), true);
            return;
        }

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Reportes/rptEstCalXGpo01.rpt"));
        rptDoc.SetDataSource(Alumnos);
        rptDoc.SetParameterValue("Periodo", Periodo);
        rptDoc.SetParameterValue("Ciclo", Ciclo);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        rptDoc.Close();
        rptDoc.Dispose();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "inline; filename=rptEstCalXGpo01.xls");
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        stream.Close();
        stream.Dispose();
    }

    private void GenerarReporteXAlumno(int Zona, int Plantel, string Ciclo, int Periodo, byte Turno, byte Semestre)
    {
        EscolarModel.EstaEntities db = new EscolarModel.EstaEntities();

        var Alumnos = (from A in db.vwRPTParcial1XALU
                       join P in db.vwPlanteles on new { z = A.or_zona, pl = A.or_plant } equals new { z = (byte)P.idZona, pl = (byte)P.idPlantel }
                       where A.Ciclo == Ciclo && A.Periodo == Periodo &&
                            A.or_zona == (Zona == 0 ? A.or_zona : Zona) && A.or_plant == (Plantel == 0 ? A.or_plant : Plantel)
                            && A.or_turno == (Turno == 0 ? A.or_turno : Turno) && A.or_semest == (Semestre == 0 ? A.or_semest : Semestre)
                            && P.EsEmsad == 0
                       orderby A.or_zona, A.or_plant, A.or_turno, A.or_semest, A.or_grupo, A.or_matric
                       select A).ToList();

        if (Alumnos.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');", "No hay registros encontrados"), true);
            return;
        }

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Reportes/rptEstCalXAlu01.rpt"));
        rptDoc.SetDataSource(Alumnos);
        rptDoc.SetParameterValue("Periodo", Periodo);
        rptDoc.SetParameterValue("Ciclo", Ciclo);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        rptDoc.Close();
        rptDoc.Dispose();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "inline; filename=rptEstCalXAlu01.xls");
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        stream.Close();
        stream.Dispose();
    }

    protected void chkMostrarGrafica_CheckedChanged(object sender, EventArgs e)
    {
        this.GraficarEstado();
    }

    protected void btnActualizarGraficas_Click(object sender, EventArgs e)
    {
        this.Graficar();

        this.MostrarBitacora();
    }

    private void GraficarEstado()
    {
        this.Chart0.Visible = this.chkMostrarGrafica.Checked;
        if (this.chkMostrarGrafica.Checked)
        {
            string Ciclo = this.ddlCiclo.SelectedValue;
            int Periodo = int.Parse(this.ddlPeriodo.SelectedValue);
            byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
            byte Semestre = byte.Parse(this.ddlSemestres.SelectedValue);

            DBEstadisticas.Escolar db = new DBEstadisticas.Escolar();

            var res = (from A in db.vwRPTParcial1XALU
                       join P in db.vwPlanteles on new { z = A.or_zona, pl = A.or_plant } equals new { z = (byte)P.idZona, pl = (byte)P.idPlantel }
                       where A.Ciclo == Ciclo && A.Periodo == Periodo &&
                            A.or_turno == (Turno == 0 ? A.or_turno : Turno) && A.or_semest == (Semestre == 0 ? A.or_semest : Semestre)
                            && P.EsEmsad == 0
                       group A by 1 into g
                       select new
                       {
                           R0 = g.Count(x => x.ReprobadasP1 == 0),
                           R1 = g.Count(x => x.ReprobadasP1 == 1),
                           R2 = g.Count(x => x.ReprobadasP1 == 2),
                           R3 = g.Count(x => x.ReprobadasP1 == 3),
                           R4 = g.Count(x => x.ReprobadasP1 > 3)
                       }).SingleOrDefault();

            if (res == null)
            {
                foreach (DataPoint P in Chart0.Series[0].Points)
                    P.SetValueY(0);

                return;
            }

            Chart0.Series[0].Points[0].SetValueY(res.R0);
            Chart0.Series[0].Points[1].SetValueY(res.R1);
            Chart0.Series[0].Points[2].SetValueY(res.R2);
            Chart0.Series[0].Points[3].SetValueY(res.R3);
            Chart0.Series[0].Points[4].SetValueY(res.R4);
        }
    }

    private void Graficar()
    {
        string Ciclo = this.ddlCiclo.SelectedValue;
        int Periodo = int.Parse(this.ddlPeriodo.SelectedValue);
        int Zona = int.Parse(this.ddlZonas.SelectedValue);
        int Plantel = int.Parse(this.ddlPlanteles.SelectedValue);
        byte Turno = byte.Parse(this.ddlTurnos.SelectedValue);
        byte Semestre = byte.Parse(this.ddlSemestres.SelectedValue);

        this.GraficarEstado();

        DBEstadisticas.Escolar db = new DBEstadisticas.Escolar();

        var tempXZona = (from A in db.vwRPTParcial1XALU
                         join P in db.vwPlanteles on new { z = A.or_zona, pl = A.or_plant } equals new { z = (byte)P.idZona, pl = (byte)P.idPlantel }
                         where A.Ciclo == Ciclo && A.Periodo == Periodo &&
                                A.or_turno == (Turno == 0 ? A.or_turno : Turno) && A.or_semest == (Semestre == 0 ? A.or_semest : Semestre)
                                && P.EsEmsad == 0
                         group A by A.or_zona into g
                         select new
                         {
                             Key = g.Key,
                             R0 = g.Count(x => x.ReprobadasP1 == 0),
                             R1 = g.Count(x => x.ReprobadasP1 == 1),
                             R2 = g.Count(x => x.ReprobadasP1 == 2),
                             R3 = g.Count(x => x.ReprobadasP1 == 3),
                             R4 = g.Count(x => x.ReprobadasP1 > 3)
                         }).ToList();

        var EstXZona = (from A in tempXZona
                        select new
                        {
                            Key = A.Key,
                            R0 = ((double)A.R0 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                            R1 = ((double)A.R1 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                            R2 = ((double)A.R2 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                            R3 = ((double)A.R3 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                            R4 = ((double)A.R4 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100
                        }).ToList();

        this.Chart1.DataSource = EstXZona;
        this.Chart1.DataBind();

        if (EstXZona.Count > 0)
        {
            double[] Promedios = new double[6];
            Promedios[0] = EstXZona.Sum(x => x.R0);
            Promedios[1] = EstXZona.Sum(x => x.R1);
            Promedios[2] = EstXZona.Sum(x => x.R2);
            Promedios[3] = EstXZona.Sum(x => x.R3);
            Promedios[4] = EstXZona.Sum(x => x.R4);
            Promedios[5] = EstXZona.Sum(x => x.R0 + x.R1 + x.R2 + x.R3 + x.R4);

            for (int p = 0; p < this.Chart1.Series[0].Points.Count; p++)//Zonas
            {
                int Alumnos = (int)tempXZona.Where(A => A.Key == p + 1).Sum(x => x.R0 + x.R1 + x.R2 + x.R3 + x.R4);
                for (int s = 0; s < this.Chart1.Series.Count; s++)
                {
                    double EnEstatusP = (double)this.Chart1.Series[s].Points[p].YValues[0] / 100;
                    int EnEstatusA = (int)((double)Alumnos * EnEstatusP);
                    this.Chart1.Series[s].Points[p].ToolTip = string.Format("Alumnos: {0} ({1:P})\nPromedio Estatal: {2:P}", EnEstatusA, EnEstatusP, Promedios[s] / Promedios[5]);
                }
            }
        }

        /////////////////////Grafica x PLANTELES
        if (this.ddlZonas.SelectedIndex > 0)
        {
            this.Chart2.Visible = true;
            var tempXPlantel = (from A in db.vwRPTParcial1XALU
                                where A.or_zona == Zona && A.Ciclo == Ciclo && A.Periodo == Periodo
                                     && A.or_turno == (Turno == 0 ? A.or_turno : Turno) && A.or_semest == (Semestre == 0 ? A.or_semest : Semestre)
                                group A by A.or_plant into g
                                orderby g.Key
                                select new
                                {
                                    Key = g.Key,
                                    R0 = g.Count(x => x.ReprobadasP1 == 0),
                                    R1 = g.Count(x => x.ReprobadasP1 == 1),
                                    R2 = g.Count(x => x.ReprobadasP1 == 2),
                                    R3 = g.Count(x => x.ReprobadasP1 == 3),
                                    R4 = g.Count(x => x.ReprobadasP1 > 3)
                                }).ToList();

            var EstXPlantel = from A in tempXPlantel
                              select new
                              {
                                  Key = A.Key.ToString(),
                                  R0 = ((double)A.R0 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                                  R1 = ((double)A.R1 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                                  R2 = ((double)A.R2 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                                  R3 = ((double)A.R3 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100,
                                  R4 = ((double)A.R4 / (A.R0 + A.R1 + A.R2 + A.R3 + A.R4)) * 100
                              };

            this.Chart2.DataSource = EstXPlantel;
            this.Chart2.DataBind();

            if (EstXPlantel != null)
            {
                for (int p = 0; p < this.Chart2.Series[0].Points.Count; p++)//Planteles
                {
                    int Alumnos = (int)tempXPlantel.Skip(p).Take(1).Sum(x => x.R0 + x.R1 + x.R2 + x.R3 + x.R4);
                    for (int s = 0; s < this.Chart2.Series.Count; s++)
                    {
                        double promZona = (this.Chart1.Series[s].Points[this.ddlZonas.SelectedIndex - 1].YValues[0]) / 100;
                        double EnEstatusP = (double)this.Chart2.Series[s].Points[p].YValues[0] / 100;
                        int EnEstatusA = (int)((double)Alumnos * EnEstatusP);
                        this.Chart2.Series[s].Points[p].ToolTip = string.Format("Alumnos: {0} ({1:P})\nPromedio de Zona: {2:P}", EnEstatusA, EnEstatusP, promZona);
                    }
                }
            }
        }

    }

    private void MostrarBitacora()
    {
        string Ciclo = this.ddlCiclo.SelectedValue;
        if (this.Ciclo == Ciclo)
            return;

        this.Ciclo = Ciclo;
        DBEstadisticas.Escolar db = new DBEstadisticas.Escolar();
        var temp = (from P in db.Bitacora
                    where P.Ciclo == Ciclo
                    group P by new { P.Z, P.P } into g
                    select new { Z = g.Key.Z, P = g.Key.P, Fecha = g.Max(x => x.Fecha) }).OrderByDescending(x => x.Fecha).ToList();

        System.Collections.ArrayList lst = new System.Collections.ArrayList();
        lst.Add((from P in temp
                 select new { Descripcion = "Más Reciente", P.Z, P.P, P.Fecha }).FirstOrDefault());
        lst.Add((from P in temp
                 select new { Descripcion = "Más Antiguo", P.Z, P.P, P.Fecha }).LastOrDefault());

        if (lst[0] != null)
        {
            this.gvInfoBitacora.DataSource = lst;
            this.gvInfoBitacora.DataBind();
        }
    }

    protected void btninfoBitacora_Click(object sender, EventArgs e)
    {
        string Ciclo = this.ddlCiclo.SelectedValue;
        DBEstadisticas.Escolar db = new DBEstadisticas.Escolar();

        var Bitacora = (from P in db.Bitacora
                        where P.Ciclo == Ciclo
                        group P by new { P.Z, P.P, P.Descripcion } into g
                        select new { Z = g.Key.Z, P = g.Key.P, g.Key.Descripcion, Fecha = g.Max(x => x.Fecha) }).OrderByDescending(x => x.Fecha).ToList();

        if (Bitacora.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');", "No hay registros encontrados"), true);
            return;
        }

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Reportes/rptEstBitacora01.rpt"));
        rptDoc.SetDataSource(Bitacora);
        rptDoc.SetParameterValue("Ciclo", Ciclo);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        rptDoc.Close();
        rptDoc.Dispose();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "inline; filename=rptEstBitacora01.xls");
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        stream.Close();
        stream.Dispose();
    }

    private decimal Porcentaje(int Zona,int Plantel)
    {
        decimal Reprobadas = -1;
        mySQLTemp.mySQL db = new mySQLTemp.mySQL();

        DbConnection cnn = db.Connection;

        try
        {
            DbCommand cmd = cnn.CreateCommand();
            DbParameter param;
            cmd.CommandText = "mySQL.Porcentaje_Captura";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            param = cmd.CreateParameter();
            param.ParameterName = "Plantel";
            param.Value = Plantel;
            cmd.Parameters.Add(param);
            
            param = cmd.CreateParameter();
            param.ParameterName = "Zona";
            param.Value = Zona;
            cmd.Parameters.Add(param);
            
            cnn.Open();

            object res = (object)cmd.ExecuteScalar();
            Reprobadas = decimal.Parse(res.ToString());
        }
        catch
        {
            return -2;
        }
        finally
        {
            cnn.Close();
        }

        return Reprobadas;
    }


    protected void ddlPlanteles_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
    protected void Percent_TextChanged(object sender, EventArgs e)
    {

    }
}
