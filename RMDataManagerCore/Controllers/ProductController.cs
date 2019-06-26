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
    public class ProductController : ControllerBase
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

        [HttpGet("ProductNames")]
        public List<string> GetAllProductNames()
        {
            return _productData.GetAllProductNames();
        }

        [HttpGet("ProductName/{productName}")]
        public ProductModel GetProductByProductName(string productName)
        {
            return _productData.GetByProductName(productName);
        }

        [HttpPost]
        public void Post(InsertProductModel productModel)
        {
            _productData.InsertProducts(productModel);
        }

        [HttpDelete("{id}")]
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