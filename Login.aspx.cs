using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            this.UserName.Focus();
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string Usuario = this.UserName.Text.Trim();
        if (FormsAuthentication.Authenticate(Usuario, this.Password.Text.Trim()))
            FormsAuthentication.RedirectFromLoginPage(Usuario, false);
        else
            this.FailureText.Text = "Usuario y/o contraseña incorrecta";
    }

}