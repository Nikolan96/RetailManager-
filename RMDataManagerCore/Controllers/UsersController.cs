using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;

namespace RMDataManagerCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserData _userData;

        public UsersController(IUserData userData)
        {
            _userData = userData;
        }

        [HttpGet]
        public UserModel GetById()
        {
            string UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userData.GetUserById(UserId).First();
        }
    }
}