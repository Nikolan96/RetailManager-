using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return output;
        }

        public List<string> GetAllProductNames()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<string, dynamic>("dbo.spGetAllProductNames", new { }, "RMData");

            return output;
        }

        public ProductModel GetByProductName(string productName)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { productName = productName };

            var output = sql.LoadOne<ProductModel, dynamic>("dbo.spGetProductByProductName", p, "RMData");

            return output;
        }

        public void InsertProducts(InsertProductModel productModel)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<InsertProductModel, dynamic>("dbo.spInsertProduct", productModel, "RMData");
        }

        public void DeleteProduct(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { id = id };

            sql.SaveData<dynamic, dynamic>("dbo.spDeleteProduct", p, "RMData");
        }

        public void UpdateProduct(UpdateProductModel productModel)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<UpdateProductModel, dynamic>("dbo.spUpdateProduct", productModel, "RMData");
        }
    }
}
