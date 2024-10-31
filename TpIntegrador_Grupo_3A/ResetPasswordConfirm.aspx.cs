using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class ResetPasswordConfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            var businessUser = new BusinessUser();
            var email = Request.QueryString["email"];
            var token = Request.QueryString["token"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                lblConfirmError.Text = "El enlace no es válido.";
                return;
            }

            // Verificar si el token es válido (NO ANDA AUN)
            //if (!businessUser.VerifyResetToken(email, token))
            //{
            //    lblConfirmError.Text = "El enlace no es válido o ha expirado.";
            //    return;
            //}

            // Verificar las contraseñas
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblConfirmError.Text = "Las contraseñas no coinciden.";
                return;
            }

            // Llama al método de restablecimiento de contraseña
            businessUser.ResetPassword(email, txtNewPassword.Text);

            Response.Redirect("Login.aspx");
        }
    }
}