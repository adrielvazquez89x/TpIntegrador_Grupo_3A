using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemCart
    {
        public Product Product { get; set; }
        public int Number { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Product.Price * Number;
            }
        }
    }
}
