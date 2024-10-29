using Business;
using Business.ProductAttributes;
using Model;
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
        public int IdSelectedProd;
        public User user { get; set; }
        public List<Product> products { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;
            if (id > 0 && !IsPostBack)
            {
                BusinessProduct businessProduct = new BusinessProduct();
                products = businessProduct.list(id);
                rptProducts.DataSource = products;
                rptProducts.DataBind();
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            IdSelectedProd = int.Parse(((Button)sender).CommandArgument);
        }


        protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int id = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Product currentProduct = (Product)e.Item.DataItem;
                Repeater rptImagesList = (Repeater)e.Item.FindControl("rptImages"); // Toma el Repeater anidado
                rptImagesList.DataSource = currentProduct.Images;
                rptImagesList.DataBind();
            }
        }

        protected void bntFav_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Add(user.Id, IdSelectedProd);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}