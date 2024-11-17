using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Categories : System.Web.UI.Page
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
        public enum Buttons { NotPicked = 0, Category = 1, Season = 2, Section = 3 };
        //Esto es para renderizar en el cliente.
        public Buttons CurrentOption;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentOption = Buttons.NotPicked;
                BindCategories();
            }
        }

        // CATEGORIAS
        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            BusinessCategory businessCategory = new BusinessCategory();
            string result = "";

            if (Validator.IsEmpty(txtCategory.Text))
            {
                UserControl_Toast.ShowToast("Debe completar el campo", false);
                return;
            }

            //Manejamos el botón de agregar y editar
            if (btnAddCategory.Text == "Agregar")
            {
                result = businessCategory.Add(new Category { Description = txtCategory.Text.Trim() });
            }
            else
            {
                if (Session["ObjectInMod"] != null)
                {
                    Category category = (Category)Session["ObjectInMod"];
                    category.Description = txtCategory.Text.Trim();
                    result = businessCategory.Update(category);
                    Session["ObjectInMod"] = null;
                    btnAddCategory.Text = "Agregar";
                }
            }
            //Para mostrar el toast
            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría agregada correctamente.", true);
                txtCategory.Text = "";

                BindCategories();
            }
            else
            {
                UserControl_Toast.ShowToast(result, false);
            }
            CurrentOption = Buttons.Category;
        }

        private void BindCategories()
        {
            try
            {
                BusinessCategory businessCategory = new BusinessCategory();
                List<Category> categories = businessCategory.list();

                dgvCategories.DataSource = categories;
                dgvCategories.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las categorías", false);
            }
        }

        protected void btnPickCategory_Click(object sender, EventArgs e)
        {
            CurrentOption = Buttons.Category;
            BindCategories();

        }

        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            Category category = GetCategoryFromCommandArgument(sender);

            Session["ObjectInMod"] = category;

            txtCategory.Text = category.Description;
            btnAddCategory.Text = "Editar";
            CurrentOption = Buttons.Category;
        }

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            Category category = GetCategoryFromCommandArgument(sender);
            BusinessCategory businessCategory = new BusinessCategory();

            string result = businessCategory.Delete(category.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Eliminada correctamente", true);
                CurrentOption = Buttons.Category;
                BindCategories();
            }

        }

        protected void btnActivateCategory_Click(object sender, EventArgs e)
        {
            Category category = GetCategoryFromCommandArgument(sender);
            BusinessCategory businessCategory = new BusinessCategory();

            string result = businessCategory.Activate(category.Id);

            if (result == "ok")
            {
                UserControl_Toast.ShowToast("Categoría Activada correctamente", true);
                CurrentOption = Buttons.Category;
                BindCategories();
            }
        }

        private Category GetCategoryFromCommandArgument(object sender)
        {
            //Viene el botón de editar, se hace un split para obtener los datos
            LinkButton btn = (LinkButton)sender;
            //Separamos todo el string, con el método split, muy similar a js
            string[] args = btn.CommandArgument.Split('|');
            //Parseamos los datos, porque split me devuelve un array de strings
            int categoryId = int.Parse(args[0]);
            string categoryName = args[1];
            bool categoryActive = bool.Parse(args[2]);

            return new Category { Id = categoryId, Description = categoryName, Active = categoryActive };
        }



        protected void dgvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategories.PageIndex = e.NewPageIndex;
            BindCategories();
            CurrentOption = Buttons.Category;
        }
    }
}