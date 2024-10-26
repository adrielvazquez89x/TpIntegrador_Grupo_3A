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
        public string Code { get; set; } //B001AXL
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }  //buzos, pantalones, remeras, camisas...
        public Colour Colour { get; set; }  //el color y talle es unico porque el id ya hace referencia a una prenda en particular con cierto talle y color
        public Size Size { get; set; }
        public Season Season { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ImageProduct> Images { get; set; }
        public List<Section> Sections { get; set; } //Ultimos lanzamientos, urbano, fiesta, formal, tendencia, noche, clasicos, estampados...
    }
}
