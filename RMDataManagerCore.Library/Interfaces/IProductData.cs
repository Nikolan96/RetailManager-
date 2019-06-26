using RMDataManagerCore.Library.Models;
using System.Collections.Generic;


namespace RMDataManagerCore.Library.Interfaces
{
    public interface IProductData
    {
        void DeleteProduct(int id);
        List<string> GetAllProductNames();
        ProductModel GetByProductName(string productName);
        List<ProductModel> GetProducts();
        void InsertProducts(InsertProductModel productModel);
        void UpdateProduct(UpdateProductModel productModel);
    }
}