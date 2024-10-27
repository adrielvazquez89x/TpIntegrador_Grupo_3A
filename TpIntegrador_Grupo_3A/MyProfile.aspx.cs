using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Model;

namespace TpIntegrador_Grupo_3A
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Security.ActiveSession(Session["user"]))
                    {
                        
                        User user = (User)Session["user"];
                        txtEmail.Text = user.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = user.FirstName;
                        txtApellido.Text = user.LastName;

                        
                    }
                    else
                    {
                        Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                       // Response.Redirect("Error.aspx", false);

                    }

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}