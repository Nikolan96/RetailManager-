using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Interfaces
{
    public interface IUserData
    {
        UserModel GetUserByEmail(string email);
        List<UserModel> GetUsers();
        List<string> GetUserRoles();
        void UpdateUser(UpdateUserModel UpdateUserModel);
        void InsertUser(InsertUserModel userModel);
        void DeleteUser(int ID);
    }
}
