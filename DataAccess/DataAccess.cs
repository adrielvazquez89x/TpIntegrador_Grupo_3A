using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
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
            private SqlTransaction _transaction;

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
                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                    }
                    _command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
                finally
                {
                    // Solo cerrar la conexión si no hay una transacción activa
                    if (_transaction == null && _connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            public int ActionScalar()
            {
                try
                {
                    if(_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                    }
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

            public void BeginTransaction()
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                _transaction = _connection.BeginTransaction();
                _command.Transaction = _transaction;
            }

            
            public void CommitTransaction()
            {
                if (_transaction != null)
                {
                    _transaction.Commit();
                    _transaction = null;
                    _command.Transaction = null;
                }

                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            
            public void RollbackTransaction()
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction = null;
                    _command.Transaction = null;
                }

                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }


        }
    }

}
