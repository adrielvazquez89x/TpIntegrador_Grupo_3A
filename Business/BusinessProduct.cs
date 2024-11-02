using DataAccessService;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using DataAccessService.DataAccessService;

namespace Business
{
    public class BusinessProduct
    {
        List<Product> productList = new List<Product>();
        List<ImageProduct> imageList = new List<ImageProduct>();
        DataAccess data = new DataAccess();
        // Método para listar productos como administrador
        public List<Product> listAdmin(int id = 0)
        {
            List<Product> productList = new List<Product>();

            try
            {
                string queryAdmin = @"
                Select P.Id as IdProducto, P.Codigo, P.Nombre,P.Precio,
	                   P.Descripcion,P.FechaCreacion,P.IdCategoria as IdCategoria,
	                   P.IdSubCategoria as IdSubCategoria, P.IdTemporada as IdTemporada, P.Activo,
	                   C.Descripcion as Categoria, SC.Descripcion as SubCategoria, S.Descripcion as Temporada
	                   from Productos P
	                   Inner Join Categorias C on P.IdCategoria = C.Id
	                   Inner Join SubCategorias SC on P.IdSubCategoria = SC.Id
	                   Inner Join Temporadas S on P.IdTemporada = S.Id
            ";

                if (id != 0)
                {
                    queryAdmin += " WHERE p.Id = @IdProducto";
                    data.setParameter("@IdProducto", id);
                }

                data.setQuery(queryAdmin);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Product aux = new Product();
                    aux.Id = (int)data.Reader["IdProducto"];
                    aux.Code = data.Reader["Codigo"] != DBNull.Value ? (string)data.Reader["Codigo"] : string.Empty;
                    aux.Name = data.Reader["Nombre"] != DBNull.Value ? (string)data.Reader["Nombre"] : string.Empty;
                    aux.Price = data.Reader["Precio"] != DBNull.Value ? Math.Round((decimal)data.Reader["Precio"], 2) : 0;
                    aux.CreationDate = data.Reader["FechaCreacion"] != DBNull.Value ? (DateTime)data.Reader["FechaCreacion"] : DateTime.MinValue;

                    aux.Category = new Category
                    {
                        Id = data.Reader["IdCategoria"] != DBNull.Value ? (int)data.Reader["IdCategoria"] : 0,
                        Description = data.Reader["Categoria"] != DBNull.Value ? (string)data.Reader["Categoria"] : string.Empty
                    };

                    aux.SubCategory = new SubCategory
                    {
                        Id = data.Reader["IdSubCategoria"] != DBNull.Value ? (int)data.Reader["IdSubCategoria"] : 0,
                        Description = data.Reader["SubCategoria"] != DBNull.Value ? (string)data.Reader["SubCategoria"] : string.Empty
                    };

                    aux.Season = new Season
                    {
                        Id = data.Reader["IdTemporada"] != DBNull.Value ? (int)data.Reader["IdTemporada"] : 0,
                        Description = data.Reader["Temporada"] != DBNull.Value ? (string)data.Reader["Temporada"] : string.Empty
                    };

                    // Obtener imágenes del producto
                    BusinessImageProduct businessImage = new BusinessImageProduct();
                    List<ImageProduct> imageList = businessImage.list(aux.Code);
                    aux.Images = imageList;

                    productList.Add(aux);
                }

                return productList;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (puedes registrar el error o lanzarlo nuevamente)
                throw ex;
            }
            finally
            {
                data.closeConnection();
            }
        }

        // Método para listar productos de manera estándar
        public List<Product> list(int id = 0)
        {
            List<Product> productList = new List<Product>();

            try
            {
                string query = @"
                SELECT 
                    P.Id AS IdProducto, 
                    P.Codigo, 
                    P.Nombre, 
                    P.Precio, 
                    P.Descripcion, 
                    P.FechaCreacion, 
                    P.IdCategoria, 
                    P.IdSubCategoria, 
                    P.IdTemporada, 
                    Ca.Descripcion AS Categoria, 
                    SuC.Descripcion AS SubCategoria, 
                    Te.Descripcion AS Temporada 
                FROM Productos P
                INNER JOIN Categorias Ca ON Ca.Id = P.IdCategoria
                INNER JOIN SubCategorias SuC ON P.IdSubCategoria = SuC.Id
                INNER JOIN Temporadas Te ON Te.Id = P.IdTemporada
                WHERE P.Activo = 1
            ";

                if (id != 0)
                {
                    query += " AND P.Id = @IdProducto";
                    data.setParameter("@IdProducto", id);
                }

                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Product aux = new Product();
                    aux.Id = (int)data.Reader["IdProducto"];
                    aux.Code = data.Reader["Codigo"] != DBNull.Value ? (string)data.Reader["Codigo"] : string.Empty;
                    aux.Name = data.Reader["Nombre"] != DBNull.Value ? (string)data.Reader["Nombre"] : string.Empty;
                    aux.Price = data.Reader["Precio"] != DBNull.Value ? Math.Round((decimal)data.Reader["Precio"], 2) : 0;
                    aux.Description = data.Reader["Descripcion"] != DBNull.Value ? (string)data.Reader["Descripcion"] : string.Empty;
                    aux.CreationDate = data.Reader["FechaCreacion"] != DBNull.Value ? (DateTime)data.Reader["FechaCreacion"] : DateTime.MinValue;

                    aux.Category = new Category
                    {
                        Id = data.Reader["IdCategoria"] != DBNull.Value ? (int)data.Reader["IdCategoria"] : 0,
                        Description = data.Reader["Categoria"] != DBNull.Value ? (string)data.Reader["Categoria"] : string.Empty
                    };

                    aux.SubCategory = new SubCategory
                    {
                        Id = data.Reader["IdSubCategoria"] != DBNull.Value ? (int)data.Reader["IdSubCategoria"] : 0,
                        Description = data.Reader["SubCategoria"] != DBNull.Value ? (string)data.Reader["SubCategoria"] : string.Empty
                    };

                    aux.Season = new Season
                    {
                        Id = data.Reader["IdTemporada"] != DBNull.Value ? (int)data.Reader["IdTemporada"] : 0,
                        Description = data.Reader["Temporada"] != DBNull.Value ? (string)data.Reader["Temporada"] : string.Empty
                    };

                    // Obtener imágenes del producto
                    BusinessImageProduct businessImage = new BusinessImageProduct();
                    List<ImageProduct> imageList = businessImage.list(aux.Code);
                    aux.Images = imageList;

                    productList.Add(aux);
                }

                return productList;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
            finally
            {
                data.closeConnection();
            }
        }

        public List<Product> listByCategory(int idCategory, int idSubCat = 0) //lista todos los productos de cierta categoria o filtra tambien por SubCat
        {
            try
            {
                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Descripcion, P.FechaCreacion, P.IdCategoria, " +
                              "P.IdSubCategoria, P.IdTemporada, Ca.Descripcion AS Categoria, SuC.Descripcion AS SubCategoria, " +
                              "Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria " +
                              "JOIN SubCategorias SuC ON P.IdSubCategoria=SuC.Id " +
                              "JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              $"WHERE P.Activo=1 AND P.IdCategoria={idCategory}";

                if (idSubCat != 0)
                {
                    query += " AND P.IdSubCategoria= " + idSubCat;
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
