using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUserByEmail(string email);
        Task<List<string>> GetUserRoles();
        Task<HttpResponseMessage> InsertUser(InsertUserModel userModel);
        Task<HttpResponseMessage> UpdateUser(UpdateUserModel userModel);
        Task<HttpResponseMessage> DeleteUser(int ID);
    }
}
