﻿using RMDataManager.Library.DataAccess;
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
        [HttpGet]
        public List<BillItemModel> GetBills(string BillId)
        {
            BillItemData data = new BillItemData();

            return data.GetBillItems(BillId);
        }

        [HttpPost]
        public void InsertBillItem(InsertBillItemModel billItemModel)
        {
            BillItemData data = new BillItemData();

            data.InsertBillItem(billItemModel);
        }

        [HttpDelete]
        public void Delete(string BillId)
        {
            BillItemData data = new BillItemData();

            data.DeleteBillItems(BillId);
        }
    }
}
