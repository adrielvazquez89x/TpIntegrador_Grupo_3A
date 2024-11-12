﻿using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cart
    {
        public List<ItemCart> Items { get; set; }
        public decimal Total => Items.Sum(item => item.Subtotal);
        public void AddProduct(Product prod, Stock stock, int number)
        {
            if (Items == null)
            {
                Items = new List<ItemCart>();
            }

            //var itemOnCart = Items.FirstOrDefault(i => i.Product.Code == stock.ProdCode && i.stock.Id==stock.Id);
            var itemOnCart = Items.FirstOrDefault(i => i.Stock.Id == stock.Id);
            if (itemOnCart != null)
            {
               itemOnCart.Number +=number;
            }
            else
            {
                ItemCart newItem = new ItemCart
                {
                    Number = number,
                    Product = prod,
                    Stock = stock,
                };
                Items.Add(newItem);
            }
        }

        public void DeleteProduct(string code)
        {
            var item = Items.FirstOrDefault(i => i.Product.Code== code);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void ClearCart()
        {
            Items.Clear();
        }
    }
}
