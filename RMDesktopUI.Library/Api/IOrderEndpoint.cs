using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IOrderEndpoint
    {
        Task<OrderModel> GetOrderByID(string ID);
        Task<List<OrderModel>> GetOrdersByShopID(int ShopID);
        Task<HttpResponseMessage> InsertOrder(InsertOrderModel orderModel);
        Task<HttpResponseMessage> DeleteOrder(string ID);
        Task<HttpResponseMessage> ApproveOrder(string ID);
    }
}
