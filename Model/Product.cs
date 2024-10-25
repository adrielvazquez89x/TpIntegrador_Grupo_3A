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
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Image> Images { get; set; }
        public Category Category { get; set; }  //buzos, pantalones, remeras, camisas...
        public List<Season> Seasons { get; set; }
        public List<Section> Sections { get; set; } //urbano, fiesta, formal, tendencia, noche, clasicos, estampados...
        public int Stock {  get; set; }
        public string Description { get; set; }
        public Colour Colour { get; set; }  //el color y talle es unico porque el id ya hace referencia a una prenda en particular con cierto talle y color
        public Size Size { get; set; }

    }
}
