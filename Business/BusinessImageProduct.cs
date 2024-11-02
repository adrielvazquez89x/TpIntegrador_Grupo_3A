using DataAccessService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessService.DataAccessService;

namespace Business
{
    public class BusinessImageProduct
    {
        List<ImageProduct> listImages = new List<ImageProduct>();
        DataAccess data = new DataAccess();
        public List<ImageProduct> list(string code="")
        {

            try
            {
                string query = "SELECT Id, CodigoProducto, UrlImagen FROM ImagenesProductos";
                
                if (code != "")
                {
                   query += " WHERE CodigoProducto = '"+ code +"'";
                }
                data.setQuery(query);
                data.executeRead();

                while (data.Reader.Read())
                {
                    ImageProduct aux = new ImageProduct();
                    aux.Id = (int)data.Reader["Id"];
                    aux.CodProd =(string)data.Reader["CodigoProducto"];
                    aux.UrlImage = (string)data.Reader["UrlImagen"];

                    listImages.Add(aux);
                }
                return listImages;
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

        public string delete(string code)
        {
            try
            {
                data.setQuery("DELETE FROM ImagenesProductos WHERE CodigoProducto = '" + code + "'");

                data.executeRead();

                while (data.Reader.Read())
                {
                    ImageProduct aux = new ImageProduct();
                    aux.Id = (int)data.Reader["Id"];
                    aux.CodProd = (string)data.Reader["CodigoProducto"];
                    aux.UrlImage = (string)data.Reader["UrlImagen"];

                    listImages.Add(aux);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "Error al borrar las imagenes: " + ex.Message;
            }
            finally
            {
                data.closeConnection();
            }
        }
    }
}
