using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class BillController : ApiController
    {
        [HttpGet]
        public BillModel GetBill(string ID)
        {
            BillData data = new BillData();

            return data.GetBill(ID);
        }

        [HttpGet]
        [Route("api/Bill/GetBills")]
        public List<BillModel> GetBills()
        {
            BillData data = new BillData();

            return data.GetBills();
        }

        [HttpPost]
        public void InsertBill(InsertBillModel billModel)
        {
            BillData data = new BillData();

            data.InsertBill(billModel);
        }

        [HttpDelete]
        public void Delete(string ID)
        {
            BillData data = new BillData();

            data.DeleteBill(ID);
        }
    }
}
