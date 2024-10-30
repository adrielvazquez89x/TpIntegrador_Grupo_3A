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
        DataAccess data = new DataAccess();
        public List<Section> list(bool showAll = true, int id = 0)
        {
            try
            {
                string query = "SELECT * FROM Secciones ";
                data.setQuery(query);

                if (id != 0)
                {
                    data.setQuery(query += " AND Id = @id");
                    data.setParameter("@id", id);
                }
                if (!showAll)
                {
                    data.setQuery(query += " WHERE Activo = 1");
                }
                data.executeRead();

                while (data.Reader.Read())
                {
                    Section aux = new Section
                    {
                        Id = (int)data.Reader["Id"],
                        Description = (string)data.Reader["Descripcion"],
                        Active = (bool)data.Reader["Activo"]
                    };

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

        public string Add(Section section)
        {
            try
            {
                data.setQuery("INSERT INTO Secciones (Descripcion) Values (@seccion)");
                data.setParameter("@seccion", section.Description);
                data.executeAction();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                data.closeConnection();
            }
        }

        public string Update(Section section)
        {
            try
            {
                data.setQuery("UPDATE Secciones SET Descripcion = @seccion WHERE Id = @id");
                data.setParameter("@seccion", section.Description);
                data.setParameter("@id", section.Id);
                data.executeAction();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
                data.setQuery("Update Secciones SET Activo = 0 where Id = @Id");
                data.setParameter("@id", id);
                data.executeAction();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
                data.setQuery("Update Secciones SET Activo = 1 where Id = @Id");
                data.setParameter("@Id", id);
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
