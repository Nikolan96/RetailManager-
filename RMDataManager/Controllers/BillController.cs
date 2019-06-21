using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Interfaces;
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
        private readonly IBillData _billData;

        public BillController(IBillData billData)
        {
            _billData = billData;
        }

        [HttpGet]
        public BillModel GetBill(string ID)
        {
            return _billData.GetBill(ID);
        }

        [HttpGet]
        [Route("api/Bill/GetBills")]
        public List<BillModel> GetBills()
        {
            return _billData.GetBills();
        }

        [HttpPost]
        public void InsertBill(InsertBillModel billModel)
        {
            _billData.InsertBill(billModel);
        }

        [HttpDelete]
        public void Delete(string ID)
        {
            _billData.DeleteBill(ID);
        }
    }
}
