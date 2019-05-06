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
        // Returns a UserModel based on Id.
        [HttpGet]
        public UserModel GetById()
        {
            // Gets ID from logged in user
            string id = RequestContext.Principal.Identity.GetUserId();

            // Creates a dependency for now, will be changed.
            UserData data = new UserData();

            return data.GetUserById(id).First();
        }     
    }
}
