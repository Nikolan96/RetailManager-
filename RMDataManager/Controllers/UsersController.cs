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
    public class UsersController : ApiController
    {

        // GET: Users/Details/5
        public List<UserModel> GetById()
        {

            string userID = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();

            return data.GetUserById(userID);
        }

    }
}
