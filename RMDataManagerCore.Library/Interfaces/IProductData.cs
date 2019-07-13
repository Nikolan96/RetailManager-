using RMDataManagerCore.Library.Models;
using System.Collections.Generic;


namespace RMDataManagerCore.Library.Interfaces
{
    public interface IProductData
    {
        void DeleteProduct(string ID);
        List<string> GetAllProductNames(int ShopID);
        ProductModel GetByProductName(string productName);
        List<ProductModel> GetProducts();
        void InsertProducts(InsertProductModel productModel);
        void UpdateProduct(UpdateProductModel productModel);
        List<ProductModel> GetProductsByShopID(int ShopID);
        void UpdateProductQuantitySold(UpdateProductQuantityModel updateModel);
        void UpdateProductQuantityCanceled(UpdateProductQuantityModel updateModel);
        ProductModel GetProductByID(string ID);
    }
}