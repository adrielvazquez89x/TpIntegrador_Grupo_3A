using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FavouriteProducts
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string ProductCode { get; set; }
    }
}
