using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
        Task<List<string>> GetAllProductNames(int ShopID);
        Task<ProductModel> GetByProductName(string productName);
        Task<HttpResponseMessage> InsertProduct(InsertProductModel productModel);
        Task<HttpResponseMessage> DeleteProduct(string id);
        Task<HttpResponseMessage> UpdateProduct(UpdateProductModel productModel);
        Task<HttpResponseMessage> UpdateProductQuantitySold(UpdateProductQuantityModel updateModel);
        Task<HttpResponseMessage> UpdateProductQuantityCanceled(UpdateProductQuantityModel updateModel);
        Task<List<ProductModel>> GetProductsByShopID(int ShopID);
        Task<ProductModel> GetProductByID(string ID);
    }
}