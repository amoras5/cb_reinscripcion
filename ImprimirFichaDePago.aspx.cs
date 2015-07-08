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

public partial class ImprimirFichaDePago : System.Web.UI.Page
{

    public int AlumnoID
    {
        get
        {
            object AlumnoID = Session["AlumnoID"] ?? 0;
            return (int)AlumnoID;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.AlumnoID == 0)
            Response.Redirect("Default.aspx");
        //TODO1: Que redireccione a insc o reinsc segun sea el caso

        this.Imprimir(this.AlumnoID);
    }

    private void Imprimir(int AlumnoID)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var Pago = (from P in db.vwRPTPagosEncabezados join D in db.vwRPTPagosDetalles on P.PagoID equals D.PagoID
                    where P.AlumnoID == AlumnoID && P.Ciclo == Utils.CicloActual && (P.Estatus != 0)
                    && D.Clave == "A01"
                    select P).FirstOrDefault();

        if (Pago == null)
        {
            this.lblMensaje.Text = "No se ha encontrado la ficha de pago.<br />Nota: Para poder generar la Ficha de Pago, debes de estar validado.";
            return;
        }
        else if (Pago.Estatus == 3) {

            Alumno Alumno = (from A in db.Alumnos
                             where A.AlumnoID == AlumnoID
                             select A).FirstOrDefault();

            this.lblMensaje.Text = "Felicidades. Tú ficha ha sido pagada y ya te encuentras inscrito con matrícula: " + Alumno.Matricula;
            return;

        
        }

        var Detalle = (from D in db.vwRPTPagosDetalles
                       where D.PagoID == Pago.PagoID
                       select D).ToList();

        //Actualiza el estatus del pago a 2:Impreso
        db.spEXEPagosCambiaEstatus(AlumnoID, Pago.PagoID, 2, Utils.CicloActual);

        ArrayList col = new ArrayList();
        col.Add(Pago);
        ReportDocument rptDoc = new ReportDocument();

        rptDoc.Load(Server.MapPath("Reportes/rptFichaDePago.rpt"));
        //set dataset to the report viewer.
        rptDoc.SetDataSource(col);
        rptDoc.Subreports["Detalle"].SetDataSource(Detalle);
        rptDoc.Subreports["Detalle2"].SetDataSource(Detalle);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        rptDoc.Close();
        rptDoc.Dispose();
        Response.Clear();
        Response.ContentType = @"Application/pdf";
        Response.AddHeader("Content-Disposition", "inline; filename=FichaDePago.pdf");
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        stream.Dispose();
    }

}