using RMDataManagerCore.Library.Models;
using System.Collections.Generic;


namespace RMDataManagerCore.Library.Interfaces
{
    public interface IBillItemData
    {
        void DeleteBillItems(string BillId);
        List<BillItemModel> GetBillItems(string BillId);
        void InsertBillItem(InsertBillItemModel billItemModel);
    }
}