using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

public partial class SolicitudPre : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        PreInscrito PreInsc = (PreInscrito)Session["PreInsc"];

        if (PreInsc == null)//Si no existe la variable en la sesion regresarlo a la pagina de inicio
        {
            Response.Redirect("NuevoIngreso.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {
            if (PreInsc.Estatus > 0) //Ya lo validaron
            {
                //Si ya esta validado, ya debe de haberse pasado a la tabla de Alumnos
                //DBEscolarDataContext db = new DBEscolarDataContext();
                //var Alumno = (from A in db.Alumnos
                //              where A.AlumnoID == PreInsc.AlumnoID
                //              select A).FirstOrDefault();

                this.pnlInfo.Visible = true;
                this.pnlSolicitud.Visible = false;
                this.lblInfo.Text = string.Format("¡Hola {0} {1} {2}!<br />", PreInsc.ApPaterno, PreInsc.ApMaterno, PreInsc.Nombre);
                this.lblInfo.Text += string.Format("Tus datos ya fueron guardados:<br /><strong>CURP: {0}<br />FOLIO: {1}</strong>", PreInsc.CURP, PreInsc.Folio);



                //Session["AlumnoID"] = Alumno.AlumnoID;//Guardar el AlumnoID en la sesion para que pueda imprimir la ficha de pago

                //if (Alumno.Estatus == 2)
                //    this.lblInfo.Text = "Para imprimir tu Ficha de Pago, da click <a href='ImprimirFichaDePago.aspx'>AQUÍ</a>";

                //if (Alumno.Estatus == 3)
                //    this.lblInfo.Text = "Para reimprimir tu Ficha de Pago, da click <a href='ImprimirFichaDePago.aspx'>AQUÍ</a>.<br />Si ya pagaste, espera de 1 a 2 dias hábiles para que tu pago sea procesado.";

                //if (Alumno.Estatus == 4)
                //    this.lblInfo.Text = string.Format("Tu pago ya ha sido recibido, Gracias. Tu Matrícula es: {0}", Alumno.Matricula);

                return;
            }

            //Si esta entrando por primera vez
            if (PreInsc.Estatus == 0 && string.IsNullOrEmpty(PreInsc.Folio))
            {
                this.txtCURP.Text = PreInsc.CURP;
                this.txtApPaterno.Text = PreInsc.ApPaterno;
                this.txtApMaterno.Text = PreInsc.ApMaterno;
                this.txtNombre.Text = PreInsc.Nombre;
                this.ddlSexo.SelectedValue = PreInsc.Sexo.ToString();
                this.txtFecNac.Text = PreInsc.FechaNac.ToShortDateString();

                this.CargarMunicipios();
                this.tabCambioDePlantel.Visible = false;
                return;
            }

            this.txtCURP.Text = PreInsc.CURP;
            this.txtApPaterno.Text = PreInsc.ApPaterno;
            this.txtApMaterno.Text = PreInsc.ApMaterno;
            this.txtNombre.Text = PreInsc.Nombre;
            this.ddlSexo.SelectedValue = PreInsc.Sexo.ToString();
            this.txtFecNac.Text = PreInsc.FechaNac.ToShortDateString();

            this.txtTelefono.Text = PreInsc.Telefono;
            this.txtCelular.Text = PreInsc.Celular;
            this.txtCorreo.Text = PreInsc.Correo;

            this.txtDirCalle.Text = PreInsc.Calle;
            this.txtDirNumero.Text = PreInsc.Numero;
            this.txtDirCP.Text = PreInsc.CodigoPostal.ToString();
            if (!string.IsNullOrEmpty(PreInsc.ColoniaTXT))
            {
                if (PreInsc.ColoniaID != 0)
                {
                    this.ddlColonia.Items.Add(new ListItem(PreInsc.ColoniaTXT, PreInsc.ColoniaID.ToString()));
                    this.ddlColonia.SelectedValue = PreInsc.ColoniaID.ToString();
                    this.txtDirColonia.Text = PreInsc.ColoniaTXT;
                }
            }

            this.ddlEstadoCivil.SelectedValue = PreInsc.EstadoCivil.ToString();
            this.ddlOcupacion.SelectedValue = PreInsc.Ocupacion.ToString();
            this.ddlTipoBeca.SelectedValue = PreInsc.TipoBeca.ToString();
            this.ddlTipoSangre.SelectedValue = PreInsc.TipoSangre.ToString();

            this.chkAlergias.Checked = !string.IsNullOrEmpty(PreInsc.Alergias);
            this.txtAlergias.Visible = this.chkAlergias.Checked;
            this.txtAlergias.Text = PreInsc.Alergias;
            this.chkEnfCronicas.Checked = !string.IsNullOrEmpty(PreInsc.EnfermedadesCronicas);
            this.txtEnfCronicas.Visible = this.chkEnfCronicas.Checked;
            this.txtEnfCronicas.Text = PreInsc.EnfermedadesCronicas;
            this.chkCapDiferentes.Checked = !string.IsNullOrEmpty(PreInsc.CapacidadesDiferentes);
            this.txtCapDiferentes.Visible = this.chkCapDiferentes.Checked;
            this.txtCapDiferentes.Text = PreInsc.CapacidadesDiferentes;

            this.chkIMSS.Checked = PreInsc.DIMSS;
            this.chkSeguro.Checked = PreInsc.DSeguroPopular;
            this.chkISSSTE.Checked = PreInsc.DISSSTE;
            this.chkPrivado.Checked = PreInsc.DPrivado;

            var Intereses = this.CargarTiposDeInteres();
            var InteresSeleccionado = Intereses.Find(x => x.TipoDeInteresID == PreInsc.IntPers1);
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
                this.ddlIntDet1.SelectedValue = PreInsc.IntPers1.ToString();
            }
            this.txtIntOtro1.Text = PreInsc.IntPersOtros1;

            InteresSeleccionado = Intereses.Find(x => x.TipoDeInteresID == PreInsc.IntPers2);
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
                this.ddlIntDet2.SelectedValue = PreInsc.IntPers2.ToString();
            }
            this.txtIntOtro2.Text = PreInsc.IntPersOtros2;

            this.rblViveCon.SelectedValue = PreInsc.ViveCon.ToString();
            this.OcultaMuestraTabs(PreInsc.ViveCon.ToString());
            this.ddlViveConTutor.SelectedValue = PreInsc.ViveConTutor.ToString();
            this.rblTipoDeCasa.SelectedValue = PreInsc.TipoVivienda.ToString();
            this.rblIngresoMensual.SelectedValue = PreInsc.IngresoMensual.ToString();
            this.ddlIntegrantes.SelectedValue = PreInsc.NumIntegrantes.ToString();
            this.chkTienePC.Checked = PreInsc.TienePC;
            this.chkTieneInternet.Checked = PreInsc.TieneInternet;

            if (!string.IsNullOrEmpty(PreInsc.SecundariaTXT))
            {
                if (PreInsc.SecundariaID != 0)
                {
                    this.ddlSecSecundaria.Items.Add(new ListItem(PreInsc.SecundariaTXT, PreInsc.SecundariaID.ToString()));
                    this.ddlSecSecundaria.SelectedValue = PreInsc.SecundariaID.ToString();
                    this.txtSecundaria.Text = PreInsc.SecundariaTXT;
                }
            }

            this.txtPadreApPaterno.Text = PreInsc.PadreApPaterno;
            this.txtPadreApMaterno.Text = PreInsc.PadreApMaterno;
            this.txtPadreNombre.Text = PreInsc.PadreNombre;
            this.txtPadreTelefono.Text = PreInsc.PadreTelefono;
            this.ddlPadreOcupacion.SelectedValue = PreInsc.PadreOcupacion == null ? "-1" : PreInsc.PadreOcupacion.ToString();
            this.txtPadreEmpresa.Text = PreInsc.PadreEmpresa;

            this.txtMadreApPaterno.Text = PreInsc.MadreApPaterno;
            this.txtMadreApMaterno.Text = PreInsc.MadreApMaterno;
            this.txtMadreNombre.Text = PreInsc.MadreNombre;
            this.txtMadreTelefono.Text = PreInsc.MadreTelefono;
            this.ddlMadreOcupacion.SelectedValue = PreInsc.MadreOcupacion == null ? "-1" : PreInsc.MadreOcupacion.ToString();
            this.txtMadreEmpresa.Text = PreInsc.MadreEmpresa;

            //Cambiar de Plantel
            this.ddlPlantelPlantel.Items.Add(new ListItem("Si quieres cambiar de plantel, elige un Municipio", PreInsc.PlantelID.ToString()));

            this.CargarMunicipios();
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

        //Combo de municipios en cambio plantel
        Utils.InicializaCombo(this.ddlPlantelMunicipio);
        this.ddlPlantelMunicipio.DataSource = Municipios;
        this.ddlPlantelMunicipio.DataBind();
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
        var res = from I in this.CargarTiposDeInteres()
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
        var res = from I in this.CargarTiposDeInteres()
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
        this.tabPadre.HeaderText = "Padre";
        this.pnlViveConTutor.Visible = false;
        this.ddlViveConTutor.SelectedValue = "-1";

        switch (ViveCon)
        {
            case "2": //Padre
                this.tabMadre.Visible = false;
                break;

            case "1": //Madre
                this.tabPadre.Visible = false;
                break;

            case "3": //Ambos
                break;

            case "4": //Tutor
                this.tabMadre.Visible = false;
                this.tabPadre.HeaderText = "Tutor";
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

    protected void ddlPlantelMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var res = from P in db.Plantels
                  where P.Activo && P.MunicipioID == byte.Parse(this.ddlPlantelMunicipio.SelectedValue)
                  orderby P.plantel
                  select new { ID = P.PlantelID, Nombre = P.plantel + " - " + P.Descripcion };

        Utils.InicializaCombo(this.ddlPlantelPlantel);
        this.ddlPlantelPlantel.DataSource = res;
        this.ddlPlantelPlantel.DataBind();
    }

    protected void ddlPlantelPlantel_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBEscolarDataContext db = new DBEscolarDataContext();
        var res = (from P in db.Plantels
                   where P.PlantelID == byte.Parse(this.ddlPlantelPlantel.SelectedValue)
                   select new { ID = P.PlantelID, Nombre = P.Descripcion, P.TurnoMatutino, P.TurnoVespertino, P.TurnoNocturno }).FirstOrDefault();

        if (res == null)//Nunca debe de pasar
            return;

        Utils.InicializaCombo(this.ddlPlantelTurno);

        if (res.TurnoMatutino)
            this.ddlPlantelTurno.Items.Add(new ListItem("Matutino", "1"));
        if (res.TurnoVespertino)
            this.ddlPlantelTurno.Items.Add(new ListItem("Vespertino", "2"));
        if (res.TurnoNocturno)
            this.ddlPlantelTurno.Items.Add(new ListItem("Nocturno", "3"));
        if (res.Nombre.ToUpper().Contains("EMSAD"))
            this.ddlPlantelTurno.Items.Add(new ListItem("Turno Único", "4"));

        //Si solo hay un turno, seleccionarlo automaticamente
        if (this.ddlPlantelTurno.Items.Count == 2)
            this.ddlPlantelTurno.SelectedIndex = 1;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
            this.Guardar();
    }

    private void Guardar()
    {
        PreInscrito PreInsc = (PreInscrito)Session["PreInsc"];

        bool EsNuevo = PreInsc.Estatus == 0 && string.IsNullOrEmpty(PreInsc.Folio);

        DBEscolarDataContext db = new DBEscolarDataContext();
        if (!EsNuevo)
        {
            string CURP = PreInsc.CURP;
            PreInsc = (from A in db.PreInscritos
                       where A.CURP == CURP && A.Ciclo == Utils.CicloPreInscripciones
                       select A).FirstOrDefault();
        }

        //PreInsc.CURP = this.txtCURP.Text.ToUpper();
        PreInsc.ApPaterno = this.txtApPaterno.Text.ToUpper();
        PreInsc.ApMaterno = this.txtApMaterno.Text.ToUpper();
        PreInsc.Nombre = this.txtNombre.Text.ToUpper();
        PreInsc.Sexo = char.Parse(this.ddlSexo.SelectedValue);
        PreInsc.FechaNac = DateTime.Parse(this.txtFecNac.Text);
        PreInsc.EntidadFed = "SINALOA";
        PreInsc.Telefono = this.txtTelefono.Text.ToUpper();
        PreInsc.Celular = this.txtCelular.Text.ToUpper();
        PreInsc.Correo = this.txtCorreo.Text.ToLower();

        PreInsc.Calle = this.txtDirCalle.Text.ToUpper();
        PreInsc.Numero = this.txtDirNumero.Text.ToUpper();
        PreInsc.CodigoPostal = int.Parse(this.txtDirCP.Text);
        PreInsc.ColoniaID = int.Parse(this.ddlColonia.SelectedValue);
        PreInsc.ColoniaTXT = PreInsc.ColoniaID > 0 ? this.ddlColonia.SelectedItem.Text.ToUpper() : this.txtDirColonia.Text.ToUpper();

        PreInsc.EstadoCivil = byte.Parse(this.ddlEstadoCivil.SelectedValue);
        PreInsc.Ocupacion = byte.Parse(this.ddlOcupacion.SelectedValue);
        PreInsc.TipoBeca = byte.Parse(this.ddlTipoBeca.SelectedValue);
        PreInsc.TipoSangre = byte.Parse(this.ddlTipoSangre.SelectedValue);
        PreInsc.Alergias = this.chkAlergias.Checked ? this.txtAlergias.Text.ToUpper() : null;
        PreInsc.EnfermedadesCronicas = this.chkEnfCronicas.Checked ? this.txtEnfCronicas.Text.ToUpper() : null;
        PreInsc.CapacidadesDiferentes = this.chkCapDiferentes.Checked ? this.txtCapDiferentes.Text.ToUpper() : null;

        PreInsc.DIMSS = this.chkIMSS.Checked;
        PreInsc.DSeguroPopular = this.chkSeguro.Checked;
        PreInsc.DISSSTE = this.chkISSSTE.Checked;
        PreInsc.DPrivado = this.chkPrivado.Checked;

        PreInsc.IntPers1 = byte.Parse(this.ddlIntDet1.SelectedValue);
        PreInsc.IntPersOtros1 = this.txtIntOtro1.Text.ToUpper();
        PreInsc.IntPers2 = byte.Parse(this.ddlIntDet2.SelectedValue);
        PreInsc.IntPersOtros2 = this.txtIntOtro2.Text.ToUpper();

        PreInsc.ViveCon = byte.Parse(this.rblViveCon.SelectedValue);
        if (this.ddlViveConTutor.SelectedValue == "-1")
            PreInsc.ViveConTutor = null;
        else
            PreInsc.ViveConTutor = byte.Parse(this.ddlViveConTutor.SelectedValue);
        PreInsc.TipoVivienda = byte.Parse(this.rblTipoDeCasa.SelectedValue);
        PreInsc.IngresoMensual = byte.Parse(this.rblIngresoMensual.SelectedValue);
        PreInsc.NumIntegrantes = byte.Parse(this.ddlIntegrantes.SelectedValue);
        PreInsc.TienePC = this.chkTienePC.Checked;
        PreInsc.TieneInternet = this.chkTieneInternet.Checked;

        PreInsc.SecundariaID = int.Parse(this.ddlSecSecundaria.SelectedValue);
        PreInsc.SecundariaTXT = PreInsc.SecundariaID > 0 ? this.ddlSecSecundaria.SelectedItem.Text.ToUpper() : this.txtSecundaria.Text.ToUpper();

        if (this.tabPadre.Visible)
        {
            PreInsc.PadreApPaterno = this.txtPadreApPaterno.Text.ToUpper();
            PreInsc.PadreApMaterno = this.txtPadreApMaterno.Text.ToUpper();
            PreInsc.PadreNombre = this.txtPadreNombre.Text.ToUpper();
            PreInsc.PadreTelefono = this.txtPadreTelefono.Text.ToUpper();
            PreInsc.PadreOcupacion = byte.Parse(this.ddlPadreOcupacion.SelectedValue);
            PreInsc.PadreEmpresa = this.txtPadreEmpresa.Text.ToUpper();
        }
        else
        {
            PreInsc.PadreApPaterno = PreInsc.PadreApMaterno = PreInsc.PadreNombre = PreInsc.PadreTelefono = PreInsc.PadreEmpresa = null;
            PreInsc.PadreOcupacion = null;
        }

        if (this.tabMadre.Visible)
        {
            PreInsc.MadreApPaterno = this.txtMadreApPaterno.Text.ToUpper();
            PreInsc.MadreApMaterno = this.txtMadreApMaterno.Text.ToUpper();
            PreInsc.MadreNombre = this.txtMadreNombre.Text.ToUpper();
            PreInsc.MadreTelefono = this.txtMadreTelefono.Text.ToUpper();
            PreInsc.MadreOcupacion = byte.Parse(this.ddlMadreOcupacion.SelectedValue);
            PreInsc.MadreEmpresa = this.txtMadreEmpresa.Text.ToUpper();
        }
        else
        {
            PreInsc.MadreApPaterno = PreInsc.MadreApMaterno = PreInsc.MadreNombre = PreInsc.MadreTelefono = PreInsc.MadreEmpresa = null;
            PreInsc.MadreOcupacion = null;
        }

        PreInsc.FechaDeModif = DateTime.Now;


        if (EsNuevo)
        {
            string Folio = "";
            db.spEXEGetFolioPreInscripcion(PreInsc.PlantelID, PreInsc.Ciclo, ref Folio);
            PreInsc.Folio = Folio;

            //PreInsc.EntidadFed = "";
            PreInsc.FechaDeAlta = DateTime.Now;
            PreInsc.Estatus = 0;

            db.PreInscritos.InsertOnSubmit(PreInsc);
        }
        else
        {
            //Checar si hubo cambio de plantel
            if (PreInsc.PlantelID.ToString() != this.ddlPlantelPlantel.SelectedValue)
            {
                PreInsc.PlantelID = byte.Parse(this.ddlPlantelPlantel.SelectedValue);
                PreInsc.Turno = byte.Parse(this.ddlPlantelTurno.SelectedValue);

                string Folio = "";
                db.spEXEGetFolioPreInscripcion(PreInsc.PlantelID, PreInsc.Ciclo, ref Folio);
                PreInsc.Folio = Folio;
            }
        }

        try
        {
            db.SubmitChanges();
            Session["PreInsc"] = PreInsc;
        }
        catch (Exception ex)
        {
            this.AlertToPage("Lo sentimos, ha ocurrido un problema al guardar tus datos. Si el problema persiste, favor de acudir directamente al plantel. \\n ERROR: " + ex.Message);
            return;
        }

        this.TabContainer1.ActiveTabIndex = 0;

        this.GenerarPDF();
    }

    protected void btnReimprimir_Click(object sender, EventArgs e)
    {
        this.GenerarPDF();
    }

    private void GenerarPDF()
    {
        try
        {
            PreInscrito PreInsc = (PreInscrito)Session["PreInsc"];
            if (PreInsc == null)//Nunca debe de pasar
                return;

            DBEscolarDataContext db = new DBEscolarDataContext();
            var Alumno = (from A in db.vwRPTPreInscripcions
                          where A.Folio == PreInsc.Folio
                          select A).FirstOrDefault();

            if (Alumno == null)
                return;


            System.Collections.ArrayList col = new System.Collections.ArrayList();
            col.Add(Alumno);
            ReportDocument rptDoc = new ReportDocument();

            rptDoc.Load(Server.MapPath("Reportes/rptInscripcion.rpt"));
            //set dataset to the report viewer.
            rptDoc.SetDataSource(col);

            MemoryStream stream = (MemoryStream)rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptDoc.Close();
            rptDoc.Dispose();

            Response.Clear();
            Response.ContentType = @"Application/pdf";
            Response.AddHeader("Content-Disposition", "inline; filename=File.pdf");
            //Response.AddHeader("Content-Disposition", "attachment; filename=File.pdf");
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            stream.Dispose();
        }
        catch (Exception e) { }
    }

    private void AlertToPage(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');", msg), true);
    }

}