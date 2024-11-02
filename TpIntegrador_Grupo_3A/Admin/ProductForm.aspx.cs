using Business;
using Business.ProductAttributes;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class ProductForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessCategory businessCategory = new BusinessCategory();
            BusinessSeason businessSeason = new BusinessSeason();
            BusinessSection businessSection = new BusinessSection();
            int categoryId;

            if (!IsPostBack)
            {
                ddlCategory.DataSource = businessCategory.list(false);
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataTextField = "Description";
                ddlCategory.DataBind();
                categoryId = int.Parse(ddlCategory.Items[0].Value);


                ddlSeason.DataSource = businessSeason.list(false);
                ddlSeason.DataValueField = "Id";
                ddlSeason.DataTextField = "Description";
                ddlSeason.DataBind();

                ddlSection.DataSource = businessSection.list(false);
                ddlSection.DataValueField = "Id";
                ddlSection.DataTextField = "Description";
                ddlSection.DataBind();

                BindSubCategories(categoryId);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdCategory = int.Parse(ddlCategory.SelectedValue);
            BindSubCategories(IdCategory);

        }

        private void BindSubCategories(int IdCategory)
        {
            BusinessSubCategory businessSubCategory = new BusinessSubCategory();
            ddlSubCategory.DataSource = businessSubCategory.list(IdCategory);
            ddlSubCategory.DataValueField = "Id";
            ddlSubCategory.DataTextField = "Description";
            ddlSubCategory.DataBind();

            if (ddlSubCategory.Items.Count == 0)
            {
                ddlSubCategory.Enabled = false;
                ddlSubCategory.Items.Insert(0, new ListItem("No hay subcategorías", "0"));
            }
            else
            {
                ddlSubCategory.Enabled = true;
            }
        }
    }
}