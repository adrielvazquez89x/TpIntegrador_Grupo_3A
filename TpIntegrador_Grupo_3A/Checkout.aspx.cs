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
        public Model.Cart Cart { get; set; }
        public new List<ItemCart> Items = new List<ItemCart>();
        public decimal Total;
        public bool delivery = false;
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

                    txtName.Text = user.FirstName is null ? "" : user.FirstName;
                    txtDni.Text = user.Dni is null ? "" : user.Dni.ToString();
                    txtProvince.Text = user.Address.Province is null ? "" : user.Address.Province;
                    txtTown.Text = user.Address.Town is null ? "" : user.Address.Town;
                    txtDistrict.Text = user.Address.District is null ? "" : user.Address.District;
                    txtCP.Text = user.Address.CP is null ? "" : user.Address.CP.ToString();
                    txtStreet.Text = user.Address.Street is null ? "" : user.Address.Street;
                    txtNumber.Text = user.Address.Number.ToString() is null ? "" : user.Address.Number.ToString();
                    txtFloor.Text = user.Address.Floor is null ? "" : user.Address.Floor;
                    txtUnit.Text = user.Address.Unit is null ? "" : user.Address.Unit;

                    ViewState["delivery"] = false; //inicia con la opcion de retiro en tienda
                }
                else
                {
                    Session.Add("error", "Debes estar logueado para ingresar a esta seccion");
                    Response.Redirect("Error.aspx", false);
                }
            }
            else
            {
                if (ViewState["delivery"] != null)
                    delivery = (bool)ViewState["delivery"];
            }
        }

        protected void ddlEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            delivery = ddlEntrega.SelectedValue == "1";
            ViewState["delivery"] = delivery;
            UpdatePanelDelivery.Update();
        }
    }
}