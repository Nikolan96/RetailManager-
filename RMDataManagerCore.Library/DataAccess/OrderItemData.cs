using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.DataAccess
{
    public class OrderItemData : IOrderItemData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public OrderItemData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public OrderItemModel GetOrderItem(int ID)
        {
            var p = new { ID = ID };

            var output = _sqlDataAccess.LoadOne<OrderItemModel, dynamic>("dbo.spGetOrderItemByID", p);

            return output;
        }

        public List<OrderItemModel> GetOrderItems(string OrderID)
        {
            var p = new { OrderID = OrderID };

            var output = _sqlDataAccess.LoadData<OrderItemModel, dynamic>("dbo.spGetOrderItemsByOrderID", p);

            return output;
        }

        public void InsertOrderItem(InsertOrderItemModel orderItemModel)
        {
            _sqlDataAccess.SaveData<InsertOrderItemModel, dynamic>("dbo.spInsertOrderItem", orderItemModel);
        }

        public void UpdateOrderItem(UpdateOrderItemModel updateOrderItemModel)
        {
            _sqlDataAccess.SaveData<UpdateOrderItemModel, dynamic>("dbo.spUpdateOrderItem", updateOrderItemModel);
        }

        public void DeleteOrderItem(int ID)
        {
            var p = new { ID = ID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteOrderItem", p);
        }

        public void DeleteOrderItems(string OrderID)
        {
            var p = new { OrderID = OrderID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteOrderItems", p);
        }
    }
}
