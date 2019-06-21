using System.Collections.Generic;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.Interfaces
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}