using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class SiteMaster : MasterPage
    {
        public List<Category> categList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BusinessCategory businessCategory = new BusinessCategory();
                categList = businessCategory.list();

                RepeaterSidebar.DataSource = categList;
                RepeaterSidebar.DataBind();
            }

            if (Request.Url.AbsolutePath.Contains("login") || Request.Url.AbsolutePath.Contains("Login"))
            {
                sidebar.Visible = false;
            }
        }

        protected void RepeaterSidebar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Category currentCategory = (Category)e.Item.DataItem;

                // Obtener el HiddenField que almacena el IdCategory
                HiddenField hfCategoryId = (HiddenField)e.Item.FindControl("hfCategoryId");

                // Establecer el valor del HiddenField al Id de la categoría actual
                hfCategoryId.Value = currentCategory.Id.ToString();

                Repeater rptSubCat = (Repeater)e.Item.FindControl("rptSubCat"); // Toma el Repeater anidado
                rptSubCat.DataSource = currentCategory.SubCategory;
                rptSubCat.DataBind();
            }
        }

        protected void rptSubCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtengo el IdCategory del Repeater padre:
                RepeaterItem parentRptItem = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
                HiddenField hfCategoryId = (HiddenField)parentRptItem.FindControl("hfCategoryId");

                int subCategoryId = (int)DataBinder.Eval(e.Item.DataItem, "Id");

                // Configuro el enlace
                HyperLink link = (HyperLink)e.Item.FindControl("SubCategoryLink");
                link.NavigateUrl = $"/products?IdCategory={hfCategoryId.Value}&IdSubCategory={subCategoryId}";
            }
        }
    }
}