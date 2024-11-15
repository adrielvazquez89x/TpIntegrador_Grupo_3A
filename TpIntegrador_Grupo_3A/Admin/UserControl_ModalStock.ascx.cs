using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class UserControl_ModalStock : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ShowModal(string codigo, string nombre, List<Stock> stockActual, List<Colour> colores, List<Size> talles)
        {
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
            
            rptStock.DataSource = stockActual;
            rptStock.DataBind();

            // Cargar colores y talles en los dropdowns
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

        protected void btnAgregarStock_Click(object sender, EventArgs e)
        {
            // Lógica para agregar una nueva combinación de stock
            int colorId = int.Parse(ddlColor.SelectedValue);
            int talleId = int.Parse(ddlTalle.SelectedValue);
            int cantidad = int.Parse(txtCantidad.Text);

            // Aquí agregarías la lógica para insertar un nuevo stock en la base de datos
        }

        protected void rptStock_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStock")
            {
                
                int newStock;
                if (int.TryParse(((TextBox)e.Item.FindControl("txtEditStock")).Text, out newStock))
                {
                    int stockId = int.Parse(e.CommandArgument.ToString());

                    // Actualizar el stock en la base de datos usando stockId y newStock
                }
            }
        }
    }
}
