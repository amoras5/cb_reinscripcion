using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data.Common;
public partial class Solicitud : System.Web.UI.Page
{

    public string Matricula
    {
        get
        {
            object Matricula = Session["Matricula"] ?? "";
            return Matricula.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Matricula == "")
        {
            Response.Redirect("Default.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {
            DBEscolarDataContext db = new DBEscolarDataContext();

            Alumno Alumno = Session["Alumno"] as Alumno ?? (from A in db.Alumnos
                                                            where A.Matricula == this.Matricula
                                                            select A).FirstOrDefault();

            //Si no esta en la sesion, que lo cargue
            AlumnoReinsc Alu = Session["AlumnoReinsc"] as AlumnoReinsc ?? (from A in db.AlumnoReinscs
                                                                           where A.Matricula == this.Matricula
                                                                           select A).FirstOrDefault();

            //if (Alumno == null || Alumno.Estatus == 0)
            //{
            //    Response.Redirect("PlazoFinalizado.aspx");
            //    return;
            //}

            if (Alumno == null) //Carga solo la informacion desde AlumnosReinsc
            {
                //Poner la informacion leida desde AlumnosReinsc (O de la RENAPO)
                this.txtCURP.Text = Alu.CURP;
                this.txtApPaterno.Text = Alu.ApPaterno;
                this.txtApMaterno.Text = Alu.ApMaterno;
                this.txtNombre.Text = Alu.Nombre;
                this.ddlSexo.SelectedValue = Alu.Sexo.ToString();
                this.txtFecNac.Text = Alu.FechaDeNac.ToShortDateString();

                this.CargarMunicipios();
                return;
            }

            //Si es un alumno que existe en Alumnos pero no ha llenado y no coincide el CURP con el de AlumnosReinsc
            //pasar la info de la RENAPO a Alumno
            if (Alumno.Estatus == 0 && Alumno.CURP != Alu.CURP)
            {
                Alumno.CURP = Alu.CURP;
                Alumno.ApPaterno = Alu.ApPaterno;
                Alumno.ApMaterno = Alu.ApMaterno;
                Alumno.Nombre = Alu.Nombre;
                Alumno.Sexo = Alu.Sexo;
                Alumno.FechaNac = Alu.FechaDeNac;
            }

            if (Alumno.Estatus >= 2) //Ya lo validaron y no puede modificar nada
            {
                this.pnlInfo.Visible = true;
                this.pnlSolicitud.Visible = false;

                Session["AlumnoID"] = Alumno.AlumnoID;//Guardar el AlumnoID en la sesion para que pueda imprimir la ficha de pago

                //if (Alumno.Estatus == 1)//Ya llenó la solicitud
                //{
                //    int Reprobadas = this.MateriasReprobadas(Alumno.Matricula, CicloAnterior);//TODO??
                //    if (this.PasaValidacionAutomatica(Reprobadas))
                //    {
                //        if (Pagos.GenerarReciboDePago(Alumno, CicloActual, true))
                //            Alumno.Estatus = 2;
                //        //Response.Redirect("ImprimirFichaDePago.aspx");
                //        //return;
                //    }
                //}

                if (Alumno.Estatus == 2)
                    this.lblInfo.Text = "Para imprimir tu Ficha de Pago, da click <a href='ImprimirFichaDePago.aspx'>AQUÍ</a>";

                if (Alumno.Estatus == 3)
                    this.lblInfo.Text = "Para reimprimir tu Ficha de Pago, da click <a href='ImprimirFichaDePago.aspx'>AQUÍ</a>.<br />Si ya pagaste, espera de 1 a 2 dias hábiles para que tu pago sea procesado.";

                if (Alumno.Estatus == 4)
                    this.lblInfo.Text = "Tu pago ya ha sido recibido, gracias.";

                return;
            }

            //Poner la informacion leida desde Alumnos
            this.txtCURP.Text = Alumno.CURP;
            this.txtApPaterno.Text = Alumno.ApPaterno;
            this.txtApMaterno.Text = Alumno.ApMaterno;
            this.txtNombre.Text = Alumno.Nombre;
            this.ddlSexo.SelectedValue = Alumno.Sexo.ToString();
            this.txtFecNac.Text = Alumno.FechaNac.ToShortDateString();

            this.txtTelefono.Text = Alumno.Telefono;
            this.txtCelular.Text = Alumno.Celular;
            this.txtCorreo.Text = Alumno.Correo;

            this.txtDirCalle.Text = Alumno.Calle;
            this.txtDirNumero.Text = Alumno.Numero;
            this.txtDirCP.Text = Alumno.CodigoPostal.ToString();
            if (!string.IsNullOrEmpty(Alumno.ColoniaTXT))
            {
                if (Alumno.ColoniaID != 0)
                {
                    this.ddlColonia.Items.Add(new ListItem(Alumno.ColoniaTXT, Alumno.ColoniaID.ToString()));
                    this.ddlColonia.SelectedValue = Alumno.ColoniaID.ToString();
                    this.txtDirColonia.Text = Alumno.ColoniaTXT;
                }
            }

            this.ddlEstadoCivil.SelectedValue = Alumno.EstadoCivil.ToString();
            this.ddlOcupacion.SelectedValue = Alumno.Ocupacion.ToString();
            this.ddlTipoBeca.SelectedValue = Alumno.TipoBeca.ToString();
            this.ddlTipoSangre.SelectedValue = Alumno.TipoSangre.ToString();

            this.chkAlergias.Checked = !string.IsNullOrEmpty(Alumno.Alergias);
            this.txtAlergias.Visible = this.chkAlergias.Checked;
            this.txtAlergias.Text = Alumno.Alergias;
            this.chkEnfCronicas.Checked = !string.IsNullOrEmpty(Alumno.EnfermedadesCronicas);
            this.txtEnfCronicas.Visible = this.chkEnfCronicas.Checked;
            this.txtEnfCronicas.Text = Alumno.EnfermedadesCronicas;
            this.chkCapDiferentes.Checked = !string.IsNullOrEmpty(Alumno.CapacidadesDiferentes);
            this.txtCapDiferentes.Visible = this.chkCapDiferentes.Checked;
            this.txtCapDiferentes.Text = Alumno.CapacidadesDiferentes;

            this.chkIMSS.Checked = Alumno.DIMSS;
            this.chkSeguro.Checked = Alumno.DSeguroPopular;
            this.chkISSSTE.Checked = Alumno.DISSSTE;
            this.chkPrivado.Checked = Alumno.DPrivado;

            var Intereses = this.CargarTiposDeInteres();
            var InteresSeleccionado = Intereses.Find(x => x.TipoDeInteresID == Alumno.IntPers1);
            if (InteresSeleccionado != null)
            {
                //Seleccionar el combo de categorias
                this.ddlInt1.SelectedValue = InteresSeleccionado.Interes.ToString();
                //Llenar el combo de intereses y seleccionar el correspondiente
                this.ddlIntDet1.Items.Clear();
                this.ddlIntDet1.DataSource = Intereses.Where(x => x.Interes == InteresSeleccionado.Interes);
                this.ddlIntDet1.DataValueField = "TipoDeInteresID";
                this.ddlIntDet1.DataTextField = "Descripcion";
                this.ddlIntDet1.DataBind();
                this.ddlIntDet1.SelectedValue = Alumno.IntPers1.ToString();
            }
            this.txtIntOtro1.Text = Alumno.IntPersOtros1;

            InteresSeleccionado = Intereses.Find(x => x.TipoDeInteresID == Alumno.IntPers2);
            if (InteresSeleccionado != null)
            {
                //Seleccionar el combo de categorias
                this.ddlInt2.SelectedValue = InteresSeleccionado.Interes.ToString();
                //Llenar el combo de intereses y seleccionar el correspondiente
                this.ddlIntDet2.Items.Clear();
                this.ddlIntDet2.DataSource = Intereses.Where(x => x.Interes == InteresSeleccionado.Interes);
                this.ddlIntDet2.DataValueField = "TipoDeInteresID";
                this.ddlIntDet2.DataTextField = "Descripcion";
                this.ddlIntDet2.DataBind();
                this.ddlIntDet2.SelectedValue = Alumno.IntPers2.ToString();
            }
            this.txtIntOtro2.Text = Alumno.IntPersOtros2;

            this.rblViveCon.SelectedValue = Alumno.ViveCon.ToString();
            this.OcultaMuestraTabs(Alumno.ViveCon.ToString());
            this.ddlViveConTutor.SelectedValue = Alumno.ViveConTutor.ToString();
            this.rblTipoDeCasa.SelectedValue = Alumno.TipoVivienda.ToString();
            this.rblIngresoMensual.SelectedValue = Alumno.IngresoMensual.ToString();
            this.ddlIntegrantes.SelectedValue = Alumno.NumIntegrantes.ToString();
            this.chkTienePC.Checked = Alumno.TienePC;
            this.chkTieneInternet.Checked = Alumno.TieneInternet;

            if (!string.IsNullOrEmpty(Alumno.SecundariaTXT))
            {
                if (Alumno.SecundariaID != 0)
                {
                    this.ddlSecSecundaria.Items.Add(new ListItem(Alumno.SecundariaTXT, Alumno.SecundariaID.ToString()));
                    this.ddlSecSecundaria.SelectedValue = Alumno.SecundariaID.ToString();
                    this.txtSecundaria.Text = Alumno.SecundariaTXT;
                }
            }

            this.txtPadreApPaterno.Text = Alumno.PadreApPaterno;
            this.txtPadreApMaterno.Text = Alumno.PadreApMaterno;
            this.txtPadreNombre.Text = Alumno.PadreNombre;
            this.txtPadreTelefono.Text = Alumno.PadreTelefono;
            this.ddlPadreOcupacion.SelectedValue = Alumno.PadreOcupacion == null ? "-1" : Alumno.PadreOcupacion.ToString();
            this.txtPadreEmpresa.Text = Alumno.PadreEmpresa;

            this.txtMadreApPaterno.Text = Alumno.MadreApPaterno;
            this.txtMadreApMaterno.Text = Alumno.MadreApMaterno;
            this.txtMadreNombre.Text = Alumno.MadreNombre;
            this.txtMadreTelefono.Text = Alumno.MadreTelefono;
            this.ddlMadreOcupacion.SelectedValue = Alumno.MadreOcupacion == null ? "-1" : Alumno.MadreOcupacion.ToString();
            this.txtMadreEmpresa.Text = Alumno.MadreEmpresa;

            this.CargarMunicipios();
            return;
        }
    }

    private void CargarMunicipios()
    {
        object Municipios = Cache["Municipios"];
        if (Municipios == null)
        {
            DBEscolarDataContext db = new DBEscolarDataContext();
            Municipios = (from M in db.Municipios
                          where M.MunicipioID >= 1 && M.MunicipioID <= 18
                          orderby M.Nombre
                          select new { ID = M.MunicipioID, M.Nombre }).ToList();
            Cache.Insert("Municipios", Municipios, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        //Combo de municipios en secundaria
        Utils.InicializaCombo(this.ddlSecMunicipio);
        this.ddlSecMunicipio.DataSource = Municipios;
        this.ddlSecMunicipio.DataBind();
        ddlSecMunicipio.Items.Add(new ListItem("OTRO ESTADO", "0"));
    }

    private List<DBEscolar.TipoDeInteres> CargarTiposDeInteres()
    {
        object TiposDeInteres = Cache["TiposDeInteres"];
        if (TiposDeInteres == null)
        {
            DBEscolarDataContext dbInt = new DBEscolarDataContext();
            TiposDeInteres = (from I in dbInt.TipoDeInteres
                              select I).ToList();
            Cache.Insert("TiposDeInteres", TiposDeInteres, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        return (List<DBEscolar.TipoDeInteres>)TiposDeInteres;
    }

    protected void btnMostrarColonias_Click(object sender, EventArgs e)
    {
        BuscarColonias();
    }

    private void BuscarColonias()
    {
        if (this.txtDirCP.Text == "")
            return;

        DBEscolarDataContext db = new DBEscolarDataContext();
        var res = from C in db.Colonias
                  where C.CodigoPostal == this.txtDirCP.Text
                  orderby C.Nombre
                  select new { ID = C.ColoniaID, Nombre = string.Format("{0} ({1})", C.Nombre, C.Tipo) };

        DropDownList ddl = this.ddlColonia;
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Seleccionar:", "-1"));
        ddl.Items.Add(new ListItem("=== NO APARECE EN LA LISTA ===", "0"));

        ddl.DataSource = res;
        ddl.DataTextField = "Nombre";
        ddl.DataValueField = "ID";
        ddl.DataBind();
    }

    protected void ddlInt1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();

        var res = from I in db.TipoDeInteres
                  where I.Interes == byte.Parse(this.ddlInt1.SelectedValue.ToString())
                  select new { ID = I.TipoDeInteresID, Nombre = I.Descripcion };

        DropDownList ddl = this.ddlIntDet1;
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Seleccionar:", "-1"));

        ddl.DataSource = res;
        ddl.DataTextField = "Nombre";
        ddl.DataValueField = "ID";
        ddl.DataBind();
    }

    protected void ddlInt2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();

        var res = from I in db.TipoDeInteres
                  where I.Interes == byte.Parse(this.ddlInt2.SelectedValue.ToString())
                  select new { ID = I.TipoDeInteresID, Nombre = I.Descripcion };

        DropDownList ddl = this.ddlIntDet2;
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Seleccionar:", "-1"));

        ddl.DataSource = res;
        ddl.DataTextField = "Nombre";
        ddl.DataValueField = "ID";
        ddl.DataBind();
    }

    protected void rblViveCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.OcultaMuestraTabs(this.rblViveCon.SelectedValue);
    }

    private void OcultaMuestraTabs(string ViveCon)
    {
        this.tabPadre.Visible = this.tabMadre.Visible = true;
        this.pnlViveConTutor.Visible = false;
        this.ddlViveConTutor.SelectedValue = "-1";

        switch (ViveCon)
        {
            case "2": //Padre
                this.tabMadre.Visible = false;
                break;

            case "1": // Madre
                this.tabPadre.Visible = false;
                break;

            case "3": //Ambos

                break;

            case "4": //Tutor
                this.tabMadre.Visible = false;
                this.pnlViveConTutor.Visible = true;
                break;
        }
    }

    protected void ddlSecMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();

        var res = from L in db.Localidads
                  where L.MunicipioID == int.Parse(this.ddlSecMunicipio.SelectedValue)
                    && L.Secundarias.Count > 0
                  orderby L.Nombre
                  select new { ID = L.LocalidadID, Nombre = L.Nombre };

        DropDownList ddl = this.ddlSecLocalidad;
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Seleccionar:", "-1"));
        ddl.Items.Add(new ListItem("=== NO APARECE EN LA LISTA ===", "0"));

        ddl.DataSource = res;
        ddl.DataTextField = "Nombre";
        ddl.DataValueField = "ID";
        ddl.DataBind();
    }

    protected void ddlSecLocalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();

        var res = from S in db.Secundarias
                  where S.LocalidadID == int.Parse(this.ddlSecLocalidad.SelectedValue)
                  orderby S.Nombre
                  select new { ID = S.SecundariaID, Nombre = string.Format("{0} - {1} ({2})", S.ClaveCCT, S.Nombre, S.Turno == 1 ? "Matutino" : "Vespertino") };

        DropDownList ddl = this.ddlSecSecundaria;
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Seleccionar:", "-1"));
        ddl.Items.Add(new ListItem("=== NO APARECE EN LA LISTA ===", "0"));

        ddl.DataSource = res;
        ddl.DataTextField = "Nombre";
        ddl.DataValueField = "ID";
        ddl.DataBind();
    }

    protected void ddlSecSecundaria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlSecSecundaria.SelectedValue == "0")
        {
            this.txtSecundaria.Text = "Escribe aquí el nombre de la secundaria";
            this.txtSecundaria.Visible = true;
        }
        else
        {
            this.txtSecundaria.Text = this.ddlSecSecundaria.SelectedItem.Text;
            this.txtSecundaria.Visible = false;
        }
    }

    protected void ddlColonia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlColonia.SelectedValue == "0")
        {
            this.txtDirColonia.Text = "Escribe aquí el nombre de la colonia";
            this.txtDirColonia.Visible = true;
        }
        else
        {
            this.txtDirColonia.Text = this.ddlColonia.SelectedItem.Text;
            this.txtDirColonia.Visible = false;
        }
    }

    protected void chkAlergias_CheckedChanged(object sender, EventArgs e)
    {
        this.txtAlergias.Visible = this.chkAlergias.Checked;
        this.txtAlergias.Text = this.chkAlergias.Checked ? "EXPLIQUE:" : "";
    }

    protected void chkEnfCronicas_CheckedChanged(object sender, EventArgs e)
    {
        this.txtEnfCronicas.Visible = this.chkEnfCronicas.Checked;
        this.txtEnfCronicas.Text = this.chkEnfCronicas.Checked ? "EXPLIQUE:" : "";
    }

    protected void chkCapDiferentes_CheckedChanged(object sender, EventArgs e)
    {
        this.txtCapDiferentes.Visible = this.chkCapDiferentes.Checked;
        this.txtCapDiferentes.Text = this.chkCapDiferentes.Checked ? "EXPLIQUE:" : "";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
            this.Guardar();
    }

    private void Guardar()
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var Alumno = (from A in db.Alumnos
                      where A.Matricula == this.Matricula
                      select A).FirstOrDefault();

        if (Alumno == null) //NO existe en Alumnos
        {
            //Si no esta en la sesion, que lo cargue
            AlumnoReinsc Alu = Session["AlumnoReinsc"] as AlumnoReinsc ?? (from A in db.AlumnoReinscs
                                                                           where A.Matricula == this.Matricula
                                                                           select A).FirstOrDefault();

            if (Alu == null) //NO EXISTE, nunca debe de pasar, si no, no hubiera pasado Accesar
                return;

            Alumno = new Alumno();
            Alumno.PlantelID = Alu.PlantelID;
            Alumno.Turno = Alu.Turno;
            Alumno.Grupo = Alu.Grupo;
            Alumno.Semestre = Alu.Semestre;
            Alumno.Matricula = Alu.Matricula;

            Alumno.FechaDeAlta = DateTime.Now;
            db.Alumnos.InsertOnSubmit(Alumno);
        }
        else if (Alumno.Estatus == 0 && Alumno.Semestre < 6)//Si ya esta en alumnos pero esta actualizando
        {
            Alumno.Semestre += 1;
            Alumno.Grupo += 100;
        }

        Alumno.CURP = this.txtCURP.Text.ToUpper();
        Alumno.ApPaterno = this.txtApPaterno.Text.ToUpper();
        Alumno.ApMaterno = this.txtApMaterno.Text.ToUpper();
        Alumno.Nombre = this.txtNombre.Text.ToUpper();
        Alumno.Sexo = char.Parse(this.ddlSexo.SelectedValue);
        Alumno.FechaNac = DateTime.Parse(this.txtFecNac.Text);

        Alumno.Telefono = this.txtTelefono.Text.ToUpper();
        Alumno.Celular = this.txtCelular.Text.ToUpper();
        Alumno.Correo = this.txtCorreo.Text.ToLower();

        Alumno.Calle = this.txtDirCalle.Text.ToUpper();
        Alumno.Numero = this.txtDirNumero.Text.ToUpper();
        Alumno.CodigoPostal = int.Parse(this.txtDirCP.Text);
        Alumno.ColoniaID = int.Parse(this.ddlColonia.SelectedValue);
        Alumno.ColoniaTXT = Alumno.ColoniaID > 0 ? this.ddlColonia.SelectedItem.Text.ToUpper() : this.txtDirColonia.Text.ToUpper();

        Alumno.EstadoCivil = byte.Parse(this.ddlEstadoCivil.SelectedValue);
        Alumno.Ocupacion = byte.Parse(this.ddlOcupacion.SelectedValue);
        Alumno.TipoBeca = byte.Parse(this.ddlTipoBeca.SelectedValue);
        Alumno.TipoSangre = byte.Parse(this.ddlTipoSangre.SelectedValue);
        Alumno.Alergias = this.chkAlergias.Checked ? this.txtAlergias.Text.ToUpper() : null;
        Alumno.EnfermedadesCronicas = this.chkEnfCronicas.Checked ? this.txtEnfCronicas.Text.ToUpper() : null;
        Alumno.CapacidadesDiferentes = this.chkCapDiferentes.Checked ? this.txtCapDiferentes.Text.ToUpper() : null;

        Alumno.DIMSS = this.chkIMSS.Checked;
        Alumno.DSeguroPopular = this.chkSeguro.Checked;
        Alumno.DISSSTE = this.chkISSSTE.Checked;
        Alumno.DPrivado = this.chkPrivado.Checked;

        Alumno.IntPers1 = byte.Parse(this.ddlIntDet1.SelectedValue);
        Alumno.IntPersOtros1 = this.txtIntOtro1.Text.ToUpper();
        Alumno.IntPers2 = byte.Parse(this.ddlIntDet2.SelectedValue);
        Alumno.IntPersOtros2 = this.txtIntOtro2.Text.ToUpper();

        Alumno.ViveCon = byte.Parse(this.rblViveCon.SelectedValue);
        if (this.ddlViveConTutor.SelectedValue == "-1")
            Alumno.ViveConTutor = null;
        else
            Alumno.ViveConTutor = byte.Parse(this.ddlViveConTutor.SelectedValue);
        Alumno.TipoVivienda = byte.Parse(this.rblTipoDeCasa.SelectedValue);
        Alumno.IngresoMensual = byte.Parse(this.rblIngresoMensual.SelectedValue);
        Alumno.NumIntegrantes = byte.Parse(this.ddlIntegrantes.SelectedValue);
        Alumno.TienePC = this.chkTienePC.Checked;
        Alumno.TieneInternet = this.chkTieneInternet.Checked;

        Alumno.SecundariaID = int.Parse(this.ddlSecSecundaria.SelectedValue);
        Alumno.SecundariaTXT = Alumno.SecundariaID > 0 ? this.ddlSecSecundaria.SelectedItem.Text.ToUpper() : this.txtSecundaria.Text.ToUpper();

        if (this.tabPadre.Visible)
        {
            Alumno.PadreApPaterno = this.txtPadreApPaterno.Text.ToUpper();
            Alumno.PadreApMaterno = this.txtPadreApMaterno.Text.ToUpper();
            Alumno.PadreNombre = this.txtPadreNombre.Text.ToUpper();
            Alumno.PadreTelefono = this.txtPadreTelefono.Text.ToUpper();
            Alumno.PadreOcupacion = byte.Parse(this.ddlPadreOcupacion.SelectedValue);
            Alumno.PadreEmpresa = this.txtPadreEmpresa.Text.ToUpper();
        }
        else
        {
            Alumno.PadreApPaterno = Alumno.PadreApMaterno = Alumno.PadreNombre = Alumno.PadreTelefono = Alumno.PadreEmpresa = null;
            Alumno.PadreOcupacion = null;
        }

        if (this.tabMadre.Visible)
        {
            Alumno.MadreApPaterno = this.txtMadreApPaterno.Text.ToUpper();
            Alumno.MadreApMaterno = this.txtMadreApMaterno.Text.ToUpper();
            Alumno.MadreNombre = this.txtMadreNombre.Text.ToUpper();
            Alumno.MadreTelefono = this.txtMadreTelefono.Text.ToUpper();
            Alumno.MadreOcupacion = byte.Parse(this.ddlMadreOcupacion.SelectedValue);
            Alumno.MadreEmpresa = this.txtMadreEmpresa.Text.ToUpper();
        }
        else
        {
            Alumno.MadreApPaterno = Alumno.MadreApMaterno = Alumno.MadreNombre = Alumno.MadreTelefono = Alumno.MadreEmpresa = null;
            Alumno.MadreOcupacion = null;
        }

        Alumno.EntidadFed = "";
        Alumno.FechaDeModif = DateTime.Now;
        Alumno.Estatus = 1;

        db.SubmitChanges();
        this.TabContainer1.ActiveTabIndex = 0;

        //this.GenerarPDF();

        //int Reprobadas = this.MateriasReprobadas(Alumno.Matricula, Utils.CicloAnterior);
        //if (this.PasaValidacionAutomatica(Reprobadas))
        //{

        int folioPa=0;
        Plantel P = new Plantel();
        if (Pagos.GenerarReciboDePago(Alumno, Utils.CicloActual, true, out folioPa,out P))
        {
            Session["AlumnoID"] = Alumno.AlumnoID;
            try
            {

                Activa_Correo(Alumno.Matricula, folioPa, P.Zona, P.plantel);


            }
            catch (Exception e) { }
            //Guardar el AlumnoID en la sesion para que pueda imprimir la ficha de pago
            Response.Redirect("ImprimirFichaDePago.aspx");

          

            return;
        }
        //}

        this.GenerarPDF();
    }

    protected void btnReimprimir_Click(object sender, EventArgs e)
    {
        this.GenerarPDF();
    }

    private void GenerarPDF()
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var Alumno = (from A in db.vwRPTReinscripcions
                      where A.Matricula == this.Matricula
                      select A).FirstOrDefault();

        if (Alumno == null)//No se encontro
            return;


        System.Collections.ArrayList col = new System.Collections.ArrayList();
        col.Add(Alumno);
        ReportDocument rptDoc = new ReportDocument();

        rptDoc.Load(Server.MapPath("Reportes/rptReinscripcion.rpt"));
        //set dataset to the report viewer.
        rptDoc.SetDataSource(col);

        MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        rptDoc.Close();
        rptDoc.Dispose();
        Response.Clear();
        Response.ContentType = @"Application/pdf";
        Response.AddHeader("Content-Disposition", "inline; filename=Solicitud.pdf");
        // Response.AddHeader("Content-Disposition", "attachment; filename=File.pdf");
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        stream.Dispose();
    }

    private void AlertToPage(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');", msg), true);
    }




    private int MateriasReprobadas(string Matricula, string Ciclo)
    {
        int Reprobadas = -1;
        mySQLTemp.mySQL db = new mySQLTemp.mySQL();

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
            param.Value = Ciclo;
            cmd.Parameters.Add(param);
            cnn.Open();

            object res = (object)cmd.ExecuteScalar();
            Reprobadas = int.Parse(res.ToString());
        }
        catch
        { }
        finally
        {
            cnn.Close();
        }

        return Reprobadas;
    }

    public void Activa_Correo(string matricula, int folio,int Zona,int plantel) {


        DbCorreosDataContext dbCorreos = new DbCorreosDataContext();

        Correos Correo = (from C in dbCorreos.Correos
                          where C.matricula.ToString() == matricula
                          select C).FirstOrDefault();
        if (Correo != null)
        {
            Correo.situacion = -1;
            Correo.folio = folio;
            dbCorreos.SubmitChanges();
        }
        else {


            string cuenta = Zona.ToString() + plantel.ToString().PadLeft(3, '0') + folio.ToString();

            Correo = new Correos();
            Correo.situacion = -2;
            Correo.matricula = Convert.ToInt32(matricula);
            Correo.cuenta = cuenta;
            Correo.folio = folio;

            dbCorreos.Correos.InsertOnSubmit(Correo);

            dbCorreos.SubmitChanges();
        
        }
    }

    private bool PasaValidacionAutomatica(int MateriasReprobadas)
    {
        return MateriasReprobadas >= 0 && MateriasReprobadas <= 3;
    }

}