using RMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;

        public APIHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            // Loads api url into api variable.
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            // Clears headers so it starts from a blank slate.
            apiClient.DefaultRequestHeaders.Accept.Clear();
            // Looking for json datapack
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Creates the data that will be sent to API endpoint. Calls Token and gets back a resposne.
        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string, string>("grant_type", "password"),
               new KeyValuePair<string, string>("username", username),
               new KeyValuePair<string, string>("password", password),

            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                // If it succedes it gets the information from content and puts it inot AuthenticatedUser model
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    // Returns why it failed.
                    throw new Exception(response.ReasonPhrase);
                }
                
            }
        }
    }
}
