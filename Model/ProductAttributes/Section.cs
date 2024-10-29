using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public List<Product> Products { get; set; }
        public bool Active { get; set; }
    }
}
