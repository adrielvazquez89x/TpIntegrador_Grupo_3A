﻿using Business.ProductAttributes;
using DataAccessService;
using Model;
using Model.ProductAttributes;
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
        List<SubCategory> subCatList = new List<SubCategory>();
        DataAccess data = new DataAccess();
        public List<Category> list(int id=0)
        {
            try
            {
                string query = "SELECT * FROM Categorias WHERE Activo=1";
                

                if (id != 0)
                {
                    query += " AND Id = "+ id;
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read()) 
                {
                    Category aux = new Category();

                    aux.Id = (int)data.Reader["Id"];
                    aux.Description = (string)data.Reader["Descripcion"];
                    aux.Icon = (string)data.Reader["Icono"];
                    aux.Active = (bool)data.Reader["Activo"];

                    BusinessSubCategory businessSubCat = new BusinessSubCategory();
                    subCatList = businessSubCat.list(aux.Id);
                    aux.SubCategory = subCatList;


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
                data.setQuery($"INSERT INTO Categorias (Descripcion) Values ('{category.Description}')");
                data.executeAction();
                return "ok";
            }
            catch(SqlException ex)
            {
                if (ex.Number == 2627) // Viene de la base de datos
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

        public void Update(Category category)
        {
            try
            {
                data.setQuery($"UPDATE Categorias SET Descripcion = '{category.Description}' WHERE Id = {category.Id}");
                data.executeAction();
            }
            catch (SqlException ex)
            {

                throw ex;

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
                data.setQuery($"Update Categorias SET Activo = 0 where Id = {id}");
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
                data.setQuery($"Update Categorias SET Activo = 1 where Id = {id}");
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
