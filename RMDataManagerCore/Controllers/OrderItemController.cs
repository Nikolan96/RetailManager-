using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;

namespace RMDataManagerCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemData _orderItemData;

        public OrderItemController(IOrderItemData orderItemData)
        {
            _orderItemData = orderItemData;
        }

        [HttpGet("{ID}")]
        public OrderItemModel GetOrderItem(int ID)
        {
            return _orderItemData.GetOrderItem(ID);
        }

        [HttpGet("GetOrderItems/{ShopID}")]
        public List<OrderItemModel> GetOrderItems(string ShopID)
        {
            return _orderItemData.GetOrderItems(ShopID);
        }

        [HttpPost]
        public void Post(InsertOrderItemModel insertOrderItemModel)
        {
            _orderItemData.InsertOrderItem(insertOrderItemModel);
        }

        [HttpPut]
        public void Update(UpdateOrderItemModel updateOrderItemModel)
        {
            _orderItemData.UpdateOrderItem(updateOrderItemModel);
        }

        [HttpDelete("{ID}")]
        public void Delete(int ID)
        {
            _orderItemData.DeleteOrderItem(ID);
        }

        [HttpDelete("DeleteOrderItems/{OrderID}")]
        public void DeleteOrderItems(string OrderID)
        {
            _orderItemData.DeleteOrderItems(OrderID);
        }
    }
}