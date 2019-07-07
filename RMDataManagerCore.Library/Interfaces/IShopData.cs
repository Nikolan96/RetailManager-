using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Interfaces
{
    public interface IShopData
    {
        ShopModel GetShopById(int ID);

        List<ShopModel> GetShops();

        List<int> GetShopIds();

        void UpdateShop(UpdateShopModel updateShopModel);

        void InsertShop(InsertShopModel ShopModel);

        void UpdateNumOfEmployees(UpdateShopNumOfEmployeesModel updateShopNumOfEmployeesModel);

        void DeleteShop(int ID);
    }
}
