using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Interfaces
{
    public interface IOrderData
    {
        OrderModel GetOrderByID(string ID);
        void DeleteOrder(string ID);
        List<OrderModel> GetOrdersByShopID(int ShopID);
        void InsertOrder(InsertOrderModel orderModel);
        void ApproveOrder(string ID);
    }
}
