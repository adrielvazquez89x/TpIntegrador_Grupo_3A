using Microsoft.Ajax.Utilities;
using Model;
using Model.ProductAttributes;
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
    public partial class Cart : System.Web.UI.Page
    {
        public new List<ItemCart> Items = new List<ItemCart>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (SessionSecurity.ActiveSession(Session["user"]))
                    {
                        Model.User user = (Model.User)Session["user"];
                        Items = user.Cart.Items;

                        repeater.DataSource = Items;
                        repeater.DataBind();
                    }
                    else
                    {
                        Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                        Response.Redirect("Error.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

        }

        protected void btnMore_Click(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];
            int stockId = int.Parse(((LinkButton)sender).CommandArgument);
            Items = user.Cart.Items;
            ItemCart selectedItem = Items.Find(item => item.Stock.Id == stockId);

            bool edited=user.Cart.UpdateProduct(selectedItem.Stock, 0); //envio un 1 para disminuir, un 0 para aumentar
            if (!edited)
            {
                //Control_Toast.ShowToast($"No es posible. El pedido de este producto llegó al límite de stock disponible", false);
            }
            else
            {
                Session["user"] = user;
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnLess_Click(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];
            int stockId = int.Parse(((LinkButton)sender).CommandArgument);
            Items = user.Cart.Items;
            ItemCart selectedItem = Items.Find(item => item.Stock.Id == stockId);

            if (selectedItem.Number > 1)
            {
                user.Cart.UpdateProduct(selectedItem.Stock, 1); //envio un 1 para disminuir, un 0 para aumentar
            }
            else
            {
                //Items.Remove(selectedItem);
                user.Cart.DeleteProduct(stockId);
            }

            Session["user"] = user;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];
            int stockId = int.Parse(((LinkButton)sender).CommandArgument);
            ItemCart selectedItem =Items.Find(item => item.Stock.Id == stockId);

            //Items.Remove(selectedItem);
            user.Cart.DeleteProduct(stockId);

            Session["user"] = user;
            Response.Redirect(Request.RawUrl);
        }
        protected void btnBuy_Click(object sender, EventArgs e)
        {

        }

        protected void btnEmptyCart_Click(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];
            //Items.Clear();
            user.Cart.ClearCart();

            Session["user"] = user;
            Response.Redirect(Request.RawUrl);
        }
    }
}