using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Model;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        /*
         La idea que tuve en esta parte, era hacer algo similar "react"
        que no actualiza la página cada vez que se hace click. 
        Por eso van a ver que hay bastante código en esta parte.
        La idea es que haya 3 botones, uno para agregar una categoría, otro para temporada y otro para sección.
        Al final, cada botón renderiza el crud correspondiente.
        Hice el tema de los toasts, para poder manejar todo en esta página
        sin tener que hacer 1 página para cada situación. 
         */
        // Enum para manejar los botones, es como un array mas limpio. 
        public enum Buttons { NotPicked = 0, Category = 1, Season = 2, Section = 3};
        //Esto es para renderizar en el cliente.
        public Buttons CurrentOption;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentOption = Buttons.NotPicked;
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            BusinessCategory businessCategory = new BusinessCategory();
            string result = businessCategory.Add(new Category { Name = txtCategory.Text.Trim() });

            if (result == "ok")
            {
                ShowToast("Categoría agregada correctamente.", true);
                txtCategory.Text = "";
            }
            else
            {
                ShowToast(result, false);
            }
        }

        private void ShowToast(string message, bool isSuccess)
        {
            ltlToastMessage.Text = message;

            //Inyección en JavaScript de script para mostrar el toast
            string script = $@"
            var toastEl = document.getElementById('liveToast');
             if (toastEl) {{
            // Remover clases previas de fondo
            toastEl.classList.remove('bg-success', 'bg-danger', 'text-white');
            // Añadir la clase correspondiente
            toastEl.classList.add('{(isSuccess ? "bg-success" : "bg-danger")}', 'text-white');
            var toast = new bootstrap.Toast(toastEl, {{ delay: 3000 }});
            toast.show();
            }}
             ";

            ScriptManager.RegisterStartupScript(this, GetType(), "showToastie", script, true);
        }

        protected void btnPickCategory_Click(object sender, EventArgs e)
        {
            CurrentOption = Buttons.Category;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            CurrentOption = Buttons.NotPicked;
        }
    }
}