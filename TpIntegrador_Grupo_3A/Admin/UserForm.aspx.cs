using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class UserForm : System.Web.UI.Page
    {
        BusinessUser businessUser = new BusinessUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                if (id != "" && !IsPostBack)
                {
                    Model.User select = (businessUser.ListUsers(id))[0];

                    txtDni.Text = select.Dni;
                    txtFirstName.Text = select.FirstName;
                    txtLastName.Text = select.LastName;
                    txtEmail.Text = select.Email;
                    txtMobile.Text = select.Mobile;
                    txtPassword.Text = select.PasswordHash;
                    //txtBirthDate.Text = select.BirthDate.ToString("yyyy-MM-dd");

                    btnSave.Text = "Actualizar Usuario";
                    divPassword.Visible = false;

                }
                else
                {
                    btnSave.Text = "Agregar Usuario";
                    divPassword.Visible = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Model.User user = new Model.User
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Dni = txtDni.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Mobile = txtMobile.Text.Trim(),
                    RegistrationDate = DateTime.Now,
                    Admin = true,
                    Owner = false,
                    Active = true,

                };

                if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) ||
                    string.IsNullOrWhiteSpace(user.Dni) || string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.Mobile))
                {
                    UserControl_Toast.ShowToast("Por favor, complete todos los campos.", false);
                    return;
                }

                var passwordHasher = new PasswordHasher();
                string hashedPassword = passwordHasher.HashPassword(txtPassword.Text); // Hasheamos la contraseña
                user.PasswordHash = hashedPassword;

                if (Request.QueryString["id"] != null)
                {
                    // Actualizar el usuario existente
                    user.UserId = int.Parse(Request.QueryString["id"]);
                    businessUser.Update(user);
                    UserControl_Toast.ShowToast("Usuario actualizado correctamente.", true);
                }
                else
                {

                    businessUser.CreateAdmin(user);
                    UserControl_Toast.ShowToast("Usuario agregado correctamente.", true);
                }
            }

            catch (Exception ex)
            {
                // Manejo de errores
                UserControl_Toast.ShowToast($"Error al crear el usuario: {ex.Message}", false);

                Response.Write($"Error al crear el usuario: {ex.Message}");
            }
        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersManagement.aspx", false);
        }
    }
}