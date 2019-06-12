using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    public class BillController : ApiController
    {
        [HttpGet]
        [Route("api/Bill/GetBill")]
        public BillModel GetBill()
        {
            BillData data = new BillData();

            return data.GetBill();
        }

        [HttpGet]
        [Route("api/Bill/GetBills")]
        public List<BillModel> GetBills()
        {
            BillData data = new BillData();

            return data.GetBills();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            BillData data = new BillData();

            data.DeleteBill(id);
        }
    }
}
