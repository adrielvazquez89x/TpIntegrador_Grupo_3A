using Business.ProductAttributes;
using DataAccessService;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Business
{
    public class BusinessProduct
    {
        List<Product> productList = new List<Product>();
        List<Model.Section> sectionList = new List<Model.Section>();
        List<ImageProduct> imageList = new List<ImageProduct>();
        DataAccess data = new DataAccess();
        public List<Product> list(int id=0) //lista todos los productos o uno en particular
        {
            try
            {
                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Descripcion, P.FechaCreacion, P.IdCategoria, " +
                              "P.IdSubCategoria, P.IdTemporada, Ca.Descripcion AS Categoria, SuC.Descripcion AS SubCategoria, " +
                              "Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria " +
                              "JOIN SubCategorias SuC ON P.IdSubCategoria=SuC.Id " +
                              "JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              "WHERE P.Activo=1 ";  //quité S.Descripcion AS Seccion,  y JOIN Secciones S ON S.Id=P.IdSeccion
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
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Category = new Category();
                    aux.Category.Id = (int)data.Reader["IdCategoria"];
                    aux.Category.Description = (string)data.Reader["Categoria"];                   
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
                              "WHERE P.Activo=1 AND P.IdCategoria=@IdCategoria";
                data.setQuery(query);
                
                if (idSubCat != 0)
                {
                    data.setQuery(query += " AND P.IdSubCategoria=@IdSubCat");
                    data.setParameter("@IdSubCat", idSubCat);
                }
                data.setParameter("@IdCategoria", idCategory);
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
                string query = "SELECT P.Id AS IdProducto, P.Codigo, P.Nombre, P.Precio, P.Descripcion, P.FechaCreacion, P.IdCategoria, " +
                              "P.IdSubCategoria, P.IdTemporada, Ca.Descripcion AS Categoria, SuC.Descripcion AS SubCategoria, " +
                              "Te.Descripcion AS Temporada " +
                              "FROM PRODUCTOS P JOIN CATEGORIAS Ca ON Ca.Id = P.IdCategoria " +
                              "JOIN SubCategorias SuC ON P.IdSubCategoria=SuC.Id " +
                              "JOIN Temporadas Te ON Te.Id=P.IdTemporada " +
                              "JOIN ProductosXSecciones  PXS on P.Codigo=PXS.CodigoProducto " +
                              "WHERE P.Activo=1 AND PXS.IdSeccion=@IdSeccion";
                data.setQuery(query);

                data.setParameter("@IdSeccion", idSection);
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
