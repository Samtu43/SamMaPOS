
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using EnterprisePOS.Data.Properties;

namespace EnterprisePOS.Data
{
    public class POSCartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class POSRepository
    {
        private readonly string connectionString =
            Settings.Default.PosDBConnectionString;

        public DataTable GetAvailableProducts()
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT
                    ProductId,
                    Sku,
                    Barcode,
                    Name,
                    Price,
                    StockQty
                FROM dbo.Products
                WHERE IsActive = 1
                  AND StockQty > 0
                ORDER BY Name";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            using (SqlDataAdapter adapter =
                new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            return table;
        }

        public DataTable SearchProducts(string keyword)
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT
                    ProductId,
                    Sku,
                    Barcode,
                    Name,
                    Price,
                    StockQty
                FROM dbo.Products
                WHERE IsActive = 1
                  AND StockQty > 0
                  AND (
                      Name LIKE '%' + @Keyword + '%'
                      OR Sku LIKE '%' + @Keyword + '%'
                      OR Barcode LIKE '%' + @Keyword + '%'
                  )
                ORDER BY Name";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.Add(
                    "@Keyword", SqlDbType.NVarChar, 100).Value =
                    (keyword ?? "").Trim();

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public DataRow GetProductById(int productId)
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT
                    ProductId,
                    Sku,
                    Barcode,
                    Name,
                    Price,
                    StockQty,
                    IsActive
                FROM dbo.Products
                WHERE ProductId = @ProductId";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.Add(
                    "@ProductId", SqlDbType.Int).Value =
                    productId;

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            if (table.Rows.Count == 0)
                return null;

            return table.Rows[0];
        }

        public int GetAvailableStock(int productId)
        {
            string query = @"
                SELECT StockQty
                FROM dbo.Products
                WHERE ProductId = @ProductId
                  AND IsActive = 1";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.Add(
                    "@ProductId", SqlDbType.Int).Value =
                    productId;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
        }

        public DataTable GetCustomers()
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT
                    CustomerId,
                    FullName
                FROM dbo.Customers
                ORDER BY FullName";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            using (SqlDataAdapter adapter =
                new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }

            return table;
        }

        public void CreateOrder(
            int userId,
            int? customerId,
            string paymentMethod,
            decimal amountPaid,
            decimal discountAmount,
            List<POSCartItem> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException(
                    "The shopping cart is empty.");

            XElement xml = new XElement(
                "Items",
                items.Select(item =>
                    new XElement(
                        "Item",
                        new XAttribute(
                            "ProductId",
                            item.ProductId),
                        new XAttribute(
                            "Quantity",
                            item.Quantity)
                    )
                )
            );

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(
                    "dbo.usp_Order_Create",
                    connection))
            {
                command.CommandType =
                    CommandType.StoredProcedure;

                command.CommandTimeout = 60;

                command.Parameters.Add(
                    "@UserId",
                    SqlDbType.Int).Value = userId;

                command.Parameters.Add(
                    "@CustomerId",
                    SqlDbType.Int).Value =
                    customerId.HasValue
                        ? (object)customerId.Value
                        : DBNull.Value;

                command.Parameters.Add(
                    "@PaymentMethod",
                    SqlDbType.NVarChar,
                    30).Value = paymentMethod;

                SqlParameter discountParameter =
                    command.Parameters.Add(
                        "@DiscountAmount",
                        SqlDbType.Decimal);

                discountParameter.Precision = 18;
                discountParameter.Scale = 2;
                discountParameter.Value = discountAmount;

                SqlParameter taxParameter =
                    command.Parameters.Add(
                        "@TaxRate",
                        SqlDbType.Decimal);

                taxParameter.Precision = 18;
                taxParameter.Scale = 4;
                taxParameter.Value = 0m;

                SqlParameter paidParameter =
                    command.Parameters.Add(
                        "@AmountPaid",
                        SqlDbType.Decimal);

                paidParameter.Precision = 18;
                paidParameter.Scale = 2;
                paidParameter.Value = amountPaid;

                command.Parameters.Add(
                    "@ItemsXml",
                    SqlDbType.Xml).Value =
                    xml.ToString(
                        SaveOptions.DisableFormatting);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
