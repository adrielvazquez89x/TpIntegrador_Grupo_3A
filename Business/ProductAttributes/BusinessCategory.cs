using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessCategory
    {
        List<Category> listCategory = new List<Category>();
        DataAccess data = new DataAccess();
        public List<Category> list(int id=0)
        {
            try
            {
                string query = "SELECT * FROM Categorias";
                data.setQuery(query);

                if (id != 0)
                {
                    data.setQuery(query += " AND Id = @id");
                    data.setParameter("@id", id);
                }
                data.executeRead();

                while (data.Reader.Read()) 
                {
                    Category aux = new Category
                    {
                        Id = (int)data.Reader["Id"],
                        Description = (string)data.Reader["Descripcion"],
                        //Icon = (string)data.Reader["Icono"],
                        Active = (bool)data.Reader["Activo"]
                    };

                    listCategory.Add(aux);
                }

                return listCategory;
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

        public string Add(Category category)
        {
            try
            {
                data.setQuery("INSERT INTO Categorias (Descripcion) Values (@Categoria)");
                data.setParameter("@Categoria", category.Description);
                data.executeAction();
                return "ok";
            }
            catch(SqlException ex)
            {
                if (ex.Number == 2627) // Vien de la base de datos
                {
                    return "La categoría ya existe.";
                }
                else
                {
                    return "Error al agregar la categoría: " + ex.Message;
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

        public string Update(Category category)
        {
            try
            {
                data.setQuery("UPDATE Categorias SET Descripcion = @Categoria WHERE Id = @Id");
                data.setParameter("@Categoria", category.Description);
                data.setParameter("@Id", category.Id);
                data.executeAction();
                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) 
                {
                    return "La categoría ya existe.";
                }
                else
                {
                    return "Error al agregar la categoría: " + ex.Message;
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
                data.setQuery("Update Categorias SET Activo = 0 where Id = @Id");
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

        public string Activate(int id)
        {
            try
            {
                data.setQuery("Update Categorias SET Activo = 1 where Id = @Id");
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
