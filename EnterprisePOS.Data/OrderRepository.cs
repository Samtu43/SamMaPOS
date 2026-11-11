
using System;
using System.Data;
using System.Data.SqlClient;
using EnterprisePOS.Data.Properties;

namespace EnterprisePOS.Data
{
    public class OrderRepository
    {
        private readonly string connectionString =
            Settings.Default.PosDBConnectionString;

        public DataTable GetAllOrders()
        {
            return GetOrders("", DateTime.MinValue, DateTime.MaxValue);
        }

        public DataTable SearchOrders(string keyword)
        {
            return GetOrders(
                keyword,
                DateTime.MinValue,
                DateTime.MaxValue);
        }

        public DataTable GetOrders(
            string keyword,
            DateTime fromDate,
            DateTime toDate)
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT
                    o.OrderId,
                    o.OrderNumber,
                    o.OrderDate,
                    ISNULL(c.FullName, 'Walk-in Customer') AS CustomerName,
                    u.FullName AS CashierName,
                    o.Subtotal,
                    o.TaxAmount,
                    o.DiscountAmount,
                    o.Total,
                    o.PaymentMethod,
                    o.AmountPaid,
                    o.ChangeDue,
                    o.Status
                FROM dbo.Orders o
                LEFT JOIN dbo.Customers c
                    ON o.CustomerId = c.CustomerId
                INNER JOIN dbo.Users u
                    ON o.UserId = u.UserId
                WHERE
                    (@Keyword = '' OR o.OrderNumber LIKE '%' + @Keyword + '%')
                    AND (@FromDate IS NULL OR o.OrderDate >= @FromDate)
                    AND (@ToDate IS NULL OR o.OrderDate < @ToDate)
                ORDER BY o.OrderDate DESC, o.OrderId DESC";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.Add(
                    "@Keyword", SqlDbType.NVarChar, 100).Value =
                    (keyword ?? "").Trim();

                command.Parameters.Add(
                    "@FromDate", SqlDbType.DateTime).Value =
                    fromDate == DateTime.MinValue
                        ? (object)DBNull.Value
                        : fromDate.Date;

                command.Parameters.Add(
                    "@ToDate", SqlDbType.DateTime).Value =
                    toDate == DateTime.MaxValue
                        ? (object)DBNull.Value
                        : toDate.Date.AddDays(1);

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public DataTable GetOrderDetails(int orderId)
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT
                    od.OrderDetailId,
                    od.ProductId,
                    p.Sku,
                    p.Name AS ProductName,
                    od.Quantity,
                    od.UnitPrice,
                    od.LineTotal
                FROM dbo.OrderDetails od
                INNER JOIN dbo.Products p
                    ON od.ProductId = p.ProductId
                WHERE od.OrderId = @OrderId
                ORDER BY od.OrderDetailId";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.Add(
                    "@OrderId", SqlDbType.Int).Value = orderId;

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public DataRow GetOrderById(int orderId)
        {
            string query = @"
                SELECT
                    o.OrderId,
                    o.OrderNumber,
                    o.OrderDate,
                    o.CustomerId,
                    ISNULL(c.FullName, 'Walk-in Customer') AS CustomerName,
                    o.UserId,
                    u.FullName AS CashierName,
                    o.Subtotal,
                    o.TaxAmount,
                    o.DiscountAmount,
                    o.Total,
                    o.PaymentMethod,
                    o.AmountPaid,
                    o.ChangeDue,
                    o.Status
                FROM dbo.Orders o
                LEFT JOIN dbo.Customers c
                    ON o.CustomerId = c.CustomerId
                INNER JOIN dbo.Users u
                    ON o.UserId = u.UserId
                WHERE o.OrderId = @OrderId";

            DataTable table = new DataTable();

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(query, connection))
            {
                command.Parameters.Add(
                    "@OrderId", SqlDbType.Int).Value = orderId;

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
    }
}
