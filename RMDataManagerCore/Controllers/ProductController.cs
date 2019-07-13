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

        [HttpGet("ProductNames/{ShopID}")]
        public List<string> GetAllProductNamesByShopId(int ShopID)
        {
            return _productData.GetAllProductNames(ShopID);
        }

        [HttpGet("{ID}")]
        public ProductModel GetProductByID(string ID)
        {
            return _productData.GetProductByID(ID);
        }

        [HttpGet("GetProductsByShopID/{ShopID}")]
        public List<ProductModel> GetProductsByShopID(int ShopID)
        {
            return _productData.GetProductsByShopID(ShopID);
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
        public void Delete(string id)
        {
            _productData.DeleteProduct(id);
        }

        [HttpPut]
        public void Update(UpdateProductModel productModel)
        {
            _productData.UpdateProduct(productModel);
        }

        [HttpPut("Sold")]
        public void UpdateQuantitySold(UpdateProductQuantityModel updateModel)
        {
            _productData.UpdateProductQuantitySold(updateModel);
        }

        [HttpPut("Canceled")]
        public void UpdateQuantityCanceled(UpdateProductQuantityModel updateModel)
        {
            _productData.UpdateProductQuantityCanceled(updateModel);
        }
    }
}