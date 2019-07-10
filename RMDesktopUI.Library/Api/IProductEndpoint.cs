﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
        Task<List<string>> GetAllProductNames(int ShopID);
        Task<ProductModel> GetByProductName(string productName);
        Task<HttpResponseMessage> InsertProduct(InsertProductModel productModel);
        Task<HttpResponseMessage> DeleteProduct(int id);
        Task<HttpResponseMessage> UpdateProduct(UpdateProductModel productModel);
        Task<List<ProductModel>> GetProductsByShopID(int ShopID);
    }
}