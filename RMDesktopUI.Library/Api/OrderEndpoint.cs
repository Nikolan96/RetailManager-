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
    public class OrderEndpoint : IOrderEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public OrderEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<OrderModel> GetOrderByID(string ID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Order/" + ID))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<OrderModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<OrderModel>> GetOrdersByShopID(int ShopID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Order/GetOrdersByShopID/" + ShopID))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<OrderModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertOrder(InsertOrderModel orderModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(orderModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/Order", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> ApproveOrder(string ID)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(ID), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Order/" + ID, stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteOrder(string ID)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/Order/" + ID);

            return response;
        }

    }
}
