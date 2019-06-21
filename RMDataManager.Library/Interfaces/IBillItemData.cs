using System.Collections.Generic;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.Interfaces
{
    public interface IBillItemData
    {
        void DeleteBillItems(string BillId);
        List<BillItemModel> GetBillItems(string BillId);
        void InsertBillItem(InsertBillItemModel billItemModel);
    }
}