using Newtonsoft.Json;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public class UserEndpoint : IUserEndpoint
    {
        private IAPIHelper _apiHelper;

        public UserEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Users/GetUserByEmail/{email}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<UserModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<UserModel>> GetUsers()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Users"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<UserModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<string>> GetUserRoles()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Users/GetUserRoles"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<string>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertUser(InsertUserModel userModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/Users", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateUser(UpdateUserModel userModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Users", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteUser(int ID)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/Users/" + ID);

            return response;
        }
    }
}
