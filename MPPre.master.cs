using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MPPre : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Redireccionar a todos los que lleguen a /preinscripciones/
        if (Request.Url.AbsolutePath.StartsWith("/preinscripciones/"))
            Response.Redirect(Request.Url.AbsolutePath.Replace("/preinscripciones/", "/inscripciones/"));
    }
}
