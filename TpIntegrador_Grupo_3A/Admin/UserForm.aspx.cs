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
        protected void Page_Load(object sender, EventArgs e)
        {
          BusinessUser businessUser = new BusinessUser();
            try
            {
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                if (!IsPostBack && !string.IsNullOrEmpty(id))
             
                {
                    Model.User select = (businessUser.ListUsers(id))[0];

                    txtDni.Text = select.Dni;
                    txtFirstName.Text = select.FirstName;
                    txtLastName.Text = select.LastName;
                    txtEmail.Text = select.Email;
                    txtMobile.Text = select.Mobile;
                    txtPassword.Text = select.PasswordHash;
                    if (select.BirthDate.HasValue)
                    {
                        txtBirthDate.Text = select.BirthDate.Value.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        txtBirthDate.Text = ""; 
                    }
                   

                    btnSave.Text = "Actualizar Usuario";
                    txtEmail.ReadOnly = true;
                    txtPassword.Visible = false;
                    divPassword.Visible = false;
                    rfvPassword.Enabled = false;
                    revPassword.Enabled = false;

                }
                else if (!IsPostBack)
                {
                    btnSave.Text = "Agregar Usuario";

                    txtEmail.ReadOnly = false;

                    txtPassword.Visible = true;
                    rfvPassword.Enabled = true;
                    divPassword.Visible = true;
                    revPassword.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void cvBirthDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
            DateTime birthDate;
            if (DateTime.TryParse(args.Value, out birthDate))
            {
                if (birthDate > DateTime.Now)
                {
                    args.IsValid = false; 
                }
                else
                {
                    args.IsValid = true; 
                }
            }
            else
            {
                args.IsValid = false; 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessUser businessUser = new BusinessUser();
            try
            {
                if (Page.IsValid)
                {

                    Model.User user = new Model.User
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Dni = txtDni.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Mobile = txtMobile.Text.Trim(),
                        RegistrationDate = DateTime.Now,
                        BirthDate = string.IsNullOrEmpty(txtBirthDate.Text) ? (DateTime?)null : DateTime.Parse(txtBirthDate.Text),
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


                    if (Request.QueryString["id"] != null)
                    {
                     
                        user.UserId = int.Parse(Request.QueryString["id"]);
                        businessUser.UpdateUserForm(user);
                        UserControl_Toast.ShowToast("Usuario actualizado correctamente.", true);
                        Response.Redirect("/Admin/UsersManagement.aspx", false);
                    }
                    else
                    {
                        if (businessUser.emailExists(txtEmail.Text.Trim()))
                        {
                            UserControl_Toast.ShowToast("El correo electrónico ya está registrado. Por favor, ingrese otro.", false);
                            return;
                        }
                        var passwordHasher = new PasswordHasher();
                        string hashedPassword = passwordHasher.HashPassword(txtPassword.Text); // Hasheamos la contraseña
                        user.PasswordHash = hashedPassword;

                        businessUser.CreateAdmin(user);
                        UserControl_Toast.ShowToast("Usuario agregado correctamente.", true);
                        Response.Redirect("/Admin/UsersManagement.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                
                UserControl_Toast.ShowToast($"Error al crear el usuario: {ex.Message}", false);
            }
        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersManagement.aspx", false);
        }
    }
}