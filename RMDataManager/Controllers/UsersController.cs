using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Interfaces;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("/Users")]
    public class UsersController : ApiController
    {
        private readonly IUserData _userData;

        public UsersController(IUserData userData)
        {
            _userData = userData;
        }

        [HttpGet] 
        public UserModel GetById()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();

            return _userData.GetUserById(userID).First();
        }
    }
}
