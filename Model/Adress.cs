using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Adress
    {
        public int Id { get; set; }
        public string Province { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string CP { get; set; }
        public int Floor { get; set; }
        public string Unit { get; set; }
    }
}
