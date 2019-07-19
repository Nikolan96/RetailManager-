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
    public class OrderController : ControllerBase
    {
        private readonly IOrderData _orderData;

        public OrderController(IOrderData orderData)
        {
            _orderData = orderData;
        }

        [HttpGet("{ID}")]
        public OrderModel GetBill(string ID)
        {
            return _orderData.GetOrderByID(ID);
        }

        [HttpGet("GetOrdersByShopID/{ShopID}")]
        public List<OrderModel> GetOrdersByShopID(int ShopID)
        {
            return _orderData.GetOrdersByShopID(ShopID);
        }

        [HttpPost]
        public void InsertOrder(OrderModel orderModel)
        {
            _orderData.InsertOrder(orderModel);
        }

        [HttpDelete("{ID}")]
        public void Delete(string ID)
        {
            _orderData.DeleteOrder(ID);
        }

        [HttpPut]
        public void ApproveOrder(string ID)
        {
            _orderData.ApproveOrder(ID);
        }
    }
}