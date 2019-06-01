using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
        Task<HttpResponseMessage> InsertProduct(InsertProductModel productModel);
        Task<HttpResponseMessage> DeleteProduct(int id);
    }
}