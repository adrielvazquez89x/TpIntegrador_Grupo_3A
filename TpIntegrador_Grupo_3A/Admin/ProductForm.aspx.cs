﻿using Business;
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
            //BusinessSection businessSection = new BusinessSection();
            

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

                string code = Request.QueryString["code"] != null ? Request.QueryString["code"] : "";

                if(code != "")
                {
                    FillForm(code);
                }

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

                product.SubCategory.Id = int.Parse(ddlSubCategory.SelectedValue);
                product.SubCategory.CategoryId = int.Parse(ddlCategory.SelectedValue);

                product.Season = new Season();
                product.Season.Id = int.Parse(ddlSeason.SelectedValue);

                product.CreationDate = DateTime.Now;


                product.Images = new List<ImageProduct>();

                foreach(string imageUrl in ViewState[ImagesViewStateKey] as List<string>)
                {
                    var imgAux = new ImageProduct();
                    imgAux.UrlImage = imageUrl;
                    imgAux.CodProd = product.Code;

                    product.Images.Add(imgAux);
                }
                                
                BusinessProduct businessProduct = new BusinessProduct();

                //Hay que ver si usamos el ID para poder identificar una edición o un alta

                var result = businessProduct.Add(product);

                if(result)
                {
                    UserControl_Toast.ShowToast("Producto agregado correctamente", true);
                    ClearForm();
                }
                else
                {

                   UserControl_Toast.ShowToast("Error al agregar el producto", false);
                }

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

        private void ClearForm()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
            ddlCategory.SelectedIndex = 0;
            ddlSubCategory.SelectedIndex = 0;
            ddlSeason.SelectedIndex = 0;
            //ddlSection.SelectedIndex = 0;

            // Limpiar la lista de imágenes en el ViewState y actualizar el ListBox
            ViewState[ImagesViewStateKey] = new List<string>();
            BindImagesListBox();

            // Ocultar la imagen de previsualización
            imgPreview.Visible = false;
        }

        private void FillForm(string code)
        {
            BusinessProduct businessProduct = new BusinessProduct();

            try
            {
                List<Product> product = businessProduct.list(code);

                if (product.Count > 0)
                {
                    txtCode.Text = product[0].Code;
                    txtName.Text = product[0].Name;
                    txtPrice.Text = product[0].Price.ToString();
                    txtDescription.Text = product[0].Description;

                    ddlCategory.SelectedValue = product[0].Category.Id.ToString();
                    BindSubCategories(product[0].Category.Id);
                    ddlSubCategory.SelectedValue = product[0].SubCategory.Id.ToString();
                    ddlSeason.SelectedValue = product[0].Season.Id.ToString();

                    //ddlSection.SelectedValue = product[0].Section.Id.ToString();

                    ViewState[ImagesViewStateKey] = product[0].Images.Select(i => i.UrlImage).ToList();
                    BindImagesListBox();
                }
            }
            catch (Exception ex )
            {

                throw ex ;
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