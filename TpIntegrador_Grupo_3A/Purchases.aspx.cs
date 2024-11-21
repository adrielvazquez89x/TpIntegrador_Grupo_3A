using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TpIntegrador_Grupo_3A.Admin;

namespace TpIntegrador_Grupo_3A
{
    public partial class Purchases : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["user"] != null)
                {
                    Model.User user = (Model.User)Session["user"];
                    BindPurchases(user.UserId.ToString());
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }


        private void BindPurchases(string userId)
        {
            try
            {
                BusisnessPurchase businessPurchase = new BusisnessPurchase();
                List<Purchase> purchases = businessPurchase.ListPurchases(userId);
                if (purchases == null || purchases.Count == 0)
                {
                    // si no hay compras  muestro el mensaje y oculto el GridView
                    noPurchasesPanel.Visible = true; 
                    dgvPurchases.Visible = false;   
                }
                else
                {
                    // Si hay compras, muestr el GridView y oculto el mensaje
                    noPurchasesPanel.Visible = false; 
                    dgvPurchases.Visible = true;      
                    dgvPurchases.DataSource = purchases;
                    dgvPurchases.DataBind();
                }

                dgvPurchases.DataSource = purchases;
                dgvPurchases.DataBind();
            }
            catch (Exception)
            {
                Control_Toast.ShowToast("Error al cargar los usuarios", false);
            }
        }


        protected void dgvPurchases_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvPurchases_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPurchases.PageIndex = e.NewPageIndex;

            if (Session["user"] != null)
            {
                Model.User user = (Model.User)Session["user"];
                string userId = user.UserId.ToString();
                BindPurchases(userId);
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnViewDetails_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

           
            int purchaseId = Convert.ToInt32(btn.CommandArgument);

            bool isDetailsVisible = dgvPurchaseDetails.Visible;

            if (!isDetailsVisible)
            {
                
                BusisnessPurchase businessPurchase = new BusisnessPurchase();
                List<PurchaseDetail> purchaseDetails = businessPurchase.ListPurchaseDetails(purchaseId);

                
                dgvPurchaseDetails.DataSource = purchaseDetails;
                dgvPurchaseDetails.DataBind();

                
                dgvPurchaseDetails.Visible = true;
            }
            else
            {
                
                dgvPurchaseDetails.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx",false);
        }
    }
}