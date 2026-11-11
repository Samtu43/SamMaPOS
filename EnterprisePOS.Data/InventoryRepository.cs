
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EnterprisePOS.Data
{
    public class InventoryRepository
    {
        public List<Product> GetAllProducts()
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Products
                    .Where(p => p.IsActive == true)
                    .OrderBy(p => p.Name)
                    .ToList();
            }
        }

        public List<Product> SearchProducts(string keyword)
        {
            using (var db = new PosDBDataDataContext())
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return db.Products
                        .Where(p => p.IsActive == true)
                        .OrderBy(p => p.Name)
                        .ToList();

                keyword = keyword.Trim();

                return db.Products
                    .Where(p => p.IsActive == true &&
                        (p.Name.Contains(keyword) ||
                         p.Sku.Contains(keyword) ||
                         (p.Barcode != null &&
                          p.Barcode.Contains(keyword))))
                    .OrderBy(p => p.Name)
                    .ToList();
            }
        }

        public Product GetProductById(int productId)
        {
            using (var db = new PosDBDataDataContext())
            {
                return db.Products.FirstOrDefault(
                    p => p.ProductId == productId &&
                         p.IsActive == true);
            }
        }

        public DataTable GetInventoryHistory()
        {
            using (var db = new PosDBDataDataContext())
            using (var command = new SqlCommand(
                @"SELECT
                    l.LogId,
                    l.CreatedAt,
                    p.Sku,
                    p.Name AS ProductName,
                    l.ChangeQty,
                    l.Reason,
                    u.FullName AS AdjustedBy
                  FROM dbo.InventoryLog l
                  INNER JOIN dbo.Products p
                      ON l.ProductId = p.ProductId
                  INNER JOIN dbo.Users u
                      ON l.UserId = u.UserId
                  ORDER BY l.CreatedAt DESC, l.LogId DESC",
                (SqlConnection)db.Connection))
            {
                using (var adapter = new SqlDataAdapter(command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public int AdjustStock(
            int productId,
            int changeQty,
            string reason,
            int userId)
        {
            using (var db = new PosDBDataDataContext())
            using (var command = new SqlCommand(
                "dbo.usp_Inventory_Adjust",
                (SqlConnection)db.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(
                    "@ProductId", productId);

                command.Parameters.AddWithValue(
                    "@ChangeQty", changeQty);

                command.Parameters.AddWithValue(
                    "@Reason", reason);

                command.Parameters.AddWithValue(
                    "@UserId", userId);

                db.Connection.Open();

                object result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    throw new InvalidOperationException(
                        "Stock adjustment did not return the new quantity.");

                return Convert.ToInt32(result);
            }
        }
    }
}
