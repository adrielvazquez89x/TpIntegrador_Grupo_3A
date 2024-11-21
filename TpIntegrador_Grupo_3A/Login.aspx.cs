using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Security;

namespace TpIntegrador_Grupo_3A
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionSecurity.ActiveSession(Session["user"]))
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessUser businessUser = new BusinessUser();
                Model.User user = new Model.User();

                user.Email = txtEmail.Text;
                user.PasswordHash = txtPassword.Text;


                if (businessUser.Login(user))
                {
                    user = businessUser.GetUserById(user.UserId);
                    if (!user.Active)
                    {
                        
                        Control_Toast.ShowToast("Tu cuenta está inactiva. Contacta con el administrador.",false);
                        lblError.Text = "Tu cuenta está inactiva. Contacta con el administrador.";
                        return;  
                    }
                    Session.Add("user", user);

                    if (Session["Cart"] == null)
                    {
                        Model.Cart cart = new Model.Cart(); 
                        Session["Cart"] = cart;
                        user.Cart = cart;
                    }

                    if (SessionSecurity.IsAdmin(Session["user"]))
                    {

                        Response.Redirect("Admin/ProductsManagement.aspx");
                        
                    }

                   
                    if (user.firstAccess)
                    {
                        
                        Response.Redirect("/MyProfile.aspx", false);
                    }
                    else
                    {
                     
                        Response.Redirect("/Default.aspx", false);
                    }

                }
                else
                {
                    lblError.Text = "Usuario o contrasenia incorrectos. Inténtalo de nuevo.";
                }
            }

            catch (Exception ex)
            {
              
                Session.Add("error", ex.ToString());
                lblError.Text = "Ocurrió un error durante el inicio de sesión. Inténtalo de nuevo.";
            }
        }
    }
}