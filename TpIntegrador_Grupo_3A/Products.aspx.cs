using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.ProductAttributes;
using Microsoft.Ajax.Utilities;
using Model;
using Model.ProductAttributes;

namespace TpIntegrador_Grupo_3A
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Product> prodList;
        public List<Model.ImageProduct> ImageList;

        public int IdSelectedProd;
        public bool SessionOn { get; set; }
        public User user {  get; set; }
        public bool ProdIsFav { get; set; }

        public Product selectedProd = new Product();
        public Category selectedCateg = new Category();

        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessProduct businessProd = new BusinessProduct();
            int idCategory =Request.QueryString["Idcategory"] is null ? 0 : int.Parse(Request.QueryString["Idcategory"]);  //validarlo (podrian a mano ponerle algo no entero)

            int idSubCategory = Request.QueryString["IdSubCategory"] is null ? 0 : int.Parse(Request.QueryString["IdSubCategory"]);  //validarlo (podrian a mano ponerle algo no entero)


            if (!IsPostBack)
            {
                prodList = businessProd.listByCategory(idCategory, idSubCategory);    //Carga los productos según la categoría

                rptProdList.DataSource = prodList;
                rptProdList.DataBind();
            }

        }

        protected void rptProdList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Product currentProduct = (Product)e.Item.DataItem;
                Repeater rptImagesList = (Repeater)e.Item.FindControl("rptImagesList"); // Toma el Repeater anidado
                rptImagesList.DataSource = currentProduct.Images;
                rptImagesList.DataBind();
            }
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            IdSelectedProd = int.Parse(((Button)sender).CommandArgument);
            Response.Redirect($"/Details?id={IdSelectedProd}");
        }

        protected void bntFav_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Add(user.UserId, IdSelectedProd);

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
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Delete(user.UserId, IdSelectedProd);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}