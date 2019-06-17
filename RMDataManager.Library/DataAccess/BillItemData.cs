using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class BillItemData
    {
        public List<BillItemModel> GetBillItems(string BillId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { BillId = BillId };

            var output = sql.LoadData<BillItemModel, dynamic>("dbo.spGetBillItems", p, "RMData");

            return output;
        }

        public void InsertBillItem(InsertBillItemModel billItemModel)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<InsertBillItemModel, dynamic>("dbo.spInsertBillItem", billItemModel, "RMData");
        }

        public void DeleteBillItems(string BillId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { BillId = BillId };

            sql.SaveData<dynamic, dynamic>("dbo.spDeleteBillItem", p, "RMData");
        }
    }
}
