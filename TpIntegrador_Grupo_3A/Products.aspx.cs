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


            if (!IsPostBack)
            {
                if (idCategory == 0)
                {
                    prodList = businessProd.list();    //Carga los productos
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

        protected void bntFav_Click(object sender, EventArgs e)
        {
            try
            {
                CodeSelectedProd = ((LinkButton)sender).CommandArgument.ToString();
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Add(user.UserId, CodeSelectedProd);

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
                CodeSelectedProd = ((Button)sender).CommandArgument.ToString();
                BusinessFavourite businessFav = new BusinessFavourite();
                businessFav.Delete(user.UserId, CodeSelectedProd);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}