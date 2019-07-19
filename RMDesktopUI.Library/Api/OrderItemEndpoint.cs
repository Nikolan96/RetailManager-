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
    public class OrderItemEndpoint : IOrderItemEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public OrderItemEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<OrderItemModel> GetOrderItem(int ID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/OrderItem/{ID}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<OrderItemModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<OrderItemModel>> GetOrderItems(string OrderID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/OrderItem/GetOrderItems/{OrderID}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<OrderItemModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertOrderItem(InsertOrderItemModel orderItemModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(orderItemModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/OrderItem", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateOrderItem(UpdateOrderItemModel updateOrderItemModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateOrderItemModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/OrderItem", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteOrderItem(int ID)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/OrderItem/" + ID);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteOrderItems(string OrderID)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/OrderItem/DeleteOrderItems/" + OrderID);

            return response;
        }

    }
}
