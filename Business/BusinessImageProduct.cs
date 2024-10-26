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
        public List<ImageProduct> list(string code="")
        {
            List<ImageProduct> list = new List<ImageProduct>();
            DataAccess data = new DataAccess();

            try
            {
                string query = "SELECT Id, CodigoProducto, UrlImagen FROM ImagenesProductos";
                data.setQuery(query);

                if (code != "")
                {
                    data.setQuery(query += " WHERE CodigoProducto = @CodigoProducto");
                    data.setParameter("@CodigoProducto", code);
                }

                data.executeRead();

                while (data.Reader.Read())
                {
                    ImageProduct aux = new ImageProduct();
                    aux.Id = (int)data.Reader["Id"];
                    aux.CodProd =(string)data.Reader["CodigoProducto"];
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
