using Business.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Colours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindColours();
        }

        protected void btnAddColour_Click(object sender, EventArgs e)
        {
            BusinessColour businessColour = new BusinessColour();
            string result = "";

            if (Validator.IsEmpty(txtColour.Text))
            {
                UserControl_Toast.ShowToast("Debe completar el campo", false);
                return;
            }

            //Manejamos el botón de agregar y editar
            if (btnAddColour.Text == "Agregar")
            {
                result = businessColour.Add(new Colour { Description = txtColour.Text.Trim() });
            }
            else
            {
                if (Session["ObjectInMod"] != null)
                {
                    Colour colour = (Colour)Session["ObjectInMod"];
                    colour.Description = txtColour.Text.Trim();
                    result = businessColour.Update(colour);
                    Session["ObjectInMod"] = null;
                    btnAddColour.Text = "Agregar";
                }
            }
            //Para mostrar el toast
            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Sección agregada correctamente.", true);
                txtColour.Text = "";

                BindColours();
            }
            else
            {
                UserControl_Toast.ShowToast(result, false);
            }
            
        }

        private void BindColours()
        {
            try
            {
                BusinessColour businessColour = new BusinessColour();
                List<Colour> colours = businessColour.list();

                dgvColours.DataSource = colours;
                dgvColours.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las Secciones", false);
            }
        }

        protected void btnEditColour_Click(object sender, EventArgs e)
        {
            Colour colour = GetColourFromCommandArgument(sender);

            Session["ObjectInMod"] = colour;

            txtColour.Text = colour.Description;
            btnAddColour.Text = "Editar";
            
        }
        protected void btnDeleteColour_Click(object sender, EventArgs e)
        {
            Colour colour = GetColourFromCommandArgument(sender);
            BusinessColour businessColour = new BusinessColour();

            string result = businessColour.Delete(colour.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Eliminada correctamente", true);
                
                BindColours();
            }

        }
        protected void btnActivateColour_Click(object sender, EventArgs e)
        {
            Colour colour = GetColourFromCommandArgument(sender);
            BusinessColour businessColour = new BusinessColour();

            string result = businessColour.Activate(colour.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Activada correctamente", true);
                
                BindColours();
            }
        }
        private Colour GetColourFromCommandArgument(object sender)
        {
            //Viene el botón de editar, se hace un split para obtener los datos
            LinkButton btn = (LinkButton)sender;
            //Separamos todo el string, con el método split, muy similar a js
            string[] args = btn.CommandArgument.Split('|');
            //Parseamos los datos, porque split me devuelve un array de strings
            int colourId = int.Parse(args[0]);
            string colourName = args[1];
            bool colourActive = bool.Parse(args[2]);

            return new Colour { Id = colourId, Description = colourName, Active = colourActive };
        }
        protected void dgvColours_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvColours.PageIndex = e.NewPageIndex;
            BindColours();
            
        }
    }
}