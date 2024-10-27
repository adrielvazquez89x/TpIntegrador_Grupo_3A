using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendReset_Click(object sender, EventArgs e)
        {
            var businessUser = new BusinessUser();
            var userEmail = txtResetEmail.Text;

            var user = businessUser.GetUserByEmail(userEmail);


            if (user != null)
            {
                // Generar el token de restablecimiento de contraseña
                var token = businessUser.GenerateToken();

                businessUser.StoreResetToken(userEmail, token);


                // Crear el enlace para restablecer la contraseña
                var resetLink = $"https://localhost:44379/ResetPasswordConfirm.aspx?email={userEmail}&token={token}";

                // Enviar el correo
                EmailService emailService = new EmailService("programacionsorteos@gmail.com", "zlujsnytsxpeuvsy");

                var subject = "Restablecer Contraseña";
                var body = $"Haz clic en este enlace para restablecer tu contraseña: <a href='{resetLink}'>Restablecer Contraseña</a>";

                Task.Run(() => emailService.SendEmailAsync(userEmail, subject, body));

                lblResetError.Text = "Se ha enviado un enlace a tu correo electrónico.";
            }
            else
            {
                lblResetError.Text = "Este correo no está registrado.";
            }
        }
    }
}