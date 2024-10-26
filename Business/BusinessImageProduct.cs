using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessImageProduct
    {
        public List<ImageProduct> list(int id = 0)
        {
            List<ImageProduct> list = new List<ImageProduct>();
            DataAccess data = new DataAccess();

            try
            {
                string query = "SELECT Id, IdProducto, UrlImagen FROM ImagenesProductos";
                data.setQuery(query);

                if (id != 0)
                {
                    data.setQuery(query += " WHERE IdProducto = @IdProducto");
                    data.setParameter("@IdProducto", id);
                }

                data.executeRead();

                while (data.Reader.Read())
                {
                    ImageProduct aux = new ImageProduct();
                    aux.Id = (int)data.Reader["Id"];
                    aux.IdProduct = (int)data.Reader["IdProducto"];
                    aux.UrlImage = (string)data.Reader["UrlImagen"];

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
    }
}
