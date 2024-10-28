using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ProductAttributes
{
    public class BusinessSection
    {
        List<Section> listSection = new List<Section>();
        List<Product> prodList = new List<Product>();
        DataAccess data = new DataAccess();
        public List<Section> list(int id=0)
        {
            try
            {
                string query = "SELECT * FROM Secciones WHERE Activo=1";
                data.setQuery(query);

                if (id != 0)
                {
                    data.setQuery(query += " AND Id = @id");
                    data.setParameter("@id", id);
                }
                data.executeRead();

                while (data.Reader.Read())
                {
                    Section aux = new Section();

                    aux.Id = (int)data.Reader["Id"];
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Description2 = data.Reader["Descripcion2"] is DBNull ? "" : (string)data.Reader["Descripcion2"];
                    aux.Active = (bool)data.Reader["Activo"];

                    BusinessProduct businessProduct = new BusinessProduct();
                    prodList = businessProduct.listBySection(aux.Id);
                    aux.Products = prodList;

                    listSection.Add(aux);
                }

                return listSection;
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
