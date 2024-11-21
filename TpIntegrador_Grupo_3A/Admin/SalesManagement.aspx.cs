using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Business; 

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class SalesManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            BusisnessPurchase businessPurchase = new BusisnessPurchase();
            List<Purchase> purchases = businessPurchase.ListPurchases();

            List<PurchaseView> purchaseViewModels = new List<PurchaseView>();

            BusinessUser businessUser = new BusinessUser();

            foreach (var purchase in purchases)
            {
                string customerName = "";

                Model.User user = businessUser.GetUserById(purchase.IdUser);
                if (user != null)
                {
                    customerName = $"{user.FirstName} {user.LastName}";
                }

                PurchaseView viewModel = new PurchaseView
                {
                    Id = purchase.Id,
                    CustomerName = customerName,
                    Date = purchase.date,
                    Total = purchase.Total,
                    State = purchase.State
                };

                purchaseViewModels.Add(viewModel);
            }

            var filteredData = purchaseViewModels.AsQueryable();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                filteredData = filteredData.Where(s => s.Id.ToString().Contains(txtSearch.Text) || s.CustomerName.Contains(txtSearch.Text));
            }

            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                filteredData = filteredData.Where(s => s.State.Equals(ddlStatus.SelectedValue, StringComparison.OrdinalIgnoreCase));
            }

            gvSales.DataSource = filteredData.ToList();
            gvSales.DataBind();
        }

        protected void gvSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSales.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        
        protected void FilterChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        
        protected string GetStatusCssClass(string status)
        {
            switch (status.ToLower())
            {
                case "pagado":
                    return "badge bg-success";
                case "pendiente":
                    return "badge bg-warning text-dark";
                case "cancelado":
                    return "badge bg-danger";
                default:
                    return "badge bg-secondary";
            }
        }

        protected void gvSales_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSales.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvSales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetPagado" || e.CommandName == "SetPendiente" || e.CommandName == "SetCancelado")
            {
                int purchaseId = Convert.ToInt32(e.CommandArgument);
                string newState = "";

                switch (e.CommandName)
                {
                    case "SetPagado":
                        newState = "Pagado";
                        break;
                    case "SetPendiente":
                        newState = "Pendiente";
                        break;
                    case "SetCancelado":
                        newState = "Cancelado";
                        break;
                }

                BusisnessPurchase businessPurchase = new BusisnessPurchase();
                businessPurchase.Update(purchaseId, newState);

                BindGrid();
            }
        }

    }
}
