using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ProductAttributes
{
    public class Stock
    {
        public int Id { get; set; }
        public string ProdCode { get; set; }
        public int IdColour { get; set; }
        public int IdSize { get; set; }
        public int Amount { get; set; }
    }
}
