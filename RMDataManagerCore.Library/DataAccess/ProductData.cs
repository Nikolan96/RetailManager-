using RMDataManagerCore.Library.Interfaces;
using RMDataManagerCore.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManagerCore.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ProductData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<ProductModel> GetProducts()
        {
            var output = _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { });

            return output;
        }

        public ProductModel GetProductByID(string ID)
        {
            var p = new { ID = ID };

            var output = _sqlDataAccess.LoadOne<ProductModel, dynamic>("dbo.spGetProductByID", p);

            return output;
        }

        public List<string> GetAllProductNames(int ShopID)
        {
            var p = new { ShopID = ShopID };

            var output = _sqlDataAccess.LoadData<string, dynamic>("dbo.spGetAllProductNames", p);

            return output;
        }

        public ProductModel GetByProductName(string productName)
        {
            var p = new { productName = productName };

            var output = _sqlDataAccess.LoadOne<ProductModel, dynamic>("dbo.spGetProductByProductName", p);

            return output;
        }

        public void InsertProducts(InsertProductModel productModel)
        {
            _sqlDataAccess.SaveData<InsertProductModel, dynamic>("dbo.spInsertProduct", productModel);
        }

        public void DeleteProduct(string id)
        {
            var p = new { id = id };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteProduct", p);
        }

        public void UpdateProduct(UpdateProductModel productModel)
        {
            _sqlDataAccess.SaveData<UpdateProductModel, dynamic>("dbo.spUpdateProduct", productModel);
        }

        public List<ProductModel> GetProductsByShopID(int ShopID)
        {
            var p = new { ShopID = ShopID };

            var output = _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.spGetProductsByShopID", p);

            return output;
        }

        public void UpdateProductQuantitySold(UpdateProductQuantityModel updateModel)
        {
            _sqlDataAccess.SaveData<UpdateProductQuantityModel, dynamic>("dbo.spUpdateProductQuantitySold", updateModel);
        }

        public void UpdateProductQuantityCanceled(UpdateProductQuantityModel updateModel)
        {
            _sqlDataAccess.SaveData<UpdateProductQuantityModel, dynamic>("dbo.spUpdateProductQuantityCanceled", updateModel);
        }

        public int GetQuantityOfProductByName(string ProductName)
        {
            var p = new { ProductName = ProductName };

            var output = _sqlDataAccess.LoadOne<int, dynamic>("spGetQuantityOfProductByName", p);

            return output;
        }

        public List<ProductNameQuantityModel> GetAllProductNamesAndQuantities(int ShopID)
        {
            var p = new { ShopID = ShopID };

            var output = _sqlDataAccess.LoadData<ProductNameQuantityModel, dynamic>("spGetAllProductNamesAndQuantities", p);

            return output;
        }
    }
}
