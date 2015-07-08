using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBEscolar;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

public partial class SolicitudPV : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Alumnos_Virtual PreInsc = (Alumnos_Virtual)Session["PreInsc"];

        if (PreInsc == null)//Si no existe la variable en la sesion regresarlo a la pagina de inicio
        {
            Response.Redirect("NuevoIngresoPV.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {
            if (PreInsc.Estatus > 1) //Ya lo validaron
            {
                this.pnlInfo.Visible = true;
                this.pnlSolicitud.Visible = false;
                this.lblInfo.Text = string.Format("¡Hola {0} {1} {2}!<br />", PreInsc.ApPaterno, PreInsc.ApMaterno, PreInsc.Nombre);
                this.lblInfo.Text += string.Format("Tus datos ya fueron guardados:<br /><strong>CURP: {0}<br />MATRÍCULA: {1}</strong>", PreInsc.CURP, PreInsc.Matricula);
                return;
            }

            //Si esta entrando por primera vez
            if (PreInsc.Estatus == 0 && string.IsNullOrEmpty(PreInsc.Matricula))
            {
                this.txtCURP.Text = PreInsc.CURP;
                this.txtApPaterno.Text = PreInsc.ApPaterno;
                this.txtApMaterno.Text = PreInsc.ApMaterno;
                this.txtNombre.Text = PreInsc.Nombre;
                this.ddlSexo.SelectedValue = PreInsc.Sexo.ToString();
                this.txtFecNac.Text = PreInsc.FechaNac.ToShortDateString();
               
                this.CargarMunicipios();
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
            this.ddlMunicipio.SelectedValue = PreInsc.MunicipioID.ToString();

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

        //Combo de municipios
        Utils.InicializaCombo(this.ddlMunicipio);
        this.ddlMunicipio.DataSource = Municipios;
        this.ddlMunicipio.DataBind();
        ddlMunicipio.Items.Add(new ListItem("OTRO ESTADO", "0"));
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
            this.Guardar();
    }

    private void Guardar()
    {
        Alumnos_Virtual PreInsc = (Alumnos_Virtual)Session["PreInsc"];

        bool EsNuevo = PreInsc.Estatus == 0 && string.IsNullOrEmpty(PreInsc.Matricula);

        DBEscolarDataContext db = new DBEscolarDataContext();
        if (!EsNuevo)
        {
            string CURP = PreInsc.CURP;
            PreInsc = (from A in db.Alumnos_Virtuals
                       where A.CURP == CURP
                       select A).FirstOrDefault();
        }

        PreInsc.CURP = this.txtCURP.Text.ToUpper();
        PreInsc.ApPaterno = this.txtApPaterno.Text.ToUpper();
        PreInsc.ApMaterno = this.txtApMaterno.Text.ToUpper();
        PreInsc.Nombre = this.txtNombre.Text.ToUpper();
        PreInsc.Sexo = char.Parse(this.ddlSexo.SelectedValue);
        PreInsc.FechaNac = DateTime.Parse(this.txtFecNac.Text);
        PreInsc.EntidadFed = this.txtEstado.Text.ToUpper();

        PreInsc.Telefono = this.txtTelefono.Text.ToUpper();
        PreInsc.Celular = this.txtCelular.Text.ToUpper();
        PreInsc.Correo = this.txtCorreo.Text.ToLower();

        PreInsc.Calle = this.txtDirCalle.Text.ToUpper();
        PreInsc.Numero = this.txtDirNumero.Text.ToUpper();
        PreInsc.CodigoPostal = int.Parse(this.txtDirCP.Text);
        PreInsc.ColoniaID = int.Parse(this.ddlColonia.SelectedValue);
        PreInsc.ColoniaTXT = PreInsc.ColoniaID > 0 ? this.ddlColonia.SelectedItem.Text.ToUpper() : this.txtDirColonia.Text.ToUpper();
        PreInsc.MunicipioID = byte.Parse(this.ddlMunicipio.SelectedValue.ToString());

        if (EsNuevo)
        {
            string Folio = "";
            db.spEXEGetFolioPreInscripcion(PreInsc.PlantelID, "2015B", ref Folio);
            PreInsc.Matricula = Folio;

            //PreInsc.EntidadFed = "";
            PreInsc.Matricula = Folio; //Guardar el folio por mientras
            PreInsc.FechaDeAlta = DateTime.Now;
            PreInsc.Estatus = 1;

            db.Alumnos_Virtuals.InsertOnSubmit(PreInsc);
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

        this.GenerarPDF();
    }

    protected void btnReimprimir_Click(object sender, EventArgs e)
    {
        this.GenerarPDF();
    }

    private void GenerarPDF()
    {
        Alumnos_Virtual PreInsc = (Alumnos_Virtual)Session["PreInsc"];
        if (PreInsc == null)//Nunca debe de pasar
            return;

        DBEscolarDataContext db = new DBEscolarDataContext();
        var Alumno = (from A in db.vwRPTPreInscripcionPVs
                      where A.Matricula == PreInsc.Matricula
                      select A).FirstOrDefault();

        if (Alumno == null)
            return;


        System.Collections.ArrayList col = new System.Collections.ArrayList();
        col.Add(Alumno);
        ReportDocument rptDoc = new ReportDocument();

        rptDoc.Load(Server.MapPath("Reportes/rptPreInscripcionPV.rpt"));
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

}