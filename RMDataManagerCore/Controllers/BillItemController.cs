using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;

namespace RMDataManagerCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillItemController : ControllerBase
    {
        private readonly IBillItemData _billItemData;

        public BillItemController(IBillItemData billItemData)
        {
            _billItemData = billItemData;
        }

        [HttpGet("{BillId}")]
        public List<BillItemModel> GetBills(string BillId)
        {
            return _billItemData.GetBillItems(BillId);
        }

        [HttpPost]
        public void InsertBillItem(InsertBillItemModel billItemModel)
        {
            _billItemData.InsertBillItem(billItemModel);
        }

        [HttpDelete("{BillId}")]
        public void Delete(string BillId)
        {
            _billItemData.DeleteBillItems(BillId);
        }
    }
}