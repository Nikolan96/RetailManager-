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

        public UserModel GetUserByEmail(string email)
        {
            var p = new { EmailAddress = email };

            var output = _sqlDataAccess.LoadOne<UserModel, dynamic>("dbo.spGetUserByEmail", p);

            return output;
        }

        public List<UserModel> GetUsers()
        {
            var output = _sqlDataAccess.LoadData<UserModel, dynamic>("dbo.spGetUsers", new { });

            return output;
        }

        public void UpdateUser(UpdateUserModel updateUserModel)
        {
            _sqlDataAccess.SaveData<UpdateUserModel, dynamic>("dbo.spUpdateUser", updateUserModel);
        }

        public void InsertUser(InsertUserModel UserModel)
        {
            _sqlDataAccess.SaveData<InsertUserModel, dynamic>("dbo.spInsertUser", UserModel);
        }

        public List<string> GetUserRoles()
        {
            return _sqlDataAccess.LoadData<string, dynamic>("dbo.spGetUserRoles", new { });
        }

        public void DeleteUser(int ID)
        {
            var p = new { ID = ID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteUser", p);
        }
    }
}
