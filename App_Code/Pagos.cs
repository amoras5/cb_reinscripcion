using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBEscolar;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for Pagos
/// </summary>
public class Pagos
{

    public Pagos()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool GenerarReciboDePago(DBEscolar.Alumno Alumno, string Ciclo, bool AgregarLibros,out int folioPa,out Plantel Pla)
    {
      

        DBIngresosDataContext db = new DBIngresosDataContext();
        var Recibos = (from P in db.Pagos
                       where P.AlumnoID == Alumno.AlumnoID && P.Ciclo == Ciclo && (P.Estatus == 1 || P.Estatus == 2)
                        && P.DetalleDePagos.Count(x => x.Clave == "A01") > 0
                       select P).ToList();

        if (Recibos.Count() != 0)
        {//Ya esta generado{
            folioPa = 0;
            Pla = null;
            return true;
        }

        Pago Pago = new Pago();
        Pago.AlumnoID = Alumno.AlumnoID;
        Pago.Ciclo = Ciclo;
        Pago.Estatus = 1;
        Pago.FechaDeAlta = DateTime.Now;
        Pago.PlantelID = Alumno.PlantelID;
        Pago.Tipo = 0;
        Pago.Comentarios = "Generado por web";

        //Sacar el FOLIO
        DBEscolar.DBEscolarDataContext dbEsc = new DBEscolar.DBEscolarDataContext();
        var Plantel = (from P in dbEsc.Plantels
                       where P.PlantelID == Alumno.PlantelID
                       select P).FirstOrDefault();

        Pla = Plantel;

        if (Plantel == null)
        {//No debe de pasar
            folioPa = 0;
            return false;
        }

        int? Folio =0;
        byte zona = Plantel.Zona;
        byte plantel = Plantel.plantel;
       db.spEXEGetFolio(zona, plantel, ref Folio);

        Pago.Folio = (int)Folio;
        Pago.Referencia = string.Format("{0}{1:000}{2}{3:000000}", zona, plantel, Alumno.Matricula, Folio);
        Pago.Referencia += Utils.getVerificador(Pago.Referencia);

        bool Exonerado = EstaExonerado(Alumno.Matricula, Ciclo); //Matricula para reinscripcion / Folio si es nuevo ingreso

        DetalleDePago Detalle;

        //Cuota Semestral
        Detalle = new DetalleDePago();
        Detalle.Clave = "A01";
        Detalle.Concepto = "Cuota Semestral";
        if (zona == 1 && plantel == 9)
            Detalle.Monto = 163;
        else
            Detalle.Monto = 326;

        if (Exonerado)
        {
            Detalle.Monto = 0;
            Detalle.Concepto += " (EXONERADO)";
        }

        Pago.DetalleDePagos.Add(Detalle);

        //Servicio de Laboratorio y Taller
        Detalle = new DetalleDePago();
        if (!((zona == 1 && (plantel == 115 || plantel == 117 || plantel == 118 || plantel == 127)) ||
           (zona == 2 && (plantel == 69 || plantel == 71)) ||
           (zona == 4 && (plantel == 94 || plantel == 96 || plantel == 98)) ||
           (zona == 5 && plantel == 68)))
        {
            Detalle.Clave = "A20";
            Detalle.Concepto = "Servicio de Laboratorio y Taller";
            Detalle.Monto = 66.50m;
            Pago.DetalleDePagos.Add(Detalle);
        }

        //Cooperación Pro-Mantenimiento del Plantel
        Detalle = new DetalleDePago();
        Detalle.Clave = "A21";
        Detalle.Concepto = "Cooperación Pro-Mantenimiento del Plantel";
        Detalle.Monto = 66.50m;
        Pago.DetalleDePagos.Add(Detalle);

        //Cuota de Seguro Estudiantil
        Detalle = new DetalleDePago();
        Detalle.Clave = "A22";
        Detalle.Concepto = "Cuota de Seguro Estudiantil";
        Detalle.Monto = 40;
        Pago.DetalleDePagos.Add(Detalle);

        //LIBROS
        if (AgregarLibros)
        {
            Detalle = new DetalleDePago();
            switch (Alumno.Semestre)
            {
                
                case 2:
                    Detalle.Clave = "A16";
                    Detalle.Concepto = "PAQUETE DE LIBROS DE 2DO SEMESTRE";
                    Detalle.Monto = 520;
                    break;
                case 4:
                    Detalle.Clave = "A18";
                    Detalle.Concepto = "PAQUETE DE LIBROS DE 4TO SEMESTRE";
                    Detalle.Monto = 520;
                    break;
                case 6:
                    {
                        int cap =  getCap(Alumno);
                       
                        Detalle.Clave = "A25";
                        Detalle.Concepto = "PAQUETE DE LIBROS DE 6TO SEMESTRE";

                        if (cap == 73 || cap == 80 || cap == 81)
                            Detalle.Monto = 578;
                        else Detalle.Monto = 520;
                        break;
                    }
            }
            if(Detalle.Clave != null)
                Pago.DetalleDePagos.Add(Detalle);
        }

        Pago.Total = Pago.DetalleDePagos.Sum(x => x.Monto);
        db.Pagos.InsertOnSubmit(Pago);

        try
        {
            db.SubmitChanges();
        }
        catch (Exception)
        {
            folioPa = 0;
            return false;
        }
        folioPa = Folio.Value;
        return true;
    }

    private static bool EstaExonerado(string Matricula, string Ciclo)
    {
        SqlConnection conn = new SqlConnection("Data Source=mssql.cobaes.edu.mx;Initial Catalog=EscolarWeb;Persist Security Info=True;User ID=sa;Password=Cobaes2011");

        try
        {

            string SSQL = "SELECT * " +
                          "FROM Ex_Solicitudes " +
                          "WHERE Matricula = @Matricula AND Semestre = @Ciclo AND Estatus = 3 ";


            SqlCommand cmd = new SqlCommand(SSQL, conn);
            cmd.Parameters.AddWithValue("@Matricula", Matricula);
            cmd.Parameters.AddWithValue("@Ciclo", Ciclo);

            SqlDataReader areader;

            ArrayList Result = new ArrayList();
            Alumno aux = null;
            conn.Open();
            areader = cmd.ExecuteReader();

            if (areader.Read())
            {
                conn.Close();
                return true;

            }
            conn.Close();
        }
        catch (Exception esx)
        {
            conn.Close();
        }
        return false;
    }


    private static int getCap(Alumno alumno) {

        DBEscolar.DBEscolarDataContext dbEscolar = new DBEscolarDataContext();

      
        int? Rep = (from A in dbEscolar.Alumnos
                   join C in dbEscolar.Capacitacions on new {p = A.PlantelID, g = ((int)A.Grupo) ,t = A.Turno} equals new {
                   p = C.PlantelID, g=(int)C.Grupo,t=C.Turno}
                   where A.Matricula == alumno.Matricula
                   select C.Capacitacion1).FirstOrDefault();

        if (Rep == null)
            return 0;
        return Rep.Value;
    }

}