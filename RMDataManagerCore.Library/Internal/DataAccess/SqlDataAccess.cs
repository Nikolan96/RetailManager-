using Dapper;
using RMDataManagerCore.Library.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;

namespace RMDataManagerCore.Library.Internal.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {

        public string GetConnectionString()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RMData;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;";
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters)
        {
            string connectionString = GetConnectionString();

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure).ToList();

                return rows;

            }
        }

        public T LoadOne<T, U>(string storedProcedure, U parameters)
        {
            string connectionString = GetConnectionString();

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void SaveData<T, U>(string storedProcedure, T parameters)
        {
            string connectionString = GetConnectionString();

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters,
                           commandType: CommandType.StoredProcedure);
            }
        }
    }
}
