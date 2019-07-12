using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public class ShopEndpoint : IShopEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public ShopEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<ShopModel> GetShopById(int ID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Shop/GetShopById/{ID}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ShopModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<ShopModel> GetShopByAddress(string Address)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Shop/GetShopByAddress/{Address}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ShopModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<ShopModel>> GetShops()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Shop"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ShopModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<int>> GetShopIds()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Shop/GetShopIds"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<int>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertShop(InsertShopModel insertShopModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(insertShopModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/Shop", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateShop(UpdateShopModel updateShopModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateShopModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Shop", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateShopNumOfEmployees(UpdateShopNumOfEmployeesModel updateShopNumOfEmployeesModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateShopNumOfEmployeesModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Shop/NumOfEmployees", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteShop(int ID)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/Shop/" + ID);

            return response;
        }
    }
}
