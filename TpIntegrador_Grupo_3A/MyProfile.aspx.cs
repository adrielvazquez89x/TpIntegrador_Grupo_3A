using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Model;
using Security;

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
                    if (SessionSecurity.ActiveSession(Session["user"]))
                    {
                        
                        Model.User user = (Model.User)Session["user"];
                        txtEmail.Text = user.Email;
                        txtEmail.ReadOnly = true;
                        if (user.FirstName != null)
                            txtNombre.Text = user.FirstName;
                        if (user.LastName != null) 
                            txtApellido.Text = user.LastName;
                        if (user.ImageUrl != null)
                            imgNuevoPerfil.ImageUrl = "~/img/" + user.ImageUrl;
                    }
                    else
                    {
                        //Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                        // Response.Redirect("Error.aspx", false);
                        Response.Redirect("Login.aspx", false);
                    }

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessUser businessUser = new BusinessUser();
                Model.User user = (Model.User)Session["user"];
                user.FirstName = txtNombre.Text;
                user.LastName = txtApellido.Text;
                
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    string imgName = "perfl-" + user.LastName + user.FirstName + user.UserId + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + imgName);
                    user.ImageUrl = imgName;
                }

                businessUser.Update(user);

                if (user.ImageUrl != null)
                {
                    Image img = (Image)Master.FindControl("imgAvatar");
                    img.ImageUrl = "~/img/" + user.ImageUrl;
                }

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                //Session.Add("error", "Error inesperado");
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}