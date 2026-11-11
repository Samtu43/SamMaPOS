
using System;
using System.Data;

namespace EnterprisePOS.Data
{
    public class DashboardRepository
    {
        public DataTable GetDashboardStatistics()
        {
            return ExecuteTable("dbo.usp_Report_Dashboard", true);
        }

        public DataTable GetLowStock()
        {
            return ExecuteTable("dbo.usp_Report_LowStock", true);
        }

        public DataTable GetRecentOrders()
        {
            return ExecuteTable(
                @"SELECT TOP (10)
                    o.OrderNumber,
                    o.OrderDate,
                    u.FullName AS Cashier,
                    ISNULL(c.FullName, 'Walk-in') AS Customer,
                    o.Total,
                    o.PaymentMethod,
                    o.Status
                  FROM dbo.Orders o
                  INNER JOIN dbo.Users u ON o.UserId = u.UserId
                  LEFT JOIN dbo.Customers c
                    ON o.CustomerId = c.CustomerId
                  ORDER BY o.OrderDate DESC, o.OrderId DESC",
                false
            );
        }

        private DataTable ExecuteTable(
            string commandText,
            bool storedProcedure)
        {
            using (var db = new PosDBDataDataContext())
            using (var command = db.Connection.CreateCommand())
            {
                command.CommandText = commandText;

                command.CommandType = storedProcedure
                    ? CommandType.StoredProcedure
                    : CommandType.Text;

                db.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
        }
    }
}
