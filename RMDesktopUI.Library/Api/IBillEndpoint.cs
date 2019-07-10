using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IBillEndpoint
    {
        Task<BillModel> GetBill(string ID);

        Task<List<BillModel>> GetBills();

        Task<HttpResponseMessage> InsertBill(InsertBillModel billModel);

        Task<HttpResponseMessage> Delete(string id);

        Task<List<BillModel>> GetBillsByShopID(int ShopID);

    }
}
