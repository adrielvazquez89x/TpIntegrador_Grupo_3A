﻿using DataAccessService;
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

        public List<Category> ListCategories()
        {
            List<Category> list = new List<Category>();
            DataAccess data = new DataAccess();

            try
            {
                data.setQuery("SELECT * FROM Categorias");
                data.executeRead();

                while (data.Reader.Read()) 
                {
                    Category aux = new Category
                    {
                        Id = (int)data.Reader["Id"],
                        Name = (string)data.Reader["Nombre"],
                        Active = (bool)data.Reader["Activo"]
                    };

                    list.Add(aux);
                }

                return list;
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
            DataAccess data = new DataAccess();

            try
            {
                data.setQuery("INSERT INTO Categorias (Nombre) Values (@Category)");
                data.setParameter("@Category", category.Name);
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
    }
}