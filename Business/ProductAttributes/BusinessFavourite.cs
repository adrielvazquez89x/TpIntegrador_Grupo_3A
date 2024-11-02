using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessService.DataAccessService;

namespace Business.ProductAttributes
{
    public class BusinessFavourite
    {
        List<FavouriteProducts> favList = new List<FavouriteProducts>();
        DataAccess data = new DataAccess();
        public List<FavouriteProducts> list(int id = 0)
        {
            try
            {
                string query = "SELECT * FROM Favoritos ";

                if (id != 0)
                {
                    query += " WHERE IdUser = " + id;
                }

                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    FavouriteProducts aux = new FavouriteProducts();

                    aux.Id = (int)data.Reader["Id"];
                    aux.IdUser = (int)data.Reader["IdUsuario"];
                    aux.ProductCode = (int)data.Reader["CodigoProducto"];

                    favList.Add(aux);

                }
                return favList;
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                data.closeConnection();
            }
        }

        public void Add(int idUser, int idProduct)
        {
            try
            {
                data.setQuery($"INSERT INTO Favoritos (IdUsuario, CodigoProducto) VALUES ({idUser},{idProduct})");
                data.executeRead();
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

        public void Delete(int idUser, int idProduct)
        {
            try
            {
                data.setQuery($"DELETE FROM Favoritos WHERE IdUsuario = {idUser} AND CodigoProducto = {idProduct}");
                data.executeRead();
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
