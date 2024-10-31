using DataAccessService;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;

namespace Business
{
    public class BusinessProduct
    {
        List<Product> productList = new List<Product>();
        List<ImageProduct> imageList = new List<ImageProduct>();
        DataAccess data = new DataAccess();
        public List<Product> list(int id = 0) //lista todos los productos o uno en particular
        {
            try
            {
                //string query = @"
                //                SELECT 
                //                        p.Id AS IdProducto, p.Codigo, 
                //                        p.Nombre, p.Precio, 
                //                        p.Stock, p.Descripcion, 
                //                        p.FechaCreacion, p.Activo, 
                //                        c.Id AS IdCategoria, c.Descripcion AS Categoria, 
                //                        c.Activo AS CategoriaActivo, col.Id AS IdColor, 
                //                        col.Descripcion AS Color, col.Activo AS ColorActivo, t.Id AS IdTalle,
                //                        t.Descripcion AS Talle, t.Activo AS TalleActivo, s.Id AS IdTemporada,
                //                        s.Descripcion AS Temporada, s.Activo AS TemporadaActivo FROM Productos p
                //                    INNER JOIN Categorias c ON p.IdCategoria = c.Id
                //                    INNER JOIN Colores col ON p.IdColor = col.Id
                //                    INNER JOIN Talles t ON p.IdTalle = t.Id
                //                    INNER JOIN Temporadas s ON p.IdTemporada = s.Id
                //                ";

                //if (id != 0)
                //{
                //    query += " WHERE p.Id = @IdProducto";
                //}
                //data.setQuery(query);

                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Descripcion, P.FechaCreacion, P.IdCategoria, " +
                              "P.IdSubCategoria, P.IdTemporada, Ca.Descripcion AS Categoria, SuC.Descripcion AS SubCategoria, " +
                              "Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria " +
                              "JOIN SubCategorias SuC ON P.IdSubCategoria=SuC.Id " +
                              "JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              "WHERE P.Activo=1 ";

                if (id != 0)
                {
                    data.setParameter("@IdProducto", id);
                    query += " AND P.Id = "+ id;
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Product aux = new Product();
                    aux.Id = (int)data.Reader["IdProducto"];
                    aux.Code = (string)data.Reader["Codigo"];
                    aux.Name = (string)data.Reader["Nombre"];
                    aux.Price = Math.Round((decimal)data.Reader["Precio"], 2);
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Category = new Category();
                    aux.Category.Id = (int)data.Reader["IdCategoria"];
                    aux.Category.Description = (string)data.Reader["Categoria"];
                    aux.SubCategory = new SubCategory();
                    aux.SubCategory.Id = (int)data.Reader["IdSubCategoria"];
                    aux.SubCategory.Description = (string)data.Reader["SubCategoria"];
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

        public List<Product> listByCategory(int idCategory, int idSubCat=0) //lista todos los productos de cierta categoria o filtra tambien por SubCat
        {
            try
            {
                string query= "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Descripcion, P.FechaCreacion, P.IdCategoria, " +
                              "P.IdSubCategoria, P.IdTemporada, Ca.Descripcion AS Categoria, SuC.Descripcion AS SubCategoria, " +
                              "Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria " +
                              "JOIN SubCategorias SuC ON P.IdSubCategoria=SuC.Id "+
                              "JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              $"WHERE P.Activo=1 AND P.IdCategoria={idCategory}";
                
                if (idSubCat != 0)
                {
                    query += " AND P.IdSubCategoria= "+ idSubCat;
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Product aux = new Product();
                    aux.Id = (int)data.Reader["IdProducto"];
                    aux.Code = (string)data.Reader["Codigo"];
                    aux.Name = (string)data.Reader["Nombre"];
                    aux.Price = Math.Round((decimal)data.Reader["Precio"], 2);
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Category = new Category();
                    aux.Category.Id = (int)data.Reader["IdCategoria"];
                    aux.Category.Description = (string)data.Reader["Categoria"];
                    aux.SubCategory = new SubCategory();
                    aux.SubCategory.Id = (int)data.Reader["IdSubCategoria"];
                    aux.SubCategory.Description = (string)data.Reader["SubCategoria"];
                    aux.Season = new Season();
                    aux.Season.Id = (int)data.Reader["IdTemporada"];
                    aux.Season.Description = (string)data.Reader["Temporada"];

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

        public List<Product> listBySection(int idSection) //lista todos los productos de cierta categoria o filtra tambien por SubCat
        {
            try
            {
                data.setQuery("SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Descripcion, P.FechaCreacion, P.IdCategoria, " +
                              "P.IdSubCategoria, P.IdTemporada, Ca.Descripcion AS Categoria, SuC.Descripcion AS SubCategoria, " +
                              "Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria " +
                              "JOIN SubCategorias SuC ON P.IdSubCategoria=SuC.Id " +
                              "JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              "JOIN ProductosXSecciones  PXS on P.Codigo=PXS.CodigoProducto " +
                              $"WHERE P.Activo=1 AND PXS.IdSeccion={idSection}");

                data.executeRead();

                while (data.Reader.Read())
                {
                    Product aux = new Product();
                    aux.Id = (int)data.Reader["IdProducto"];
                    aux.Code = (string)data.Reader["Codigo"];
                    aux.Name = (string)data.Reader["Nombre"];
                    aux.Price = Math.Round((decimal)data.Reader["Precio"], 2);
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Category = new Category();
                    aux.Category.Id = (int)data.Reader["IdCategoria"];
                    aux.Category.Description = (string)data.Reader["Categoria"];
                    aux.SubCategory = new SubCategory();
                    aux.SubCategory.Id = (int)data.Reader["IdSubCategoria"];
                    aux.SubCategory.Description = (string)data.Reader["SubCategoria"];
                    aux.Season = new Season();
                    aux.Season.Id = (int)data.Reader["IdTemporada"];
                    aux.Season.Description = (string)data.Reader["Temporada"];

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
