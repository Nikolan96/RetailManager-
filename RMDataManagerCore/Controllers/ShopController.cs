using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManagerCore.Library.DataAccess;
using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;

namespace RMDataManagerCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopData _shopData;

        public ShopController(IShopData shopData)
        {
            _shopData = shopData;
        }

        [HttpGet("GetShopById/{ID}")]
        public ShopModel GetShopById(int ID)
        {
           return _shopData.GetShopById(ID);
        }

        [HttpGet]
        public List<ShopModel> GetShops()
        {
            return _shopData.GetShops();
        }

        [HttpGet("GetShopByAddress/{Address}")]
        public ShopModel GetShopByAddress(string Address)
        {
            return _shopData.GetShopByAddress(Address);
        }

        [HttpGet("GetShopIds")]
        public List<int> GetShopIds()
        {
            return _shopData.GetShopIds();
        }

        [HttpPut]
        public void UpdateShop(UpdateShopModel updateShopModel)
        {
            _shopData.UpdateShop(updateShopModel);
        }

        [HttpPost]
        public void InsertShop(InsertShopModel insertShopModel)
        {
            _shopData.InsertShop(insertShopModel);
        }

        [HttpPut("NumOfEmployees")]
        public void UpdateShopNumOfEmployees(UpdateShopNumOfEmployeesModel updateShopNumOfEmployeesModel)
        {
            _shopData.UpdateNumOfEmployees(updateShopNumOfEmployeesModel);
        }

        [HttpDelete("{ID}")]
        public void InsertShop(int ID)
        {
            _shopData.DeleteShop(ID);
        }
    }
}