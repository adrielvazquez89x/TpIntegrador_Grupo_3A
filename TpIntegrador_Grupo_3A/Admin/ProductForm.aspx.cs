using Business;
using Business.ProductAttributes;
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
            
            if(!IsPostBack)
            {
                ddlCategory.DataSource = businessCategory.list(false);
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataTextField = "Description";
                ddlCategory.DataBind();

                ddlSeason.DataSource = businessSeason.list(false);
                ddlSeason.DataValueField = "Id";
                ddlSeason.DataTextField = "Description";
                ddlSeason.DataBind();

                ddlSection.DataSource = businessSection.list(false);
                ddlSection.DataValueField = "Id";
                ddlSection.DataTextField = "Description";
                ddlSection.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}