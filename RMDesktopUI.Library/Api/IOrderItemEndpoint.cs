using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IOrderItemEndpoint
    {
        Task<OrderItemModel> GetOrderItem(int ID);
        Task<List<OrderItemModel>> GetOrderItems(string OrderID);
        Task<HttpResponseMessage> InsertOrderItem(InsertOrderItemModel orderItemModel);
        Task<HttpResponseMessage> UpdateOrderItem(UpdateOrderItemModel updateOrderItemModel);
        Task<HttpResponseMessage> DeleteOrderItem(int ID);
        Task<HttpResponseMessage> DeleteOrderItems(string OrderID);
    }
}
