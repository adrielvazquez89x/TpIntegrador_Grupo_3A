using Business;
using Microsoft.AspNet.Identity;
using Model;
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

            // chequeamos si la pag que estoy por cargar no es ninguna de estas 2
            // si no es quiero que me verifique la seguridad 

            if (SessionSecurity.ActiveSession(Session["user"]))
            {
                Model.User user = (Model.User)Session["user"];
                lblUser.Text = user.FirstName;
                if (!string.IsNullOrEmpty(user.ImageUrl))
                    imgAvatar.ImageUrl = "~/img/" + ((Model.User)Session["user"]).ImageUrl;
                else
                    imgAvatar.ImageUrl = "https://www.pngplay.com/wp-content/uploads/12/User-Avatar-Profile-Download-Free-PNG.png";
            }
            else
            {
                if (!(Page is Login || Page is Register || Page is Products || Page is Default))
                    Response.Redirect("Login.aspx", false);
            }

            //if (Request.Url.AbsolutePath.Contains("login") || Request.Url.AbsolutePath.Contains("Login"))
            //{
            //    sidebar.Visible = false;
            //}
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


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}