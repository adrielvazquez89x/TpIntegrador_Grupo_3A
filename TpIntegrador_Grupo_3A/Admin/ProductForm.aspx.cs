using Business;
using Business.ProductAttributes;
using Model;
using Model.ProductAttributes;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class ProductForm : System.Web.UI.Page
    {
        private const string ImagesViewStateKey = "ProductImages";
        private const string NewImagesViewStateKey = "NewProductImages";

        protected int currentProductId;
        private string currentProductCode = "";
        List<int> imagesToDelete;

        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessCategory businessCategory = new BusinessCategory();
            BusinessSeason businessSeason = new BusinessSeason();

            if (!IsPostBack)
            {
                // Vincular las categorías
                ddlCategory.DataSource = businessCategory.list(false);
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataTextField = "Description";
                ddlCategory.DataBind();

                // Obtener el ID del producto actual
                currentProductId = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;
                Session["idCurrentItem"] = currentProductId;


                if (currentProductId != 0)
                {
                    FillForm(currentProductId);
                }
                else
                {
                    int categoryId = int.Parse(ddlCategory.Items[0].Value);
                    BindSubCategories(categoryId);
                }

                ddlSeason.DataSource = businessSeason.list(false);
                ddlSeason.DataValueField = "Id";
                ddlSeason.DataTextField = "Description";
                ddlSeason.DataBind();

                BindImagesRepeater();
                InitializeImages();
            }
            else
            {
                currentProductId = Session["idCurrentItem"] != null ? (int)Session["idCurrentItem"] : 0;
                currentProductCode = Session["CurrentProductCode"] != null ? (string)Session["CurrentProductCode"] : "";

                if (currentProductId != 0)
                {
                    BindImagesRepeater();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] txtsValues = { txtCode.Text, txtName.Text, txtDescription.Text };

            try
            {

                foreach (var txt in txtsValues)
                {
                    if (Validator.IsEmpty(txt))
                    {
                        UserControl_Toast.ShowToast("Todos los campos son obligatorios", false);
                        return;
                    }
                }

                if (!Validator.IsOnlyNumbers(txtPrice.Text))
                {
                    UserControl_Toast.ShowToast("El precio debe ser numérico", false);
                    return;
                }

                if (lstImages.Items.Count == 0 && currentProductId == 0)
                {
                    UserControl_Toast.ShowToast("Debes agregar al menos una imagen", false);
                    return;
                }


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
                product.IsActive = (bool)Session["isActive"];

                // Lista para almacenar las nuevas imágenes
                List<ImageProduct> newImagesList = new List<ImageProduct>();

                // Obtener las nuevas imágenes desde el ViewState
                List<string> newImages = ViewState[NewImagesViewStateKey] as List<string>;
                if (newImages != null && newImages.Count > 0)
                {
                    foreach (string imageUrl in newImages)
                    {
                        ImageProduct imgAux = new ImageProduct();
                        imgAux.UrlImage = imageUrl;
                        imgAux.CodProd = product.Code;

                        newImagesList.Add(imgAux);
                    }
                }

                // Asignar las nuevas imágenes al producto
                product.Images = newImagesList;

                BusinessProduct businessProduct = new BusinessProduct();
                currentProductId = Session["idCurrentItem"] != null ? (int)Session["idCurrentItem"] : 0;

                if (currentProductId != 0)
                {
                    product.Id = currentProductId;

                    // Obtener la lista de IDs de imágenes a eliminar
                    List<int> imagesToDelete = GetImagesToDelete();

                    // Llamar al método Edit con las imágenes a eliminar y las nuevas imágenes
                    bool result = businessProduct.Edit(product, imagesToDelete);

                    if (result)
                    {
                        UserControl_Toast.ShowToast("Producto editado correctamente", true);
                        ClearForm();
                    }
                    else
                    {
                        UserControl_Toast.ShowToast("Error al editar el producto", false);
                    }
                    return;
                }
                else
                {

                    bool result = businessProduct.Add(product);
                    if (result)
                    {
                        UserControl_Toast.ShowToast("Producto agregado correctamente", true);
                        ClearForm();
                    }
                    else
                    {
                        UserControl_Toast.ShowToast("Error al agregar el producto", false);
                    }
                }
            }
            catch (Exception ex)
            {

                lblMessage.Text = "Ocurrió un error: " + ex.Message;
                lblMessage.CssClass = "text-danger";
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
            ddlSubCategory.Items.Clear();
            ddlSubCategory.Items.Insert(0, new ListItem("Selecciona una subcategoría", "0"));
            ddlSubCategory.Enabled = false;
            ddlSeason.SelectedIndex = 0;

            ViewState[NewImagesViewStateKey] = new List<string>();
            BindImagesListBox();

            rptExistingImages.DataSource = null;
            rptExistingImages.DataBind();

            currentProductId = 0;
            Session["idCurrentItem"] = currentProductId;
            Session["CurrentProductCode"] = "";


            imgPreview.Visible = false;

            lblMessage.Text = "";
            lblMessage.CssClass = "";
        }

        private void FillForm(int id)
        {
            BusinessProduct businessProduct = new BusinessProduct();

            try
            {
                List<Product> product = businessProduct.listAdmin(id);

                if (product.Count > 0)
                {
                    currentProductCode = product[0].Code;

                    txtCode.Text = product[0].Code;
                    txtCode.ReadOnly = true;
                    txtCode.Enabled =  false;
                    txtName.Text = product[0].Name;
                    txtPrice.Text = product[0].Price.ToString();
                    txtDescription.Text = product[0].Description;

                    Session["isActive"] = product[0].IsActive;
                    Session["CurrentProductCode"] = currentProductCode;

                    ddlCategory.SelectedValue = product[0].Category.Id.ToString();
                    BindSubCategories(product[0].Category.Id);

                    string subCategoryId = product[0].SubCategory.Id.ToString();
                    if (ddlSubCategory.Items.FindByValue(subCategoryId) != null)
                    {
                        ddlSubCategory.SelectedValue = subCategoryId;
                    }
                    else
                    {
                        ddlSubCategory.SelectedValue = "0"; 
                        lblMessage.Text = "La subcategoría del producto no está disponible para la categoría seleccionada.";
                        lblMessage.CssClass = "text-warning";
                    }

                    ddlSeason.SelectedValue = product[0].Season.Id.ToString();


                    HandleToggleStateButton(product[0].IsActive);
                }
                else
                {
                    Response.Redirect("ProductsManagement.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cargar el producto: " + ex.Message;
                lblMessage.CssClass = "text-danger";
            }
        }

        private void HandleToggleStateButton(bool product)
        {
            btnToggleEstado.Text = product ? "Desactivar" : "Activar";
            btnToggleEstado.CssClass = "";
            btnToggleEstado.CssClass = product ? "btn btn-danger" : "btn btn-success";

        }
        //IMAGENES
        private void InitializeImages()
        {
            if (ViewState[NewImagesViewStateKey] == null)
            {
                ViewState[NewImagesViewStateKey] = new List<string>();
            }
        }
        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            string imgUrl = txtImageUrl.Text.Trim();

            if (string.IsNullOrEmpty(imgUrl))
            {
                return;
            }

            List<string> newImages = ViewState[NewImagesViewStateKey] as List<string>;

            newImages.Add(imgUrl);
            ViewState[NewImagesViewStateKey] = newImages;

            BindImagesListBox();

            txtImageUrl.Text = "";
            lblMessage.Text = "";
        }

        private void BindImagesListBox()
        {
            List<string> newImages = ViewState[NewImagesViewStateKey] as List<string>;
            lstImages.Items.Clear();
            foreach (string imageUrl in newImages)
            {
                lstImages.Items.Add(new ListItem(imageUrl));
            }
        }

        private void BindImagesRepeater()
        {
            BusinessImageProduct businessImageProduct = new BusinessImageProduct();

            currentProductId = Session["idCurrentItem"] != null ? (int)Session["idCurrentItem"] : 0;

            if (currentProductId != 0)
            {
                if (string.IsNullOrEmpty(currentProductCode))
                {
                    BusinessProduct businessProduct = new BusinessProduct();
                    List<Product> product = businessProduct.listAdmin(currentProductId);
                    if (product.Count > 0)
                    {
                        currentProductCode = product[0].Code;
                    }
                }

                List<ImageProduct> images = businessImageProduct.list(currentProductCode);
                rptExistingImages.DataSource = images;
                rptExistingImages.DataBind();
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
            List<string> images = ViewState[NewImagesViewStateKey] as List<string>;

            if (images == null)
            {
                lblMessage.Text = "No hay imágenes para eliminar";
                lblMessage.CssClass = "text-danger";
                return;
            }

            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem item in lstImages.Items)
            {
                if (item.Selected)
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
            foreach (ListItem item in itemsToRemove)
            {
                images.Remove(item.Text);
            }


            ViewState[NewImagesViewStateKey] = images;


            BindImagesListBox();

            lblMessage.Text = "Imágenes eliminadas correctamente.";
            lblMessage.CssClass = "text-success";
            imgPreview.Visible = false;

        }

        private List<int> GetImagesToDelete()
        {
            List<int> imagesToDelete = new List<int>();
            string imagesToDeleteValue = hfImagesToDelete.Value;

            if (!string.IsNullOrEmpty(imagesToDeleteValue))
            {
                string[] ids = imagesToDeleteValue.Split(',');
                foreach (string idStr in ids)
                {
                    if (int.TryParse(idStr, out int id))
                    {
                        imagesToDelete.Add(id);
                    }
                }
            }

            return imagesToDelete;
        }

        protected void btnToggleEstado_Click(object sender, EventArgs e)
        {

            bool state = (bool)Session["isActive"] ? false : true;

            BusinessProduct businnesProduct = new BusinessProduct();
            try
            {
                bool result = businnesProduct.ToggleActivation(currentProductId, state);
                string message = state ? "activado" : "desactivado";

                if (result)
                {
                    UserControl_Toast.ShowToast($"El producto ha sido {message} correctamente", true);
                    HandleToggleStateButton(state);
                    Session["isActive"] = state;


                }
                else
                {
                    UserControl_Toast.ShowToast("Error al cambiar el producto", false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnEliminarDefinitivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminar.Checked)
                {
                    BusinessProduct businessProduct = new BusinessProduct();

                    currentProductCode = Session["CurrentProductCode"] != null ? (string)Session["CurrentProductCode"] : "";
                    currentProductId = Session["idCurrentItem"] != null ? (int)Session["idCurrentItem"] : 0;

                    bool result = businessProduct.Delete(currentProductId, currentProductCode);

                    if (result)
                    {
                        UserControl_Toast.ShowToast("Producto eliminado correctamente", true);
                        ClearForm();

                        string script = @"
                         setTimeout(function() {
                           window.location.href = 'ProductsManagement.aspx';
                         }, 1260);
                         ";

                        ScriptManager.RegisterStartupScript(this, GetType(), "redirectAfterDelay", script, true);

                    }
                    else
                    {
                        UserControl_Toast.ShowToast("Error al eliminar el producto", false);
                    }

                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}