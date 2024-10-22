using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Image> Images { get; set; }
        public Category Category { get; set; }
        public Season Season { get; set; }
        public Section Section { get; set; }

    }
}
