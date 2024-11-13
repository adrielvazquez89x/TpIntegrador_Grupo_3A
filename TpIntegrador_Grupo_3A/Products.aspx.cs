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
            try 
            {
                BusinessProduct businessProd = new BusinessProduct();
                
                int idCategory = Request.QueryString["Idcategory"] is null ? 0 : int.Parse(Request.QueryString["Idcategory"]);  //validarlo (podrian a mano ponerle algo no entero)

                int idSubCategory = Request.QueryString["IdSubCategory"] is null ? 0 : int.Parse(Request.QueryString["IdSubCategory"]);  //validarlo (podrian a mano ponerle algo no entero)

                int idSection = Request.QueryString["IdSection"] is null ? 0 : int.Parse(Request.QueryString["IdSection"]);  //validarlo (podrian a mano ponerle algo no entero)

                prodList = prodList is null ? businessProd.list() : prodList;


                if (!IsPostBack)
                {
                    if (idCategory == 0)
                    {
                        if (idSection == 0)
                        {
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
                            prodList = businessProd.listBySection(idSection);    //Carga los productos según la seccion
                        }
                    }
                    else
                    {
                        prodList = businessProd.listByCategory(idCategory, idSubCategory);    //Carga los productos según la categoría
                    }

                    Session.Add("AllProducts", prodList);

                    rptProdList.DataSource = prodList;
                    rptProdList.DataBind();
                }
                if (SessionSecurity.ActiveSession(Session["user"]))
                {
                    user = (Model.User)Session["user"];
                }
            }
            catch (Exception ex) 
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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

        protected void OrderByCriteria()
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
        }

        private void filterByPrice() 
        {
            int min = 0, max = 0;

            string.IsNullOrEmpty(txtPriceMin.Text);
            min = string.IsNullOrEmpty(txtPriceMin.Text) ? 0 : int.Parse(txtPriceMin.Text);

            max = string.IsNullOrEmpty(txtPriceMax.Text) ? 0 : int.Parse(txtPriceMax.Text);


            if (max == 0)
                prodList = prodList.FindAll(x => x.Price >= min);
            else
                prodList = prodList.FindAll(x => x.Price >= min && x.Price <= max);

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            prodList = (List<Product>)Session["AllProducts"];
            filterByPrice();
            OrderByCriteria();
            rptProdList.DataSource = prodList;
            rptProdList.DataBind();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            prodList = (List<Product>)Session["AllProducts"];
            txtPriceMax.Text = "";
            txtPriceMin.Text = "";
            ddlOrdenar.SelectedIndex = 0;

            rptProdList.DataSource = prodList;
            rptProdList.DataBind();
        }
    }
}