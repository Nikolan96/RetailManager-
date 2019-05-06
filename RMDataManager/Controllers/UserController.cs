using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {          
        public List<UserModel> GetById()
        {
            // Gets ID from logged in user.
            string id = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();

            return data.GetUserById(id);
        }     
    }
}
