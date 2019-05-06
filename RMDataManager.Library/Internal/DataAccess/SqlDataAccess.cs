using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess
    {       

        // Gets the connection string from configuration file of RMDataManager.
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        // Takes a connection to db, does a query and says this is a type of model I want each row to be.
        // Passes a stored procedure name and parameters generic ( U ).  
        // Returns a set of rows.
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        // Saves data, does not work currently, this is just a "Skeleton".
        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, 
                      commandType: CommandType.StoredProcedure);
            }
        }

    }
}
