using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Interfaces
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}
