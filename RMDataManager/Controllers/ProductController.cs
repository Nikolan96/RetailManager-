using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Interfaces;
using RMDataManager.Library.Models;


namespace RMDataManager.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {
            return _productData.GetProducts();
        }

        [HttpGet]
        [Route("api/Product/ProductNames")]
        public List<string> GetAllProductNames()
        {
            return _productData.GetAllProductNames();
        }

        [HttpGet]
        [Route("api/Product/ProductName")]
        public ProductModel GetProductByProductName(string productName)
        {
            return _productData.GetByProductName(productName);
        }

        [HttpPost]
        public void Post(InsertProductModel productModel)
        {
            _productData.InsertProducts(productModel);
        }

        [HttpDelete]     
        public void Delete(int id)
        {
            _productData.DeleteProduct(id);
        }

        [HttpPut]
        public void Update(UpdateProductModel productModel)
        {
            _productData.UpdateProduct(productModel);
        }
    }
}
