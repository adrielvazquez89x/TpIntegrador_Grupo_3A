using DataAccessService;
using Model;
using System;
using System.Collections.Generic;

namespace Business
{
    public class BusinessProduct
    {
        List<Product> productList = new List<Product>();
        List<Model.Section> sectionList = new List<Model.Section>();
        List<ImageProduct> imageList = new List<ImageProduct>();
        DataAccess data = new DataAccess();
        public List<Product> list(int id = 0) //lista todos los productos o uno en particular
        {
            try
            {
                string query = @"
                                SELECT 
                                        p.Id AS IdProducto, p.Codigo, 
                                        p.Nombre, p.Precio, 
                                        p.Stock, p.Descripcion, 
                                        p.FechaCreacion, p.Activo, 
                                        c.Id AS IdCategoria, c.Descripcion AS Categoria, 
                                        c.Activo AS CategoriaActivo, col.Id AS IdColor, 
                                        col.Descripcion AS Color, col.Activo AS ColorActivo, t.Id AS IdTalle,
                                        t.Descripcion AS Talle, t.Activo AS TalleActivo, s.Id AS IdTemporada,
                                        s.Descripcion AS Temporada, s.Activo AS TemporadaActivo FROM Productos p
                                    INNER JOIN Categorias c ON p.IdCategoria = c.Id
                                    INNER JOIN Colores col ON p.IdColor = col.Id
                                    INNER JOIN Talles t ON p.IdTalle = t.Id
                                    INNER JOIN Temporadas s ON p.IdTemporada = s.Id
                                ";

                if (id != 0)
                {
                    query += " WHERE p.Id = @IdProducto";
                }

                data.setQuery(query);

                if (id != 0)
                {
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
                    aux.Season = new Season();
                    aux.Season.Id = (int)data.Reader["IdTemporada"];
                    aux.Season.Description = (string)data.Reader["Temporada"];
                    aux.CreationDate = (DateTime)data.Reader["FechaCreacion"];

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

        public List<Product> listByCategory(int idCategory) //lista todos los productos o uno en particular
        {
            try
            {
                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Stock, P.Descripcion, P.FechaCreacion, P.IdCategoria, P.IdColor, " +
                              "P.IdTalle, P.IdSeccion, P.IdTemporada, Ca.Descripcion AS Categoria, Co.Descripcion AS Color, Ta.Descripcion AS Talle, " +
                              "S.Descripcion AS Seccion, Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria JOIN COLORES Co ON Co.Id = P.IdColor " +
                              "JOIN TALLES Ta ON Ta.Id = P.IdTalle JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              "JOIN Secciones S ON S.Id=P.IdSeccion WHERE P.Activo=1";
                data.setQuery(query);
                if (idCategory != 0)
                {
                    data.setQuery(query += "  AND P.IdCategoria=@IdCategoria");
                    data.setParameter("@IdCategoria", idCategory);
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
