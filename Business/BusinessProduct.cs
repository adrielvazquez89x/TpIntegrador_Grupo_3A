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
                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Stock, P.Descripcion, P.FechaCreacion, P.IdCategoria, P.IdColor, " +
                              "P.IdTalle, P.IdSeccion, P.IdTemporada, Ca.Descripcion AS Categoria, Co.Descripcion AS Color, Ta.Descripcion AS Talle, " +
                              "S.Descripcion AS Seccion, Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria JOIN COLORES Co ON Co.Id = P.IdColor " +
                              "JOIN TALLES Ta ON Ta.Id = P.IdTalle JOIN Temporadas Te ON Te.Id=P.IdTemporada "+
                              "JOIN Secciones S ON S.Id=P.IdSeccion WHERE P.Activo=1 ";
                data.setQuery(query);

                if (id != 0)
                {
                    data.setQuery(query += " AND IdProducto = @IdProducto");
                    data.setParameter("@IdProducto", id);
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


                    BusinessImageProduct businessImage = new BusinessImageProduct();
                    imageList = businessImage.list(aux.Code);
                    aux.Images = imageList;


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
