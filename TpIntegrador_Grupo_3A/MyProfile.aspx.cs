using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                            imgNuevoPerfil.ImageUrl = "~/Images/" + user.ImageUrl;
                        else
                            imgNuevoPerfil.ImageUrl = "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg";

                        //imgNuevoPerfil.ImageUrl = "~/images/" + user.ImageUrl;
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
            if (Page.IsValid)
            {
                try
                {

                    BusinessUser businessUser = new BusinessUser();
                    Model.User user = (Model.User)Session["user"];
                    user.FirstName = txtNombre.Text;
                    user.LastName = txtApellido.Text;
                    user.Mobile = txtCel.Text;
                    user.Dni = txtDni.Text;
                    user.BirthDate = Convert.ToDateTime(txtNacimiento.Text);

                    if (txtImagen.PostedFile != null && txtImagen.PostedFile.ContentLength > 0)
                    {
                        // Verifica si es JPG
                        string fileExtension = System.IO.Path.GetExtension(txtImagen.PostedFile.FileName).ToLower();
                        if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                        {
                            string ruta = Server.MapPath("~/Images/");
                            string imgName = "perfil-" + user.LastName + user.FirstName + user.UserId + ".jpg";
                            txtImagen.PostedFile.SaveAs(ruta + imgName);
                            user.ImageUrl = imgName;

                            // Actualiza la URL de la imagen mostrada en la página
                            imgNuevoPerfil.ImageUrl = "~/Images/" + imgName;
                        }
                        else
                        {
                            // Manejo de error en caso de que el archivo no sea JPG
                            Response.Write("<script>alert('Por favor sube una imagen en formato JPG.');</script>");
                            return;
                        }
                    }

                    // Actualiza la base de datos
                    // businessUser.Update(user);

                    // Actualiza el avatar en el MasterPage si existe
                    Image imgAvatar = (Image)Master.FindControl("imgAvatar");
                    if (imgAvatar != null && user.ImageUrl != null)
                    {
                        imgAvatar.ImageUrl = "~/Images/" + user.ImageUrl;
                    }

                    BusinessAdress businessAdress = new BusinessAdress();
                    Adress adress = new Adress();

                    adress.Province = txtProvincia.Text;
                    adress.Town = txtCiudad.Text;
                    adress.District = txtBarrio.Text;
                    adress.Street = txtCalle.Text;
                    adress.Number = string.IsNullOrEmpty(txtNumero.Text) ? 0 : Convert.ToInt32(txtNumero.Text);
                    adress.CP = txtCP.Text;
                    adress.Floor = string.IsNullOrEmpty(txtPiso.Text) ? null : txtPiso.Text;

                    adress.Unit = txtDpto.Text;


                    businessAdress.Add(adress);
                    user.AddressId = adress.Id;

                    businessUser.Update(user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                // Redirige
                Response.Redirect("Default.aspx", false);
            }
        }

    }
}