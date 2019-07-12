using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.DataAccess
{
    public class ShopData : IShopData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ShopData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public ShopModel GetShopById(int ID)
        {
            var p = new { ID = ID };

            var output = _sqlDataAccess.LoadOne<ShopModel, dynamic>("dbo.spGetShopById", p);

            return output;
        }

        public ShopModel GetShopByAddress(string Address)
        {
            var p = new { Address = Address };

            var output = _sqlDataAccess.LoadOne<ShopModel, dynamic>("dbo.spGetShopByAddress", p);

            return output;
        }

        public List<ShopModel> GetShops()
        {
            var output = _sqlDataAccess.LoadData<ShopModel, dynamic>("dbo.spGetShops", new { });

            return output;
        }

        public void InsertShop(InsertShopModel ShopModel)
        {
            _sqlDataAccess.SaveData<InsertShopModel, dynamic>("dbo.spInsertShop", ShopModel);
        }

        public void UpdateNumOfEmployees(UpdateShopNumOfEmployeesModel updateShopNumOfEmployeesModel)
        {
            _sqlDataAccess.SaveData<UpdateShopNumOfEmployeesModel, dynamic>("dbo.spUpdateShopNumOfEmployees", updateShopNumOfEmployeesModel);
        }

        public void UpdateShop(UpdateShopModel updateShopModel)
        {
            _sqlDataAccess.SaveData<UpdateShopModel, dynamic>("dbo.spUpdateShop", updateShopModel);
        }

        public void DeleteShop(int ID)
        {
            var p = new { ID = ID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteShop", p);
        }

        public List<int> GetShopIds()
        {
            var output = _sqlDataAccess.LoadData<int, dynamic>("dbo.spGetShopIds", new { });

            return output;
        }
    }
}
