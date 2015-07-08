using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;

public partial class NuevoIngresoPV : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMensaje.Text = "";

        if (!Page.IsPostBack)
        {
            Session.Abandon();
        }
    }

    protected void txtCURP_TextChanged(object sender, EventArgs e)
    {
        Session.Clear();

        DBEscolarDataContext db = new DBEscolarDataContext();
        var PreInsc = (from A in db.Alumnos_Virtuals
                       where A.CURP == this.txtCURP.Text
                       select A).FirstOrDefault();

        if (PreInsc != null)//Si ya existe
        {
            Session["PreInsc"] = PreInsc;
            Response.Redirect("SolicitudPV.aspx");
            return;
        }
    }

    protected void btnAccesar_Click(object sender, EventArgs e)
    {
        Session.Clear();

        Alumnos_Virtual PreInsc = new Alumnos_Virtual();

        CURP curp = new CURP();
        curp.Curp = this.txtCURP.Text;
       // curp.getFromRENAPO(this.txtCURP.Text);
        curp.Encontrado = true;

        if (curp.Encontrado)
        {
            PreInsc.CURP = curp.Curp;
            PreInsc.Nombre = "";
            PreInsc.ApPaterno ="";
            PreInsc.ApMaterno ="";
            PreInsc.Sexo = char.Parse("H");
            PreInsc.FechaNac = new DateTime();
            PreInsc.EntidadFed = curp.Estado;
            /*if (PreInsc.EntidadFed.Length > 20)//TODO: Mover esta comprobacion a la clase CURP
                PreInsc.EntidadFed = "EXTRANJERO";*/
        }
        else// Si no esta en la RENAPO :(
        {
            DBEscolarDataContext db = new DBEscolarDataContext();
            var curpDB = (from C in db.CURPs
                          where C.curp == this.txtCURP.Text
                          select C).FirstOrDefault();

            if (curpDB == null) //Si tampoco lo encontro en la BD:CURPS
            {
                lblMensaje.Text = "El CURP que proporcionaste no ha sido encontrado ni en el sitio oficial de la RENAPO, y tampoco ha sido encontrado en nuestra BD. <br /> Si estas seguro que es correcto, favor de acudir a control escolar del plantel donde quieres inscribirte.";
                return;
            }

            PreInsc.CURP = curpDB.curp;
            PreInsc.Nombre = curpDB.Nombre;
            PreInsc.ApPaterno = curpDB.ApPaterno;
            PreInsc.ApMaterno = curpDB.ApMaterno;
            PreInsc.Sexo = curpDB.Sexo;
            PreInsc.FechaNac = curpDB.FechaNacimiento;
            PreInsc.EntidadFed = curpDB.Estado;
        }

        PreInsc.PlantelID = 141;
        PreInsc.Turno = 1;
        PreInsc.Estatus = 0;

        Session["PreInsc"] = PreInsc;
        Response.Redirect("SolicitudPV.aspx");
    }

}