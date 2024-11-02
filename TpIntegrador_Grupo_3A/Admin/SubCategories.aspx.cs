using Business;
using Business.ProductAttributes;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class SubCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSubCategories();
            }
        }

        // CATEGORIAS
        protected void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            BusinessSubCategory businessSubCategory = new BusinessSubCategory();
            string result = "";

            //Manejamos el botón de agregar y editar
            if (btnAddSubCategory.Text == "Agregar")
            {
               // result = businessSubCategory.Add(new SubCategory { Description = txtSubCategory.Text.Trim() });
            }
            else
            {
                if (Session["ObjectInMod"] != null)
                {
                    SubCategory SubCategory = (SubCategory)Session["ObjectInMod"];
                    SubCategory.Description = txtSubCategory.Text.Trim();
                    //result = businessSubCategory.Update(SubCategory);
                    Session["ObjectInMod"] = null;
                    btnAddSubCategory.Text = "Agregar";
                }
            }
            //Para mostrar el toast
            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría agregada correctamente.", true);
                txtSubCategory.Text = "";

                BindSubCategories();
            }
            else
            {
                UserControl_Toast.ShowToast(result, false);
            }

        }

        private void BindSubCategories()
        {
            try
            {
                BusinessSubCategory businessSubCategory = new BusinessSubCategory();
                //List<SubCategory> SubCategories = businessSubCategory.list();

               // dgvSubCategories.DataSource = SubCategories;
                dgvSubCategories.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las categorías", false);
            }
        }

        protected void btnPickSubCategory_Click(object sender, EventArgs e)
        {
            BindSubCategories();

        }

        protected void btnEditSubCategory_Click(object sender, EventArgs e)
        {
            SubCategory SubCategory = GetSubCategoryFromCommandArgument(sender);

            Session["ObjectInMod"] = SubCategory;

            txtSubCategory.Text = SubCategory.Description;
            btnAddSubCategory.Text = "Editar";
        }

        protected void btnDeleteSubCategory_Click(object sender, EventArgs e)
        {
            SubCategory SubCategory = GetSubCategoryFromCommandArgument(sender);
            BusinessSubCategory businessSubCategory = new BusinessSubCategory();

            //string result = businessSubCategory.Delete(SubCategory.Id);

            //if (result == "ok")
            //{
            //    UserControl_Toast.ShowToast("Categoría Eliminada correctamente", true);
            //    BindSubCategories();
            //}

        }

        protected void btnActivateSubCategory_Click(object sender, EventArgs e)
        {
            SubCategory SubCategory = GetSubCategoryFromCommandArgument(sender);
            BusinessSubCategory businessSubCategory = new BusinessSubCategory();

            //string result = businessSubCategory.Activate(SubCategory.Id);

            //if (result == "ok")
            //{
            //    UserControl_Toast.ShowToast("Categoría Activada correctamente", true);
            //    BindSubCategories();
            //}
        }

        private SubCategory GetSubCategoryFromCommandArgument(object sender)
        {
            //Viene el botón de editar, se hace un split para obtener los datos
            LinkButton btn = (LinkButton)sender;
            //Separamos todo el string, con el método split, muy similar a js
            string[] args = btn.CommandArgument.Split('|');
            //Parseamos los datos, porque split me devuelve un array de strings
            int SubCategoryId = int.Parse(args[0]);
            string SubCategoryName = args[1];
            bool SubCategoryActive = bool.Parse(args[2]);

            return new SubCategory { Id = SubCategoryId, Description = SubCategoryName, Active = SubCategoryActive };
        }



        protected void dgvSubCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvSubCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSubCategories.PageIndex = e.NewPageIndex;
            BindSubCategories();

        }
    }
}
