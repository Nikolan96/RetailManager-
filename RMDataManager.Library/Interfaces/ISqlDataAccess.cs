using System.Collections.Generic;

namespace RMDataManager.Library.Interfaces
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        T LoadOne<T, U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveData<T, U>(string storedProcedure, T parameters, string connectionStringName);
    }
}