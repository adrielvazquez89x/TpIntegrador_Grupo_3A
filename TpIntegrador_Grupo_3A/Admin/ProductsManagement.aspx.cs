using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.ProductAttributes;
using Model;
using static TpIntegrador_Grupo_3A.Admin.Categories;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class Products : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //volver a poner para verificar la seguridad
            //var user = Session["user"];
            //if (user == null || !Security.isAdmin(user))
            //{
            //    // Redirige a Login.aspx si no es un administrador o si no hay sesión
            //    Response.Redirect("~/Login.aspx");
            //}
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
            dgvProducts.PageIndex = e.NewPageIndex;
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

                // Obtener la información del producto
                BusinessProduct businessProduct = new BusinessProduct();
                BusinessStock businessStock = new BusinessStock();
                List<Product> listProduct = businessProduct.list(productCode);
                Product product = listProduct.FirstOrDefault();
                List<Model.ProductAttributes.Stock> stockActual = businessStock.list(productCode);

                if (product != null)
                {
                    // Obtener el stock actual (ajusta según tu modelo de datos)
                    
                    // Obtener las listas de colores y talles
                    BusinessColour businessColour = new BusinessColour();
                    List<Colour> colores = businessColour.list();

                    BusinessSize businessSize = new BusinessSize();
                    List<Size> talles = businessSize.list();

                    // Mostrar el modal con la información del producto
                    UserControl_ModalStock.ShowModal(product.Code, product.Name, stockActual, colores, talles);
                }
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