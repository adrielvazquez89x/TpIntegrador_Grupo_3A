using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessService
{
    namespace DataAccessService
    {
        public class DataAccess : IDisposable
        {
            private SqlConnection _connection;
            private SqlCommand _command;
            private SqlDataReader _reader;

            public SqlDataReader Reader => _reader;

            public DataAccess()
            {
                _connection = new SqlConnection($"server=.\\SQLEXPRESS; database = LocalRopa_DB; integrated security = true");
                _command = new SqlCommand { Connection = _connection };
            }

            public void setQuery(string query)
            {
                _command.CommandType = System.Data.CommandType.Text;
                _command.CommandText = query;
            }

            public void executeRead()
            {
                _command.Connection = _connection;
                try
                {
                    _connection.Open();
                    _reader = _command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            public void executeAction()
            {
                try
                {
                    _connection.Open();
                    _command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            public int ActionScalar()
            {
                try
                {
                    _connection.Open();
                    return (int)_command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            public void setParameter(string name, object value)
            {
                _command.Parameters.AddWithValue(name, value);
            }

            public void clearParams()
            {
                _command.Parameters.Clear();
            }

            public void closeConnection()
            {
                
                _reader?.Close();
                _connection.Close();
            }

            public void Dispose()
            {
                closeConnection();
                _command.Dispose();
                _connection.Dispose();
            }
        }
    }

}
