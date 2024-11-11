using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public Season Season { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ImageProduct> Images { get; set; }
    }
}
