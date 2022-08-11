using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib
{
    public class LocalMerchandiserDbContext : ILocalMerchandiserDbContext
    {
        private readonly SqlConnection _connection;

        public LocalMerchandiserDbContext(string connectionString)
        {
            _connection = CreateConnection(connectionString);
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString empty");
            }

            return new SqlConnection(connectionString);
        }

        public IDataReader ExecuteReader(string query, ICollection<SqlParameter> parameters)
        {
            _connection.Open();

            SqlCommand command = new SqlCommand(query, _connection);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command.ExecuteReader();
        }
    }

    public interface ILocalMerchandiserDbContext
    {
        IDataReader ExecuteReader(string query, ICollection<SqlParameter> parameters);
    }

}
