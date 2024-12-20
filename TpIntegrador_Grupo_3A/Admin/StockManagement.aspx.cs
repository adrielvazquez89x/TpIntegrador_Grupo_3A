﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.ProductAttributes;
using Model;
using Model.ProductAttributes;

namespace TpIntegrador_Grupo_3A.Admin
{
    public partial class StockManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string productCode = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(productCode))
                {
                    LoadProductInfo(productCode);
                    LoadStock(productCode);
                    LoadDropdowns();
                }
                else
                {
                    Response.Redirect("ProductsManagement.aspx");
                }
            }
        }

        private void LoadProductInfo(string productCode)
        {
            BusinessProduct businessProduct = new BusinessProduct();
            Product product = businessProduct.list(productCode).FirstOrDefault();

            if (product != null)
            {
                txtCodigo.Text = product.Code;
                txtNombre.Text = product.Name;
            }
            else
            {
                Response.Redirect("ProductsManagement.aspx");
            }
        }

        private void LoadStock(string productCode)
        {
            BusinessStock businessStock = new BusinessStock();
            List<Stock> stockActual = businessStock.list(productCode);

            rptStock.DataSource = stockActual;
            rptStock.DataBind();
        }

        private void LoadDropdowns()
        {
            // Cargar colores
            BusinessColour businessColour = new BusinessColour();
            List<Colour> colores = businessColour.list();

            ddlColor.DataSource = colores;
            ddlColor.DataValueField = "Id";
            ddlColor.DataTextField = "Description";
            ddlColor.DataBind();
            ddlColor.Items.Insert(0, new ListItem("Seleccione un color", "0"));

            // Cargar talles
            BusinessSize businessSize = new BusinessSize();
            List<Model.Size> talles = businessSize.list();

            ddlTalle.DataSource = talles;
            ddlTalle.DataValueField = "Id";
            ddlTalle.DataTextField = "Description";
            ddlTalle.DataBind();
            ddlTalle.Items.Insert(0, new ListItem("Seleccione un talle", "0"));
        }

        protected void rptStock_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStock")
            {

                Label lblUpdateMessage = (Label)e.Item.FindControl("lblUpdateMessage");
                TextBox txtStockAmount = (TextBox)e.Item.FindControl("txtEditStock");
                HiddenField hfOriginalAmount = (HiddenField)e.Item.FindControl("hfOriginalAmount");

                string currentValue = txtStockAmount.Text;


                if(!Validator.IsOnlyNumbers(txtStockAmount.Text))
                {
                    UserControl_Toast.ShowToast("La cantidad de stock debe ser un número entero, mayor o igual a cero.", false);
                    txtStockAmount.Text = hfOriginalAmount.Value;
                    return;
                }

                if(currentValue == hfOriginalAmount.Value)
                {
                    return;
                }

                int stockId = int.Parse(e.CommandArgument.ToString());
                int stockAmount = int.Parse(txtStockAmount.Text);

                BusinessStock businessStock = new BusinessStock();
                string resultado = businessStock.Update(stockId, stockAmount);

                if (resultado == "ok")
                {
                    // Recargar el stock
                    LoadStock(txtCodigo.Text);
                    UserControl_Toast.ShowToast("Stock actualizado correctamente.", true);
                }
                else
                {
                    lblUpdateMessage.Text = resultado;
                }
            }
        }

        protected void btnAgregarStock_Click(object sender, EventArgs e)
        {
            if (ddlColor.SelectedValue != "0"
                && ddlTalle.SelectedValue != "0" 
                && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                string productCode = txtCodigo.Text;
                int colorId = int.Parse(ddlColor.SelectedValue);
                int talleId = int.Parse(ddlTalle.SelectedValue);

                Stock newStock = new Stock();

                newStock.ProdCode = productCode;
                newStock.Colour = new Colour() { Id = colorId };
                newStock.Size = new Model.Size() { Id = talleId };
                newStock.Amount = cantidad;

                BusinessStock businessStock = new BusinessStock();
                string resultado = businessStock.Add(newStock);

                if (resultado == "ok")
                {
                    ddlColor.SelectedIndex = 0;
                    ddlTalle.SelectedIndex = 0;
                    txtCantidad.Text = "";

                    LoadStock(productCode);
                }
                else
                {
                    UserControl_Toast.ShowToast(resultado, false);
                }
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductsManagement.aspx");
        }
    }
}
