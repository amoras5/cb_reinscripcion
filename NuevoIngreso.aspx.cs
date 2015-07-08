using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;

public partial class NuevoIngreso : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMensaje.Text = "";

        if (!Page.IsPostBack)
        {
            Session.Abandon();

            this.CargarMunicipios();
        }
    }

    protected void txtCURP_TextChanged(object sender, EventArgs e)
    {
        Session.Clear();

        DBEscolarDataContext db = new DBEscolarDataContext();
        var PreInsc = (from A in db.PreInscritos
                       where A.CURP == this.txtCURP.Text && A.Ciclo == Utils.CicloPreInscripciones
                       select A).FirstOrDefault();

        if (PreInsc != null)//Si ya existe
        {

                Session["PreInsc"] = PreInsc;
                Response.Redirect("SolicitudPre.aspx");
                return;
            /*
            else {

                Session["AlumnoID"] = PreInsc.AlumnoID;
                Response.Redirect("ImprimirFichaDePago.aspx");
                return;
            
            }*/
        }
    }

    protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var res = from P in db.Plantels
                  where P.Activo && P.MunicipioID == byte.Parse(this.ddlMunicipio.SelectedValue)
                  orderby P.plantel
                  select new { ID = P.PlantelID, Nombre = P.plantel + " - " + P.Descripcion };

        Utils.InicializaCombo(this.ddlPlantel);
        this.ddlPlantel.DataSource = res;
        this.ddlPlantel.DataBind();
    }

    protected void ddlPlantel_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var res = (from P in db.Plantels
                   where P.PlantelID == byte.Parse(this.ddlPlantel.SelectedValue)
                   select new { ID = P.PlantelID, Nombre = P.Descripcion, P.TurnoMatutino, P.TurnoVespertino, P.TurnoNocturno, P.EsEmsad }).FirstOrDefault();

        if (res == null)//Nunca debe de pasar
            return;

        Utils.InicializaCombo(this.ddlTurno);

        if (res.TurnoMatutino)
            this.ddlTurno.Items.Add(new ListItem("Matutino", "1"));
        if (res.TurnoVespertino)
            this.ddlTurno.Items.Add(new ListItem("Vespertino", "2"));
        if (res.TurnoNocturno)
            this.ddlTurno.Items.Add(new ListItem("Nocturno", "3"));
        if (res.EsEmsad)
            this.ddlTurno.Items.Add(new ListItem("Turno Único", "1"));

        //Si solo hay un turno, seleccionarlo automaticamente
        if (this.ddlTurno.Items.Count == 2)
            this.ddlTurno.SelectedIndex = 1;
    }

    protected void btnAccesar_Click(object sender, EventArgs e)
    {
        Session.Clear();

        PreInscrito PreInsc = new PreInscrito();

        CURP curp = new CURP();
       // curp.getFromRENAPO(this.txtCURP.Text);
        curp.Curp = this.txtCURP.Text;

        DBEscolarDataContext db = new DBEscolarDataContext();
        var curpDB = (from C in db.CURPs
                      where C.curp == this.txtCURP.Text 
                      select C).FirstOrDefault();

        if (curpDB != null) //Si tampoco lo encontro en la BD:CURPS
        {
            PreInsc.CURP = curpDB.curp;
            PreInsc.Nombre = curpDB.Nombre;
            PreInsc.ApPaterno = curpDB.ApPaterno;
            PreInsc.ApMaterno = curpDB.ApMaterno;
            PreInsc.Sexo = curpDB.Sexo;
            PreInsc.FechaNac = curpDB.FechaNacimiento;
            PreInsc.EntidadFed = curpDB.Estado;
        }
        else
        {
            /*curp.Encontrado = true;
            PreInsc.CURP = curp.Curp;
            PreInsc.Nombre = "" ;
            PreInsc.ApPaterno = "";
            PreInsc.ApMaterno = "";
            PreInsc.Sexo = char.Parse(" ");
            PreInsc.FechaNac = new DateTime();
            PreInsc.EntidadFed = "";
            if (PreInsc.EntidadFed.Length > 20)//TODO: Mover esta comprobacion a la clase CURP
                PreInsc.EntidadFed = "EXTRANJERO";*/
            lblMensaje.Text = "Tu CURP no puede ser validada o es incorrecta, debes acudir al plantel para poder preinscribirte";
            return;
        }

        PreInsc.PlantelID = byte.Parse(this.ddlPlantel.SelectedValue);
        PreInsc.Turno = byte.Parse(this.ddlTurno.SelectedValue);
        PreInsc.Ciclo = Utils.CicloPreInscripciones;
        PreInsc.Estatus = 0;

        Session["PreInsc"] = PreInsc;
        Response.Redirect("SolicitudPre.aspx");
    }

    private void CargarMunicipios()
    {
        if (Cache["Municipios"] == null)
        {
            DBEscolarDataContext db = new DBEscolarDataContext();
            var res = (from M in db.Municipios
                       where M.MunicipioID >= 1 && M.MunicipioID <= 18
                       orderby M.Nombre
                       select new { ID = M.MunicipioID, M.Nombre }).ToList();
            Cache.Insert("Municipios", res, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        var Municipios = Cache["Municipios"];

        Utils.InicializaCombo(this.ddlMunicipio);
        this.ddlMunicipio.DataSource = Municipios;
        this.ddlMunicipio.DataBind();
    }





}