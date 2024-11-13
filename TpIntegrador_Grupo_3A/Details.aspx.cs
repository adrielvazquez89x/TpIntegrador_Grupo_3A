using Business;
using Business.ProductAttributes;
using Microsoft.Ajax.Utilities;
using Model;
using Model.ProductAttributes;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TpIntegrador_Grupo_3A.Admin;

namespace TpIntegrador_Grupo_3A
{
    public partial class Details : System.Web.UI.Page
    {
        public string CodeSelectedProd;
        public bool isFavorite;
        public Model.User user { get; set; }
        public List<Product> products { get; set; }
        public Stock stock = new Stock();

        protected void Page_Load(object sender, EventArgs e)
        {
            CodeSelectedProd = Request.QueryString["Code"] != null ? (Request.QueryString["Code"]).ToString() : "";
            if(CodeSelectedProd=="")
                Response.Redirect("~/Default.aspx", false);

            if (!IsPostBack)
            {
                LoadSizes();
                LoadColours();
                if (CodeSelectedProd != "")
                {
                    BusinessProduct businessProduct = new BusinessProduct();
                    products = businessProduct.list(CodeSelectedProd);
                    rptProducts.DataSource = products;
                    rptProducts.DataBind();
                }
            }

           
            if (SessionSecurity.ActiveSession(Session["user"]))
            {
                user = (Model.User)Session["user"];

                BusinessFavourite businessFav = new BusinessFavourite();
                string favCodes = businessFav.list(user.UserId).Select(fav => fav.ProductCode).ToList().ToString();
                Session["favCodes"] = favCodes; // Guardo los favoritos en sesión
                isFavorite = checkFav(CodeSelectedProd, user);
            }
        }

        private void LoadSizes()
        {
            BusinessSize businessSize = new BusinessSize();
            List<Size> sizes = businessSize.list();

            ddlSize.DataSource = sizes;
            ddlSize.DataTextField = "Description";
            ddlSize.DataValueField = "Id";
            ddlSize.DataBind();

            ddlSize.Items.Insert(0, new ListItem("Seleccione un talle", "0")); // Opción por defecto
        }

        private void LoadColours()
        {
            BusinessColour businessColour = new BusinessColour();
            List<Colour> colours = businessColour.list();

            ddlColour.DataSource = colours;
            ddlColour.DataTextField = "Description";
            ddlColour.DataValueField = "Id";
            ddlColour.DataBind();

            ddlColour.Items.Insert(0, new ListItem("Seleccione un color", "0"));
        }

        private bool checkFav(string code, Model.User user)
        {
            BusinessFavourite businesFav = new BusinessFavourite();
            List<FavouriteProducts> listFav = businesFav.list();

            foreach (FavouriteProducts fav in listFav)
            {
                if (fav.IdUser == user.UserId && fav.ProductCode == code)
                {
                    return true;
                }
            }
            return false;
        }
        protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // int id = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Product currentProduct = (Product)e.Item.DataItem;
                Repeater rptImagesList = (Repeater)e.Item.FindControl("rptImages"); // Toma el Repeater anidado
                rptImagesList.DataSource = currentProduct.Images;
                rptImagesList.DataBind();


                // Retomo los Code de los Favoritos del usuario, guardados en sesion:
                List<string> favCodes = Session["favCodes"] as List<string>;

                //Indico si el producto está en favoritos
                isFavorite = favCodes != null && favCodes.Contains(currentProduct.Code);
            }
        }

        protected void btnFav_Click(object sender, EventArgs e)
        {
            try
            {
                CodeSelectedProd = ((LinkButton)sender).CommandArgument.ToString();
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Add(user.UserId, CodeSelectedProd);
                isFavorite = true;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnUndoFav_Click(object sender, EventArgs e)
        {
            try
            {
                CodeSelectedProd = ((LinkButton)sender).CommandArgument.ToString();
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Delete(user.UserId, CodeSelectedProd);
                isFavorite = false;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);

            quantity++;

            txtQuantity.Text = quantity.ToString();
        }

        protected void btnSubtract_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);

            if (quantity > 0)
            {
                quantity--;

                txtQuantity.Text = quantity.ToString();
            }
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            //CodeSelectedProd = ((Button)sender).CommandArgument.ToString();
            BusinessProduct businessProduct = new BusinessProduct();
            Product selectedProd = businessProduct.listByCode(CodeSelectedProd);
            int number = int.Parse(txtQuantity.Text);

            // Obtener el talle y color seleccionados
            int selectedSizeId = int.Parse(ddlSize.SelectedValue);
            int selectedColourId = int.Parse(ddlColour.SelectedValue);

            // Validar selección
            if (selectedSizeId == 0 || selectedColourId == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Por favor, selecciona un talle y un color.";
                return;
            }

            Model.User user = (Model.User)Session["user"];

            //aca habria primero que sumar number con la cant en carrito si es que ya existe !
            bool validateStock = ValidateStock(selectedColourId, selectedSizeId, number);
            bool added=false;
            if (validateStock)
            {
               added = user.Cart.AddProduct(selectedProd, stock, number);
            }
            if (!added)
            {
                Control_Toast.ShowToast($"Este pedido no se sumo. Al agregar esto se excede el stock disponible", false);
            }
            else 
            {
                Session["user"] = user;
                Response.Redirect(Request.RawUrl); // Redirige a la misma página para actualizar la vista
            }            
        }
        protected bool ValidateStock(int selectedColourId, int selectedSizeId, int number)
        {
            BusinessStock businessStock = new BusinessStock();
            stock = businessStock.getStock(CodeSelectedProd, selectedColourId, selectedSizeId);
            if (number > stock.Amount)
            {
                Control_Toast.ShowToast($"No es posible añadirlo al carrito. \n El producto con el talle y color seleccionados cuenta con un stock de {stock.Amount} prendas", false);
                return false;
            }
            else
            {
                Control_Toast.ShowToast("Articulo Añadido exitosamente", true);
                //todavia no se resta del stock, eso recien al confirmar la compra se vuelve a ver
                return true;
            }
        }


    }
}