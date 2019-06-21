using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Interfaces
{
    public interface ISqlDataAccess
    {          
        string GetConnectionString();
        List<T> LoadData<T, U>(string storedProcedure, U parameters);
        T LoadOne<T, U>(string storedProcedure, U parameters);
        void SaveData<T, U>(string storedProcedure, T parameters);
    }
}
