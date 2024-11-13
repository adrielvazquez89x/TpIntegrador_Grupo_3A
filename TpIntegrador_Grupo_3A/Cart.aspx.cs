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
    public partial class Cart : System.Web.UI.Page
    {
        public List<ItemCart> Items = new List<ItemCart>();
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
    }
}