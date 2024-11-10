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
using Security;

namespace TpIntegrador_Grupo_3A
{
    public partial class Products : System.Web.UI.Page
    {
        public List<Product> prodList;
        public List<Model.ImageProduct> ImageList;

        public string CodeSelectedProd;
        public int IdSelectedProd;
        public bool SessionOn { get; set; }
        public Model.User user {  get; set; }
        public bool ProdIsFav { get; set; }

        public Product selectedProd = new Product();
        public Category selectedCateg = new Category();

        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessProduct businessProd = new BusinessProduct();
            int idCategory =Request.QueryString["Idcategory"] is null ? 0 : int.Parse(Request.QueryString["Idcategory"]);  //validarlo (podrian a mano ponerle algo no entero)

            int idSubCategory = Request.QueryString["IdSubCategory"] is null ? 0 : int.Parse(Request.QueryString["IdSubCategory"]);  //validarlo (podrian a mano ponerle algo no entero)

            prodList= prodList is null ? businessProd.list() : prodList;

            if (!IsPostBack)
            {
                if (idCategory == 0)
                {
                    //prodList = businessProd.list();    //Carga los productos
                    string filter = Session["productFilter"] as string;
                    if (!string.IsNullOrEmpty(filter))
                    {
                        prodList = prodList.FindAll(prod => prod.Name.ToUpper().Contains(filter.ToUpper()) ||
                                                            prod.Description.ToUpper().Contains(filter.ToUpper()) ||
                                                            prod.Category.Description.ToUpper().Contains(filter.ToUpper()) ||
                                                            prod.SubCategory.Description.ToUpper().Contains(filter.ToUpper()) ||
                                                            prod.Season.Description.ToUpper().Contains(filter.ToUpper()) ||
                                                            prod.Price.ToString().Contains(filter));
                        Session.Remove("productFilter");
                    }
                }
                else
                {
                    prodList = businessProd.listByCategory(idCategory, idSubCategory);    //Carga los productos según la categoría
                }
                rptProdList.DataSource = prodList;
                rptProdList.DataBind();
            }
            if (SessionSecurity.ActiveSession(Session["user"]))
            {
                user = (Model.User)Session["user"];
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
            CodeSelectedProd = (((Button)sender).CommandArgument).ToString();
            Response.Redirect($"/Details?Code={CodeSelectedProd}");
        }

        protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOrdenar.SelectedIndex)
            {
                case 0:
                    prodList = prodList.OrderBy(x => x.Name).ToList();
                    break;
                case 1:
                    prodList = prodList.OrderByDescending(x => x.Name).ToList();
                    break;
                case 2:
                    prodList = prodList.OrderBy(x => x.Price).ToList();
                    break;
                case 3:
                    prodList = prodList.OrderByDescending(x => x.Price).ToList();
                    break;
            }
            rptProdList.DataSource = prodList;
            rptProdList.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            int min=0, max=0;

            min = txtPriceMin.Text is null ? 0 : int.Parse(txtPriceMin.Text);

            max = txtPriceMax.Text is null ? 0 : int.Parse(txtPriceMax.Text);


            if (max == 0)
                prodList = prodList.FindAll(x => x.Price >= min);
            else
                prodList = prodList.FindAll(x => x.Price >= min && x.Price <= max);

            rptProdList.DataSource = prodList;
            rptProdList.DataBind();
        }
    }
}