using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Business.ProductAttributes
{
    public class BusinessSection
    {
        List<Model.Section> listSection = new List<Model.Section>();
        List<Product> prodList = new List<Product>();
        DataAccess data = new DataAccess();
        public List<Model.Section> list(int id=0)
        {
            try
            {
                string query = "SELECT * FROM Secciones WHERE Activo=1";

                if (id != 0)
                {
                    query += " AND Id = "+ id;
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    Model.Section aux = new Model.Section();

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
        public string Add(Model.Section section)
        {
            try
            {
                data.setQuery($"INSERT INTO Secciones (Descripcion, Descripcion2) Values ('{section.Description}', '{section.Description2}')");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "La sección ya existe.";
                }
                else
                {
                    return "Error al agregar la sección: " + ex.Message;
                }

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

        public string Update(Model.Section section)
        {
            try
            {
                data.setQuery($"UPDATE Secciones SET Descripcion = '{section.Description}', Descripcion2 = '{section.Description2}' WHERE Id = {section.Id}");
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return "La sección ya existe.";
                }
                else
                {
                    return "Error al agregar la sección: " + ex.Message;
                }

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

        public string Delete(int id)
        {
            try
            {
                data.setQuery("Update Secciones SET Activo = 0 where Id = " + id);
                data.executeAction();
                return "ok";
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

        public string Activate(int id)
        {
            try
            {
                data.setQuery("Update Secciones SET Activo = 1 where Id = " + id);
                data.executeAction();
                return "ok";
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
