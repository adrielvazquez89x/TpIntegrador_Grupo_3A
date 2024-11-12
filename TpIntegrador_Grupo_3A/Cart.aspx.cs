using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TpIntegrador_Grupo_3A.User
{
    public partial class Cart : System.Web.UI.Page
    {
        public List<ItemCart> Items { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Model.User user = (Model.User)Session["user"];
            Items = user.Cart.Items;


            repeater.DataSource = Items;
            repeater.DataBind();

        }
    }
}