using RMDesktopUI.Library.Models;
using RMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text; 
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient;
        private ILoggedInUserModel _loggedInUserModel;

        public APIHelper(ILoggedInUserModel loggedInUserModel)
        {
            InitializeClient();
            _loggedInUserModel = loggedInUserModel;
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        private void InitializeClient()
        {
            // Loads api url into api variable.
            string api = ConfigurationManager.AppSettings["apiCore"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            // Clears headers so it starts from a blank slate.
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            // Looking for json datapack
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Creates the data that will be sent to API endpoint. Calls Token and gets back a resposne.
        //public async Task<AuthenticatedUser> Authenticate(string username, string password)
        //{
        //    var data = new FormUrlEncodedContent(new[]
        //    {
        //       new KeyValuePair<string, string>("grant_type", "password"),
        //       new KeyValuePair<string, string>("email", username),
        //       new KeyValuePair<string, string>("password", password),
        //    });

        //    using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
        //    {
        //        // If it succedes it gets the information from content and puts it inot AuthenticatedUser model
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
        //            return result;
        //        }
        //        else
        //        {
        //            // Returns why it failed.
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}

        //// Method which can fuck things up.
        //public async Task GetLoggedInUserInfo(string token)
        //{
        //    _apiClient.DefaultRequestHeaders.Clear();
        //    _apiClient.DefaultRequestHeaders.Accept.Clear(); 
        //    _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

        //    using (HttpResponseMessage response = await _apiClient.GetAsync("/api/Users"))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<LoggedInUserModel>();

        //            _loggedInUserModel.CreatedDate = result.CreatedDate;
        //            _loggedInUserModel.EmailAddress = result.EmailAddress;
        //            _loggedInUserModel.FirstName = result.FirstName;
        //            _loggedInUserModel.ID = result.ID;
        //            _loggedInUserModel.LastName = result.LastName;
        //            _loggedInUserModel.Token = token;
                    
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}
    }
}
