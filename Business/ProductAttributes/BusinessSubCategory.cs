using DataAccessService;
using Model;
using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                data.setQuery("SELECT * FROM SubCategorias WHERE IdCategoria=@IdCategoria");
                data.setParameter("@IdCategoria", IdCategory);
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
    }
}
