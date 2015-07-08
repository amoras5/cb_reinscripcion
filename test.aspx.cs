using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data;
using System.Data.Objects;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Exoneraciones.wsExoneraciones ws = new Exoneraciones.wsExoneraciones();
        //bool x = ws.EstaExonerado("1240250001", "2013B");
        //Response.Write(x.ToString());


        //DBEscolar.DBEscolarDataContext db = new DBEscolar.DBEscolarDataContext();
        //var Alumnos = (from A in db.Alumnos
        //               select A).Take(1);

        //Pagos.GenerarReciboDePago2013B(Alumnos, true);

        //JavaScriptSerializer serializer = new JavaScriptSerializer();
        //Response.Write(serializer.Serialize(Alumnos));


        //bool res = TieneMateriasReprobadas("1250530034");
        //Response.Write(res.ToString());

        Response.Write(this.TieneRecibosPendientes(35320).ToString());
    }

    private bool TieneMateriasReprobadas(string Matricula)
    {
        bool TieneReprobadas = true;
        mySQLTemp.mySQL db = new mySQLTemp.mySQL();

        //object res = db.ExecuteFunction<object>("spEXEMateriasReprobadas", new ObjectParameter("cMatricula", "0940320029"), new ObjectParameter("cCiclo", "2013A"));

        DbConnection cnn = db.Connection;

        try
        {
            DbCommand cmd = cnn.CreateCommand();
            DbParameter param;
            cmd.CommandText = "mySQL.spEXEMateriasReprobadas";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            param = cmd.CreateParameter();
            param.ParameterName = "cMatricula";
            param.Value = Matricula;
            cmd.Parameters.Add(param);
            param = cmd.CreateParameter();
            param.ParameterName = "cCiclo";
            param.Value = "2013B";
            cmd.Parameters.Add(param);
            cnn.Open();

            object res = (object)cmd.ExecuteScalar();
            int Mat = int.Parse(res.ToString());
            TieneReprobadas = Mat != 0;
        }
        catch
        { }
        finally
        {
            cnn.Close();
        }

        return TieneReprobadas;
    }

    private bool TieneRecibosPendientes(int AlumnoID)
    {
        using (DBIngresosDataContext dbIngresos = new DBIngresosDataContext())
        {
            var Recibos = (from P in dbIngresos.Pagos
                           where P.AlumnoID == AlumnoID && P.Ciclo != Utils.CicloActual && (P.Estatus == 1 || P.Estatus == 2 || P.Estatus == 5)
                            && P.DetalleDePagos.Count(x => x.Clave == "A01") > 0
                           select P).ToList();

            return Recibos.Count() != 0;
        }
    }

    private void ChecarFichasDeExonerados()
    {
        DBIngresosDataContext dbIng = new DBIngresosDataContext();
        DBEscolar.DBEscolarDataContext dbEsc = new DBEscolar.DBEscolarDataContext();

        var Alumnos = (from A in dbEsc.Alumnos
                      where A.Estatus > 1
                      select A).ToList();
        Exoneraciones.wsExoneraciones ws = new Exoneraciones.wsExoneraciones();
        ArrayList Matriculas = new ArrayList();
        int cont=0;
        foreach (DBEscolar.Alumno Alu in Alumnos)
        {
            cont += 1;
            if (ws.EstaExonerado(Alu.Matricula, "2013B"))
            {
                var Pagos = (from P in dbIng.Pagos
                             where P.AlumnoID == Alu.AlumnoID && P.Ciclo == "2013B" && P.Estatus > 0
                             select P).ToList();

                Matriculas.Add(Alu.Matricula);
            }
        }
        Response.Write(string.Join("','",Matriculas.ToArray()));
    }
}


 //DbConnection cnn = new System.Data.SqlClient.SqlConnection("server=mysql.cobaes.edu.mx;User Id=alexei;password=Cobaes2011;Persist Security Info=True;database=Escolar;");
 //       try
 //       {
 //           DbCommand cmd = cnn.CreateCommand();
 //           DbParameter param;
 //           cmd.CommandText = "spEXEMateriasReprobadas";
 //           cmd.CommandType = System.Data.CommandType.StoredProcedure;
 //           param = cmd.CreateParameter();
 //           param.ParameterName = "cMatricula";
 //           param.Value = Matricula;
 //           cmd.Parameters.Add(param);
 //           param = cmd.CreateParameter();
 //           param.ParameterName = "cCiclo";
 //           param.Value = "2013A";
 //           cmd.Parameters.Add(param);
 //           cnn.Open();

 //           object res = (object)cmd.ExecuteScalar();
 //           int Mat = int.Parse(res.ToString());
 //           TieneReprobadas = Mat != 0;
 //       }