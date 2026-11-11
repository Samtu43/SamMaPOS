
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;

namespace EnterprisePOS.Data
{
    public class ProductRepository
    {
        public List<Product> GetAllProducts()
        {
            using (var db = new PosDBDataDataContext())
            {
                var options = new DataLoadOptions();
                options.LoadWith<Product>(p => p.Category);
                db.LoadOptions = options;

                return db.Products.ToList();
            }
        }

        public Product GetProductById(int productId)
        {
            using (var db = new PosDBDataDataContext())
            {
                var options = new DataLoadOptions();
                options.LoadWith<Product>(p => p.Category);
                db.LoadOptions = options;

                return db.Products.FirstOrDefault(
                    p => p.ProductId == productId);
            }
        }

        public List<Product> SearchProducts(string keyword)
        {
            using (var db = new PosDBDataDataContext())
            {
                var options = new DataLoadOptions();
                options.LoadWith<Product>(p => p.Category);
                db.LoadOptions = options;

                if (string.IsNullOrWhiteSpace(keyword))
                    return db.Products.ToList();

                keyword = keyword.Trim();

                return db.Products
                    .Where(p => p.Name.Contains(keyword)
                             || p.Sku.Contains(keyword)
                             || (p.Barcode != null &&
                                 p.Barcode.Contains(keyword)))
                    .ToList();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Categories
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }

        public void AddProduct(
            string sku,
            string name,
            int categoryId,
            decimal price,
            decimal cost,
            int stockQty,
            int userId)
        {
            using (var db = new PosDBDataDataContext())
            using (var command = new SqlCommand(
                "dbo.usp_Product_Insert",
                (SqlConnection)db.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Sku", sku);
                command.Parameters.AddWithValue(
                    "@Barcode", DBNull.Value);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue(
                    "@CategoryId", categoryId);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Cost", cost);
                command.Parameters.AddWithValue(
                    "@StockQty", stockQty);
                command.Parameters.AddWithValue(
                    "@ReorderLevel", 5);
                command.Parameters.AddWithValue(
                    "@Image", DBNull.Value);
                command.Parameters.AddWithValue("@UserId", userId);

                db.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(
            int productId,
            string sku,
            string name,
            int categoryId,
            decimal price,
            decimal cost)
        {
            using (var db = new PosDBDataDataContext())
            {
                var existingProduct = db.Products
                    .FirstOrDefault(p => p.ProductId == productId);

                if (existingProduct == null)
                    throw new InvalidOperationException(
                        "Product not found.");

                string barcode = existingProduct.Barcode;
                int reorderLevel = existingProduct.ReorderLevel;
                bool isActive = existingProduct.IsActive;

                using (var command = new SqlCommand(
                    "dbo.usp_Product_Update",
                    (SqlConnection)db.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@ProductId", productId);
                    command.Parameters.AddWithValue("@Sku", sku);
                    command.Parameters.AddWithValue(
                        "@Barcode",
                        (object)barcode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue(
                        "@CategoryId", categoryId);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue(
                        "@ReorderLevel", reorderLevel);
                    command.Parameters.AddWithValue(
                        "@IsActive", isActive);
                    command.Parameters.AddWithValue(
                        "@Image", DBNull.Value);
                    command.Parameters.AddWithValue(
                        "@ChangeImage", false);

                    if (db.Connection.State != ConnectionState.Open)
                        db.Connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var db = new PosDBDataDataContext())
            using (var command = new SqlCommand(
                "dbo.usp_Product_Delete",
                (SqlConnection)db.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(
                    "@ProductId", productId);

                db.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
