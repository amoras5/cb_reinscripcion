using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;
using System.Data.Common;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.lblMensaje.Text = "Por el momento la página se encuentra en mantenimiento, favor de intentar más tarde";
        //this.btnAccesar.Enabled = false;

       this.lblMensaje.Text = "";
        if (!Page.IsPostBack)
            Session.Clear();
    }


    private void Accesar() {

        //Limpiar Sesión
        Session.Clear();


        string CURP = this.txtCURP.Text.ToUpper();
        string Matricula = this.txtMatricula.Text;

        DBEscolarDataContext db = new DBEscolarDataContext();

        var AlumnoReinsc = (from A in db.AlumnoReinscs
                            where A.Matricula == Matricula
                            select A).FirstOrDefault();

        //Checar si existe
        if (AlumnoReinsc == null) 
        {
            this.lblMensaje.Text = "No eres alumno vigente, acude al plantel que desees reinscribirte.";
            return;
        }

        DBEscolar.Alumno Alumno = null;
        if (AlumnoReinsc.Tipo == 1)
        {
            Alumno = (from A in db.Alumnos
                      where A.Matricula == Matricula && A.CURP == CURP
                      select A).FirstOrDefault();

            //Checar que el plantel este habilitado
            
            if (Alumno == null || Alumno.Estatus == 0)
            {
                var Plantel = (from P in db.PlantelesEnReinscripcions
                               where P.PlantelID == AlumnoReinsc.PlantelID
                               select P).FirstOrDefault();

                DateTime Fecha = DateTime.Now;
                if (Plantel == null || Fecha < Plantel.FechaInicial || Fecha > Plantel.FechaLimite)
                {
                    this.lblMensaje.Text = "LO SENTIMOS! De momento no podemos procesar tu solicitud de RE INSCRIPCIÓN,<br />" +
                                           "El periodo de RE INSCRIPCIÓN no está activo en tu plantel.";
                    return;
                }
            }

            //Checar que no tenga adeudos, solo cuando es Estatus = 0
            if (Alumno != null && Alumno.Estatus == 0)
            {
                if (this.TieneRecibosPendientes(Alumno.AlumnoID))
                {
                    this.lblMensaje.Text = "LO SENTIMOS! De momento no podemos procesar tu solicitud de RE INSCRIPCIÓN, TIENES ADEUDOS PENDIENTES, favor de acudir a tu plantel para aclarar tu situación.";
                    return;
                }
            }

            //Checar que no sea baja
            if (Alumno != null && (Alumno.Estatus == 5 || Alumno.Estatus == 6))
            {
                this.lblMensaje.Text = "No eres alumno vigente, acude al plantel que desees reinscribirte.";
                return;
            }

            //Checar que el plantel ya haya subido calificaciones
            if (Alumno == null || Alumno.Estatus == 0)
            {
                var Plantel = (from P in db.Plantels
                               where P.PlantelID == AlumnoReinsc.PlantelID && P.CalificacionesCompletas == true
                               select P).FirstOrDefault();

                if (Plantel == null)
                {
                    this.lblMensaje.Text = "LO SENTIMOS! De momento no podemos procesar tu solicitud de RE INSCRIPCIÓN,<br />" +
                                           "NO ENCONTRAMOS TUS CALIFICACIONES finales del semestre anterior. <br />" +
                                           "POR FAVOR INTENTA MÁS TARDE, o bien, verifica en tu plantel que puede estar pasando.";
                    return;
                }
            }

            //Checar las materias reprobadas, solo cuando es Estatus = 0
            if (AlumnoReinsc.Tipo == 1 && (Alumno == null || Alumno.Estatus == 0))
            {
                int Reprobadas = this.MateriasReprobadas(Matricula);
                if (Reprobadas > 3)
                {
                    this.lblMensaje.Text = "No eres alumno vigente, acude al plantel que desees reinscribirte.";
                    return;
                }
                if (Reprobadas == -1)
                {
                    this.lblMensaje.Text = "No eres alumno vigente, por el momento no tenemos tus calificaciones completas.";
                    return;
                }
                if (Reprobadas == -2)
                {
                    this.lblMensaje.Text = "LO SENTIMOS! Ha ocurrido un problema de conexión, por favor intente nuevamente en un momento.";
                    return;
                }
            }
        }

        if (AlumnoReinsc.CURP != CURP) //NO COINCIDE Matricula y CURP
        {
            //Checar contra los datos de RENAPO
            bool match = false;
            CURP renapo = new CURP();
            //renapo.getFromRENAPO(CURP);

            this.lblMensaje.Text = "La Matricula y el CURP no coinciden con nuestros registros, si los datos son correctos, acude primero a Control Escolar en tu plantel.";
            return;
        }

        Session["Matricula"] = Matricula;
        Session["Alumno"] = Alumno;
        Session["AlumnoReinsc"] = AlumnoReinsc;
        Response.Redirect("Solicitud.aspx");


    
    }



    private int MateriasReprobadas(string Matricula)
    {
        int Reprobadas = 0;

        DBEscolarDataContext db = new DBEscolarDataContext();

        var Repro = (from A in db.Reprobados_Is
                            where A.or_matric == Matricula
                            select A).FirstOrDefault();

        if (Repro == null)
            return 0;

        if (Repro.Incompletas > 0)
            return -1;
        


        return Repro.Reprobadas.Value;
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


    

    #region Eventos

    protected void btnAccesar_Click(object sender, EventArgs e)
    {
        Accesar();
    }

    #endregion


}