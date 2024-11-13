using Model.ProductAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cart
    {
        public List<ItemCart> Items = new List<ItemCart>();
        public decimal Total => Items.Sum(item => item.Subtotal);
        public bool AddProduct(Product prod, Stock stock, int number)
        {
            var itemOnCart = Items.FirstOrDefault(i => i.Stock.Id == stock.Id);
            if (itemOnCart != null)
            {
                if(itemOnCart.Number+number>stock.Amount)  //si el prod ya estaba en la lista hay que chequear si al sumar los pedidos alcanza el stock
                    return false;
                itemOnCart.Number +=number;
                itemOnCart.Product= prod;
                itemOnCart.Stock = stock;
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
            return true;
        }

        public void DeleteProduct(int stockId)
        {
            var item = Items.FirstOrDefault((i => i.Stock.Id == stockId));
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public bool UpdateProduct(Stock stock, int a)
        {
            ItemCart itemOnCart = Items.FirstOrDefault(i => i.Stock.Id == stock.Id);
            if (a == 1) //si recibe un 1 diminuir, sino, aumentar
            {
                itemOnCart.Number--;
            }
            else 
            {
                if (itemOnCart.Number + 1 > stock.Amount)  //hay que chequear si al sumar un item alcanza el stock
                    return false;
                itemOnCart.Number++;
            }
            

            //Items.Update(itemOnCart);
            return true;
        }
        public void ClearCart()
        {
            Items.Clear();
        }
    }
}
