using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class BillData
    {
        public BillModel GetBill()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadOne<BillModel, dynamic>("dbo.spGetBill", new { }, "RMData");

            return output;
        }

        public List<BillModel> GetBills()
        {
            SqlDataAccess sql = new SqlDataAccess();
            
            var output = sql.LoadData<BillModel, dynamic>("dbo.spGetBills", new { }, "RMData");

            return output;
        }

        public void DeleteBill(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { id = id };

            sql.SaveData<dynamic, dynamic>("dbo.spDeleteBill", p, "RMData");
        }
    }
}
