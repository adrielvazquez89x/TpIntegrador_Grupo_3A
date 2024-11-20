using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

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

        private List<Sale> GenerateMockSales()
        {
            return new List<Sale>
            {
                new Sale { OrderNumber = "ORD-001", Customer = "John Doe", Date = new DateTime(2023, 5, 15), Total = 150.00m, PaymentStatus = "Paid", Items = 3, DeliveryMethod = "Standard Shipping" },
                new Sale { OrderNumber = "ORD-002", Customer = "Jane Smith", Date = new DateTime(2023, 5, 16), Total = 75.50m, PaymentStatus = "Pending", Items = 2, DeliveryMethod = "Express Delivery" },
                new Sale { OrderNumber = "ORD-003", Customer = "Bob Johnson", Date = new DateTime(2023, 5, 17), Total = 200.00m, PaymentStatus = "Cancelled", Items = 4, DeliveryMethod = "In-store Pickup" },
                // Agrega más datos simulados según sea necesario
                new Sale { OrderNumber = "ORD-004", Customer = "Alice Brown", Date = new DateTime(2023, 6, 1), Total = 120.00m, PaymentStatus = "Paid", Items = 1, DeliveryMethod = "Standard Shipping" },
                new Sale { OrderNumber = "ORD-005", Customer = "Charlie Davis", Date = new DateTime(2023, 6, 3), Total = 250.00m, PaymentStatus = "Pending", Items = 5, DeliveryMethod = "Express Delivery" },
                new Sale { OrderNumber = "ORD-006", Customer = "Eve Martinez", Date = new DateTime(2023, 6, 5), Total = 300.00m, PaymentStatus = "Paid", Items = 6, DeliveryMethod = "In-store Pickup" },
                // ... más ventas
            };
        }
        private List<Sale> SalesData
        {
            get
            {
                if (ViewState["SalesData"] == null)
                {
                    ViewState["SalesData"] = GenerateMockSales();
                }
                return (List<Sale>)ViewState["SalesData"];
            }
            set
            {
                ViewState["SalesData"] = value;
            }
        }
        private void BindGrid()
        {
            var filteredData = SalesData.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                filteredData = filteredData.Where(s => s.OrderNumber.Contains(txtSearch.Text) || s.Customer.Contains(txtSearch.Text));
            }
            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                filteredData = filteredData.Where(s => s.PaymentStatus == ddlStatus.SelectedValue);
            }

            gvSales.DataSource = filteredData.ToList();
            gvSales.DataBind();
        }

        protected void gvSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSales.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        // Maneja cambios en los filtros
        protected void FilterChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        // Devuelve la clase CSS correspondiente al estado de pago
        protected string GetStatusCssClass(string status)
        {
            switch (status)
            {
                case "Paid":
                    return "badge bg-success";
                case "Pending":
                    return "badge bg-warning text-dark";
                case "Cancelled":
                    return "badge bg-danger";
                default:
                    return "badge bg-secondary";
            }
        }
    }
}