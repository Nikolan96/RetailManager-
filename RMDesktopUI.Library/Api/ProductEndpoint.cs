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
    public class ProductEndpoint : IProductEndpoint
    {
        private IAPIHelper _apiHelper;

        public ProductEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProductModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<ProductModel> GetProductByID(string ID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Product/{ID}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ProductModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<string>> GetAllProductNames(int ShopID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product/ProductNames/" + ShopID))
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

        public async Task<ProductModel> GetByProductName(string productName)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product/ProductName/" + productName))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ProductModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<ProductModel>> GetProductsByShopID(int ShopID)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product/GetProductsByShopID/" + ShopID))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProductModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<HttpResponseMessage> InsertProduct(InsertProductModel productModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(productModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/api/Product", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteProduct(string id)
        {
            HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync("/api/Product/"+id);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateProduct(UpdateProductModel productModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(productModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Product", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateProductQuantitySold(UpdateProductQuantityModel updateModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Product/Sold", stringContent);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateProductQuantityCanceled(UpdateProductQuantityModel updateModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync("/api/Product/Canceled", stringContent);

            return response;
        }
    }
}
