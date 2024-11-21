using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.ProductAttributes;
using Model;
using Security;
using static TpIntegrador_Grupo_3A.Admin.Categories;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class ProductsManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var user = Session["user"];
            if (user == null || !Security.SessionSecurity.IsAdmin(user))
            {
                // Redirige a Login.aspx si no es un administrador o si no hay sesión
                Response.Redirect("~/Login.aspx");
            }
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
                List<Product> products = businessProducts.listAdmin();

                dgvProducts.DataSource = products;
                dgvProducts.DataBind();
            }
            catch (Exception)
            {
                UserControl_Toast.ShowToast("Error al cargar las categorías", false);
            }
        }

        protected void dgvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex;

            // Validar que el nuevo índice está dentro del rango válido
            if (newPageIndex < 0)
            {
                newPageIndex = 0;
            }
            else if (newPageIndex >= dgvProducts.PageCount)
            {
                newPageIndex = dgvProducts.PageCount - 1;
            }

            dgvProducts.PageIndex = newPageIndex;
            BindProducts();
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductForm.aspx");
        }

        protected void dgvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditStock")
            {
                string productCode = e.CommandArgument.ToString();

                // Redirigir a StockManagement.aspx pasando el código del producto
                Response.Redirect("StockManagement.aspx?code=" + Server.UrlEncode(productCode));
            }
            else if (e.CommandName == "EditProduct")
            {
                string productId = e.CommandArgument.ToString();
                Response.Redirect("ProductForm.aspx?id=" + productId);
            }
            else if (e.CommandName == "View")
            {
                string code = e.CommandArgument.ToString();
                string url = ResolveUrl($"~/Details.aspx?code={code}");
                string script = $"window.open('{url}', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
            }
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string arg = btn.CommandArgument;

            Response.Redirect("ProductForm.aspx?id=" + arg);
        }
    }
}