using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;


namespace RMDataManager.Controllers
{
    //[Authorize]
    public class ProductController : ApiController
    {
        [HttpGet]
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }

        [HttpPost]
        public void Post(InsertProductModel productModel)
        {
            ProductData data = new ProductData();

            data.InsertProducts(productModel);
        }

        [HttpDelete]     
        public void Delete(int id)
        {
            ProductData data = new ProductData();

            data.DeleteProduct(id);
        }
    }
}
