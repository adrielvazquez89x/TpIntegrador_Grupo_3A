using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Model;
using static TpIntegrador_Grupo_3A.Admin.Categories;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Products : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            try
            {
                BusinessProduct businessProducts = new BusinessProduct();
                List<Product> categories = businessProducts.listAdmin();

                dgvProducts.DataSource = categories;
                dgvProducts.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las categorías", false);
            }
        }

        protected void dgvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProducts.PageIndex = e.NewPageIndex;
            BindProducts();
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductForm.aspx");
        }

        protected void dgvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}