
using System;
using System.Data;
using System.Data.SqlClient;
using EnterprisePOS.Data.Properties;

namespace EnterprisePOS.Data
{
    public class ReportRepository
    {
        private readonly string connectionString =
            Settings.Default.PosDBConnectionString;

        private DataTable ExecuteReport(string procedureName)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60;

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public DataTable GetDailySales(DateTime fromDate, DateTime toDate)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            using (SqlCommand command =
                new SqlCommand("usp_Report_DailySales", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60;

                command.Parameters.Add(
                    "@From", SqlDbType.DateTime).Value =
                    fromDate.Date;

                command.Parameters.Add(
                    "@To", SqlDbType.DateTime).Value =
                    toDate.Date.AddDays(1).AddTicks(-1);

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public DataTable GetTopProducts()
        {
            return ExecuteReport("usp_Report_TopProducts");
        }

        public DataTable GetSalesByCategory()
        {
            return ExecuteReport("usp_Report_SalesByCategory");
        }

        public DataTable GetLowStock()
        {
            return ExecuteReport("usp_Report_LowStock");
        }

        public DataTable GetDashboardSummary()
        {
            return ExecuteReport("usp_Report_Dashboard");
        }
    }
}
