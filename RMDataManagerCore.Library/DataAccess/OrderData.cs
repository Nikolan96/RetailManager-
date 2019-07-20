using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.DataAccess
{
    public class OrderData : IOrderData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public OrderData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public OrderModel GetOrderByID(string ID)
        {
            var p = new { ID = ID };

            var output = _sqlDataAccess.LoadOne<OrderModel, dynamic>("dbo.spGetOrderByID", p);

            return output;
        }

        public List<OrderModel> GetOrdersByShopID(int ShopID)
        {
            var p = new { ShopID = ShopID };

            var output = _sqlDataAccess.LoadData<OrderModel, dynamic>("dbo.spGetOrdersByShopID", p);

            return output;
        }

        public void InsertOrder(InsertOrderModel orderModel)
        {
            _sqlDataAccess.SaveData<InsertOrderModel, dynamic>("dbo.spInsertOrder", orderModel);
        }

        public void DeleteOrder(string ID)
        {
            var p = new { ID = ID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteOrder", p);
        }

        public void ApproveOrder(string ID)
        {
            var p = new { ID = ID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spApproveOrder", p);
        }
    }
}
