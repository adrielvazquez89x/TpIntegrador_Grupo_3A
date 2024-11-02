using DataAccessService;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessService.DataAccessService;

namespace Business.ProductAttributes
{
    public class BusinessSubCategory
    {
        List<SubCategory> listSubCat = new List<SubCategory>();
        DataAccess data = new DataAccess();
        public List<SubCategory> list(int IdCategory)
        {
            try
            {
                data.setQuery("SELECT * FROM SubCategorias WHERE IdCategoria= "+ IdCategory);
                data.executeRead();

                while (data.Reader.Read())
                {
                    SubCategory aux = new SubCategory
                    {
                        Id = (int)data.Reader["Id"],
                        Description = (string)data.Reader["Descripcion"],
                        Active = (bool)data.Reader["Activo"]
                    };

                    listSubCat.Add(aux);
                }

                return listSubCat;
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
        
        public string Update(int IdCategory, SubCategory subCategory)
        {
            try
            {
                data.setQuery($"UPDATE SubCategorias SET Descripcion = '{subCategory.Description}' WHERE IdCategoria = {IdCategory}");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                return "Error al actualizar la descripcion. Info de error: " + ex.Message;
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
