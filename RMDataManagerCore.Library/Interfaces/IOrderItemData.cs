using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Interfaces
{
    public interface IOrderItemData
    {
        OrderItemModel GetOrderItem(int ID);
        List<OrderItemModel> GetOrderItems(string OrderID);
        void InsertOrderItem(InsertOrderItemModel orderItemModel);
        void UpdateOrderItem(UpdateOrderItemModel updateOrderItemModel);
        void DeleteOrderItem(int ID);
        void DeleteOrderItems(string OrderID);
    }
}
