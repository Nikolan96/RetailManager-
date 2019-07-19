using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public class BillEndpoint : IBillEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public BillEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
  
        public async Task<BillModel> GetBill(string ID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Bill/" + ID))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<BillModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<BillModel>> GetBills()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Bill/GetBills"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<BillModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<BillModel>> GetBillsByShopID(int ShopID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Bill/GetBillsByShopID/" + ShopID))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<BillModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertBill(InsertBillModel billModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(billModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/Bill", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(string Id)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/Bill/" + Id);

            return response;
        }
    }
}
