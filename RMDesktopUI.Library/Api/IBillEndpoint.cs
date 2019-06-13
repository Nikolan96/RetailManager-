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

        Task<HttpResponseMessage> InsertBill(BillModel billModel);

        Task<HttpResponseMessage> Delete(int id);

    }
}
