using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected async void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {

                BusinessUser usersNegocio = new BusinessUser();
                EmailService emailService = new EmailService("programacionsorteos@gmail.com", "rdnnfccpmyfoamap");
                PasswordHasher passwordHasher = new PasswordHasher();



                if (txtPassword.Text != txtConfirmPass.Text)
                {
                    
                    return;
                }

                if (txtPassword.Text.Length < 8)
                {
                   
                    return;
                }

                Model.User user = new Model.User
                {
                    Email = txtEmail.Text,
                    PasswordHash = passwordHasher.HashPassword(txtPassword.Text),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if (!usersNegocio.emailExists(user.Email))
                {
                    usersNegocio.CreateUser(user);

                    string to = user.Email;
                    string subject = "Confirmación de Registro";
                    string body = $"Hola {user.Email},<br/>Gracias por registrarte en nuestro sistema. Por favor, confirma tu registro.";
                    await emailService.SendEmailAsync(to, subject, body);

                    Response.Redirect("Login.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblError.Text = "El correo ya está registrado.";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
               
            }

        }
           
        }
}