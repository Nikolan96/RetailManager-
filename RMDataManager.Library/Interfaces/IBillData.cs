using System.Collections.Generic;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.Interfaces
{
    public interface IBillData
    {
        void DeleteBill(string ID);
        BillModel GetBill(string ID);
        List<BillModel> GetBills();
        void InsertBill(InsertBillModel billModel);
    }
}