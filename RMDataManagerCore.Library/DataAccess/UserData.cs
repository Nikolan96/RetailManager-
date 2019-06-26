using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public UserData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<UserModel> GetUserById(string id)
        {
            var p = new { ID = id };

            var output = _sqlDataAccess.LoadData<UserModel, dynamic>("dbo.spUserLookup", p);

            return output;
        }
    }
}
