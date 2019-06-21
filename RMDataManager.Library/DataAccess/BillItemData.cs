using RMDataManager.Library.Interfaces;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class BillItemData : IBillItemData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public BillItemData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<BillItemModel> GetBillItems(string BillId)
        {
            var p = new { BillId = BillId };

            var output = _sqlDataAccess.LoadData<BillItemModel, dynamic>("dbo.spGetBillItems", p, "RMData");

            return output;
        }

        public void InsertBillItem(InsertBillItemModel billItemModel)
        {
            _sqlDataAccess.SaveData<InsertBillItemModel, dynamic>("dbo.spInsertBillItem", billItemModel, "RMData");
        }

        public void DeleteBillItems(string BillId)
        {
            var p = new { BillId = BillId };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteBillItem", p, "RMData");
        }
    }
}
