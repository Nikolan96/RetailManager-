using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Interfaces;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class BillItemController : ApiController
    {
        private readonly IBillItemData _billItemData;

        public BillItemController(IBillItemData billItemData)
        {
            _billItemData = billItemData;
        }

        [HttpGet]
        public List<BillItemModel> GetBills(string BillId)
        {
            return _billItemData.GetBillItems(BillId);
        }

        [HttpPost]
        public void InsertBillItem(InsertBillItemModel billItemModel)
        {
            _billItemData.InsertBillItem(billItemModel);
        }

        [HttpDelete]
        public void Delete(string BillId)
        {
            _billItemData.DeleteBillItems(BillId);
        }
    }
}
