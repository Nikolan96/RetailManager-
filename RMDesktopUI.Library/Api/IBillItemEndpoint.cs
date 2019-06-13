using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IBillItemEndpoint
    {

        Task<List<BillItemModel>> GetBillItems(string BillId);

        Task<HttpResponseMessage> InsertBillItem(InsertBillItemModel billItemModel);

        Task<HttpResponseMessage> Delete(string BillId);

    }
}
