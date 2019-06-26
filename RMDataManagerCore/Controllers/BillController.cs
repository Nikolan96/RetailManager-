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
    public class BillController : ControllerBase
    {
        private readonly IBillData _billData;

        public BillController(IBillData billData)
        {
            _billData = billData;
        }

        [HttpGet("{ID}")]
        public BillModel GetBill(string ID)
        {
            return _billData.GetBill(ID);
        }

        [HttpGet("GetBills")]
        public List<BillModel> GetBills()
        {
            return _billData.GetBills();
        }

        [HttpPost]
        public void InsertBill(InsertBillModel billModel)
        {
            _billData.InsertBill(billModel);
        }

        [HttpDelete("{ID}")]
        public void Delete(string ID)
        {
            _billData.DeleteBill(ID);
        }
    }
}