using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Preinscritos
/// </summary>
public class Preinscritos
{
    //Atributos
    private long folio;
    private int estatus;  
    private string nombre;
    private string curp;
    private string sexo;
    private int turno;  
    private DateTime fechanac;
    private DateTime fechaalta;

    //Metodos 
    public long Folio { get { return folio; } set { folio = value; } }
    public int Estatus { get { return estatus; } set { estatus = value; } }
    public string Nombre { get { return nombre; } set { nombre = value; } }
    public string Curp { get { return curp; } set { curp = value; } }
    public string Sexo { get { return sexo; } set { sexo = value; } }
    public int Turno { get { return turno; } set { turno = value; } }
    public DateTime FechaNac { get { return fechanac; } set { fechanac = value; } }
    public DateTime FechaAlta { get { return fechaalta; } set { fechaalta = value; } } 

}