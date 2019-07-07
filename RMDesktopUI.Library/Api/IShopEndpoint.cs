using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IShopEndpoint
    {
        Task<ShopModel> GetShopById(int ID);
        Task<List<ShopModel>> GetShops();
        Task<List<int>> GetShopIds();
        Task<HttpResponseMessage> UpdateShop(UpdateShopModel updateShopModel);
        Task<HttpResponseMessage> InsertShop(InsertShopModel insertShopModel);
        Task<HttpResponseMessage> UpdateShopNumOfEmployees(UpdateShopNumOfEmployeesModel updateShopNumOfEmployeesModel);
        Task<HttpResponseMessage> DeleteShop(int ID);
    }
}
