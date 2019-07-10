using RMDataManagerCore.Library.Models;
using System.Collections.Generic;


namespace RMDataManagerCore.Library.Interfaces
{
    public interface IBillData
    {
        void DeleteBill(string ID);
        BillModel GetBill(string ID);
        List<BillModel> GetBills();
        void InsertBill(InsertBillModel billModel);
        List<BillModel> GetBillsByShopID(int ShopID);
    }
}