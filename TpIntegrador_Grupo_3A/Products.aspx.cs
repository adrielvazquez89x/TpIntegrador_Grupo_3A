using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Microsoft.Ajax.Utilities;
using Model;

namespace TpIntegrador_Grupo_3A
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Product> prodList;
        public List<Model.ImageProduct> ImageList;

        public int IdSelectedArt;

        public Product selectedProd = new Product();

        public Category selectedCateg = new Category();

        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessProduct businessProd = new BusinessProduct();
            BusinessImageProduct businessImage = new BusinessImageProduct();
            prodList = businessProd.list();
            if (!IsPostBack)
            {
                //for (int i = 0; i < prodList.Count; i++)
                //{
                //    Product aux = prodList[i];
                //    aux.Images = businessImage.list(aux.Id);
                //}
                rptProdList.DataSource = prodList;
                rptProdList.DataBind();
            }


            //string category = Request.QueryString["category"];
            //if (!IsPostBack)
            //{
            //    // LoadProductsByCategory(category);    //Carga los productos según la categoría
            //}
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

        protected void btnPick_Click(object sender, EventArgs e)
        {

        }
    }
}