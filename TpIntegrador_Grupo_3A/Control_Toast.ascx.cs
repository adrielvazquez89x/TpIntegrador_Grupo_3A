using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Control_Toast : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // Propiedad para obtener el ClientID del toast
        public string ToastClientID => toastDiv.ClientID;

        // Método para mostrar el toast
        public void ShowToast(string message, bool isSuccess)
        {
            ltlToastMessage.Text = message;

            // Construir el script para mostrar el toast
            string script = $@"
                var toastEl = document.getElementById('{ToastClientID}');
                if (toastEl) {{
                    // Remover clases previas de fondo
                    toastEl.classList.remove('bg-success', 'bg-danger', 'text-white');
                    // Añadir la clase correspondiente
                    toastEl.classList.add('{(isSuccess ? "bg-success" : "bg-danger")}', 'text-white');
                    var toast = new bootstrap.Toast(toastEl, {{ delay: 3000 }});
                    toast.show();
                }}
            ";

            // Registrar el script para que se ejecute en el cliente
            ScriptManager.RegisterStartupScript(this, GetType(), "showToast", script, true);
        }
    }
}