using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ProductAttributes
{
    public class SubCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } // Esto no está
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
