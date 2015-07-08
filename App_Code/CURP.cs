using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Globalization;

/// <summary>
/// Summary description for RENAPO
/// </summary>
public class CURP
{
    public string Curp { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Sexo { get; set; }
    public string FechaDeNacimiento { get; set; }
    public string Estado { get; set; }
    public bool Encontrado { get; set; }
    public int Fuente { get; set; }

    public CURP() { }

    public void getFromRENAPO(string CURP)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://consultas.curp.gob.mx/CurpSP/curp2.do?strCurp=" + CURP + "&strTipo=B");
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.46 Safari/535.11";
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);
        System.IO.StreamReader stream = new System.IO.StreamReader(response.GetResponseStream(), enc);

        string tmp = stream.ReadToEnd();
        int x;

        if (tmp.Length > 4000)
        {
            this.Curp = CURP;

            x = tmp.IndexOf("strPrimerApellido\" value=\"") + 26;
            this.ApellidoPaterno = tmp.Substring(x, tmp.IndexOf("\">", x) - x);

            x = tmp.IndexOf("strSegundoAplido\" value=\"") + 25;
            this.ApellidoMaterno = tmp.Substring(x, tmp.IndexOf("\">", x) - x);

            x = tmp.IndexOf("strNombre\" value=\"") + 18;
            this.Nombre = tmp.Substring(x, tmp.IndexOf("\">", x) - x);

            x = tmp.IndexOf("strSexo\" value=\"") + 16;
            this.Sexo = tmp.Substring(x, tmp.IndexOf("\">", x) - x);
            this.Sexo = this.Sexo == "HOMBRE" ? "M" : "F";

            x = tmp.IndexOf("strFechanacimiento\" value=\"") + 27;
            this.FechaDeNacimiento = tmp.Substring(x, tmp.IndexOf("\">", x) - x);

            x = tmp.IndexOf("strCveEnt\" value=\"") + 18;
            this.Estado = tmp.Substring(x, tmp.IndexOf("\">", x) - x);

            this.Encontrado = true;
            this.Fuente = 1;
        }
        else
            this.Encontrado = false;
    }

    //public void getFromDB(string CURP)
    //{
    //    CURP aux = BO_Preinscripcion.getCURPInfo(CURP);
    //    if (aux == null)
    //    {
    //        this.Encontrado = false;
    //        return;
    //    }

    //    this.Curp = CURP;
    //    this.ApellidoPaterno = aux.ApellidoPaterno;
    //    this.ApellidoMaterno = aux.ApellidoMaterno;
    //    this.Nombre = aux.Nombre;
    //    this.Sexo = aux.Sexo;
    //    this.FechaDeNacimiento = aux.FechaDeNacimiento;
    //    this.Estado = "Sinaloa";
    //    this.Encontrado = true;
    //    this.Fuente = 2;
    //}

    public void getInfo(string CURP)
    {
        //this.getFromDB(CURP);

        //if (!this.Encontrado)
        this.getFromRENAPO(CURP);
    }

}