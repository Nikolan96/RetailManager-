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
    public class BillItemEndpoint : IBillItemEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public BillItemEndpoint(IAPIHelper apiHelper)
        {
           _apiHelper = apiHelper;
        }

        public async Task<HttpResponseMessage> Delete(string BillId)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/BillItem?BillId=" + BillId);

            return response;
        }

        public async Task<List<BillItemModel>> GetBillItems(string BillId)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/BillItem/GetBillItems?BillId=" + BillId))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<BillItemModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertBillItem(InsertBillItemModel billItemModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(billItemModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/BillItem", stringContent);

            return response;
        }
    }
}
