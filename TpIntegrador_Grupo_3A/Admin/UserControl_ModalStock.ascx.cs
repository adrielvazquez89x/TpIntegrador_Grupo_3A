using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class UserControl_ModalStock : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicialización si es necesario
        }

        // Método para mostrar el modal desde el servidor
        public void ShowModal(string codigo, string nombre, int stockActual, List<Colour> colores, List<Size> talles)
        {
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
            lblStockActual.Text = stockActual.ToString();

            ddlColor.DataSource = colores;
            ddlColor.DataValueField = "Id";
            ddlColor.DataTextField = "Description";
            ddlColor.DataBind();
            ddlColor.Items.Insert(0, new ListItem("Seleccione un color", "0"));

            ddlTalle.DataSource = talles;
            ddlTalle.DataValueField = "Id";
            ddlTalle.DataTextField = "Description";
            ddlTalle.DataBind();
            ddlTalle.Items.Insert(0, new ListItem("Seleccione un talle", "0"));

            // Registrar y ejecutar el script para mostrar el modal
            string script = $@"
                var toastEl = document.getElementById('{stockModal.ClientID}');
                var modal = new bootstrap.Modal(toastEl, {{ backdrop: 'static', keyboard: false }});
                modal.show();
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", script, true);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // Lógica para manejar la aceptación del modal
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            int cantidad = int.Parse(txtCantidad.Text);
            int colorId = int.Parse(ddlColor.SelectedValue);
            int talleId = int.Parse(ddlTalle.SelectedValue);

            // Aquí puedes agregar la lógica para actualizar el stock en la base de datos

            // Después de la lógica, ocultar el modal
            string script = $@"
                var toastEl = document.getElementById('{stockModal.ClientID}');
                var modal = bootstrap.Modal.getInstance(toastEl);
                if (modal) {{
                    modal.hide();
                }}
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "hideModal", script, true);
        }
    }
}