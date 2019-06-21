using RMDataManager.Library.Interfaces;
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
    public class BillData : IBillData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public BillData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public BillModel GetBill(string ID)
        {
            var p = new { ID = ID };

            var output = _sqlDataAccess.LoadOne<BillModel, dynamic>("dbo.spGetBill", p, "RMData");

            return output;
        }

        public List<BillModel> GetBills()
        {
            var output = _sqlDataAccess.LoadData<BillModel, dynamic>("dbo.spGetBills", new { }, "RMData");

            return output;
        }

        public void InsertBill(InsertBillModel billModel)
        {
            _sqlDataAccess.SaveData<InsertBillModel, dynamic>("dbo.spInsertBill", billModel, "RMData");
        }

        public void DeleteBill(string ID)
        {
            var p = new { ID = ID };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteBill", p, "RMData");
        }
    }
}
