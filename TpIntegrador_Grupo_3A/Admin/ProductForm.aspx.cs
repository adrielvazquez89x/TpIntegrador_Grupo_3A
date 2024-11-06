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
    public partial class ProductForm : System.Web.UI.Page
    {
        private const string ImagesViewStateKey = "ProductImages";
        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessCategory businessCategory = new BusinessCategory();
            BusinessSeason businessSeason = new BusinessSeason();
//            BusinessSection businessSection = new BusinessSection();
            

            int categoryId;

            if (!IsPostBack)
            {
                ddlCategory.DataSource = businessCategory.list(false);
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataTextField = "Description";
                ddlCategory.DataBind();
                categoryId = int.Parse(ddlCategory.Items[0].Value);


                ddlSeason.DataSource = businessSeason.list(false);
                ddlSeason.DataValueField = "Id";
                ddlSeason.DataTextField = "Description";
                ddlSeason.DataBind();

                //ddlSection.DataSource = businessSection.list(false);
                //ddlSection.DataValueField = "Id";
                //ddlSection.DataTextField = "Description";
                //ddlSection.DataBind();



                BindSubCategories(categoryId);
                InitializeImages();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                Product product = new Product();
                product.Code = txtCode.Text;
                product.Name = txtName.Text;
                product.Price = decimal.Parse(txtPrice.Text);
                product.Description = txtDescription.Text;

                product.Category = new Category();
                product.Category.Id = int.Parse(ddlCategory.SelectedValue);

                product.SubCategory = new SubCategory();
                product.SubCategory.CategoryId = int.Parse(ddlCategory.SelectedValue);

                product.Season = new Season();
                product.Season.Id = int.Parse(ddlSeason.SelectedValue);

                product.CreationDate = DateTime.Now;


                List<ImageProduct> imageUrls = new List<ImageProduct>();

                foreach(string imageUrl in ViewState[ImagesViewStateKey] as List<string>)
                {
                    var imgAux = new ImageProduct();
                    imgAux.UrlImage = imageUrl;
                    imgAux.CodProd = product.Code;
                }

                product.Images = imageUrls;
                BusinessProduct businessProduct = new BusinessProduct();
                var result = businessProduct.Add(product);

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdCategory = int.Parse(ddlCategory.SelectedValue);
            BindSubCategories(IdCategory);

        }

        private void BindSubCategories(int IdCategory)
        {
            BusinessSubCategory businessSubCategory = new BusinessSubCategory();
            ddlSubCategory.DataSource = businessSubCategory.list(IdCategory);
            ddlSubCategory.DataValueField = "Id";
            ddlSubCategory.DataTextField = "Description";
            ddlSubCategory.DataBind();

            if (ddlSubCategory.Items.Count == 0)
            {
                ddlSubCategory.Enabled = false;
                ddlSubCategory.Items.Insert(0, new ListItem("No hay subcategorías", "0"));
            }
            else
            {
                ddlSubCategory.Enabled = true;
            }
        }


        //IMAGENES

        private void InitializeImages()
        {
            // Inicializa la lista de imágenes en ViewState si está vacía
            if (ViewState[ImagesViewStateKey] == null)
            {
                ViewState[ImagesViewStateKey] = new List<string>();
            }

            BindImagesListBox();
        }
        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            string imgUrl = txtImageUrl.Text.Trim();

            if (string.IsNullOrEmpty(imgUrl))
            {

                return;
            }

            List<string> images = ViewState[ImagesViewStateKey] as List<string>;

            images.Add(imgUrl);
            BindImagesListBox();

            txtImageUrl.Text = "";
            lblMessage.Text = "";
        }

        private void BindImagesListBox()
        {
            List<string> images = ViewState[ImagesViewStateKey] as List<string>;
            lstImages.Items.Clear();
            foreach (string imageUrl in images)
            {
                lstImages.Items.Add(new ListItem(imageUrl));
            }
        }

        protected void lstImages_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lstImages.SelectedItem != null)
            {
                string selectedUrl = lstImages.SelectedItem.Text;

                imgPreview.ImageUrl = selectedUrl;
                imgPreview.Visible = true;
                lblMessage.Text = "";

            }
            else
            {
                imgPreview.Visible = false;
                lblMessage.Text = "";
            }
        }

        protected void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            List<string> images = ViewState[ImagesViewStateKey] as List<string>;

            if(images == null)
            {
                lblMessage.Text = "No hay imágenes para eliminar";
                lblMessage.CssClass = "text-danger";
               return;
            }

            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach(ListItem item in lstImages.Items)
            {
                if(item.Selected)
                {
                    itemsToRemove.Add(item);
                }
            }
            if (itemsToRemove.Count == 0)
            {
                lblMessage.Text = "Selecciona al menos una imagen para eliminar.";
                lblMessage.CssClass = "text-warning";
                return;
            }
            foreach(ListItem item in itemsToRemove)
            {
                images.Remove(item.Text);
            }

            
            ViewState[ImagesViewStateKey] = images;

            
            BindImagesListBox();

            lblMessage.Text = "Imágenes eliminadas correctamente.";
            lblMessage.CssClass = "text-success";
            imgPreview.Visible = false;

        }
    }
}