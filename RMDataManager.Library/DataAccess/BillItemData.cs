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
        public List<BillItemModel> GetBillItems(int BillId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { BillId = BillId };

            var output = sql.LoadData<BillItemModel, dynamic>("dbo.spGetBillItems", p, "RMData");

            return output;
        }

        public void DeleteBillItems(int Billid)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Billid = Billid };

            sql.SaveData<dynamic, dynamic>("dbo.spDeleteBillItems", p, "RMData");
        }
    }
}
