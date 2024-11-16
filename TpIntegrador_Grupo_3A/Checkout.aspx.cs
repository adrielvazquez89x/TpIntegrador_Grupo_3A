using Model;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A
{
    public partial class Checkout : System.Web.UI.Page
    {
        public Model.Cart Cart {get;set;}
        public new List<ItemCart> Items = new List<ItemCart>();
        public decimal Total;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionSecurity.ActiveSession(Session["user"]))
                {
                    Model.User user = (Model.User)Session["user"];
                    Cart = ((User)Session["user"]).Cart;

                    if (Cart.Items.Count == 0)
                        Response.Redirect("Cart.aspx", false);

                    Items = user.Cart.Items;
                    Total = user.Cart.SumTotal();
                    //repeater.DataSource = Items;
                    //repeater.DataBind();

                    txtName.Text = user.FirstName;
                }
                else
                {
                    Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                    Response.Redirect("Error.aspx", false);
                }
            }
        }
    }
}