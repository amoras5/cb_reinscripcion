using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml.Linq;
using System.Web.Security;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.UI.WebControls.WebParts;

public class Utils
{
    public const string CicloActual = "2015A";
    public const string CicloAnterior = "2014B";
    public const string CicloPreInscripciones = "2015B";

    public static string getVerificador(string referencia)
    {
        string original = "02151" + referencia;

        string val = "";
        int aux = 0;

        for (int i = 1; i <= original.Length; i++)
        {
            if (i % 2 == 0)
                val += original.Substring(i - 1, 1);
            else
            {
                aux = Convert.ToInt32(original.Substring(i - 1, 1)) * 2;
                if (aux >= 10)
                    val += left(aux.ToString(), 1) + right(aux.ToString(), 1);
                else
                    val += aux.ToString().Trim();
            }
        }
        int suma = 0;

        for (int i = 0; i < val.Length; i++)
            suma += Convert.ToInt32(val.Substring(i, 1));

        return (suma % 10 == 0) ? (suma % 10).ToString() : (10 - suma % 10).ToString();

    }

    private static string left(string param, int length)
    {
        string result = param.Substring(0, length);
        return result;
    }

    private static string right(string param, int length)
    {
        int l = param.Length;
        string result = param.Substring((l - length), length);
        return result;
    }

    public static void InicializaCombo(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Nombre";
        ddl.Items.Add(new ListItem("Selecciona una opción:", "-1"));
    }
}