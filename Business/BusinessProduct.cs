using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessProduct
    {
        List<Product> productList = new List<Product>();
        List<ImageProduct> imageList = new List<ImageProduct>();
        DataAccess data = new DataAccess();
        public List<Product> list(int id=0) //lista todos los productos o uno en particular
        {
            try
            {
                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Stock, P.Descripcion, P.IdCategoria, P.IdColor, P.IdTalle, " +
                              "Ca.Descripcion AS Categoria, Co.Descripcion AS Color, T.Descripcion AS Talle " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria JOIN COLORES Co ON Co.Id = P.IdColor " +
                              "JOIN TALLES T ON T.Id = P.IdTalle";
                data.setQuery(query);

                if (id != 0)
                {
                    data.setQuery(query += " WHERE IdProducto = @IdProducto");
                    data.setParameter("@Idproducto", id);
                }
                data.executeRead();

                while (data.Reader.Read())
                {
                    Product aux = new Product();
                    aux.Id = (int)data.Reader["IdProducto"];
                    aux.Code = (string)data.Reader["Codigo"];
                    aux.Name = (string)data.Reader["Nombre"];
                    aux.Price = Math.Round((decimal)data.Reader["Precio"], 2);
                    aux.Stock = (int)data.Reader["Stock"];
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Category = new Category();
                    aux.Category.Id = (int)data.Reader["IdCategoria"];
                    aux.Category.Description = (string)data.Reader["Categoria"];                   
                    aux.Colour = new Colour();
                    aux.Colour.Id = (int)data.Reader["IdColor"];
                    aux.Colour.Description = (string)data.Reader["Color"];
                    aux.Size = new Size();
                    aux.Size.Id = (int)data.Reader["IdTalle"];
                    aux.Size.Description = (string)data.Reader["Talle"];

                    //DESDE ACÁ HABRIA QUE LLAMAR AL METODO LISTAR DE LA CLASE IMG PASANDO EL ID.PRODUCTO, y lo mismo con temporadas y secciones
                    BusinessImageProduct businessImage = new BusinessImageProduct();
                    imageList = businessImage.list(aux.Id);
                    aux.Images = imageList;
                    //esto mismo hay que hacerlo con Temporadas y Secciones

                    productList.Add(aux);
                }
                return productList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                data.closeConnection();
            }
        }
    }
}
