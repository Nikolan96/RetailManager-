using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class BillData
    {
        public BillModel GetBill(string ID)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { ID = ID };

            var output = sql.LoadOne<BillModel, dynamic>("dbo.spGetBill", p, "RMData");

            return output;
        }

        public List<BillModel> GetBills()
        {
            SqlDataAccess sql = new SqlDataAccess();
            
            var output = sql.LoadData<BillModel, dynamic>("dbo.spGetBills", new { }, "RMData");

            return output;
        }

        public void InsertBill(InsertBillModel billModel)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<InsertBillModel, dynamic>("dbo.spInsertBill", billModel, "RMData");
        }

        public void DeleteBill(string ID)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { ID = ID };

            sql.SaveData<dynamic, dynamic>("dbo.spDeleteBill", p, "RMData");
        }
    }
}
