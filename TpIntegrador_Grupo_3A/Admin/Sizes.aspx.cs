using Business.ProductAttributes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Sizes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSizes();
        }

        protected void btnAddSize_Click(object sender, EventArgs e)
        {
            BusinessSize businessSize = new BusinessSize();
            string result = "";

            if (Validator.IsEmpty(txtSize.Text))
            {
                UserControl_Toast.ShowToast("Debe completar el campo", false);
                return;
            }

            //Manejamos el botón de agregar y editar
            if (btnAddSize.Text == "Agregar")
            {
                result = businessSize.Add(new Size { Description = txtSize.Text.Trim() });
            }
            else
            {
                if (Session["ObjectInMod"] != null)
                {
                    Size size = (Size)Session["ObjectInMod"];
                    size.Description = txtSize.Text.Trim();
                    result = businessSize.Update(size);
                    Session["ObjectInMod"] = null;
                    btnAddSize.Text = "Agregar";
                }
            }
            //Para mostrar el toast
            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Talle agregado correctamente.", true);
                txtSize.Text = "";

                BindSizes();
            }
            else
            {
                UserControl_Toast.ShowToast(result, false);
            }
            //CurrentOption = Buttons.Size;
        }

        private void BindSizes()
        {
            try
            {
                BusinessSize businessSize = new BusinessSize();
                List<Size> sizes = businessSize.list();

                dgvSizes.DataSource = sizes;
                dgvSizes.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar los talles", false);
            }
        }

        protected void btnEditSize_Click(object sender, EventArgs e)
        {
            Size size = GetSizeFromCommandArgument(sender);

            Session["ObjectInMod"] = size;

            txtSize.Text = size.Description;
            btnAddSize.Text = "Editar";
            
        }
        protected void btnDeleteSize_Click(object sender, EventArgs e)
        {
            Size size = GetSizeFromCommandArgument(sender);
            BusinessSize businessSize = new BusinessSize();

            string result = businessSize.Delete(size.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Talle Eliminado correctamente", true);
                
                BindSizes();
            }

        }
        protected void btnActivateSize_Click(object sender, EventArgs e)
        {
            Size size = GetSizeFromCommandArgument(sender);
            BusinessSize businessSize = new BusinessSize();

            string result = businessSize.Activate(size.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Talle Activado correctamente", true);
                
                BindSizes();
            }
        }
        private Size GetSizeFromCommandArgument(object sender)
        {
            //Viene el botón de editar, se hace un split para obtener los datos
            LinkButton btn = (LinkButton)sender;
            //Separamos todo el string, con el método split, muy similar a js
            string[] args = btn.CommandArgument.Split('|');
            //Parseamos los datos, porque split me devuelve un array de strings
            int sizeId = int.Parse(args[0]);
            string sizeName = args[1];
            bool sizeActive = bool.Parse(args[2]);

            return new Size { Id = sizeId, Description = sizeName, Active = sizeActive };
        }
        protected void dgvSizes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSizes.PageIndex = e.NewPageIndex;
            BindSizes();
            
        }
    }
}