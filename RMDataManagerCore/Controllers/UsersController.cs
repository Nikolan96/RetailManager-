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

        [HttpGet("GetUserByEmail/{email}")]
        public UserModel GetUserByEmail(string email)
        {
            return _userData.GetUserByEmail(email);
        }

        [HttpGet]
        public List<UserModel> GetUsers()
        {
            return _userData.GetUsers();
        }

        [HttpGet("GetUserRoles")]
        public List<string> GetUserRoles()
        {
            return _userData.GetUserRoles();
        }

        [HttpPost]
        public void Post(InsertUserModel userModel)
        {
            _userData.InsertUser(userModel);
        }

        [HttpPut]
        public void Update(UpdateUserModel userModel)
        {
            _userData.UpdateUser(userModel);
        }

        [HttpDelete("{ID}")]
        public void Delete(int ID)
        {
            _userData.DeleteUser(ID);
        }

    }
}