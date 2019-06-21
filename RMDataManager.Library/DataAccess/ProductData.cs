using RMDataManager.Library.Interfaces;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
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
            var output = _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return output;
        }

        public List<string> GetAllProductNames()
        {
            var output = _sqlDataAccess.LoadData<string, dynamic>("dbo.spGetAllProductNames", new { }, "RMData");

            return output;
        }

        public ProductModel GetByProductName(string productName)
        {
            var p = new { productName = productName };

            var output = _sqlDataAccess.LoadOne<ProductModel, dynamic>("dbo.spGetProductByProductName", p, "RMData");

            return output;
        }

        public void InsertProducts(InsertProductModel productModel)
        {
            _sqlDataAccess.SaveData<InsertProductModel, dynamic>("dbo.spInsertProduct", productModel, "RMData");
        }

        public void DeleteProduct(int id)
        {
            var p = new { id = id };

            _sqlDataAccess.SaveData<dynamic, dynamic>("dbo.spDeleteProduct", p, "RMData");
        }

        public void UpdateProduct(UpdateProductModel productModel)
        {
            _sqlDataAccess.SaveData<UpdateProductModel, dynamic>("dbo.spUpdateProduct", productModel, "RMData");
        }
    }
}
