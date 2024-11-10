using Business;
using Business.ProductAttributes;
using Model;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Details : System.Web.UI.Page
    {
        public string CodeSelectedProd;
        public bool isFavorite;
        public Model.User user { get; set; }
        public List<Product> products { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["Code"] != null ? (Request.QueryString["Code"]).ToString() : "";
            if (code != "" && !IsPostBack)
            {
                BusinessProduct businessProduct = new BusinessProduct();
                products = businessProduct.list(code);
                rptProducts.DataSource = products;
                rptProducts.DataBind();
            }
            if (SessionSecurity.ActiveSession(Session["user"]))
            {
                user = (Model.User)Session["user"];

                BusinessFavourite businessFav = new BusinessFavourite();
                string favCodes = businessFav.list(user.UserId).Select(fav => fav.ProductCode).ToList().ToString();
                Session["favCodes"] = favCodes; // Guardo los favoritos en sesión
                isFavorite = checkFav(code, user);
            }
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
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            CodeSelectedProd = ((LinkButton)sender).CommandArgument.ToString();
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

        protected void bntFav_Click(object sender, EventArgs e)
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

        protected void btnSubtract_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);

            if (quantity > 0)
            {
                quantity--;

                txtQuantity.Text = quantity.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);

            quantity++;

            txtQuantity.Text = quantity.ToString();
        }
    }
}